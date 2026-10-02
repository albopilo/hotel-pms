import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card, StatCard } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal } from '@/components/ui/Modal';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { Input, Textarea, Select } from '@/components/ui/Form';
import { Pagination } from '@/components/ui/Pagination';
import { loyaltyService } from '@/services/loyaltyService';
import { formatDate, formatDateTime } from '@/lib/format';
import { Users, Star, Ticket, TrendingUp, Search, Minus, Plus, History as HistoryIcon, QrCode } from 'lucide-react';
import { openPrintTab } from '@/lib/printRoute';
import type { M13MemberWithGuest, M13PointLedger, M13RewardRedemptionWithReward, M13Member, UserRole } from '@/types/database';

const PAGE_SIZE = 20;

export function LoyaltyMembersPage() {
  const { user } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [members, setMembers] = useState<M13MemberWithGuest[]>([]);
  const [loading, setLoading] = useState(true);
  const [search, setSearch] = useState('');
  const [page, setPage] = useState(1);
  const [selectedMember, setSelectedMember] = useState<M13MemberWithGuest | null>(null);

  const load = useCallback(async () => {
    if (!user) return;
    setLoading(true);
    const data = await loyaltyService.getAllMembers(user.organization_id, search);
    setMembers(data);
    setLoading(false);
  }, [user, search]);

  useEffect(() => { load(); }, [load]);

  const stats = {
    total: members.length,
    active: members.filter((m) => m.status === 'active').length,
    points: members.reduce((s, m) => s + m.points_balance, 0),
  };

  const paged = members.slice((page - 1) * PAGE_SIZE, page * PAGE_SIZE);

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-slate-900">{t('m13.members_title')}</h1>
        <Button variant="outline" size="sm" onClick={() => openPrintTab({ type: 'm13-qr' })}>
          <QrCode size={16} /> Print QR Code
        </Button>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        <StatCard label={t('m13.total_members')} value={stats.total} icon={<Users size={20} />} color="blue" />
        <StatCard label={t('m13.active_members')} value={stats.active} icon={<Star size={20} />} color="green" />
        <StatCard label={t('m13.total_points_circulating')} value={stats.points} icon={<TrendingUp size={20} />} color="amber" />
      </div>

      <div className="relative max-w-md">
        <Search size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
        <input
          type="text"
          value={search}
          onChange={(e) => { setSearch(e.target.value); setPage(1); }}
          placeholder={t('m13.search_members')}
          className="w-full rounded-lg border border-slate-300 pl-10 pr-3 py-2 text-sm outline-none focus:ring-2 focus:ring-blue-500"
        />
      </div>

      {members.length === 0 ? (
        <EmptyState icon={<Users size={48} />} title={t('m13.no_members')} />
      ) : (
        <Card noPadding>
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 text-slate-500">
                  <th className="text-left py-3 px-4">{t('m13.member_id')}</th>
                  <th className="text-left py-3 px-4">{t('m13.guest_name')}</th>
                  <th className="text-left py-3 px-4">{t('common.phone')}</th>
                  <th className="text-right py-3 px-4">{t('m13.points_balance')}</th>
                  <th className="text-left py-3 px-4">{t('m13.member_status')}</th>
                  <th className="text-left py-3 px-4">{t('m13.joined')}</th>
                  <th className="text-right py-3 px-4">{t('common.actions')}</th>
                </tr>
              </thead>
              <tbody>
                {paged.map((m) => (
                  <tr key={m.id} className="border-b border-slate-100 hover:bg-slate-50 cursor-pointer" onClick={() => setSelectedMember(m)}>
                    <td className="py-3 px-4 font-mono font-medium text-slate-800">{m.member_number}</td>
                    <td className="py-3 px-4 font-medium text-slate-800">{m.guest?.full_name || '-'}</td>
                    <td className="py-3 px-4">{m.guest?.phone || '-'}</td>
                    <td className="py-3 px-4 text-right font-bold text-amber-600">{m.points_balance}</td>
                    <td className="py-3 px-4"><Badge color={m.status === 'active' ? 'green' : 'gray'}>{m.status === 'active' ? t('common.active') : t('common.inactive')}</Badge></td>
                    <td className="py-3 px-4 text-slate-500">{formatDate(m.created_at)}</td>
                    <td className="py-3 px-4 text-right">
                      <Button size="sm" variant="outline" onClick={(e) => { e.stopPropagation(); setSelectedMember(m); }}>{t('m13.view_history')}</Button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
          <Pagination page={page} pageSize={PAGE_SIZE} total={members.length} onPageChange={setPage} />
        </Card>
      )}

      {selectedMember && (
        <MemberDetailModal member={selectedMember} onClose={() => setSelectedMember(null)} />
      )}
    </div>
  );
}

function MemberDetailModal({ member, onClose }: { member: M13MemberWithGuest; onClose: () => void }) {
  const { user } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [tab, setTab] = useState<'history' | 'redemptions'>('history');
  const [history, setHistory] = useState<M13PointLedger[]>([]);
  const [redemptions, setRedemptions] = useState<M13RewardRedemptionWithReward[]>([]);
  const [loadingData, setLoadingData] = useState(true);
  const [showAdjust, setShowAdjust] = useState(false);
  const [adjustPoints, setAdjustPoints] = useState('');
  const [adjustReason, setAdjustReason] = useState('');
  const [adjusting, setAdjusting] = useState(false);

  useEffect(() => {
    (async () => {
      setLoadingData(true);
      const [h, r] = await Promise.all([
        loyaltyService.getMemberPointHistory(member.id),
        loyaltyService.getMemberRedemptions(member.id),
      ]);
      setHistory(h);
      setRedemptions(r);
      setLoadingData(false);
    })();
  }, [member.id]);

  const role = user?.role as UserRole;
  const canAddPoints = role === 'super_admin';
  const canDeduct = role === 'super_admin' || role === 'manager' || role === 'receptionist';

  const handleAdjust = async (isAdd: boolean) => {
    const pts = parseInt(adjustPoints, 10);
    if (!pts || pts <= 0) { showToast('Invalid points value', 'error'); return; }
    if (!adjustReason.trim()) { showToast(t('m13.adjust_reason_required'), 'error'); return; }
    setAdjusting(true);
    const signedPts = isAdd ? pts : -pts;
    const { error } = await loyaltyService.adjustPoints(member.id, signedPts, adjustReason, user!.id);
    if (error) {
      showToast(t('m13.adjust_failed') + ': ' + error, 'error');
    } else {
      showToast(t('m13.adjust_success'), 'success');
      setShowAdjust(false);
      setAdjustPoints('');
      setAdjustReason('');
      const h = await loyaltyService.getMemberPointHistory(member.id);
      setHistory(h);
    }
    setAdjusting(false);
  };

  const typeLabels: Record<string, string> = {
    EARN: t('m13.earn'), REDEEM: t('m13.redeem'),
    ADJUSTMENT_DEBIT: t('m13.adjustment_debit'), ADJUSTMENT_CREDIT: t('m13.adjustment_credit'),
    EXPIRATION: t('m13.expiration'), REVERSAL: t('m13.reversal'),
  };

  return (
    <Modal open onClose={onClose} title={`${member.member_number} — ${member.guest?.full_name || ''}`} size="lg">
      <div className="space-y-4">
        <div className="flex items-center gap-4">
          <div className="bg-amber-50 rounded-xl p-4 flex-1 text-center">
            <p className="text-xs text-slate-500 font-medium">{t('m13.points_balance')}</p>
            <p className="text-3xl font-bold text-amber-600 mt-1">{member.points_balance}</p>
          </div>
          <div className="flex flex-col gap-2">
            {canDeduct && <Button size="sm" variant="danger" onClick={() => setShowAdjust(true)}><Minus size={14} /> {t('m13.deduct_points')}</Button>}
            {canAddPoints && <Button size="sm" variant="success" onClick={() => setShowAdjust(true)}><Plus size={14} /> {t('m13.add_points')}</Button>}
          </div>
        </div>

        <div className="flex gap-2">
          <button
            onClick={() => setTab('history')}
            className={`px-4 py-2 rounded-lg text-sm font-medium ${tab === 'history' ? 'bg-blue-600 text-white' : 'bg-slate-100 text-slate-600'}`}
          >
            {t('m13.point_history')}
          </button>
          <button
            onClick={() => setTab('redemptions')}
            className={`px-4 py-2 rounded-lg text-sm font-medium ${tab === 'redemptions' ? 'bg-blue-600 text-white' : 'bg-slate-100 text-slate-600'}`}
          >
            {t('m13.redemptions')}
          </button>
        </div>

        {loadingData ? (
          <p className="text-center text-slate-400 py-4">{t('common.loading')}</p>
        ) : tab === 'history' ? (
          history.length === 0 ? (
            <EmptyState icon={<HistoryIcon size={32} />} title={t('m13.no_history')} />
          ) : (
            <div className="space-y-2 max-h-64 overflow-y-auto">
              {history.map((e) => (
                <div key={e.id} className="flex items-center justify-between border border-slate-100 rounded-lg px-3 py-2">
                  <div>
                    <p className="text-sm font-medium text-slate-800">{e.description || typeLabels[e.type] || e.type}</p>
                    <p className="text-xs text-slate-400">{formatDateTime(e.created_at)}</p>
                  </div>
                  <div className="text-right">
                    <p className={`font-bold text-sm ${(e.type === 'EARN' || e.type === 'ADJUSTMENT_CREDIT') ? 'text-emerald-600' : 'text-red-600'}`}>
                      {(e.type === 'EARN' || e.type === 'ADJUSTMENT_CREDIT') ? '+' : '-'}{e.points}
                    </p>
                    <p className="text-xs text-slate-400">{typeLabels[e.type] || e.type}</p>
                  </div>
                </div>
              ))}
            </div>
          )
        ) : (
          redemptions.length === 0 ? (
            <EmptyState icon={<Ticket size={32} />} title={t('m13.no_redemptions')} />
          ) : (
            <div className="space-y-2 max-h-64 overflow-y-auto">
              {redemptions.map((r) => (
                <div key={r.id} className="flex items-center justify-between border border-slate-100 rounded-lg px-3 py-2">
                  <div>
                    <p className="text-sm font-medium text-slate-800">{r.reward?.name || '-'}</p>
                    <p className="text-xs text-slate-400">{r.redemption_code} · {formatDate(r.redeemed_at)}</p>
                  </div>
                  <div className="flex items-center gap-2">
                    <span className="text-sm font-bold text-amber-600">{r.points_used} pts</span>
                    <Badge color={r.status === 'UNUSED' ? 'green' : 'gray'}>{r.status}</Badge>
                  </div>
                </div>
              ))}
            </div>
          )
        )}

        {showAdjust && (
          <div className="bg-slate-50 border border-slate-200 rounded-lg p-4 space-y-3">
            <h4 className="font-semibold text-slate-800">{canAddPoints ? t('m13.add_points') + ' / ' + t('m13.deduct_points') : t('m13.deduct_points')}</h4>
            <Input
              type="number"
              label={canAddPoints ? t('m13.points_to_add') + ' / ' + t('m13.points_to_deduct') : t('m13.points_to_deduct')}
              value={adjustPoints}
              onChange={(e) => setAdjustPoints(e.target.value)}
              placeholder="0"
            />
            <Textarea
              label={t('common.reason')}
              value={adjustReason}
              onChange={(e) => setAdjustReason(e.target.value)}
              placeholder={t('m13.adjust_reason_required')}
              rows={2}
            />
            <div className="flex gap-2">
              {canAddPoints && (
                <Button size="sm" variant="success" loading={adjusting} onClick={() => handleAdjust(true)}>
                  <Plus size={14} /> {t('m13.add_points')}
                </Button>
              )}
              <Button size="sm" variant="danger" loading={adjusting} onClick={() => handleAdjust(false)}>
                <Minus size={14} /> {t('m13.deduct_points')}
              </Button>
              <Button size="sm" variant="secondary" onClick={() => setShowAdjust(false)}>{t('common.cancel')}</Button>
            </div>
          </div>
        )}
      </div>
    </Modal>
  );
}
