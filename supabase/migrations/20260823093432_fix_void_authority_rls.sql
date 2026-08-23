/*
# Fix Void Authority: Restrict reservation/folio/invoice voiding to manager + super admin

## Problem
- reservations UPDATE policy allows ANY branch user (including receptionists)
  to change status to 'cancelled', effectively letting receptionists void
  reservations.
- folios UPDATE policy already blocks receptionists from most status changes
  (only allows 'finalized' or manager+), but does not explicitly guard 'void'.
- invoices UPDATE policy allows any branch user to set status to 'void' when
  finalized_at IS NULL — receptionists could void open invoices.

## Changes
1. reservations UPDATE WITH CHECK: when the new status is 'cancelled', require
   is_manager_or_admin(). Other field updates (check_in_time, rate, etc.)
   remain available to any branch-access user.
2. folios UPDATE WITH CHECK: when the new status is 'void', require
   is_manager_or_admin(). Existing finalized-folio protection is preserved.
3. invoices UPDATE WITH CHECK: when the new status is 'void', require
   is_manager_or_admin(). Existing finalized-invoice protection is preserved.

## Security
- All three UPDATE policies now enforce is_manager_or_admin() for the specific
  status-transition to 'cancelled' / 'void'.
- Normal operational updates by receptionists (check-in, check-out, rate
  adjustments, etc.) are unaffected.
- In WITH CHECK expressions, unqualified column names refer to the NEW row
  (PostgreSQL RLS convention), so `status <> 'void'` checks the proposed
  new status.

## Notes
- The helper functions is_manager_or_admin() and user_has_branch_access()
  already exist from prior migrations.
- This migration is idempotent: policies are dropped before re-creation.
*/

-- reservations: receptionists can update normal fields, but only manager+ can cancel
DROP POLICY IF EXISTS "res_update" ON reservations;

CREATE POLICY "res_update"
ON reservations FOR UPDATE
TO authenticated
USING (user_has_branch_access(branch_id))
WITH CHECK (
  user_has_branch_access(branch_id)
  AND (status <> 'cancelled' OR is_manager_or_admin())
);

-- folios: keep existing finalized protection + add void restriction
DROP POLICY IF EXISTS "folios_update" ON folios;

CREATE POLICY "folios_update"
ON folios FOR UPDATE
TO authenticated
USING (
  user_has_branch_access(branch_id)
  AND (status <> 'finalized' OR is_manager_or_admin())
)
WITH CHECK (
  user_has_branch_access(branch_id)
  AND (status = 'finalized' OR is_manager_or_admin())
  AND (status <> 'void' OR is_manager_or_admin())
);

-- invoices: keep existing finalized protection + add void restriction
DROP POLICY IF EXISTS "inv_update" ON invoices;

CREATE POLICY "inv_update"
ON invoices FOR UPDATE
TO authenticated
USING (
  user_has_branch_access(branch_id)
  AND (finalized_at IS NULL OR is_manager_or_admin())
)
WITH CHECK (
  user_has_branch_access(branch_id)
  AND (finalized_at IS NULL OR is_manager_or_admin())
  AND (status <> 'void' OR is_manager_or_admin())
);
