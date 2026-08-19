import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal } from '@/components/ui/Modal';
import { Input, Select, Textarea } from '@/components/ui/Form';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatIDR, formatDateTime, todayISO } from '@/lib/format';
import { Plus, FileText, Search, ArrowRightLeft, TriangleAlert as AlertTriangle } from 'lucide-react';
import { getLockProvider } from '@/lib/hotel-lock/provider';
import type { Folio, FolioItem, Reservation, Guest, Room, ChargeCategory, PaymentMethod } from '@/types/database';

export function FolioPage({ searchQuery, reservationId }: { searchQuery?: string; reservationId?: string | null }) {
  const { user, branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [folios, setFolios] = useState<Folio[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedFolio, setSelectedFolio] = useState<Folio | null>(null);
  const [localSearch, setLocalSearch] = useState(searchQuery || '');

  const branchIds = selectedBranchId ? [selectedBranchId] : branches.map((b) => b.id);

  const load = useCallback(async () => {
    if (branchIds.length === 0) { setLoading(false); return; }
    setLoading(true);
    const { data } = await supabase.from('folios').select('*').in('branch_id', branchIds).order('created_at', { ascending: false }).limit(100);
    setFolios((data as Folio[]) || []);
    setLoading(false);
  }, [branchIds]);

  useEffect(() => { load(); }, [load]);

  useEffect(() => {
    if (reservationId) {
      (async () => {
        const { data } = await supabase.from('folios').select('*').eq('reservation_id', reservationId).maybeSingle();
        if (data) setSelectedFolio(data as Folio);
      })();
    }
  }, [reservationId]);

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-slate-900">{t('folio.title')}</h1>

      <div className="relative max-w-md">
        <Search size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
        <input type="text" value={localSearch} onChange={(e) => setLocalSearch(e.target.value)} placeholder={t('common.search')} className="w-full rounded-lg border border-slate-300 pl-10 pr-3 py-2 text-sm outline-none focus:ring-2 focus:ring-blue-500" />
      </div>

      {folios.length === 0 ? (
        <EmptyState icon={<FileText size={48} />} title={t('common.no_data')} />
      ) : (
        <Card noPadding>
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 text-slate-500">
                  <th className="text-left py-3 px-4">{t('folio.folio_number')}</th>
                  <th className="text-left py-3 px-4">{t('common.balance')}</th>
                  <th className="text-center py-3 px-4">{t('common.status')}</th>
                  <th className="text-right py-3 px-4">{t('common.actions')}</th>
                </tr>
              </thead>
              <tbody>
                {folios.filter((f) => !localSearch || f.folio_number.toLowerCase().includes(localSearch.toLowerCase())).map((f) => (
                  <tr key={f.id} className="border-b border-slate-100 hover:bg-slate-50 cursor-pointer" onClick={() => setSelectedFolio(f)}>
                    <td className="py-3 px-4 font-medium text-blue-600">{f.folio_number}</td>
                    <td className="py-3 px-4">{formatIDR(f.balance)}</td>
                    <td className="text-center py-3 px-4"><Badge color={f.status === 'open' ? 'blue' : f.status === 'finalized' ? 'gray' : 'red'}>{f.status}</Badge></td>
                    <td className="text-right py-3 px-4"><button className="text-blue-600 text-xs font-medium">{t('common.view')}</button></td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </Card>
      )}

      {selectedFolio && <FolioDetailModal folio={selectedFolio} onClose={() => { setSelectedFolio(null); load(); }} />}
    </div>
  );
}

function FolioDetailModal({ folio, onClose }: { folio: Folio; onClose: () => void }) {
  const { user, branches } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [items, setItems] = useState<FolioItem[]>([]);
  const [reservation, setReservation] = useState<Reservation | null>(null);
  const [guest, setGuest] = useState<Guest | null>(null);
  const [room, setRoom] = useState<Room | null>(null);
  const [chargeCats, setChargeCats] = useState<ChargeCategory[]>([]);
  const [paymentMethods, setPaymentMethods] = useState<PaymentMethod[]>([]);
  const [loading, setLoading] = useState(true);
  const [showAddCharge, setShowAddCharge] = useState(false);
  const [showTakePayment, setShowTakePayment] = useState(false);
  const [showTransfer, setShowTransfer] = useState(false);
  const [showPostStay, setShowPostStay] = useState(false);

  const isFinalized = folio.status === 'finalized';

  useEffect(() => {
    (async () => {
      setLoading(true);
      const [{ data: fi }, { data: resData }, { data: g }, { data: cc }, { data: pm }] = await Promise.all([
        supabase.from('folio_items').select('*').eq('folio_id', folio.id).order('created_at'),
        supabase.from('reservations').select('*').eq('id', folio.reservation_id).maybeSingle(),
        supabase.from('guests').select('*').eq('id', folio.guest_id).maybeSingle(),
        supabase.from('charge_categories').select('*').eq('is_active', true).order('sort_order'),
        supabase.from('payment_methods').select('*').eq('is_active', true).order('sort_order'),
      ]);
      const resRecord = resData as Reservation | null;
      const { data: r } = resRecord?.room_id
        ? await supabase.from('rooms').select('*').eq('id', resRecord.room_id).maybeSingle()
        : { data: null };
      setItems((fi as FolioItem[]) || []);
      setReservation(resRecord);
      setGuest(g as Guest);
      setRoom(r as Room);
      setChargeCats((cc as ChargeCategory[]) || []);
      setPaymentMethods((pm as PaymentMethod[]) || []);
      setLoading(false);
    })();
  }, [folio]);

  const charges = items.filter((i) => i.item_type === 'charge' && !i.voided && i.amount > 0);
  const payments = items.filter((i) => i.item_type === 'payment' && !i.voided);
  const discounts = items.filter((i) => i.item_type === 'discount' && !i.voided);
  const taxes = items.filter((i) => i.item_type === 'tax' && !i.voided);
  const totalCharges = charges.reduce((s, i) => s + i.amount, 0);
  const totalPayments = payments.reduce((s, i) => s + Math.abs(i.amount), 0);
  const totalDiscounts = discounts.reduce((s, i) => s + Math.abs(i.amount), 0);
  const totalTax = taxes.reduce((s, i) => s + i.amount, 0);
  const netBalance = totalCharges + totalTax - totalDiscounts - totalPayments;

  const voidItem = async (item: FolioItem) => {
    const { error } = await supabase.from('folio_items').update({ voided: true, voided_by: user!.id, voided_at: new Date().toISOString() }).eq('id', item.id);
    if (error) { showToast(error.message, 'error'); return; }
    await supabase.from('audit_logs').insert({
      organization_id: user!.organization_id, branch_id: folio.branch_id, user_id: user!.id,
      action: 'charge_voided', object_type: 'folio_item', object_id: item.id,
      previous_value: { description: item.description, amount: item.amount },
    });
    showToast('Item voided', 'success');
    // Reload
    const { data } = await supabase.from('folio_items').select('*').eq('folio_id', folio.id).order('created_at');
    setItems((data as FolioItem[]) || []);
  };

  if (loading) return <Modal open onClose={onClose} title={t('folio.title')}><LoadingPage /></Modal>;

  return (
    <Modal open onClose={onClose} title={`${t('folio.title')} — ${folio.folio_number}`} size="xl">
      <div className="space-y-4">
        <div className="grid grid-cols-2 md:grid-cols-4 gap-3 text-sm">
          <div><span className="text-slate-500">{t('common.guest')}:</span> <span className="font-medium">{guest?.full_name || '-'}</span></div>
          <div><span className="text-slate-500">{t('common.room')}:</span> <span className="font-medium">{room?.room_number || '-'}</span></div>
          <div><span className="text-slate-500">{t('common.reservation')}:</span> <span className="font-medium">{reservation?.reservation_number || '-'}</span></div>
          <div><span className="text-slate-500">{t('common.status')}:</span> <Badge color={folio.status === 'open' ? 'blue' : 'gray'}>{folio.status}</Badge></div>
        </div>

        {isFinalized && (
          <div className="bg-amber-50 border border-amber-200 rounded-lg p-3 flex items-center gap-2 text-amber-700 text-sm">
            <AlertTriangle size={16} /> This folio is finalized. Use post-stay charges or adjustments instead of editing.
          </div>
        )}

        {/* Transaction history */}
        <div className="border border-slate-200 rounded-lg overflow-hidden">
          <table className="w-full text-sm">
            <thead>
              <tr className="border-b border-slate-200 bg-slate-50 text-slate-500">
                <th className="text-left py-2 px-3">{t('common.description')}</th>
                <th className="text-left py-2 px-3">{t('common.category')}</th>
                <th className="text-center py-2 px-3">Type</th>
                <th className="text-right py-2 px-3">{t('common.amount')}</th>
                <th className="text-left py-2 px-3">{t('common.created_at')}</th>
                {!isFinalized && <th className="text-right py-2 px-3"></th>}
              </tr>
            </thead>
            <tbody>
              {items.map((item) => (
                <tr key={item.id} className={`border-b border-slate-100 ${item.voided ? 'opacity-40 line-through' : ''}`}>
                  <td className="py-2 px-3">{item.description}{item.is_post_stay && <span className="ml-2 text-xs text-amber-600 font-medium">POST-STAY</span>}</td>
                  <td className="py-2 px-3">{item.category || '-'}</td>
                  <td className="text-center py-2 px-3"><Badge color={item.item_type === 'charge' ? 'red' : item.item_type === 'payment' ? 'green' : 'gray'}>{item.item_type}</Badge></td>
                  <td className={`text-right py-2 px-3 font-medium ${item.amount > 0 ? 'text-red-600' : 'text-emerald-600'}`}>{item.amount > 0 ? '+' : ''}{formatIDR(item.amount)}</td>
                  <td className="py-2 px-3 text-xs text-slate-400">{formatDateTime(item.created_at)}</td>
                  {!isFinalized && !item.voided && <td className="text-right py-2 px-3"><button onClick={() => voidItem(item)} className="text-xs text-red-500 hover:text-red-700">Void</button></td>}
                </tr>
              ))}
              {items.length === 0 && <tr><td colSpan={6} className="text-center py-4 text-slate-400">{t('common.no_data')}</td></tr>}
            </tbody>
            <tfoot>
              <tr className="bg-slate-50 font-bold">
                <td colSpan={3} className="py-2 px-3">{t('folio.total_charges')}</td>
                <td className="text-right py-2 px-3 text-red-600">{formatIDR(totalCharges + totalTax)}</td>
                <td colSpan={2}></td>
              </tr>
              <tr className="bg-slate-50 font-bold">
                <td colSpan={3} className="py-2 px-3">{t('folio.total_payments')}</td>
                <td className="text-right py-2 px-3 text-emerald-600">{formatIDR(totalPayments)}</td>
                <td colSpan={2}></td>
              </tr>
              <tr className="bg-slate-100 font-bold">
                <td colSpan={3} className="py-2 px-3">{t('folio.net_balance')}</td>
                <td className={`text-right py-2 px-3 ${netBalance > 0 ? 'text-red-600' : 'text-emerald-600'}`}>{formatIDR(Math.abs(netBalance))}</td>
                <td colSpan={2}></td>
              </tr>
            </tfoot>
          </table>
        </div>

        {!isFinalized && (
          <div className="flex flex-wrap gap-2">
            <Button size="sm" onClick={() => setShowAddCharge(true)}><Plus size={14} /> {t('folio.add_charge')}</Button>
            <Button size="sm" variant="success" onClick={() => setShowTakePayment(true)}><Plus size={14} /> {t('folio.take_payment')}</Button>
            <Button size="sm" variant="outline" onClick={() => setShowTransfer(true)}><ArrowRightLeft size={14} /> {t('res.transfer_room')}</Button>
          </div>
        )}
        {isFinalized && (
          <Button size="sm" variant="warning" onClick={() => setShowPostStay(true)}><Plus size={14} /> {t('folio.post_stay_charge')}</Button>
        )}
      </div>

      {showAddCharge && <AddChargeModal folio={folio} reservation={reservation} room={room} chargeCats={chargeCats} userId={user!.id} orgId={user!.organization_id} onClose={() => setShowAddCharge(false)} onSaved={async () => { setShowAddCharge(false); const { data } = await supabase.from('folio_items').select('*').eq('folio_id', folio.id).order('created_at'); setItems((data as FolioItem[]) || []); }} />}
      {showTakePayment && <TakePaymentModal folio={folio} reservation={reservation} paymentMethods={paymentMethods} userId={user!.id} orgId={user!.organization_id} onClose={() => setShowTakePayment(false)} onSaved={async () => { setShowTakePayment(false); const { data } = await supabase.from('folio_items').select('*').eq('folio_id', folio.id).order('created_at'); setItems((data as FolioItem[]) || []); }} />}
      {showTransfer && <RoomTransferModal folio={folio} reservation={reservation} currentRoom={room} userId={user!.id} orgId={user!.organization_id} branchId={folio.branch_id} onClose={() => setShowTransfer(false)} onSaved={onClose} />}
      {showPostStay && <PostStayChargeModal folio={folio} reservation={reservation} room={room} chargeCats={chargeCats} userId={user!.id} orgId={user!.organization_id} onClose={() => setShowPostStay(false)} onSaved={async () => { setShowPostStay(false); const { data } = await supabase.from('folio_items').select('*').eq('folio_id', folio.id).order('created_at'); setItems((data as FolioItem[]) || []); }} />}
    </Modal>
  );
}

function AddChargeModal({ folio, reservation, room, chargeCats, userId, orgId, onClose, onSaved }: {
  folio: Folio; reservation: Reservation | null; room: Room | null; chargeCats: ChargeCategory[];
  userId: string; orgId: string; onClose: () => void; onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [form, setForm] = useState({ category_id: '', description: '', amount: '0', quantity: '1', notes: '' });

  const handleSubmit = async () => {
    const cat = chargeCats.find((c) => c.id === form.category_id);
    if (!form.description || parseFloat(form.amount) <= 0) { showToast('Description and amount required', 'error'); return; }
    setSaving(true);
    const amount = parseFloat(form.amount) * parseFloat(form.quantity);
    const needsApproval = cat?.requires_approval && amount > (cat?.approval_threshold || 0);
    const payload = {
      folio_id: folio.id, branch_id: folio.branch_id, reservation_id: folio.reservation_id,
      guest_id: folio.guest_id, room_id: room?.id || reservation?.room_id || null,
      item_type: 'charge', category: cat?.code || 'miscellaneous', description: form.description,
      quantity: parseFloat(form.quantity), unit_amount: parseFloat(form.amount), amount,
      business_date: todayISO(), created_by: userId, notes: form.notes || null,
      approved_by: needsApproval ? null : userId,
    };
    const { error } = await supabase.from('folio_items').insert(payload);
    if (error) { showToast(error.message, 'error'); setSaving(false); return; }
    await supabase.from('transactions').insert({
      branch_id: folio.branch_id, organization_id: orgId, reservation_id: folio.reservation_id,
      guest_id: folio.guest_id, folio_id: folio.id, transaction_type: 'additional_charge',
      description: form.description, amount, debit_credit: 'debit', business_date: todayISO(), created_by: user_id_or_null(userId),
    });
    await supabase.from('audit_logs').insert({
      organization_id: orgId, branch_id: folio.branch_id, user_id: userId,
      action: cat?.is_damage ? 'damage_charge' : 'additional_charge', object_type: 'folio', object_id: folio.id,
      new_value: { description: form.description, amount, category: cat?.code },
    });
    showToast('Charge added', 'success');
    setSaving(false);
    onSaved();
  };

  return (
    <Modal open onClose={onClose} title={t('folio.add_charge')} size="md"
      footer={<><Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button><Button loading={saving} onClick={handleSubmit}>{t('common.save')}</Button></>}>
      <form className="space-y-4">
        <Select label={t('common.category')} value={form.category_id} onChange={(e) => setForm({ ...form, category_id: e.target.value })}>
          <option value="">--</option>
          {chargeCats.map((c) => <option key={c.id} value={c.id}>{c.name}{c.is_damage ? ' (Damage)' : ''}</option>)}
        </Select>
        <Input label={t('common.description')} value={form.description} onChange={(e) => setForm({ ...form, description: e.target.value })} required />
        <div className="grid grid-cols-2 gap-4">
          <Input label={t('common.amount')} type="number" value={form.amount} onChange={(e) => setForm({ ...form, amount: e.target.value })} />
          <Input label={t('common.quantity')} type="number" value={form.quantity} onChange={(e) => setForm({ ...form, quantity: e.target.value })} />
        </div>
        <Textarea label={t('common.notes')} value={form.notes} onChange={(e) => setForm({ ...form, notes: e.target.value })} rows={2} />
      </form>
    </Modal>
  );
}

function TakePaymentModal({ folio, reservation, paymentMethods, userId, orgId, onClose, onSaved }: {
  folio: Folio; reservation: Reservation | null; paymentMethods: PaymentMethod[];
  userId: string; orgId: string; onClose: () => void; onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [form, setForm] = useState({ method_id: '', subtype: '', edc_terminal: '', reference_number: '', approval_code: '', amount: '0', notes: '' });

  const selectedMethod = paymentMethods.find((m) => m.id === form.method_id);

  const handleSubmit = async () => {
    if (!form.method_id || parseFloat(form.amount) <= 0) { showToast('Method and amount required', 'error'); return; }
    setSaving(true);
    const amount = parseFloat(form.amount);
    const method = selectedMethod!;
    const payNum = `PAY-${Date.now().toString().slice(-8)}`;
    // Insert payment record
    const { error: payErr } = await supabase.from('payments').insert({
      branch_id: folio.branch_id, reservation_id: folio.reservation_id, folio_id: folio.id,
      guest_id: folio.guest_id, payment_number: payNum, amount,
      payment_method_id: method.id, payment_method_code: method.code,
      payment_subtype: form.subtype || null, edc_terminal: form.edc_terminal || null,
      reference_number: form.reference_number || null, approval_code: form.approval_code || null,
      is_ota: method.is_ota, business_date: todayISO(), created_by: userId, notes: form.notes || null,
    });
    if (payErr) { showToast(payErr.message, 'error'); setSaving(false); return; }
    // Insert folio item (negative for payment)
    await supabase.from('folio_items').insert({
      folio_id: folio.id, branch_id: folio.branch_id, reservation_id: folio.reservation_id,
      guest_id: folio.guest_id, item_type: 'payment', category: method.code,
      description: `Payment: ${method.name}${form.subtype ? ` (${form.subtype})` : ''}`,
      quantity: 1, unit_amount: -amount, amount: -amount, business_date: todayISO(), created_by: userId,
      notes: form.notes || null,
    });
    // Ledger
    await supabase.from('transactions').insert({
      branch_id: folio.branch_id, organization_id: orgId, reservation_id: folio.reservation_id,
      guest_id: folio.guest_id, folio_id: folio.id, transaction_type: 'payment',
      description: `Payment ${method.name}${form.subtype ? ` ${form.subtype}` : ''}`, amount,
      debit_credit: 'credit', payment_method_code: method.code,
      reference_number: form.reference_number || null, business_date: todayISO(), created_by: userId,
    });
    await supabase.from('audit_logs').insert({
      organization_id: orgId, branch_id: folio.branch_id, user_id: userId,
      action: 'payment', object_type: 'folio', object_id: folio.id,
      new_value: { amount, method: method.code, subtype: form.subtype },
    });
    showToast('Payment recorded', 'success');
    setSaving(false);
    onSaved();
  };

  return (
    <Modal open onClose={onClose} title={t('payment.record_payment')} size="md"
      footer={<><Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button><Button loading={saving} variant="success" onClick={handleSubmit}>{t('common.save')}</Button></>}>
      <form className="space-y-4">
        <Select label={t('common.payment_method')} value={form.method_id} onChange={(e) => setForm({ ...form, method_id: e.target.value, subtype: '' })} required>
          <option value="">--</option>
          {paymentMethods.map((m) => <option key={m.id} value={m.id}>{m.name}</option>)}
        </Select>
        {selectedMethod?.is_edc && (
          <Select label="EDC Subtype" value={form.subtype} onChange={(e) => setForm({ ...form, subtype: e.target.value })}>
            <option value="">--</option>
            <option value="debit">{t('payment.edc_debit')}</option>
            <option value="credit">{t('payment.edc_credit')}</option>
            <option value="qris">{t('payment.edc_qris')}</option>
          </Select>
        )}
        {selectedMethod?.is_edc && (
          <Input label={t('common.edc_terminal')} value={form.edc_terminal} onChange={(e) => setForm({ ...form, edc_terminal: e.target.value })} />
        )}
        <Input label={t('common.amount')} type="number" value={form.amount} onChange={(e) => setForm({ ...form, amount: e.target.value })} required />
        <Input label={t('common.reference_number')} value={form.reference_number} onChange={(e) => setForm({ ...form, reference_number: e.target.value })} />
        {selectedMethod?.is_edc && <Input label={t('common.approval_code')} value={form.approval_code} onChange={(e) => setForm({ ...form, approval_code: e.target.value })} />}
        <Textarea label={t('common.notes')} value={form.notes} onChange={(e) => setForm({ ...form, notes: e.target.value })} rows={2} />
      </form>
    </Modal>
  );
}

function RoomTransferModal({ folio, reservation, currentRoom, userId, orgId, branchId, onClose, onSaved }: {
  folio: Folio; reservation: Reservation | null; currentRoom: Room | null;
  userId: string; orgId: string; branchId: string;
  onClose: () => void; onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [rooms, setRooms] = useState<Room[]>([]);
  const [toRoomId, setToRoomId] = useState('');
  const [reason, setReason] = useState('');

  useEffect(() => {
    supabase.from('rooms').select('*').eq('branch_id', branchId).eq('is_active', true).in('status', ['available', 'dirty', 'inspected', 'cleaning']).order('room_number').then(({ data }) => setRooms((data as Room[]) || []));
  }, [branchId]);

  const handleSubmit = async () => {
    if (!toRoomId) { showToast('Select a room', 'error'); return; }
    setSaving(true);
    // Update reservation
    await supabase.from('reservations').update({ room_id: toRoomId }).eq('id', folio.reservation_id);
    // Old room -> dirty, new room -> occupied
    if (currentRoom) await supabase.from('rooms').update({ status: 'dirty' }).eq('id', currentRoom.id);
    await supabase.from('rooms').update({ status: 'occupied' }).eq('id', toRoomId);
    // Record transfer
    await supabase.from('room_transfers').insert({
      reservation_id: folio.reservation_id, from_room_id: currentRoom?.id || null,
      to_room_id: toRoomId, reason: reason || null, performed_by: userId,
    });
    await supabase.from('audit_logs').insert({
      organization_id: orgId, branch_id: branchId, user_id: userId,
      action: 'room_transfer', object_type: 'reservation', object_id: folio.reservation_id,
      previous_value: { room: currentRoom?.room_number }, new_value: { room: rooms.find((r) => r.id === toRoomId)?.room_number },
    });
    // Invalidate old card
    const provider = getLockProvider();
    await provider.invalidateGuestCard({ cardId: folio.reservation_id });
    showToast('Room transferred', 'success');
    setSaving(false);
    onSaved();
  };

  return (
    <Modal open onClose={onClose} title={t('res.transfer_room')} size="md"
      footer={<><Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button><Button loading={saving} onClick={handleSubmit}>{t('common.confirm')}</Button></>}>
      <div className="space-y-4">
        <div className="text-sm text-slate-600">{t('common.room')}: <span className="font-medium">{currentRoom?.room_number || '-'}</span> → <span className="text-blue-600 font-medium">{rooms.find((r) => r.id === toRoomId)?.room_number || '?'}</span></div>
        <Select label="New Room" value={toRoomId} onChange={(e) => setToRoomId(e.target.value)} required>
          <option value="">--</option>
          {rooms.map((r) => <option key={r.id} value={r.id}>{r.room_number} ({t(`room.${r.status}`)})</option>)}
        </Select>
        <Textarea label={t('common.reason')} value={reason} onChange={(e) => setReason(e.target.value)} rows={2} />
      </div>
    </Modal>
  );
}

function PostStayChargeModal({ folio, reservation, room, chargeCats, userId, orgId, onClose, onSaved }: {
  folio: Folio; reservation: Reservation | null; room: Room | null; chargeCats: ChargeCategory[];
  userId: string; orgId: string; onClose: () => void; onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [form, setForm] = useState({ category_id: '', description: '', amount: '0', notes: '' });

  const handleSubmit = async () => {
    const cat = chargeCats.find((c) => c.id === form.category_id);
    if (!form.description || parseFloat(form.amount) <= 0) { showToast('Description and amount required', 'error'); return; }
    setSaving(true);
    const amount = parseFloat(form.amount);
    await supabase.from('folio_items').insert({
      folio_id: folio.id, branch_id: folio.branch_id, reservation_id: folio.reservation_id,
      guest_id: folio.guest_id, room_id: room?.id || reservation?.room_id || null,
      item_type: 'charge', category: cat?.code || 'post_stay', description: `POST-STAY: ${form.description}`,
      quantity: 1, unit_amount: amount, amount, business_date: todayISO(),
      is_post_stay: true, created_by: userId, notes: form.notes || null,
    });
    await supabase.from('additional_charges').insert({
      branch_id: folio.branch_id, reservation_id: folio.reservation_id, folio_id: folio.id,
      guest_id: folio.guest_id, room_id: room?.id || reservation?.room_id || null,
      charge_category_id: cat?.id || null, category_code: cat?.code || 'post_stay',
      description: form.description, amount, quantity: 1, is_post_stay: true,
      status: 'posted', business_date: todayISO(), created_by: userId, notes: form.notes || null,
    });
    await supabase.from('transactions').insert({
      branch_id: folio.branch_id, organization_id: orgId, reservation_id: folio.reservation_id,
      guest_id: folio.guest_id, folio_id: folio.id, transaction_type: 'post_stay_charge',
      description: `Post-stay: ${form.description}`, amount, debit_credit: 'debit',
      business_date: todayISO(), created_by: userId,
    });
    await supabase.from('audit_logs').insert({
      organization_id: orgId, branch_id: folio.branch_id, user_id: userId,
      action: 'post_stay_charge', object_type: 'folio', object_id: folio.id,
      new_value: { description: form.description, amount },
    });
    showToast('Post-stay charge added', 'success');
    setSaving(false);
    onSaved();
  };

  return (
    <Modal open onClose={onClose} title={t('folio.post_stay_charge')} size="md"
      footer={<><Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button><Button loading={saving} variant="warning" onClick={handleSubmit}>{t('common.save')}</Button></>}>
      <div className="space-y-4">
        <div className="bg-amber-50 border border-amber-200 rounded-lg p-3 text-sm text-amber-700">This charge will be added as a post-stay additional charge. The original finalized invoice will not be modified.</div>
        <Select label={t('common.category')} value={form.category_id} onChange={(e) => setForm({ ...form, category_id: e.target.value })}>
          <option value="">--</option>
          {chargeCats.map((c) => <option key={c.id} value={c.id}>{c.name}</option>)}
        </Select>
        <Input label={t('common.description')} value={form.description} onChange={(e) => setForm({ ...form, description: e.target.value })} required />
        <Input label={t('common.amount')} type="number" value={form.amount} onChange={(e) => setForm({ ...form, amount: e.target.value })} required />
        <Textarea label={t('common.notes')} value={form.notes} onChange={(e) => setForm({ ...form, notes: e.target.value })} rows={2} />
      </div>
    </Modal>
  );
}

function user_id_or_null(id: string): string {
  return id;
}
