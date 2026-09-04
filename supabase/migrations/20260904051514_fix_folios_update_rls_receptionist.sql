/*
# Fix folios UPDATE RLS policy for receptionists

## Problem
The `folios_update` policy had a `WITH CHECK` clause that required
`status = 'finalized' OR is_manager_or_admin()`. This meant receptionists
could only update a folio TO 'finalized' status — they were blocked from
updating open folios (e.g. when syncing totals after adding charges).

This silently broke the "Extend Stay" flow for receptionists:
- The reservation UPDATE succeeded (res_update allows branch access).
- The folio_items INSERT succeeded.
- But `syncFolioTotals` (which UPDATEs the folio with new totals while
  keeping status='open') silently affected 0 rows due to the WITH CHECK.
- The app then showed a generic error because the folio totals were stale.

Super admins bypassed this via `is_manager_or_admin()`.

## Fix
Replace the `folios_update` policy so `WITH CHECK` matches `USING`:
both allow branch users to update non-finalized folios, and managers/admins
can update any folio (including finalized ones).

## Security
- No new tables or columns.
- RLS policy on `folios` updated only.
*/

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