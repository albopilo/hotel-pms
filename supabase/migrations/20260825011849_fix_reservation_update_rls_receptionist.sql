-- Allow receptionists to update reservations (for check-in/check-out).
-- Previously only super_admin and manager could update, which silently
-- blocked receptionists from completing check-ins — the UPDATE affected
-- 0 rows and returned no error, so the reservation stayed "confirmed".

DROP POLICY IF EXISTS res_update ON reservations;

CREATE POLICY res_update ON reservations FOR UPDATE
  TO authenticated
  USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));
