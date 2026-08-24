/*
# Group Reservations, Extend Stay, and Split Room Support

1. New Tables
- `reservation_rooms` — Links a reservation to multiple rooms (group reservations).
  Each row represents one room within a group booking, with its own room type,
  rate, check-in/check-out dates, and status. This allows a single reservation
  to book multiple rooms of different types simultaneously.
  - `id` (uuid, primary key)
  - `reservation_id` (uuid, FK to reservations, cascade delete)
  - `branch_id` (uuid, FK to branches)
  - `room_id` (uuid, FK to rooms, nullable until assigned)
  - `room_type_id` (uuid, FK to room_types)
  - `rate` (numeric, per-night rate for this room)
  - `check_in_date` (date)
  - `check_out_date` (date)
  - `num_nights` (int, computed from dates)
  - `status` (text: active, split, cancelled — tracks whether this room
    was split into a separate reservation or is still part of the group)
  - `created_at`, `updated_at` (timestamps)

2. Modified Tables
- `reservations` — Added `parent_reservation_id` (uuid, nullable, self-referencing)
  to track reservations created by splitting a room from a group reservation.
  Added `is_group` (boolean, default false) to flag group reservations.

3. Security
- RLS enabled on `reservation_rooms` with branch-scoped access matching
  the existing reservations policy pattern.

4. Notes
- The existing single-room reservation flow continues to work: when a
  reservation has only one room, it can use either the legacy `room_id`/
  `room_type_id`/`rate` columns on the reservations table OR the new
  `reservation_rooms` table. The UI will use `reservation_rooms` for
  multi-room bookings.
- Splitting a room from a group creates a NEW reservation row with
  `parent_reservation_id` pointing back to the original, and moves the
  `reservation_rooms` row's status to 'split'.
*/

-- Add group reservation support columns to reservations table
DO $$ BEGIN
  IF NOT EXISTS (
    SELECT 1 FROM information_schema.columns
    WHERE table_name = 'reservations' AND column_name = 'parent_reservation_id'
  ) THEN
    ALTER TABLE reservations ADD COLUMN parent_reservation_id uuid REFERENCES reservations(id) ON DELETE SET NULL;
  END IF;
END $$;

DO $$ BEGIN
  IF NOT EXISTS (
    SELECT 1 FROM information_schema.columns
    WHERE table_name = 'reservations' AND column_name = 'is_group'
  ) THEN
    ALTER TABLE reservations ADD COLUMN is_group boolean NOT NULL DEFAULT false;
  END IF;
END $$;

-- Create reservation_rooms table for group reservations
CREATE TABLE IF NOT EXISTS reservation_rooms (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  reservation_id uuid NOT NULL REFERENCES reservations(id) ON DELETE CASCADE,
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  room_id uuid REFERENCES rooms(id) ON DELETE SET NULL,
  room_type_id uuid REFERENCES room_types(id) ON DELETE SET NULL,
  rate numeric(14,2) NOT NULL DEFAULT 0,
  check_in_date date NOT NULL,
  check_out_date date NOT NULL,
  num_nights int NOT NULL DEFAULT 1,
  status text NOT NULL DEFAULT 'active',
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_reservation_rooms_res ON reservation_rooms(reservation_id);
CREATE INDEX IF NOT EXISTS idx_reservation_rooms_room ON reservation_rooms(room_id);
CREATE INDEX IF NOT EXISTS idx_reservation_rooms_branch ON reservation_rooms(branch_id);

-- RLS for reservation_rooms
ALTER TABLE reservation_rooms ENABLE ROW LEVEL SECURITY;

DROP POLICY IF EXISTS "rr_select" ON reservation_rooms;
CREATE POLICY "rr_select" ON reservation_rooms FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));

DROP POLICY IF EXISTS "rr_insert" ON reservation_rooms;
CREATE POLICY "rr_insert" ON reservation_rooms FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));

DROP POLICY IF EXISTS "rr_update" ON reservation_rooms;
CREATE POLICY "rr_update" ON reservation_rooms FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));

DROP POLICY IF EXISTS "rr_delete" ON reservation_rooms;
CREATE POLICY "rr_delete" ON reservation_rooms FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin' OR current_user_role() = 'manager');
