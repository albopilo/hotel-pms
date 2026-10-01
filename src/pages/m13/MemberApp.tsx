import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
import { useI18n } from '@/lib/i18n';
import { memberAuth } from '@/services/memberAuth';
import { loyaltyService } from '@/services/loyaltyService';
import type { M13Member, Guest, M13Reward, M13RewardRedemptionWithReward, M13PointLedger } from '@/types/database';
import { formatDate, formatDateTime } from '@/lib/format';
import {
  Award, Gift, History, User as UserIcon, LogOut, Eye, EyeOff, KeyRound,
  Star, Ticket, CheckCircle2, AlertCircle, Loader2, Hotel, ChevronLeft,
} from 'lucide-react';

type MemberPage = 'dashboard' | 'rewards' | 'my_rewards' | 'history' | 'profile';

export function MemberApp() {
  const { t, language, setLanguage } = useI18n();
  const [session, setSession] = useState<boolean | null>(null);
  const [member, setMember] = useState<M13Member | null>(null);
  const [guest, setGuest] = useState<Guest | null>(null);
  const [loading, setLoading] = useState(true);
  const [page, setPage] = useState<MemberPage>('dashboard');

  const loadMember = useCallback(async () => {
    const { member: m, guest: g } = await memberAuth.getCurrentMember();
    if (m) {
      setMember(m);
      setGuest(g);
    } else {
      setMember(null);
      setGuest(null);
    }
  }, []);

  useEffect(() => {
    (async () => {
      setLoading(true);
      const { data: { session: s } } = await supabase.auth.getSession();
      setSession(!!s);
      if (s) {
        await loadMember();
      }
      setLoading(false);
    })();

    const { data: sub } = supabase.auth.onAuthStateChange((_event, s) => {
      (async () => {
        setSession(!!s);
        if (s) {
          await loadMember();
        } else {
          setMember(null);
          setGuest(null);
        }
        setPage('dashboard');
      })();
    });
    return () => sub.subscription.unsubscribe();
  }, [loadMember]);

  if (loading) {
    return (
      <div className="min-h-screen bg-gradient-to-br from-slate-900 via-slate-800 to-slate-900 flex items-center justify-center">
        <Loader2 size={32} className="animate-spin text-amber-400" />
      </div>
    );
  }

  if (!session) {
    return <MemberLogin />;
  }

  if (!member) {
    return (
      <div className="min-h-screen bg-gradient-to-br from-slate-900 via-slate-800 to-slate-900 flex items-center justify-center p-4">
        <div className="text-center max-w-md">
          <AlertCircle size={48} className="text-amber-400 mx-auto mb-4" />
          <p className="text-white text-lg font-medium mb-2">{t('m13.not_member_account')}</p>
          <button
            onClick={async () => { await memberAuth.memberSignOut(); }}
            className="mt-4 text-amber-400 hover:text-amber-300 text-sm font-medium"
          >
            {t('m13.back_to_login')}
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-900 via-slate-800 to-slate-900">
      <MemberHeader
        member={member}
        guest={guest}
        page={page}
        onNavigate={setPage}
        onSignOut={async () => { await memberAuth.memberSignOut(); }}
        language={language}
        onLanguageChange={setLanguage}
      />
      <main className="max-w-2xl mx-auto px-4 py-6">
        {page === 'dashboard' && <MemberDashboard member={member} guest={guest} onNavigate={setPage} />}
        {page === 'rewards' && <MemberRewards member={member} />}
        {page === 'my_rewards' && <MemberMyRewards member={member} />}
        {page === 'history' && <MemberHistory member={member} />}
        {page === 'profile' && <MemberProfile member={member} guest={guest} />}
      </main>
    </div>
  );
}

function MemberHeader({
  member, guest, page, onNavigate, onSignOut, language, onLanguageChange,
}: {
  member: M13Member;
  guest: Guest | null;
  page: MemberPage;
  onNavigate: (p: MemberPage) => void;
  onSignOut: () => Promise<void>;
  language: string;
  onLanguageChange: (l: 'en' | 'id') => void;
}) {
  const { t } = useI18n();
  const navItems: { key: MemberPage; label: string; icon: typeof Award }[] = [
    { key: 'dashboard', label: t('m13.your_points'), icon: Star },
    { key: 'rewards', label: t('m13.available_rewards'), icon: Gift },
    { key: 'my_rewards', label: t('m13.my_rewards'), icon: Ticket },
    { key: 'history', label: t('m13.my_point_history'), icon: History },
    { key: 'profile', label: t('m13.my_profile'), icon: UserIcon },
  ];

  return (
    <header className="bg-slate-950/80 backdrop-blur-sm border-b border-slate-700 sticky top-0 z-40">
      <div className="max-w-2xl mx-auto px-4 py-3">
        <div className="flex items-center justify-between">
          <div className="flex items-center gap-2">
            <div className="w-9 h-9 rounded-lg bg-amber-500 flex items-center justify-center">
              <Hotel size={20} className="text-slate-900" />
            </div>
            <div>
              <p className="font-bold text-white text-sm">M13 Club</p>
              <p className="text-xs text-slate-400">{member.member_number}</p>
            </div>
          </div>
          <div className="flex items-center gap-3">
            <button
              onClick={() => onLanguageChange(language === 'en' ? 'id' : 'en')}
              className="text-xs text-slate-400 hover:text-white uppercase"
            >
              {language}
            </button>
            <button onClick={onSignOut} className="text-slate-400 hover:text-red-400">
              <LogOut size={18} />
            </button>
          </div>
        </div>
        <nav className="flex gap-1 mt-3 overflow-x-auto">
          {navItems.map((item) => {
            const Icon = item.icon;
            return (
              <button
                key={item.key}
                onClick={() => onNavigate(item.key)}
                className={`flex items-center gap-1.5 px-3 py-1.5 rounded-lg text-xs font-medium whitespace-nowrap transition-colors ${
                  page === item.key
                    ? 'bg-amber-500 text-slate-900'
                    : 'text-slate-400 hover:bg-slate-800 hover:text-white'
                }`}
              >
                <Icon size={14} />
                {item.label}
              </button>
            );
          })}
        </nav>
      </div>
    </header>
  );
}

function MemberLogin() {
  const { t } = useI18n();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const [showForgot, setShowForgot] = useState(false);
  const [forgotEmail, setForgotEmail] = useState('');
  const [forgotSent, setForgotSent] = useState(false);

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError('');
    const { error } = await memberAuth.memberSignIn(email, password);
    if (error) {
      setError(t('m13.login_error'));
      setLoading(false);
    }
  };

  const handleForgot = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    const { error } = await memberAuth.memberForgotPassword(forgotEmail);
    if (error) {
      setError(t('m13.reset_failed'));
    } else {
      setForgotSent(true);
      setError('');
    }
    setLoading(false);
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-900 via-slate-800 to-slate-900 flex items-center justify-center p-4">
      <div className="w-full max-w-sm">
        <div className="text-center mb-8">
          <div className="w-16 h-16 rounded-2xl bg-amber-500 flex items-center justify-center mx-auto mb-4">
            <Hotel size={32} className="text-slate-900" />
          </div>
          <h1 className="text-2xl font-bold text-white">{t('m13.welcome')}</h1>
          <p className="text-slate-400 text-sm mt-1">{t('m13.login_subtitle')}</p>
        </div>

        {!showForgot ? (
          <form onSubmit={handleLogin} className="space-y-4">
            <div>
              <label className="text-sm font-medium text-slate-300">{t('auth.email')}</label>
              <input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
                className="mt-1 w-full rounded-lg border border-slate-600 bg-slate-800 px-3 py-2.5 text-sm text-white outline-none focus:ring-2 focus:ring-amber-500"
              />
            </div>
            <div>
              <label className="text-sm font-medium text-slate-300">{t('auth.password')}</label>
              <input
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
                className="mt-1 w-full rounded-lg border border-slate-600 bg-slate-800 px-3 py-2.5 text-sm text-white outline-none focus:ring-2 focus:ring-amber-500"
              />
            </div>
            {error && <p className="text-red-400 text-sm">{error}</p>}
            <button
              type="submit"
              disabled={loading}
              className="w-full bg-amber-500 text-slate-900 font-semibold py-2.5 rounded-lg hover:bg-amber-400 transition-colors disabled:opacity-50 flex items-center justify-center gap-2"
            >
              {loading ? <Loader2 size={18} className="animate-spin" /> : <KeyRound size={18} />}
              {t('m13.sign_in')}
            </button>
            <button
              type="button"
              onClick={() => { setShowForgot(true); setError(''); }}
              className="w-full text-center text-sm text-amber-400 hover:text-amber-300"
            >
              {t('m13.forgot_password')}
            </button>
          </form>
        ) : !forgotSent ? (
          <form onSubmit={handleForgot} className="space-y-4">
            <p className="text-slate-300 text-sm">{t('m13.reset_instructions')}</p>
            <div>
              <label className="text-sm font-medium text-slate-300">{t('auth.email')}</label>
              <input
                type="email"
                value={forgotEmail}
                onChange={(e) => setForgotEmail(e.target.value)}
                required
                className="mt-1 w-full rounded-lg border border-slate-600 bg-slate-800 px-3 py-2.5 text-sm text-white outline-none focus:ring-2 focus:ring-amber-500"
              />
            </div>
            {error && <p className="text-red-400 text-sm">{error}</p>}
            <button
              type="submit"
              disabled={loading}
              className="w-full bg-amber-500 text-slate-900 font-semibold py-2.5 rounded-lg hover:bg-amber-400 transition-colors disabled:opacity-50"
            >
              {t('m13.reset_password')}
            </button>
            <button
              type="button"
              onClick={() => { setShowForgot(false); setForgotSent(false); setError(''); }}
              className="w-full text-center text-sm text-amber-400 hover:text-amber-300"
            >
              {t('m13.back_to_login')}
            </button>
          </form>
        ) : (
          <div className="text-center space-y-4">
            <CheckCircle2 size={48} className="text-emerald-400 mx-auto" />
            <p className="text-white">{t('m13.reset_sent')}</p>
            <button
              onClick={() => { setShowForgot(false); setForgotSent(false); }}
              className="text-amber-400 hover:text-amber-300 text-sm font-medium"
            >
              {t('m13.back_to_login')}
            </button>
          </div>
        )}
      </div>
    </div>
  );
}

function MemberDashboard({ member, guest, onNavigate }: { member: M13Member; guest: Guest | null; onNavigate: (p: MemberPage) => void }) {
  const { t } = useI18n();
  const firstName = (guest?.full_name || '').split(' ')[0] || t('m13.hello');

  const buttons: { key: MemberPage; label: string; icon: typeof Award; color: string }[] = [
    { key: 'rewards', label: t('m13.browse_rewards'), icon: Gift, color: 'bg-amber-500 text-slate-900' },
    { key: 'my_rewards', label: t('m13.my_rewards'), icon: Ticket, color: 'bg-emerald-500 text-white' },
    { key: 'history', label: t('m13.my_point_history'), icon: History, color: 'bg-blue-500 text-white' },
    { key: 'profile', label: t('m13.my_profile'), icon: UserIcon, color: 'bg-slate-700 text-white' },
  ];

  return (
    <div className="space-y-6">
      <div className="text-center">
        <p className="text-slate-400 text-sm">{t('m13.hello')},</p>
        <h2 className="text-2xl font-bold text-white">{firstName}!</h2>
      </div>

      <div className="bg-gradient-to-br from-amber-500 to-amber-600 rounded-2xl p-6 text-center shadow-lg">
        <p className="text-amber-900 text-sm font-medium uppercase tracking-wider">{t('m13.your_points')}</p>
        <p className="text-5xl font-bold text-slate-900 mt-2">{member.points_balance}</p>
        <p className="text-amber-900/70 text-xs mt-2">{t('m13.member_since')} {formatDate(member.created_at)}</p>
      </div>

      <div className="grid grid-cols-2 gap-3">
        {buttons.map((btn) => {
          const Icon = btn.icon;
          return (
            <button
              key={btn.key}
              onClick={() => onNavigate(btn.key)}
              className={`flex flex-col items-center gap-2 p-4 rounded-xl font-medium transition-transform hover:scale-105 ${btn.color}`}
            >
              <Icon size={28} />
              <span className="text-sm">{btn.label}</span>
            </button>
          );
        })}
      </div>
    </div>
  );
}

function MemberRewards({ member }: { member: M13Member }) {
  const { t } = useI18n();
  const [rewards, setRewards] = useState<M13Reward[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedReward, setSelectedReward] = useState<M13Reward | null>(null);
  const [redeeming, setRedeeming] = useState(false);
  const [redeemResult, setRedeemResult] = useState<{ code: string } | null>(null);
  const [error, setError] = useState('');

  const load = useCallback(async () => {
    setLoading(true);
    const data = await loyaltyService.getAvailableRewards();
    setRewards(data);
    setLoading(false);
  }, []);

  useEffect(() => { load(); }, [load]);

  const handleRedeem = async () => {
    if (!selectedReward) return;
    setRedeeming(true);
    setError('');
    const { data, error } = await supabase.rpc('m13_redeem_reward', { p_reward_id: selectedReward.id });
    if (error) {
      setError(error.message);
    } else if (data) {
      setRedeemResult({ code: (data as { redemption_code: string }).redemption_code });
      setSelectedReward(null);
      load();
    }
    setRedeeming(false);
  };

  if (loading) {
    return <div className="flex justify-center py-12"><Loader2 size={24} className="animate-spin text-amber-400" /></div>;
  }

  return (
    <div className="space-y-4">
      <h2 className="text-xl font-bold text-white">{t('m13.available_rewards')}</h2>

      {redeemResult && (
        <div className="bg-emerald-600 rounded-xl p-5 text-center">
          <CheckCircle2 size={32} className="text-white mx-auto mb-2" />
          <p className="text-white font-medium">{t('m13.redeem_success')}</p>
          <p className="text-emerald-100 text-sm mt-1">{t('m13.your_code')}:</p>
          <p className="text-2xl font-bold text-white tracking-wider mt-1">{redeemResult.code}</p>
          <button
            onClick={() => setRedeemResult(null)}
            className="mt-3 text-emerald-100 text-sm hover:text-white"
          >
            {t('common.close')}
          </button>
        </div>
      )}

      {rewards.length === 0 ? (
        <div className="text-center py-12">
          <Gift size={48} className="text-slate-600 mx-auto mb-3" />
          <p className="text-slate-400">{t('m13.no_rewards_available')}</p>
        </div>
      ) : (
        rewards.map((reward) => {
          const remaining = reward.total_redemption_limit - reward.total_redeemed;
          const canAfford = member.points_balance >= reward.points_required;
          return (
            <div
              key={reward.id}
              className="bg-slate-800 border border-slate-700 rounded-xl p-4 space-y-3"
            >
              <div className="flex items-start justify-between">
                <div className="flex-1">
                  <h3 className="font-semibold text-white">{reward.name}</h3>
                  {reward.description && <p className="text-slate-400 text-sm mt-1">{reward.description}</p>}
                </div>
                <div className="flex items-center gap-1 text-amber-400 font-bold text-lg ml-3">
                  <Star size={16} />
                  {reward.points_required}
                </div>
              </div>
              <div className="flex items-center gap-4 text-xs text-slate-400">
                {reward.redemption_deadline && (
                  <span>{t('m13.deadline')}: {formatDate(reward.redemption_deadline)}</span>
                )}
                <span>{t('m13.remaining')}: {remaining}</span>
              </div>
              <button
                onClick={() => { setSelectedReward(reward); setError(''); }}
                disabled={!canAfford}
                className={`w-full py-2 rounded-lg font-medium text-sm transition-colors ${
                  canAfford
                    ? 'bg-amber-500 text-slate-900 hover:bg-amber-400'
                    : 'bg-slate-700 text-slate-500 cursor-not-allowed'
                }`}
              >
                {canAfford ? t('m13.view_details') : `${reward.points_required} ${t('m13.points')}`}
              </button>
            </div>
          );
        })
      )}

      {selectedReward && (
        <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-black/60" onClick={() => setSelectedReward(null)}>
          <div className="bg-slate-800 rounded-xl p-6 max-w-sm w-full space-y-4" onClick={(e) => e.stopPropagation()}>
            <h3 className="text-lg font-bold text-white">{selectedReward.name}</h3>
            {selectedReward.description && <p className="text-slate-300 text-sm">{selectedReward.description}</p>}
            {selectedReward.terms_conditions && (
              <div className="bg-slate-900 rounded-lg p-3">
                <p className="text-xs font-semibold text-slate-400 uppercase mb-1">{t('m13.terms_conditions')}</p>
                <p className="text-slate-300 text-sm whitespace-pre-wrap">{selectedReward.terms_conditions}</p>
              </div>
            )}
            <div className="flex items-center justify-between text-sm">
              <span className="text-slate-400">{t('m13.points_required')}</span>
              <span className="text-amber-400 font-bold">{selectedReward.points_required} {t('m13.points')}</span>
            </div>
            {error && <p className="text-red-400 text-sm">{error}</p>}
            <div className="flex gap-2">
              <button
                onClick={() => setSelectedReward(null)}
                className="flex-1 py-2.5 rounded-lg bg-slate-700 text-white text-sm font-medium hover:bg-slate-600"
              >
                {t('common.cancel')}
              </button>
              <button
                onClick={handleRedeem}
                disabled={redeeming}
                className="flex-1 py-2.5 rounded-lg bg-amber-500 text-slate-900 text-sm font-semibold hover:bg-amber-400 disabled:opacity-50"
              >
                {redeeming ? <Loader2 size={16} className="animate-spin mx-auto" /> : `${t('m13.redeem_btn')} ${selectedReward.points_required}`}
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

function MemberMyRewards({ member }: { member: M13Member }) {
  const { t } = useI18n();
  const [redemptions, setRedemptions] = useState<M13RewardRedemptionWithReward[]>([]);
  const [loading, setLoading] = useState(true);
  const [showCode, setShowCode] = useState<string | null>(null);
  const [useTarget, setUseTarget] = useState<M13RewardRedemptionWithReward | null>(null);
  const [using, setUsing] = useState(false);
  const [error, setError] = useState('');

  const load = useCallback(async () => {
    setLoading(true);
    const data = await loyaltyService.getMemberRedemptions(member.id);
    setRedemptions(data);
    setLoading(false);
  }, [member.id]);

  useEffect(() => { load(); }, [load]);

  const handleUse = async () => {
    if (!useTarget) return;
    setUsing(true);
    setError('');
    const { error } = await supabase.rpc('m13_use_reward', { p_redemption_id: useTarget.id });
    if (error) {
      setError(error.message);
    } else {
      setUseTarget(null);
      load();
    }
    setUsing(false);
  };

  if (loading) {
    return <div className="flex justify-center py-12"><Loader2 size={24} className="animate-spin text-amber-400" /></div>;
  }

  const unused = redemptions.filter((r) => r.status === 'UNUSED');
  const used = redemptions.filter((r) => r.status === 'USED');

  return (
    <div className="space-y-6">
      <h2 className="text-xl font-bold text-white">{t('m13.my_rewards')}</h2>

      <div className="space-y-3">
        <h3 className="text-sm font-semibold text-slate-400 uppercase">{t('m13.unused_rewards')} ({unused.length})</h3>
        {unused.length === 0 ? (
          <p className="text-slate-500 text-sm">{t('m13.no_unused_rewards')}</p>
        ) : (
          unused.map((r) => (
            <div key={r.id} className="bg-emerald-600/20 border border-emerald-600/40 rounded-xl p-4 space-y-3">
              <div className="flex items-start justify-between">
                <div>
                  <h4 className="font-semibold text-white">{r.reward?.name || '-'}</h4>
                  <p className="text-slate-300 text-xs mt-1">{t('m13.redeemed_on')} {formatDate(r.redeemed_at)}</p>
                </div>
                <span className="text-amber-400 text-xs font-bold">{r.points_used} pts</span>
              </div>
              <div className="bg-slate-900 rounded-lg p-3 text-center">
                {showCode === r.id ? (
                  <p className="text-2xl font-bold text-amber-400 tracking-widest">{r.redemption_code}</p>
                ) : (
                  <p className="text-slate-500 text-sm">•••••</p>
                )}
                <button
                  onClick={() => setShowCode(showCode === r.id ? null : r.id)}
                  className="text-xs text-slate-400 hover:text-white mt-1 flex items-center gap-1 mx-auto"
                >
                  {showCode === r.id ? <><EyeOff size={12} /> {t('m13.hide_code')}</> : <><Eye size={12} /> {t('m13.show_code')}</>}
                </button>
              </div>
              <button
                onClick={() => { setUseTarget(r); setError(''); }}
                className="w-full py-2.5 rounded-lg bg-amber-500 text-slate-900 font-semibold text-sm hover:bg-amber-400"
              >
                {t('m13.use_reward')}
              </button>
            </div>
          ))
        )}
      </div>

      <div className="space-y-3">
        <h3 className="text-sm font-semibold text-slate-400 uppercase">{t('m13.used_rewards')} ({used.length})</h3>
        {used.length === 0 ? (
          <p className="text-slate-500 text-sm">{t('m13.no_used_rewards')}</p>
        ) : (
          used.map((r) => (
            <div key={r.id} className="bg-slate-800/50 border border-slate-700 rounded-xl p-4 opacity-60">
              <div className="flex items-start justify-between">
                <div>
                  <h4 className="font-semibold text-slate-300">{r.reward?.name || '-'}</h4>
                  <p className="text-slate-500 text-xs mt-1">{t('m13.used_on')} {r.used_at ? formatDateTime(r.used_at) : '-'}</p>
                </div>
                <span className="text-slate-500 text-xs">{r.points_used} pts</span>
              </div>
              <div className="mt-2 text-center">
                <span className="inline-flex items-center gap-1 text-slate-500 text-sm font-medium">
                  <CheckCircle2 size={14} /> {t('m13.already_used')}
                </span>
              </div>
            </div>
          ))
        )}
      </div>

      {useTarget && (
        <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-black/60" onClick={() => setUseTarget(null)}>
          <div className="bg-slate-800 rounded-xl p-6 max-w-sm w-full space-y-4" onClick={(e) => e.stopPropagation()}>
            <AlertCircle size={32} className="text-amber-400 mx-auto" />
            <h3 className="text-lg font-bold text-white text-center">{t('m13.use_reward')}</h3>
            <p className="text-slate-300 text-sm text-center">{t('m13.use_confirm')}</p>
            <p className="text-slate-400 text-xs text-center">{t('m13.use_confirm_desc')}</p>
            {error && <p className="text-red-400 text-sm text-center">{error}</p>}
            <div className="flex gap-2">
              <button
                onClick={() => setUseTarget(null)}
                className="flex-1 py-2.5 rounded-lg bg-slate-700 text-white text-sm font-medium hover:bg-slate-600"
              >
                {t('common.cancel')}
              </button>
              <button
                onClick={handleUse}
                disabled={using}
                className="flex-1 py-2.5 rounded-lg bg-amber-500 text-slate-900 text-sm font-semibold hover:bg-amber-400 disabled:opacity-50"
              >
                {using ? <Loader2 size={16} className="animate-spin mx-auto" /> : t('m13.use_reward')}
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

function MemberHistory({ member }: { member: M13Member }) {
  const { t } = useI18n();
  const [entries, setEntries] = useState<M13PointLedger[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    (async () => {
      const data = await loyaltyService.getMemberPointHistory(member.id);
      setEntries(data);
      setLoading(false);
    })();
  }, [member.id]);

  if (loading) {
    return <div className="flex justify-center py-12"><Loader2 size={24} className="animate-spin text-amber-400" /></div>;
  }

  const typeLabels: Record<string, string> = {
    EARN: t('m13.earn'),
    REDEEM: t('m13.redeem'),
    ADJUSTMENT_DEBIT: t('m13.adjustment_debit'),
    ADJUSTMENT_CREDIT: t('m13.adjustment_credit'),
    EXPIRATION: t('m13.expiration'),
    REVERSAL: t('m13.reversal'),
  };

  const isPositive = (type: string) => type === 'EARN' || type === 'ADJUSTMENT_CREDIT';

  return (
    <div className="space-y-4">
      <div className="bg-gradient-to-br from-amber-500 to-amber-600 rounded-2xl p-5 text-center">
        <p className="text-amber-900 text-xs font-medium uppercase tracking-wider">{t('m13.current_balance')}</p>
        <p className="text-4xl font-bold text-slate-900 mt-1">{member.points_balance}</p>
      </div>

      <h2 className="text-xl font-bold text-white">{t('m13.history_title')}</h2>

      {entries.length === 0 ? (
        <div className="text-center py-12">
          <History size={48} className="text-slate-600 mx-auto mb-3" />
          <p className="text-slate-400">{t('m13.no_history_yet')}</p>
        </div>
      ) : (
        <div className="space-y-2">
          {entries.map((entry) => (
            <div key={entry.id} className="bg-slate-800 border border-slate-700 rounded-lg p-3 flex items-center justify-between">
              <div className="flex-1">
                <p className="text-white text-sm font-medium">{entry.description || typeLabels[entry.type] || entry.type}</p>
                <p className="text-slate-500 text-xs">{formatDateTime(entry.created_at)}</p>
              </div>
              <div className="text-right">
                <p className={`font-bold text-sm ${isPositive(entry.type) ? 'text-emerald-400' : 'text-red-400'}`}>
                  {isPositive(entry.type) ? '+' : '-'}{entry.points}
                </p>
                <p className="text-slate-500 text-xs">{typeLabels[entry.type] || entry.type}</p>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}

function MemberProfile({ member, guest }: { member: M13Member; guest: Guest | null }) {
  const { t } = useI18n();
  const [currentPassword, setCurrentPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState(false);
  const [changing, setChanging] = useState(false);

  const fields: { label: string; value: string | null }[] = [
    { label: t('m13.member_id'), value: member.member_number },
    { label: t('m13.full_name'), value: guest?.full_name || null },
    { label: t('common.email'), value: guest?.email || null },
    { label: t('m13.phone'), value: guest?.phone || null },
    { label: t('common.address'), value: guest?.address || null },
    { label: t('m13.id_number'), value: guest?.id_number || null },
    { label: t('m13.date_of_birth'), value: guest?.date_of_birth ? formatDate(guest.date_of_birth) : null },
  ];

  const handleChangePassword = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    setSuccess(false);

    if (newPassword.length < 6) {
      setError(t('m13.password_too_short'));
      return;
    }
    if (newPassword !== confirmPassword) {
      setError(t('m13.passwords_dont_match'));
      return;
    }

    setChanging(true);
    const { error } = await memberAuth.memberChangePassword(currentPassword, newPassword);
    if (error) {
      setError(error);
    } else {
      setSuccess(true);
      setCurrentPassword('');
      setNewPassword('');
      setConfirmPassword('');
    }
    setChanging(false);
  };

  return (
    <div className="space-y-6">
      <h2 className="text-xl font-bold text-white">{t('m13.profile_title')}</h2>

      <div className="bg-amber-500/10 border border-amber-500/30 rounded-lg p-3 flex items-start gap-2">
        <AlertCircle size={16} className="text-amber-400 flex-shrink-0 mt-0.5" />
        <p className="text-amber-200 text-sm">{t('m13.contact_reception')}</p>
      </div>

      <div className="bg-slate-800 border border-slate-700 rounded-xl divide-y divide-slate-700">
        {fields.map((field) => (
          <div key={field.label} className="px-4 py-3 flex items-center justify-between">
            <span className="text-slate-400 text-sm">{field.label}</span>
            <span className="text-white text-sm font-medium text-right">{field.value || '-'}</span>
          </div>
        ))}
      </div>

      <div className="bg-slate-800 border border-slate-700 rounded-xl p-5 space-y-4">
        <h3 className="font-semibold text-white flex items-center gap-2"><KeyRound size={18} /> {t('m13.change_password')}</h3>

        {success && (
          <div className="bg-emerald-600/20 border border-emerald-600/40 rounded-lg p-3 flex items-center gap-2">
            <CheckCircle2 size={16} className="text-emerald-400" />
            <p className="text-emerald-200 text-sm">{t('m13.password_changed')}</p>
          </div>
        )}

        <form onSubmit={handleChangePassword} className="space-y-3">
          <div>
            <label className="text-sm font-medium text-slate-300">{t('m13.current_password')}</label>
            <input
              type="password"
              value={currentPassword}
              onChange={(e) => setCurrentPassword(e.target.value)}
              required
              className="mt-1 w-full rounded-lg border border-slate-600 bg-slate-900 px-3 py-2 text-sm text-white outline-none focus:ring-2 focus:ring-amber-500"
            />
          </div>
          <div>
            <label className="text-sm font-medium text-slate-300">{t('m13.new_password')}</label>
            <input
              type="password"
              value={newPassword}
              onChange={(e) => setNewPassword(e.target.value)}
              required
              className="mt-1 w-full rounded-lg border border-slate-600 bg-slate-900 px-3 py-2 text-sm text-white outline-none focus:ring-2 focus:ring-amber-500"
            />
          </div>
          <div>
            <label className="text-sm font-medium text-slate-300">{t('m13.confirm_password')}</label>
            <input
              type="password"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
              required
              className="mt-1 w-full rounded-lg border border-slate-600 bg-slate-900 px-3 py-2 text-sm text-white outline-none focus:ring-2 focus:ring-amber-500"
            />
          </div>
          {error && <p className="text-red-400 text-sm">{error}</p>}
          <button
            type="submit"
            disabled={changing}
            className="w-full bg-amber-500 text-slate-900 font-semibold py-2.5 rounded-lg hover:bg-amber-400 transition-colors disabled:opacity-50 flex items-center justify-center gap-2"
          >
            {changing ? <Loader2 size={16} className="animate-spin" /> : <KeyRound size={16} />}
            {t('m13.change_password')}
          </button>
        </form>
      </div>
    </div>
  );
}
