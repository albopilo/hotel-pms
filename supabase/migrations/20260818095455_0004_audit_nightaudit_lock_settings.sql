/*
# Audit Logs, Night Audit, Business Dates, Hotel Lock Integration, Settings

1. New Tables
- `audit_logs` — complete audit trail of all important events.
- `hotel_business_dates` — tracks the current open business date per branch.
- `night_audits` — records of each night audit close.
- `hotel_lock_integrations` — per-branch lock integration config (mock/production).
- `hotel_lock_devices` — encoder devices per branch.
- `hotel_lock_events` — integration event logs.
- `card_issuances` — guest card issuance/replacement/invalidation records.
- `system_settings` — org-level key/value configurable settings.
- `reconciliations` — payment reconciliation records.
2. Security
- RLS on all tables; audit logs readable by super_admin/manager; lock settings admin-only writes.
3. Notes
- Hotel lock integration is abstracted; provider_type = 'mock' | 'production'.
- Card issuance records link to reservation + room + guest; status pending|success|failed.
- system_settings stores: early_checkin_charge, late_checkout_charge, damage_approval_threshold, etc.
*/

-- Audit logs
CREATE TABLE IF NOT EXISTS audit_logs (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid REFERENCES branches(id) ON DELETE SET NULL,
  organization_id uuid NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
  user_id uuid REFERENCES profiles(id) ON DELETE SET NULL,
  action text NOT NULL,
  object_type text,
  object_id uuid,
  previous_value jsonb,
  new_value jsonb,
  reason text,
  created_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_audit_logs_branch ON audit_logs(branch_id);
CREATE INDEX IF NOT EXISTS idx_audit_logs_org ON audit_logs(organization_id);
CREATE INDEX IF NOT EXISTS idx_audit_logs_action ON audit_logs(action);
CREATE INDEX IF NOT EXISTS idx_audit_logs_created ON audit_logs(created_at);
CREATE INDEX IF NOT EXISTS idx_audit_logs_user ON audit_logs(user_id);

-- Hotel business dates
CREATE TABLE IF NOT EXISTS hotel_business_dates (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  business_date date NOT NULL,
  status text NOT NULL DEFAULT 'open', -- open|closed
  closed_at timestamptz,
  closed_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  created_at timestamptz NOT NULL DEFAULT now(),
  UNIQUE (branch_id, business_date)
);

CREATE INDEX IF NOT EXISTS idx_hbd_branch ON hotel_business_dates(branch_id);
CREATE INDEX IF NOT EXISTS idx_hbd_status ON hotel_business_dates(status);

-- Night audits
CREATE TABLE IF NOT EXISTS night_audits (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  business_date date NOT NULL,
  summary jsonb NOT NULL DEFAULT '{}',
  exceptions jsonb NOT NULL DEFAULT '[]',
  arrivals int NOT NULL DEFAULT 0,
  departures int NOT NULL DEFAULT 0,
  in_house int NOT NULL DEFAULT 0,
  checked_in int NOT NULL DEFAULT 0,
  checked_out int NOT NULL DEFAULT 0,
  no_shows int NOT NULL DEFAULT 0,
  cancellations int NOT NULL DEFAULT 0,
  room_charges numeric(14,2) NOT NULL DEFAULT 0,
  additional_charges numeric(14,2) NOT NULL DEFAULT 0,
  payments numeric(14,2) NOT NULL DEFAULT 0,
  cash numeric(14,2) NOT NULL DEFAULT 0,
  edc numeric(14,2) NOT NULL DEFAULT 0,
  ota numeric(14,2) NOT NULL DEFAULT 0,
  refunds numeric(14,2) NOT NULL DEFAULT 0,
  discounts numeric(14,2) NOT NULL DEFAULT 0,
  outstanding numeric(14,2) NOT NULL DEFAULT 0,
  closed_at timestamptz NOT NULL DEFAULT now(),
  closed_by uuid NOT NULL REFERENCES profiles(id) ON DELETE SET NULL,
  created_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_night_audits_branch ON night_audits(branch_id);
CREATE INDEX IF NOT EXISTS idx_night_audits_date ON night_audits(business_date);

-- Hotel lock integrations
CREATE TABLE IF NOT EXISTS hotel_lock_integrations (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  provider_type text NOT NULL DEFAULT 'mock', -- mock|production
  lock_system text NOT NULL DEFAULT 'ZKBiolock',
  lock_model text NOT NULL DEFAULT 'SOLUTION HL400',
  card_technology text NOT NULL DEFAULT 'MIFARE / ISO14443 Type-A',
  bridge_url text,
  bridge_token text,
  is_enabled boolean NOT NULL DEFAULT true,
  connection_status text NOT NULL DEFAULT 'disconnected', -- connected|disconnected
  encoder_status text NOT NULL DEFAULT 'disconnected',
  last_heartbeat timestamptz,
  last_success_encoding timestamptz,
  last_error text,
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now(),
  UNIQUE (branch_id)
);

CREATE INDEX IF NOT EXISTS idx_hli_branch ON hotel_lock_integrations(branch_id);

-- Hotel lock devices (encoders)
CREATE TABLE IF NOT EXISTS hotel_lock_devices (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  integration_id uuid NOT NULL REFERENCES hotel_lock_integrations(id) ON DELETE CASCADE,
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  device_name text NOT NULL,
  device_type text NOT NULL DEFAULT 'encoder',
  status text NOT NULL DEFAULT 'unknown',
  last_seen timestamptz,
  created_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_hld_integration ON hotel_lock_devices(integration_id);

-- Hotel lock events
CREATE TABLE IF NOT EXISTS hotel_lock_events (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  integration_id uuid REFERENCES hotel_lock_integrations(id) ON DELETE CASCADE,
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  event_type text NOT NULL,
  event_data jsonb,
  status text NOT NULL DEFAULT 'info', -- info|success|warning|error
  message text,
  created_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_hle_integration ON hotel_lock_events(integration_id);
CREATE INDEX IF NOT EXISTS idx_hle_branch ON hotel_lock_events(branch_id);
CREATE INDEX IF NOT EXISTS idx_hle_created ON hotel_lock_events(created_at);

-- Card issuances
CREATE TABLE IF NOT EXISTS card_issuances (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  reservation_id uuid NOT NULL REFERENCES reservations(id) ON DELETE CASCADE,
  guest_id uuid REFERENCES guests(id) ON DELETE SET NULL,
  room_id uuid REFERENCES rooms(id) ON DELETE SET NULL,
  issuance_type text NOT NULL DEFAULT 'issue', -- issue|replace|invalidate|extend
  card_sequence int NOT NULL DEFAULT 1,
  valid_from timestamptz,
  valid_until timestamptz,
  status text NOT NULL DEFAULT 'pending', -- pending|success|failed
  failure_reason text,
  provider_type text NOT NULL DEFAULT 'mock',
  performed_by uuid NOT NULL REFERENCES profiles(id) ON DELETE SET NULL,
  created_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_card_issuances_branch ON card_issuances(branch_id);
CREATE INDEX IF NOT EXISTS idx_card_issuances_res ON card_issuances(reservation_id);
CREATE INDEX IF NOT EXISTS idx_card_issuances_status ON card_issuances(status);

-- System settings (org-level key/value)
CREATE TABLE IF NOT EXISTS system_settings (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  organization_id uuid NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
  key text NOT NULL,
  value text NOT NULL,
  value_type text NOT NULL DEFAULT 'string', -- string|number|boolean|json
  description text,
  category text NOT NULL DEFAULT 'general',
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now(),
  UNIQUE (organization_id, key)
);

CREATE INDEX IF NOT EXISTS idx_system_settings_org ON system_settings(organization_id);
CREATE INDEX IF NOT EXISTS idx_system_settings_key ON system_settings(key);

-- Reconciliations
CREATE TABLE IF NOT EXISTS reconciliations (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  business_date date NOT NULL,
  method_code text NOT NULL, -- cash|edc|ota
  expected_amount numeric(14,2) NOT NULL DEFAULT 0,
  actual_amount numeric(14,2) NOT NULL DEFAULT 0,
  difference numeric(14,2) NOT NULL DEFAULT 0,
  notes text,
  approved_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  approved_at timestamptz,
  created_by uuid NOT NULL REFERENCES profiles(id) ON DELETE SET NULL,
  created_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_reconciliations_branch ON reconciliations(branch_id);
CREATE INDEX IF NOT EXISTS idx_reconciliations_date ON reconciliations(business_date);

-- RLS: audit_logs
ALTER TABLE audit_logs ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "al_select" ON audit_logs;
CREATE POLICY "al_select" ON audit_logs FOR SELECT
  TO authenticated USING (
    current_user_role() = 'super_admin'
    OR (branch_id IS NOT NULL AND user_has_branch_access(branch_id))
    OR (branch_id IS NULL AND organization_id = (SELECT organization_id FROM profiles WHERE id = auth.uid()))
  );
DROP POLICY IF EXISTS "al_insert" ON audit_logs;
CREATE POLICY "al_insert" ON audit_logs FOR INSERT
  TO authenticated WITH CHECK (
    current_user_role() IN ('super_admin','manager','receptionist')
  );
DROP POLICY IF EXISTS "al_delete" ON audit_logs;
CREATE POLICY "al_delete" ON audit_logs FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin');

-- RLS: hotel_business_dates
ALTER TABLE hotel_business_dates ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "hbd_select" ON hotel_business_dates;
CREATE POLICY "hbd_select" ON hotel_business_dates FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "hbd_insert" ON hotel_business_dates;
CREATE POLICY "hbd_insert" ON hotel_business_dates FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "hbd_update" ON hotel_business_dates;
CREATE POLICY "hbd_update" ON hotel_business_dates FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));

-- RLS: night_audits
ALTER TABLE night_audits ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "na_select" ON night_audits;
CREATE POLICY "na_select" ON night_audits FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "na_insert" ON night_audits;
CREATE POLICY "na_insert" ON night_audits FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));

-- RLS: hotel_lock_integrations
ALTER TABLE hotel_lock_integrations ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "hli_select" ON hotel_lock_integrations;
CREATE POLICY "hli_select" ON hotel_lock_integrations FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "hli_insert" ON hotel_lock_integrations;
CREATE POLICY "hli_insert" ON hotel_lock_integrations FOR INSERT
  TO authenticated WITH CHECK (current_user_role() = 'super_admin');
DROP POLICY IF EXISTS "hli_update" ON hotel_lock_integrations;
CREATE POLICY "hli_update" ON hotel_lock_integrations FOR UPDATE
  TO authenticated USING (current_user_role() = 'super_admin')
  WITH CHECK (current_user_role() = 'super_admin');
DROP POLICY IF EXISTS "hli_delete" ON hotel_lock_integrations;
CREATE POLICY "hli_delete" ON hotel_lock_integrations FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin');

-- RLS: hotel_lock_devices
ALTER TABLE hotel_lock_devices ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "hld_select" ON hotel_lock_devices;
CREATE POLICY "hld_select" ON hotel_lock_devices FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "hld_insert" ON hotel_lock_devices;
CREATE POLICY "hld_insert" ON hotel_lock_devices FOR INSERT
  TO authenticated WITH CHECK (current_user_role() = 'super_admin');
DROP POLICY IF EXISTS "hld_update" ON hotel_lock_devices;
CREATE POLICY "hld_update" ON hotel_lock_devices FOR UPDATE
  TO authenticated USING (current_user_role() = 'super_admin')
  WITH CHECK (current_user_role() = 'super_admin');
DROP POLICY IF EXISTS "hld_delete" ON hotel_lock_devices;
CREATE POLICY "hld_delete" ON hotel_lock_devices FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin');

-- RLS: hotel_lock_events
ALTER TABLE hotel_lock_events ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "hle_select" ON hotel_lock_events;
CREATE POLICY "hle_select" ON hotel_lock_events FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "hle_insert" ON hotel_lock_events;
CREATE POLICY "hle_insert" ON hotel_lock_events FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));

-- RLS: card_issuances
ALTER TABLE card_issuances ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "ci_select" ON card_issuances;
CREATE POLICY "ci_select" ON card_issuances FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "ci_insert" ON card_issuances;
CREATE POLICY "ci_insert" ON card_issuances FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "ci_update" ON card_issuances;
CREATE POLICY "ci_update" ON card_issuances FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));

-- RLS: system_settings (org-level)
ALTER TABLE system_settings ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "ss_select" ON system_settings;
CREATE POLICY "ss_select" ON system_settings FOR SELECT
  TO authenticated USING (
    current_user_role() = 'super_admin'
    OR organization_id = (SELECT organization_id FROM profiles WHERE id = auth.uid())
  );
DROP POLICY IF EXISTS "ss_insert" ON system_settings;
CREATE POLICY "ss_insert" ON system_settings FOR INSERT
  TO authenticated WITH CHECK (current_user_role() = 'super_admin');
DROP POLICY IF EXISTS "ss_update" ON system_settings;
CREATE POLICY "ss_update" ON system_settings FOR UPDATE
  TO authenticated USING (current_user_role() = 'super_admin')
  WITH CHECK (current_user_role() = 'super_admin');
DROP POLICY IF EXISTS "ss_delete" ON system_settings;
CREATE POLICY "ss_delete" ON system_settings FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin');

-- RLS: reconciliations
ALTER TABLE reconciliations ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "rc_select" ON reconciliations;
CREATE POLICY "rc_select" ON reconciliations FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "rc_insert" ON reconciliations;
CREATE POLICY "rc_insert" ON reconciliations FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "rc_update" ON reconciliations;
CREATE POLICY "rc_update" ON reconciliations FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));
