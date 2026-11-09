/*
# Advance Payments

1. New Tables
- `advance_payments` — standalone pre-stay payments received from a guest before
  check-in. Each entry records the guest name, expected check-in date, amount, and
  payment method. An advance can later be applied to a folio (claiming it), or
  cancelled/refunded by a manager or super admin.
  Columns:
  - `id` (uuid, primary key)
  - `organization_id` (uuid, references organizations)
  - `branch_id` (uuid, references branches)
  - `guest_name` (text, not null)
  - `expected_check_in_date` (date, not null)
  - `amount` (numeric(14,2), not null, > 0)
  - `payment_method_id` (uuid, references payment_methods)
  - `payment_method_code` (text, not null)
  - `status` (text, not null, default 'available'; one of available|claimed|cancelled)
  - `business_date` (date, not null — the business day the money was received)
  - `notes` (text, nullable)
  - `received_by` (uuid, references profiles — who recorded the receipt)
  - `claimed_folio_id` (uuid, nullable — the folio this advance was applied to)
  - `claimed_at` (timestamptz, nullable)
  - `claimed_by` (uuid, nullable, references profiles)
  - `cancelled_at` (timestamptz, nullable)
  - `cancelled_by` (uuid, nullable, references profiles)
  - `cancelled_business_date` (date, nullable — the business day the refund was issued)
  - `cancel_reason` (text, nullable)
  - `created_at`, `updated_at` (timestamptz)

2. New RPCs
- `claim_advance_payment(p_advance_id, p_folio_id)` — marks an available advance as
  claimed by the current user and links it to a folio. Enforces: advance exists,
  belongs to a branch the user can access, is in 'available' status, and the folio
  exists, is open, and belongs to the same branch. Returns the updated advance row.
- `release_advance_payment(p_advance_id, p_folio_id)` — reverses a claim (used as
  a compensating action if the folio item insert fails after claiming). Resets the
  advance back to 'available' and clears the claim fields. Only works if the
  advance is currently claimed on the given folio.
- `cancel_advance_payment(p_advance_id, p_reason, p_business_date)` — cancels an
  available advance and records the refund. Only managers and super admins may
  cancel; a non-empty reason is required. Sets status to 'cancelled', records
  cancelled_at/by, cancelled_business_date, and cancel_reason. Returns the
  updated advance row.

3. Security
- RLS enabled on `advance_payments`.
- SELECT/INSERT/UPDATE/DELETE granted to `authenticated` (branch-scoped access via
  `user_has_branch_access`). Also granted to `service_role` for edge functions.
- Four policies (select/insert/update/delete) matching the folios pattern.
- RPCs are SECURITY DEFINER so they can update the row regardless of the
  caller's column-level write privileges; EXECUTE is revoked from PUBLIC/anon
  and granted to authenticated only.

4. Notes
- The advance_payments table is queried by the daily income report to show
  advance receipts and refunds. When an advance is applied to a folio, a
  `folio_items` row with item_type='payment' is inserted by the service; that
  folio item is an internal accounting transfer, NOT a new cash receipt, so it
  is intentionally excluded from the daily income report's payment totals to
  avoid double-counting the advance.
*/

CREATE TABLE IF NOT EXISTS advance_payments (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  organization_id uuid NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  guest_name text NOT NULL,
  expected_check_in_date date NOT NULL,
  amount numeric(14,2) NOT NULL CHECK (amount > 0),
  payment_method_id uuid REFERENCES payment_methods(id) ON DELETE SET NULL,
  payment_method_code text NOT NULL,
  status text NOT NULL DEFAULT 'available' CHECK (status IN ('available', 'claimed', 'cancelled')),
  business_date date NOT NULL DEFAULT CURRENT_DATE,
  notes text,
  received_by uuid NOT NULL REFERENCES profiles(id) ON DELETE SET NULL,
  claimed_folio_id uuid REFERENCES folios(id) ON DELETE SET NULL,
  claimed_at timestamptz,
  claimed_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  cancelled_at timestamptz,
  cancelled_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  cancelled_business_date date,
  cancel_reason text,
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_advance_payments_branch ON advance_payments(branch_id);
CREATE INDEX IF NOT EXISTS idx_advance_payments_status ON advance_payments(status);
CREATE INDEX IF NOT EXISTS idx_advance_payments_business_date ON advance_payments(business_date);
CREATE INDEX IF NOT EXISTS idx_advance_payments_cancelled_business_date ON advance_payments(cancelled_business_date);
CREATE INDEX IF NOT EXISTS idx_advance_payments_claimed_folio ON advance_payments(claimed_folio_id);

-- updated_at trigger (use a table-specific function to avoid clobbering a shared one)
CREATE OR REPLACE FUNCTION set_timestamp_advance_payments()
RETURNS trigger
LANGUAGE plpgsql
AS $$
BEGIN
  NEW.updated_at = now();
  RETURN NEW;
END;
$$;

DROP TRIGGER IF EXISTS trg_set_timestamp_advance_payments ON advance_payments;
CREATE TRIGGER trg_set_timestamp_advance_payments
BEFORE UPDATE ON advance_payments
FOR EACH ROW
EXECUTE FUNCTION set_timestamp_advance_payments();

-- ── RLS ────────────────────────────────────────────────────────────────────
ALTER TABLE advance_payments ENABLE ROW LEVEL SECURITY;

GRANT SELECT, INSERT, UPDATE, DELETE ON advance_payments TO authenticated;
GRANT SELECT, INSERT, UPDATE, DELETE ON advance_payments TO service_role;

DROP POLICY IF EXISTS "ap_select" ON advance_payments;
CREATE POLICY "ap_select" ON advance_payments FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));

DROP POLICY IF EXISTS "ap_insert" ON advance_payments;
CREATE POLICY "ap_insert" ON advance_payments FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));

DROP POLICY IF EXISTS "ap_update" ON advance_payments;
CREATE POLICY "ap_update" ON advance_payments FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));

DROP POLICY IF EXISTS "ap_delete" ON advance_payments;
CREATE POLICY "ap_delete" ON advance_payments FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin' OR current_user_role() = 'manager');

-- ── RPC: claim_advance_payment ─────────────────────────────────────────────
CREATE OR REPLACE FUNCTION claim_advance_payment(p_advance_id uuid, p_folio_id uuid)
RETURNS advance_payments
LANGUAGE plpgsql
SECURITY DEFINER
SET search_path = public
AS $$
DECLARE
  v_advance advance_payments%ROWTYPE;
  v_folio RECORD;
BEGIN
  SELECT * INTO v_advance FROM advance_payments WHERE id = p_advance_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'not_found';
  END IF;
  IF NOT user_has_branch_access(v_advance.branch_id) THEN
    RAISE EXCEPTION 'permission_denied';
  END IF;
  IF v_advance.status <> 'available' THEN
    RAISE EXCEPTION 'not_available';
  END IF;

  SELECT id, branch_id, status INTO v_folio FROM folios WHERE id = p_folio_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'folio_not_found';
  END IF;
  IF v_folio.branch_id <> v_advance.branch_id THEN
    RAISE EXCEPTION 'folio_not_found';
  END IF;
  IF v_folio.status <> 'open' THEN
    RAISE EXCEPTION 'folio_not_open';
  END IF;

  UPDATE advance_payments
  SET status = 'claimed',
      claimed_folio_id = p_folio_id,
      claimed_at = now(),
      claimed_by = auth.uid()
  WHERE id = p_advance_id
  RETURNING * INTO v_advance;

  RETURN v_advance;
END;
$$;

-- ── RPC: release_advance_payment ───────────────────────────────────────────
CREATE OR REPLACE FUNCTION release_advance_payment(p_advance_id uuid, p_folio_id uuid)
RETURNS advance_payments
LANGUAGE plpgsql
SECURITY DEFINER
SET search_path = public
AS $$
DECLARE
  v_advance advance_payments%ROWTYPE;
BEGIN
  SELECT * INTO v_advance FROM advance_payments WHERE id = p_advance_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'not_found';
  END IF;
  IF NOT user_has_branch_access(v_advance.branch_id) THEN
    RAISE EXCEPTION 'permission_denied';
  END IF;
  IF v_advance.status <> 'claimed' OR v_advance.claimed_folio_id IS NULL OR v_advance.claimed_folio_id <> p_folio_id THEN
    RAISE EXCEPTION 'not_claimed_to_folio';
  END IF;

  UPDATE advance_payments
  SET status = 'available',
      claimed_folio_id = NULL,
      claimed_at = NULL,
      claimed_by = NULL
  WHERE id = p_advance_id
  RETURNING * INTO v_advance;

  RETURN v_advance;
END;
$$;

-- ── RPC: cancel_advance_payment ────────────────────────────────────────────
CREATE OR REPLACE FUNCTION cancel_advance_payment(p_advance_id uuid, p_reason text, p_business_date date)
RETURNS advance_payments
LANGUAGE plpgsql
SECURITY DEFINER
SET search_path = public
AS $$
DECLARE
  v_advance advance_payments%ROWTYPE;
  v_role text;
BEGIN
  SELECT role INTO v_role FROM profiles WHERE id = auth.uid();
  IF v_role IS NULL OR (v_role <> 'super_admin' AND v_role <> 'manager') THEN
    RAISE EXCEPTION 'permission_denied';
  END IF;

  SELECT * INTO v_advance FROM advance_payments WHERE id = p_advance_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'not_found';
  END IF;
  IF NOT user_has_branch_access(v_advance.branch_id) THEN
    RAISE EXCEPTION 'permission_denied';
  END IF;
  IF v_advance.status <> 'available' THEN
    RAISE EXCEPTION 'not_cancellable';
  END IF;
  IF p_reason IS NULL OR btrim(p_reason) = '' THEN
    RAISE EXCEPTION 'reason_required';
  END IF;

  UPDATE advance_payments
  SET status = 'cancelled',
      cancelled_at = now(),
      cancelled_by = auth.uid(),
      cancelled_business_date = p_business_date,
      cancel_reason = p_reason
  WHERE id = p_advance_id
  RETURNING * INTO v_advance;

  RETURN v_advance;
END;
$$;

REVOKE EXECUTE ON FUNCTION claim_advance_payment(uuid, uuid) FROM PUBLIC, anon;
GRANT EXECUTE ON FUNCTION claim_advance_payment(uuid, uuid) TO authenticated;

REVOKE EXECUTE ON FUNCTION release_advance_payment(uuid, uuid) FROM PUBLIC, anon;
GRANT EXECUTE ON FUNCTION release_advance_payment(uuid, uuid) TO authenticated;

REVOKE EXECUTE ON FUNCTION cancel_advance_payment(uuid, text, date) FROM PUBLIC, anon;
GRANT EXECUTE ON FUNCTION cancel_advance_payment(uuid, text, date) TO authenticated;
