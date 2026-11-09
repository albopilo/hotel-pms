import { useState, useEffect, useCallback, useMemo } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useToast } from '@/lib/toast';
import { useI18n } from '@/lib/i18n';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal } from '@/components/ui/Modal';
import { Badge } from '@/components/ui/Badge';
import { Input, Select, MoneyInput } from '@/components/ui/Form';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatIDR, formatDate, formatDateTime, todayISO } from '@/lib/format';
import { advancePaymentService } from '@/services/advancePaymentService';
import { FinancialError } from '@/services/financial';
import { Plus, HandCoins } from 'lucide-react';
import type { AdvancePayment, AdvancePaymentStatus, PaymentMethod } from '@/types/database';

export function AdvancePaymentsPage() {
  const { user, branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { showToast } = useToast();
  const { t } = useI18n();

  const branchIds = useMemo(
    () => (selectedBranchId ? [selectedBranchId] : branches.map((b) => b.id)),
    [selectedBranchId, branches],
  );
  const canRefund = user?.role === 'super_admin' || user?.role === 'manager';

  const [rows, setRows] = useState<AdvancePayment[]>([]);
  const [methods, setMethods] = useState<PaymentMethod[]>([]);
  const [loading, setLoading] = useState(true);
  const [showCreate, setShowCreate] = useState(false);
  const [refundTarget, setRefundTarget] = useState<AdvancePayment | null>(null);

  const branchNameById = useMemo(
    () => Object.fromEntries(branches.map((b) => [b.id, b.name])),
    [branches],
  );

  const statusBadge = (status: AdvancePaymentStatus) => {
    const map: Record<AdvancePaymentStatus, { color: 'green' | 'blue' | 'gray'; labelKey: string }> = {
      available: { color: 'green', labelKey: 'adv.status_available' },
      claimed: { color: 'blue', labelKey: 'adv.status_claimed' },
      cancelled: { color: 'gray', labelKey: 'adv.status_cancelled' },
    };
    return { color: map[status].color, label: t(map[status].labelKey) };
  };

  const loadRows = useCallback(async () => {
    setLoading(true);
    try {
      setRows(await advancePaymentService.listForBranches(branchIds));
    } catch (e) {
      showToast(e instanceof FinancialError ? e.message : t('adv.load_error'), 'error');
    } finally {
      setLoading(false);
    }
  }, [branchIds, showToast, t]);

  useEffect(() => { loadRows(); }, [loadRows]);

  useEffect(() => {
    (async () => {
      const { data, error } = await supabase
        .from('payment_methods').select('*').eq('is_active', true).order('sort_order');
      if (error) showToast(t('adv.methods_error'), 'error');
      else setMethods((data as PaymentMethod[]) || []);
    })();
  }, [showToast, t]);

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between gap-4 flex-wrap">
        <h1 className="text-2xl font-bold text-slate-900">{t('adv.title')}</h1>
        <Button onClick={() => setShowCreate(true)}>
          <Plus size={16} /> {t('adv.new')}
        </Button>
      </div>

      <Card noPadding>
        {loading ? <LoadingPage /> : rows.length === 0 ? (
          <EmptyState icon={<HandCoins size={48} />} title={t('adv.no_data')} message={t('adv.no_data_desc')} />
        ) : (
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 bg-slate-50 text-slate-500">
                  <th className="text-left py-2 px-3">{t('adv.col_guest')}</th>
                  <th className="text-left py-2 px-3">{t('adv.col_checkin')}</th>
                  <th className="text-left py-2 px-3">{t('adv.col_method')}</th>
                  <th className="text-right py-2 px-3">{t('adv.col_amount')}</th>
                  <th className="text-left py-2 px-3">{t('adv.col_received')}</th>
                  <th className="text-left py-2 px-3">{t('adv.col_branch')}</th>
                  <th className="text-left py-2 px-3">{t('adv.col_status')}</th>
                  <th className="text-right py-2 px-3"></th>
                </tr>
              </thead>
              <tbody>
                {rows.map((r) => {
                  const sb = statusBadge(r.status);
                  return (
                    <tr key={r.id} className="border-b border-slate-100">
                      <td className="py-2 px-3 font-medium text-slate-800">{r.guest_name}</td>
                      <td className="py-2 px-3 text-slate-600">{formatDate(r.expected_check_in_date)}</td>
                      <td className="py-2 px-3 text-slate-600">{r.payment_method_code}</td>
                      <td className="py-2 px-3 text-right font-medium text-slate-800">{formatIDR(Number(r.amount))}</td>
                      <td className="py-2 px-3 text-xs text-slate-500">
                        {formatDateTime(r.created_at)}
                        <span className="block text-slate-400">{t('adv.business_date')} {formatDate(r.business_date)}</span>
                      </td>
                      <td className="py-2 px-3 text-slate-600">{branchNameById[r.branch_id] || '-'}</td>
                      <td className="py-2 px-3"><Badge color={sb.color}>{sb.label}</Badge></td>
                      <td className="py-2 px-3 text-right">
                        {canRefund && r.status === 'available' && (
                          <Button size="sm" variant="outline" onClick={() => setRefundTarget(r)}>{t('adv.cancel_refund')}</Button>
                        )}
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        )}
      </Card>

      {showCreate && (
        <CreateAdvancePaymentModal
          methods={methods}
          branches={branches}
          initialBranchId={selectedBranchId || branches[0]?.id || ''}
          onClose={() => setShowCreate(false)}
          onCreated={() => { setShowCreate(false); loadRows(); }}
        />
      )}

      {refundTarget && (
        <RefundAdvancePaymentModal
          advance={refundTarget}
          onClose={() => setRefundTarget(null)}
          onRefunded={() => { setRefundTarget(null); loadRows(); }}
        />
      )}
    </div>
  );
}

function CreateAdvancePaymentModal({ methods, branches, initialBranchId, onClose, onCreated }: {
  methods: PaymentMethod[];
  branches: { id: string; name: string }[];
  initialBranchId: string;
  onClose: () => void;
  onCreated: () => void;
}) {
  const { user } = useAuth();
  const { showToast } = useToast();
  const { t } = useI18n();
  const [branchId, setBranchId] = useState(initialBranchId);
  const [guestName, setGuestName] = useState('');
  const [checkIn, setCheckIn] = useState('');
  const [amount, setAmount] = useState('');
  const [methodId, setMethodId] = useState('');
  const [notes, setNotes] = useState('');
  const [saving, setSaving] = useState(false);
  const [errors, setErrors] = useState<Record<string, string>>({});

  const submit = async () => {
    const next: Record<string, string> = {};
    if (!branchId) next.branch = t('adv.select_branch');
    if (!guestName.trim()) next.guestName = t('adv.guest_name_req');
    if (!checkIn) next.checkIn = t('adv.checkin_req');
    else if (checkIn < todayISO()) next.checkIn = t('adv.checkin_future');
    const numericAmount = Number(amount);
    if (!numericAmount || numericAmount <= 0) next.amount = t('adv.amount_req');
    const method = methods.find((m) => m.id === methodId);
    if (!method) next.method = t('adv.method_req');
    if (!user) next.branch = t('adv.no_user');
    setErrors(next);
    if (Object.keys(next).length > 0 || !method || !user) return;

    setSaving(true);
    try {
      await advancePaymentService.create({
        orgId: user.organization_id,
        branchId,
        guestName,
        expectedCheckInDate: checkIn,
        amount: numericAmount,
        method,
        notes,
        userId: user.id,
      });
      showToast(t('adv.saved'), 'success');
      onCreated();
    } catch (e) {
      showToast(e instanceof FinancialError ? e.message : t('adv.save_error'), 'error');
    } finally {
      setSaving(false);
    }
  };

  return (
    <Modal open onClose={onClose} title={t('adv.create_title')} size="md"
      footer={
        <div className="flex justify-end gap-2">
          <Button variant="outline" onClick={onClose} disabled={saving}>{t('common.cancel')}</Button>
          <Button onClick={submit} loading={saving}>{t('common.save')}</Button>
        </div>
      }>
      <div className="space-y-4">
        <Select label={t('common.branch')} value={branchId} onChange={(e) => setBranchId(e.target.value)} error={errors.branch}>
          <option value="">{t('adv.select_branch')}</option>
          {branches.map((b) => <option key={b.id} value={b.id}>{b.name}</option>)}
        </Select>
        <Input label={t('adv.guest_name')} value={guestName} onChange={(e) => setGuestName(e.target.value)} error={errors.guestName} />
        <Input label={t('adv.expected_checkin')} type="date" min={todayISO()} value={checkIn} onChange={(e) => setCheckIn(e.target.value)} error={errors.checkIn} />
        <MoneyInput label={t('adv.amount_paid')} value={amount} onChange={setAmount} error={errors.amount} />
        <Select label={t('common.payment_method')} value={methodId} onChange={(e) => setMethodId(e.target.value)} error={errors.method}>
          <option value="">{t('adv.select_method')}</option>
          {methods.map((m) => <option key={m.id} value={m.id}>{m.name}</option>)}
        </Select>
        <Input label={t('adv.notes_optional')} value={notes} onChange={(e) => setNotes(e.target.value)} />
      </div>
    </Modal>
  );
}

function RefundAdvancePaymentModal({ advance, onClose, onRefunded }: {
  advance: AdvancePayment;
  onClose: () => void;
  onRefunded: () => void;
}) {
  const { user } = useAuth();
  const { showToast } = useToast();
  const { t } = useI18n();
  const [reason, setReason] = useState('');
  const [error, setError] = useState('');
  const [saving, setSaving] = useState(false);

  const submit = async () => {
    if (!reason.trim()) { setError(t('adv.refund_reason_req')); return; }
    if (!user) return;
    setSaving(true);
    try {
      await advancePaymentService.cancelAndRefund(advance, reason, user.id);
      showToast(t('adv.refunded'), 'success');
      onRefunded();
    } catch (e) {
      showToast(e instanceof FinancialError ? e.message : t('adv.refund_error'), 'error');
    } finally {
      setSaving(false);
    }
  };

  return (
    <Modal open onClose={onClose} title={t('adv.refund_title')} size="sm"
      footer={
        <div className="flex justify-end gap-2">
          <Button variant="outline" onClick={onClose} disabled={saving}>{t('common.back')}</Button>
          <Button variant="danger" onClick={submit} loading={saving}>{t('adv.refund_btn')} {formatIDR(Number(advance.amount))}</Button>
        </div>
      }>
      <div className="space-y-4">
        <p className="text-sm text-slate-600">
          {t('adv.refund_desc')}
        </p>
        <Input label={t('common.reason')} value={reason} onChange={(e) => { setReason(e.target.value); setError(''); }} error={error} />
      </div>
    </Modal>
  );
}
