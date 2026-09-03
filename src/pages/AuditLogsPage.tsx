import { useState, useEffect, useCallback, Fragment, useMemo } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { Card } from '@/components/ui/Card';
import { Input, Select } from '@/components/ui/Form';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { Pagination } from '@/components/ui/Pagination';
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

interface RefInfo {
  refNumber: string;
  roomNumber: string;
  guestName: string;
}

export function AuditLogsPage() {
  const { user } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();
  const [logs, setLogs] = useState<AuditLog[]>([]);
  const [profiles, setProfiles] = useState<Record<string, string>>({});
  const [refMap, setRefMap] = useState<Record<string, RefInfo>>({});
  const [loading, setLoading] = useState(true);
  const [dateFrom, setDateFrom] = useState(addDays(todayISO(), -30));
  const [dateTo, setDateTo] = useState(addDays(todayISO(), 1));
  const [actionFilter, setActionFilter] = useState('all');
  const [searchText, setSearchText] = useState('');
  const [expandedId, setExpandedId] = useState<string | null>(null);
  const [page, setPage] = useState(1);
  const PAGE_SIZE = 50;

  const load = useCallback(async () => {
    setLoading(true);
    let query = supabase.from('audit_logs').select('*').eq('organization_id', user!.organization_id).gte('created_at', dateFrom).lte('created_at', dateTo + 'T23:59:59').order('created_at', { ascending: false }).limit(2000);
    if (selectedBranchId) query = query.eq('branch_id', selectedBranchId);
    if (actionFilter !== 'all') query = query.eq('action', actionFilter);
    const { data } = await query;
    const logData = (data as AuditLog[]) || [];
    setLogs(logData);

    const { data: prof } = await supabase.from('profiles').select('id, full_name');
    const map: Record<string, string> = {};
    (prof || []).forEach((p: any) => { map[p.id] = p.full_name; });
    setProfiles(map);

    // Resolve object_ids to reference numbers, room numbers, and guest names
    const objectIds = logData.map(l => l.object_id).filter(Boolean) as string[];
    const refInfoMap: Record<string, RefInfo> = {};

    if (objectIds.length > 0) {
      const [{ data: reservations }, { data: folios }, { data: invoices }] = await Promise.all([
        supabase.from('reservations').select('id, reservation_number, room_id, primary_guest_id, primary_guest:guests(full_name)').in('id', objectIds),
        supabase.from('folios').select('id, folio_number, reservation_id, guest_id, guest:guests(full_name)').in('id', objectIds),
        supabase.from('invoices').select('id, invoice_number, reservation_id, folio_id, guest_id, guest:guests(full_name)').in('id', objectIds),
      ]);

      const roomIds = new Set<string>();
      let folioRes: any[] | null = null;
      let invRes: any[] | null = null;

      (reservations || []).forEach((r: any) => {
        if (r.room_id) roomIds.add(r.room_id);
        refInfoMap[r.id] = {
          refNumber: r.reservation_number,
          roomNumber: '',
          guestName: r.primary_guest?.full_name || '',
        };
      });

      // Collect folio reservation room_ids
      const folioResIds = (folios || []).map((f: any) => f.reservation_id).filter(Boolean);
      if (folioResIds.length > 0) {
        const { data } = await supabase.from('reservations').select('id, room_id, primary_guest_id, primary_guest:guests(full_name)').in('id', folioResIds);
        folioRes = data;
        const folioResMap: Record<string, any> = {};
        (folioRes || []).forEach((r: any) => { folioResMap[r.id] = r; if (r.room_id) roomIds.add(r.room_id); });

        (folios || []).forEach((f: any) => {
          const res = folioResMap[f.reservation_id];
          refInfoMap[f.id] = {
            refNumber: f.folio_number,
            roomNumber: '',
            guestName: f.guest?.full_name || res?.primary_guest?.full_name || '',
          };
        });
      }

      // Collect invoice reservation/folio room_ids
      const invResIds = (invoices || []).filter((i: any) => i.reservation_id).map((i: any) => i.reservation_id);
      if (invResIds.length > 0) {
        const { data } = await supabase.from('reservations').select('id, room_id, primary_guest_id, primary_guest:guests(full_name)').in('id', invResIds);
        invRes = data;
        const invResMap: Record<string, any> = {};
        (invRes || []).forEach((r: any) => { invResMap[r.id] = r; if (r.room_id) roomIds.add(r.room_id); });

        (invoices || []).forEach((i: any) => {
          const res = invResMap[i.reservation_id];
          refInfoMap[i.id] = {
            refNumber: i.invoice_number,
            roomNumber: '',
            guestName: i.guest?.full_name || res?.primary_guest?.full_name || '',
          };
        });
      }

      // Resolve room numbers
      if (roomIds.size > 0) {
        const { data: rooms } = await supabase.from('rooms').select('id, room_number').in('id', Array.from(roomIds));
        const roomMap: Record<string, string> = {};
        (rooms || []).forEach((r: any) => { roomMap[r.id] = r.room_number; });

        // Fill in room numbers for reservations
        (reservations || []).forEach((r: any) => {
          if (refInfoMap[r.id]) refInfoMap[r.id].roomNumber = roomMap[r.room_id] || '';
        });
        (folios || []).forEach((f: any) => {
          if (refInfoMap[f.id]) {
            const res = (folioRes || [])?.find((r: any) => r.id === f.reservation_id);
            if (res) refInfoMap[f.id].roomNumber = roomMap[res.room_id] || '';
          }
        });
        (invoices || []).forEach((i: any) => {
          if (refInfoMap[i.id]) {
            const res = (invRes || [])?.find((r: any) => r.id === i.reservation_id);
            if (res) refInfoMap[i.id].roomNumber = roomMap[res.room_id] || '';
          }
        });
      }
    }

    setRefMap(refInfoMap);
    setLoading(false);
  }, [user, selectedBranchId, dateFrom, dateTo, actionFilter]);

  useEffect(() => { load(); }, [load]);

  const filteredLogs = useMemo(() => {
    return logs.filter((log) => {
      if (!searchText.trim()) return true;
      const q = searchText.toLowerCase().trim();
      const userName = profiles[log.user_id || ''] || '';
      if (userName.toLowerCase().includes(q)) return true;
      const ref = refMap[log.object_id || ''];
      if (ref) {
        if (ref.refNumber.toLowerCase().includes(q)) return true;
        if (ref.roomNumber.toLowerCase().includes(q)) return true;
        if (ref.guestName.toLowerCase().includes(q)) return true;
      }
      const detailStr = [
        log.reason || '',
        log.new_value ? JSON.stringify(log.new_value).toLowerCase() : '',
        log.previous_value ? JSON.stringify(log.previous_value).toLowerCase() : '',
        formatActionLabel(log.action).toLowerCase(),
      ].join(' ');
      return detailStr.toLowerCase().includes(q);
    });
  }, [logs, searchText, profiles, refMap]);

  const pagedLogs = filteredLogs.slice((page - 1) * PAGE_SIZE, page * PAGE_SIZE);
  useEffect(() => { setPage(1); }, [searchText, actionFilter, dateFrom, dateTo]);

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-slate-900">{t('nav.audit_logs')}</h1>
        <span className="text-sm text-slate-500">{filteredLogs.length} {filteredLogs.length === 1 ? 'entry' : 'entries'}</span>
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
                placeholder="Guest, RES-, FOL-, INV-, room..."
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
                  <th className="text-left py-3 px-4">Reference</th>
                  <th className="text-left py-3 px-4">{t('common.room')}</th>
                  <th className="text-left py-3 px-4">{t('common.guest')}</th>
                  <th className="text-left py-3 px-4">Details</th>
                  <th className="w-8"></th>
                </tr>
              </thead>
              <tbody>
                {pagedLogs.map((log) => {
                  const prevValues = formatValueObj(log.previous_value);
                  const newValues = formatValueObj(log.new_value);
                  const hasDetails = prevValues.length > 0 || newValues.length > 0 || !!log.reason;
                  const isExpanded = expandedId === log.id;
                  const ref = refMap[log.object_id || ''];

                  return (
                    <Fragment key={log.id}>
                      <tr
                        className={`border-b border-slate-100 hover:bg-slate-50 ${hasDetails ? 'cursor-pointer' : ''}`}
                        onClick={() => hasDetails && setExpandedId(isExpanded ? null : log.id)}
                      >
                        <td className="py-2 px-4 text-xs text-slate-400 whitespace-nowrap">{formatDateTime(log.created_at)}</td>
                        <td className="py-2 px-4 font-medium text-slate-700 whitespace-nowrap">{profiles[log.user_id || ''] || '-'}</td>
                        <td className="py-2 px-4"><Badge color={getActionColor(log.action)}>{formatActionLabel(log.action)}</Badge></td>
                        <td className="py-2 px-4 font-medium text-blue-600 whitespace-nowrap">{ref?.refNumber || '-'}</td>
                        <td className="py-2 px-4 text-slate-600 whitespace-nowrap">{ref?.roomNumber || '-'}</td>
                        <td className="py-2 px-4 text-slate-600 whitespace-nowrap">{ref?.guestName || '-'}</td>
                        <td className="py-2 px-4 text-xs text-slate-500 max-w-xs truncate">
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
                          <td colSpan={8} className="bg-slate-50 px-6 py-4">
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
          <Pagination page={page} pageSize={PAGE_SIZE} total={filteredLogs.length} onPageChange={setPage} />
        </Card>
      )}
    </div>
  );
}
