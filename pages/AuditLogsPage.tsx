import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { Card } from '@/components/ui/Card';
import { Input, Select } from '@/components/ui/Form';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatDateTime, todayISO, addDays } from '@/lib/format';
import { ScrollText } from 'lucide-react';
import type { AuditLog, Profile } from '@/types/database';

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
            <option value="reservation_created">Reservation Created</option>
            <option value="reservation_modified">Reservation Modified</option>
            <option value="reservation_cancelled">Reservation Cancelled</option>
            <option value="check_in">Check In</option>
            <option value="check_out">Check Out</option>
            <option value="room_transfer">Room Transfer</option>
            <option value="payment">Payment</option>
            <option value="additional_charge">Additional Charge</option>
            <option value="damage_charge">Damage Charge</option>
            <option value="charge_voided">Charge Voided</option>
            <option value="business_day_closed">Business Day Closed</option>
            <option value="early_checkin_no_charge">Early Check-in (No Charge)</option>
            <option value="late_checkout_no_charge">Late Checkout (No Charge)</option>
            <option value="post_stay_charge">Post-Stay Charge</option>
          </Select>
        </div>
      </Card>

      {logs.length === 0 ? (
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
                </tr>
              </thead>
              <tbody>
                {logs.slice(0, 200).map((log) => (
                  <tr key={log.id} className="border-b border-slate-100 hover:bg-slate-50">
                    <td className="py-2 px-4 text-xs text-slate-400">{formatDateTime(log.created_at)}</td>
                    <td className="py-2 px-4 font-medium text-slate-700">{profiles[log.user_id || ''] || '-'}</td>
                    <td className="py-2 px-4"><Badge color="blue">{log.action.replace(/_/g, ' ')}</Badge></td>
                    <td className="py-2 px-4 text-slate-500">{log.object_type || '-'}</td>
                    <td className="py-2 px-4 text-xs text-slate-500 max-w-md truncate">
                      {log.reason || (log.new_value ? JSON.stringify(log.new_value).substring(0, 100) : '-')}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
          {logs.length > 200 && <p className="text-xs text-slate-400 p-3">Showing 200 of {logs.length} logs</p>}
        </Card>
      )}
    </div>
  );
}
