/*
# Apply M13 Club Loyalty System + fix member guest access + super_admin guest deletion

## What this does
1. Creates all M13 Club tables (m13_members, m13_loyalty_settings, m13_point_ledger, m13_rewards, m13_reward_redemptions)
2. Creates all M13 SECURITY DEFINER functions (member ID lookup, create member, earn points, redeem reward, use reward, adjust points, member number sequence)
3. Enables RLS on all M13 tables with appropriate policies
4. Seeds default loyalty settings from existing charge categories
5. Adds guests SELECT policy so M13 members can read their own linked guest record
6. Creates safe_delete_guest function with p_force parameter for super_admin to delete any guest

## Fix 1: M13 member profile cannot fetch guest data
The guests table SELECT policy requires a profiles row. M13 member auth users don't have one.
New policy "guests_select_m13_member" allows reading a guest row linked to the caller's m13_members record.

## Fix 2: Super admin cannot delete guests with non-voided reservations
safe_delete_guest now accepts p_force boolean. When true, skips all pre-checks and deletes directly.
The frontend passes p_force=true only for super_admin users.

## Security
- New guests SELECT policy scoped to M13 members reading only their own linked guest row
- safe_delete_guest is SECURITY DEFINER, bypasses RLS for the delete operation
- All M13 tables have RLS enabled with proper ownership/org-scoped policies
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
-- 7. Helper functions
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
-- 8. m13_create_member
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
  SELECT * INTO v_guest FROM guests WHERE id = p_guest_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Guest not found';
  END IF;

  IF EXISTS (SELECT 1 FROM m13_members WHERE pms_guest_id = p_guest_id) THEN
    SELECT * INTO v_member FROM m13_members WHERE pms_guest_id = p_guest_id;
    RETURN v_member;
  END IF;

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

  v_org_id := v_guest.organization_id;
  v_phone := COALESCE(v_guest.phone, '');
  v_member_number := m13_member_number_seq_next();

  INSERT INTO m13_members (pms_guest_id, member_number, organization_id, status, points_balance)
  VALUES (p_guest_id, v_member_number, v_org_id, 'active', 0)
  RETURNING * INTO v_member;

  IF (v_guest.email IS NULL OR v_guest.email = '') AND p_email IS NOT NULL AND p_email != '' THEN
    UPDATE guests SET email = p_email, updated_at = now() WHERE id = p_guest_id;
  END IF;

  RETURN v_member;
END;
$$;

REVOKE EXECUTE ON FUNCTION m13_create_member(uuid, text, uuid) FROM anon, authenticated;
GRANT EXECUTE ON FUNCTION m13_create_member(uuid, text, uuid) TO authenticated;

-- ============================================================
-- 9. m13_earn_points_on_checkout
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
  SELECT * INTO v_reservation FROM reservations WHERE id = p_reservation_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Reservation not found';
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
          'Points earned from ' || v_category_totals.category || ' charges',
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

-- ============================================================
-- 10. m13_redeem_reward
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
  v_member_id := m13_current_member_id();
  IF v_member_id IS NULL THEN
    RAISE EXCEPTION 'Not a member account';
  END IF;

  SELECT * INTO v_member FROM m13_members WHERE id = v_member_id;
  IF v_member.status != 'active' THEN
    RAISE EXCEPTION 'Member account is not active';
  END IF;

  v_today := CURRENT_DATE;

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

  v_balance := v_member.points_balance;
  SELECT COALESCE(SUM(CASE
    WHEN type IN ('EARN', 'ADJUSTMENT_CREDIT') THEN points
    WHEN type IN ('REDEEM', 'ADJUSTMENT_DEBIT', 'EXPIRATION', 'REVERSAL') THEN -points
  END), 0) INTO v_ledger_sum
  FROM m13_point_ledger WHERE member_id = v_member_id;

  IF v_ledger_sum < v_reward.points_required THEN
    RAISE EXCEPTION 'Insufficient points. You have % points, need %', v_ledger_sum, v_reward.points_required;
  END IF;

  v_code := 'M13-' || upper(substr(encode(gen_random_bytes(5), 'hex'), 1, 5));

  UPDATE m13_members
  SET points_balance = points_balance - v_reward.points_required,
      updated_at = now()
  WHERE id = v_member_id;

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

  INSERT INTO m13_reward_redemptions (member_id, reward_id, redemption_code, points_used, status)
  VALUES (v_member_id, p_reward_id, v_code, v_reward.points_required, 'UNUSED')
  RETURNING * INTO v_redemption;

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
-- 11. m13_use_reward
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
-- 12. m13_adjust_points
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
  SELECT role INTO v_staff_role FROM profiles WHERE id = p_staff_user_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Staff user not found';
  END IF;

  IF v_staff_role IN ('receptionist', 'manager') AND p_points > 0 THEN
    RAISE EXCEPTION 'Only super_admin can add points. Receptionists and managers can only deduct.';
  END IF;

  SELECT * INTO v_member FROM m13_members WHERE id = p_member_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Member not found';
  END IF;

  IF p_points > 0 THEN
    v_type := 'ADJUSTMENT_CREDIT';
    v_signed_points := p_points;
  ELSE
    v_type := 'ADJUSTMENT_DEBIT';
    v_signed_points := -p_points;
  END IF;

  IF p_points < 0 AND v_member.points_balance < v_signed_points THEN
    RAISE EXCEPTION 'Insufficient points balance. Member has % points, trying to deduct %.', v_member.points_balance, v_signed_points;
  END IF;

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
-- 13. Enable RLS on all M13 tables
-- ============================================================
ALTER TABLE m13_members ENABLE ROW LEVEL SECURITY;
ALTER TABLE m13_loyalty_settings ENABLE ROW LEVEL SECURITY;
ALTER TABLE m13_point_ledger ENABLE ROW LEVEL SECURITY;
ALTER TABLE m13_rewards ENABLE ROW LEVEL SECURITY;
ALTER TABLE m13_reward_redemptions ENABLE ROW LEVEL SECURITY;

-- ============================================================
-- 14. RLS Policies for m13_members
-- ============================================================
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

-- ============================================================
-- 15. RLS Policies for m13_point_ledger
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

-- ============================================================
-- 16. RLS Policies for m13_rewards
-- ============================================================
DROP POLICY IF EXISTS "m13_rewards_select_all" ON m13_rewards;
CREATE POLICY "m13_rewards_select_all"
ON m13_rewards FOR SELECT
TO authenticated
USING (true);

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
-- 17. RLS Policies for m13_reward_redemptions
-- ============================================================
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

-- ============================================================
-- 18. RLS Policies for m13_loyalty_settings
-- ============================================================
DROP POLICY IF EXISTS "m13_settings_select_staff" ON m13_loyalty_settings;
CREATE POLICY "m13_settings_select_staff"
ON m13_loyalty_settings FOR SELECT
TO authenticated
USING (
  m13_is_staff() AND organization_id = m13_current_staff_org_id()
);

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
-- 19. Seed default loyalty settings
-- ============================================================
INSERT INTO m13_loyalty_settings (organization_id, charge_category_code, spending_threshold, points_awarded, is_active)
SELECT cc.organization_id, cc.code,
  CASE
    WHEN cc.code = 'ROOM' THEN 10000
    WHEN cc.code IN ('AMENITY', 'EXTRA_BED', 'EXTRA_GUEST') THEN 15000
    ELSE 20000
  END,
  1,
  true
FROM charge_categories cc
WHERE cc.organization_id IN (SELECT id FROM organizations)
ON CONFLICT (organization_id, charge_category_code) DO NOTHING;

-- ============================================================
-- 20. FIX: Allow M13 members to read their own linked guest record
-- ============================================================
DROP POLICY IF EXISTS "guests_select_m13_member" ON guests;
CREATE POLICY "guests_select_m13_member" ON guests FOR SELECT
  TO authenticated USING (
    EXISTS (
      SELECT 1 FROM m13_members
      WHERE m13_members.pms_guest_id = guests.id
        AND m13_members.auth_user_id = auth.uid()
    )
  );

-- ============================================================
-- 21. FIX: safe_delete_guest with p_force for super_admin
-- ============================================================
CREATE OR REPLACE FUNCTION safe_delete_guest(p_guest_id uuid, p_force boolean DEFAULT false)
RETURNS void
LANGUAGE plpgsql
SECURITY DEFINER
SET search_path = public
AS $$
DECLARE
  v_count integer;
BEGIN
  PERFORM 1 FROM guests WHERE id = p_guest_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Guest not found';
  END IF;

  IF NOT p_force THEN
    SELECT count(*) INTO v_count
    FROM reservations
    WHERE primary_guest_id = p_guest_id AND status <> 'void';
    IF v_count > 0 THEN
      RAISE EXCEPTION 'Cannot delete guest: % reservation(s) are not voided. Void all reservations first.', v_count;
    END IF;
  END IF;

  UPDATE reservations SET primary_guest_id = NULL WHERE primary_guest_id = p_guest_id;
  UPDATE folios SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE folio_items SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE invoices SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE payments SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE deposits SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE additional_charges SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE card_issuances SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE transactions SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE refunds SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE reservation_guests SET guest_id = NULL WHERE guest_id = p_guest_id;

  DELETE FROM guests WHERE id = p_guest_id;
END;
$$;

REVOKE EXECUTE ON FUNCTION safe_delete_guest(uuid, boolean) FROM anon, authenticated;
GRANT EXECUTE ON FUNCTION safe_delete_guest(uuid, boolean) TO authenticated;
