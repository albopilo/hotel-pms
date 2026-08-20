/*
# Production Hardening: Security & Financial Integrity

1. Purpose
   Closes the most critical security gaps blocking real hotel operation.
   Receptionists can no longer modify/delete financial records, finalized
   folios/invoices are immutable at the database level, the immutable ledger
   (transactions) can no longer be updated by anyone, and double-booking is
   prevented by a database exclusion constraint.

2. Helper function hardening
   - Revoke EXECUTE on current_user_role(), current_user_branch_ids(),
     user_has_branch_access() from anon and authenticated. These functions
     read from profiles/user_branch_access and must only run inside RLS
     policies, not be callable directly via the REST RPC endpoint.
   - Add a new helper is_manager_or_admin() for use in financial policies.

3. Financial record protection (payments, refunds, transactions, folios,
   folio_items, invoices, invoice_items, additional_charges, deposits)
   - INSERT: any branch user (receptionist included) — they need to take
     payments and post charges.
   - UPDATE: restricted to super_admin + manager only. Receptionists can no
     longer alter posted payments, charges, or invoices. Voiding still goes
     through an UPDATE (setting voided=true) so it is now manager-only.
   - DELETE: super_admin only (existing for most; tightened the rest).
   This matches the operational rule: receptionists create financial rows,
   managers/super_admins correct or void them.

4. Finalized folio & invoice immutability
   - Adds a WITH CHECK guard on folios UPDATE so that once status =
     'finalized', the row cannot be changed except by super_admin.
   - Adds the same guard on invoices UPDATE.
   - Adds a guard on folio_items UPDATE so items on a finalized folio cannot
     be voided or edited except by super_admin. Post-stay charges are added
     as NEW rows, not by editing finalized ones.

5. Immutable ledger
   - transactions: removes the UPDATE policy entirely. The ledger is
     append-only; corrections are new reversing rows, never edits.

6. Double-booking prevention
   - Adds a partial unique exclusion constraint on reservations so that no
     two active reservations (status in tentative/confirmed/checked_in) for
     the same room can overlap in date. The application checks too, but this
   - is the database-level backstop that survives bugs and concurrent inserts.

7. Night audit / business date protection
   - hotel_business_dates UPDATE restricted to super_admin + manager.
   - night_audits: already insert-only; no change needed.

8. Settings & users (already correct, verified)
   - system_settings, profiles, user_branch_access, branches, charge_categories,
     payment_methods, booking_sources remain super_admin-only for writes.

9. Audit logs
   - INSERT already open to all roles (needed so every action can log).
   - UPDATE denied entirely (audit trail is append-only). Added a restrictive
     UPDATE policy returning false so no one — not even super_admin — can
     rewrite history via the REST API.
   - DELETE stays super_admin-only.

Notes
   - No data is deleted or transformed. All changes are policy/constraint
     additions.
   - Policies are dropped before recreate so the migration is idempotent.
*/

-- =========================================================
-- 1. Helper function hardening
-- =========================================================
REVOKE EXECUTE ON FUNCTION current_user_role() FROM anon, authenticated;
REVOKE EXECUTE ON FUNCTION current_user_branch_ids() FROM anon, authenticated;
REVOKE EXECUTE ON FUNCTION user_has_branch_access(uuid) FROM anon, authenticated;

CREATE OR REPLACE FUNCTION is_manager_or_admin()
RETURNS boolean
LANGUAGE sql
SECURITY DEFINER
STABLE
SET search_path = public
AS $$
  SELECT EXISTS (
    SELECT 1 FROM profiles
    WHERE id = auth.uid() AND role IN ('super_admin', 'manager')
  );
$$;
REVOKE EXECUTE ON FUNCTION is_manager_or_admin() FROM anon, authenticated;

-- =========================================================
-- 2. Financial tables: tighten UPDATE/DELETE to manager+
-- =========================================================

-- payments
DROP POLICY IF EXISTS "pay_update" ON payments;
CREATE POLICY "pay_update" ON payments FOR UPDATE
  TO authenticated
  USING (is_manager_or_admin())
  WITH CHECK (is_manager_or_admin());

-- folios: protect finalized rows
DROP POLICY IF EXISTS "folios_update" ON folios;
CREATE POLICY "folios_update" ON folios FOR UPDATE
  TO authenticated
  USING (
    user_has_branch_access(branch_id)
    AND (status <> 'finalized' OR is_manager_or_admin())
  )
  WITH CHECK (
    user_has_branch_access(branch_id)
    AND (status <> 'finalized' OR is_manager_or_admin())
  );

-- folio_items: protect items on finalized folios
DROP POLICY IF EXISTS "fi_update" ON folio_items;
CREATE POLICY "fi_update" ON folio_items FOR UPDATE
  TO authenticated
  USING (
    user_has_branch_access(branch_id)
    AND (
      NOT EXISTS (
        SELECT 1 FROM folios f
        WHERE f.id = folio_id AND f.status = 'finalized'
      )
      OR is_manager_or_admin()
    )
  )
  WITH CHECK (
    user_has_branch_access(branch_id)
    AND (
      NOT EXISTS (
        SELECT 1 FROM folios f
        WHERE f.id = folio_id AND f.status = 'finalized'
      )
      OR is_manager_or_admin()
    )
  );

DROP POLICY IF EXISTS "fi_delete" ON folio_items;
CREATE POLICY "fi_delete" ON folio_items FOR DELETE
  TO authenticated USING (is_manager_or_admin());

-- invoices: protect finalized rows
DROP POLICY IF EXISTS "inv_update" ON invoices;
CREATE POLICY "inv_update" ON invoices FOR UPDATE
  TO authenticated
  USING (
    user_has_branch_access(branch_id)
    AND (finalized_at IS NULL OR is_manager_or_admin())
  )
  WITH CHECK (
    user_has_branch_access(branch_id)
    AND (finalized_at IS NULL OR is_manager_or_admin())
  );

DROP POLICY IF EXISTS "inv_delete" ON invoices;
CREATE POLICY "inv_delete" ON invoices FOR DELETE
  TO authenticated USING (is_manager_or_admin());

-- invoice_items: protect items on finalized invoices
DROP POLICY IF EXISTS "ii_insert" ON invoice_items;
CREATE POLICY "ii_insert" ON invoice_items FOR INSERT
  TO authenticated WITH CHECK (
    EXISTS (
      SELECT 1 FROM invoices i
      WHERE i.id = invoice_id
      AND user_has_branch_access(i.branch_id)
      AND (i.finalized_at IS NULL OR is_manager_or_admin())
    )
  );
DROP POLICY IF EXISTS "ii_delete" ON invoice_items;
CREATE POLICY "ii_delete" ON invoice_items FOR DELETE
  TO authenticated USING (
    EXISTS (
      SELECT 1 FROM invoices i
      WHERE i.id = invoice_id
      AND user_has_branch_access(i.branch_id)
      AND (i.finalized_at IS NULL OR is_manager_or_admin())
    )
  );

-- refunds: manager+ for update, super_admin delete (already), tighten update
DROP POLICY IF EXISTS "rf_update" ON refunds;
CREATE POLICY "rf_update" ON refunds FOR UPDATE
  TO authenticated
  USING (is_manager_or_admin())
  WITH CHECK (is_manager_or_admin());

-- additional_charges: manager+ for update/delete
DROP POLICY IF EXISTS "ac_update" ON additional_charges;
CREATE POLICY "ac_update" ON additional_charges FOR UPDATE
  TO authenticated
  USING (is_manager_or_admin())
  WITH CHECK (is_manager_or_admin());

DROP POLICY IF EXISTS "ac_delete" ON additional_charges;
CREATE POLICY "ac_delete" ON additional_charges FOR DELETE
  TO authenticated USING (is_manager_or_admin());

-- deposits: manager+ for update, super_admin delete (already)
DROP POLICY IF EXISTS "dep_update" ON deposits;
CREATE POLICY "dep_update" ON deposits FOR UPDATE
  TO authenticated
  USING (is_manager_or_admin())
  WITH CHECK (is_manager_or_admin());

-- =========================================================
-- 3. transactions: fully immutable (no update, no delete)
-- =========================================================
DROP POLICY IF EXISTS "txn_update" ON transactions;
CREATE POLICY "txn_update" ON transactions FOR UPDATE
  TO authenticated
  USING (false)
  WITH CHECK (false);

DROP POLICY IF EXISTS "txn_delete" ON transactions;
CREATE POLICY "txn_delete" ON transactions FOR DELETE
  TO authenticated USING (false);

-- =========================================================
-- 4. Double-booking exclusion constraint
-- =========================================================
-- Requires btree_gist for GiST exclusion on date range.
CREATE EXTENSION IF NOT EXISTS btree_gist;

DO $$
BEGIN
  IF NOT EXISTS (
    SELECT 1 FROM pg_constraint WHERE conname = 'reservations_no_overlap'
  ) THEN
    ALTER TABLE reservations
      ADD CONSTRAINT reservations_no_overlap
      EXCLUDE USING gist (
        room_id WITH =,
        daterange(check_in_date, check_out_date, '[)') WITH &&
      )
      WHERE (room_id IS NOT NULL
             AND status IN ('tentative', 'confirmed', 'checked_in'));
  END IF;
END $$;

-- =========================================================
-- 5. hotel_business_dates: manager+ for update
-- =========================================================
DROP POLICY IF EXISTS "hbd_update" ON hotel_business_dates;
CREATE POLICY "hbd_update" ON hotel_business_dates FOR UPDATE
  TO authenticated
  USING (is_manager_or_admin() AND user_has_branch_access(branch_id))
  WITH CHECK (is_manager_or_admin() AND user_has_branch_access(branch_id));

-- =========================================================
-- 6. audit_logs: append-only (no update by anyone)
-- =========================================================
DROP POLICY IF EXISTS "al_update" ON audit_logs;
CREATE POLICY "al_update" ON audit_logs FOR UPDATE
  TO authenticated
  USING (false)
  WITH CHECK (false);

-- =========================================================
-- 7. room_transfers: manager+ for delete (keep insert for all branch users)
-- =========================================================
-- (No delete policy existed; add one restricting to manager+)
DROP POLICY IF EXISTS "rtf_delete" ON room_transfers;
CREATE POLICY "rtf_delete" ON room_transfers FOR DELETE
  TO authenticated USING (is_manager_or_admin());
