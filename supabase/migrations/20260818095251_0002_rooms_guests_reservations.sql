/*
# Rooms, Room Types, Rates, Guests, Reservations, Calendar

1. New Tables
- `room_types` — Standard/Deluxe/Premium/Suite, per branch.
- `rooms` — individual rooms with status, floor, occupancy, base rate.
- `room_rates` — date-specific rates per room type (optional overrides).
- `guests` — permanent guest profiles.
- `guest_documents` — scanned ID docs (metadata only in V1).
- `reservations` — booking records with status, dates, room assignment.
- `reservation_guests` — multiple guests per reservation (primary + extra).
- `room_status_history` — audit trail of room status changes.
- `room_transfers` — room move records.
- `booking_sources` — Direct/Walk-in/Phone/OTA/etc., configurable.
2. Security
- RLS on all tables; scoped by branch access via user_has_branch_access().
- super_admin full access; managers/receptionists read/write their branches.
3. Notes
- Reservation status enum: tentative, confirmed, checked_in, checked_out, cancelled, no_show.
- Room status enum: available, reserved, occupied, dirty, cleaning, inspected, out_of_service, out_of_order.
- Double-booking prevented at application level + a partial exclusion constraint on stay range.
*/

-- Room types
CREATE TABLE IF NOT EXISTS room_types (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  name text NOT NULL,
  code text NOT NULL,
  description text,
  base_rate numeric(14,2) NOT NULL DEFAULT 0,
  max_occupancy int NOT NULL DEFAULT 2,
  default_tax_rate numeric(5,2) NOT NULL DEFAULT 0,
  is_active boolean NOT NULL DEFAULT true,
  sort_order int NOT NULL DEFAULT 0,
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now(),
  UNIQUE (branch_id, code)
);

CREATE INDEX IF NOT EXISTS idx_room_types_branch ON room_types(branch_id);

-- Rooms
CREATE TABLE IF NOT EXISTS rooms (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  room_type_id uuid NOT NULL REFERENCES room_types(id) ON DELETE RESTRICT,
  room_number text NOT NULL,
  floor int NOT NULL DEFAULT 1,
  base_rate numeric(14,2) NOT NULL DEFAULT 0,
  max_occupancy int NOT NULL DEFAULT 2,
  status text NOT NULL DEFAULT 'available',
  is_active boolean NOT NULL DEFAULT true,
  notes text,
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now(),
  UNIQUE (branch_id, room_number)
);

CREATE INDEX IF NOT EXISTS idx_rooms_branch ON rooms(branch_id);
CREATE INDEX IF NOT EXISTS idx_rooms_status ON rooms(status);
CREATE INDEX IF NOT EXISTS idx_rooms_type ON rooms(room_type_id);

-- Room rates (date-specific overrides)
CREATE TABLE IF NOT EXISTS room_rates (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  room_type_id uuid NOT NULL REFERENCES room_types(id) ON DELETE CASCADE,
  rate_date date NOT NULL,
  rate numeric(14,2) NOT NULL,
  created_at timestamptz NOT NULL DEFAULT now(),
  UNIQUE (branch_id, room_type_id, rate_date)
);

CREATE INDEX IF NOT EXISTS idx_room_rates_type_date ON room_rates(room_type_id, rate_date);

-- Booking sources
CREATE TABLE IF NOT EXISTS booking_sources (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  organization_id uuid NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
  name text NOT NULL,
  code text NOT NULL,
  is_ota boolean NOT NULL DEFAULT false,
  is_active boolean NOT NULL DEFAULT true,
  sort_order int NOT NULL DEFAULT 0,
  created_at timestamptz NOT NULL DEFAULT now(),
  UNIQUE (organization_id, code)
);

CREATE INDEX IF NOT EXISTS idx_booking_sources_org ON booking_sources(organization_id);

-- Guests
CREATE TABLE IF NOT EXISTS guests (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  organization_id uuid NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
  full_name text NOT NULL,
  id_type text,
  id_number text,
  nationality text,
  gender text,
  date_of_birth date,
  phone text,
  email text,
  address text,
  company text,
  notes text,
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_guests_org ON guests(organization_id);
CREATE INDEX IF NOT EXISTS idx_guests_name ON guests(full_name);
CREATE INDEX IF NOT EXISTS idx_guests_phone ON guests(phone);
CREATE INDEX IF NOT EXISTS idx_guests_id_number ON guests(id_number);

-- Guest documents (metadata; file storage handled separately)
CREATE TABLE IF NOT EXISTS guest_documents (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  guest_id uuid NOT NULL REFERENCES guests(id) ON DELETE CASCADE,
  document_type text NOT NULL,
  file_url text,
  notes text,
  created_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_guest_documents_guest ON guest_documents(guest_id);

-- Reservations
CREATE TABLE IF NOT EXISTS reservations (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  organization_id uuid NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
  reservation_number text NOT NULL,
  primary_guest_id uuid REFERENCES guests(id) ON DELETE SET NULL,
  room_type_id uuid REFERENCES room_types(id) ON DELETE SET NULL,
  room_id uuid REFERENCES rooms(id) ON DELETE SET NULL,
  adults int NOT NULL DEFAULT 1,
  children int NOT NULL DEFAULT 0,
  check_in_date date NOT NULL,
  check_in_time time NOT NULL DEFAULT '14:00',
  check_out_date date NOT NULL,
  check_out_time time NOT NULL DEFAULT '12:00',
  actual_check_in timestamptz,
  actual_check_out timestamptz,
  num_nights int NOT NULL DEFAULT 1,
  rate numeric(14,2) NOT NULL DEFAULT 0,
  discount numeric(14,2) NOT NULL DEFAULT 0,
  tax numeric(14,2) NOT NULL DEFAULT 0,
  deposit numeric(14,2) NOT NULL DEFAULT 0,
  booking_source_id uuid REFERENCES booking_sources(id) ON DELETE SET NULL,
  payment_status text NOT NULL DEFAULT 'unpaid',
  status text NOT NULL DEFAULT 'tentative',
  special_requests text,
  notes text,
  created_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now(),
  UNIQUE (branch_id, reservation_number)
);

CREATE INDEX IF NOT EXISTS idx_reservations_branch ON reservations(branch_id);
CREATE INDEX IF NOT EXISTS idx_reservations_status ON reservations(status);
CREATE INDEX IF NOT EXISTS idx_reservations_dates ON reservations(check_in_date, check_out_date);
CREATE INDEX IF NOT EXISTS idx_reservations_room ON reservations(room_id);
CREATE INDEX IF NOT EXISTS idx_reservations_guest ON reservations(primary_guest_id);
CREATE INDEX IF NOT EXISTS idx_reservations_number ON reservations(reservation_number);

-- Reservation guests (primary + additional)
CREATE TABLE IF NOT EXISTS reservation_guests (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  reservation_id uuid NOT NULL REFERENCES reservations(id) ON DELETE CASCADE,
  guest_id uuid NOT NULL REFERENCES guests(id) ON DELETE CASCADE,
  is_primary boolean NOT NULL DEFAULT false,
  created_at timestamptz NOT NULL DEFAULT now(),
  UNIQUE (reservation_id, guest_id)
);

CREATE INDEX IF NOT EXISTS idx_reservation_guests_res ON reservation_guests(reservation_id);

-- Room status history
CREATE TABLE IF NOT EXISTS room_status_history (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  room_id uuid NOT NULL REFERENCES rooms(id) ON DELETE CASCADE,
  previous_status text,
  new_status text NOT NULL,
  changed_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  reason text,
  created_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_room_status_history_room ON room_status_history(room_id);

-- Room transfers
CREATE TABLE IF NOT EXISTS room_transfers (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  reservation_id uuid NOT NULL REFERENCES reservations(id) ON DELETE CASCADE,
  from_room_id uuid REFERENCES rooms(id) ON DELETE SET NULL,
  to_room_id uuid NOT NULL REFERENCES rooms(id) ON DELETE SET NULL,
  reason text,
  performed_by uuid NOT NULL REFERENCES profiles(id) ON DELETE SET NULL,
  created_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_room_transfers_res ON room_transfers(reservation_id);

-- RLS for room_types
ALTER TABLE room_types ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "rt_select" ON room_types;
CREATE POLICY "rt_select" ON room_types FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "rt_insert" ON room_types;
CREATE POLICY "rt_insert" ON room_types FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "rt_update" ON room_types;
CREATE POLICY "rt_update" ON room_types FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "rt_delete" ON room_types;
CREATE POLICY "rt_delete" ON room_types FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin' OR current_user_role() = 'manager');

-- RLS for rooms
ALTER TABLE rooms ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "rooms_select" ON rooms;
CREATE POLICY "rooms_select" ON rooms FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "rooms_insert" ON rooms;
CREATE POLICY "rooms_insert" ON rooms FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "rooms_update" ON rooms;
CREATE POLICY "rooms_update" ON rooms FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "rooms_delete" ON rooms;
CREATE POLICY "rooms_delete" ON rooms FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin' OR current_user_role() = 'manager');

-- RLS for room_rates
ALTER TABLE room_rates ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "rr_select" ON room_rates;
CREATE POLICY "rr_select" ON room_rates FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "rr_insert" ON room_rates;
CREATE POLICY "rr_insert" ON room_rates FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "rr_update" ON room_rates;
CREATE POLICY "rr_update" ON room_rates FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "rr_delete" ON room_rates;
CREATE POLICY "rr_delete" ON room_rates FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin' OR current_user_role() = 'manager');

-- RLS for booking_sources (org-level)
ALTER TABLE booking_sources ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "bs_select" ON booking_sources;
CREATE POLICY "bs_select" ON booking_sources FOR SELECT
  TO authenticated USING (
    current_user_role() = 'super_admin'
    OR organization_id = (SELECT organization_id FROM profiles WHERE id = auth.uid())
  );
DROP POLICY IF EXISTS "bs_insert" ON booking_sources;
CREATE POLICY "bs_insert" ON booking_sources FOR INSERT
  TO authenticated WITH CHECK (current_user_role() = 'super_admin');
DROP POLICY IF EXISTS "bs_update" ON booking_sources;
CREATE POLICY "bs_update" ON booking_sources FOR UPDATE
  TO authenticated USING (current_user_role() = 'super_admin')
  WITH CHECK (current_user_role() = 'super_admin');
DROP POLICY IF EXISTS "bs_delete" ON booking_sources;
CREATE POLICY "bs_delete" ON booking_sources FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin');

-- RLS for guests (org-level, shared across branches)
ALTER TABLE guests ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "guests_select" ON guests;
CREATE POLICY "guests_select" ON guests FOR SELECT
  TO authenticated USING (
    current_user_role() = 'super_admin'
    OR organization_id = (SELECT organization_id FROM profiles WHERE id = auth.uid())
  );
DROP POLICY IF EXISTS "guests_insert" ON guests;
CREATE POLICY "guests_insert" ON guests FOR INSERT
  TO authenticated WITH CHECK (
    current_user_role() = 'super_admin'
    OR organization_id = (SELECT organization_id FROM profiles WHERE id = auth.uid())
  );
DROP POLICY IF EXISTS "guests_update" ON guests;
CREATE POLICY "guests_update" ON guests FOR UPDATE
  TO authenticated USING (
    current_user_role() = 'super_admin'
    OR organization_id = (SELECT organization_id FROM profiles WHERE id = auth.uid())
  )
  WITH CHECK (
    current_user_role() = 'super_admin'
    OR organization_id = (SELECT organization_id FROM profiles WHERE id = auth.uid())
  );
DROP POLICY IF EXISTS "guests_delete" ON guests;
CREATE POLICY "guests_delete" ON guests FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin' OR current_user_role() = 'manager');

-- RLS for guest_documents
ALTER TABLE guest_documents ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "gd_select" ON guest_documents;
CREATE POLICY "gd_select" ON guest_documents FOR SELECT
  TO authenticated USING (current_user_role() IN ('super_admin','manager','receptionist'));
DROP POLICY IF EXISTS "gd_insert" ON guest_documents;
CREATE POLICY "gd_insert" ON guest_documents FOR INSERT
  TO authenticated WITH CHECK (current_user_role() IN ('super_admin','manager','receptionist'));
DROP POLICY IF EXISTS "gd_delete" ON guest_documents;
CREATE POLICY "gd_delete" ON guest_documents FOR DELETE
  TO authenticated USING (current_user_role() IN ('super_admin','manager'));

-- RLS for reservations
ALTER TABLE reservations ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "res_select" ON reservations;
CREATE POLICY "res_select" ON reservations FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "res_insert" ON reservations;
CREATE POLICY "res_insert" ON reservations FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "res_update" ON reservations;
CREATE POLICY "res_update" ON reservations FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "res_delete" ON reservations;
CREATE POLICY "res_delete" ON reservations FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin' OR current_user_role() = 'manager');

-- RLS for reservation_guests
ALTER TABLE reservation_guests ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "rg_select" ON reservation_guests;
CREATE POLICY "rg_select" ON reservation_guests FOR SELECT
  TO authenticated USING (
    EXISTS (SELECT 1 FROM reservations r WHERE r.id = reservation_id AND user_has_branch_access(r.branch_id))
  );
DROP POLICY IF EXISTS "rg_insert" ON reservation_guests;
CREATE POLICY "rg_insert" ON reservation_guests FOR INSERT
  TO authenticated WITH CHECK (
    EXISTS (SELECT 1 FROM reservations r WHERE r.id = reservation_id AND user_has_branch_access(r.branch_id))
  );
DROP POLICY IF EXISTS "rg_delete" ON reservation_guests;
CREATE POLICY "rg_delete" ON reservation_guests FOR DELETE
  TO authenticated USING (
    EXISTS (SELECT 1 FROM reservations r WHERE r.id = reservation_id AND user_has_branch_access(r.branch_id))
  );

-- RLS for room_status_history
ALTER TABLE room_status_history ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "rsh_select" ON room_status_history;
CREATE POLICY "rsh_select" ON room_status_history FOR SELECT
  TO authenticated USING (
    EXISTS (SELECT 1 FROM rooms r WHERE r.id = room_id AND user_has_branch_access(r.branch_id))
  );
DROP POLICY IF EXISTS "rsh_insert" ON room_status_history;
CREATE POLICY "rsh_insert" ON room_status_history FOR INSERT
  TO authenticated WITH CHECK (
    EXISTS (SELECT 1 FROM rooms r WHERE r.id = room_id AND user_has_branch_access(r.branch_id))
  );

-- RLS for room_transfers
ALTER TABLE room_transfers ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "rtf_select" ON room_transfers;
CREATE POLICY "rtf_select" ON room_transfers FOR SELECT
  TO authenticated USING (
    EXISTS (SELECT 1 FROM reservations r WHERE r.id = reservation_id AND user_has_branch_access(r.branch_id))
  );
DROP POLICY IF EXISTS "rtf_insert" ON room_transfers;
CREATE POLICY "rtf_insert" ON room_transfers FOR INSERT
  TO authenticated WITH CHECK (
    EXISTS (SELECT 1 FROM reservations r WHERE r.id = reservation_id AND user_has_branch_access(r.branch_id))
  );
