import { useState, useEffect, useCallback, useMemo, useRef } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal } from '@/components/ui/Modal';
import { Input, Select, Textarea, MoneyInput } from '@/components/ui/Form';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { Pagination } from '@/components/ui/Pagination';
import { formatIDR, formatDateTime, formatDate, formatTime } from '@/lib/format';
import { Plus, FileText, Search, ArrowRightLeft, TriangleAlert as AlertTriangle, Receipt, User as UserIcon } from 'lucide-react';
import { openPrintTab } from '@/lib/printRoute';
import { ConfirmModal } from '@/components/ui/Modal';
import { getLockProviderByType, integrationToConfig } from '@/lib/hotel-lock/provider';
import { folioService, paymentService, chargeService, FinancialError } from '@/services/financial';
import { parseDbError } from '@/lib/error-handler';
import { saveDraft, loadDraft, clearDraft } from '@/lib/formDraft';
import type { Folio, FolioItem, Reservation, Guest, Room, ChargeCategory, PaymentMethod, BookingSource, RoomType, ReservationRoom, HotelLockIntegration } from '@/types/database';

const ADD_CHARGE_DRAFT_KEY = 'folio_add_charge_draft';
const TAKE_PAYMENT_DRAFT_KEY = 'folio_take_payment_draft';
const POST_STAY_DRAFT_KEY = 'folio_post_stay_draft';
const ROOM_TRANSFER_DRAFT_KEY = 'folio_room_transfer_draft';

type FolioListRow = Folio & {
  guest?: Pick<Guest, 'full_name'> | null;
  reservation?: { room?: Pick<Room, 'room_number'> | null } | null;
};

export function FolioPage({ searchQuery, reservationId, onNavigateToInvoice, onSelectReservation, onNavigateToGuest }: { searchQuery?: string; reservationId?: string | null; onNavigateToInvoice?: (id: string) => void; onSelectReservation?: (id: string) => void; onNavigateToGuest?: (id: string) => void; }) {
  const { user, branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();
  const [folios, setFolios] = useState<FolioListRow[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedFolio, setSelectedFolio] = useState<Folio | null>(null);
  const [localSearch, setLocalSearch] = useState(searchQuery || '');
  const [page, setPage] = useState(1);
  const PAGE_SIZE = 20;
  const processedResId = useRef<string | null>(null);

  const branchIds = useMemo(
  () => selectedBranchId ? [selectedBranchId] : branches.map((b) => b.id),
  [selectedBranchId, branches]
);

  const load = useCallback(async () => {
    if (branchIds.length === 0) { setLoading(false); return; }
    setLoading(true);
    const { data, error } = await supabase.from('folios').select('*, guest:guests(full_name), reservation:reservations(room:rooms(room_number))').in('branch_id', branchIds).order('created_at', { ascending: false });
    if (error) { setLoading(false); return; }
    setFolios((data as FolioListRow[]) || []);
    setLoading(false);
  }, [branchIds]);

  useEffect(() => { load(); }, [load]);

  useEffect(() => {
    if (!reservationId) return;
    if (processedResId.current === reservationId) return;
    processedResId.current = reservationId;
    (async () => {
      const { data } = await supabase.from('folios').select('*').eq('reservation_id', reservationId).maybeSingle();
      if (data) setSelectedFolio(data as Folio);
    })();
  }, [reservationId]);

  const filteredFolios = folios.filter((f) => {
    const q = localSearch.toLowerCase();
    return !q || f.folio_number.toLowerCase().includes(q) || (f.guest?.full_name || '').toLowerCase().includes(q) || (f.reservation?.room?.room_number || '').toLowerCase().includes(q);
  });
  const pagedFolios = filteredFolios.slice((page - 1) * PAGE_SIZE, page * PAGE_SIZE);
  useEffect(() => { setPage(1); }, [localSearch]);

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-slate-900">{t('nav.payments')}</h1>

      <div className="relative max-w-md">
        <Search size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
        <input type="text" value={localSearch} onChange={(e) => setLocalSearch(e.target.value)} placeholder={t('common.search')} className="w-full rounded-lg border border-slate-300 pl-10 pr-3 py-2 text-sm outline-none focus:ring-2 focus:ring-blue-500" />
      </div>

      {filteredFolios.length === 0 ? (
        <EmptyState icon={<FileText size={48} />} title={t('common.no_data')} />
      ) : (
        <Card noPadding>
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 text-slate-500">
                  <th className="text-left py-3 px-4">{t('folio.folio_number')}</th>
                  <th className="text-left py-3 px-4">{t('common.guest')}</th>
                  <th className="text-left py-3 px-4">{t('common.room')}</th>
                  <th className="text-left py-3 px-4">{t('common.balance')}</th>
                  <th className="text-center py-3 px-4">{t('common.status')}</th>
                  <th className="text-right py-3 px-4">{t('common.actions')}</th>
                </tr>
              </thead>
              <tbody>
                {pagedFolios.map((f) => (
                  <tr key={f.id} className="border-b border-slate-100 hover:bg-slate-50 cursor-pointer" onClick={() => setSelectedFolio(f)}>
                    <td className="py-3 px-4 font-medium text-blue-600">{f.folio_number}</td>
                    <td className="py-3 px-4">{f.guest?.full_name || '-'}</td>
                    <td className="py-3 px-4">{f.reservation?.room?.room_number || '-'}</td>
                    <td className="py-3 px-4">{formatIDR(f.balance)}</td>
                    <td className="text-center py-3 px-4"><Badge color={f.status === 'open' ? 'blue' : f.status === 'finalized' ? 'gray' : 'red'}>{f.status}</Badge></td>
                    <td className="text-right py-3 px-4"><button className="text-blue-600 text-xs font-medium">{t('common.view')}</button></td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
          <Pagination page={page} pageSize={PAGE_SIZE} total={filteredFolios.length} onPageChange={setPage} />
        </Card>
      )}

      {selectedFolio && <FolioDetailModal folio={selectedFolio} onClose={() => { setSelectedFolio(null); load(); }} onNavigateToInvoice={onNavigateToInvoice} onSelectReservation={onSelectReservation} onNavigateToGuest={onNavigateToGuest} />}
    </div>
  );
}

function FolioDetailModal({ folio, onClose, onNavigateToInvoice, onSelectReservation, onNavigateToGuest }: { folio: Folio; onClose: () => void; onNavigateToInvoice?: (id: string) => void; onSelectReservation?: (id: string) => void; onNavigateToGuest?: (id: string) => void; }) {
  const { user } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [items, setItems] = useState<FolioItem[]>([]);
  const [reservation, setReservation] = useState<Reservation | null>(null);
  const [guest, setGuest] = useState<Guest | null>(null);
  const [room, setRoom] = useState<Room | null>(null);
  const [bookingSource, setBookingSource] = useState<BookingSource | null>(null);
  const [roomType, setRoomType] = useState<RoomType | null>(null);
  const [roomCount, setRoomCount] = useState<number>(1);
  const [groupRooms, setGroupRooms] = useState<ReservationRoom[]>([]);
  const [chargeCats, setChargeCats] = useState<ChargeCategory[]>([]);
  const [paymentMethods, setPaymentMethods] = useState<PaymentMethod[]>([]);
  const [loading, setLoading] = useState(true);
  const [showAddCharge, setShowAddCharge] = useState(false);
  const [showTakePayment, setShowTakePayment] = useState(false);
  const [showTransfer, setShowTransfer] = useState(false);
  const [showPostStay, setShowPostStay] = useState(false);
  const [voidTarget, setVoidTarget] = useState<FolioItem | null>(null);

  const isFinalized = folio.status === 'finalized';
  const canVoid = user?.role === 'super_admin' || user?.role === 'manager';

  const reloadItems = useCallback(async () => {
    const { data } = await supabase.from('folio_items').select('*').eq('folio_id', folio.id).order('created_at');
    setItems((data as FolioItem[]) || []);
  }, [folio.id]);

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

      let bs: BookingSource | null = null;
      if (resRecord?.booking_source_id) {
        const { data: bsData } = await supabase.from('booking_sources').select('*').eq('id', resRecord.booking_source_id).maybeSingle();
        bs = bsData as BookingSource | null;
      }

      let rt: RoomType | null = null;
      if (resRecord?.room_type_id) {
        const { data: rtData } = await supabase.from('room_types').select('*').eq('id', resRecord.room_type_id).maybeSingle();
        rt = rtData as RoomType | null;
      } else if (r) {
        const { data: rtData } = await supabase.from('room_types').select('*').eq('id', (r as Room).room_type_id).maybeSingle();
        rt = rtData as RoomType | null;
      }

      let count = 1;
      let groupRoomList: ReservationRoom[] = [];
      if (resRecord?.is_group) {
        const { data: rrData } = await supabase.from('reservation_rooms')
          .select('*,room:rooms(*)').eq('reservation_id', resRecord.id).eq('status', 'active').order('created_at');
        groupRoomList = (rrData as ReservationRoom[]) || [];
        count = groupRoomList.length || 1;
      }

      setItems((fi as FolioItem[]) || []);
      setReservation(resRecord);
      setGuest(g as Guest);
      setRoom(r as Room);
      setBookingSource(bs);
      setRoomType(rt);
      setRoomCount(count);
      setGroupRooms(groupRoomList);
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
    try {
      await folioService.voidItem(item.id, folio.id, user!.id, user!.organization_id, folio.branch_id);
      showToast(item.item_type === 'payment' ? 'Payment voided' : 'Item voided', 'success');
      await reloadItems();
    } catch (e) {
      const err = e instanceof FinancialError ? e : parseDbError(e as { message?: string });
      showToast(err.message, 'error');
    }
  };

  if (loading) return <Modal open onClose={onClose} title={t('folio.title')}><LoadingPage /></Modal>;

  return (
    <>
      <Modal open onClose={onClose} title={`${t('folio.title')} — ${folio.folio_number}`} size="xl">
        <div className="space-y-4">
        <div className="grid grid-cols-2 md:grid-cols-4 gap-3 text-sm">
          <div><span className="text-slate-500">{t('common.guest')}:</span> {onNavigateToGuest && folio.guest_id ? <button onClick={() => onNavigateToGuest(folio.guest_id!)} className="font-medium text-blue-600 hover:text-blue-700 flex items-center gap-1"><UserIcon size={12} />{guest?.full_name || '-'}</button> : <span className="font-medium">{guest?.full_name || '-'}</span>}</div>
          <div><span className="text-slate-500">{t('common.room')}:</span> <span className="font-medium">{room?.room_number || '-'}</span></div>
          <div><span className="text-slate-500">{t('common.reservation')}:</span> {onSelectReservation && folio.reservation_id ? <button onClick={() => onSelectReservation(folio.reservation_id)} className="font-medium text-blue-600 hover:text-blue-700">{reservation?.reservation_number || '-'}</button> : <span className="font-medium">{reservation?.reservation_number || '-'}</span>}</div>
          <div><span className="text-slate-500">{t('common.status')}:</span> <Badge color={folio.status === 'open' ? 'blue' : 'gray'}>{folio.status}</Badge></div>
          {roomType && <div><span className="text-slate-500">{t('common.room_type')}:</span> <span className="font-medium">{roomType.name}</span></div>}
          <div><span className="text-slate-500">{t('common.check_in')}:</span> <span className="font-medium">{reservation ? `${formatDate(reservation.check_in_date)} ${formatTime(reservation.check_in_time)}` : '-'}</span></div>
          <div><span className="text-slate-500">{t('common.check_out')}:</span> <span className="font-medium">{reservation ? `${formatDate(reservation.check_out_date)} ${formatTime(reservation.check_out_time)}` : '-'}</span></div>
          <div><span className="text-slate-500">{t('common.nights')}:</span> <span className="font-medium">{reservation?.num_nights || '-'}</span></div>
          <div><span className="text-slate-500">{t('res.adults_children')}:</span> <span className="font-medium">{reservation ? `${reservation.adults} ${t('common.adults')} / ${reservation.children} ${t('common.children')}` : '-'}</span></div>
          {bookingSource && <div><span className="text-slate-500">{t('common.booking_source')}:</span> <span className="font-medium">{bookingSource.name}</span></div>}
          <div><span className="text-slate-500">{t('res.group_rooms')}:</span> <span className="font-medium">{roomCount}</span></div>
          <div><span className="text-slate-500">{t('common.deposit')}:</span> <span className="font-medium">{reservation ? formatIDR(reservation.deposit) : '-'}</span></div>
        </div>

        {groupRooms.length > 1 && (
          <div className="border border-slate-200 rounded-lg p-3">
            <p className="text-xs font-semibold text-slate-500 uppercase mb-2">{t('res.group_rooms')} ({groupRooms.length})</p>
            <div className="flex flex-wrap gap-2">
              {groupRooms.map(rr => (
                <span key={rr.id} className="text-sm bg-slate-50 border border-slate-200 rounded px-2 py-1">
                  {(rr as any).room?.room_number || 'Unassigned'} · {formatIDR(rr.rate)}/{t('common.nights')}
                </span>
              ))}
            </div>
          </div>
        )}

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
                <th className="text-right py-2 px-3"></th>
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
                  <td className="text-right py-2 px-3 whitespace-nowrap">
                    {item.item_type === 'payment' && !item.voided && item.payment_id && <button onClick={() => openPrintTab({ type: 'receipt', paymentId: item.payment_id! })} className="text-xs text-blue-600 hover:text-blue-700 mr-3">Receipt</button>}
                    {!isFinalized && !item.voided && canVoid && <button onClick={() => setVoidTarget(item)} className="text-xs text-red-500 hover:text-red-700">Void</button>}
                  </td>
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

        <div className="flex flex-wrap gap-2">
          {!isFinalized && (
            <>
              <Button size="sm" onClick={() => setShowAddCharge(true)}><Plus size={14} /> {t('folio.add_charge')}</Button>
              <Button size="sm" variant="success" onClick={() => setShowTakePayment(true)}><Plus size={14} /> {t('folio.take_payment')}</Button>
              <Button size="sm" variant="outline" onClick={() => setShowTransfer(true)}><ArrowRightLeft size={14} /> {t('res.transfer_room')}</Button>
            </>
          )}
          {isFinalized && (
            <Button size="sm" variant="warning" onClick={() => setShowPostStay(true)}><Plus size={14} /> {t('folio.post_stay_charge')}</Button>
          )}
          {onNavigateToInvoice && folio.reservation_id && (
            <Button size="sm" variant="outline" onClick={() => onNavigateToInvoice(folio.reservation_id)}><Receipt size={14} /> {t('res.view_invoice')}</Button>
          )}
          {onSelectReservation && folio.reservation_id && (
            <Button size="sm" variant="outline" onClick={() => onSelectReservation(folio.reservation_id)}><FileText size={14} /> {t('nav.checkin_checkout')}</Button>
          )}
        </div>
        </div>
      </Modal>

      {showAddCharge && <AddChargeModal folio={folio} reservation={reservation} room={room} chargeCats={chargeCats} userId={user!.id} orgId={user!.organization_id} onClose={() => setShowAddCharge(false)} onSaved={async () => { setShowAddCharge(false); await reloadItems(); openPrintTab({ type: 'charge-summary', folioId: folio.id }); }} />}
      {showTakePayment && <TakePaymentModal folio={folio} reservation={reservation} paymentMethods={paymentMethods} userId={user!.id} orgId={user!.organization_id} onClose={() => setShowTakePayment(false)} onSaved={async (paymentId?: string) => { setShowTakePayment(false); await reloadItems(); if (paymentId) openPrintTab({ type: 'receipt', paymentId }); }} />}
      {showTransfer && <RoomTransferModal folio={folio} reservation={reservation} currentRoom={room} userId={user!.id} orgId={user!.organization_id} branchId={folio.branch_id} onClose={() => setShowTransfer(false)} onSaved={onClose} />}
      {showPostStay && <PostStayChargeModal folio={folio} reservation={reservation} room={room} chargeCats={chargeCats} userId={user!.id} orgId={user!.organization_id} onClose={() => setShowPostStay(false)} onSaved={async () => { setShowPostStay(false); await reloadItems(); }} />}
      <ConfirmModal
        open={!!voidTarget}
        onClose={() => setVoidTarget(null)}
        onConfirm={() => { if (voidTarget) voidItem(voidTarget); setVoidTarget(null); }}
        title={voidTarget?.item_type === 'payment' ? 'Void Payment' : 'Void Charge'}
        message={`Void "${voidTarget?.description}" (${formatIDR(Math.abs(voidTarget?.amount || 0))})? This will remove it from the folio and mark the underlying ${voidTarget?.item_type === 'payment' ? 'payment' : 'charge'} record as voided.`}
        confirmLabel={t('common.void')}
        variant="danger"
      />
    </>
  );
}

const initialAddChargeForm = { category_id: '', description: '', amount: '0', quantity: '1', notes: '' };

function AddChargeModal({ folio, reservation, room, chargeCats, userId, orgId, onClose, onSaved }: {
  folio: Folio; reservation: Reservation | null; room: Room | null; chargeCats: ChargeCategory[];
  userId: string; orgId: string; onClose: () => void; onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [form, setForm] = useState(() => {
    const draft = loadDraft<typeof initialAddChargeForm>(ADD_CHARGE_DRAFT_KEY);
    return draft || { ...initialAddChargeForm };
  });

  useEffect(() => { saveDraft(ADD_CHARGE_DRAFT_KEY, form); }, [form]);

  const handleSubmit = async () => {
    if (!form.description || parseFloat(form.amount) <= 0) { showToast('Description and amount required', 'error'); return; }
    setSaving(true);
    try {
      await chargeService.addCharge({
        folioId: folio.id, branchId: folio.branch_id, reservationId: folio.reservation_id,
        guestId: folio.guest_id, roomId: room?.id || reservation?.room_id || null,
        categoryId: form.category_id, description: form.description,
        amount: parseFloat(form.amount), quantity: parseFloat(form.quantity),
        notes: form.notes, userId, orgId,
      }, chargeCats);
      showToast('Charge added', 'success');
      clearDraft(ADD_CHARGE_DRAFT_KEY);
      setSaving(false);
      onSaved();
    } catch (e) {
      const err = e instanceof FinancialError ? e : parseDbError(e as { message?: string });
      showToast(err.message, 'error');
      setSaving(false);
    }
  };

  const handleCancel = () => { clearDraft(ADD_CHARGE_DRAFT_KEY); onClose(); };

  return (
    <Modal open onClose={handleCancel} title={t('folio.add_charge')} size="md"
      footer={<><Button variant="secondary" onClick={handleCancel}>{t('common.cancel')}</Button><Button loading={saving} onClick={handleSubmit}>{t('common.save')}</Button></>}>
      <form className="space-y-4">
        <Select label={t('common.category')} value={form.category_id} onChange={(e) => setForm({ ...form, category_id: e.target.value })}>
          <option value="">--</option>
          {chargeCats.map((c) => <option key={c.id} value={c.id}>{c.name}{c.is_damage ? ' (Damage)' : ''}</option>)}
        </Select>
        <Input label={t('common.description')} value={form.description} onChange={(e) => setForm({ ...form, description: e.target.value })} required />
        <div className="grid grid-cols-2 gap-4">
          <MoneyInput label={t('common.amount')} value={form.amount} onChange={(v) => setForm({ ...form, amount: v })} />
          <Input label={t('common.quantity')} type="number" value={form.quantity} onChange={(e) => setForm({ ...form, quantity: e.target.value })} />
        </div>
        <Textarea label={t('common.notes')} value={form.notes} onChange={(e) => setForm({ ...form, notes: e.target.value })} rows={2} />
      </form>
    </Modal>
  );
}

const initialTakePaymentForm = { method_id: '', subtype: '', edc_terminal: '', reference_number: '', approval_code: '', amount: '0', notes: '' };

function TakePaymentModal({ folio, reservation, paymentMethods, userId, orgId, onClose, onSaved }: {
  folio: Folio; reservation: Reservation | null; paymentMethods: PaymentMethod[];
  userId: string; orgId: string; onClose: () => void; onSaved: (paymentId?: string) => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [form, setForm] = useState(() => {
    const draft = loadDraft<typeof initialTakePaymentForm>(TAKE_PAYMENT_DRAFT_KEY);
    return draft || { ...initialTakePaymentForm };
  });

  useEffect(() => { saveDraft(TAKE_PAYMENT_DRAFT_KEY, form); }, [form]);

  const selectedMethod = paymentMethods.find((m) => m.id === form.method_id);

  const handleSubmit = async () => {
    if (!form.method_id || parseFloat(form.amount) <= 0) { showToast('Method and amount required', 'error'); return; }
    setSaving(true);
    try {
      const paymentId = await paymentService.recordPayment({
        folioId: folio.id, branchId: folio.branch_id, reservationId: folio.reservation_id,
        guestId: folio.guest_id, methodId: form.method_id, amount: parseFloat(form.amount),
        subtype: form.subtype, edcTerminal: form.edc_terminal,
        referenceNumber: form.reference_number, approvalCode: form.approval_code,
        notes: form.notes, userId, orgId,
      }, paymentMethods);
      showToast('Payment recorded', 'success');
      clearDraft(TAKE_PAYMENT_DRAFT_KEY);
      setSaving(false);
      onSaved(paymentId);
    } catch (e) {
      const err = e instanceof FinancialError ? e : parseDbError(e as { message?: string });
      showToast(err.message, 'error');
      setSaving(false);
    }
  };

  const handleCancel = () => { clearDraft(TAKE_PAYMENT_DRAFT_KEY); onClose(); };

  return (
    <Modal open onClose={handleCancel} title={t('payment.record_payment')} size="md"
      footer={<><Button variant="secondary" onClick={handleCancel}>{t('common.cancel')}</Button><Button loading={saving} variant="success" onClick={handleSubmit}>{t('common.save')}</Button></>}>
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
        <MoneyInput label={t('common.amount')} value={form.amount} onChange={(v) => setForm({ ...form, amount: v })} required />
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
  const initialTransferForm = { to_room_id: '', reason: '' };
  const [form, setForm] = useState(() => {
    const draft = loadDraft<typeof initialTransferForm>(ROOM_TRANSFER_DRAFT_KEY);
    return draft || { ...initialTransferForm };
  });

  useEffect(() => {
    supabase.from('rooms').select('*').eq('branch_id', branchId).eq('is_active', true).in('status', ['available', 'dirty', 'inspected', 'cleaning']).order('room_number').then(({ data }) => setRooms((data as Room[]) || []));
  }, [branchId]);

  useEffect(() => { saveDraft(ROOM_TRANSFER_DRAFT_KEY, form); }, [form]);

  const handleSubmit = async () => {
    if (!form.to_room_id) { showToast('Select a room', 'error'); return; }
    setSaving(true);
    try {
      const { error: resErr } = await supabase.from('reservations').update({ room_id: form.to_room_id }).eq('id', folio.reservation_id);
      if (resErr) throw resErr;
      if (currentRoom) await supabase.from('rooms').update({ status: 'dirty' }).eq('id', currentRoom.id);
      await supabase.from('rooms').update({ status: 'occupied' }).eq('id', form.to_room_id);
      await supabase.from('room_transfers').insert({
        reservation_id: folio.reservation_id, from_room_id: currentRoom?.id || null,
        to_room_id: form.to_room_id, reason: form.reason || null, performed_by: userId,
      });
      await supabase.from('audit_logs').insert({
        organization_id: orgId, branch_id: branchId, user_id: userId,
        action: 'room_transfer', object_type: 'reservation', object_id: folio.reservation_id,
        previous_value: { room: currentRoom?.room_number }, new_value: { room: rooms.find((r) => r.id === form.to_room_id)?.room_number },
      });
      const { data: lockInteg } = await supabase.from('hotel_lock_integrations').select('*').eq('branch_id', branchId).maybeSingle();
      const lockType = (lockInteg as HotelLockIntegration | null)?.provider_type || 'mock';
      const provider = getLockProviderByType(lockType);
      provider.configure(integrationToConfig(lockInteg as HotelLockIntegration | null));
      await provider.invalidateGuestCard({ cardId: folio.reservation_id });
      showToast('Room transferred', 'success');
      clearDraft(ROOM_TRANSFER_DRAFT_KEY);
      setSaving(false);
      onSaved();
    } catch (e) {
      const err = parseDbError(e as { message?: string });
      showToast(err.message, 'error');
      setSaving(false);
    }
  };

  const handleCancel = () => { clearDraft(ROOM_TRANSFER_DRAFT_KEY); onClose(); };

  return (
    <Modal open onClose={handleCancel} title={t('res.transfer_room')} size="md"
      footer={<><Button variant="secondary" onClick={handleCancel}>{t('common.cancel')}</Button><Button loading={saving} onClick={handleSubmit}>{t('common.confirm')}</Button></>}>
      <div className="space-y-4">
        <div className="text-sm text-slate-600">{t('common.room')}: <span className="font-medium">{currentRoom?.room_number || '-'}</span> → <span className="text-blue-600 font-medium">{rooms.find((r) => r.id === form.to_room_id)?.room_number || '?'}</span></div>
        <Select label="New Room" value={form.to_room_id} onChange={(e) => setForm({ ...form, to_room_id: e.target.value })} required>
          <option value="">--</option>
          {rooms.map((r) => <option key={r.id} value={r.id}>{r.room_number} ({t(`room.${r.status}`)})</option>)}
        </Select>
        <Textarea label={t('common.reason')} value={form.reason} onChange={(e) => setForm({ ...form, reason: e.target.value })} rows={2} />
      </div>
    </Modal>
  );
}

const initialPostStayForm = { category_id: '', description: '', amount: '0', notes: '' };

function PostStayChargeModal({ folio, reservation, room, chargeCats, userId, orgId, onClose, onSaved }: {
  folio: Folio; reservation: Reservation | null; room: Room | null; chargeCats: ChargeCategory[];
  userId: string; orgId: string; onClose: () => void; onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [form, setForm] = useState(() => {
    const draft = loadDraft<typeof initialPostStayForm>(POST_STAY_DRAFT_KEY);
    return draft || { ...initialPostStayForm };
  });

  useEffect(() => { saveDraft(POST_STAY_DRAFT_KEY, form); }, [form]);

  const handleSubmit = async () => {
    if (!form.description || parseFloat(form.amount) <= 0) { showToast('Description and amount required', 'error'); return; }
    setSaving(true);
    try {
      await chargeService.addPostStayCharge({
        folioId: folio.id, branchId: folio.branch_id, reservationId: folio.reservation_id,
        guestId: folio.guest_id, roomId: room?.id || reservation?.room_id || null,
        categoryId: form.category_id, description: form.description,
        amount: parseFloat(form.amount), notes: form.notes, userId, orgId,
      }, chargeCats);
      showToast('Post-stay charge added', 'success');
      clearDraft(POST_STAY_DRAFT_KEY);
      setSaving(false);
      onSaved();
    } catch (e) {
      const err = e instanceof FinancialError ? e : parseDbError(e as { message?: string });
      showToast(err.message, 'error');
      setSaving(false);
    }
  };

  const handleCancel = () => { clearDraft(POST_STAY_DRAFT_KEY); onClose(); };

  return (
    <Modal open onClose={handleCancel} title={t('folio.post_stay_charge')} size="md"
      footer={<><Button variant="secondary" onClick={handleCancel}>{t('common.cancel')}</Button><Button loading={saving} variant="warning" onClick={handleSubmit}>{t('common.save')}</Button></>}>
      <div className="space-y-4">
        <div className="bg-amber-50 border border-amber-200 rounded-lg p-3 text-sm text-amber-700">This charge will be added as a post-stay additional charge. The original finalized invoice will not be modified.</div>
        <Select label={t('common.category')} value={form.category_id} onChange={(e) => setForm({ ...form, category_id: e.target.value })}>
          <option value="">--</option>
          {chargeCats.map((c) => <option key={c.id} value={c.id}>{c.name}</option>)}
        </Select>
        <Input label={t('common.description')} value={form.description} onChange={(e) => setForm({ ...form, description: e.target.value })} required />
        <MoneyInput label={t('common.amount')} value={form.amount} onChange={(v) => setForm({ ...form, amount: v })} required />
        <Textarea label={t('common.notes')} value={form.notes} onChange={(e) => setForm({ ...form, notes: e.target.value })} rows={2} />
      </div>
    </Modal>
  );
}
