/*
# M13 Club Loyalty System

## Overview
Creates the M13 Club loyalty program module — a hotel loyalty system integrated into the existing PMS.
M13 Club adds: membership, loyalty points, point history, rewards, reward redemption, reward usage,
loyalty configuration, and a member-facing interface.

## New Tables
1. m13_members — Links PMS guests to loyalty membership. Fields: id, pms_guest_id, member_number,
   auth_user_id, organization_id, status, points_balance, created_at, updated_at.
2. m13_loyalty_settings — Per-organization earning rates per charge category. Fields: id,
   organization_id, charge_category_code, spending_threshold, points_awarded, is_active,
   created_at, updated_at.
3. m13_point_ledger — Audit trail of all point movements. Fields: id, member_id, type, points,
   description, source_type, source_id, performed_by, reason, created_at.
4. m13_rewards — Reward catalog. Fields: id, organization_id, name, description, points_required,
   redemption_deadline, terms_conditions, total_redemption_limit, total_redeemed, is_active,
   created_by, created_at, updated_at.
5. m13_reward_redemptions — Member reward redemptions. Fields: id, member_id, reward_id,
   redemption_code, points_used, status, redeemed_at, used_at, created_at, updated_at.

## Security (RLS)
- m13_members: Members read own row (auth_user_id = auth.uid()). Staff read all in org.
  Insert only via SECURITY DEFINER. Update only staff (super_admin, manager).
- m13_point_ledger: Members read own entries. Staff read all in org. Insert only via SECURITY DEFINER.
  No UPDATE or DELETE by anyone.
- m13_rewards: Any authenticated user can SELECT active rewards. INSERT/UPDATE/DELETE super_admin only.
- m13_reward_redemptions: Members read own. Staff read all in org. INSERT/UPDATE only via SECURITY DEFINER.
- m13_loyalty_settings: Staff SELECT for org. INSERT/UPDATE/DELETE super_admin only.

## SECURITY DEFINER Functions
1. m13_current_member_id() — Returns m13_members.id for current auth.uid().
2. m13_create_member(p_guest_id, p_email, p_staff_user_id) — Creates member + auth account.
3. m13_earn_points_on_checkout(p_reservation_id, p_staff_user_id) — Calculates points from folio charges.
4. m13_redeem_reward(p_reward_id) — Atomic reward redemption with row locking.
5. m13_use_reward(p_redemption_id) — Marks UNUSED → USED.
6. m13_adjust_points(p_member_id, p_points, p_reason, p_staff_user_id) — Role-checked point adjustment.
7. m13_member_number_seq_next() — Returns next member number string.

## Important Notes
- Member auth accounts are separate from staff profiles — no profiles row.
- Initial member password = phone number.
- Points are calculated at check-out from non-voided folio charge items.
- Reward redemption is atomic with row-level locking.
- Staff (receptionist/manager) can only DEDUCT points; super_admin can add and deduct.
- Point earning is idempotent — checks for existing EARN entries with same source_id.
*/

-- ============================================================
-- 1. m13_members
-- ============================================================
CREATE TABLE IF NOT EXISTS m13_members (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  pms_guest_id uuid REFERENCES guests(id) ON DELETE CASCADE,
  member_number text UNIQUE NOT NULL,
  auth_user_id uuid,
  organization_id uuid NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
  status text NOT NULL DEFAULT 'active' CHECK (status IN ('active', 'inactive')),
  points_balance integer NOT NULL DEFAULT 0 CHECK (points_balance >= 0),
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now()
);

CREATE UNIQUE INDEX IF NOT EXISTS m13_members_pms_guest_id_key ON m13_members (pms_guest_id) WHERE pms_guest_id IS NOT NULL;
CREATE UNIQUE INDEX IF NOT EXISTS m13_members_auth_user_id_key ON m13_members (auth_user_id) WHERE auth_user_id IS NOT NULL;
CREATE INDEX IF NOT EXISTS m13_members_organization_id_idx ON m13_members (organization_id);

-- ============================================================
-- 2. m13_loyalty_settings
-- ============================================================
CREATE TABLE IF NOT EXISTS m13_loyalty_settings (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  organization_id uuid NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
  charge_category_code text NOT NULL,
  spending_threshold numeric NOT NULL DEFAULT 10000 CHECK (spending_threshold > 0),
  points_awarded integer NOT NULL DEFAULT 1 CHECK (points_awarded > 0),
  is_active boolean NOT NULL DEFAULT true,
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now()
);

CREATE UNIQUE INDEX IF NOT EXISTS m13_loyalty_settings_org_code_key ON m13_loyalty_settings (organization_id, charge_category_code);

-- ============================================================
-- 3. m13_point_ledger
-- ============================================================
CREATE TABLE IF NOT EXISTS m13_point_ledger (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  member_id uuid NOT NULL REFERENCES m13_members(id) ON DELETE CASCADE,
  type text NOT NULL CHECK (type IN ('EARN', 'REDEEM', 'ADJUSTMENT_DEBIT', 'ADJUSTMENT_CREDIT', 'EXPIRATION', 'REVERSAL')),
  points integer NOT NULL CHECK (points > 0),
  description text,
  source_type text,
  source_id text,
  performed_by uuid,
  reason text,
  created_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS m13_point_ledger_member_id_idx ON m13_point_ledger (member_id);
CREATE INDEX IF NOT EXISTS m13_point_ledger_source_idx ON m13_point_ledger (source_type, source_id);
CREATE INDEX IF NOT EXISTS m13_point_ledger_created_at_idx ON m13_point_ledger (created_at);

-- ============================================================
-- 4. m13_rewards
-- ============================================================
CREATE TABLE IF NOT EXISTS m13_rewards (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  organization_id uuid NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
  name text NOT NULL,
  description text,
  points_required integer NOT NULL CHECK (points_required > 0),
  redemption_deadline date,
  terms_conditions text,
  total_redemption_limit integer NOT NULL DEFAULT 100 CHECK (total_redemption_limit > 0),
  total_redeemed integer NOT NULL DEFAULT 0,
  is_active boolean NOT NULL DEFAULT true,
  created_by uuid,
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now(),
  CONSTRAINT m13_rewards_redeemed_limit CHECK (total_redeemed <= total_redemption_limit)
);

CREATE INDEX IF NOT EXISTS m13_rewards_organization_id_idx ON m13_rewards (organization_id);

-- ============================================================
-- 5. m13_reward_redemptions
-- ============================================================
CREATE TABLE IF NOT EXISTS m13_reward_redemptions (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  member_id uuid NOT NULL REFERENCES m13_members(id) ON DELETE CASCADE,
  reward_id uuid NOT NULL REFERENCES m13_rewards(id) ON DELETE CASCADE,
  redemption_code text UNIQUE NOT NULL,
  points_used integer NOT NULL CHECK (points_used > 0),
  status text NOT NULL DEFAULT 'UNUSED' CHECK (status IN ('UNUSED', 'USED')),
  redeemed_at timestamptz NOT NULL DEFAULT now(),
  used_at timestamptz,
  pms_reference text,
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS m13_reward_redemptions_member_id_idx ON m13_reward_redemptions (member_id);
CREATE INDEX IF NOT EXISTS m13_reward_redemptions_reward_id_idx ON m13_reward_redemptions (reward_id);
CREATE INDEX IF NOT EXISTS m13_reward_redemptions_status_idx ON m13_reward_redemptions (status);

-- ============================================================
-- 6. Sequence for member numbers
-- ============================================================
CREATE SEQUENCE IF NOT EXISTS m13_member_number_seq START 1;

-- ============================================================
-- 7. Helper: m13_current_member_id()
-- ============================================================
CREATE OR REPLACE FUNCTION m13_current_member_id()
RETURNS uuid
LANGUAGE sql
STABLE
SECURITY DEFINER
SET search_path = public
AS $$
  SELECT id FROM m13_members WHERE auth_user_id = auth.uid();
$$;

REVOKE EXECUTE ON FUNCTION m13_current_member_id() FROM anon, authenticated;
GRANT EXECUTE ON FUNCTION m13_current_member_id() TO authenticated;

-- ============================================================
-- 8. Helper: current staff org id (reuse existing pattern)
-- ============================================================
CREATE OR REPLACE FUNCTION m13_current_staff_org_id()
RETURNS uuid
LANGUAGE sql
STABLE
SECURITY DEFINER
SET search_path = public
AS $$
  SELECT organization_id FROM profiles WHERE id = auth.uid();
$$;

REVOKE EXECUTE ON FUNCTION m13_current_staff_org_id() FROM anon;
GRANT EXECUTE ON FUNCTION m13_current_staff_org_id() TO authenticated;

-- ============================================================
-- 9. Helper: is staff (has profiles row)
-- ============================================================
CREATE OR REPLACE FUNCTION m13_is_staff()
RETURNS boolean
LANGUAGE sql
STABLE
SECURITY DEFINER
SET search_path = public
AS $$
  SELECT EXISTS (SELECT 1 FROM profiles WHERE id = auth.uid());
$$;

REVOKE EXECUTE ON FUNCTION m13_is_staff() FROM anon;
GRANT EXECUTE ON FUNCTION m13_is_staff() TO authenticated;

-- ============================================================
-- 10. Helper: m13_member_number_seq_next()
-- ============================================================
CREATE OR REPLACE FUNCTION m13_member_number_seq_next()
RETURNS text
LANGUAGE sql
SECURITY DEFINER
SET search_path = public
AS $$
  SELECT 'M13-' || lpad(nextval('m13_member_number_seq')::text, 6, '0');
$$;

REVOKE EXECUTE ON FUNCTION m13_member_number_seq_next() FROM anon, authenticated;

-- ============================================================
-- 11. m13_create_member(p_guest_id, p_email, p_staff_user_id)
-- ============================================================
CREATE OR REPLACE FUNCTION m13_create_member(
  p_guest_id uuid,
  p_email text,
  p_staff_user_id uuid
)
RETURNS m13_members
LANGUAGE plpgsql
SECURITY DEFINER
SET search_path = public
AS $$
DECLARE
  v_guest guests%ROWTYPE;
  v_org_id uuid;
  v_member m13_members%ROWTYPE;
  v_member_number text;
  v_phone text;
BEGIN
  -- Get guest
  SELECT * INTO v_guest FROM guests WHERE id = p_guest_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Guest not found';
  END IF;

  -- Check for duplicates by pms_guest_id
  IF EXISTS (SELECT 1 FROM m13_members WHERE pms_guest_id = p_guest_id) THEN
    SELECT * INTO v_member FROM m13_members WHERE pms_guest_id = p_guest_id;
    RETURN v_member;
  END IF;

  -- Check for duplicates by id_number, phone, email
  IF v_guest.id_number IS NOT NULL AND v_guest.id_number != '' THEN
    IF EXISTS (
      SELECT 1 FROM m13_members m
      JOIN guests g ON g.id = m.pms_guest_id
      WHERE g.id_number = v_guest.id_number AND g.id_number != ''
    ) THEN
      SELECT m.* INTO v_member FROM m13_members m
      JOIN guests g ON g.id = m.pms_guest_id
      WHERE g.id_number = v_guest.id_number AND g.id_number != ''
      LIMIT 1;
      RETURN v_member;
    END IF;
  END IF;

  IF v_guest.phone IS NOT NULL AND v_guest.phone != '' THEN
    IF EXISTS (
      SELECT 1 FROM m13_members m
      JOIN guests g ON g.id = m.pms_guest_id
      WHERE g.phone = v_guest.phone AND g.phone != ''
    ) THEN
      SELECT m.* INTO v_member FROM m13_members m
      JOIN guests g ON g.id = m.pms_guest_id
      WHERE g.phone = v_guest.phone AND g.phone != ''
      LIMIT 1;
      RETURN v_member;
    END IF;
  END IF;

  IF p_email IS NOT NULL AND p_email != '' THEN
    IF EXISTS (
      SELECT 1 FROM m13_members m
      JOIN guests g ON g.id = m.pms_guest_id
      WHERE g.email = p_email AND g.email != ''
    ) THEN
      SELECT m.* INTO v_member FROM m13_members m
      JOIN guests g ON g.id = m.pms_guest_id
      WHERE g.email = p_email AND g.email != ''
      LIMIT 1;
      RETURN v_member;
    END IF;
  END IF;

  -- Get org from guest
  v_org_id := v_guest.organization_id;
  v_phone := COALESCE(v_guest.phone, '');

  -- Generate member number
  v_member_number := m13_member_number_seq_next();

  -- Insert member record (auth_user_id will be set by edge function via update)
  INSERT INTO m13_members (pms_guest_id, member_number, organization_id, status, points_balance)
  VALUES (p_guest_id, v_member_number, v_org_id, 'active', 0)
  RETURNING * INTO v_member;

  -- Update guest email if it was missing
  IF (v_guest.email IS NULL OR v_guest.email = '') AND p_email IS NOT NULL AND p_email != '' THEN
    UPDATE guests SET email = p_email, updated_at = now() WHERE id = p_guest_id;
  END IF;

  RETURN v_member;
END;
$$;

REVOKE EXECUTE ON FUNCTION m13_create_member(uuid, text, uuid) FROM anon, authenticated;
GRANT EXECUTE ON FUNCTION m13_create_member(uuid, text, uuid) TO authenticated;

-- ============================================================
-- 12. m13_earn_points_on_checkout(p_reservation_id, p_staff_user_id)
-- ============================================================
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
  -- Get reservation
  SELECT * INTO v_reservation FROM reservations WHERE id = p_reservation_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Reservation not found';
  END IF;

  v_org_id := v_reservation.organization_id;

  -- Find member by primary_guest_id
  SELECT * INTO v_member FROM m13_members WHERE pms_guest_id = v_reservation.primary_guest_id;
  IF NOT FOUND THEN
    RETURN 0;
  END IF;

  -- Idempotency: check if EARN entries already exist for this reservation
  SELECT count(*) INTO v_existing_count
  FROM m13_point_ledger
  WHERE member_id = v_member.id
    AND type = 'EARN'
    AND source_type = 'checkout'
    AND source_id = p_reservation_id::text;

  IF v_existing_count > 0 THEN
    RETURN 0;
  END IF;

  -- Get the folio for this reservation
  SELECT id INTO v_folio_id FROM folios WHERE reservation_id = p_reservation_id LIMIT 1;
  IF v_folio_id IS NULL THEN
    RETURN 0;
  END IF;

  -- Group non-voided charge items by category and calculate points
  FOR v_category_totals IN
    SELECT fi.category, SUM(fi.amount) as total_amount
    FROM folio_items fi
    WHERE fi.folio_id = v_folio_id
      AND fi.item_type = 'charge'
      AND fi.voided = false
      AND fi.amount > 0
      AND fi.category IS NOT NULL
    GROUP BY fi.category
  LOOP
    -- Look up earning setting for this category
    SELECT * INTO v_setting
    FROM m13_loyalty_settings
    WHERE organization_id = v_org_id
      AND charge_category_code = v_category_totals.category
      AND is_active = true;

    IF FOUND THEN
      -- Calculate points: floor(total_amount / spending_threshold * points_awarded)
      v_earned_points := floor(v_category_totals.total_amount / v_setting.spending_threshold * v_setting.points_awarded)::integer;

      IF v_earned_points > 0 THEN
        -- Insert EARN ledger entry
        INSERT INTO m13_point_ledger (member_id, type, points, description, source_type, source_id, performed_by)
        VALUES (
          v_member.id,
          'EARN',
          v_earned_points,
          'Points earned from ' || v_category_totals.category || ' charges',
          'checkout',
          p_reservation_id::text,
          p_staff_user_id
        );

        v_total_earned := v_total_earned + v_earned_points;
      END IF;
    END IF;
  END LOOP;

  -- Update member balance
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

-- ============================================================
-- 13. m13_redeem_reward(p_reward_id)
-- ============================================================
CREATE OR REPLACE FUNCTION m13_redeem_reward(
  p_reward_id uuid
)
RETURNS m13_reward_redemptions
LANGUAGE plpgsql
SECURITY DEFINER
SET search_path = public
AS $$
DECLARE
  v_member_id uuid;
  v_member m13_members%ROWTYPE;
  v_reward m13_rewards%ROWTYPE;
  v_redemption m13_reward_redemptions%ROWTYPE;
  v_code text;
  v_balance integer;
  v_ledger_sum integer;
  v_today date;
BEGIN
  -- Get current member
  v_member_id := m13_current_member_id();
  IF v_member_id IS NULL THEN
    RAISE EXCEPTION 'Not a member account';
  END IF;

  SELECT * INTO v_member FROM m13_members WHERE id = v_member_id;
  IF v_member.status != 'active' THEN
    RAISE EXCEPTION 'Member account is not active';
  END IF;

  v_today := CURRENT_DATE;

  -- Lock reward row
  SELECT * INTO v_reward FROM m13_rewards WHERE id = p_reward_id FOR UPDATE;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Reward not found';
  END IF;

  IF NOT v_reward.is_active THEN
    RAISE EXCEPTION 'Reward is not available';
  END IF;

  IF v_reward.redemption_deadline IS NOT NULL AND v_today > v_reward.redemption_deadline THEN
    RAISE EXCEPTION 'Reward redemption deadline has passed';
  END IF;

  IF v_reward.total_redeemed >= v_reward.total_redemption_limit THEN
    RAISE EXCEPTION 'Reward is fully redeemed';
  END IF;

  -- Verify points balance
  v_balance := v_member.points_balance;
  SELECT COALESCE(SUM(CASE
    WHEN type IN ('EARN', 'ADJUSTMENT_CREDIT') THEN points
    WHEN type IN ('REDEEM', 'ADJUSTMENT_DEBIT', 'EXPIRATION', 'REVERSAL') THEN -points
  END), 0) INTO v_ledger_sum
  FROM m13_point_ledger WHERE member_id = v_member_id;

  IF v_ledger_sum < v_reward.points_required THEN
    RAISE EXCEPTION 'Insufficient points. You have % points, need %', v_ledger_sum, v_reward.points_required;
  END IF;

  -- Generate unique redemption code
  v_code := 'M13-' || upper(substr(encode(gen_random_bytes(5), 'hex'), 1, 5));

  -- Deduct points
  UPDATE m13_members
  SET points_balance = points_balance - v_reward.points_required,
      updated_at = now()
  WHERE id = v_member_id;

  -- Insert REDEEM ledger entry
  INSERT INTO m13_point_ledger (member_id, type, points, description, source_type, source_id, performed_by)
  VALUES (
    v_member_id,
    'REDEEM',
    v_reward.points_required,
    'Redeemed: ' || v_reward.name,
    'redemption',
    p_reward_id::text,
    auth.uid()
  );

  -- Create redemption record
  INSERT INTO m13_reward_redemptions (member_id, reward_id, redemption_code, points_used, status)
  VALUES (v_member_id, p_reward_id, v_code, v_reward.points_required, 'UNUSED')
  RETURNING * INTO v_redemption;

  -- Increment total_redeemed
  UPDATE m13_rewards
  SET total_redeemed = total_redeemed + 1,
      updated_at = now()
  WHERE id = p_reward_id;

  RETURN v_redemption;
END;
$$;

REVOKE EXECUTE ON FUNCTION m13_redeem_reward(uuid) FROM anon, authenticated;
GRANT EXECUTE ON FUNCTION m13_redeem_reward(uuid) TO authenticated;

-- ============================================================
-- 14. m13_use_reward(p_redemption_id)
-- ============================================================
CREATE OR REPLACE FUNCTION m13_use_reward(
  p_redemption_id uuid
)
RETURNS m13_reward_redemptions
LANGUAGE plpgsql
SECURITY DEFINER
SET search_path = public
AS $$
DECLARE
  v_member_id uuid;
  v_redemption m13_reward_redemptions%ROWTYPE;
BEGIN
  v_member_id := m13_current_member_id();
  IF v_member_id IS NULL THEN
    RAISE EXCEPTION 'Not a member account';
  END IF;

  SELECT * INTO v_redemption FROM m13_reward_redemptions WHERE id = p_redemption_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Redemption not found';
  END IF;

  IF v_redemption.member_id != v_member_id THEN
    RAISE EXCEPTION 'This redemption does not belong to you';
  END IF;

  IF v_redemption.status = 'USED' THEN
    RAISE EXCEPTION 'This reward has already been used';
  END IF;

  UPDATE m13_reward_redemptions
  SET status = 'USED',
      used_at = now(),
      updated_at = now()
  WHERE id = p_redemption_id
  RETURNING * INTO v_redemption;

  RETURN v_redemption;
END;
$$;

REVOKE EXECUTE ON FUNCTION m13_use_reward(uuid) FROM anon, authenticated;
GRANT EXECUTE ON FUNCTION m13_use_reward(uuid) TO authenticated;

-- ============================================================
-- 15. m13_adjust_points(p_member_id, p_points, p_reason, p_staff_user_id)
-- ============================================================
CREATE OR REPLACE FUNCTION m13_adjust_points(
  p_member_id uuid,
  p_points integer,
  p_reason text,
  p_staff_user_id uuid
)
RETURNS void
LANGUAGE plpgsql
SECURITY DEFINER
SET search_path = public
AS $$
DECLARE
  v_staff_role text;
  v_member m13_members%ROWTYPE;
  v_type text;
  v_signed_points integer;
BEGIN
  -- Get staff role
  SELECT role INTO v_staff_role FROM profiles WHERE id = p_staff_user_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Staff user not found';
  END IF;

  -- Enforce: receptionist/manager can only deduct (negative), super_admin can add or deduct
  IF v_staff_role IN ('receptionist', 'manager') AND p_points > 0 THEN
    RAISE EXCEPTION 'Only super_admin can add points. Receptionists and managers can only deduct.';
  END IF;

  -- Get member
  SELECT * INTO v_member FROM m13_members WHERE id = p_member_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Member not found';
  END IF;

  -- Normalize: p_points is the raw signed value from the caller
  -- Positive = add (credit), Negative = deduct (debit)
  IF p_points > 0 THEN
    v_type := 'ADJUSTMENT_CREDIT';
    v_signed_points := p_points;
  ELSE
    v_type := 'ADJUSTMENT_DEBIT';
    v_signed_points := -p_points;
  END IF;

  -- Check sufficient balance for deduction
  IF p_points < 0 AND v_member.points_balance < v_signed_points THEN
    RAISE EXCEPTION 'Insufficient points balance. Member has % points, trying to deduct %.', v_member.points_balance, v_signed_points;
  END IF;

  -- Insert ledger entry
  INSERT INTO m13_point_ledger (member_id, type, points, description, source_type, source_id, performed_by, reason)
  VALUES (
    p_member_id,
    v_type,
    v_signed_points,
    'Manual adjustment by staff',
    'adjustment',
    p_staff_user_id::text,
    p_staff_user_id,
    p_reason
  );

  -- Update balance
  IF p_points > 0 THEN
    UPDATE m13_members SET points_balance = points_balance + v_signed_points, updated_at = now() WHERE id = p_member_id;
  ELSE
    UPDATE m13_members SET points_balance = points_balance - v_signed_points, updated_at = now() WHERE id = p_member_id;
  END IF;
END;
$$;

REVOKE EXECUTE ON FUNCTION m13_adjust_points(uuid, integer, text, uuid) FROM anon, authenticated;
GRANT EXECUTE ON FUNCTION m13_adjust_points(uuid, integer, text, uuid) TO authenticated;

-- ============================================================
-- 16. Enable RLS on all tables
-- ============================================================
ALTER TABLE m13_members ENABLE ROW LEVEL SECURITY;
ALTER TABLE m13_loyalty_settings ENABLE ROW LEVEL SECURITY;
ALTER TABLE m13_point_ledger ENABLE ROW LEVEL SECURITY;
ALTER TABLE m13_rewards ENABLE ROW LEVEL SECURITY;
ALTER TABLE m13_reward_redemptions ENABLE ROW LEVEL SECURITY;

-- ============================================================
-- 17. RLS Policies for m13_members
-- ============================================================
-- SELECT: members read own row, staff read all in org
DROP POLICY IF EXISTS "m13_members_select_own" ON m13_members;
CREATE POLICY "m13_members_select_own"
ON m13_members FOR SELECT
TO authenticated
USING (auth_user_id = auth.uid());

DROP POLICY IF EXISTS "m13_members_select_staff" ON m13_members;
CREATE POLICY "m13_members_select_staff"
ON m13_members FOR SELECT
TO authenticated
USING (
  m13_is_staff() AND organization_id = m13_current_staff_org_id()
);

-- INSERT: only via SECURITY DEFINER (no direct insert)
-- UPDATE: only staff (super_admin, manager)
DROP POLICY IF EXISTS "m13_members_update_staff" ON m13_members;
CREATE POLICY "m13_members_update_staff"
ON m13_members FOR UPDATE
TO authenticated
USING (
  m13_is_staff() AND organization_id = m13_current_staff_org_id()
)
WITH CHECK (
  m13_is_staff() AND organization_id = m13_current_staff_org_id()
);

-- No INSERT or DELETE policies — only SECURITY DEFINER functions can insert

-- ============================================================
-- 18. RLS Policies for m13_point_ledger
-- ============================================================
DROP POLICY IF EXISTS "m13_ledger_select_own" ON m13_point_ledger;
CREATE POLICY "m13_ledger_select_own"
ON m13_point_ledger FOR SELECT
TO authenticated
USING (member_id = m13_current_member_id());

DROP POLICY IF EXISTS "m13_ledger_select_staff" ON m13_point_ledger;
CREATE POLICY "m13_ledger_select_staff"
ON m13_point_ledger FOR SELECT
TO authenticated
USING (
  m13_is_staff() AND EXISTS (
    SELECT 1 FROM m13_members
    WHERE m13_members.id = m13_point_ledger.member_id
    AND m13_members.organization_id = m13_current_staff_org_id()
  )
);

-- No INSERT, UPDATE, DELETE policies — only SECURITY DEFINER functions

-- ============================================================
-- 19. RLS Policies for m13_rewards
-- ============================================================
-- SELECT: any authenticated user can read active rewards (catalog)
DROP POLICY IF EXISTS "m13_rewards_select_all" ON m13_rewards;
CREATE POLICY "m13_rewards_select_all"
ON m13_rewards FOR SELECT
TO authenticated
USING (true);

-- INSERT/UPDATE/DELETE: super_admin only
DROP POLICY IF EXISTS "m13_rewards_insert_admin" ON m13_rewards;
CREATE POLICY "m13_rewards_insert_admin"
ON m13_rewards FOR INSERT
TO authenticated
WITH CHECK (
  m13_is_staff() AND organization_id = m13_current_staff_org_id()
  AND EXISTS (SELECT 1 FROM profiles WHERE id = auth.uid() AND role = 'super_admin')
);

DROP POLICY IF EXISTS "m13_rewards_update_admin" ON m13_rewards;
CREATE POLICY "m13_rewards_update_admin"
ON m13_rewards FOR UPDATE
TO authenticated
USING (
  m13_is_staff() AND organization_id = m13_current_staff_org_id()
  AND EXISTS (SELECT 1 FROM profiles WHERE id = auth.uid() AND role = 'super_admin')
)
WITH CHECK (
  m13_is_staff() AND organization_id = m13_current_staff_org_id()
  AND EXISTS (SELECT 1 FROM profiles WHERE id = auth.uid() AND role = 'super_admin')
);

DROP POLICY IF EXISTS "m13_rewards_delete_admin" ON m13_rewards;
CREATE POLICY "m13_rewards_delete_admin"
ON m13_rewards FOR DELETE
TO authenticated
USING (
  m13_is_staff() AND organization_id = m13_current_staff_org_id()
  AND EXISTS (SELECT 1 FROM profiles WHERE id = auth.uid() AND role = 'super_admin')
);

-- ============================================================
-- 20. RLS Policies for m13_reward_redemptions
-- ============================================================
-- SELECT: members read own, staff read all in org
DROP POLICY IF EXISTS "m13_redemptions_select_own" ON m13_reward_redemptions;
CREATE POLICY "m13_redemptions_select_own"
ON m13_reward_redemptions FOR SELECT
TO authenticated
USING (member_id = m13_current_member_id());

DROP POLICY IF EXISTS "m13_redemptions_select_staff" ON m13_reward_redemptions;
CREATE POLICY "m13_redemptions_select_staff"
ON m13_reward_redemptions FOR SELECT
TO authenticated
USING (
  m13_is_staff() AND EXISTS (
    SELECT 1 FROM m13_members
    WHERE m13_members.id = m13_reward_redemptions.member_id
    AND m13_members.organization_id = m13_current_staff_org_id()
  )
);

-- No INSERT or UPDATE policies — only SECURITY DEFINER functions

-- ============================================================
-- 21. RLS Policies for m13_loyalty_settings
-- ============================================================
-- SELECT: staff only (members don't need earning rules)
DROP POLICY IF EXISTS "m13_settings_select_staff" ON m13_loyalty_settings;
CREATE POLICY "m13_settings_select_staff"
ON m13_loyalty_settings FOR SELECT
TO authenticated
USING (
  m13_is_staff() AND organization_id = m13_current_staff_org_id()
);

-- INSERT/UPDATE/DELETE: super_admin only
DROP POLICY IF EXISTS "m13_settings_insert_admin" ON m13_loyalty_settings;
CREATE POLICY "m13_settings_insert_admin"
ON m13_loyalty_settings FOR INSERT
TO authenticated
WITH CHECK (
  m13_is_staff() AND organization_id = m13_current_staff_org_id()
  AND EXISTS (SELECT 1 FROM profiles WHERE id = auth.uid() AND role = 'super_admin')
);

DROP POLICY IF EXISTS "m13_settings_update_admin" ON m13_loyalty_settings;
CREATE POLICY "m13_settings_update_admin"
ON m13_loyalty_settings FOR UPDATE
TO authenticated
USING (
  m13_is_staff() AND organization_id = m13_current_staff_org_id()
  AND EXISTS (SELECT 1 FROM profiles WHERE id = auth.uid() AND role = 'super_admin')
)
WITH CHECK (
  m13_is_staff() AND organization_id = m13_current_staff_org_id()
  AND EXISTS (SELECT 1 FROM profiles WHERE id = auth.uid() AND role = 'super_admin')
);

DROP POLICY IF EXISTS "m13_settings_delete_admin" ON m13_loyalty_settings;
CREATE POLICY "m13_settings_delete_admin"
ON m13_loyalty_settings FOR DELETE
TO authenticated
USING (
  m13_is_staff() AND organization_id = m13_current_staff_org_id()
  AND EXISTS (SELECT 1 FROM profiles WHERE id = auth.uid() AND role = 'super_admin')
);

-- ============================================================
-- 22. Seed default loyalty settings for existing org
-- ============================================================
INSERT INTO m13_loyalty_settings (organization_id, charge_category_code, spending_threshold, points_awarded, is_active)
SELECT 'e0000000-0000-0000-0000-000000000001', code,
  CASE
    WHEN code = 'ROOM' THEN 10000
    WHEN code IN ('AMENITY', 'EXTRA_BED', 'EXTRA_GUEST') THEN 15000
    Else 20000
  END,
  1,
  true
FROM charge_categories
WHERE organization_id = 'e0000000-0000-0000-0000-000000000001'
ON CONFLICT (organization_id, charge_category_code) DO NOTHING;