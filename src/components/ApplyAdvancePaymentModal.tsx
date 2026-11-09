import { useEffect, useState } from 'react';
import { supabase } from '@/lib/supabase';
import { useToast } from '@/lib/toast';
import { useI18n } from '@/lib/i18n';
import { Modal } from '@/components/ui/Modal';
import { Button } from '@/components/ui/Button';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatIDR, formatDate } from '@/lib/format';
import { advancePaymentService } from '@/services/advancePaymentService';
import { FinancialError } from '@/services/financial';
import { HandCoins } from 'lucide-react';
import type { AdvancePayment, Folio } from '@/types/database';

export function ApplyAdvancePaymentModal({ folio, userId, onClose, onApplied }: {
  folio: Folio;
  userId: string;
  onClose: () => void;
  onApplied: () => void;
}) {
  const { showToast } = useToast();
  const { t } = useI18n();
  const [rows, setRows] = useState<AdvancePayment[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedId, setSelectedId] = useState<string | null>(null);
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    (async () => {
      const { data, error } = await supabase
        .from('advance_payments')
        .select('*')
        .eq('branch_id', folio.branch_id)
        .eq('status', 'available')
        .order('expected_check_in_date');
      if (error) showToast(t('adv.load_error'), 'error');
      else setRows((data as AdvancePayment[]) || []);
      setLoading(false);
    })();
  }, [folio.branch_id, showToast, t]);

  const selected = rows.find((r) => r.id === selectedId) || null;

  const apply = async () => {
    if (!selected) return;
    setSaving(true);
    try {
      await advancePaymentService.applyToFolio(selected, folio, userId);
      showToast(t('adv.apply_success'), 'success');
      onApplied();
    } catch (e) {
      showToast(e instanceof FinancialError ? e.message : t('adv.apply_error'), 'error');
      setSaving(false);
    }
  };

  return (
    <Modal open onClose={onClose} title={t('adv.apply_title')} size="md"
      footer={
        <div className="flex justify-end gap-2">
          <Button variant="outline" onClick={onClose} disabled={saving}>{t('common.cancel')}</Button>
          <Button onClick={apply} disabled={!selected} loading={saving}>
            {selected ? `${t('adv.apply_btn')} ${formatIDR(Number(selected.amount))}` : t('adv.apply_btn')}
          </Button>
        </div>
      }>
      {loading ? <LoadingPage /> : rows.length === 0 ? (
        <EmptyState icon={<HandCoins size={40} />} title={t('adv.no_available')} message={t('adv.no_available_desc')} />
      ) : (
        <ul className="space-y-2">
          {rows.map((r) => (
            <li key={r.id}>
              <button
                type="button"
                onClick={() => setSelectedId(r.id)}
                className={`w-full text-left rounded-lg border px-3 py-2 transition-colors ${
                  selectedId === r.id ? 'border-blue-500 bg-blue-50' : 'border-slate-200 hover:bg-slate-50'
                }`}
              >
                <div className="flex justify-between gap-3 text-sm">
                  <span className="font-medium text-slate-800">{r.guest_name}</span>
                  <span className="font-medium text-slate-800">{formatIDR(Number(r.amount))}</span>
                </div>
                <div className="text-xs text-slate-500">
                  {t('adv.check_in_label')} {formatDate(r.expected_check_in_date)} · {r.payment_method_code}
                </div>
              </button>
            </li>
          ))}
        </ul>
      )}
    </Modal>
  );
}
