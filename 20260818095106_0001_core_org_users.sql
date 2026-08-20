/*
# Core: Organizations, Branches, Users, Roles, Permissions

1. New Tables
- `organizations` — top-level company entity (the hotel group).
- `branches` — hotel properties belonging to an organization.
- `profiles` — app users linked to auth.users, with role + language preference.
- `user_branch_access` — many-to-many: which branches a user can access.
- `roles` — lookup/enum of role names (super_admin, manager, receptionist).
2. Security
- RLS enabled on all tables.
- Helper functions: `current_user_role()`, `current_user_branch_ids()`, `user_has_branch_access(uuid)`.
- Policies: users can read their own profile; super_admin reads all; users read branches they have access to.
3. Notes
- `profiles.id` references `auth.users.id` (1:1).
- Role stored as text enum for simplicity; enforced in app + RLS.
- Language preference stored per user (en / id).
*/

-- Organizations
CREATE TABLE IF NOT EXISTS organizations (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  name text NOT NULL,
  legal_name text,
  address text,
  phone text,
  email text,
  tax_id text,
  currency text NOT NULL DEFAULT 'IDR',
  timezone text NOT NULL DEFAULT 'Asia/Jakarta',
  logo_url text,
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now()
);

-- Branches
CREATE TABLE IF NOT EXISTS branches (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  organization_id uuid NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
  name text NOT NULL,
  code text NOT NULL,
  address text,
  phone text,
  email text,
  tax_id text,
  timezone text NOT NULL DEFAULT 'Asia/Jakarta',
  standard_checkin_time time NOT NULL DEFAULT '14:00',
  standard_checkout_time time NOT NULL DEFAULT '12:00',
  business_day_cutoff time NOT NULL DEFAULT '04:30',
  is_active boolean NOT NULL DEFAULT true,
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_branches_organization ON branches(organization_id);

-- Profiles (app users)
CREATE TABLE IF NOT EXISTS profiles (
  id uuid PRIMARY KEY REFERENCES auth.users(id) ON DELETE CASCADE,
  organization_id uuid NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
  full_name text NOT NULL,
  email text NOT NULL,
  role text NOT NULL DEFAULT 'receptionist', -- super_admin | manager | receptionist
  phone text,
  is_active boolean NOT NULL DEFAULT true,
  language text NOT NULL DEFAULT 'en',
  created_at timestamptz NOT NULL DEFAULT now(),
  updated_at timestamptz NOT NULL DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_profiles_organization ON profiles(organization_id);
CREATE INDEX IF NOT EXISTS idx_profiles_role ON profiles(role);

-- User branch access
CREATE TABLE IF NOT EXISTS user_branch_access (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  user_id uuid NOT NULL REFERENCES profiles(id) ON DELETE CASCADE,
  branch_id uuid NOT NULL REFERENCES branches(id) ON DELETE CASCADE,
  created_at timestamptz NOT NULL DEFAULT now(),
  UNIQUE (user_id, branch_id)
);

CREATE INDEX IF NOT EXISTS idx_user_branch_access_user ON user_branch_access(user_id);
CREATE INDEX IF NOT EXISTS idx_user_branch_access_branch ON user_branch_access(branch_id);

-- Roles lookup (for reference; role enforced as text on profiles)
CREATE TABLE IF NOT EXISTS roles (
  name text PRIMARY KEY,
  description text,
  created_at timestamptz NOT NULL DEFAULT now()
);

INSERT INTO roles (name, description) VALUES
  ('super_admin', 'Owner / super administrator with full access'),
  ('manager', 'Branch manager with operational access'),
  ('receptionist', 'Front desk receptionist')
ON CONFLICT (name) DO NOTHING;

-- Helper: current user's role
CREATE OR REPLACE FUNCTION current_user_role()
RETURNS text
LANGUAGE sql
SECURITY DEFINER
STABLE
SET search_path = public
AS $$
  SELECT role FROM profiles WHERE id = auth.uid();
$$;

-- Helper: branch ids the current user can access
CREATE OR REPLACE FUNCTION current_user_branch_ids()
RETURNS uuid[]
LANGUAGE sql
SECURITY DEFINER
STABLE
SET search_path = public
AS $$
  SELECT COALESCE(array_agg(branch_id), ARRAY[]::uuid[])
  FROM user_branch_access
  WHERE user_id = auth.uid();
$$;

-- Helper: does current user have access to a branch
CREATE OR REPLACE FUNCTION user_has_branch_access(p_branch_id uuid)
RETURNS boolean
LANGUAGE sql
SECURITY DEFINER
STABLE
SET search_path = public
AS $$
  SELECT
    EXISTS (SELECT 1 FROM profiles WHERE id = auth.uid() AND role = 'super_admin')
    OR
    EXISTS (SELECT 1 FROM user_branch_access WHERE user_id = auth.uid() AND branch_id = p_branch_id);
$$;

-- Enable RLS
ALTER TABLE organizations ENABLE ROW LEVEL SECURITY;
ALTER TABLE branches ENABLE ROW LEVEL SECURITY;
ALTER TABLE profiles ENABLE ROW LEVEL SECURITY;
ALTER TABLE user_branch_access ENABLE ROW LEVEL SECURITY;
ALTER TABLE roles ENABLE ROW LEVEL SECURITY;

-- Organizations: super_admin full; others read their own org
DROP POLICY IF EXISTS "org_select" ON organizations;
CREATE POLICY "org_select" ON organizations FOR SELECT
  TO authenticated USING (
    current_user_role() = 'super_admin'
    OR id = (SELECT organization_id FROM profiles WHERE id = auth.uid())
  );

DROP POLICY IF EXISTS "org_write" ON organizations;
CREATE POLICY "org_write" ON organizations FOR ALL
  TO authenticated USING (current_user_role() = 'super_admin')
  WITH CHECK (current_user_role() = 'super_admin');

-- Branches: super_admin full; users read branches they can access
DROP POLICY IF EXISTS "branch_select" ON branches;
CREATE POLICY "branch_select" ON branches FOR SELECT
  TO authenticated USING (
    current_user_role() = 'super_admin'
    OR id = ANY(current_user_branch_ids())
  );

DROP POLICY IF EXISTS "branch_insert" ON branches;
CREATE POLICY "branch_insert" ON branches FOR INSERT
  TO authenticated WITH CHECK (current_user_role() = 'super_admin');

DROP POLICY IF EXISTS "branch_update" ON branches;
CREATE POLICY "branch_update" ON branches FOR UPDATE
  TO authenticated USING (current_user_role() = 'super_admin')
  WITH CHECK (current_user_role() = 'super_admin');

DROP POLICY IF EXISTS "branch_delete" ON branches;
CREATE POLICY "branch_delete" ON branches FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin');

-- Profiles: super_admin full; users read/update own profile
DROP POLICY IF EXISTS "profile_select" ON profiles;
CREATE POLICY "profile_select" ON profiles FOR SELECT
  TO authenticated USING (
    current_user_role() = 'super_admin'
    OR id = auth.uid()
  );

DROP POLICY IF EXISTS "profile_insert" ON profiles;
CREATE POLICY "profile_insert" ON profiles FOR INSERT
  TO authenticated WITH CHECK (current_user_role() = 'super_admin');

DROP POLICY IF EXISTS "profile_update" ON profiles;
CREATE POLICY "profile_update" ON profiles FOR UPDATE
  TO authenticated USING (
    current_user_role() = 'super_admin'
    OR id = auth.uid()
  )
  WITH CHECK (
    current_user_role() = 'super_admin'
    OR id = auth.uid()
  );

DROP POLICY IF EXISTS "profile_delete" ON profiles;
CREATE POLICY "profile_delete" ON profiles FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin');

-- user_branch_access: super_admin full; users read their own
DROP POLICY IF EXISTS "uba_select" ON user_branch_access;
CREATE POLICY "uba_select" ON user_branch_access FOR SELECT
  TO authenticated USING (
    current_user_role() = 'super_admin'
    OR user_id = auth.uid()
  );

DROP POLICY IF EXISTS "uba_insert" ON user_branch_access;
CREATE POLICY "uba_insert" ON user_branch_access FOR INSERT
  TO authenticated WITH CHECK (current_user_role() = 'super_admin');

DROP POLICY IF EXISTS "uba_update" ON user_branch_access;
CREATE POLICY "uba_update" ON user_branch_access FOR UPDATE
  TO authenticated USING (current_user_role() = 'super_admin')
  WITH CHECK (current_user_role() = 'super_admin');

DROP POLICY IF EXISTS "uba_delete" ON user_branch_access;
CREATE POLICY "uba_delete" ON user_branch_access FOR DELETE
  TO authenticated USING (current_user_role() = 'super_admin');

-- roles: readable by all authenticated
DROP POLICY IF EXISTS "roles_select" ON roles;
CREATE POLICY "roles_select" ON roles FOR SELECT
  TO authenticated USING (true);
