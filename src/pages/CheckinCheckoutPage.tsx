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
import { formatIDR, formatDate, formatTime, formatDateTime, todayISO, todayInTimezone, nowInTimezone, addDays, nightsBetween, formatHoursShort } from '@/lib/format';
import { getLockProviderByType, integrationToConfig } from '@/lib/hotel-lock/provider';
import type { HotelLockIntegration } from '@/types/database';
import { generateDocumentNumber } from '@/lib/documentNumber';
import { calculateTotalRate, getRateTypeLabel } from '@/lib/rate-calculator';
import { folioService } from '@/services/financial';
import { getBusinessDate } from '@/services/businessDateService';
import { saveDraft, loadDraft, clearDraft } from '@/lib/formDraft';
import { LogIn, LogOut, KeyRound, CircleAlert as AlertCircle, CircleCheck as CheckCircle2, Loader as Loader2, CalendarPlus, Split, FileText, Receipt } from 'lucide-react';
import type { Reservation, Guest, Room, Folio, BookingSource, RoomType, ReservationRoom, IndonesianHoliday } from '@/types/database';

const CHECKIN_DRAFT_KEY = 'checkin_time_draft';
const CHECKOUT_DRAFT_KEY = 'checkout_time_draft';
const EXTEND_DRAFT_KEY = 'extend_stay_draft';
const SPLIT_DRAFT_KEY = 'split_room_draft';

export function CheckinCheckoutPage({ initialReservationId, searchQuery, onNavigateToPayment, onNavigateToInvoice }: { initialReservationId?: string | null; searchQuery?: string; onNavigateToPayment?: (id: string) => void; onNavigateToInvoice?: (id: string) => void }) {
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
            <div className="flex gap-1">
                <Button size="sm" onClick={() => handleSelectReservation(r, 'checkin')}
><LogIn size={14}/>{t('action.check_in')}</Button>
                {onNavigateToPayment && <Button size="sm" variant="outline" onClick={() => onNavigateToPayment(r.id)}><FileText size={14}/></Button>}
              </div>
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
                <Button size="sm" variant="outline" onClick={() => handleSelectReservation(r, 'extend')}
><CalendarPlus size={14}/>{t('res.extend_stay')}</Button>
                {r.is_group && <Button size="sm" variant="outline" onClick={() => handleSelectReservation(r, 'split')}><Split size={14}/>{t('res.split_room')}</Button>}
                {onNavigateToPayment && <Button size="sm" variant="outline" onClick={() => onNavigateToPayment(r.id)}><FileText size={14}/></Button>}
                <Button size="sm" variant="warning" onClick={() => handleSelectReservation(r, 'checkout')}
><LogOut size={14}/>{t('action.check_out')}</Button>
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
              {onNavigateToPayment && <Button size="sm" variant="outline" onClick={() => onNavigateToPayment(r.id)}><FileText size={14}/></Button>}
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
            <div className="flex items-center gap-1">
              {onNavigateToPayment && <Button size="sm" variant="outline" onClick={() => onNavigateToPayment(r.id)}><FileText size={14}/>{t('res.view_folio')}</Button>}
              {onNavigateToInvoice && <Button size="sm" variant="outline" onClick={() => onNavigateToInvoice(r.id)}><Receipt size={14}/>{t('res.view_invoice')}</Button>}
              <Badge color="gray">{t('res.checked_out')}</Badge>
            </div>
          </div>;
        })}</div>}
      </Card>

      {selected && mode === 'checkin' && <CheckinModal reservation={selected} onClose={handleCloseModal} onNavigateToPayment={onNavigateToPayment} onNavigateToInvoice={onNavigateToInvoice} />}
      {selected && mode === 'checkout' && <CheckoutModal reservation={selected} onClose={handleCloseModal} onNavigateToPayment={onNavigateToPayment} onNavigateToInvoice={onNavigateToInvoice} />}
      {selected && mode === 'extend' && <ExtendStayModal reservation={selected} onClose={handleCloseModal} />}
      {selected && mode === 'split' && <SplitRoomModal reservation={selected} reservationRooms={reservationRooms} rooms={rooms} onClose={handleCloseModal} />}
    </div>
  );
}

function CheckinModal({ reservation, onClose, onNavigateToPayment, onNavigateToInvoice }: { reservation: Reservation; onClose: () => void; onNavigateToPayment?: (id: string) => void; onNavigateToInvoice?: (id: string) => void }) {
  const { user, branches } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();

  const [guest, setGuest] = useState<Guest | null>(null);
  const [room, setRoom] = useState<Room | null>(null);
  const [folio, setFolio] = useState<Folio | null>(null);
  const [bookingSource, setBookingSource] = useState<BookingSource | null>(null);
  const [roomType, setRoomType] = useState<RoomType | null>(null);
  const [groupRooms, setGroupRooms] = useState<ReservationRoom[]>([]);
  const [loading, setLoading] = useState(true);
  const [earlyWarning, setEarlyWarning] = useState(false);
  const [checkinTime, setCheckinTime] = useState(() => {
    const draft = loadDraft<string>(CHECKIN_DRAFT_KEY);
    return draft || nowInTimezone('Asia/Jakarta');
  });
  const [cardState, setCardState] = useState<'idle'|'connecting'|'checking_encoder'|'waiting_for_card'|'writing'|'completed'|'success'|'failed'|'unavailable'>('idle');
  const [cardMessage, setCardMessage] = useState('');
  const [completing, setCompleting] = useState(false);
  const [lockIntegration, setLockIntegration] = useState<HotelLockIntegration | null>(null);

  const lockProviderType = lockIntegration?.provider_type || 'mock';
  const isProductionLock = lockProviderType === 'production';

  const branch = branches.find(b => b.id === reservation.branch_id);
  const standardTime = branch?.standard_checkin_time || '14:00';
  const branchTimezone = branch?.timezone || 'Asia/Jakarta';

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

      if (reservation.booking_source_id) {
        const { data: bs } = await supabase.from('booking_sources').select('*').eq('id', reservation.booking_source_id).maybeSingle();
        setBookingSource(bs as BookingSource | null);
      }
      if (reservation.room_type_id) {
        const { data: rt } = await supabase.from('room_types').select('*').eq('id', reservation.room_type_id).maybeSingle();
        setRoomType(rt as RoomType | null);
      } else if (r) {
        const { data: rt } = await supabase.from('room_types').select('*').eq('id', (r as Room).room_type_id).maybeSingle();
        setRoomType(rt as RoomType | null);
      }
      if (reservation.is_group) {
        const { data: rrData } = await supabase.from('reservation_rooms')
          .select('*,room:rooms(*)').eq('reservation_id', reservation.id).eq('status','active').order('created_at');
        setGroupRooms((rrData as ReservationRoom[]) || []);
      }

      const { data: lockInteg } = await supabase.from('hotel_lock_integrations').select('*').eq('branch_id', reservation.branch_id).maybeSingle();
      setLockIntegration(lockInteg as HotelLockIntegration | null);

      setLoading(false);
    })();
  }, [reservation]);

  useEffect(() => { saveDraft(CHECKIN_DRAFT_KEY, checkinTime); }, [checkinTime]);

  useEffect(() => {
    // Use the branch's timezone to determine the actual local date.
    // The actual check-in datetime is: today (in branch timezone) + the entered check-in time.
    // The scheduled check-in datetime is: reservation.check_in_date + standard check-in time.
    // Early check-in = actual datetime < scheduled datetime.
    // This correctly handles cases like 22 Aug 00:01 Jakarta (business date 21 Aug) vs standard 21 Aug 14:00.
    const localDate = todayInTimezone(branchTimezone);
    const scheduledDateTime = new Date(`${reservation.check_in_date}T${standardTime}:00`);
    const actualDateTime = new Date(`${localDate}T${checkinTime}:00`);
    setEarlyWarning(actualDateTime < scheduledDateTime);
  }, [standardTime, checkinTime, reservation.check_in_date, branchTimezone]);

  const handleAddEarlyCharge = async () => {
    if (!folio) return;
    const { data } = await supabase.from('system_settings').select('value').eq('key','early_checkin_charge').maybeSingle();
    const charge = parseFloat(data?.value || '0');
    if (charge <= 0) return showToast('No early check-in charge configured','warning');

    const businessDate = await getBusinessDate(reservation.branch_id);

    await supabase.from('folio_items').insert({
      folio_id: folio.id, branch_id: reservation.branch_id, reservation_id: reservation.id,
      guest_id: reservation.primary_guest_id, room_id: reservation.room_id,
      item_type:'charge', category:'early_checkin', description:'Early check-in charge',
      quantity:1, unit_amount:charge, amount:charge, business_date: businessDate, created_by:user!.id
    });

    await supabase.from('transactions').insert({
      branch_id:reservation.branch_id, organization_id:user!.organization_id,
      reservation_id:reservation.id, guest_id:reservation.primary_guest_id, folio_id:folio.id,
      transaction_type:'early_checkin_charge', description:'Early check-in charge',
      amount:charge, debit_credit:'debit', business_date: businessDate, created_by:user!.id
    });

    showToast(`Early check-in charge added: ${formatIDR(charge)}`,'success');
    setEarlyWarning(false);
  };

  const handleContinueNoCharge = async () => {
    await supabase.from('audit_logs').insert({
      organization_id:user!.organization_id, branch_id:reservation.branch_id, user_id:user!.id,
      action:'early_checkin_no_charge', object_type:'reservation', object_id:reservation.id,
      reason:`Early check-in at ${todayInTimezone(branchTimezone)} ${checkinTime} (standard: ${reservation.check_in_date} ${standardTime}) - no charge applied`
    });
    setEarlyWarning(false);
    showToast('Continued without charge (logged)','info');
  };

  const encodeCard = async () => {
    if (!room || !guest) return;
    const provider = getLockProviderByType(lockProviderType);
    provider.configure(integrationToConfig(lockIntegration));

    // Step 1: Connecting
    setCardState('connecting');
    setCardMessage(t('checkin.connecting'));
    if (!(await provider.connect())) {
      setCardState('unavailable');
      setCardMessage(t('checkin.lock_unavailable'));
      await supabase.from('card_issuances').insert({
        branch_id:reservation.branch_id, reservation_id:reservation.id, guest_id:guest.id, room_id:room.id,
        issuance_type:'issue', card_sequence:1,
        valid_from:`${reservation.check_in_date}T${checkinTime}`,
        valid_until:`${reservation.check_out_date}T${reservation.check_out_time}`,
        status:'failed', failure_reason:t('checkin.lock_unavailable'), provider_type:lockProviderType, performed_by:user!.id
      });
      return;
    }

    // Step 2: Checking encoder
    setCardState('checking_encoder');
    setCardMessage(isProductionLock ? 'Checking encoder...' : t('checkin.connecting'));
    if (isProductionLock) {
      const encStatus = await provider.readEncoderStatus();
      if (!encStatus.connected) {
        setCardState('unavailable');
        setCardMessage('Encoder unavailable');
        await supabase.from('card_issuances').insert({
          branch_id:reservation.branch_id, reservation_id:reservation.id, guest_id:guest.id, room_id:room.id,
          issuance_type:'issue', card_sequence:1,
          valid_from:`${reservation.check_in_date}T${checkinTime}`,
          valid_until:`${reservation.check_out_date}T${reservation.check_out_time}`,
          status:'failed', failure_reason:'Encoder unavailable', provider_type:lockProviderType, performed_by:user!.id
        });
        return;
      }
    }
    await new Promise(r => setTimeout(r, 300));

    // Step 3: Waiting for card
    setCardState('waiting_for_card');
    setCardMessage(isProductionLock ? 'Waiting for card...' : t('checkin.writing'));
    await new Promise(r => setTimeout(r, isProductionLock ? 800 : 200));

    // Step 4: Writing card
    setCardState('writing');
    setCardMessage(t('checkin.writing'));
    const result = await provider.encodeGuestCard({
      roomId:room.id, roomNumber:room.room_number, guestName:guest.full_name,
      validFrom:`${reservation.check_in_date}T${checkinTime}`,
      validUntil:`${reservation.check_out_date}T${reservation.check_out_time}`
    });

    if (result.success) {
      // Step 5: Completed
      setCardState('completed');
      setCardMessage(isProductionLock ? 'Completed' : t('checkin.confirming'));
      await new Promise(r => setTimeout(r, 500));
      setCardState('success');
      setCardMessage(t('checkin.card_success'));
      await supabase.from('card_issuances').insert({
        branch_id:reservation.branch_id, reservation_id:reservation.id, guest_id:guest.id, room_id:room.id,
        issuance_type:'issue', card_sequence:1,
        valid_from:`${reservation.check_in_date}T${checkinTime}`,
        valid_until:`${reservation.check_out_date}T${reservation.check_out_time}`,
        status:'success', provider_type:lockProviderType, performed_by:user!.id
      });
      if (lockIntegration) {
        await supabase.from('hotel_lock_integrations').update({
          last_success_encoding: new Date().toISOString(),
          encoder_status: 'connected',
        }).eq('id', lockIntegration.id);
      }
    } else {
      setCardState('failed');
      setCardMessage(result.message);
      await supabase.from('card_issuances').insert({
        branch_id:reservation.branch_id, reservation_id:reservation.id, guest_id:guest.id, room_id:room.id,
        issuance_type:'issue', card_sequence:1,
        valid_from:`${reservation.check_in_date}T${checkinTime}`,
        valid_until:`${reservation.check_out_date}T${reservation.check_out_time}`,
        status:'failed', failure_reason:result.message, provider_type:lockProviderType, performed_by:user!.id
      });
      if (lockIntegration) {
        await supabase.from('hotel_lock_integrations').update({
          last_error: result.message,
        }).eq('id', lockIntegration.id);
      }
    }
  };

  const completeCheckin = async () => {
    setCompleting(true);
    const localDate = todayInTimezone(branchTimezone);
    const { error } = await supabase.from('reservations').update({
      status:'checked_in', actual_check_in:`${localDate}T${checkinTime}:00`, check_in_time:checkinTime
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
    clearDraft(CHECKIN_DRAFT_KEY);
    setCompleting(false);
    onClose();
  };

  if (loading) return <Modal open onClose={onClose} title={t('checkin.title')}><LoadingPage /></Modal>;

  return (
    <Modal open onClose={onClose} title={t('checkin.title')} size="lg">
      <div className="space-y-4">
        <div className="grid grid-cols-2 md:grid-cols-4 gap-3 text-sm">
          <div><span className="text-slate-500">{t('common.guest')}:</span> <span className="font-medium">{guest?.full_name || '-'}</span></div>
          <div><span className="text-slate-500">{t('common.room')}:</span> <span className="font-medium">{room?.room_number || '-'}</span></div>
          {roomType && <div><span className="text-slate-500">{t('common.room_type')}:</span> <span className="font-medium">{roomType.name}</span></div>}
          {bookingSource && <div><span className="text-slate-500">{t('common.booking_source')}:</span> <span className="font-medium">{bookingSource.name}</span></div>}
          <div><span className="text-slate-500">{t('common.check_in')}:</span> <span className="font-medium">{formatDate(reservation.check_in_date)} {formatTime(reservation.check_in_time)}</span></div>
          <div><span className="text-slate-500">{t('common.check_out')}:</span> <span className="font-medium">{formatDate(reservation.check_out_date)} {formatTime(reservation.check_out_time)}</span></div>
          <div><span className="text-slate-500">{t('common.nights')}:</span> <span className="font-medium">{reservation.num_nights}</span></div>
          <div><span className="text-slate-500">{t('common.adults')} / {t('common.children')}:</span> <span className="font-medium">{reservation.adults} / {reservation.children}</span></div>
          <div><span className="text-slate-500">{t('common.rate')}:</span> <span className="font-medium">{formatIDR(reservation.rate)}</span></div>
          <div><span className="text-slate-500">{t('common.deposit')}:</span> <span className="font-medium">{formatIDR(reservation.deposit)}</span></div>
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

        <div>
          <label className="text-sm font-medium text-slate-700">{t('checkin.actual_time')}</label>
          <input type="time" value={checkinTime} onChange={e=>setCheckinTime(e.target.value)} className="mt-1 rounded-lg border border-slate-300 px-3 py-2 text-sm"/>
        </div>

        {earlyWarning && (
          <div className="bg-amber-50 border border-amber-300 rounded-lg p-4 space-y-3">
            <div className="flex items-center gap-2 text-amber-700 font-medium"><AlertCircle size={18}/>{t('checkin.early_warning')}</div>
            <div className="text-sm text-slate-600">
              <div>{t('checkin.standard_time')}: <span className="font-medium">{formatDate(reservation.check_in_date)} {standardTime}</span></div>
              <div>{t('checkin.actual_time')}: <span className="font-medium">{formatDate(todayInTimezone(branchTimezone))} {checkinTime}</span></div>
              <div>{t('checkin.difference')}: <span className="font-medium">{formatHoursShort((new Date(`${todayInTimezone(branchTimezone)}T${checkinTime}:00`).getTime() - new Date(`${reservation.check_in_date}T${standardTime}:00`).getTime()) / 3600000)}</span></div>
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
            {isProductionLock
              ? <span className="text-xs text-emerald-600 font-medium bg-emerald-50 px-2 py-0.5 rounded">PRODUCTION MODE</span>
              : <span className="text-xs text-amber-600 font-medium bg-amber-50 px-2 py-0.5 rounded">DEVELOPMENT / MOCK MODE</span>}
          </div>
          {room && guest && <div className="text-sm text-slate-500 mb-3">{t('common.room')}: <span className="font-medium">{room.room_number}</span> · {t('common.guest')}: <span className="font-medium">{guest.full_name}</span></div>}

          {cardState === 'idle' && <Button onClick={encodeCard} disabled={!room}><KeyRound size={16}/>{t('checkin.encode_card')}</Button>}

          {cardState !== 'idle' && (
            <div className="space-y-3">
              {/* Progress steps */}
              <div className="space-y-1.5">
                {[
                  { key: 'connecting', label: t('checkin.connecting') },
                  { key: 'checking_encoder', label: 'Checking encoder' },
                  { key: 'waiting_for_card', label: 'Waiting for card' },
                  { key: 'writing', label: t('checkin.writing') },
                  { key: 'completed', label: 'Completed' },
                ].map((step) => {
                  const stepOrder = ['connecting','checking_encoder','waiting_for_card','writing','completed','success'];
                  const currentIdx = stepOrder.indexOf(cardState);
                  const stepIdx = stepOrder.indexOf(step.key);
                  const isDone = currentIdx > stepIdx || cardState === 'success';
                  const isActive = cardState === step.key;
                  return (
                    <div key={step.key} className="flex items-center gap-2 text-sm">
                      {isDone ? <CheckCircle2 size={16} className="text-emerald-600"/> :
                       isActive ? <Loader2 size={16} className="animate-spin text-blue-600"/> :
                       <div className="w-4 h-4 rounded-full border-2 border-slate-200"/>}
                      <span className={isDone ? 'text-emerald-600 font-medium' : isActive ? 'text-blue-600 font-medium' : 'text-slate-400'}>{step.label}</span>
                    </div>
                  );
                })}
              </div>

              {/* Status message */}
              <div className="flex items-center gap-2 pt-1">
                {['connecting','checking_encoder','waiting_for_card','writing','completed'].includes(cardState) && <Loader2 size={18} className="animate-spin text-blue-600"/>}
                {cardState === 'success' && <CheckCircle2 size={18} className="text-emerald-600"/>}
                {['failed','unavailable'].includes(cardState) && <AlertCircle size={18} className="text-red-500"/>}
                <span className={`text-sm font-medium ${['failed','unavailable'].includes(cardState) ? 'text-red-600' : cardState === 'success' ? 'text-emerald-600' : 'text-slate-700'}`}>{cardMessage}</span>
              </div>

              {/* Retry button on failure */}
              {['failed','unavailable'].includes(cardState) && (
                <Button size="sm" variant="outline" onClick={encodeCard}><KeyRound size={14}/> Retry encoding</Button>
              )}
            </div>
          )}
        </div>

        <div className="flex justify-end gap-2">
          {onNavigateToPayment && <Button size="sm" variant="outline" onClick={() => onNavigateToPayment(reservation.id)}><FileText size={14}/>{t('res.view_folio')}</Button>}
          {onNavigateToInvoice && <Button size="sm" variant="outline" onClick={() => onNavigateToInvoice(reservation.id)}><Receipt size={14}/>{t('res.view_invoice')}</Button>}
          <Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button>
          <Button loading={completing} onClick={completeCheckin}><LogIn size={16}/>{t('checkin.complete')}</Button>
        </div>
      </div>
    </Modal>
  );
}

function CheckoutModal({ reservation, onClose, onNavigateToPayment, onNavigateToInvoice }: { reservation: Reservation; onClose: () => void; onNavigateToPayment?: (id: string) => void; onNavigateToInvoice?: (id: string) => void }) {
  const { user, branches } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();

  const [guest,setGuest]=useState<Guest|null>(null);
  const [room,setRoom]=useState<Room|null>(null);
  const [folio,setFolio]=useState<Folio|null>(null);
  const [folioItems,setFolioItems]=useState<any[]>([]);
  const [bookingSource,setBookingSource]=useState<BookingSource|null>(null);
  const [roomType,setRoomType]=useState<RoomType|null>(null);
  const [groupRooms,setGroupRooms]=useState<ReservationRoom[]>([]);
  const [loading,setLoading]=useState(true);
  const [lateWarning,setLateWarning]=useState(false);
  const [checkoutTime,setCheckoutTime]=useState(() => {
    const draft = loadDraft<string>(CHECKOUT_DRAFT_KEY);
    return draft || new Date().toTimeString().slice(0,5);
  });
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

      if(reservation.booking_source_id){
        const {data:bs}=await supabase.from('booking_sources').select('*').eq('id',reservation.booking_source_id).maybeSingle();
        setBookingSource(bs as BookingSource|null);
      }
      if(reservation.room_type_id){
        const {data:rt}=await supabase.from('room_types').select('*').eq('id',reservation.room_type_id).maybeSingle();
        setRoomType(rt as RoomType|null);
      } else if(r){
        const {data:rt}=await supabase.from('room_types').select('*').eq('id',(r as Room).room_type_id).maybeSingle();
        setRoomType(rt as RoomType|null);
      }
      if(reservation.is_group){
        const {data:rrData}=await supabase.from('reservation_rooms')
          .select('*,room:rooms(*)').eq('reservation_id',reservation.id).eq('status','active').order('created_at');
        setGroupRooms((rrData as ReservationRoom[])||[]);
      }

      if(f){
        const {data:items}=await supabase.from('folio_items').select('*').eq('folio_id',f.id).eq('voided',false).order('created_at');
        setFolioItems(items||[]);
      }
      setLoading(false);
    })();
  },[reservation]);

  useEffect(() => { saveDraft(CHECKOUT_DRAFT_KEY, checkoutTime); }, [checkoutTime]);

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

    const businessDate = await getBusinessDate(reservation.branch_id);
    await supabase.from('folio_items').insert({
      folio_id:folio.id, branch_id:reservation.branch_id, reservation_id:reservation.id,
      guest_id:reservation.primary_guest_id, room_id:reservation.room_id,
      item_type:'charge', category:'late_checkout', description:'Late check-out charge',
      quantity:1, unit_amount:charge, amount:charge, business_date: businessDate, created_by:user!.id
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

      try {
        const { data: lockInteg } = await supabase.from('hotel_lock_integrations').select('*').eq('branch_id', reservation.branch_id).maybeSingle();
        const lockType = (lockInteg as HotelLockIntegration | null)?.provider_type || 'mock';
        const provider = getLockProviderByType(lockType);
        provider.configure(integrationToConfig(lockInteg as HotelLockIntegration | null));
        await provider.invalidateGuestCard({ cardId: reservation.id });
      } catch(e) { console.warn('Card invalidation failed', e); }

      await supabase.from('audit_logs').insert({
        organization_id: user!.organization_id, branch_id: reservation.branch_id, user_id: user!.id,
        action: 'check_out', object_type: 'reservation', object_id: reservation.id,
        new_value: { checkout_time: checkoutTime, total_charges: tCharges, total_payments: tPayments, balance: outstanding }
      });

      showToast(t('checkout.complete'), 'success');
      clearDraft(CHECKOUT_DRAFT_KEY);
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
        <div className="grid grid-cols-2 md:grid-cols-4 gap-3 text-sm">
          <div><span className="text-slate-500">{t('common.guest')}:</span> <span className="font-medium">{guest?.full_name||'-'}</span></div>
          <div><span className="text-slate-500">{t('common.room')}:</span> <span className="font-medium">{room?.room_number||'-'}</span></div>
          {roomType && <div><span className="text-slate-500">{t('common.room_type')}:</span> <span className="font-medium">{roomType.name}</span></div>}
          {bookingSource && <div><span className="text-slate-500">{t('common.booking_source')}:</span> <span className="font-medium">{bookingSource.name}</span></div>}
          <div><span className="text-slate-500">{t('common.check_in')}:</span> <span className="font-medium">{formatDate(reservation.check_in_date)} {formatTime(reservation.check_in_time)}</span></div>
          <div><span className="text-slate-500">{t('common.check_out')}:</span> <span className="font-medium">{formatDate(reservation.check_out_date)} {formatTime(reservation.check_out_time)}</span></div>
          <div><span className="text-slate-500">{t('common.nights')}:</span> <span className="font-medium">{reservation.num_nights}</span></div>
          <div><span className="text-slate-500">{t('common.adults')} / {t('common.children')}:</span> <span className="font-medium">{reservation.adults} / {reservation.children}</span></div>
          <div><span className="text-slate-500">{t('common.deposit')}:</span> <span className="font-medium">{formatIDR(reservation.deposit)}</span></div>
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
          {onNavigateToPayment && <Button size="sm" variant="outline" onClick={() => onNavigateToPayment(reservation.id)}><FileText size={14}/>{t('res.view_folio')}</Button>}
          {onNavigateToInvoice && <Button size="sm" variant="outline" onClick={() => onNavigateToInvoice(reservation.id)}><Receipt size={14}/>{t('res.view_invoice')}</Button>}
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

interface ExtendDraft {
  newCheckoutDate: string;
  roomRate: string;
  newRoomTypeId: string;
  newRoomId: string;
}

function ExtendStayModal({ reservation, onClose }: { reservation: Reservation; onClose: () => void }) {
  const { user } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();

  const [guest, setGuest] = useState<Guest | null>(null);
  const [room, setRoom] = useState<Room | null>(null);
  const [folio, setFolio] = useState<Folio | null>(null);
  const [bookingSource, setBookingSource] = useState<BookingSource | null>(null);
  const [roomType, setRoomType] = useState<RoomType | null>(null);
  const [allRoomTypes, setAllRoomTypes] = useState<RoomType[]>([]);
  const [availableRooms, setAvailableRooms] = useState<Room[]>([]);
  const [groupRooms, setGroupRooms] = useState<ReservationRoom[]>([]);
  const [holidays, setHolidays] = useState<IndonesianHoliday[]>([]);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [rateTouched, setRateTouched] = useState(false);

  const previousCheckoutDate = reservation.check_out_date;
  const originalRoomTypeId = reservation.room_type_id || room?.room_type_id || '';

  const [newCheckoutDate, setNewCheckoutDate] = useState(() => {
    const draft = loadDraft<ExtendDraft>(EXTEND_DRAFT_KEY);
    return draft?.newCheckoutDate || addDays(previousCheckoutDate, 1);
  });
  const [roomRate, setRoomRate] = useState(() => {
    const draft = loadDraft<ExtendDraft>(EXTEND_DRAFT_KEY);
    return draft?.roomRate || String(reservation.rate);
  });
  const [newRoomTypeId, setNewRoomTypeId] = useState(() => {
    const draft = loadDraft<ExtendDraft>(EXTEND_DRAFT_KEY);
    return draft?.newRoomTypeId || originalRoomTypeId;
  });
  const [newRoomId, setNewRoomId] = useState(() => {
    const draft = loadDraft<ExtendDraft>(EXTEND_DRAFT_KEY);
    return draft?.newRoomId || reservation.room_id || '';
  });

  const isRoomTypeUpgraded = newRoomTypeId !== originalRoomTypeId;
  const isRoomChanged = newRoomId !== (reservation.room_id || '');

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

      if (reservation.booking_source_id) {
        const { data: bs } = await supabase.from('booking_sources').select('*').eq('id', reservation.booking_source_id).maybeSingle();
        setBookingSource(bs as BookingSource | null);
      }
      if (reservation.room_type_id) {
        const { data: rt } = await supabase.from('room_types').select('*').eq('id', reservation.room_type_id).maybeSingle();
        setRoomType(rt as RoomType | null);
      } else if (r) {
        const { data: rt } = await supabase.from('room_types').select('*').eq('id', (r as Room).room_type_id).maybeSingle();
        setRoomType(rt as RoomType | null);
      }

      // Load all room types for this branch so user can upgrade
      const { data: rtList } = await supabase.from('room_types').select('*').eq('branch_id', reservation.branch_id).eq('is_active', true).order('sort_order');
      setAllRoomTypes((rtList as RoomType[]) || []);

      if (reservation.is_group) {
        const { data: rrData } = await supabase.from('reservation_rooms')
          .select('*,room:rooms(*)').eq('reservation_id', reservation.id).eq('status', 'active').order('created_at');
        setGroupRooms((rrData as ReservationRoom[]) || []);
      }

      const { data: hol } = await supabase.from('indonesian_holidays').select('*').eq('organization_id', user!.organization_id).order('holiday_date');
      setHolidays((hol as IndonesianHoliday[]) || []);

      setLoading(false);
    })();
  }, [reservation, user]);

  // Load available rooms when room type or checkout date changes
  useEffect(() => {
    if (!newRoomTypeId || !reservation.branch_id) return;
    (async () => {
      const { data: roomsOfType } = await supabase
        .from('rooms')
        .select('*')
        .eq('branch_id', reservation.branch_id)
        .eq('room_type_id', newRoomTypeId)
        .eq('is_active', true)
        .order('room_number');

      const candidateRooms = (roomsOfType as Room[]) || [];

      const available: Room[] = [];
      for (const candidate of candidateRooms) {
        if (candidate.id === reservation.room_id) {
          available.push(candidate);
          continue;
        }
        const { data: conflicts } = await supabase
          .from('reservations')
          .select('id')
          .eq('room_id', candidate.id)
          .in('status', ['confirmed', 'checked_in', 'tentative'])
          .neq('id', reservation.id)
          .lt('check_in_date', newCheckoutDate)
          .gt('check_out_date', previousCheckoutDate);
        if (!conflicts || conflicts.length === 0) {
          available.push(candidate);
        }
      }
      setAvailableRooms(available);

      if (isRoomTypeUpgraded && !available.find(rm => rm.id === newRoomId)) {
        setNewRoomId(available[0]?.id || '');
      }
    })();
  }, [newRoomTypeId, newCheckoutDate, reservation.branch_id, reservation.id, reservation.room_id, previousCheckoutDate, isRoomTypeUpgraded, newRoomId]);

  useEffect(() => { saveDraft(EXTEND_DRAFT_KEY, { newCheckoutDate, roomRate, newRoomTypeId, newRoomId }); }, [newCheckoutDate, roomRate, newRoomTypeId, newRoomId]);

  // The selected room type for rate calculation (upgraded or original)
  const selectedRoomType = useMemo(() => {
    return allRoomTypes.find(rt => rt.id === newRoomTypeId) || roomType || null;
  }, [allRoomTypes, newRoomTypeId, roomType]);

  // Calculation: extra nights = new checkout date - previous checkout date
  const extraNights = Math.max(0, nightsBetween(previousCheckoutDate, newCheckoutDate));

  // Rate breakdown for the extra nights using weekday/weekend rates of the selected room type
  const rateBreakdown = useMemo(() => {
    if (!selectedRoomType || extraNights <= 0) return null;
    const { breakdown, total } = calculateTotalRate(previousCheckoutDate, newCheckoutDate, selectedRoomType, holidays);
    return { breakdown, total };
  }, [selectedRoomType, previousCheckoutDate, newCheckoutDate, extraNights, holidays]);

  // When room type changes and rate hasn't been manually touched, auto-update the rate field
  useEffect(() => {
    if (!rateTouched && selectedRoomType) {
      setRoomRate(String(selectedRoomType.base_rate));
    }
  }, [selectedRoomType, rateTouched]);

  const manualRate = rateTouched && Number(roomRate) > 0 ? Number(roomRate) : null;
  const additionalCharge = manualRate ? manualRate * extraNights : (rateBreakdown ? rateBreakdown.total : Number(roomRate) * extraNights);

  const handleRoomTypeChange = (rtId: string) => {
    setNewRoomTypeId(rtId);
    setNewRoomId('');
    setRateTouched(false);
    const rt = allRoomTypes.find(r => r.id === rtId);
    if (rt) setRoomRate(String(rt.base_rate));
  };

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
    if (isRoomTypeUpgraded && !newRoomId) {
      showToast('Please select a room for the upgraded room type.', 'error');
      return;
    }

    setSaving(true);

    try {
      // Check room availability for the extended dates (or new room)
      const roomIdToCheck = isRoomChanged ? newRoomId : reservation.room_id;
      if (roomIdToCheck) {
        const { data: conflicts } = await supabase
          .from('reservations')
          .select('id,reservation_number')
          .eq('room_id', roomIdToCheck)
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

      // Determine the effective room and room type
      const effectiveRoomId = isRoomChanged ? newRoomId : reservation.room_id;
      const effectiveRoomTypeId = isRoomTypeUpgraded ? newRoomTypeId : reservation.room_type_id;

      // Update reservation with new checkout date, num_nights, and optionally room/room type
      const totalNights = nightsBetween(reservation.check_in_date, newCheckoutDate);
      const updatePayload: Record<string, unknown> = {
        check_out_date: newCheckoutDate,
        num_nights: totalNights,
      };

      if (isRoomTypeUpgraded) {
        updatePayload.room_type_id = effectiveRoomTypeId;
        updatePayload.rate = Number(roomRate);
      }
      if (isRoomChanged) {
        updatePayload.room_id = effectiveRoomId;
      }

      const { error: resError } = await supabase.from('reservations').update(updatePayload).eq('id', reservation.id);

      if (resError) throw resError;

      // Handle room status changes for room upgrade/transfer
      if (isRoomChanged && reservation.room_id) {
        await supabase.from('rooms').update({ status: 'dirty' }).eq('id', reservation.room_id);
        await supabase.from('rooms').update({ status: 'occupied' }).eq('id', effectiveRoomId);

        await supabase.from('room_transfers').insert({
          reservation_id: reservation.id,
          from_room_id: reservation.room_id,
          to_room_id: effectiveRoomId,
          reason: `Room type upgrade during stay extension: ${roomType?.name || ''} → ${selectedRoomType?.name || ''}`,
          performed_by: user!.id,
        });
      }

      // Add room charge for extra nights to folio
      if (folio) {
        const chargeRoomId = isRoomChanged ? effectiveRoomId : reservation.room_id;
        const chargeDescriptionPrefix = isRoomTypeUpgraded
          ? `Extended stay (Upgraded to ${selectedRoomType?.name})`
          : 'Extended stay';

        if (!manualRate && rateBreakdown && rateBreakdown.breakdown.length > 0) {
          const items = rateBreakdown.breakdown.map((day) => ({
            folio_id: folio.id,
            branch_id: reservation.branch_id,
            reservation_id: reservation.id,
            guest_id: reservation.primary_guest_id,
            room_id: chargeRoomId,
            item_type: 'charge' as const,
            category: 'room',
            description: `${chargeDescriptionPrefix} - ${formatDate(day.date)} (${getRateTypeLabel(day.rateType, 'en')})`,
            quantity: 1,
            unit_amount: day.rate,
            amount: day.rate,
            business_date: day.date,
            created_by: user!.id,
          }));
          const { error: chargeError } = await supabase.from('folio_items').insert(items);
          if (chargeError) throw chargeError;
        } else {
          const businessDate = await getBusinessDate(reservation.branch_id);
          const { error: chargeError } = await supabase.from('folio_items').insert({
            folio_id: folio.id,
            branch_id: reservation.branch_id,
            reservation_id: reservation.id,
            guest_id: reservation.primary_guest_id,
            room_id: chargeRoomId,
            item_type: 'charge',
            category: 'room',
            description: `${chargeDescriptionPrefix} - ${extraNights} extra night(s) at ${formatIDR(Number(roomRate))}/night`,
            quantity: extraNights,
            unit_amount: Number(roomRate),
            amount: additionalCharge,
            business_date: businessDate,
            created_by: user!.id,
          });
          if (chargeError) throw chargeError;
        }

        await folioService.syncFolioTotals(folio.id);

        // Sync invoice to reflect the new folio charges
        try {
          await invoiceService.ensureInvoice({
            folioId: folio.id,
            branchId: reservation.branch_id,
            organizationId: user!.organization_id,
            reservationId: reservation.id,
            guestId: reservation.primary_guest_id,
            userId: user!.id,
          });
        } catch (invoiceErr: any) {
          console.error('Invoice sync during extend stay failed:', invoiceErr);
          showToast(`Invoice update after extend: ${invoiceErr.message || invoiceErr}`, 'warning');
        }
      }

      // Update reservation_rooms if group
      if (reservation.is_group) {
        const rrUpdate: Record<string, unknown> = {
          check_out_date: newCheckoutDate,
          num_nights: totalNights,
        };
        if (isRoomTypeUpgraded) {
          rrUpdate.room_type_id = effectiveRoomTypeId;
          rrUpdate.rate = Number(roomRate);
        }
        if (isRoomChanged) {
          rrUpdate.room_id = effectiveRoomId;
        }
        await supabase.from('reservation_rooms')
          .update(rrUpdate)
          .eq('reservation_id', reservation.id)
          .eq('status', 'active');
      }

      // Invalidate old guest card and encode new one if room changed
      if (isRoomChanged) {
        try {
          const { data: lockInteg } = await supabase.from('hotel_lock_integrations').select('*').eq('branch_id', reservation.branch_id).maybeSingle();
          const lockType = (lockInteg as HotelLockIntegration | null)?.provider_type || 'mock';
          const provider = getLockProviderByType(lockType);
          provider.configure(integrationToConfig(lockInteg as HotelLockIntegration | null));
          await provider.invalidateGuestCard({ cardId: reservation.id });

          const newRoom = availableRooms.find(rm => rm.id === effectiveRoomId);
          if (newRoom && guest) {
            await provider.encodeGuestCard({
              roomId: newRoom.id,
              roomNumber: newRoom.room_number,
              guestName: guest.full_name,
              validFrom: new Date().toISOString(),
              validUntil: `${newCheckoutDate}T${reservation.check_out_time}:00`,
            });

            await supabase.from('card_issuances').insert({
              branch_id: reservation.branch_id,
              reservation_id: reservation.id,
              guest_id: guest.id,
              room_id: newRoom.id,
              issuance_type: 'replace',
              card_sequence: 2,
              valid_from: new Date().toISOString(),
              valid_until: `${newCheckoutDate}T${reservation.check_out_time}:00`,
              status: 'success',
              provider_type: lockType,
              performed_by: user!.id,
            });
          }
        } catch (e) {
          console.warn('Card re-encoding during room upgrade failed', e);
        }
      }

      // Audit log
      await supabase.from('audit_logs').insert({
        organization_id: user!.organization_id,
        branch_id: reservation.branch_id,
        user_id: user!.id,
        action: 'extend_stay',
        object_type: 'reservation',
        object_id: reservation.id,
        previous_value: {
          check_out_date: previousCheckoutDate,
          room_id: reservation.room_id,
          room_type_id: reservation.room_type_id,
          rate: reservation.rate,
        },
        new_value: {
          check_out_date: newCheckoutDate,
          extra_nights: extraNights,
          additional_charge: additionalCharge,
          room_rate: Number(roomRate),
          room_type_upgraded: isRoomTypeUpgraded,
          new_room_type_id: isRoomTypeUpgraded ? effectiveRoomTypeId : null,
          room_changed: isRoomChanged,
          new_room_id: isRoomChanged ? effectiveRoomId : null,
        },
      });

      const upgradeMsg = isRoomTypeUpgraded ? ` (Upgraded to ${selectedRoomType?.name})` : '';
      const roomMsg = isRoomChanged ? ` (Moved to room ${availableRooms.find(rm => rm.id === effectiveRoomId)?.room_number || effectiveRoomId})` : '';
      showToast(`Stay extended by ${extraNights} night(s). Additional charge: ${formatIDR(additionalCharge)}${upgradeMsg}${roomMsg}`, 'success');
      clearDraft(EXTEND_DRAFT_KEY);
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
    <Modal open onClose={onClose} title={t('res.extend_stay')} size="lg"
      footer={<><Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button><Button loading={saving} onClick={handleExtend}><CalendarPlus size={16} />{t('common.confirm')}</Button></>}>
      <div className="space-y-4">
        <div className="grid grid-cols-2 md:grid-cols-4 gap-3 text-sm">
          <div><span className="text-slate-500">{t('common.guest')}:</span> <span className="font-medium">{guest?.full_name || '-'}</span></div>
          <div><span className="text-slate-500">{t('common.room')}:</span> <span className="font-medium">{room?.room_number || '-'}</span></div>
          {roomType && <div><span className="text-slate-500">{t('common.room_type')}:</span> <span className="font-medium">{roomType.name}</span></div>}
          {bookingSource && <div><span className="text-slate-500">{t('common.booking_source')}:</span> <span className="font-medium">{bookingSource.name}</span></div>}
          <div><span className="text-slate-500">{t('common.check_in')}:</span> <span className="font-medium">{formatDate(reservation.check_in_date)} {formatTime(reservation.check_in_time)}</span></div>
          <div><span className="text-slate-500">{t('common.check_out')}:</span> <span className="font-medium">{formatDate(reservation.check_out_date)} {formatTime(reservation.check_out_time)}</span></div>
          <div><span className="text-slate-500">{t('common.nights')}:</span> <span className="font-medium">{reservation.num_nights}</span></div>
          <div><span className="text-slate-500">{t('common.adults')} / {t('common.children')}:</span> <span className="font-medium">{reservation.adults} / {reservation.children}</span></div>
          <div><span className="text-slate-500">{t('common.deposit')}:</span> <span className="font-medium">{formatIDR(reservation.deposit)}</span></div>
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

        <Input label={t('res.new_checkout_date')} type="date" value={newCheckoutDate} onChange={e => setNewCheckoutDate(e.target.value)} required />

        {/* Room type upgrade selector */}
        <div className="space-y-3">
          <div className="flex flex-col gap-1">
            <label className="text-sm font-medium text-slate-700">Upgrade Room Type (optional)</label>
            <select
              value={newRoomTypeId}
              onChange={e => handleRoomTypeChange(e.target.value)}
              className="rounded-lg border border-slate-300 px-3 py-2 text-sm bg-white outline-none focus:ring-2 focus:ring-blue-500"
            >
              {allRoomTypes.map(rt => (
                <option key={rt.id} value={rt.id}>
                  {rt.name} — {formatIDR(rt.base_rate)}/night{rt.id === originalRoomTypeId ? ' (current)' : ''}
                </option>
              ))}
            </select>
          </div>

          {/* Room selector — only show if room type was upgraded */}
          {isRoomTypeUpgraded && (
            <div className="flex flex-col gap-1">
              <label className="text-sm font-medium text-slate-700">Select New Room ({selectedRoomType?.name})</label>
              {availableRooms.length === 0 ? (
                <p className="text-sm text-red-500">No rooms available for the selected dates.</p>
              ) : (
                <select
                  value={newRoomId}
                  onChange={e => setNewRoomId(e.target.value)}
                  className="rounded-lg border border-slate-300 px-3 py-2 text-sm bg-white outline-none focus:ring-2 focus:ring-blue-500"
                >
                  <option value="">-- Select a room --</option>
                  {availableRooms.map(r => (
                    <option key={r.id} value={r.id}>
                      Room {r.room_number} (Floor {r.floor}) — {t(`room.${r.status}`)}
                    </option>
                  ))}
                </select>
              )}
              <p className="text-xs text-amber-600">
                Upgrading will move the guest to the new room. The old room will be marked dirty and a new key card will be encoded.
              </p>
            </div>
          )}
        </div>

        <Input
          label={t('res.room_rate_per_night')}
          type="number"
          value={roomRate}
          onChange={e => { setRoomRate(e.target.value); setRateTouched(true); }}
          hint={rateTouched ? 'Custom rate (auto-calc overridden)' : (rateBreakdown ? 'Auto-calculated from weekday/weekend rates — edit to override' : undefined)}
          required
        />

        {rateBreakdown && rateBreakdown.breakdown.length > 0 && !manualRate && (
          <div className="rounded-lg border border-blue-200 bg-blue-50 p-4 space-y-3 text-sm">
            <div className="flex items-center justify-between">
              <p className="text-sm font-semibold text-blue-900">{t('room_types.rate_preview')}{selectedRoomType ? ` · ${selectedRoomType.name}` : ''}</p>
              <span className="text-xs text-blue-700">{formatIDR(rateBreakdown.total)} total</span>
            </div>
            <div className="grid grid-cols-2 sm:grid-cols-4 lg:grid-cols-7 gap-2">
              {rateBreakdown.breakdown.map((day) => (
                <div key={day.date} className="rounded-md border border-white bg-white/70 p-2 text-center">
                  <p className="text-xs text-slate-500">{formatDate(day.date)}</p>
                  <p className="text-xs font-semibold text-slate-700">{getRateTypeLabel(day.rateType, 'en')}</p>
                  <p className="text-xs font-bold text-blue-700">{formatIDR(day.rate)}</p>
                </div>
              ))}
            </div>
          </div>
        )}

        {manualRate && (
          <div className="rounded-lg border border-amber-200 bg-amber-50 p-3 text-sm text-amber-700">
            Custom rate active: {formatIDR(manualRate)} × {extraNights} {t('common.nights')} = {formatIDR(additionalCharge)}
          </div>
        )}

        <div className="bg-blue-50 border border-blue-200 rounded-lg p-4 space-y-2 text-sm">
          <div className="flex justify-between"><span className="text-slate-600">{t('res.prev_checkout')}:</span> <span className="font-medium">{formatDate(previousCheckoutDate)} {formatTime(reservation.check_out_time)}</span></div>
          <div className="flex justify-between"><span className="text-slate-600">{t('res.new_checkout')}:</span> <span className="font-medium">{formatDate(newCheckoutDate)}</span></div>
          <div className="flex justify-between"><span className="text-slate-600">{t('res.extra_nights')}:</span> <span className="font-bold text-blue-700">{extraNights} {t('common.nights')}</span></div>
          {isRoomTypeUpgraded && (
            <div className="flex justify-between"><span className="text-slate-600">Room Type:</span> <span className="font-medium text-blue-700">{roomType?.name} → {selectedRoomType?.name}</span></div>
          )}
          {isRoomChanged && (
            <div className="flex justify-between"><span className="text-slate-600">Room:</span> <span className="font-medium text-blue-700">{room?.room_number} → {availableRooms.find(rm => rm.id === newRoomId)?.room_number || '-'}</span></div>
          )}
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
  const [selectedRoomId, setSelectedRoomId] = useState(() => {
    const draft = loadDraft<string>(SPLIT_DRAFT_KEY);
    return draft || '';
  });

  useEffect(() => { saveDraft(SPLIT_DRAFT_KEY, selectedRoomId); }, [selectedRoomId]);

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
      clearDraft(SPLIT_DRAFT_KEY);
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
                    value={rr.room_id || ''}
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
