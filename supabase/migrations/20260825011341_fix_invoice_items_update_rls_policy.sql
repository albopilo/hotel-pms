-- Add the missing UPDATE policy for invoice_items.
--
-- The syncFromFolio() function uses upsert (INSERT ... ON CONFLICT DO UPDATE),
-- which requires both INSERT and UPDATE RLS policies to pass.
-- Without an UPDATE policy, RLS denies all updates by default, causing the
-- "new row violates row-level security policy (USING expression)" error.

CREATE POLICY "ii_update" ON invoice_items FOR UPDATE
  TO authenticated
  USING (
    EXISTS (
      SELECT 1 FROM invoices i
      WHERE i.id = invoice_items.invoice_id
        AND user_has_branch_access(i.branch_id)
        AND (i.finalized_at IS NULL OR is_manager_or_admin())
    )
  )
  WITH CHECK (
    EXISTS (
      SELECT 1 FROM invoices i
      WHERE i.id = invoice_items.invoice_id
        AND user_has_branch_access(i.branch_id)
        AND (i.finalized_at IS NULL OR is_manager_or_admin())
    )
  );
