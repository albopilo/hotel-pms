import { useState, useEffect, useCallback, useMemo } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { Card } from '@/components/ui/Card';
import { Input, Select } from '@/components/ui/Form';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatDateTime, formatIDR, todayISO, addDays } from '@/lib/format';
import { ScrollText, User, FileText, CreditCard, LogIn, LogOut, ArrowRightLeft, Plus, Circle as XCircle, Moon, CalendarPlus, Split as SplitIcon, Wrench } from 'lucide-react';
import type { AuditLog, Profile } from '@/types/database';

const ACTION_META: Record<string, { label: string; color: 'blue' | 'green' | 'red' | 'amber' | 'gray' | 'teal' | 'purple'; icon: typeof FileText }> = {
  reservation_created: { label: 'Reservation Created', color: 'blue', icon: FileText },
  reservation_modified: { label: 'Reservation Modified', color: 'blue', icon: FileText },
  reservation_cancelled: { label: 'Reservation Cancelled', color: 'red', icon: FileText },
  check_in: { label: 'Check In', color: 'green', icon: LogIn },
  check_out: { label: 'Check Out', color: 'teal', icon: LogOut },
  room_transfer: { label: 'Room Transfer', color: 'purple', icon: ArrowRightLeft },
  payment: { label: 'Payment', color: 'green', icon: CreditCard },
  additional_charge: { label: 'Additional Charge', color: 'amber', icon: Plus },
  damage_charge: { label: 'Damage Charge', color: 'red', icon: Wrench },
  charge_voided: { label: 'Charge Voided', color: 'red', icon: XCircle },
  business_day_closed: { label: 'Business Day Closed', color: 'gray', icon: Moon },
  early_checkin_no_charge: { label: 'Early Check-in (No Charge)', color: 'amber', icon: LogIn },
  late_checkout_no_charge: { label: 'Late Checkout (No Charge)', color: 'amber', icon: LogOut },
  post_stay_charge: { label: 'Post-Stay Charge', color: 'amber', icon: Plus },
  extend_stay: { label: 'Extend Stay', color: 'blue', icon: CalendarPlus },
  split_room: { label: 'Split Room', color: 'purple', icon: SplitIcon },
};

function formatDetails(log: AuditLog): string {
  if (log.reason) return log.reason;
  if (!log.new_value && !log.previous_value) return '-';
  const parts: string[] = [];
  if (log.previous_value) {
    const prev = log.previous_value as Record<string, unknown>;
    Object.entries(prev).forEach(([k, v]) => parts.push(`${k}: ${formatValue(v)}`));
  }
  if (log.new_value) {
    const next = log.new_value as Record<string, unknown>;
    Object.entries(next).forEach(([k, v]) => {
      const prefix = log.previous_value ? `→ ${k}: ` : `${k}: `;
      parts.push(prefix + formatValue(v));
    });
  }
  return parts.join(' · ');
}

function formatValue(v: unknown): string {
  if (v === null || v === undefined) return '-';
  if (typeof v === 'number') return v > 1000 ? formatIDR(v) : String(v);
  if (typeof v === 'object') return JSON.stringify(v).slice(0, 80);
  return String(v);
}

export function AuditLogsPage() {
  const { user } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();
  const [logs, setLogs] = useState<AuditLog[]>([]);
  const [profiles, setProfiles] = useState<Record<string, string>>({});
  const [loading, setLoading] = useState(true);
  const [dateFrom, setDateFrom] = useState(addDays(todayISO(), -30));
  const [dateTo, setDateTo] = useState(addDays(todayISO(), 1));
  const [actionFilter, setActionFilter] = useState('all');
  const [search, setSearch] = useState('');

  const actionOptions = useMemo(() => Object.keys(ACTION_META).sort(), []);

  const load = useCallback(async () => {
    setLoading(true);
    let query = supabase.from('audit_logs').select('*').eq('organization_id', user!.organization_id).gte('created_at', dateFrom).lte('created_at', dateTo + 'T23:59:59').order('created_at', { ascending: false }).limit(500);
    if (selectedBranchId) query = query.eq('branch_id', selectedBranchId);
    if (actionFilter !== 'all') query = query.eq('action', actionFilter);
    const { data } = await query;
    setLogs((data as AuditLog[]) || []);

    const { data: prof } = await supabase.from('profiles').select('id, full_name');
    const map: Record<string, string> = {};
    (prof || []).forEach((p: any) => { map[p.id] = p.full_name; });
    setProfiles(map);
    setLoading(false);
  }, [user, selectedBranchId, dateFrom, dateTo, actionFilter]);

  useEffect(() => { load(); }, [load]);

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-slate-900">{t('nav.audit_logs')}</h1>

      <Card>
        <div className="flex gap-3 flex-wrap items-end">
          <Input label={t('common.from')} type="date" value={dateFrom} onChange={(e) => setDateFrom(e.target.value)} />
          <Input label={t('common.to')} type="date" value={dateTo} onChange={(e) => setDateTo(e.target.value)} />
          <Select label={t('audit.action')} value={actionFilter} onChange={(e) => setActionFilter(e.target.value)}>
            <option value="all">{t('common.all')}</option>
            {actionOptions.map(a => <option key={a} value={a}>{ACTION_META[a].label}</option>)}
          </Select>
          <div className="flex-1 min-w-[200px]">
            <Input label="Search" value={search} onChange={(e) => setSearch(e.target.value)} placeholder="Search user or details" />
          </div>
        </div>
      </Card>

      {logs.length === 0 ? (
        <EmptyState icon={<ScrollText size={48} />} title={t('audit.no_logs')} />
      ) : (
        <Card noPadding>
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 text-slate-500 bg-slate-50">
                  <th className="text-left py-3 px-4">{t('common.date')}</th>
                  <th className="text-left py-3 px-4">{t('audit.user')}</th>
                  <th className="text-left py-3 px-4">{t('audit.action')}</th>
                  <th className="text-left py-3 px-4">{t('audit.object_type')}</th>
                  <th className="text-left py-3 px-4">Details</th>
                </tr>
              </thead>
              <tbody>
                {logs.filter(log => {
                  if (!search) return true;
                  const q = search.toLowerCase();
                  const userName = profiles[log.user_id || ''] || '';
                  return userName.toLowerCase().includes(q) || formatDetails(log).toLowerCase().includes(q) || log.action.toLowerCase().includes(q);
                }).slice(0, 200).map((log) => {
                  const meta = ACTION_META[log.action] || { label: log.action.replace(/_/g, ' '), color: 'gray' as const, icon: ScrollText };
                  const Icon = meta.icon;
                  return (
                    <tr key={log.id} className="border-b border-slate-100 hover:bg-slate-50">
                      <td className="py-2.5 px-4 text-xs text-slate-400 whitespace-nowrap">{formatDateTime(log.created_at)}</td>
                      <td className="py-2.5 px-4 font-medium text-slate-700">{profiles[log.user_id || ''] || '-'}</td>
                      <td className="py-2.5 px-4">
                        <span className="inline-flex items-center gap-1.5">
                          <Icon size={14} className="text-slate-400" />
                          <Badge color={meta.color}>{meta.label}</Badge>
                        </span>
                      </td>
                      <td className="py-2.5 px-4 text-slate-500 capitalize">{log.object_type ? log.object_type.replace(/_/g, ' ') : '-'}</td>
                      <td className="py-2.5 px-4 text-xs text-slate-600 max-w-lg">
                        {formatDetails(log)}
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
          {logs.length > 200 && <p className="text-xs text-slate-400 p-3">Showing 200 of {logs.length} logs</p>}
        </Card>
      )}
    </div>
  );
}
