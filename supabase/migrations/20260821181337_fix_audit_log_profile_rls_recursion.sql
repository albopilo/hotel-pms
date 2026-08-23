/*
# Fix Audit Log Profile RLS Recursion

1. Problem
- The `profile_select_org_members` policy (from a prior migration) queries `profiles`
  inside a SELECT policy ON `profiles`, causing infinite RLS recursion → 500 errors.
- This breaks the Audit Logs page (and any cross-user profile lookup).

2. Fix
- Create a SECURITY DEFINER function `current_user_organization_id()` that returns
  the caller's organization_id without triggering RLS recursion.
- Replace the recursive subquery in the policy with this function.
*/

CREATE OR REPLACE FUNCTION current_user_organization_id()
RETURNS uuid
LANGUAGE sql
SECURITY DEFINER
STABLE
SET search_path = public
AS $$
  SELECT organization_id FROM profiles WHERE id = auth.uid();
$$;

DROP POLICY IF EXISTS "profile_select_org_members" ON profiles;
CREATE POLICY "profile_select_org_members" ON profiles FOR SELECT
  TO authenticated USING (
    organization_id = current_user_organization_id()
  );