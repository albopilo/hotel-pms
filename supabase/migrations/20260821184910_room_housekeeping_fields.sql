/*
# Room Housekeeping Fields

## Summary
Adds housekeeping-related columns to the `rooms` table to support the new Housekeeping modal on the Rooms page.
Also adds a `revert_after_nights` column to `room_status_history` so the system can track when a room set to Out of Order / Out of Service should automatically revert to dirty.

## New Columns on `rooms`
- `out_of_service_reason` (text, nullable) — reason entered when a room is set to Out of Order or Out of Service.
- `out_of_service_until` (date, nullable) — the business date on which the room should automatically revert to dirty. Computed from "nights until revert" at the time the status is changed.

## New Columns on `room_status_history`
- `revert_after_nights` (int, nullable) — number of nights after which the room should revert to dirty.
- `revert_to` (text, nullable) — the status the room should revert to (always 'dirty').

## Security
- No new tables; RLS already enabled on `rooms` and `room_status_history`.
- No policy changes needed — existing UPDATE policies cover the new columns.

## Important Notes
1. The automatic revert is handled by the nightly audit / business-day-close process (or manually by staff via the Housekeeping modal). This migration only adds the storage columns.
2. `out_of_service_until` is a `date` (not timestamp) so it aligns with the business-day concept.
*/

ALTER TABLE rooms
  ADD COLUMN IF NOT EXISTS out_of_service_reason text,
  ADD COLUMN IF NOT EXISTS out_of_service_until date;

ALTER TABLE room_status_history
  ADD COLUMN IF NOT EXISTS revert_after_nights int,
  ADD COLUMN IF NOT EXISTS revert_to text;
