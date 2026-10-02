import { supabase } from '@/lib/supabase';
import type {
  M13Member,
  M13MemberWithGuest,
  M13LoyaltySettings,
  M13PointLedger,
  M13Reward,
  M13RewardRedemption,
  M13RewardRedemptionWithReward,
  Guest,
} from '@/types/database';

export const loyaltyService = {
  async getEligibleBranchIds(): Promise<Set<string>> {
    const { data, error } = await supabase
      .from('m13_eligible_branches')
      .select('branch_id')
      .eq('is_active', true);
    if (error || !data) return new Set();
    return new Set(data.map((r: { branch_id: string }) => r.branch_id));
  },

  async isBranchEligible(branchId: string): Promise<boolean> {
    const { data, error } = await supabase.rpc('m13_is_branch_eligible', { p_branch_id: branchId });
    if (error) return false;
    return (data as boolean) || false;
  },

  async getEligibleBranches(): Promise<Array<{ branch_id: string; is_active: boolean }>> {
    const { data, error } = await supabase
      .from('m13_eligible_branches')
      .select('branch_id, is_active');
    if (error || !data) return [];
    return data as Array<{ branch_id: string; is_active: boolean }>;
  },

  async setBranchEligibility(orgId: string, branchId: string, eligible: boolean): Promise<{ error: string | null }> {
    if (eligible) {
      const { error } = await supabase
        .from('m13_eligible_branches')
        .upsert({
          organization_id: orgId,
          branch_id: branchId,
          is_active: true,
          updated_at: new Date().toISOString(),
        }, { onConflict: 'organization_id,branch_id' });
      if (error) return { error: error.message };
    } else {
      const { error } = await supabase
        .from('m13_eligible_branches')
        .update({ is_active: false, updated_at: new Date().toISOString() })
        .eq('organization_id', orgId)
        .eq('branch_id', branchId);
      if (error) return { error: error.message };
    }
    return { error: null };
  },

  async getMemberByGuestId(guestId: string): Promise<M13Member | null> {
    const { data, error } = await supabase
      .from('m13_members')
      .select('*')
      .eq('pms_guest_id', guestId)
      .maybeSingle();
    if (error) return null;
    return data as M13Member | null;
  },

  async getMemberById(memberId: string): Promise<M13Member | null> {
    const { data, error } = await supabase
      .from('m13_members')
      .select('*')
      .eq('id', memberId)
      .maybeSingle();
    if (error) return null;
    return data as M13Member | null;
  },

  async createMember(guestId: string, email: string, staffUserId: string): Promise<{ member: M13Member | null; error: string | null }> {
    const { data: sessionData } = await supabase.auth.getSession();
    const accessToken = sessionData?.session?.access_token;
    if (!accessToken) return { member: null, error: 'Not authenticated' };

    const { data, error } = await supabase.functions.invoke('m13-create-member', {
      body: { guest_id: guestId, email, staff_user_id: staffUserId },
      headers: { Authorization: `Bearer ${accessToken}` },
    });
    if (error) return { member: null, error: error.message };
    if (data?.error) return { member: null, error: data.error };
    return { member: (data?.member as M13Member) || null, error: null };
  },

  async earnPointsOnCheckout(reservationId: string, staffUserId: string): Promise<{ points: number; error: string | null }> {
    const { data, error } = await supabase.rpc('m13_earn_points_on_checkout', {
      p_reservation_id: reservationId,
      p_staff_user_id: staffUserId,
    });
    if (error) return { points: 0, error: error.message };
    return { points: (data as number) || 0, error: null };
  },

  async adjustPoints(memberId: string, points: number, reason: string, staffUserId: string): Promise<{ error: string | null }> {
    const { error } = await supabase.rpc('m13_adjust_points', {
      p_member_id: memberId,
      p_points: points,
      p_reason: reason,
      p_staff_user_id: staffUserId,
    });
    if (error) return { error: error.message };
    return { error: null };
  },

  async getMemberPointHistory(memberId: string): Promise<M13PointLedger[]> {
    const { data, error } = await supabase
      .from('m13_point_ledger')
      .select('*')
      .eq('member_id', memberId)
      .order('created_at', { ascending: false });
    if (error) return [];
    return (data as M13PointLedger[]) || [];
  },

  async getMemberRedemptions(memberId: string): Promise<M13RewardRedemptionWithReward[]> {
    await supabase.rpc('m13_expire_redemptions');
    const { data, error } = await supabase
      .from('m13_reward_redemptions')
      .select('*, reward:m13_rewards(*)')
      .eq('member_id', memberId)
      .order('created_at', { ascending: false });
    if (error) return [];
    return (data as M13RewardRedemptionWithReward[]) || [];
  },

  async getAvailableRewards(): Promise<M13Reward[]> {
    const { data, error } = await supabase
      .from('m13_rewards')
      .select('*')
      .eq('is_active', true)
      .order('points_required', { ascending: true });
    if (error) return [];
    const today = new Date().toISOString().slice(0, 10);
    return ((data as M13Reward[]) || []).filter(
      (r) => (!r.redemption_deadline || r.redemption_deadline >= today) && r.total_redeemed < r.total_redemption_limit,
    );
  },

  async getAllRewards(orgId: string): Promise<M13Reward[]> {
    const { data, error } = await supabase
      .from('m13_rewards')
      .select('*')
      .eq('organization_id', orgId)
      .order('created_at', { ascending: false });
    if (error) return [];
    return (data as M13Reward[]) || [];
  },

  async createReward(reward: Partial<M13Reward>): Promise<{ error: string | null }> {
    const { error } = await supabase.from('m13_rewards').insert(reward);
    if (error) return { error: error.message };
    return { error: null };
  },

  async updateReward(id: string, updates: Partial<M13Reward>): Promise<{ error: string | null }> {
    const { error } = await supabase.from('m13_rewards').update({ ...updates, updated_at: new Date().toISOString() }).eq('id', id);
    if (error) return { error: error.message };
    return { error: null };
  },

  async deleteReward(id: string): Promise<{ error: string | null }> {
    const { error } = await supabase.from('m13_rewards').delete().eq('id', id);
    if (error) return { error: error.message };
    return { error: null };
  },

  async getLoyaltySettings(orgId: string): Promise<M13LoyaltySettings[]> {
    const { data, error } = await supabase
      .from('m13_loyalty_settings')
      .select('*')
      .eq('organization_id', orgId)
      .order('charge_category_code');
    if (error) return [];
    return (data as M13LoyaltySettings[]) || [];
  },

  async upsertLoyaltySetting(setting: Partial<M13LoyaltySettings>): Promise<{ error: string | null }> {
    const { error } = await supabase
      .from('m13_loyalty_settings')
      .upsert({
        organization_id: setting.organization_id,
        charge_category_code: setting.charge_category_code,
        spending_threshold: setting.spending_threshold,
        points_awarded: setting.points_awarded,
        is_active: setting.is_active,
        updated_at: new Date().toISOString(),
      }, { onConflict: 'organization_id,charge_category_code' });
    if (error) return { error: error.message };
    return { error: null };
  },

  async getAllMembers(orgId: string, searchQuery?: string): Promise<M13MemberWithGuest[]> {
    let query = supabase
      .from('m13_members')
      .select('*, guest:guests(*)')
      .eq('organization_id', orgId)
      .order('created_at', { ascending: false });
    if (searchQuery) {
      query = query.or(`member_number.ilike.%${searchQuery}%`);
    }
    const { data, error } = await query;
    if (error) return [];
    const members = (data as M13MemberWithGuest[]) || [];
    if (!searchQuery) return members;
    const q = searchQuery.toLowerCase();
    return members.filter(
      (m) =>
        m.member_number.toLowerCase().includes(q) ||
        (m.guest?.full_name || '').toLowerCase().includes(q) ||
        (m.guest?.phone || '').includes(q) ||
        (m.guest?.email || '').toLowerCase().includes(q),
    );
  },

  async getLoyaltyActivity(orgId: string): Promise<Array<M13PointLedger & { member: M13Member | null; staff_name: string | null }>> {
    const { data: ledger } = await supabase
      .from('m13_point_ledger')
      .select(`
        *,
        member:m13_members!inner(*)
      `)
      .eq('member.organization_id', orgId)
      .order('created_at', { ascending: false })
      .limit(100);
    if (!ledger) return [];
    const items = ledger as unknown as Array<M13PointLedger & { member: M13Member }>;
    const staffIds = [...new Set(items.map((i) => i.performed_by).filter(Boolean))] as string[];
    let staffMap: Record<string, string> = {};
    if (staffIds.length > 0) {
      const { data: profiles } = await supabase
        .from('profiles')
        .select('id, full_name')
        .in('id', staffIds);
      staffMap = (profiles || []).reduce((acc: Record<string, string>, p: { id: string; full_name: string }) => {
        acc[p.id] = p.full_name;
        return acc;
      }, {});
    }
    return items.map((i) => ({
      ...i,
      staff_name: i.performed_by ? staffMap[i.performed_by] || null : null,
    }));
  },
};
