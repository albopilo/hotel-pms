/*
# Seed checkout_balance_tolerance default setting

1. Purpose
   - Adds a new organization-wide system setting `checkout_balance_tolerance`
     with a default value of 100 (IDR).
   - This setting controls the maximum absolute folio balance that is still
     considered "settled" at check-out, preventing tiny rounding residuals
     (e.g. 0.1 IDR from tax/discount percentage math) from blocking staff
     from completing checkout.

2. Changes
   - Inserts a `checkout_balance_tolerance` row into `system_settings` for
     every organization that already has at least one system_settings row,
     if the row does not already exist.

3. Security
   - No RLS or policy changes.
   - No new tables or columns.
*/

INSERT INTO system_settings (organization_id, key, value, value_type, category)
SELECT DISTINCT s.organization_id, 'checkout_balance_tolerance', '100', 'number', 'general'
FROM system_settings s
WHERE NOT EXISTS (
  SELECT 1 FROM system_settings s2
  WHERE s2.organization_id = s.organization_id
    AND s2.key = 'checkout_balance_tolerance'
);
