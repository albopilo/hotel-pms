import { useState, useEffect, useCallback, Fragment } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { Card } from '@/components/ui/Card';
import { Input, Select } from '@/components/ui/Form';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatDateTime, todayISO, addDays, formatIDR } from '@/lib/format';
import { ScrollText, ChevronDown, ChevronUp, Search } from 'lucide-react';
import type { AuditLog } from '@/types/database';

type ActionColor = 'green' | 'red' | 'blue' | 'amber' | 'gray' | 'teal' | 'orange';

function getActionColor(action: string): ActionColor {
  if (action.includes('created')) return 'green';
  if (action.includes('cancelled') || action.includes('voided')) return 'red';
  if (action.includes('check_in')) return 'teal';
  if (action.includes('check_out')) return 'orange';
  if (action.includes('payment')) return 'green';
  if (action.includes('charge') || action.includes('damage')) return 'amber';
  if (action.includes('transfer') || action.includes('split') || action.includes('extend')) return 'blue';
  if (action.includes('no_charge')) return 'gray';
  if (action.includes('closed')) return 'gray';
  return 'blue';
}

function formatActionLabel(action: string): string {
  const labels: Record<string, string> = {
    reservation_created: 'Reservation Created',
    reservation_modified: 'Reservation Modified',
    reservation_cancelled: 'Reservation Cancelled',
    check_in: 'Check In',
    check_out: 'Check Out',
    room_transfer: 'Room Transfer',
    payment: 'Payment Recorded',
    additional_charge: 'Additional Charge',
    damage_charge: 'Damage Charge',
    charge_voided: 'Charge Voided',
    business_day_closed: 'Business Day Closed',
    early_checkin_no_charge: 'Early Check-in (No Charge)',
    late_checkout_no_charge: 'Late Checkout (No Charge)',
    post_stay_charge: 'Post-Stay Charge',
    extend_stay: 'Extend Stay',
    split_room: 'Split Room',
  };
  return labels[action] || action.replace(/_/g, ' ').replace(/\b\w/g, c => c.toUpperCase());
}

function formatObjectType(type: string | null): string {
  if (!type) return '-';
  const labels: Record<string, string> = {
    reservation: 'Reservation',
    folio: 'Folio',
    folio_item: 'Folio Item',
    room: 'Room',
    guest: 'Guest',
    invoice: 'Invoice',
  };
  return labels[type] || type.replace(/_/g, ' ').replace(/\b\w/g, c => c.toUpperCase());
}

function formatValueObj(val: Record<string, unknown> | null): { label: string; value: string }[] {
  if (!val || typeof val !== 'object') return [];
  return Object.entries(val)
    .filter(([k]) => !['id', 'organization_id', 'branch_id', 'created_at', 'updated_at', 'created_by'].includes(k))
    .map(([k, v]) => {
      const label = k.replace(/_/g, ' ');
      let value: string;
      if (typeof v === 'number' && v > 1000) {
        value = formatIDR(v);
      } else if (v === null || v === undefined) {
        value = '-';
      } else if (typeof v === 'object') {
        value = JSON.stringify(v);
      } else {
        value = String(v);
      }
      return { label, value };
    });
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
  const [searchText, setSearchText] = useState('');
  const [expandedId, setExpandedId] = useState<string | null>(null);

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

  const filteredLogs = logs.filter((log) => {
    if (!searchText.trim()) return true;
    const q = searchText.toLowerCase().trim();
    const userName = profiles[log.user_id || ''] || '';
    if (userName.toLowerCase().includes(q)) return true;
    const detailStr = [
      log.reason || '',
      log.new_value ? JSON.stringify(log.new_value).toLowerCase() : '',
      log.previous_value ? JSON.stringify(log.previous_value).toLowerCase() : '',
      log.object_id || '',
      formatActionLabel(log.action).toLowerCase(),
    ].join(' ');
    return detailStr.toLowerCase().includes(q);
  });

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-slate-900">{t('nav.audit_logs')}</h1>
        <span className="text-sm text-slate-500">{logs.length} {logs.length === 1 ? 'entry' : 'entries'}</span>
      </div>

      <Card>
        <div className="flex gap-3 flex-wrap items-end">
          <Input label={t('common.from')} type="date" value={dateFrom} onChange={(e) => setDateFrom(e.target.value)} />
          <Input label={t('common.to')} type="date" value={dateTo} onChange={(e) => setDateTo(e.target.value)} />
          <div className="flex flex-col gap-1">
            <label className="text-sm font-medium text-slate-700">{t('common.search')}</label>
            <div className="relative">
              <Search size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
              <input
                type="text"
                value={searchText}
                onChange={(e) => setSearchText(e.target.value)}
                placeholder="Guest, RES-, FOL-, INV-..."
                className="rounded-lg border border-slate-300 pl-10 pr-3 py-2 text-sm outline-none focus:ring-2 focus:ring-blue-500"
              />
            </div>
          </div>
          <Select label={t('audit.action')} value={actionFilter} onChange={(e) => setActionFilter(e.target.value)}>
            <option value="all">{t('common.all')}</option>
            <option value="reservation_created">Reservation Created</option>
            <option value="reservation_modified">Reservation Modified</option>
            <option value="reservation_cancelled">Reservation Cancelled</option>
            <option value="check_in">Check In</option>
            <option value="check_out">Check Out</option>
            <option value="room_transfer">Room Transfer</option>
            <option value="extend_stay">Extend Stay</option>
            <option value="split_room">Split Room</option>
            <option value="payment">Payment Recorded</option>
            <option value="additional_charge">Additional Charge</option>
            <option value="damage_charge">Damage Charge</option>
            <option value="charge_voided">Charge Voided</option>
            <option value="post_stay_charge">Post-Stay Charge</option>
            <option value="business_day_closed">Business Day Closed</option>
            <option value="early_checkin_no_charge">Early Check-in (No Charge)</option>
            <option value="late_checkout_no_charge">Late Checkout (No Charge)</option>
          </Select>
        </div>
      </Card>

      {filteredLogs.length === 0 ? (
        <EmptyState icon={<ScrollText size={48} />} title={t('audit.no_logs')} />
      ) : (
        <Card noPadding>
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 text-slate-500">
                  <th className="text-left py-3 px-4">{t('common.date')}</th>
                  <th className="text-left py-3 px-4">{t('audit.user')}</th>
                  <th className="text-left py-3 px-4">{t('audit.action')}</th>
                  <th className="text-left py-3 px-4">{t('audit.object_type')}</th>
                  <th className="text-left py-3 px-4">Details</th>
                  <th className="w-8"></th>
                </tr>
              </thead>
              <tbody>
                {filteredLogs.slice(0, 200).map((log) => {
                  const prevValues = formatValueObj(log.previous_value);
                  const newValues = formatValueObj(log.new_value);
                  const hasDetails = prevValues.length > 0 || newValues.length > 0 || !!log.reason;
                  const isExpanded = expandedId === log.id;

                  return (
                    <Fragment key={log.id}>
                      <tr
                        className={`border-b border-slate-100 hover:bg-slate-50 ${hasDetails ? 'cursor-pointer' : ''}`}
                        onClick={() => hasDetails && setExpandedId(isExpanded ? null : log.id)}
                      >
                        <td className="py-2 px-4 text-xs text-slate-400">{formatDateTime(log.created_at)}</td>
                        <td className="py-2 px-4 font-medium text-slate-700">{profiles[log.user_id || ''] || '-'}</td>
                        <td className="py-2 px-4"><Badge color={getActionColor(log.action)}>{formatActionLabel(log.action)}</Badge></td>
                        <td className="py-2 px-4 text-slate-500">{formatObjectType(log.object_type)}</td>
                        <td className="py-2 px-4 text-xs text-slate-500 max-w-md truncate">
                          {log.reason || (newValues.length > 0 ? newValues.map(v => `${v.label}: ${v.value}`).join(', ') : '-')}
                        </td>
                        <td className="py-2 px-2">
                          {hasDetails && (
                            isExpanded ? <ChevronUp size={16} className="text-slate-400" /> : <ChevronDown size={16} className="text-slate-400" />
                          )}
                        </td>
                      </tr>
                      {isExpanded && (
                        <tr>
                          <td colSpan={6} className="bg-slate-50 px-6 py-4">
                            <div className="space-y-3">
                              {log.reason && (
                                <div className="text-sm">
                                  <span className="font-medium text-slate-600">Reason: </span>
                                  <span className="text-slate-700">{log.reason}</span>
                                </div>
                              )}
                              {prevValues.length > 0 && (
                                <div>
                                  <p className="text-xs font-semibold text-red-600 mb-1.5">Previous</p>
                                  <div className="flex flex-wrap gap-2">
                                    {prevValues.map((v, i) => (
                                      <span key={i} className="text-xs bg-red-50 text-red-700 border border-red-100 rounded px-2 py-1">
                                        <span className="font-medium">{v.label}:</span> {v.value}
                                      </span>
                                    ))}
                                  </div>
                                </div>
                              )}
                              {newValues.length > 0 && (
                                <div>
                                  <p className="text-xs font-semibold text-emerald-600 mb-1.5">New</p>
                                  <div className="flex flex-wrap gap-2">
                                    {newValues.map((v, i) => (
                                      <span key={i} className="text-xs bg-emerald-50 text-emerald-700 border border-emerald-100 rounded px-2 py-1">
                                        <span className="font-medium">{v.label}:</span> {v.value}
                                      </span>
                                    ))}
                                  </div>
                                </div>
                              )}
                            </div>
                          </td>
                        </tr>
                      )}
                    </Fragment>
                  );
                })}
              </tbody>
            </table>
          </div>
          {filteredLogs.length > 200 && <p className="text-xs text-slate-400 p-3">Showing 200 of {filteredLogs.length} entries</p>}
        </Card>
      )}
    </div>
  );
}
