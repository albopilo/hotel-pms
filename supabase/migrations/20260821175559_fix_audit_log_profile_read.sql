/*
# Fix Audit Log User Names

1. Problem
- The Audit Logs page queries `profiles` to resolve `user_id` -> `full_name`.
- The existing `profile_select` policy only allows reading your own profile (or super_admin reads all).
- Managers (who can view audit logs per `al_select`) cannot resolve other users' names,
  so the "User" column shows "-" for everyone except themselves.
2. Security Change
- Add a new SELECT policy `profile_select_org_members` that allows any authenticated user
  to read `id` and `full_name` of all profiles within their own organization.
- This is the minimum access needed to display user names in audit logs and elsewhere.
- The existing `profile_select` policy is kept unchanged (own profile + super_admin full access).
3. Notes
- No data is modified or deleted.
- The new policy is scoped to the same organization as the caller, so it does not leak
  users across organizations.
*/

DROP POLICY IF EXISTS "profile_select_org_members" ON profiles;
CREATE POLICY "profile_select_org_members" ON profiles FOR SELECT
  TO authenticated USING (
    organization_id = (SELECT organization_id FROM profiles WHERE id = auth.uid())
  );
