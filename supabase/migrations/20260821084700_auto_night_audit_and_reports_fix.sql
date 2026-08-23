/*
# Automatic Night Audit + Reports Financial Fix

## 1. Automatic Night Audit
- Creates `compute_business_date(p_branch_id)` — pure-SQL helper that returns the current hotel business date for a branch using its timezone + business_day_cutoff.
- Creates `run_night_audit_for_branch(p_branch_id, p_business_date)` — SECURITY DEFINER function that computes the full financial + operational summary for a single branch/business-date and stores it in `night_audits`. Deposits are excluded from income. Closes the old business date and opens the next one.
- Creates `auto_night_audit()` — iterates every active branch, computes the current business date, and if the open business date is stale it runs the audit for the stale date. pg_cron (if available) schedules this every 15 minutes; the Night Audit page also calls it via RPC on load as a fallback.
- EXECUTE granted to `authenticated`.

## 2. Financial calculation rules (encoded in the function)
- Income = room charges + additional charges (excludes deposits, excludes tax). Deposits are NOT income.
- Payments grouped by method: Cash, EDC, OTA.
- All financials use `business_date` (not `created_at`).
*/

-- ---------------------------------------------------------------------------
-- Helper: compute current business date for a branch (pure SQL)
-- ---------------------------------------------------------------------------
CREATE OR REPLACE FUNCTION compute_business_date(p_branch_id uuid)
RETURNS date
LANGUAGE sql
SECURITY DEFINER
STABLE
SET search_path = public
AS $$
  SELECT
    CASE
      WHEN (
        extract(hour FROM (now() AT TIME ZONE COALESCE(b.timezone, 'Asia/Jakarta')))::int * 60
        + extract(minute FROM (now() AT TIME ZONE COALESCE(b.timezone, 'Asia/Jakarta')))::int
      ) < (
        extract(hour FROM COALESCE(b.business_day_cutoff, '04:00'::time))::int * 60
        + extract(minute FROM COALESCE(b.business_day_cutoff, '04:00'::time))::int
      )
      THEN (now() AT TIME ZONE COALESCE(b.timezone, 'Asia/Jakarta'))::date - 1
      ELSE (now() AT TIME ZONE COALESCE(b.timezone, 'Asia/Jakarta'))::date
    END
  FROM branches b
  WHERE b.id = p_branch_id;
$$;

-- ---------------------------------------------------------------------------
-- Run night audit for one branch + business date
-- ---------------------------------------------------------------------------
CREATE OR REPLACE FUNCTION run_night_audit_for_branch(p_branch_id uuid, p_business_date date)
RETURNS void
LANGUAGE plpgsql
SECURITY DEFINER
SET search_path = public
AS $$
DECLARE
  v_audit_exists boolean;
  v_room_charges        numeric(14,2) := 0;
  v_additional_charges  numeric(14,2) := 0;
  v_total_payments      numeric(14,2) := 0;
  v_cash                numeric(14,2) := 0;
  v_edc                 numeric(14,2) := 0;
  v_ota                 numeric(14,2) := 0;
  v_discounts           numeric(14,2) := 0;
  v_outstanding         numeric(14,2) := 0;
  v_arrivals            int := 0;
  v_departures          int := 0;
  v_in_house            int := 0;
  v_checked_in          int := 0;
  v_checked_out         int := 0;
  v_no_shows            int := 0;
  v_cancellations       int := 0;
  v_unpaid_count        int := 0;
  v_exceptions          jsonb := '[]'::jsonb;
  v_super_admin_id      uuid;
  v_org_id              uuid;
BEGIN
  SELECT organization_id INTO v_org_id FROM branches WHERE id = p_branch_id;
  IF v_org_id IS NULL THEN RETURN; END IF;

  -- Skip if already audited
  SELECT EXISTS(
    SELECT 1 FROM night_audits WHERE branch_id = p_branch_id AND business_date = p_business_date
  ) INTO v_audit_exists;
  IF v_audit_exists THEN RETURN; END IF;

  -- Pick a super_admin from this org as the "closed_by" (cron has no user session)
  SELECT id INTO v_super_admin_id
  FROM profiles
  WHERE role = 'super_admin' AND organization_id = v_org_id AND is_active = true
  ORDER BY created_at LIMIT 1;

  -- Room charges (deposits excluded)
  SELECT COALESCE(SUM(amount), 0) INTO v_room_charges
  FROM folio_items
  WHERE branch_id = p_branch_id
    AND business_date = p_business_date
    AND voided = false
    AND item_type = 'charge'
    AND category = 'room'
    AND amount > 0;

  -- Additional charges (exclude room AND deposit)
  SELECT COALESCE(SUM(amount), 0) INTO v_additional_charges
  FROM folio_items
  WHERE branch_id = p_branch_id
    AND business_date = p_business_date
    AND voided = false
    AND item_type = 'charge'
    AND category NOT IN ('room', 'deposit')
    AND amount > 0;

  -- Discounts
  SELECT COALESCE(SUM(ABS(amount)), 0) INTO v_discounts
  FROM folio_items
  WHERE branch_id = p_branch_id
    AND business_date = p_business_date
    AND voided = false
    AND item_type = 'discount';

  -- Payments by method
  SELECT COALESCE(SUM(amount), 0) INTO v_cash
  FROM payments
  WHERE branch_id = p_branch_id
    AND business_date = p_business_date
    AND voided = false
    AND payment_method_code = 'CASH';

  SELECT COALESCE(SUM(amount), 0) INTO v_edc
  FROM payments
  WHERE branch_id = p_branch_id
    AND business_date = p_business_date
    AND voided = false
    AND payment_method_code = 'EDC';

  SELECT COALESCE(SUM(amount), 0) INTO v_ota
  FROM payments
  WHERE branch_id = p_branch_id
    AND business_date = p_business_date
    AND voided = false
    AND is_ota = true;

  v_total_payments := v_cash + v_edc + v_ota;

  -- Outstanding from open folios
  SELECT COALESCE(SUM(balance), 0), COUNT(*) INTO v_outstanding, v_unpaid_count
  FROM folios
  WHERE branch_id = p_branch_id AND status = 'open' AND balance > 0;

  -- Reservation stats
  SELECT COUNT(*) INTO v_arrivals
  FROM reservations
  WHERE branch_id = p_branch_id
    AND check_in_date = p_business_date
    AND status IN ('confirmed', 'checked_in', 'checked_out');

  SELECT COUNT(*) INTO v_departures
  FROM reservations
  WHERE branch_id = p_branch_id
    AND check_out_date = p_business_date
    AND status IN ('checked_in', 'checked_out');

  SELECT COUNT(*) INTO v_in_house
  FROM reservations
  WHERE branch_id = p_branch_id AND status = 'checked_in';

  SELECT COUNT(*) INTO v_checked_in
  FROM reservations
  WHERE branch_id = p_branch_id
    AND actual_check_in IS NOT NULL
    AND actual_check_in::date = p_business_date;

  SELECT COUNT(*) INTO v_checked_out
  FROM reservations
  WHERE branch_id = p_branch_id
    AND actual_check_out IS NOT NULL
    AND actual_check_out::date = p_business_date;

  SELECT COUNT(*) INTO v_no_shows
  FROM reservations
  WHERE branch_id = p_branch_id AND status = 'no_show' AND check_in_date = p_business_date;

  SELECT COUNT(*) INTO v_cancellations
  FROM reservations
  WHERE branch_id = p_branch_id AND status = 'cancelled' AND check_in_date = p_business_date;

  IF v_unpaid_count > 0 THEN
    v_exceptions := v_exceptions || jsonb_build_array(
      format('%s unpaid folios', v_unpaid_count)
    );
  END IF;

  -- Insert night audit
  INSERT INTO night_audits (
    branch_id, business_date, summary, exceptions,
    arrivals, departures, in_house, checked_in, checked_out,
    no_shows, cancellations,
    room_charges, additional_charges, payments, cash, edc, ota,
    refunds, discounts, outstanding,
    closed_at, closed_by
  ) VALUES (
    p_branch_id, p_business_date,
    jsonb_build_object(
      'roomCharges', v_room_charges,
      'additionalCharges', v_additional_charges,
      'totalPayments', v_total_payments,
      'cashPayments', v_cash,
      'edcPayments', v_edc,
      'otaPayments', v_ota,
      'discounts', v_discounts,
      'outstanding', v_outstanding,
      'totalIncome', v_room_charges + v_additional_charges
    ),
    v_exceptions,
    v_arrivals, v_departures, v_in_house, v_checked_in, v_checked_out,
    v_no_shows, v_cancellations,
    v_room_charges, v_additional_charges, v_total_payments, v_cash, v_edc, v_ota,
    0, v_discounts, v_outstanding,
    now(), v_super_admin_id
  );

  -- Close the old business date
  UPDATE hotel_business_dates
  SET status = 'closed', closed_at = now(), closed_by = v_super_admin_id
  WHERE branch_id = p_branch_id
    AND business_date = p_business_date
    AND status = 'open';

  -- Open next business date
  INSERT INTO hotel_business_dates (branch_id, business_date, status)
  VALUES (p_branch_id, p_business_date + 1, 'open')
  ON CONFLICT (branch_id, business_date) DO NOTHING;

  -- Audit log
  INSERT INTO audit_logs (organization_id, branch_id, user_id, action, object_type, new_value)
  VALUES (
    v_org_id, p_branch_id, v_super_admin_id,
    'business_day_closed', 'night_audit',
    jsonb_build_object('business_date', p_business_date, 'auto', true)
  );
END;
$$;

-- ---------------------------------------------------------------------------
-- Auto night audit: roll over stale open business dates for all branches
-- ---------------------------------------------------------------------------
CREATE OR REPLACE FUNCTION auto_night_audit()
RETURNS void
LANGUAGE plpgsql
SECURITY DEFINER
SET search_path = public
AS $$
DECLARE
  v_branch record;
  v_computed_date date;
  v_open_bd date;
BEGIN
  FOR v_branch IN
    SELECT id FROM branches WHERE is_active = true
  LOOP
    v_computed_date := compute_business_date(v_branch.id);

    SELECT business_date INTO v_open_bd
    FROM hotel_business_dates
    WHERE branch_id = v_branch.id AND status = 'open'
    ORDER BY business_date DESC LIMIT 1;

    IF v_open_bd IS NOT NULL AND v_open_bd < v_computed_date THEN
      PERFORM run_night_audit_for_branch(v_branch.id, v_open_bd);
    END IF;

    IF v_open_bd IS NULL THEN
      INSERT INTO hotel_business_dates (branch_id, business_date, status)
      VALUES (v_branch.id, v_computed_date, 'open')
      ON CONFLICT (branch_id, business_date) DO NOTHING;
    END IF;
  END LOOP;
END;
$$;

GRANT EXECUTE ON FUNCTION auto_night_audit() TO authenticated;
GRANT EXECUTE ON FUNCTION run_night_audit_for_branch(uuid, date) TO authenticated;
GRANT EXECUTE ON FUNCTION compute_business_date(uuid) TO authenticated;

-- Try pg_cron; fallback is the frontend calling auto_night_audit() on load
DO $$
BEGIN
  BEGIN
    CREATE EXTENSION IF NOT EXISTS pg_cron;
    PERFORM cron.schedule('auto-night-audit', '*/15 * * * *', 'SELECT auto_night_audit()');
  EXCEPTION WHEN OTHERS THEN
    RAISE NOTICE 'pg_cron not available, frontend fallback active';
  END;
END;
$$;
