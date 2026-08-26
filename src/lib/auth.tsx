import { createContext, useContext, useEffect, useState, useCallback, type ReactNode } from 'react';
import { supabase } from '@/lib/supabase';
import type { Profile, Branch, UserRole } from '@/types/database';
import { useI18n, type Language } from '@/lib/i18n';

interface AuthContextValue {
  user: Profile | null;
  branches: Branch[];
  loading: boolean;
  signIn: (email: string, password: string) => Promise<{ error: string | null }>;
  signOut: () => Promise<void>;
  refreshUser: () => Promise<void>;
}

const AuthContext = createContext<AuthContextValue | null>(null);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<Profile | null>(null);
  const [branches, setBranches] = useState<Branch[]>([]);
  const [loading, setLoading] = useState(true);
  const { setLanguage } = useI18n();

  const loadProfile = useCallback(async (userId: string) => {
    const { data, error } = await supabase
      .from('profiles')
      .select('*')
      .eq('id', userId)
      .maybeSingle();
    if (error) return null;
    return data as Profile | null;
  }, []);

  const loadBranches = useCallback(async (userId: string, role: UserRole) => {
    if (role === 'super_admin') {
      const { data } = await supabase.from('branches').select('*').eq('is_active', true).order('name');
      return (data as Branch[]) || [];
    }
    const { data } = await supabase
      .from('user_branch_access')
      .select('branch:branches(*)')
      .eq('user_id', userId)
      .eq('branch.is_active', true);
    if (!data) return [];
    return data.map((item) => item.branch as unknown as Branch);
  }, []);

  const refreshUser = useCallback(async () => {
    const { data: { session } } = await supabase.auth.getSession();
    if (!session) {
      setUser(null);
      setBranches([]);
      return;
    }
    const profile = await loadProfile(session.user.id);
    if (profile) {
      setUser(profile);
      setLanguage(profile.language as Language);
      const branchList = await loadBranches(profile.id, profile.role);
      setBranches(branchList);
    } else {
      setUser(null);
      setBranches([]);
    }
  }, [loadProfile, loadBranches, setLanguage]);

  useEffect(() => {
    let currentUserId: string | null = null;

    (async () => {
      setLoading(true);
      await refreshUser();
      const { data: { session } } = await supabase.auth.getSession();
      currentUserId = session?.user?.id ?? null;
      setLoading(false);
    })();

    const { data: sub } = supabase.auth.onAuthStateChange((_event, session) => {
      const sessionUserId = session?.user?.id ?? null;

      // Skip re-fetching profile/branches when the user hasn't changed
      // (e.g. TOKEN_REFRESHED fired by autoRefreshToken on tab refocus)
      if (sessionUserId === currentUserId) return;
      currentUserId = sessionUserId;

      (async () => {
        if (!session) {
          setUser(null);
          setBranches([]);
          return;
        }
        const profile = await loadProfile(session.user.id);
        if (profile) {
          setUser(profile);
          setLanguage(profile.language as Language);
          const branchList = await loadBranches(profile.id, profile.role);
          setBranches(branchList);
        }
      })();
    });

    return () => sub.subscription.unsubscribe();
  }, [refreshUser, loadProfile, loadBranches, setLanguage]);

  const signIn = useCallback(async (email: string, password: string) => {
    const { error } = await supabase.auth.signInWithPassword({ email, password });
    if (error) return { error: error.message };
    return { error: null };
  }, []);

  const signOut = useCallback(async () => {
    await supabase.auth.signOut();
    setUser(null);
    setBranches([]);
  }, []);

  return (
    <AuthContext.Provider value={{ user, branches, loading, signIn, signOut, refreshUser }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error('useAuth must be used within AuthProvider');
  return ctx;
}
