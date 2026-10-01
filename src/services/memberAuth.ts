import { supabase } from '@/lib/supabase';
import type { M13Member, Guest } from '@/types/database';

export const memberAuth = {
  async memberSignIn(email: string, password: string): Promise<{ error: string | null }> {
    const { error } = await supabase.auth.signInWithPassword({ email, password });
    if (error) return { error: error.message };
    return { error: null };
  },

  async memberSignOut(): Promise<void> {
    await supabase.auth.signOut();
  },

  async memberChangePassword(currentPassword: string, newPassword: string): Promise<{ error: string | null }> {
    const { error: verifyError } = await supabase.auth.signInWithPassword({
      email: (await supabase.auth.getUser()).data.user?.email || '',
      password: currentPassword,
    });
    if (verifyError) return { error: 'Current password is incorrect' };

    const { error } = await supabase.auth.updateUser({ password: newPassword });
    if (error) return { error: error.message };
    return { error: null };
  },

  async memberForgotPassword(email: string): Promise<{ error: string | null }> {
    const { error } = await supabase.auth.resetPasswordForEmail(email);
    if (error) return { error: error.message };
    return { error: null };
  },

  async getCurrentMember(): Promise<{ member: M13Member | null; guest: Guest | null }> {
    const { data: { user } } = await supabase.auth.getUser();
    if (!user) return { member: null, guest: null };

    const { data: member, error } = await supabase
      .from('m13_members')
      .select('*')
      .eq('auth_user_id', user.id)
      .maybeSingle();
    if (error || !member) return { member: null, guest: null };

    const m = member as M13Member;
    if (!m.pms_guest_id) return { member: m, guest: null };

    const { data: guest } = await supabase
      .from('guests')
      .select('*')
      .eq('id', m.pms_guest_id)
      .maybeSingle();
    return { member: m, guest: (guest as Guest) || null };
  },
};
