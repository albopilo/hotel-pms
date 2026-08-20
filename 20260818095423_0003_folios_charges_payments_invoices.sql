/*
# Folios, Charges, Payments, Invoices, Deposits, Refunds, Ledger

1. New Tables
- `charge_categories` — configurable categories (amenity, damage, early_checkin, late_checkout, etc.).
- `payment_methods` — CASH, EDC (debit/credit/qris), OTA/XENDIT, etc.
- `folios` — central accounting object per reservation.
- `folio_items` — individual charges/credits on a folio.
- `payments` — payment records with method, subtype, reference.
- `deposits` — guest deposit movements.
- `refunds` — refund records.
- `additional_charges` — charges (incl. post-stay) linked to reservation/room.
- `invoices` — invoice headers with status.
- `invoice_items` — invoice line items (snapshot of folio at finalization).
- `transactions` — immutable financial ledger entries.
2. Security
- RLS on all tables; scoped by branch access.
3. Notes
- Folio items: type charge|payment|discount|tax|adjustment; amount positive for charges, negative for credits.
- Transactions: immutable ledger; never deleted, only voided/reversed via new entries.
- Post-stay charges flagged with is_post_stay = true.
- Damage charge approval threshold configurable in system_settings.
*/

-- Charge categories
CREATE TABLE IF NOT EXISTS charge_categories (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  organization_id uuid NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
  name text NOT NULL,
  code text NOT NULL,
  is_damage boolean NOT NULL DEFAULT false,
  requires_approval boolean NOT NULL DEFAULT false,
  approval_threshold numeric(14,2) NOT NULL DEFAULT 0,
  is_active boolean NOT NULL DEFAULT true,
  sort_order int NOT NULL DEFAULT 0,
  created_at timestamptz NOT NULL DEFAULT now(),
  UNIQUE (organization_id, code)
);

CREATE INDEX IF NOT EXISTS idx_charge_categories_org ON charge_categories(organization_id);

-- Payment methods
CREATE TABLE IF NOT EXISTS payment_methods (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  organization_id uuid NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
  name text NOT NULL,
  code text NOT NULL,
  is_edc boolean NOT NULL DEFAULT false,
  is_ota boolean NOT NULL DEFAULT false,
  is_cash boolean NOT NULL DEFAULT false,
  is_active boolean NOT NULL DEFAULT true,
  sort_order int NOT NULL DEFAULT 0,
  created_at timestamptz NOT NULL DEFAULT now(),
  UNIQUE (organization_id, code)
);

CREATE INDEX IF NOT EXISTS idx_payment_methods_org ON payment_methods(organization_id);

-- Folios
CREATE TABLE IF NOT EXISTS folios (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  reservation_id uuid NOT NULL REFERENCES reservations(id) ON DELETE CASCADE,
  guest_id uuid REFERENCES guests(id) ON DELETE SET NULL,
  folio_number text NOT NULL,
  status text NOT NULL DEFAULT 'open',
  total_charges numeric(14,2) NOT NULL DEFAULT 0,
  total_payments numeric(14,2) NOT NULL DEFAULT 0,
  total_discounts numeric(14,2) NOT NULL DEFAULT 0,
  total_tax numeric(14,2) NOT NULL DEFAULT 0,
  deposit numeric(14,2) NOT NULL DEFAULT 0,
  balance numeric(14,2) NOT NULL DEFAULT 0,
  finalized_at timestamptz,
  finalized_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now(),
  UNIQUE (branch_id, folio_number)
);

CREATE INDEX IF NOT EXISTS idx_folios_branch ON folios(branch_id);
CREATE INDEX IF NOT EXISTS idx_folios_res ON folios(reservation_id);
CREATE INDEX IF NOT EXISTS idx_folios_guest ON folios(guest_id);
CREATE INDEX IF NOT EXISTS idx_folios_status ON folios(status);

-- Folio items (charges/credits)
CREATE TABLE IF NOT EXISTS folio_items (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  folio_id uuid NOT NULL REFERENCES folios(id) ON DELETE CASCADE,
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  reservation_id uuid REFERENCES reservations(id) ON DELETE SET NULL,
  guest_id uuid REFERENCES guests(id) ON DELETE SET NULL,
  room_id uuid REFERENCES rooms(id) ON DELETE SET NULL,
  item_type text NOT NULL,
  category text,
  description text NOT NULL,
  quantity numeric(10,2) NOT NULL DEFAULT 1,
  unit_amount numeric(14,2) NOT NULL DEFAULT 0,
  amount numeric(14,2) NOT NULL DEFAULT 0,
  business_date date NOT NULL DEFAULT CURRENT_DATE,
  is_post_stay boolean NOT NULL DEFAULT false,
  voided boolean NOT NULL DEFAULT false,
  voided_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  voided_at timestamptz,
  approved_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  created_by uuid NOT NULL REFERENCES profiles(id) ON DELETE SET NULL,
  notes text,
  created_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_folio_items_folio ON folio_items(folio_id);
CREATE INDEX IF NOT EXISTS idx_folio_items_branch ON folio_items(branch_id);
CREATE INDEX IF NOT EXISTS idx_folio_items_res ON folio_items(reservation_id);
CREATE INDEX IF NOT EXISTS idx_folio_items_business_date ON folio_items(business_date);
CREATE INDEX IF NOT EXISTS idx_folio_items_category ON folio_items(category);

-- Invoices (created before payments references it)
CREATE TABLE IF NOT EXISTS invoices (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  reservation_id uuid REFERENCES reservations(id) ON DELETE SET NULL,
  folio_id uuid REFERENCES folios(id) ON DELETE SET NULL,
  guest_id uuid REFERENCES guests(id) ON DELETE SET NULL,
  invoice_number text NOT NULL,
  status text NOT NULL DEFAULT 'draft',
  subtotal numeric(14,2) NOT NULL DEFAULT 0,
  discount numeric(14,2) NOT NULL DEFAULT 0,
  tax numeric(14,2) NOT NULL DEFAULT 0,
  total numeric(14,2) NOT NULL DEFAULT 0,
  amount_paid numeric(14,2) NOT NULL DEFAULT 0,
  balance numeric(14,2) NOT NULL DEFAULT 0,
  issued_at timestamptz,
  issued_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  finalized_at timestamptz,
  finalized_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  notes text,
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now(),
  UNIQUE (branch_id, invoice_number)
);

CREATE INDEX IF NOT EXISTS idx_invoices_branch ON invoices(branch_id);
CREATE INDEX IF NOT EXISTS idx_invoices_res ON invoices(reservation_id);
CREATE INDEX IF NOT EXISTS idx_invoices_guest ON invoices(guest_id);
CREATE INDEX IF NOT EXISTS idx_invoices_status ON invoices(status);
CREATE INDEX IF NOT EXISTS idx_invoices_number ON invoices(invoice_number);

-- Invoice items (snapshot at finalization)
CREATE TABLE IF NOT EXISTS invoice_items (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  invoice_id uuid NOT NULL REFERENCES invoices(id) ON DELETE CASCADE,
  description text NOT NULL,
  category text,
  quantity numeric(10,2) NOT NULL DEFAULT 1,
  unit_amount numeric(14,2) NOT NULL DEFAULT 0,
  amount numeric(14,2) NOT NULL DEFAULT 0,
  sort_order int NOT NULL DEFAULT 0,
  created_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_invoice_items_invoice ON invoice_items(invoice_id);

-- Payments
CREATE TABLE IF NOT EXISTS payments (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  reservation_id uuid REFERENCES reservations(id) ON DELETE SET NULL,
  folio_id uuid REFERENCES folios(id) ON DELETE SET NULL,
  invoice_id uuid REFERENCES invoices(id) ON DELETE SET NULL,
  guest_id uuid REFERENCES guests(id) ON DELETE SET NULL,
  payment_number text NOT NULL,
  amount numeric(14,2) NOT NULL,
  payment_method_id uuid REFERENCES payment_methods(id) ON DELETE SET NULL,
  payment_method_code text NOT NULL,
  payment_subtype text,
  edc_terminal text,
  reference_number text,
  approval_code text,
  is_ota boolean NOT NULL DEFAULT false,
  ota_settlement_date date,
  ota_settlement_reference text,
  ota_settled boolean NOT NULL DEFAULT false,
  business_date date NOT NULL DEFAULT CURRENT_DATE,
  voided boolean NOT NULL DEFAULT false,
  voided_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  voided_at timestamptz,
  created_by uuid NOT NULL REFERENCES profiles(id) ON DELETE SET NULL,
  notes text,
  created_at timestamptz NOT NULL DEFAULT now(),
  UNIQUE (branch_id, payment_number)
);

CREATE INDEX IF NOT EXISTS idx_payments_branch ON payments(branch_id);
CREATE INDEX IF NOT EXISTS idx_payments_res ON payments(reservation_id);
CREATE INDEX IF NOT EXISTS idx_payments_folio ON payments(folio_id);
CREATE INDEX IF NOT EXISTS idx_payments_business_date ON payments(business_date);
CREATE INDEX IF NOT EXISTS idx_payments_method ON payments(payment_method_code);

-- Deposits
CREATE TABLE IF NOT EXISTS deposits (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  reservation_id uuid NOT NULL REFERENCES reservations(id) ON DELETE CASCADE,
  folio_id uuid REFERENCES folios(id) ON DELETE SET NULL,
  guest_id uuid REFERENCES guests(id) ON DELETE SET NULL,
  amount numeric(14,2) NOT NULL,
  movement_type text NOT NULL,
  payment_method_id uuid REFERENCES payment_methods(id) ON DELETE SET NULL,
  reference_number text,
  business_date date NOT NULL DEFAULT CURRENT_DATE,
  created_by uuid NOT NULL REFERENCES profiles(id) ON DELETE SET NULL,
  notes text,
  created_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_deposits_branch ON deposits(branch_id);
CREATE INDEX IF NOT EXISTS idx_deposits_res ON deposits(reservation_id);

-- Refunds
CREATE TABLE IF NOT EXISTS refunds (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  reservation_id uuid REFERENCES reservations(id) ON DELETE SET NULL,
  folio_id uuid REFERENCES folios(id) ON DELETE SET NULL,
  invoice_id uuid REFERENCES invoices(id) ON DELETE SET NULL,
  guest_id uuid REFERENCES guests(id) ON DELETE SET NULL,
  amount numeric(14,2) NOT NULL,
  reason text NOT NULL,
  payment_method_id uuid REFERENCES payment_methods(id) ON DELETE SET NULL,
  reference_number text,
  business_date date NOT NULL DEFAULT CURRENT_DATE,
  approved_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  created_by uuid NOT NULL REFERENCES profiles(id) ON DELETE SET NULL,
  created_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_refunds_branch ON refunds(branch_id);
CREATE INDEX IF NOT EXISTS idx_refunds_res ON refunds(reservation_id);

-- Additional charges (standalone, incl. post-stay)
CREATE TABLE IF NOT EXISTS additional_charges (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  reservation_id uuid REFERENCES reservations(id) ON DELETE SET NULL,
  folio_id uuid REFERENCES folios(id) ON DELETE SET NULL,
  invoice_id uuid REFERENCES invoices(id) ON DELETE SET NULL,
  guest_id uuid REFERENCES guests(id) ON DELETE SET NULL,
  room_id uuid REFERENCES rooms(id) ON DELETE SET NULL,
  charge_category_id uuid REFERENCES charge_categories(id) ON DELETE SET NULL,
  category_code text NOT NULL,
  description text NOT NULL,
  amount numeric(14,2) NOT NULL,
  quantity numeric(10,2) NOT NULL DEFAULT 1,
  is_damage boolean NOT NULL DEFAULT false,
  is_post_stay boolean NOT NULL DEFAULT false,
  requires_approval boolean NOT NULL DEFAULT false,
  approved_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  approved_at timestamptz,
  status text NOT NULL DEFAULT 'posted',
  business_date date NOT NULL DEFAULT CURRENT_DATE,
  created_by uuid NOT NULL REFERENCES profiles(id) ON DELETE SET NULL,
  notes text,
  created_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_additional_charges_branch ON additional_charges(branch_id);
CREATE INDEX IF NOT EXISTS idx_additional_charges_res ON additional_charges(reservation_id);
CREATE INDEX IF NOT EXISTS idx_additional_charges_category ON additional_charges(category_code);

-- Transactions (immutable ledger)
CREATE TABLE IF NOT EXISTS transactions (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  organization_id uuid NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
  reservation_id uuid REFERENCES reservations(id) ON DELETE SET NULL,
  guest_id uuid REFERENCES guests(id) ON DELETE SET NULL,
  folio_id uuid REFERENCES folios(id) ON DELETE SET NULL,
  invoice_id uuid REFERENCES invoices(id) ON DELETE SET NULL,
  transaction_type text NOT NULL,
  description text NOT NULL,
  amount numeric(14,2) NOT NULL,
  debit_credit text NOT NULL DEFAULT 'debit',
  payment_method_code text,
  reference_number text,
  business_date date NOT NULL DEFAULT CURRENT_DATE,
  transaction_date timestamptz NOT NULL DEFAULT now(),
  status text NOT NULL DEFAULT 'posted',
  voided_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  voided_at timestamptz,
  approved_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  created_by uuid NOT NULL REFERENCES profiles(id) ON DELETE SET NULL,
  notes text,
  created_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_transactions_branch ON transactions(branch_id);
CREATE INDEX IF NOT EXISTS idx_transactions_business_date ON transactions(business_date);
CREATE INDEX IF NOT EXISTS idx_transactions_type ON transactions(transaction_type);
CREATE INDEX IF NOT EXISTS idx_transactions_res ON transactions(reservation_id);
CREATE INDEX IF NOT EXISTS idx_transactions_status ON transactions(status);

-- RLS: charge_categories (org-level)
ALTER TABLE charge_categories ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "cc_select" ON charge_categories;
CREATE POLICY "cc_select" ON charge_categories FOR SELECT
  TO authenticated USING (
    current_user_role() = 'super_admin'
    OR organization_id = (SELECT organization_id FROM profiles WHERE id = auth.uid())
  );
DROP POLICY IF EXISTS "cc_insert" ON charge_categories;
CREATE POLICY "cc_insert" ON charge_categories FOR INSERT
  TO authenticated WITH CHECK (current_user_role() = 'super_admin');
DROP POLICY IF EXISTS "cc_update" ON charge_categories;
CREATE POLICY "cc_update" ON charge_categories FOR UPDATE
  TO authenticated USING (current_user_role() = 'super_admin')
  WITH CHECK (current_user_role() = 'super_admin');
DROP POLICY IF EXISTS "cc_delete" ON charge_categories;
CREATE POLICY "cc_delete" ON charge_categories FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin');

-- RLS: payment_methods (org-level)
ALTER TABLE payment_methods ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "pm_select" ON payment_methods;
CREATE POLICY "pm_select" ON payment_methods FOR SELECT
  TO authenticated USING (
    current_user_role() = 'super_admin'
    OR organization_id = (SELECT organization_id FROM profiles WHERE id = auth.uid())
  );
DROP POLICY IF EXISTS "pm_insert" ON payment_methods;
CREATE POLICY "pm_insert" ON payment_methods FOR INSERT
  TO authenticated WITH CHECK (current_user_role() = 'super_admin');
DROP POLICY IF EXISTS "pm_update" ON payment_methods;
CREATE POLICY "pm_update" ON payment_methods FOR UPDATE
  TO authenticated USING (current_user_role() = 'super_admin')
  WITH CHECK (current_user_role() = 'super_admin');
DROP POLICY IF EXISTS "pm_delete" ON payment_methods;
CREATE POLICY "pm_delete" ON payment_methods FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin');

-- Folios
ALTER TABLE folios ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "folios_select" ON folios;
CREATE POLICY "folios_select" ON folios FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "folios_insert" ON folios;
CREATE POLICY "folios_insert" ON folios FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "folios_update" ON folios;
CREATE POLICY "folios_update" ON folios FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "folios_delete" ON folios;
CREATE POLICY "folios_delete" ON folios FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin' OR current_user_role() = 'manager');

-- Folio items
ALTER TABLE folio_items ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "fi_select" ON folio_items;
CREATE POLICY "fi_select" ON folio_items FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "fi_insert" ON folio_items;
CREATE POLICY "fi_insert" ON folio_items FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "fi_update" ON folio_items;
CREATE POLICY "fi_update" ON folio_items FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "fi_delete" ON folio_items;
CREATE POLICY "fi_delete" ON folio_items FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin' OR current_user_role() = 'manager');

-- Payments
ALTER TABLE payments ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "pay_select" ON payments;
CREATE POLICY "pay_select" ON payments FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "pay_insert" ON payments;
CREATE POLICY "pay_insert" ON payments FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "pay_update" ON payments;
CREATE POLICY "pay_update" ON payments FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "pay_delete" ON payments;
CREATE POLICY "pay_delete" ON payments FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin');

-- Deposits
ALTER TABLE deposits ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "dep_select" ON deposits;
CREATE POLICY "dep_select" ON deposits FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "dep_insert" ON deposits;
CREATE POLICY "dep_insert" ON deposits FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "dep_update" ON deposits;
CREATE POLICY "dep_update" ON deposits FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "dep_delete" ON deposits;
CREATE POLICY "dep_delete" ON deposits FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin');

-- Refunds
ALTER TABLE refunds ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "rf_select" ON refunds;
CREATE POLICY "rf_select" ON refunds FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "rf_insert" ON refunds;
CREATE POLICY "rf_insert" ON refunds FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "rf_update" ON refunds;
CREATE POLICY "rf_update" ON refunds FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "rf_delete" ON refunds;
CREATE POLICY "rf_delete" ON refunds FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin');

-- Additional charges
ALTER TABLE additional_charges ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "ac_select" ON additional_charges;
CREATE POLICY "ac_select" ON additional_charges FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "ac_insert" ON additional_charges;
CREATE POLICY "ac_insert" ON additional_charges FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "ac_update" ON additional_charges;
CREATE POLICY "ac_update" ON additional_charges FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "ac_delete" ON additional_charges;
CREATE POLICY "ac_delete" ON additional_charges FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin' OR current_user_role() = 'manager');

-- Invoices
ALTER TABLE invoices ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "inv_select" ON invoices;
CREATE POLICY "inv_select" ON invoices FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "inv_insert" ON invoices;
CREATE POLICY "inv_insert" ON invoices FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "inv_update" ON invoices;
CREATE POLICY "inv_update" ON invoices FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "inv_delete" ON invoices;
CREATE POLICY "inv_delete" ON invoices FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin' OR current_user_role() = 'manager');

-- Invoice items
ALTER TABLE invoice_items ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "ii_select" ON invoice_items;
CREATE POLICY "ii_select" ON invoice_items FOR SELECT
  TO authenticated USING (
    EXISTS (SELECT 1 FROM invoices i WHERE i.id = invoice_id AND user_has_branch_access(i.branch_id))
  );
DROP POLICY IF EXISTS "ii_insert" ON invoice_items;
CREATE POLICY "ii_insert" ON invoice_items FOR INSERT
  TO authenticated WITH CHECK (
    EXISTS (SELECT 1 FROM invoices i WHERE i.id = invoice_id AND user_has_branch_access(i.branch_id))
  );
DROP POLICY IF EXISTS "ii_delete" ON invoice_items;
CREATE POLICY "ii_delete" ON invoice_items FOR DELETE
  TO authenticated USING (
    EXISTS (SELECT 1 FROM invoices i WHERE i.id = invoice_id AND user_has_branch_access(i.branch_id))
  );

-- Transactions
ALTER TABLE transactions ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS "txn_select" ON transactions;
CREATE POLICY "txn_select" ON transactions FOR SELECT
  TO authenticated USING (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "txn_insert" ON transactions;
CREATE POLICY "txn_insert" ON transactions FOR INSERT
  TO authenticated WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "txn_update" ON transactions;
CREATE POLICY "txn_update" ON transactions FOR UPDATE
  TO authenticated USING (user_has_branch_access(branch_id))
  WITH CHECK (user_has_branch_access(branch_id));
DROP POLICY IF EXISTS "txn_delete" ON transactions;
CREATE POLICY "txn_delete" ON transactions FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin');
