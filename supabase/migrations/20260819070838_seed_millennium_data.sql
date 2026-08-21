/*
# Seed Data — Millennium Hotel Group

Creates the organization, three branches, a super admin auth user,
and room types for each branch. No rooms are created — the super admin
will add those through the app.

1. Organization: Millennium
2. Branches:
   - Sans Vibes Millennium Inn (code: SVM01)
   - Collection O Millennium Inn 2 (code: CMI02)
   - Sans Vibes Millennium Garden (code: SVMG03)
3. Auth user: 21edoardo@gmail.com / 21edoardo (super_admin)
4. Profile linked to auth user
5. Room types per branch:
   - Branch 1: Standard, Deluxe, Premium, Premium Twin, Suite
   - Branch 2: Standard, Deluxe, Suite
   - Branch 3: Standard, Deluxe, Premium, Suite
6. Hotel business dates open for today (all branches)
7. Hotel lock integrations in mock mode (all branches)
8. Booking sources, payment methods, charge categories, system settings
*/

-- ============================================================
-- 1. ORGANIZATION
-- ============================================================
INSERT INTO organizations (id, name, legal_name, address, phone, email, currency, timezone)
VALUES (
  'e0000000-0000-0000-0000-000000000001',
  'Millennium',
  'Millennium Hospitality',
  NULL, NULL, NULL,
  'IDR', 'Asia/Jakarta'
)
ON CONFLICT (id) DO NOTHING;

-- ============================================================
-- 2. AUTH USER (super admin)
-- ============================================================
INSERT INTO auth.users (id, instance_id, aud, role, email, encrypted_password, email_confirmed_at, created_at, updated_at, raw_app_meta_data, raw_user_meta_data)
VALUES (
  'e0000000-0000-0000-0000-000000000010',
  '00000000-0000-0000-0000-000000000000',
  'authenticated', 'authenticated',
  '21edoardo@gmail.com',
  crypt('21edoardo', gen_salt('bf')),
  now(), now(), now(),
  '{"role":"super_admin"}',
  '{"full_name":"Edoardo"}'
)
ON CONFLICT (id) DO NOTHING;

-- Auth identity (email column is generated — omit it)
INSERT INTO auth.identities (id, user_id, provider_id, provider, identity_data, created_at, updated_at)
VALUES (
  gen_random_uuid(),
  'e0000000-0000-0000-0000-000000000010',
  '00000000-0000-0000-0000-000000000000',
  'email',
  '{"sub":"e0000000-0000-0000-0000-000000000010","email":"21edoardo@gmail.com"}'::jsonb,
  now(), now()
)
ON CONFLICT DO NOTHING;

-- ============================================================
-- 3. PROFILE
-- ============================================================
INSERT INTO profiles (id, organization_id, full_name, email, role, is_active, language)
VALUES (
  'e0000000-0000-0000-0000-000000000010',
  'e0000000-0000-0000-0000-000000000001',
  'Edoardo',
  '21edoardo@gmail.com',
  'super_admin',
  true, 'en'
)
ON CONFLICT (id) DO NOTHING;

-- ============================================================
-- 4. BRANCHES
-- ============================================================
INSERT INTO branches (id, organization_id, name, code, timezone, standard_checkin_time, standard_checkout_time, business_day_cutoff, is_active)
VALUES
  ('e0000000-0000-0000-0000-0000000000b1', 'e0000000-0000-0000-0000-000000000001', 'Sans Vibes Millennium Inn', 'SVM01', 'Asia/Jakarta', '14:00', '12:00', '04:30', true),
  ('e0000000-0000-0000-0000-0000000000b2', 'e0000000-0000-0000-0000-000000000001', 'Collection O Millennium Inn 2', 'CMI02', 'Asia/Jakarta', '14:00', '12:00', '04:30', true),
  ('e0000000-0000-0000-0000-0000000000b3', 'e0000000-0000-0000-0000-000000000001', 'Sans Vibes Millennium Garden', 'SVMG03', 'Asia/Jakarta', '14:00', '12:00', '04:30', true)
ON CONFLICT (id) DO NOTHING;

-- ============================================================
-- 5. ROOM TYPES
-- ============================================================
-- Branch 1: Sans Vibes Millennium Inn
INSERT INTO room_types (id, branch_id, name, code, base_rate, max_occupancy, default_tax_rate, is_active, sort_order)
VALUES
  ('e0000000-0000-0000-0000-0000000a0101', 'e0000000-0000-0000-0000-0000000000b1', 'Standard Room', 'STD', 250000, 2, 11, true, 1),
  ('e0000000-0000-0000-0000-0000000a0102', 'e0000000-0000-0000-0000-0000000000b1', 'Deluxe Room', 'DLX', 400000, 2, 11, true, 2),
  ('e0000000-0000-0000-0000-0000000a0103', 'e0000000-0000-0000-0000-0000000000b1', 'Premium Room', 'PRM', 600000, 2, 11, true, 3),
  ('e0000000-0000-0000-0000-0000000a0104', 'e0000000-0000-0000-0000-0000000000b1', 'Premium Twin Room', 'PRT', 650000, 2, 11, true, 4),
  ('e0000000-0000-0000-0000-0000000a0105', 'e0000000-0000-0000-0000-0000000000b1', 'Suite Room', 'STE', 900000, 4, 11, true, 5)
ON CONFLICT (id) DO NOTHING;

-- Branch 2: Collection O Millennium Inn 2
INSERT INTO room_types (id, branch_id, name, code, base_rate, max_occupancy, default_tax_rate, is_active, sort_order)
VALUES
  ('e0000000-0000-0000-0000-0000000a0201', 'e0000000-0000-0000-0000-0000000000b2', 'Standard Room', 'STD', 250000, 2, 11, true, 1),
  ('e0000000-0000-0000-0000-0000000a0202', 'e0000000-0000-0000-0000-0000000000b2', 'Deluxe Room', 'DLX', 400000, 2, 11, true, 2),
  ('e0000000-0000-0000-0000-0000000a0203', 'e0000000-0000-0000-0000-0000000000b2', 'Suite Room', 'STE', 900000, 4, 11, true, 3)
ON CONFLICT (id) DO NOTHING;

-- Branch 3: Sans Vibes Millennium Garden
INSERT INTO room_types (id, branch_id, name, code, base_rate, max_occupancy, default_tax_rate, is_active, sort_order)
VALUES
  ('e0000000-0000-0000-0000-0000000a0301', 'e0000000-0000-0000-0000-0000000000b3', 'Standard Room', 'STD', 250000, 2, 11, true, 1),
  ('e0000000-0000-0000-0000-0000000a0302', 'e0000000-0000-0000-0000-0000000000b3', 'Deluxe Room', 'DLX', 400000, 2, 11, true, 2),
  ('e0000000-0000-0000-0000-0000000a0303', 'e0000000-0000-0000-0000-0000000000b3', 'Premium Room', 'PRM', 600000, 2, 11, true, 3),
  ('e0000000-0000-0000-0000-0000000a0304', 'e0000000-0000-0000-0000-0000000000b3', 'Suite Room', 'STE', 900000, 4, 11, true, 4)
ON CONFLICT (id) DO NOTHING;

-- ============================================================
-- 6. BOOKING SOURCES
-- ============================================================
INSERT INTO booking_sources (id, organization_id, name, code, is_ota, is_active, sort_order)
VALUES
  ('e0000000-0000-0000-0000-0000000000e1', 'e0000000-0000-0000-0000-000000000001', 'Direct', 'DIRECT', false, true, 1),
  ('e0000000-0000-0000-0000-0000000000e2', 'e0000000-0000-0000-0000-000000000001', 'Walk-in', 'WALKIN', false, true, 2),
  ('e0000000-0000-0000-0000-0000000000e3', 'e0000000-0000-0000-0000-000000000001', 'Phone', 'PHONE', false, true, 3),
  ('e0000000-0000-0000-0000-0000000000e4', 'e0000000-0000-0000-0000-000000000001', 'WhatsApp', 'WA', false, true, 4),
  ('e0000000-0000-0000-0000-0000000000e5', 'e0000000-0000-0000-0000-000000000001', 'Booking.com', 'BCOM', true, true, 5),
  ('e0000000-0000-0000-0000-0000000000e6', 'e0000000-0000-0000-0000-000000000001', 'Agoda', 'AGODA', true, true, 6),
  ('e0000000-0000-0000-0000-0000000000e7', 'e0000000-0000-0000-0000-000000000001', 'Traveloka', 'TRAVA', true, true, 7),
  ('e0000000-0000-0000-0000-0000000000e8', 'e0000000-0000-0000-0000-000000000001', 'Tiket.com', 'TIKET', true, true, 8),
  ('e0000000-0000-0000-0000-0000000000e9', 'e0000000-0000-0000-0000-000000000001', 'OYO', 'OYO', true, true, 9),
  ('e0000000-0000-0000-0000-0000000000ea', 'e0000000-0000-0000-0000-000000000001', 'RedDoorz', 'RDDRZ', true, true, 10),
  ('e0000000-0000-0000-0000-0000000000eb', 'e0000000-0000-0000-0000-000000000001', 'Other OTA', 'OTA_OTH', true, true, 11),
  ('e0000000-0000-0000-0000-0000000000ec', 'e0000000-0000-0000-0000-000000000001', 'Other', 'OTHER', false, true, 12)
ON CONFLICT (id) DO NOTHING;

-- ============================================================
-- 7. PAYMENT METHODS
-- ============================================================
INSERT INTO payment_methods (id, organization_id, name, code, is_edc, is_ota, is_cash, is_active, sort_order)
VALUES
  ('e0000000-0000-0000-0000-0000000000d1', 'e0000000-0000-0000-0000-000000000001', 'Cash', 'CASH', false, false, true, true, 1),
  ('e0000000-0000-0000-0000-0000000000d2', 'e0000000-0000-0000-0000-000000000001', 'EDC', 'EDC', true, false, false, true, 2),
  ('e0000000-0000-0000-0000-0000000000d3', 'e0000000-0000-0000-0000-000000000001', 'OTA / Xendit', 'OTA', false, true, false, true, 3)
ON CONFLICT (id) DO NOTHING;

-- ============================================================
-- 8. CHARGE CATEGORIES
-- ============================================================
INSERT INTO charge_categories (id, organization_id, name, code, is_damage, requires_approval, approval_threshold, is_active, sort_order)
VALUES
  ('e0000000-0000-0000-0000-0000000000f1', 'e0000000-0000-0000-0000-000000000001', 'Room Charge', 'ROOM', false, false, 0, true, 1),
  ('e0000000-0000-0000-0000-0000000000f2', 'e0000000-0000-0000-0000-000000000001', 'Amenity', 'AMENITY', false, false, 0, true, 2),
  ('e0000000-0000-0000-0000-0000000000f3', 'e0000000-0000-0000-0000-000000000001', 'Early Check-in', 'EARLY_CI', false, false, 0, true, 3),
  ('e0000000-0000-0000-0000-0000000000f4', 'e0000000-0000-0000-0000-000000000001', 'Late Check-out', 'LATE_CO', false, false, 0, true, 4),
  ('e0000000-0000-0000-0000-0000000000f5', 'e0000000-0000-0000-0000-000000000001', 'Damage', 'DAMAGE', true, true, 500000, true, 5),
  ('e0000000-0000-0000-0000-0000000000f6', 'e0000000-0000-0000-0000-000000000001', 'Miscellaneous', 'MISC', false, false, 0, true, 6),
  ('e0000000-0000-0000-0000-0000000000f7', 'e0000000-0000-0000-0000-000000000001', 'Extra Bed', 'EXTRA_BED', false, false, 0, true, 7),
  ('e0000000-0000-0000-0000-0000000000f8', 'e0000000-0000-0000-0000-000000000001', 'Extra Guest', 'EXTRA_GUEST', false, false, 0, true, 8)
ON CONFLICT (id) DO NOTHING;

-- ============================================================
-- 9. SYSTEM SETTINGS
-- ============================================================
INSERT INTO system_settings (organization_id, key, value, value_type, description, category)
VALUES
  ('e0000000-0000-0000-0000-000000000001', 'early_checkin_charge', '100000', 'number', 'Default early check-in charge in IDR', 'general'),
  ('e0000000-0000-0000-0000-000000000001', 'late_checkout_charge', '100000', 'number', 'Default late check-out charge in IDR', 'general'),
  ('e0000000-0000-0000-0000-000000000001', 'damage_approval_threshold', '500000', 'number', 'Damage charges above this require manager approval', 'general'),
  ('e0000000-0000-0000-0000-000000000001', 'default_tax_rate', '11', 'number', 'Default tax rate percentage', 'general'),
  ('e0000000-0000-0000-0000-000000000001', 'invoice_prefix', 'INV', 'string', 'Invoice number prefix', 'general'),
  ('e0000000-0000-0000-0000-000000000001', 'reservation_prefix', 'RES', 'string', 'Reservation number prefix', 'general')
ON CONFLICT (organization_id, key) DO NOTHING;

-- ============================================================
-- 10. HOTEL BUSINESS DATES (open for today)
-- ============================================================
INSERT INTO hotel_business_dates (branch_id, business_date, status)
VALUES
  ('e0000000-0000-0000-0000-0000000000b1', CURRENT_DATE, 'open'),
  ('e0000000-0000-0000-0000-0000000000b2', CURRENT_DATE, 'open'),
  ('e0000000-0000-0000-0000-0000000000b3', CURRENT_DATE, 'open')
ON CONFLICT (branch_id, business_date) DO NOTHING;

-- ============================================================
-- 11. HOTEL LOCK INTEGRATIONS (mock mode)
-- ============================================================
INSERT INTO hotel_lock_integrations (branch_id, provider_type, lock_system, lock_model, card_technology, is_enabled, connection_status, encoder_status)
VALUES
  ('e0000000-0000-0000-0000-0000000000b1', 'mock', 'ZKBiolock', 'SOLUTION HL400', 'MIFARE / ISO14443 Type-A', true, 'disconnected', 'disconnected'),
  ('e0000000-0000-0000-0000-0000000000b2', 'mock', 'ZKBiolock', 'SOLUTION HL400', 'MIFARE / ISO14443 Type-A', true, 'disconnected', 'disconnected'),
  ('e0000000-0000-0000-0000-0000000000b3', 'mock', 'ZKBiolock', 'SOLUTION HL400', 'MIFARE / ISO14443 Type-A', true, 'disconnected', 'disconnected')
ON CONFLICT (branch_id) DO NOTHING;
