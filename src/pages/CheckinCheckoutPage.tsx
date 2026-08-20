import { useState, useEffect, useCallback, useMemo } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal, ConfirmModal } from '@/components/ui/Modal';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatIDR, formatDate, formatTime, todayISO, isEarlyCheckin, isLateCheckout, formatHoursShort } from '@/lib/format';
import { getLockProvider } from '@/lib/hotel-lock/provider';
import { LogIn, LogOut, KeyRound, AlertCircle, CheckCircle2, Loader2 } from 'lucide-react';
import type { Reservation, Guest, Room, Folio } from '@/types/database';

export function CheckinCheckoutPage({ initialReservationId, searchQuery }: { initialReservationId?: string | null; searchQuery?: string }) {
  const { branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();

  const [reservations, setReservations] = useState<Reservation[]>([]);
  const [guests, setGuests] = useState<Guest[]>([]);
  const [rooms, setRooms] = useState<Room[]>([]);
  const [loading, setLoading] = useState(true);
  const [selected, setSelected] = useState<Reservation | null>(null);
  const [mode, setMode] = useState<'checkin' | 'checkout' | null>(null);

  const branchIds = useMemo(() => selectedBranchId ? [selectedBranchId] : branches.map(b => b.id), [selectedBranchId, branches]);

  const load = useCallback(async () => {
    if (!branchIds.length) return setLoading(false);
    setLoading(true);

    const [{ data: res }, { data: g }, { data: r }] = await Promise.all([
      supabase.from('reservations').select('*').in('branch_id', branchIds).in('status', ['confirmed', 'checked_in']).order('check_in_date'),
      supabase.from('guests').select('*'),
      supabase.from('rooms').select('*').in('branch_id', branchIds)
    ]);

    setReservations((res as Reservation[]) || []);
    setGuests((g as Guest[]) || []);
    setRooms((r as Room[]) || []);
    setLoading(false);
  }, [branchIds]);

  useEffect(() => { load(); }, [load]);

  useEffect(() => {
    if (!initialReservationId) return;
    const r = reservations.find(x => x.id === initialReservationId);
    if (r) {
      setSelected(r);
      setMode(r.status === 'checked_in' ? 'checkout' : 'checkin');
    }
  }, [initialReservationId, reservations]);

  const guestMap = useMemo(() => new Map(guests.map(g => [g.id, g])), [guests]);
  const roomMap = useMemo(() => new Map(rooms.map(r => [r.id, r])), [rooms]);

  const arrivals = reservations.filter(r => r.status === 'confirmed');
  const departures = reservations.filter(r => r.status === 'checked_in');

  const q = (searchQuery || '').toLowerCase().trim();

  const filterFn = (r: Reservation) => {
    if (!q) return true;
    const g = guestMap.get(r.primary_guest_id || '');
    const rm = roomMap.get(r.room_id || '');
    return r.reservation_number.toLowerCase().includes(q) || (g?.full_name || '').toLowerCase().includes(q) || (rm?.room_number || '').includes(q);
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
                <p className="font-medium text-slate-800">{g?.full_name || '-'}</p>
                <p className="text-xs text-slate-500">{r.reservation_number} · {rm?.room_number || 'Unassigned'} · {formatDate(r.check_in_date)} {formatTime(r.check_in_time)}</p>
              </div>
              <Button size="sm" onClick={() => { setSelected(r); setMode('checkin'); }}><LogIn size={14}/>{t('action.check_in')}</Button>
            </div>;
          })}</div>}
        </Card>

        <Card title={`${t('dash.departures_today')} / ${t('res.checked_in')}`}>
          {departures.filter(filterFn).length === 0 ? <EmptyState title={t('common.no_data')} /> :
          <div className="space-y-2">{departures.filter(filterFn).map(r => {
            const g = guestMap.get(r.primary_guest_id || '');
            const rm = roomMap.get(r.room_id || '');
            return <div key={r.id} className="flex items-center justify-between border border-slate-100 rounded-lg px-3 py-2 hover:bg-slate-50">
              <div>
                <p className="font-medium text-slate-800">{g?.full_name || '-'}</p>
                <p className="text-xs text-slate-500">{r.reservation_number} · {rm?.room_number || '-'} · {formatDate(r.check_out_date)} {formatTime(r.check_out_time)}</p>
              </div>
              <Button size="sm" variant="warning" onClick={() => { setSelected(r); setMode('checkout'); }}><LogOut size={14}/>{t('action.check_out')}</Button>
            </div>;
          })}</div>}
        </Card>
      </div>

      {selected && mode === 'checkin' && <CheckinModal reservation={selected} onClose={() => { setSelected(null); setMode(null); load(); }} />}
      {selected && mode === 'checkout' && <CheckoutModal reservation={selected} onClose={() => { setSelected(null); setMode(null); load(); }} />}
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
    setEarlyWarning(isEarlyCheckin(standardTime, checkinTime));
  }, [standardTime, checkinTime]);

  const handleAddEarlyCharge = async () => {
    if (!folio) return;

    const { data } = await supabase.from('system_settings').select('value').eq('key','early_checkin_charge').maybeSingle();
    const charge = parseFloat(data?.value || '0');

    if (charge <= 0) return showToast('No early check-in charge configured','warning');

    await supabase.from('folio_items').insert({
      folio_id: folio.id,
      branch_id: reservation.branch_id,
      reservation_id: reservation.id,
      guest_id: reservation.primary_guest_id,
      room_id: reservation.room_id,
      item_type:'charge',
      category:'early_checkin',
      description:'Early check-in charge',
      quantity:1,
      unit_amount:charge,
      amount:charge,
      business_date:todayISO(),
      created_by:user!.id
    });

    await supabase.from('transactions').insert({
      branch_id:reservation.branch_id,
      organization_id:user!.organization_id,
      reservation_id:reservation.id,
      guest_id:reservation.primary_guest_id,
      folio_id:folio.id,
      transaction_type:'early_checkin_charge',
      description:'Early check-in charge',
      amount:charge,
      debit_credit:'debit',
      business_date:todayISO(),
      created_by:user!.id
    });

    showToast(`Early check-in charge added: ${formatIDR(charge)}`,'success');
    setEarlyWarning(false);
  };

  const handleContinueNoCharge = async () => {
    await supabase.from('audit_logs').insert({
      organization_id:user!.organization_id,
      branch_id:reservation.branch_id,
      user_id:user!.id,
      action:'early_checkin_no_charge',
      object_type:'reservation',
      object_id:reservation.id,
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
      roomId:room.id,
      roomNumber:room.room_number,
      guestName:guest.full_name,
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
        branch_id:reservation.branch_id,
        reservation_id:reservation.id,
        guest_id:guest.id,
        room_id:room.id,
        issuance_type:'issue',
        card_sequence:1,
        valid_from:`${reservation.check_in_date}T${checkinTime}`,
        valid_until:`${reservation.check_out_date}T${reservation.check_out_time}`,
        status:'success',
        provider_type:'mock',
        performed_by:user!.id
      });
    } else {
      setCardState('failed');
      setCardMessage(result.message);

      await supabase.from('card_issuances').insert({
        branch_id:reservation.branch_id,
        reservation_id:reservation.id,
        guest_id:guest.id,
        room_id:room.id,
        issuance_type:'issue',
        card_sequence:1,
        valid_from:`${reservation.check_in_date}T${checkinTime}`,
        valid_until:`${reservation.check_out_date}T${reservation.check_out_time}`,
        status:'failed',
        failure_reason:result.message,
        provider_type:'mock',
        performed_by:user!.id
      });
    }
  };

  const completeCheckin = async () => {
    setCompleting(true);

    const { error } = await supabase.from('reservations').update({
      status:'checked_in',
      actual_check_in:`${todayISO()}T${checkinTime}:00`,
      check_in_time:checkinTime
    }).eq('id',reservation.id);

    if (error) {
      showToast(error.message,'error');
      return setCompleting(false);
    }

    if (room) await supabase.from('rooms').update({status:'occupied'}).eq('id',room.id);

    await supabase.from('audit_logs').insert({
      organization_id:user!.organization_id,
      branch_id:reservation.branch_id,
      user_id:user!.id,
      action:'check_in',
      object_type:'reservation',
      object_id:reservation.id,
      new_value:{checkin_time:checkinTime}
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
              <div>{t('checkin.standard_time')}: <span className="font-medium">{standardTime}</span></div>
              <div>{t('checkin.actual_time')}: <span className="font-medium">{checkinTime}</span></div>
              <div>{t('checkin.difference')}: <span className="font-medium">{formatHoursShort(timeDiff(standardTime,checkinTime))}</span></div>
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

  useEffect(()=>setLateWarning(isLateCheckout(standardTime,checkoutTime)),[standardTime,checkoutTime]);

  const charges=folioItems.filter(i=>i.item_type==='charge'&&i.amount>0);
  const payments=folioItems.filter(i=>i.item_type==='payment');
  const discounts=folioItems.filter(i=>i.item_type==='discount');
  const taxes=folioItems.filter(i=>i.item_type==='tax');

  const totalCharges=charges.reduce((s,i)=>s+i.amount,0);
  const totalPayments=payments.reduce((s,i)=>s+Math.abs(i.amount),0);
  const totalDiscounts=discounts.reduce((s,i)=>s+Math.abs(i.amount),0);
  const totalTax=taxes.reduce((s,i)=>s+i.amount,0);

  const balance=totalCharges+totalTax-totalDiscounts-totalPayments-(reservation.deposit||0);
  const hasUnpaid=balance>0;
  const canOverride=user?.role==='super_admin'||user?.role==='manager';
    const handleAddLateCharge = async () => {
    if (!folio) return;

    const { data } = await supabase.from('system_settings').select('value').eq('key','late_checkout_charge').maybeSingle();
    const charge = parseFloat(data?.value || '0');

    if (charge <= 0) return showToast('No late checkout charge configured','warning');

    await supabase.from('folio_items').insert({
      folio_id:folio.id,
      branch_id:reservation.branch_id,
      reservation_id:reservation.id,
      guest_id:reservation.primary_guest_id,
      room_id:reservation.room_id,
      item_type:'charge',
      category:'late_checkout',
      description:'Late check-out charge',
      quantity:1,
      unit_amount:charge,
      amount:charge,
      business_date:todayISO(),
      created_by:user!.id
    });

    showToast(`Late checkout charge added: ${formatIDR(charge)}`,'success');
    setLateWarning(false);

    const { data:items } = await supabase.from('folio_items').select('*').eq('folio_id',folio.id).eq('voided',false).order('created_at');
    setFolioItems(items || []);
  };

  const handleContinueNoCharge = async () => {
    await supabase.from('audit_logs').insert({
      organization_id:user!.organization_id,
      branch_id:reservation.branch_id,
      user_id:user!.id,
      action:'late_checkout_no_charge',
      object_type:'reservation',
      object_id:reservation.id,
      reason:`Late checkout at ${checkoutTime} (standard: ${standardTime}) - no charge applied`
    });

    setLateWarning(false);
    showToast('Continued without charge (logged)','info');
  };

  const completeCheckout = async () => {
    if(hasUnpaid && !overrideUnpaid){
      setShowOverrideConfirm(true);
      return;
    }

    setCompleting(true);

    const { error } = await supabase.from('reservations').update({
      status:'checked_out',
      actual_check_out:`${todayISO()}T${checkoutTime}:00`,
      check_out_time:checkoutTime
    }).eq('id',reservation.id);

    if(error){
      showToast(error.message,'error');
      return setCompleting(false);
    }

    if(room) await supabase.from('rooms').update({status:'dirty'}).eq('id',room.id);

    if(folio) await supabase.from('folios').update({
      status:'finalized',
      finalized_at:new Date().toISOString(),
      finalized_by:user!.id
    }).eq('id',folio.id);

    await getLockProvider().invalidateGuestCard({cardId:reservation.id});

    await supabase.from('audit_logs').insert({
      organization_id:user!.organization_id,
      branch_id:reservation.branch_id,
      user_id:user!.id,
      action:'check_out',
      object_type:'reservation',
      object_id:reservation.id,
      new_value:{checkout_time:checkoutTime,balance}
    });

    showToast(t('checkout.complete'),'success');
    setCompleting(false);
    onClose();
  };

  if(loading) return <Modal open onClose={onClose} title={t('checkout.title')}><LoadingPage/></Modal>;

  return (
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

        {lateWarning && (
          <div className="bg-amber-50 border border-amber-300 rounded-lg p-4 space-y-3">
            <div className="flex items-center gap-2 text-amber-700 font-medium"><AlertCircle size={18}/>{t('checkout.late_warning')}</div>

            <div className="text-sm text-slate-600">
              <div>{t('checkout.standard_time')}: <span className="font-medium">{standardTime}</span></div>
              <div>{t('checkout.requested_time')}: <span className="font-medium">{checkoutTime}</span></div>
              <div>{t('checkin.difference')}: <span className="font-medium">{formatHoursShort(timeDiff(standardTime,checkoutTime))}</span></div>
            </div>

            <div className="flex gap-2">
              <Button size="sm" variant="warning" onClick={handleAddLateCharge}>{t('checkin.add_charge')}</Button>
              <Button size="sm" variant="outline" onClick={handleContinueNoCharge}>{t('checkin.continue_no_charge')}</Button>
            </div>
          </div>
        )}

        <div className="border border-slate-200 rounded-lg overflow-hidden">
          <table className="w-full text-sm">
            <tbody>
              <tr className="border-b border-slate-100">
                <td className="py-2 px-3 text-slate-500">{t('checkout.room_charges')}</td>
                <td className="text-right py-2 px-3 font-medium">{formatIDR(charges.filter(i=>i.category==='room').reduce((s,i)=>s+i.amount,0))}</td>
              </tr>

              <tr className="border-b border-slate-100">
                <td className="py-2 px-3 text-slate-500">{t('checkout.amenities')}</td>
                <td className="text-right py-2 px-3 font-medium">{formatIDR(charges.filter(i=>i.category==='amenity').reduce((s,i)=>s+i.amount,0))}</td>
              </tr>

              <tr className="border-b border-slate-100">
                <td className="py-2 px-3 text-slate-500">{t('checkout.additional_charges')}</td>
                <td className="text-right py-2 px-3 font-medium">{formatIDR(charges.filter(i=>!['room','amenity','early_checkin','late_checkout','damage'].includes(i.category)).reduce((s,i)=>s+i.amount,0))}</td>
              </tr>

              <tr className="border-b border-slate-100">
                <td className="py-2 px-3 text-slate-500">{t('checkout.early_checkin_charges')}</td>
                <td className="text-right py-2 px-3 font-medium">{formatIDR(charges.filter(i=>i.category==='early_checkin').reduce((s,i)=>s+i.amount,0))}</td>
              </tr>

              <tr className="border-b border-slate-100">
                <td className="py-2 px-3 text-slate-500">{t('checkout.late_checkout_charges')}</td>
                <td className="text-right py-2 px-3 font-medium">{formatIDR(charges.filter(i=>i.category==='late_checkout').reduce((s,i)=>s+i.amount,0))}</td>
              </tr>

              <tr className="border-b border-slate-100">
                <td className="py-2 px-3 text-slate-500">{t('common.discount')}</td>
                <td className="text-right py-2 px-3 font-medium text-red-600">-{formatIDR(totalDiscounts)}</td>
              </tr>

              <tr className="border-b border-slate-100">
                <td className="py-2 px-3 text-slate-500">{t('common.tax')}</td>
                <td className="text-right py-2 px-3 font-medium">{formatIDR(totalTax)}</td>
              </tr>

              <tr className="border-b border-slate-100">
                <td className="py-2 px-3 text-slate-500">{t('common.deposit')}</td>
                <td className="text-right py-2 px-3 font-medium text-emerald-600">{formatIDR(reservation.deposit)}</td>
              </tr>

              <tr className="border-b border-slate-100">
                <td className="py-2 px-3 text-slate-500">{t('checkout.amount_paid')}</td>
                <td className="text-right py-2 px-3 font-medium">{formatIDR(totalPayments)}</td>
              </tr>

              <tr className="bg-slate-50 font-bold">
                <td className="py-2 px-3">{t('common.balance')}</td>
                <td className={`text-right py-2 px-3 ${hasUnpaid?'text-red-600':'text-emerald-600'}`}>{formatIDR(Math.abs(balance))}{hasUnpaid?' due':' settled'}</td>
              </tr>
            </tbody>
          </table>
        </div>

                {hasUnpaid && !overrideUnpaid && (
          <div className="bg-red-50 border border-red-200 rounded-lg p-3 flex items-center gap-2 text-red-700">
            <AlertCircle size={18}/>
            <span className="text-sm">{t('checkout.unpaid_balance_warning')}</span>
          </div>
        )}

        {overrideUnpaid && (
          <div className="bg-amber-50 border border-amber-200 rounded-lg p-3 text-amber-700 text-sm">
            Override applied — checkout will proceed with unpaid balance (logged to audit).
          </div>
        )}

        <div className="flex justify-end gap-2">
          <Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button>
          <Button loading={completing} variant={hasUnpaid?'danger':'success'} onClick={completeCheckout}>
            <LogOut size={16}/>{t('checkout.complete')}
          </Button>
        </div>
      </div>

      <ConfirmModal
        open={showOverrideConfirm}
        onClose={()=>setShowOverrideConfirm(false)}
        onConfirm={()=>{
          setOverrideUnpaid(true);
          setShowOverrideConfirm(false);
        }}
        title={t('checkout.unpaid_balance')}
        message={t('checkout.unpaid_balance_warning')}
        confirmLabel={t('checkout.override')}
        variant="danger"
      />
    </Modal>
  );
}

function timeDiff(standard:string,actual:string):number {
  const [sh,sm]=standard.split(':').map(Number);
  const [ah,am]=actual.split(':').map(Number);
  return ((ah*60+am)-(sh*60+sm))/60;
}