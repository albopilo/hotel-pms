/*
# Archive reservations older than 3 months

1. Plain-English summary
- Reservations whose stay ended more than 3 months ago (before the cutoff date, Jakarta time) are marked as archived.
- Archived reservations, and the folios (payments) and invoices that belong to them, are hidden from the Reservations, Check-in / Check-out, Payments and Invoices pages. This keeps those pages fast.
- Archived data stays in the database. It is still visible in a guest's reservation history and in the new Archive page under Hotel Information.
- A nightly job keeps the flags up to date. Reservations still checked in are never archived, even if their check-out date is old.

2. Modified tables
- `reservations`: new column `is_archived` (boolean, default false).
- `folios`: new column `is_archived` (boolean, default false). Mirrors the flag of the reservation it belongs to.
- `invoices`: new column `is_archived` (boolean, default false). Mirrors the flag of the reservation it belongs to.

3. New indexes
- Partial-free composite indexes on (branch_id, is_archived) for reservations, folios and invoices so the filtered lists stay fast.

4. New function
- `archive_old_reservations()` recalculates `is_archived` for every reservation, then copies the value to the linked folios and invoices. It only writes rows whose flag actually changes. It returns the number of reservations whose flag changed.
- Cutoff rule: check_out_date < (today in Asia/Jakarta minus 3 months) AND status is not 'checked_in'.
- The function is not callable by app users (EXECUTE revoked from PUBLIC, anon and authenticated). It runs from the scheduler.

5. Schedule
- Runs every day at 19:00 UTC (02:00 Jakarta time) via pg_cron under the job name `archive-old-reservations`. Re-running this migration replaces the job safely.

6. Security
- No RLS or policy changes. Existing row access rules still apply to the new columns.

7. Important notes
- The function is run once at the end of this migration so existing old data is archived immediately.
- Running the migration again is safe: the flags are recalculated and only changed rows are written.
*/

ALTER TABLE reservations ADD COLUMN IF NOT EXISTS is_archived boolean NOT NULL DEFAULT false;
ALTER TABLE folios ADD COLUMN IF NOT EXISTS is_archived boolean NOT NULL DEFAULT false;
ALTER TABLE invoices ADD COLUMN IF NOT EXISTS is_archived boolean NOT NULL DEFAULT false;

CREATE INDEX IF NOT EXISTS idx_reservations_branch_archived ON reservations (branch_id, is_archived, created_at DESC);
CREATE INDEX IF NOT EXISTS idx_folios_branch_archived ON folios (branch_id, is_archived, created_at DESC);
CREATE INDEX IF NOT EXISTS idx_invoices_branch_archived ON invoices (branch_id, is_archived, created_at DESC);
CREATE INDEX IF NOT EXISTS idx_invoices_reservation_id ON invoices (reservation_id);

CREATE OR REPLACE FUNCTION archive_old_reservations()
RETURNS integer
LANGUAGE plpgsql
SET search_path = public
AS $$
DECLARE
  v_cutoff date := ((now() AT TIME ZONE 'Asia/Jakarta')::date - interval '3 months')::date;
  v_changed integer;
BEGIN
  UPDATE reservations r
  SET is_archived = (r.check_out_date < v_cutoff AND r.status <> 'checked_in')
  WHERE r.is_archived IS DISTINCT FROM (r.check_out_date < v_cutoff AND r.status <> 'checked_in');
  GET DIAGNOSTICS v_changed = ROW_COUNT;

  UPDATE folios f
  SET is_archived = r.is_archived
  FROM reservations r
  WHERE f.reservation_id = r.id
    AND f.is_archived IS DISTINCT FROM r.is_archived;

  UPDATE invoices i
  SET is_archived = r.is_archived
  FROM reservations r
  WHERE i.reservation_id = r.id
    AND i.is_archived IS DISTINCT FROM r.is_archived;

  RETURN v_changed;
END;
$$;

REVOKE EXECUTE ON FUNCTION archive_old_reservations() FROM PUBLIC, anon, authenticated;

DO $$
BEGIN
  IF EXISTS (SELECT 1 FROM cron.job WHERE jobname = 'archive-old-reservations') THEN
    PERFORM cron.unschedule('archive-old-reservations');
  END IF;
  PERFORM cron.schedule('archive-old-reservations', '0 19 * * *', 'SELECT public.archive_old_reservations()');
END $$;

DO $$
BEGIN
  PERFORM archive_old_reservations();
END $$;
