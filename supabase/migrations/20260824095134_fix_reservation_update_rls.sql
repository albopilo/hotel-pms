/*
# Restrict reservation UPDATE to manager and super_admin

1. Security Changes
- Replaces the existing `res_update` RLS policy on the `reservations` table.
- Previously, any authenticated user with branch access could edit reservations
  (including receptionists).
- Now, only `manager` and `super_admin` roles can UPDATE reservation records.
  Receptionists retain SELECT and INSERT access (they can view and create
  reservations but cannot modify existing ones).
2. Notes
- SELECT and INSERT policies remain unchanged (receptionists can still view
  and create reservations).
- DELETE policy remains restricted to super_admin/manager (unchanged).
*/

-- Drop the old update policy that allowed all branch-access users to update
DROP POLICY IF EXISTS "res_update" ON reservations;

-- Create new update policy restricted to manager and super_admin
CREATE POLICY "res_update" ON reservations FOR UPDATE
  TO authenticated
  USING (
    user_has_branch_access(branch_id)
    AND current_user_role() IN ('super_admin', 'manager')
  )
  WITH CHECK (
    user_has_branch_access(branch_id)
    AND current_user_role() IN ('super_admin', 'manager')
  );
