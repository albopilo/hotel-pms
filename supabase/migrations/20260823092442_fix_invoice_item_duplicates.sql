/*
# Fix invoice item duplicates

## Problem
The `syncFromFolio` function in the invoice service uses a delete-then-insert pattern.
When called concurrently (e.g. React StrictMode double-invoking useEffect), two calls
can overlap: both delete, then both insert, producing duplicate invoice_items rows
with the same (invoice_id, folio_item_id) pair. This causes room charges to appear
twice on invoices even though the total is only calculated once.

## Changes
1. Remove existing duplicate invoice_items rows, keeping only the oldest copy of each
   (invoice_id, folio_item_id) pair.
2. Add a UNIQUE constraint on (invoice_id, folio_item_id) so duplicates can never be
   inserted again — the application will switch to upsert.
*/

-- 1. Remove duplicates: keep the row with the smallest created_at per (invoice_id, folio_item_id)
DELETE FROM invoice_items
WHERE id IN (
  SELECT id FROM (
    SELECT
      i.id,
      ROW_NUMBER() OVER (
        PARTITION BY i.invoice_id, i.folio_item_id
        ORDER BY i.created_at ASC
      ) AS rn
    FROM invoice_items i
    WHERE i.folio_item_id IS NOT NULL
  ) ranked
  WHERE ranked.rn > 1
);

-- 2. Prevent future duplicates
CREATE UNIQUE INDEX IF NOT EXISTS invoice_items_invoice_folio_uniq
  ON invoice_items (invoice_id, folio_item_id)
  WHERE folio_item_id IS NOT NULL;
