import { useState, useEffect, useCallback, useMemo } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { Card } from '@/components/ui/Card';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatIDR, formatDate } from '@/lib/format';
import { Moon, CircleAlert as AlertCircle, CircleCheck as CheckCircle2, Clock, History } from 'lucide-react';
import type { NightAudit, HotelBusinessDate } from '@/types/database';

interface AuditRow extends NightAudit {
  closed_by_name?: string;
}

export function NightAuditPage() {
  const { user, branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();
  const [loading, setLoading] = useState(true);
  const [businessDate, setBusinessDate] = useState<HotelBusinessDate | null>(null);
  const [latestAudit, setLatestAudit] = useState<AuditRow | null>(null);
  const [history, setHistory] = useState<AuditRow[]>([]);

  const branchIds = useMemo(
    () => selectedBranchId ? [selectedBranchId] : branches.map((b) => b.id),
    [selectedBranchId, branches]
  );

  const load = useCallback(async () => {
    if (branchIds.length === 0) { setLoading(false); return; }
    setLoading(true);

    // Trigger the automatic night audit as a fallback (in case pg_cron is not running)
    try {
      await supabase.rpc('auto_night_audit');
    } catch {
      // ignore — still show whatever data exists
    }

    // Current open business date across selected branches
    const { data: bd } = await supabase
      .from('hotel_business_dates')
      .select('*')
      .in('branch_id', branchIds)
      .order('business_date', { ascending: false })
      .limit(1)
      .maybeSingle();
    setBusinessDate(bd as HotelBusinessDate | null);

    // Latest completed night audit for the selection
    const { data: audits } = await supabase
      .from('night_audits')
      .select('*')
      .in('branch_id', branchIds)
      .order('business_date', { ascending: false })
      .limit(30);
    const rows = (audits as AuditRow[]) || [];

    // Resolve closed_by names
    const userIds = Array.from(new Set(rows.map((r) => r.closed_by).filter(Boolean))) as string[];
    if (userIds.length) {
      const { data: profiles } = await supabase
        .from('profiles')
        .select('id, full_name')
        .in('id', userIds);
      const nameMap: Record<string, string> = {};
      (profiles || []).forEach((p: any) => { nameMap[p.id] = p.full_name; });
      rows.forEach((r) => { r.closed_by_name = nameMap[r.closed_by] || '-'; });
    }

    setLatestAudit(rows[0] || null);
    setHistory(rows.slice(1));
    setLoading(false);
  }, [branchIds]);

  useEffect(() => { load(); }, [load]);

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-slate-900">{t('night_audit.title')}</h1>
        <span className="inline-flex items-center gap-1.5 text-xs font-medium text-emerald-700 bg-emerald-50 border border-emerald-200 rounded-lg px-3 py-1.5">
          <Clock size={14} /> Auto-generated
        </span>
      </div>

      <Card title={t('night_audit.business_date')}>
        <div className="flex items-center gap-4 flex-wrap">
          <div className="text-3xl font-bold text-slate-900">{formatDate(businessDate?.business_date || new Date().toISOString().split('T')[0])}</div>
          <Badge color={businessDate?.status === 'open' ? 'green' : 'gray'}>{businessDate?.status || 'open'}</Badge>
        </div>
      </Card>

      {!latestAudit ? (
        <EmptyState
          icon={<Moon size={48} />}
          title={t('common.no_data')}
          message="The night audit runs automatically. Results will appear here once the business day closes."
        />
      ) : (
        <AuditResult audit={latestAudit} t={t} />
      )}

      {history.length > 0 && (
        <Card title="Recent Night Audits">
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 text-slate-500">
                  <th className="text-left py-2 px-3">{t('night_audit.business_date')}</th>
                  <th className="text-center py-2 px-3">{t('dash.arrivals_today')}</th>
                  <th className="text-center py-2 px-3">{t('dash.departures_today')}</th>
                  <th className="text-center py-2 px-3">{t('dash.in_house_guests')}</th>
                  <th className="text-right py-2 px-3">{t('dash.total_income')}</th>
                  <th className="text-right py-2 px-3">{t('common.outstanding')}</th>
                  <th className="text-left py-2 px-3">{t('night_audit.closed_at')}</th>
                </tr>
              </thead>
              <tbody>
                {history.map((a) => {
                  const income = (a.room_charges || 0) + (a.additional_charges || 0);
                  return (
                    <tr key={a.id} className="border-b border-slate-100 hover:bg-slate-50">
                      <td className="py-2 px-3 font-medium text-slate-700">{formatDate(a.business_date)}</td>
                      <td className="text-center py-2 px-3">{a.arrivals}</td>
                      <td className="text-center py-2 px-3">{a.departures}</td>
                      <td className="text-center py-2 px-3">{a.in_house}</td>
                      <td className="text-right py-2 px-3 font-medium">{formatIDR(income)}</td>
                      <td className={`text-right py-2 px-3 ${a.outstanding > 0 ? 'text-red-600' : 'text-slate-400'}`}>{a.outstanding > 0 ? formatIDR(a.outstanding) : '-'}</td>
                      <td className="py-2 px-3 text-xs text-slate-400">{formatDate(a.closed_at)}</td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        </Card>
      )}
    </div>
  );
}

function AuditResult({ audit, t }: { audit: AuditRow; t: (k: string) => string }) {
  const exceptions: string[] = Array.isArray(audit.exceptions)
    ? (audit.exceptions as unknown[]).map((e) => (typeof e === 'string' ? e : String(e)))
    : [];

  const income = (audit.room_charges || 0) + (audit.additional_charges || 0);

  return (
    <>
      <div className="flex items-center gap-2 text-sm text-slate-500">
        <History size={16} />
        <span>{t('night_audit.business_date')}: <b className="text-slate-700">{formatDate(audit.business_date)}</b></span>
        <span className="mx-1">·</span>
        <span>{t('night_audit.closed_by')}: <b className="text-slate-700">{audit.closed_by_name || '-'}</b></span>
        <span className="mx-1">·</span>
        <span>{t('night_audit.closed_at')}: <b className="text-slate-700">{formatDate(audit.closed_at)}</b></span>
      </div>

      {exceptions.length > 0 ? (
        <Card title={t('night_audit.exceptions')}>
          <div className="space-y-2">
            {exceptions.map((exc, i) => (
              <div key={i} className="flex items-center gap-2 bg-amber-50 border border-amber-200 rounded-lg px-3 py-2 text-amber-700">
                <AlertCircle size={16} /> <span className="text-sm font-medium">{exc}</span>
              </div>
            ))}
          </div>
        </Card>
      ) : (
        <Card><div className="flex items-center gap-2 text-emerald-600"><CheckCircle2 size={18} /> <span className="font-medium">{t('night_audit.no_exceptions')}</span></div></Card>
      )}

      <div className="grid grid-cols-2 md:grid-cols-4 gap-3">
        <Card><div className="text-center"><p className="text-sm text-slate-500">{t('dash.arrivals_today')}</p><p className="text-2xl font-bold text-blue-600">{audit.arrivals}</p></div></Card>
        <Card><div className="text-center"><p className="text-sm text-slate-500">{t('dash.departures_today')}</p><p className="text-2xl font-bold text-amber-600">{audit.departures}</p></div></Card>
        <Card><div className="text-center"><p className="text-sm text-slate-500">{t('dash.in_house_guests')}</p><p className="text-2xl font-bold text-emerald-600">{audit.in_house}</p></div></Card>
        <Card><div className="text-center"><p className="text-sm text-slate-500">No Shows</p><p className="text-2xl font-bold text-red-600">{audit.no_shows}</p></div></Card>
      </div>

      <Card title="Financial Summary">
        <div className="grid grid-cols-2 md:grid-cols-4 gap-3 text-sm">
          <div className="bg-slate-50 rounded-lg p-3"><p className="text-slate-500">{t('checkout.room_charges')}</p><p className="font-bold">{formatIDR(audit.room_charges)}</p></div>
          <div className="bg-slate-50 rounded-lg p-3"><p className="text-slate-500">{t('checkout.additional_charges')}</p><p className="font-bold">{formatIDR(audit.additional_charges)}</p></div>
          <div className="bg-slate-50 rounded-lg p-3"><p className="text-slate-500">{t('common.discount')}</p><p className="font-bold text-red-600">{formatIDR(audit.discounts)}</p></div>
          <div className="bg-slate-50 rounded-lg p-3"><p className="text-slate-500">{t('common.outstanding')}</p><p className="font-bold text-red-600">{formatIDR(audit.outstanding)}</p></div>
          <div className="bg-emerald-50 rounded-lg p-3"><p className="text-slate-500">{t('payment.cash')}</p><p className="font-bold text-emerald-700">{formatIDR(audit.cash)}</p></div>
          <div className="bg-emerald-50 rounded-lg p-3"><p className="text-slate-500">EDC</p><p className="font-bold text-emerald-700">{formatIDR(audit.edc)}</p></div>
          <div className="bg-emerald-50 rounded-lg p-3"><p className="text-slate-500">OTA</p><p className="font-bold text-emerald-700">{formatIDR(audit.ota)}</p></div>
          <div className="bg-emerald-50 rounded-lg p-3"><p className="text-slate-500">{t('dash.total_income')}</p><p className="font-bold text-emerald-700">{formatIDR(income)}</p></div>
        </div>
      </Card>
    </>
  );
}
