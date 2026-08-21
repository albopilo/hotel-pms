import { useState, useEffect, useCallback, useMemo, useRef } from 'react';
import { supabase } from '@/lib/supabase';
import { invoiceService } from '@/services/invoiceService';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal, ConfirmModal } from '@/components/ui/Modal';
import { Input, Select } from '@/components/ui/Form';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { Badge } from '@/components/ui/Badge';
import { formatIDR, formatDate, formatTime, formatDateTime, todayISO, addDays, nightsBetween, formatHoursShort } from '@/lib/format';
import { getLockProvider } from '@/lib/hotel-lock/provider';
import { generateDocumentNumber } from '@/lib/documentNumber';
import { LogIn, LogOut, KeyRound, CircleAlert as AlertCircle, CircleCheck as CheckCircle2, Loader as Loader2, CalendarPlus, Split } from 'lucide-react';
import type { Reservation, Guest, Room, Folio, ReservationRoom } from '@/types/database';

export function CheckinCheckoutPage({ initialReservationId, searchQuery }: { initialReservationId?: string | null; searchQuery?: string }) {
  const { branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();

  const [reservations, setReservations] = useState<Reservation[]>([]);
  const [checkedOut, setCheckedOut] = useState<Reservation[]>([]);
  const [guests, setGuests] = useState<Guest[]>([]);
  const [rooms, setRooms] = useState<Room[]>([]);
  const [loading, setLoading] = useState(true);
  const [selected, setSelected] = useState<Reservation | null>(null);
  const [mode, setMode] = useState<'checkin' | 'checkout' | 'extend' | 'split' | null>(null);
  const [reservationRooms, setReservationRooms] = useState<ReservationRoom[]>([]);
  const processedInitialId = useRef<string | null>(null);

  const branchIds = useMemo(() => selectedBranchId ? [selectedBranchId] : branches.map(b => b.id), [selectedBranchId, branches]);

  const load = useCallback(async () => {
    if (!branchIds.length) return setLoading(false);
    setLoading(true);

    const [{ data: res }, { data: co }, { data: g }, { data: r }] = await Promise.all([
      supabase.from('reservations').select('*').in('branch_id', branchIds).in('status', ['confirmed', 'checked_in']).order('check_in_date'),
      supabase.from('reservations').select('*').in('branch_id', branchIds).eq('status', 'checked_out').order('actual_check_out', { ascending: false }).limit(20),
      supabase.from('guests').select('*'),
      supabase.from('rooms').select('*').in('branch_id', branchIds)
    ]);

    setReservations((res as Reservation[]) || []);
    setCheckedOut((co as Reservation[]) || []);
    setGuests((g as Guest[]) || []);
    setRooms((r as Room[]) || []);
    setLoading(false);
  }, [branchIds]);

  useEffect(() => { load(); }, [load]);

  useEffect(() => {
    if (!initialReservationId) return;
    if (processedInitialId.current === initialReservationId) return;
    const r = reservations.find(x => x.id === initialReservationId);
    if (r) {
      processedInitialId.current = initialReservationId;
      setSelected(r);
      setMode(r.status === 'checked_in' ? 'checkout' : 'checkin');
    }
  }, [initialReservationId, reservations]);

  const guestMap = useMemo(() => new Map(guests.map(g => [g.id, g])), [guests]);
  const roomMap = useMemo(() => new Map(rooms.map(r => [r.id, r])), [rooms]);

  const arrivals = reservations.filter(r => r.status === 'confirmed');
  const departuresToday = reservations.filter(r => r.status === 'checked_in' && r.check_out_date <= todayISO());
  const departuresLater = reservations.filter(r => r.status === 'checked_in' && r.check_out_date > todayISO());

  const q = (searchQuery || '').toLowerCase().trim();

  const filterFn = (r: Reservation) => {
    if (!q) return true;
    const g = guestMap.get(r.primary_guest_id || '');
    const rm = roomMap.get(r.room_id || '');
    return r.reservation_number.toLowerCase().includes(q) || (g?.full_name || '').toLowerCase().includes(q) || (rm?.room_number || '').includes(q);
  };

  const loadReservationRooms = async (resId: string) => {
    const { data } = await supabase.from('reservation_rooms').select('*').eq('reservation_id', resId).eq('status', 'active');
    setReservationRooms((data as ReservationRoom[]) || []);
  };

  const handleSelectReservation = async (r: Reservation, m: 'checkin' | 'checkout' | 'extend' | 'split') => {
    setSelected(r);
    setMode(m);
    if (r.is_group) {
      await loadReservationRooms(r.id);
    }
  };

  const handleCloseModal = () => {
    setSelected(null);
    setMode(null);
    setReservationRooms([]);
    load();
  };

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-slate-900">{t('nav.checkin_checkout')}</h1>
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <Card title={`${t('dash.arrivals_today')} / ${t('res.confirmed')}`}>
          {arrivals.filter(filterFn).length === 0 ? <EmptyState title={t('common.no_data')} /> :
          <div className="space-y-2">{arrivals.filter(filterFn).map(r => {
            const g = guestMap.get(r.primary_guest_id || '');
            const rm = roomMap.get(r.room_id || '');
            return <div key={r.id} className="flex items-center justify-between border border-slate-100 rounded-lg px-3 py-2 hover:bg-slate-50">
              <div>
                <p className="font-medium text-slate-800">{g?.full_name || '-'} {r.is_group && <Badge color="purple" size="sm">Group ({r.reservation_number})</Badge>}</p>
                <p className="text-xs text-slate-500">{r.reservation_number} · {rm?.room_number || 'Unassigned'} · {formatDate(r.check_in_date)} {formatTime(r.check_in_time)}</p>
              </div>
              <Button size="sm" onClick={() => handleSelectReservation(r, 'checkin')}><LogIn size={14}/>{t('action.check_in')}</Button>
            </div>;
          })}</div>}
        </Card>

        <Card title={`${t('dash.departures_today')} / ${t('res.checked_in')}`}>
          {departuresToday.filter(filterFn).length === 0 ? <EmptyState title={t('common.no_data')} /> :
          <div className="space-y-2">{departuresToday.filter(filterFn).map(r => {
            const g = guestMap.get(r.primary_guest_id || '');
            const rm = roomMap.get(r.room_id || '');
            return <div key={r.id} className="flex items-center justify-between border border-slate-100 rounded-lg px-3 py-2 hover:bg-slate-50">
              <div>
                <p className="font-medium text-slate-800">{g?.full_name || '-'} {r.is_group && <Badge color="purple" size="sm">Group</Badge>}</p>
                <p className="text-xs text-slate-500">{r.reservation_number} · {rm?.room_number || '-'} · {formatDate(r.check_out_date)} {formatTime(r.check_out_time)}</p>
              </div>
              <div className="flex gap-1">
                <Button size="sm" variant="outline" onClick={() => handleSelectReservation(r, 'extend')}><CalendarPlus size={14}/>{t('res.extend_stay')}</Button>
                {r.is_group && <Button size="sm" variant="outline" onClick={() => handleSelectReservation(r, 'split')}><Split size={14}/>{t('res.split_room')}</Button>}
                <Button size="sm" variant="warning" onClick={() => handleSelectReservation(r, 'checkout')}><LogOut size={14}/>{t('action.check_out')}</Button>
              </div>
            </div>;
          })}</div>}
        </Card>
      </div>

      <Card title={t('dash.departures_later')}>
        {departuresLater.filter(filterFn).length === 0 ? <EmptyState title={t('common.no_data')} /> :
        <div className="space-y-2">{departuresLater.filter(filterFn).map(r => {
          const g = guestMap.get(r.primary_guest_id || '');
          const rm = roomMap.get(r.room_id || '');
          return <div key={r.id} className="flex items-center justify-between border border-slate-100 rounded-lg px-3 py-2 hover:bg-slate-50">
            <div>
              <p className="font-medium text-slate-800">{g?.full_name || '-'} {r.is_group && <Badge color="purple" size="sm">Group</Badge>}</p>
              <p className="text-xs text-slate-500">{r.reservation_number} · {rm?.room_number || '-'} · {formatDate(r.check_out_date)} {formatTime(r.check_out_time)}</p>
            </div>
            <div className="flex gap-1">
              <Button size="sm" variant="outline" onClick={() => handleSelectReservation(r, 'extend')}><CalendarPlus size={14}/>{t('res.extend_stay')}</Button>
              {r.is_group && <Button size="sm" variant="outline" onClick={() => handleSelectReservation(r, 'split')}><Split size={14}/>{t('res.split_room')}</Button>}
              <Button size="sm" variant="warning" onClick={() => handleSelectReservation(r, 'checkout')}><LogOut size={14}/>{t('action.check_out')}</Button>
            </div>
          </div>;
        })}</div>}
      </Card>

      <Card title={t('res.checked_out')}>
        {checkedOut.filter(filterFn).length === 0 ? <EmptyState title={t('common.no_data')} /> :
        <div className="space-y-2">{checkedOut.filter(filterFn).map(r => {
          const g = guestMap.get(r.primary_guest_id || '');
          const rm = roomMap.get(r.room_id || '');
          return <div key={r.id} className="flex items-center justify-between border border-slate-100 rounded-lg px-3 py-2">
            <div>
              <p className="font-medium text-slate-800">{g?.full_name || '-'}</p>
              <p className="text-xs text-slate-500">{r.reservation_number} · {rm?.room_number || '-'} · {r.actual_check_out ? formatDateTime(r.actual_check_out) : `${formatDate(r.check_out_date)} ${formatTime(r.check_out_time)}`}</p>
            </div>
            <Badge color="gray">{t('res.checked_out')}</Badge>
          </div>;
        })}</div>}
      </Card>

      {selected && mode === 'checkin' && <CheckinModal reservation={selected} onClose={handleCloseModal} />}
      {selected && mode === 'checkout' && <CheckoutModal reservation={selected} onClose={handleCloseModal} />}
      {selected && mode === 'extend' && <ExtendStayModal reservation={selected} onClose={handleCloseModal} />}
      {selected && mode === 'split' && <SplitRoomModal reservation={selected} reservationRooms={reservationRooms} rooms={rooms} onClose={handleCloseModal} />}
    </div>
  );
}

function CheckinModal({ reservation, onClose }: { reservation: Reservation; onClose: () => void }) {
  const { user, branches } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();

  const [guest, setGuest] = useState<Guest | null>(null);
  const [room, setRoom] = useState<Room | null>(null);
  const [folio, setFolio] = useState<Folio | null>(null);
  const [loading, setLoading] = useState(true);
  const [earlyWarning, setEarlyWarning] = useState(false);
  const [checkinTime, setCheckinTime] = useState(new Date().toTimeString().slice(0,5));
  const [cardState, setCardState] = useState<'idle'|'connecting'|'writing'|'confirming'|'success'|'failed'|'unavailable'>('idle');
  const [cardMessage, setCardMessage] = useState('');
  const [completing, setCompleting] = useState(false);

  const branch = branches.find(b => b.id === reservation.branch_id);
  const standardTime = branch?.standard_checkin_time || '14:00';

  useEffect(() => {
    (async () => {
      setLoading(true);
      const [{ data:g }, { data:r }, { data:f }] = await Promise.all([
        supabase.from('guests').select('*').eq('id', reservation.primary_guest_id).maybeSingle(),
        reservation.room_id ? supabase.from('rooms').select('*').eq('id', reservation.room_id).maybeSingle() : Promise.resolve({ data:null }),
        supabase.from('folios').select('*').eq('reservation_id', reservation.id).maybeSingle()
      ]);
      setGuest(g as Guest);
      setRoom(r as Room);
      setFolio(f as Folio);
      setLoading(false);
    })();
  }, [reservation]);

  useEffect(() => {
    // Compare full datetimes: scheduled check-in (date + standard time) vs actual (today + actual time).
    // This correctly handles cases like 22 Aug 00:01 vs 21 Aug 14:00 standard (not early).
    const scheduledDateTime = new Date(`${reservation.check_in_date}T${standardTime}:00`);
    const actualDateTime = new Date(`${todayISO()}T${checkinTime}:00`);
    setEarlyWarning(actualDateTime < scheduledDateTime);
  }, [standardTime, checkinTime, reservation.check_in_date]);

  const handleAddEarlyCharge = async () => {
    if (!folio) return;
    const { data } = await supabase.from('system_settings').select('value').eq('key','early_checkin_charge').maybeSingle();
    const charge = parseFloat(data?.value || '0');
    if (charge <= 0) return showToast('No early check-in charge configured','warning');

    await supabase.from('folio_items').insert({
      folio_id: folio.id, branch_id: reservation.branch_id, reservation_id: reservation.id,
      guest_id: reservation.primary_guest_id, room_id: reservation.room_id,
      item_type:'charge', category:'early_checkin', description:'Early check-in charge',
      quantity:1, unit_amount:charge, amount:charge, business_date:todayISO(), created_by:user!.id
    });

    await supabase.from('transactions').insert({
      branch_id:reservation.branch_id, organization_id:user!.organization_id,
      reservation_id:reservation.id, guest_id:reservation.primary_guest_id, folio_id:folio.id,
      transaction_type:'early_checkin_charge', description:'Early check-in charge',
      amount:charge, debit_credit:'debit', business_date:todayISO(), created_by:user!.id
    });

    showToast(`Early check-in charge added: ${formatIDR(charge)}`,'success');
    setEarlyWarning(false);
  };

  const handleContinueNoCharge = async () => {
    await supabase.from('audit_logs').insert({
      organization_id:user!.organization_id, branch_id:reservation.branch_id, user_id:user!.id,
      action:'early_checkin_no_charge', object_type:'reservation', object_id:reservation.id,
      reason:`Early check-in at ${checkinTime} (standard: ${standardTime}) - no charge applied`
    });
    setEarlyWarning(false);
    showToast('Continued without charge (logged)','info');
  };

  const encodeCard = async () => {
    if (!room || !guest) return;
    const provider = getLockProvider();
    setCardState('connecting');
    setCardMessage(t('checkin.connecting'));
    if (!(await provider.connect())) {
      setCardState('unavailable');
      setCardMessage(t('checkin.lock_unavailable'));
      return;
    }
    setCardState('writing');
    setCardMessage(t('checkin.writing'));
    const result = await provider.encodeGuestCard({
      roomId:room.id, roomNumber:room.room_number, guestName:guest.full_name,
      validFrom:`${reservation.check_in_date}T${checkinTime}`,
      validUntil:`${reservation.check_out_date}T${reservation.check_out_time}`
    });
    if (result.success) {
      setCardState('confirming');
      setCardMessage(t('checkin.confirming'));
      await new Promise(r => setTimeout(r,500));
      setCardState('success');
      setCardMessage(t('checkin.card_success'));
      await supabase.from('card_issuances').insert({
        branch_id:reservation.branch_id, reservation_id:reservation.id, guest_id:guest.id, room_id:room.id,
        issuance_type:'issue', card_sequence:1,
        valid_from:`${reservation.check_in_date}T${checkinTime}`,
        valid_until:`${reservation.check_out_date}T${reservation.check_out_time}`,
        status:'success', provider_type:'mock', performed_by:user!.id
      });
    } else {
      setCardState('failed');
      setCardMessage(result.message);
      await supabase.from('card_issuances').insert({
        branch_id:reservation.branch_id, reservation_id:reservation.id, guest_id:guest.id, room_id:room.id,
        issuance_type:'issue', card_sequence:1,
        valid_from:`${reservation.check_in_date}T${checkinTime}`,
        valid_until:`${reservation.check_out_date}T${reservation.check_out_time}`,
        status:'failed', failure_reason:result.message, provider_type:'mock', performed_by:user!.id
      });
    }
  };

  const completeCheckin = async () => {
    setCompleting(true);
    const { error } = await supabase.from('reservations').update({
      status:'checked_in', actual_check_in:`${todayISO()}T${checkinTime}:00`, check_in_time:checkinTime
    }).eq('id',reservation.id);
    if (error) { showToast(error.message,'error'); return setCompleting(false); }

    if (room) await supabase.from('rooms').update({status:'occupied'}).eq('id',room.id);

    if (folio) {
      try {
        await invoiceService.ensureInvoice({
          folioId: folio.id, branchId: reservation.branch_id,
          organizationId: user!.organization_id, reservationId: reservation.id,
          guestId: reservation.primary_guest_id, userId: user!.id,
        });
      } catch (err: any) {
        console.error('Invoice creation at check-in failed:', err);
        showToast(`Invoice creation failed: ${err.message || err}`, 'error');
      }
    }

    await supabase.from('audit_logs').insert({
      organization_id:user!.organization_id, branch_id:reservation.branch_id, user_id:user!.id,
      action:'check_in', object_type:'reservation', object_id:reservation.id, new_value:{checkin_time:checkinTime}
    });

    showToast(t('checkin.complete'),'success');
    setCompleting(false);
    onClose();
  };

  if (loading) return <Modal open onClose={onClose} title={t('checkin.title')}><LoadingPage /></Modal>;

  return (
    <Modal open onClose={onClose} title={t('checkin.title')} size="lg">
      <div className="space-y-4">
        <div className="grid grid-cols-2 gap-4 text-sm">
          <div><span className="text-slate-500">{t('common.guest')}:</span> <span className="font-medium">{guest?.full_name || '-'}</span></div>
          <div><span className="text-slate-500">{t('common.room')}:</span> <span className="font-medium">{room?.room_number || '-'}</span></div>
          <div><span className="text-slate-500">{t('common.check_in')}:</span> <span className="font-medium">{formatDate(reservation.check_in_date)} {formatTime(reservation.check_in_time)}</span></div>
          <div><span className="text-slate-500">{t('common.check_out')}:</span> <span className="font-medium">{formatDate(reservation.check_out_date)} {formatTime(reservation.check_out_time)}</span></div>
          <div><span className="text-slate-500">{t('common.rate')}:</span> <span className="font-medium">{formatIDR(reservation.rate)}</span></div>
          <div><span className="text-slate-500">{t('common.deposit')}:</span> <span className="font-medium">{formatIDR(reservation.deposit)}</span></div>
        </div>

        <div>
          <label className="text-sm font-medium text-slate-700">{t('checkin.actual_time')}</label>
          <input type="time" value={checkinTime} onChange={e=>setCheckinTime(e.target.value)} className="mt-1 rounded-lg border border-slate-300 px-3 py-2 text-sm"/>
        </div>

        {earlyWarning && (
          <div className="bg-amber-50 border border-amber-300 rounded-lg p-4 space-y-3">
            <div className="flex items-center gap-2 text-amber-700 font-medium"><AlertCircle size={18}/>{t('checkin.early_warning')}</div>
            <div className="text-sm text-slate-600">
              <div>{t('checkin.standard_time')}: <span className="font-medium">{formatDate(reservation.check_in_date)} {standardTime}</span></div>
              <div>{t('checkin.actual_time')}: <span className="font-medium">{formatDate(todayISO())} {checkinTime}</span></div>
              <div>{t('checkin.difference')}: <span className="font-medium">{formatHoursShort((new Date(`${todayISO()}T${checkinTime}:00`).getTime() - new Date(`${reservation.check_in_date}T${standardTime}:00`).getTime()) / 3600000)}</span></div>
            </div>
            <div className="flex gap-2">
              <Button size="sm" variant="warning" onClick={handleAddEarlyCharge}>{t('checkin.add_charge')}</Button>
              <Button size="sm" variant="outline" onClick={handleContinueNoCharge}>{t('checkin.continue_no_charge')}</Button>
            </div>
          </div>
        )}

        <div className="border border-slate-200 rounded-lg p-4">
          <div className="flex items-center justify-between mb-3">
            <div className="flex items-center gap-2"><KeyRound size={18}/><span className="font-medium text-slate-700">{t('checkin.encode_card')}</span></div>
            <span className="text-xs text-amber-600 font-medium bg-amber-50 px-2 py-0.5 rounded">DEVELOPMENT / MOCK MODE</span>
          </div>
          {room && guest && <div className="text-sm text-slate-500 mb-3">{t('common.room')}: <span className="font-medium">{room.room_number}</span> · {t('common.guest')}: <span className="font-medium">{guest.full_name}</span></div>}
          {cardState === 'idle' && <Button onClick={encodeCard} disabled={!room}><KeyRound size={16}/>{t('checkin.encode_card')}</Button>}
          {cardState !== 'idle' && (
            <div className="flex items-center gap-3">
              {['connecting','writing','confirming'].includes(cardState) && <Loader2 size={20} className="animate-spin text-blue-600"/>}
              {cardState === 'success' && <CheckCircle2 size={20} className="text-emerald-600"/>}
              {['failed','unavailable'].includes(cardState) && <AlertCircle size={20}/>}
              <span className="text-sm font-medium">{cardMessage}</span>
            </div>
          )}
        </div>

        <div className="flex justify-end gap-2">
          <Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button>
          <Button loading={completing} onClick={completeCheckin}><LogIn size={16}/>{t('checkin.complete')}</Button>
        </div>
      </div>
    </Modal>
  );
}

function CheckoutModal({ reservation, onClose }: { reservation: Reservation; onClose: () => void }) {
  const { user, branches } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();

  const [guest,setGuest]=useState<Guest|null>(null);
  const [room,setRoom]=useState<Room|null>(null);
  const [folio,setFolio]=useState<Folio|null>(null);
  const [folioItems,setFolioItems]=useState<any[]>([]);
  const [loading,setLoading]=useState(true);
  const [lateWarning,setLateWarning]=useState(false);
  const [checkoutTime,setCheckoutTime]=useState(new Date().toTimeString().slice(0,5));
  const [completing,setCompleting]=useState(false);
  const [overrideUnpaid,setOverrideUnpaid]=useState(false);
  const [showOverrideConfirm,setShowOverrideConfirm]=useState(false);

  const branch=branches.find(b=>b.id===reservation.branch_id);
  const standardTime=branch?.standard_checkout_time || '12:00';

  useEffect(()=>{
    (async()=>{
      setLoading(true);
      const [{data:g},{data:r},{data:f}]=await Promise.all([
        supabase.from('guests').select('*').eq('id',reservation.primary_guest_id).maybeSingle(),
        supabase.from('rooms').select('*').eq('id',reservation.room_id).maybeSingle(),
        supabase.from('folios').select('*').eq('reservation_id',reservation.id).maybeSingle()
      ]);
      setGuest(g as Guest);
      setRoom(r as Room);
      setFolio(f as Folio);
      if(f){
        const {data:items}=await supabase.from('folio_items').select('*').eq('folio_id',f.id).eq('voided',false).order('created_at');
        setFolioItems(items||[]);
      }
      setLoading(false);
    })();
  },[reservation]);

  useEffect(()=>{
    const today = todayISO();
    const isLate = reservation.check_out_date < today || (reservation.check_out_date === today && checkoutTime > standardTime);
    setLateWarning(isLate);
  },[standardTime,checkoutTime,reservation.check_out_date]);

  const charges=folioItems.filter(i=>i.item_type==='charge'&&i.amount>0);
  const payments=folioItems.filter(i=>i.item_type==='payment');
  const discounts=folioItems.filter(i=>i.item_type==='discount');
  const taxes=folioItems.filter(i=>i.item_type==='tax');

  const totalCharges=charges.reduce((s,i)=>s+i.amount,0);
  const totalPayments=payments.reduce((s,i)=>s+Math.abs(i.amount),0);
  const totalDiscounts=discounts.reduce((s,i)=>s+Math.abs(i.amount),0);
  const totalTax=taxes.reduce((s,i)=>s+i.amount,0);

  const balance=totalCharges+totalTax-totalDiscounts-totalPayments;
  const hasUnpaid=balance>0;

  const handleAddLateCharge = async () => {
    if (!folio) return;
    const { data } = await supabase.from('system_settings').select('value').eq('key','late_checkout_charge').maybeSingle();
    const charge = parseFloat(data?.value || '0');
    if (charge <= 0) return showToast('No late checkout charge configured','warning');

    await supabase.from('folio_items').insert({
      folio_id:folio.id, branch_id:reservation.branch_id, reservation_id:reservation.id,
      guest_id:reservation.primary_guest_id, room_id:reservation.room_id,
      item_type:'charge', category:'late_checkout', description:'Late check-out charge',
      quantity:1, unit_amount:charge, amount:charge, business_date:todayISO(), created_by:user!.id
    });

    showToast(`Late checkout charge added: ${formatIDR(charge)}`,'success');
    setLateWarning(false);
    const { data:items } = await supabase.from('folio_items').select('*').eq('folio_id',folio.id).eq('voided',false).order('created_at');
    setFolioItems(items || []);
  };

  const handleContinueNoCharge = async () => {
    await supabase.from('audit_logs').insert({
      organization_id:user!.organization_id, branch_id:reservation.branch_id, user_id:user!.id,
      action:'late_checkout_no_charge', object_type:'reservation', object_id:reservation.id,
      reason:`Late checkout at ${checkoutTime} (standard: ${standardTime}) - no charge applied`
    });
    setLateWarning(false);
    showToast('Continued without charge (logged)','info');
  };

  const completeCheckout = async () => {
    if(hasUnpaid && !overrideUnpaid){ setShowOverrideConfirm(true); return; }
    setCompleting(true);

    try {
      if(!folio) throw new Error('Folio not found');

      const { data: latestItems, error: itemsError } = await supabase.from('folio_items').select('*').eq('folio_id', folio.id).eq('voided', false);
      if(itemsError) throw itemsError;

      const items = latestItems || [];
      const tCharges = items.filter(i => i.item_type === 'charge').reduce((sum,i)=>sum + Number(i.amount || 0), 0);
      const tPayments = items.filter(i => i.item_type === 'payment').reduce((sum,i)=>sum + Math.abs(Number(i.amount || 0)), 0);
      const outstanding = tCharges - tPayments;

      if(outstanding > 0 && !overrideUnpaid){ setShowOverrideConfirm(true); setCompleting(false); return; }

      if(room){
        const {error:roomError} = await supabase.from('rooms').update({ status:'dirty' }).eq('id',room.id);
        if(roomError) throw roomError;
      }

      const {error:folioError} = await supabase.from('folios').update({
        status:'finalized', finalized_at: new Date().toISOString(), finalized_by: user!.id
      }).eq('id',folio.id);
      if(folioError) throw folioError;

      try {
        await invoiceService.ensureInvoice({
          folioId: folio.id, branchId: reservation.branch_id,
          organizationId: user!.organization_id, reservationId: reservation.id,
          guestId: reservation.primary_guest_id, userId: user!.id,
        });
      } catch(err:any) {
        console.error('Invoice sync at checkout failed:', err);
        showToast(`Invoice update failed: ${err.message || err}`, 'error');
      }

      const {error:reservationError} = await supabase.from('reservations').update({
        status:'checked_out', actual_check_out: `${todayISO()}T${checkoutTime}:00`, check_out_time: checkoutTime
      }).eq('id', reservation.id);
      if(reservationError) throw reservationError;

      try { await getLockProvider().invalidateGuestCard({ cardId:reservation.id }); } catch(e) { console.warn('Card invalidation failed', e); }

      await supabase.from('audit_logs').insert({
        organization_id: user!.organization_id, branch_id: reservation.branch_id, user_id: user!.id,
        action: 'check_out', object_type: 'reservation', object_id: reservation.id,
        new_value: { checkout_time: checkoutTime, total_charges: tCharges, total_payments: tPayments, balance: outstanding }
      });

      showToast(t('checkout.complete'), 'success');
      setCompleting(false);
      onClose();
    } catch(err:any) {
      console.error('CHECKOUT ERROR', err);
      showToast(err.message || 'Checkout failed', 'error');
      setCompleting(false);
    }
  };

  if(loading) return <Modal open onClose={onClose} title={t('checkout.title')}><LoadingPage/></Modal>;

  return (<>
    <Modal open onClose={onClose} title={t('checkout.title')} size="lg">
      <div className="space-y-4">
        <div className="grid grid-cols-2 gap-4 text-sm">
          <div><span className="text-slate-500">{t('common.guest')}:</span> <span className="font-medium">{guest?.full_name||'-'}</span></div>
          <div><span className="text-slate-500">{t('common.room')}:</span> <span className="font-medium">{room?.room_number||'-'}</span></div>
        </div>

        <div>
          <label className="text-sm font-medium text-slate-700">{t('checkout.requested_time')}</label>
          <input type="time" value={checkoutTime} onChange={e=>setCheckoutTime(e.target.value)} className="mt-1 rounded-lg border border-slate-300 px-3 py-2 text-sm"/>
        </div>

        {lateWarning && (() => {
          const today = todayISO();
          const daysLate = Math.max(0, Math.round((new Date(today).getTime() - new Date(reservation.check_out_date).getTime()) / 86400000));
          const timeLate = reservation.check_out_date === today ? timeDiff(standardTime, checkoutTime) : 0;
          return (
          <div className="bg-amber-50 border border-amber-300 rounded-lg p-4 space-y-3">
            <div className="flex items-center gap-2 text-amber-700 font-medium"><AlertCircle size={18}/>{t('checkout.late_warning')}</div>
            <div className="text-sm text-slate-600">
              <div>{t('checkout.standard_time')}: <span className="font-medium">{formatDate(reservation.check_out_date)} {standardTime}</span></div>
              <div>{t('checkout.requested_time')}: <span className="font-medium">{formatDate(today)} {checkoutTime}</span></div>
              <div>{t('checkin.difference')}: <span className="font-medium">{daysLate > 0 ? `+${daysLate} day(s)` : ''} {timeLate > 0 ? formatHoursShort(timeLate) : ''}</span></div>
            </div>
            <div className="flex gap-2">
              <Button size="sm" variant="warning" onClick={handleAddLateCharge}>{t('checkin.add_charge')}</Button>
              <Button size="sm" variant="outline" onClick={handleContinueNoCharge}>{t('checkin.continue_no_charge')}</Button>
            </div>
          </div>
          );
        })()}

        <div className="border border-slate-200 rounded-lg overflow-hidden">
          <table className="w-full text-sm">
            <tbody>
              <tr className="border-b border-slate-100"><td className="py-2 px-3 text-slate-500">{t('checkout.room_charges')}</td><td className="text-right py-2 px-3 font-medium">{formatIDR(charges.filter(i=>i.category==='room').reduce((s,i)=>s+i.amount,0))}</td></tr>
              <tr className="border-b border-slate-100"><td className="py-2 px-3 text-slate-500">{t('checkout.amenities')}</td><td className="text-right py-2 px-3 font-medium">{formatIDR(charges.filter(i=>i.category==='amenity').reduce((s,i)=>s+i.amount,0))}</td></tr>
              <tr className="border-b border-slate-100"><td className="py-2 px-3 text-slate-500">{t('checkout.additional_charges')}</td><td className="text-right py-2 px-3 font-medium">{formatIDR(charges.filter(i=>!['room','amenity','early_checkin','deposit','late_checkout','damage'].includes(i.category)).reduce((s,i)=>s+i.amount,0))}</td></tr>
              <tr className="border-b border-slate-100"><td className="py-2 px-3 text-slate-500">{t('checkout.early_checkin_charges')}</td><td className="text-right py-2 px-3 font-medium">{formatIDR(charges.filter(i=>i.category==='early_checkin').reduce((s,i)=>s+i.amount,0))}</td></tr>
              <tr className="border-b border-slate-100"><td className="py-2 px-3 text-slate-500">{t('checkout.late_checkout_charges')}</td><td className="text-right py-2 px-3 font-medium">{formatIDR(charges.filter(i=>i.category==='late_checkout').reduce((s,i)=>s+i.amount,0))}</td></tr>
              <tr className="border-b border-slate-100"><td className="py-2 px-3 text-slate-500">{t('common.discount')}</td><td className="text-right py-2 px-3 font-medium text-red-600">-{formatIDR(totalDiscounts)}</td></tr>
              <tr className="border-b border-slate-100"><td className="py-2 px-3 text-slate-500">{t('common.tax')}</td><td className="text-right py-2 px-3 font-medium">{formatIDR(totalTax)}</td></tr>
              <tr className="border-b border-slate-100"><td className="py-2 px-3 text-slate-500">{t('common.deposit')}</td><td className="text-right py-2 px-3 font-medium text-emerald-600">{formatIDR(charges.filter(i=>i.category==='deposit').reduce((s,i)=>s+i.amount,0))}</td></tr>
              <tr className="border-b border-slate-100"><td className="py-2 px-3 text-slate-500">{t('checkout.amount_paid')}</td><td className="text-right py-2 px-3 font-medium">{formatIDR(totalPayments)}</td></tr>
              <tr className="bg-slate-50 font-bold"><td className="py-2 px-3">{t('common.balance')}</td><td className={`text-right py-2 px-3 ${hasUnpaid?'text-red-600':'text-emerald-600'}`}>{formatIDR(Math.abs(balance))}{hasUnpaid?' due':' settled'}</td></tr>
            </tbody>
          </table>
        </div>

        {hasUnpaid && !overrideUnpaid && (
          <div className="bg-red-50 border border-red-200 rounded-lg p-3 flex items-center gap-2 text-red-700">
            <AlertCircle size={18}/><span className="text-sm">{t('checkout.unpaid_balance_warning')}</span>
          </div>
        )}

        {overrideUnpaid && (
          <div className="bg-amber-50 border border-amber-200 rounded-lg p-3 text-amber-700 text-sm">
            Override applied — checkout will proceed with unpaid balance (logged to audit).
          </div>
        )}

        <div className="flex justify-end gap-2">
          <Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button>
          <Button loading={completing} variant={hasUnpaid?'danger':'success'} onClick={completeCheckout}><LogOut size={16}/>{t('checkout.complete')}</Button>
        </div>
      </div>
    </Modal>
    <ConfirmModal
      open={showOverrideConfirm}
      onClose={()=>setShowOverrideConfirm(false)}
      onConfirm={()=>{ setOverrideUnpaid(true); setShowOverrideConfirm(false); }}
      title={t('checkout.unpaid_balance')}
      message={t('checkout.unpaid_balance_warning')}
      confirmLabel={t('checkout.override')}
      variant="danger"
    />
  </>);
}

function ExtendStayModal({ reservation, onClose }: { reservation: Reservation; onClose: () => void }) {
  const { user } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();

  const [guest, setGuest] = useState<Guest | null>(null);
  const [room, setRoom] = useState<Room | null>(null);
  const [folio, setFolio] = useState<Folio | null>(null);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);

  const previousCheckoutDate = reservation.check_out_date;
  const [newCheckoutDate, setNewCheckoutDate] = useState(addDays(previousCheckoutDate, 1));
  const [roomRate, setRoomRate] = useState(String(reservation.rate));

  useEffect(() => {
    (async () => {
      setLoading(true);
      const [{ data: g }, { data: r }, { data: f }] = await Promise.all([
        supabase.from('guests').select('*').eq('id', reservation.primary_guest_id).maybeSingle(),
        reservation.room_id ? supabase.from('rooms').select('*').eq('id', reservation.room_id).maybeSingle() : Promise.resolve({ data: null }),
        supabase.from('folios').select('*').eq('reservation_id', reservation.id).maybeSingle()
      ]);
      setGuest(g as Guest);
      setRoom(r as Room);
      setFolio(f as Folio);
      setLoading(false);
    })();
  }, [reservation]);

  // Calculation: extra nights = new checkout date - previous checkout date
  const extraNights = Math.max(0, nightsBetween(previousCheckoutDate, newCheckoutDate));
  const additionalCharge = Number(roomRate) * extraNights;

  const handleExtend = async () => {
    if (newCheckoutDate <= previousCheckoutDate) {
      showToast('New check-out date must be after the current check-out date.', 'error');
      return;
    }
    if (Number(roomRate) < 0) {
      showToast('Room rate cannot be negative.', 'error');
      return;
    }
    if (extraNights <= 0) {
      showToast('No additional nights to charge.', 'error');
      return;
    }

    setSaving(true);

    try {
      // Check room availability for the extended dates
      if (reservation.room_id) {
        const { data: conflicts } = await supabase
          .from('reservations')
          .select('id,reservation_number')
          .eq('room_id', reservation.room_id)
          .in('status', ['confirmed', 'checked_in'])
          .neq('id', reservation.id)
          .lt('check_in_date', newCheckoutDate)
          .gt('check_out_date', previousCheckoutDate);
        if (conflicts && conflicts.length > 0) {
          showToast(`Room is already booked for the extended dates by ${conflicts[0].reservation_number}.`, 'error');
          setSaving(false);
          return;
        }
      }

      // Update reservation with new checkout date and num_nights
      const totalNights = nightsBetween(reservation.check_in_date, newCheckoutDate);
      const { error: resError } = await supabase.from('reservations').update({
        check_out_date: newCheckoutDate,
        num_nights: totalNights,
      }).eq('id', reservation.id);

      if (resError) throw resError;

      // Add room charge for extra nights to folio
      if (folio) {
        const { error: chargeError } = await supabase.from('folio_items').insert({
          folio_id: folio.id,
          branch_id: reservation.branch_id,
          reservation_id: reservation.id,
          guest_id: reservation.primary_guest_id,
          room_id: reservation.room_id,
          item_type: 'charge',
          category: 'room',
          description: `Extended stay - ${extraNights} extra night(s) at ${formatIDR(Number(roomRate))}/night`,
          quantity: extraNights,
          unit_amount: Number(roomRate),
          amount: additionalCharge,
          business_date: todayISO(),
          created_by: user!.id,
        });

        if (chargeError) throw chargeError;

        // Update folio totals
        const { data: allItems } = await supabase.from('folio_items').select('item_type,amount').eq('folio_id', folio.id).eq('voided', false);
        const totals = { total_charges: 0, total_payments: 0, total_discounts: 0, total_tax: 0 };
        (allItems || []).forEach((item: any) => {
          switch (item.item_type) {
            case 'charge': totals.total_charges += Number(item.amount); break;
            case 'payment': totals.total_payments += Math.abs(Number(item.amount)); break;
            case 'discount': totals.total_discounts += Math.abs(Number(item.amount)); break;
            case 'tax': totals.total_tax += Number(item.amount); break;
          }
        });

        await supabase.from('folios').update({
          total_charges: totals.total_charges,
          total_payments: totals.total_payments,
          total_discounts: totals.total_discounts,
          total_tax: totals.total_tax,
          balance: totals.total_charges - totals.total_discounts + totals.total_tax - totals.total_payments,
        }).eq('id', folio.id);
      }

      // Update reservation_rooms if group
      if (reservation.is_group) {
        await supabase.from('reservation_rooms')
          .update({ check_out_date: newCheckoutDate, num_nights: totalNights })
          .eq('reservation_id', reservation.id)
          .eq('status', 'active');
      }

      // Audit log
      await supabase.from('audit_logs').insert({
        organization_id: user!.organization_id,
        branch_id: reservation.branch_id,
        user_id: user!.id,
        action: 'extend_stay',
        object_type: 'reservation',
        object_id: reservation.id,
        previous_value: { check_out_date: previousCheckoutDate },
        new_value: { check_out_date: newCheckoutDate, extra_nights: extraNights, additional_charge: additionalCharge, room_rate: Number(roomRate) },
      });

      showToast(`Stay extended by ${extraNights} night(s). Additional charge: ${formatIDR(additionalCharge)}`, 'success');
      setSaving(false);
      onClose();
    } catch (err: any) {
      console.error('Extend stay error:', err);
      showToast(err.message || 'Failed to extend stay', 'error');
      setSaving(false);
    }
  };

  if (loading) return <Modal open onClose={onClose} title={t('res.extend_stay')}><LoadingPage /></Modal>;

  return (
    <Modal open onClose={onClose} title={t('res.extend_stay')} size="md"
      footer={<><Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button><Button loading={saving} onClick={handleExtend}><CalendarPlus size={16} />{t('common.confirm')}</Button></>}>
      <div className="space-y-4">
        <div className="grid grid-cols-2 gap-4 text-sm">
          <div><span className="text-slate-500">{t('common.guest')}:</span> <span className="font-medium">{guest?.full_name || '-'}</span></div>
          <div><span className="text-slate-500">{t('common.room')}:</span> <span className="font-medium">{room?.room_number || '-'}</span></div>
          <div><span className="text-slate-500">{t('res.prev_checkout')}:</span> <span className="font-medium">{formatDate(previousCheckoutDate)}</span></div>
          <div><span className="text-slate-500">{t('common.check_in')}:</span> <span className="font-medium">{formatDate(reservation.check_in_date)}</span></div>
        </div>

        <Input label={t('res.new_checkout_date')} type="date" value={newCheckoutDate} onChange={e => setNewCheckoutDate(e.target.value)} required />
        <Input label={t('res.room_rate_per_night')} type="number" value={roomRate} onChange={e => setRoomRate(e.target.value)} required />

        <div className="bg-blue-50 border border-blue-200 rounded-lg p-4 space-y-2 text-sm">
          <div className="flex justify-between"><span className="text-slate-600">{t('res.prev_checkout')}:</span> <span className="font-medium">{formatDate(previousCheckoutDate)}</span></div>
          <div className="flex justify-between"><span className="text-slate-600">{t('res.new_checkout')}:</span> <span className="font-medium">{formatDate(newCheckoutDate)}</span></div>
          <div className="flex justify-between"><span className="text-slate-600">{t('res.extra_nights')}:</span> <span className="font-bold text-blue-700">{extraNights} {t('common.nights')}</span></div>
          <div className="flex justify-between"><span className="text-slate-600">{t('common.rate')}:</span> <span className="font-medium">{formatIDR(Number(roomRate) || 0)} / {t('common.nights')}</span></div>
          <div className="pt-2 border-t border-blue-200 flex justify-between"><span className="text-slate-700 font-medium">{t('res.additional_charge')}:</span> <span className="font-bold text-blue-700 text-lg">{formatIDR(additionalCharge)}</span></div>
        </div>

        {extraNights === 0 && (
          <div className="bg-amber-50 border border-amber-200 rounded-lg p-3 text-amber-700 text-sm">
            {t('res.no_extra_nights')}
          </div>
        )}
      </div>
    </Modal>
  );
}

function SplitRoomModal({ reservation, reservationRooms, rooms, onClose }: {
  reservation: Reservation;
  reservationRooms: ReservationRoom[];
  rooms: Room[];
  onClose: () => void;
}) {
  const { user } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [selectedRoomId, setSelectedRoomId] = useState('');

  const roomMap = useMemo(() => new Map(rooms.map(r => [r.id, r])), [rooms]);

  const handleSplit = async () => {
    if (!selectedRoomId) {
      showToast('Please select a room to split.', 'error');
      return;
    }

    setSaving(true);

    try {
      const rrToSplit = reservationRooms.find(rr => rr.room_id === selectedRoomId);
      if (!rrToSplit) {
        showToast('Selected room not found in this reservation.', 'error');
        setSaving(false);
        return;
      }

      const newResNum = await generateDocumentNumber('RES');

      // Create new reservation for the split room
      const { data: newRes, error: newResError } = await supabase.from('reservations').insert({
        branch_id: reservation.branch_id,
        organization_id: reservation.organization_id,
        reservation_number: newResNum,
        primary_guest_id: reservation.primary_guest_id,
        room_type_id: rrToSplit.room_type_id,
        room_id: rrToSplit.room_id,
        adults: reservation.adults,
        children: reservation.children,
        check_in_date: rrToSplit.check_in_date,
        check_in_time: reservation.check_in_time,
        check_out_date: rrToSplit.check_out_date,
        check_out_time: reservation.check_out_time,
        actual_check_in: reservation.actual_check_in,
        num_nights: rrToSplit.num_nights,
        rate: rrToSplit.rate,
        discount: 0,
        tax: 0,
        deposit: 0,
        booking_source_id: reservation.booking_source_id,
        payment_status: 'unpaid',
        status: reservation.status,
        parent_reservation_id: reservation.id,
        is_group: false,
        created_by: user!.id,
      }).select().single();

      if (newResError) throw newResError;

      // Mark the reservation_rooms row as split
      await supabase.from('reservation_rooms')
        .update({ status: 'split' })
        .eq('id', rrToSplit.id);

      // Create a folio for the new reservation
      const { error: folioError } = await supabase.from('folios').insert({
        branch_id: reservation.branch_id,
        reservation_id: newRes.id,
        guest_id: reservation.primary_guest_id,
        folio_number: `FOL-${newResNum.replace('RES-', '')}`,
        status: 'open',
      });

      if (folioError) {
        showToast(`Reservation split but folio creation failed: ${folioError.message}`, 'warning');
      }

      // If the original reservation now has only 1 active room, remove group flag
      const { data: remainingRooms } = await supabase.from('reservation_rooms')
        .select('*')
        .eq('reservation_id', reservation.id)
        .eq('status', 'active');

      if ((remainingRooms || []).length <= 1) {
        const lastRoom = (remainingRooms || [])[0];
        await supabase.from('reservations').update({
          is_group: false,
          room_id: lastRoom?.room_id || reservation.room_id,
          room_type_id: lastRoom?.room_type_id || reservation.room_type_id,
          rate: lastRoom?.rate || reservation.rate,
        }).eq('id', reservation.id);
      }

      // Audit log
      await supabase.from('audit_logs').insert({
        organization_id: reservation.organization_id,
        branch_id: reservation.branch_id,
        user_id: user!.id,
        action: 'split_room',
        object_type: 'reservation',
        object_id: reservation.id,
        new_value: { new_reservation_id: newRes.id, new_reservation_number: newResNum, split_room_id: selectedRoomId },
      });

      showToast(`Room split into new reservation ${newResNum}.`, 'success');
      setSaving(false);
      onClose();
    } catch (err: any) {
      console.error('Split room error:', err);
      showToast(err.message || 'Failed to split room', 'error');
      setSaving(false);
    }
  };

  return (
    <Modal open onClose={onClose} title={t('res.split_room')} size="md"
      footer={<><Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button><Button loading={saving} onClick={handleSplit}><Split size={16} />{t('common.confirm')}</Button></>}>
      <div className="space-y-4">
        <div className="bg-blue-50 border border-blue-200 rounded-lg p-3 text-sm text-blue-700">
          {t('res.split_room_desc')}
        </div>

        <div className="text-sm text-slate-600">
          <p><span className="text-slate-500">{t('res.reservation_number')}:</span> <span className="font-medium">{reservation.reservation_number}</span></p>
        </div>

        <div className="space-y-2">
          {reservationRooms.length === 0 ? (
            <p className="text-sm text-slate-400">{t('res.no_group_rooms')}</p>
          ) : (
            reservationRooms.map(rr => {
              const room = roomMap.get(rr.room_id || '');
              return (
                <label key={rr.id} className={`flex items-center gap-3 p-3 rounded-lg border cursor-pointer transition-colors ${selectedRoomId === rr.room_id ? 'border-blue-500 bg-blue-50' : 'border-slate-200 hover:bg-slate-50'}`}>
                  <input
                    type="radio"
                    name="splitRoom"
                    value={rr.room_id}
                    checked={selectedRoomId === rr.room_id}
                    onChange={e => setSelectedRoomId(e.target.value)}
                  />
                  <div className="flex-1">
                    <p className="font-medium text-slate-800">{room?.room_number || 'Unassigned'}</p>
                    <p className="text-xs text-slate-500">{formatIDR(rr.rate)}/{t('common.nights')} · {formatDate(rr.check_in_date)} → {formatDate(rr.check_out_date)}</p>
                  </div>
                </label>
              );
            })
          )}
        </div>

        {selectedRoomId && (
          <div className="bg-amber-50 border border-amber-200 rounded-lg p-3 text-sm text-amber-700">
            {t('res.split_room_confirm')}
          </div>
        )}
      </div>
    </Modal>
  );
}

function timeDiff(standard:string,actual:string):number {
  const [sh,sm]=standard.split(':').map(Number);
  const [ah,am]=actual.split(':').map(Number);
  return ((ah*60+am)-(sh*60+sm))/60;
}
