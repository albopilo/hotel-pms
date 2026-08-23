/*
# Add configuration fields to hotel_lock_integrations

1. Purpose
- Extends the per-branch hotel lock integration record with hardware/network
  configuration needed to drive a real (production) lock bridge and encoder
  instead of the hard-coded mock provider.
- The new fields are optional; existing rows keep working because every
  new column has a safe default.

2. New columns on hotel_lock_integrations
- encoder_port text — COM port / device path the card encoder is attached to
  (e.g. "COM3" on Windows or "/dev/ttyUSB0" on Linux).
- dll_path text — filesystem path to the vendor DLL / native library that the
  local bridge loads to talk to the encoder hardware.
- hotel_identifier text — unique hotel/site identifier issued by the lock
  vendor; written onto every guest card so cards from one hotel cannot open
  doors at another.
- encoding_profile text — named encoding profile the bridge should use
  (e.g. "default", "extended", vendor-specific profile ids).
- auto_poll_enabled boolean NOT NULL DEFAULT false — when true the bridge
  continuously polls the encoder for card-insert events and status changes
  instead of waiting for explicit requests.

3. Security
- No structural changes. RLS is already enabled on hotel_lock_integrations
  (select via user_has_branch_access, insert/update/delete super_admin only).
- The new columns are covered by the existing UPDATE policy automatically
  (column-level privileges are "all" for the authenticated role on this table).

4. Notes
- All additions use IF NOT EXISTS guards so the migration is idempotent and
  safe to re-apply after a timeout.
- No data is lost; no columns are dropped or renamed.
*/

ALTER TABLE hotel_lock_integrations
  ADD COLUMN IF NOT EXISTS encoder_port text,
  ADD COLUMN IF NOT EXISTS dll_path text,
  ADD COLUMN IF NOT EXISTS hotel_identifier text,
  ADD COLUMN IF NOT EXISTS encoding_profile text,
  ADD COLUMN IF NOT EXISTS auto_poll_enabled boolean NOT NULL DEFAULT false;
