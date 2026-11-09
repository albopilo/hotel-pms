import { useState, useEffect, useCallback, useMemo } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useToast } from '@/lib/toast';
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

const STATUS_BADGE: Record<AdvancePaymentStatus, { color: 'green' | 'blue' | 'gray'; label: string }> = {
  available: { color: 'green', label: 'Available' },
  claimed: { color: 'blue', label: 'Claimed' },
  cancelled: { color: 'gray', label: 'Cancelled (refunded)' },
};

export function AdvancePaymentsPage() {
  const { user, branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { showToast } = useToast();

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

  const loadRows = useCallback(async () => {
    setLoading(true);
    try {
      setRows(await advancePaymentService.listForBranches(branchIds));
    } catch (e) {
      showToast(e instanceof FinancialError ? e.message : 'Could not load advance payments', 'error');
    } finally {
      setLoading(false);
    }
  }, [branchIds, showToast]);

  useEffect(() => { loadRows(); }, [loadRows]);

  useEffect(() => {
    (async () => {
      const { data, error } = await supabase
        .from('payment_methods').select('*').eq('is_active', true).order('sort_order');
      if (error) showToast('Could not load payment methods', 'error');
      else setMethods((data as PaymentMethod[]) || []);
    })();
  }, [showToast]);

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between gap-4 flex-wrap">
        <h1 className="text-2xl font-bold text-slate-900">Advance Payments</h1>
        <Button onClick={() => setShowCreate(true)}>
          <Plus size={16} /> New Advance Payment
        </Button>
      </div>

      <Card noPadding>
        {loading ? <LoadingPage /> : rows.length === 0 ? (
          <EmptyState icon={<HandCoins size={48} />} title="No advance payments yet" message="Record money a guest paid ahead of a future stay." />
        ) : (
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 bg-slate-50 text-slate-500">
                  <th className="text-left py-2 px-3">Guest Name</th>
                  <th className="text-left py-2 px-3">Expected Check-in</th>
                  <th className="text-left py-2 px-3">Method</th>
                  <th className="text-right py-2 px-3">Amount</th>
                  <th className="text-left py-2 px-3">Received</th>
                  <th className="text-left py-2 px-3">Branch</th>
                  <th className="text-left py-2 px-3">Status</th>
                  <th className="text-right py-2 px-3"></th>
                </tr>
              </thead>
              <tbody>
                {rows.map((r) => (
                  <tr key={r.id} className="border-b border-slate-100">
                    <td className="py-2 px-3 font-medium text-slate-800">{r.guest_name}</td>
                    <td className="py-2 px-3 text-slate-600">{formatDate(r.expected_check_in_date)}</td>
                    <td className="py-2 px-3 text-slate-600">{r.payment_method_code}</td>
                    <td className="py-2 px-3 text-right font-medium text-slate-800">{formatIDR(Number(r.amount))}</td>
                    <td className="py-2 px-3 text-xs text-slate-500">
                      {formatDateTime(r.created_at)}
                      <span className="block text-slate-400">Business date {formatDate(r.business_date)}</span>
                    </td>
                    <td className="py-2 px-3 text-slate-600">{branchNameById[r.branch_id] || '-'}</td>
                    <td className="py-2 px-3"><Badge color={STATUS_BADGE[r.status].color}>{STATUS_BADGE[r.status].label}</Badge></td>
                    <td className="py-2 px-3 text-right">
                      {canRefund && r.status === 'available' && (
                        <Button size="sm" variant="outline" onClick={() => setRefundTarget(r)}>Cancel &amp; refund</Button>
                      )}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </Card>

      {showCreate && (
        <CreateAdvancePaymentModal
          methods={methods}
          branchId={selectedBranchId || branches[0]?.id || ''}
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

function CreateAdvancePaymentModal({ methods, branchId, onClose, onCreated }: {
  methods: PaymentMethod[];
  branchId: string;
  onClose: () => void;
  onCreated: () => void;
}) {
  const { user, branches } = useAuth();
  const { showToast } = useToast();
  const [guestName, setGuestName] = useState('');
  const [checkIn, setCheckIn] = useState('');
  const [amount, setAmount] = useState('');
  const [methodId, setMethodId] = useState('');
  const [notes, setNotes] = useState('');
  const [saving, setSaving] = useState(false);
  const [errors, setErrors] = useState<Record<string, string>>({});

  const submit = async () => {
    const next: Record<string, string> = {};
    if (!guestName.trim()) next.guestName = 'Guest name is required';
    if (!checkIn) next.checkIn = 'Expected check-in date is required';
    else if (checkIn < todayISO()) next.checkIn = 'Check-in date must be today or later';
    const numericAmount = Number(amount);
    if (!numericAmount || numericAmount <= 0) next.amount = 'Amount must be greater than zero';
    const method = methods.find((m) => m.id === methodId);
    if (!method) next.method = 'Select a payment method';
    if (!branchId || !user) next.branch = 'No branch selected';
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
      showToast('Advance payment recorded', 'success');
      onCreated();
    } catch (e) {
      showToast(e instanceof FinancialError ? e.message : 'Could not save advance payment', 'error');
    } finally {
      setSaving(false);
    }
  };

  return (
    <Modal open onClose={onClose} title="New Advance Payment" size="md"
      footer={
        <div className="flex justify-end gap-2">
          <Button variant="outline" onClick={onClose} disabled={saving}>Cancel</Button>
          <Button onClick={submit} loading={saving}>Save</Button>
        </div>
      }>
      <div className="space-y-4">
        <Input label="Guest name" value={guestName} onChange={(e) => setGuestName(e.target.value)} error={errors.guestName} />
        <Input label="Expected check-in date" type="date" min={todayISO()} value={checkIn} onChange={(e) => setCheckIn(e.target.value)} error={errors.checkIn} />
        <MoneyInput label="Amount paid" value={amount} onChange={setAmount} error={errors.amount} />
        <Select label="Payment method" value={methodId} onChange={(e) => setMethodId(e.target.value)} error={errors.method}>
          <option value="">Select method</option>
          {methods.map((m) => <option key={m.id} value={m.id}>{m.name}</option>)}
        </Select>
        <Input label="Notes (optional)" value={notes} onChange={(e) => setNotes(e.target.value)} />
        {errors.branch && <p className="text-xs text-red-500">{errors.branch}</p>}
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
  const [reason, setReason] = useState('');
  const [error, setError] = useState('');
  const [saving, setSaving] = useState(false);

  const submit = async () => {
    if (!reason.trim()) { setError('A reason is required'); return; }
    if (!user) return;
    setSaving(true);
    try {
      await advancePaymentService.cancelAndRefund(advance, reason, user.id);
      showToast('Advance payment cancelled and refunded', 'success');
      onRefunded();
    } catch (e) {
      showToast(e instanceof FinancialError ? e.message : 'Could not cancel advance payment', 'error');
    } finally {
      setSaving(false);
    }
  };

  return (
    <Modal open onClose={onClose} title="Cancel & refund advance payment" size="sm"
      footer={
        <div className="flex justify-end gap-2">
          <Button variant="outline" onClick={onClose} disabled={saving}>Back</Button>
          <Button variant="danger" onClick={submit} loading={saving}>Refund {formatIDR(Number(advance.amount))}</Button>
        </div>
      }>
      <div className="space-y-4">
        <p className="text-sm text-slate-600">
          The refund is counted in the business day it is made. This cannot be undone.
        </p>
        <Input label="Reason" value={reason} onChange={(e) => { setReason(e.target.value); setError(''); }} error={error} />
      </div>
    </Modal>
  );
}
