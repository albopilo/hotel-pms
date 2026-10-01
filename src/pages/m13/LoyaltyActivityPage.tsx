import { useState, useEffect, useCallback } from 'react';
import { useAuth } from '@/lib/auth';
import { useI18n } from '@/lib/i18n';
import { Card } from '@/components/ui/Card';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { loyaltyService } from '@/services/loyaltyService';
import { formatDateTime } from '@/lib/format';
import { Activity } from 'lucide-react';
import type { M13PointLedger, M13Member } from '@/types/database';

type ActivityEntry = M13PointLedger & { member: M13Member; staff_name: string | null };

export function LoyaltyActivityPage() {
  const { user } = useAuth();
  const { t } = useI18n();
  const [entries, setEntries] = useState<ActivityEntry[]>([]);
  const [loading, setLoading] = useState(true);

  const load = useCallback(async () => {
    if (!user) return;
    setLoading(true);
    const data = await loyaltyService.getLoyaltyActivity(user.organization_id);
    setEntries(data as ActivityEntry[]);
    setLoading(false);
  }, [user]);

  useEffect(() => { load(); }, [load]);

  if (loading) return <LoadingPage message={t('common.loading')} />;

  const typeLabels: Record<string, string> = {
    EARN: t('m13.earn'), REDEEM: t('m13.redeem'),
    ADJUSTMENT_DEBIT: t('m13.adjustment_debit'), ADJUSTMENT_CREDIT: t('m13.adjustment_credit'),
    EXPIRATION: t('m13.expiration'), REVERSAL: t('m13.reversal'),
  };

  const typeColors: Record<string, 'green' | 'red' | 'blue' | 'amber'> = {
    EARN: 'green', REDEEM: 'red', ADJUSTMENT_DEBIT: 'red', ADJUSTMENT_CREDIT: 'green',
    EXPIRATION: 'amber', REVERSAL: 'amber',
  };

  return (
    <div className="space-y-6">
      <div>
        <h1 className="text-2xl font-bold text-slate-900">{t('m13.activity_title')}</h1>
        <p className="text-sm text-slate-500 mt-1">{t('m13.activity_desc')}</p>
      </div>

      {entries.length === 0 ? (
        <EmptyState icon={<Activity size={48} />} title={t('m13.no_activity')} />
      ) : (
        <Card noPadding>
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 text-slate-500">
                  <th className="text-left py-3 px-4">{t('m13.date')}</th>
                  <th className="text-left py-3 px-4">{t('m13.member_id')}</th>
                  <th className="text-left py-3 px-4">{t('m13.type')}</th>
                  <th className="text-left py-3 px-4">{t('m13.description')}</th>
                  <th className="text-right py-3 px-4">{t('m13.points')}</th>
                  <th className="text-left py-3 px-4">{t('m13.performed_by')}</th>
                </tr>
              </thead>
              <tbody>
                {entries.map((e) => (
                  <tr key={e.id} className="border-b border-slate-100 hover:bg-slate-50">
                    <td className="py-3 px-4 text-slate-500">{formatDateTime(e.created_at)}</td>
                    <td className="py-3 px-4 font-mono text-slate-700">{e.member?.member_number || '-'}</td>
                    <td className="py-3 px-4"><Badge color={typeColors[e.type] || 'gray'}>{typeLabels[e.type] || e.type}</Badge></td>
                    <td className="py-3 px-4 text-slate-700">{e.description || '-'}</td>
                    <td className={`py-3 px-4 text-right font-bold ${(e.type === 'EARN' || e.type === 'ADJUSTMENT_CREDIT') ? 'text-emerald-600' : 'text-red-600'}`}>
                      {(e.type === 'EARN' || e.type === 'ADJUSTMENT_CREDIT') ? '+' : '-'}{e.points}
                    </td>
                    <td className="py-3 px-4 text-slate-500">{e.staff_name || '-'}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </Card>
      )}
    </div>
  );
}
