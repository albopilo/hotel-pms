/*
# Room Type Weekday/Weekend Rates + Indonesian National Holidays

## Purpose
Adds weekday and weekend rate columns to room_types, and creates a new
indonesian_holidays table so the PMS can charge different rates based on
the day of the week and national holidays (including H-1 / day-before-holiday).

## 1. Modified Tables
### room_types
- `weekday_rate` numeric(14,2) NOT NULL DEFAULT 0
  Rate applied for non-holiday Monday–Thursday nights.
- `weekend_rate` numeric(14,2) NOT NULL DEFAULT 0
  Rate applied for non-holiday Friday–Sunday nights, and for every
  Indonesian national holiday and the day before a holiday (H-1).

The existing `base_rate` column is retained as the fallback / default rate
when weekday_rate or weekend_rate is 0 or not set.

## 2. New Tables
### indonesian_holidays
- `id` uuid primary key
- `organization_id` uuid NOT NULL references organizations(id) ON DELETE CASCADE
- `holiday_date` date NOT NULL — the holiday date
- `holiday_name` text NOT NULL — descriptive name (e.g. "Independence Day")
- `is_active` boolean NOT NULL DEFAULT true — allows soft-deactivating
- `created_by` uuid references profiles(id) ON DELETE SET NULL
- `created_at` timestamptz DEFAULT now()
- UNIQUE (organization_id, holiday_date)

## 3. Security (RLS)
- Enable RLS on indonesian_holidays.
- SELECT: any authenticated user in the same organization can read holidays.
- INSERT/UPDATE/DELETE: super_admin and manager roles only.

## 4. Important Notes
- The rate selection logic (which rate applies on which night) is implemented
  in the frontend application code, not in the database.
- H-1 (the day before a holiday) is also treated as a weekend-rate day.
- Holidays are organization-scoped so multi-tenant data stays isolated.
*/

-- Add weekday_rate and weekend_rate columns to room_types
DO $$
BEGIN
  IF NOT EXISTS (
    SELECT 1 FROM information_schema.columns
    WHERE table_name = 'room_types' AND column_name = 'weekday_rate'
  ) THEN
    ALTER TABLE room_types ADD COLUMN weekday_rate numeric(14,2) NOT NULL DEFAULT 0;
  END IF;

  IF NOT EXISTS (
    SELECT 1 FROM information_schema.columns
    WHERE table_name = 'room_types' AND column_name = 'weekend_rate'
  ) THEN
    ALTER TABLE room_types ADD COLUMN weekend_rate numeric(14,2) NOT NULL DEFAULT 0;
  END IF;
END $$;

-- Create indonesian_holidays table
CREATE TABLE IF NOT EXISTS indonesian_holidays (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  organization_id uuid NOT NULL REFERENCES organizations(id) ON DELETE CASCADE,
  holiday_date date NOT NULL,
  holiday_name text NOT NULL,
  is_active boolean NOT NULL DEFAULT true,
  created_by uuid REFERENCES profiles(id) ON DELETE SET NULL,
  created_at timestamptz NOT NULL DEFAULT now(),
  UNIQUE (organization_id, holiday_date)
);

CREATE INDEX IF NOT EXISTS idx_indonesian_holidays_org ON indonesian_holidays(organization_id);
CREATE INDEX IF NOT EXISTS idx_indonesian_holidays_date ON indonesian_holidays(holiday_date);

-- RLS for indonesian_holidays
ALTER TABLE indonesian_holidays ENABLE ROW LEVEL SECURITY;

DROP POLICY IF EXISTS "ih_select" ON indonesian_holidays;
CREATE POLICY "ih_select" ON indonesian_holidays FOR SELECT
  TO authenticated USING (
    current_user_role() = 'super_admin'
    OR organization_id = (SELECT organization_id FROM profiles WHERE id = auth.uid())
  );

DROP POLICY IF EXISTS "ih_insert" ON indonesian_holidays;
CREATE POLICY "ih_insert" ON indonesian_holidays FOR INSERT
  TO authenticated WITH CHECK (
    current_user_role() IN ('super_admin', 'manager')
    AND organization_id = (SELECT organization_id FROM profiles WHERE id = auth.uid())
  );

DROP POLICY IF EXISTS "ih_update" ON indonesian_holidays;
CREATE POLICY "ih_update" ON indonesian_holidays FOR UPDATE
  TO authenticated
  USING (
    current_user_role() IN ('super_admin', 'manager')
    AND organization_id = (SELECT organization_id FROM profiles WHERE id = auth.uid())
  )
  WITH CHECK (
    current_user_role() IN ('super_admin', 'manager')
    AND organization_id = (SELECT organization_id FROM profiles WHERE id = auth.uid())
  );

DROP POLICY IF EXISTS "ih_delete" ON indonesian_holidays;
CREATE POLICY "ih_delete" ON indonesian_holidays FOR DELETE
  TO authenticated USING (
    current_user_role() IN ('super_admin', 'manager')
    AND organization_id = (SELECT organization_id FROM profiles WHERE id = auth.uid())
  );
