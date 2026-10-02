/*
# Fix M13 Eligible Branches + Walk-in Detection Logic

## Changes
1. Remove Sans Vibes Millennium Inn (b1) from eligible branches
2. Add Collection O Millennium Inn 2 (b2) as eligible (used as Testing Branch)
3. Keep Sans Vibes Millennium Garden (b3) as eligible
4. Rewrite m13_earn_points_on_checkout: always check each folio item individually
   by rounding pattern (divisible by 1000 = walk-in), regardless of reservation booking source
*/

-- Fix eligible branches: remove b1, add b2
DELETE FROM m13_eligible_branches
WHERE branch_id = 'e0000000-0000-0000-0000-0000000000b1';

INSERT INTO m13_eligible_branches (organization_id, branch_id, is_active)
VALUES
  ('e0000000-0000-0000-0000-000000000001', 'e0000000-0000-0000-0000-0000000000b2', true),
  ('e0000000-0000-0000-0000-000000000001', 'e0000000-0000-0000-0000-0000000000b3', true)
ON CONFLICT (organization_id, branch_id) DO NOTHING;

-- Rewrite earning function: always check each item by rounding, no booking source shortcut
CREATE OR REPLACE FUNCTION m13_earn_points_on_checkout(
  p_reservation_id uuid,
  p_staff_user_id uuid
)
RETURNS integer
LANGUAGE plpgsql
SECURITY DEFINER
SET search_path = public
AS $$
DECLARE
  v_reservation reservations%ROWTYPE;
  v_member m13_members%ROWTYPE;
  v_org_id uuid;
  v_total_earned integer := 0;
  v_category_totals RECORD;
  v_setting m13_loyalty_settings%ROWTYPE;
  v_earned_points integer;
  v_existing_count integer;
  v_folio_id uuid;
BEGIN
  SELECT * INTO v_reservation FROM reservations WHERE id = p_reservation_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Reservation not found';
  END IF;

  IF NOT m13_is_branch_eligible(v_reservation.branch_id) THEN
    RETURN 0;
  END IF;

  v_org_id := v_reservation.organization_id;

  SELECT * INTO v_member FROM m13_members WHERE pms_guest_id = v_reservation.primary_guest_id;
  IF NOT FOUND THEN
    RETURN 0;
  END IF;

  SELECT count(*) INTO v_existing_count
  FROM m13_point_ledger
  WHERE member_id = v_member.id
    AND type = 'EARN'
    AND source_type = 'checkout'
    AND source_id = p_reservation_id::text;

  IF v_existing_count > 0 THEN
    RETURN 0;
  END IF;

  SELECT id INTO v_folio_id FROM folios WHERE reservation_id = p_reservation_id LIMIT 1;
  IF v_folio_id IS NULL THEN
    RETURN 0;
  END IF;

  -- Always check each folio item individually by rounding pattern
  -- Walk-in amounts are divisible by 1000 (e.g., 250000, 300000)
  -- Non-walk-in amounts are NOT divisible by 1000 (e.g., 275500, 300250)
  -- This correctly handles extended stays where new charges come from other booking sources
  FOR v_category_totals IN
    SELECT
      fi.category,
      SUM(
        CASE
          WHEN mod(fi.amount::numeric, 1000) = 0 THEN fi.amount
          ELSE 0
        END
      ) as total_amount
    FROM folio_items fi
    WHERE fi.folio_id = v_folio_id
      AND fi.item_type = 'charge'
      AND fi.voided = false
      AND fi.amount > 0
      AND fi.category IS NOT NULL
    GROUP BY fi.category
  LOOP
    IF v_category_totals.total_amount IS NULL OR v_category_totals.total_amount <= 0 THEN
      CONTINUE;
    END IF;

    SELECT * INTO v_setting
    FROM m13_loyalty_settings
    WHERE organization_id = v_org_id
      AND charge_category_code = v_category_totals.category
      AND is_active = true;

    IF FOUND THEN
      v_earned_points := floor(v_category_totals.total_amount / v_setting.spending_threshold * v_setting.points_awarded)::integer;

      IF v_earned_points > 0 THEN
        INSERT INTO m13_point_ledger (member_id, type, points, description, source_type, source_id, performed_by)
        VALUES (
          v_member.id,
          'EARN',
          v_earned_points,
          'Points earned from ' || v_category_totals.category || ' charges (walk-in)',
          'checkout',
          p_reservation_id::text,
          p_staff_user_id
        );

        v_total_earned := v_total_earned + v_earned_points;
      END IF;
    END IF;
  END LOOP;

  IF v_total_earned > 0 THEN
    UPDATE m13_members
    SET points_balance = points_balance + v_total_earned,
        updated_at = now()
    WHERE id = v_member.id;
  END IF;

  RETURN v_total_earned;
END;
$$;

REVOKE EXECUTE ON FUNCTION m13_earn_points_on_checkout(uuid, uuid) FROM anon, authenticated;
GRANT EXECUTE ON FUNCTION m13_earn_points_on_checkout(uuid, uuid) TO authenticated;
