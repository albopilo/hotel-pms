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
import { ResStatusBadge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatIDR, formatDate, todayISO, addDays, nightsBetween } from '@/lib/format';
import { Plus, CalendarDays, Search } from 'lucide-react';
import type { Reservation, Guest, Room, RoomType, BookingSource, Branch } from '@/types/database';

export function ReservationsPage({ searchQuery = '', onSelectReservation }: { searchQuery?: string; onSelectReservation?: (id: string) => void }) {
  const { user, branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [reservations, setReservations] = useState<Reservation[]>([]);
  const [guests, setGuests] = useState<Guest[]>([]);
  const [rooms, setRooms] = useState<Room[]>([]);
  const [roomTypes, setRoomTypes] = useState<RoomType[]>([]);
  const [bookingSources, setBookingSources] = useState<BookingSource[]>([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [statusFilter, setStatusFilter] = useState('all');
  const [localSearch, setLocalSearch] = useState(searchQuery);

  const branchIds = selectedBranchId ? [selectedBranchId] : branches.map((b) => b.id);

  const load = useCallback(async () => {
    if (branchIds.length === 0) { setLoading(false); return; }
    setLoading(true);
    const [{ data: res }, { data: g }, { data: r }, { data: rt }, { data: bs }] = await Promise.all([
      supabase.from('reservations').select('*').in('branch_id', branchIds).order('created_at', { ascending: false }).limit(200),
      supabase.from('guests').select('*').limit(500),
      supabase.from('rooms').select('*').in('branch_id', branchIds),
      supabase.from('room_types').select('*').in('branch_id', branchIds),
      supabase.from('booking_sources').select('*').order('sort_order'),
    ]);
    setReservations((res as Reservation[]) || []);
    setGuests((g as Guest[]) || []);
    setRooms((r as Room[]) || []);
    setRoomTypes((rt as RoomType[]) || []);
    setBookingSources((bs as BookingSource[]) || []);
    setLoading(false);
  }, [branchIds]);

  useEffect(() => { load(); }, [load]);
  useEffect(() => { if (searchQuery !== localSearch) setLocalSearch(searchQuery); }, [searchQuery]);

  const guestMap = new Map(guests.map((g) => [g.id, g]));
  const roomMap = new Map(rooms.map((r) => [r.id, r]));
  const typeMap = new Map(roomTypes.map((rt) => [rt.id, rt]));
  const sourceMap = new Map(bookingSources.map((bs) => [bs.id, bs]));

  const filtered = reservations.filter((r) => {
    if (statusFilter !== 'all' && r.status !== statusFilter) return false;
    const q = localSearch.toLowerCase().trim();
    if (!q) return true;
    const guest = guestMap.get(r.primary_guest_id || '');
    const room = roomMap.get(r.room_id || '');
    return (r.reservation_number.toLowerCase().includes(q) || (guest?.full_name || '').toLowerCase().includes(q) || (room?.room_number || '').includes(q) || (guest?.phone || '').includes(q));
  });

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between flex-wrap gap-2">
        <h1 className="text-2xl font-bold text-slate-900">{t('nav.reservations')}</h1>
        <Button onClick={() => setShowForm(true)}><Plus size={18} /> {t('action.new_reservation')}</Button>
      </div>

      <div className="flex gap-3 flex-wrap">
        <div className="relative flex-1 min-w-[200px]">
          <Search size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
          <input type="text" value={localSearch} onChange={(e) => setLocalSearch(e.target.value)} placeholder={t('common.search')} className="w-full rounded-lg border border-slate-300 pl-10 pr-3 py-2 text-sm outline-none focus:ring-2 focus:ring-blue-500" />
        </div>
        <select value={statusFilter} onChange={(e) => setStatusFilter(e.target.value)} className="rounded-lg border border-slate-300 px-3 py-2 text-sm bg-white outline-none focus:ring-2 focus:ring-blue-500">
          <option value="all">{t('common.all')}</option>
          <option value="tentative">{t('res.tentative')}</option>
          <option value="confirmed">{t('res.confirmed')}</option>
          <option value="checked_in">{t('res.checked_in')}</option>
          <option value="checked_out">{t('res.checked_out')}</option>
          <option value="cancelled">{t('res.cancelled')}</option>
          <option value="no_show">{t('res.no_show')}</option>
        </select>
      </div>

      {filtered.length === 0 ? (
        <EmptyState icon={<CalendarDays size={48} />} title={t('res.no_reservations')} />
      ) : (
        <Card noPadding>
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 text-slate-500">
                  <th className="text-left py-3 px-4">{t('res.reservation_number')}</th>
                  <th className="text-left py-3 px-4">{t('common.guest')}</th>
                  <th className="text-left py-3 px-4">{t('common.room')}</th>
                  <th className="text-left py-3 px-4">{t('common.check_in')}</th>
                  <th className="text-left py-3 px-4">{t('common.check_out')}</th>
                  <th className="text-center py-3 px-4">{t('common.nights')}</th>
                  <th className="text-right py-3 px-4">{t('common.rate')}</th>
                  <th className="text-center py-3 px-4">{t('common.status')}</th>
                  <th className="text-right py-3 px-4">{t('common.actions')}</th>
                </tr>
              </thead>
              <tbody>
                {filtered.map((r) => {
                  const guest = guestMap.get(r.primary_guest_id || '');
                  const room = roomMap.get(r.room_id || '');
                  return (
                    <tr key={r.id} className="border-b border-slate-100 hover:bg-slate-50 cursor-pointer" onClick={() => onSelectReservation?.(r.id)}>
                      <td className="py-3 px-4 font-medium text-blue-600">{r.reservation_number}</td>
                      <td className="py-3 px-4">{guest?.full_name || '-'}</td>
                      <td className="py-3 px-4">{room?.room_number || '-'}</td>
                      <td className="py-3 px-4">{formatDate(r.check_in_date)}</td>
                      <td className="py-3 px-4">{formatDate(r.check_out_date)}</td>
                      <td className="text-center py-3 px-4">{r.num_nights}</td>
                      <td className="text-right py-3 px-4">{formatIDR(r.rate)}</td>
                      <td className="text-center py-3 px-4"><ResStatusBadge status={r.status} label={t(`res.${r.status}`)} /></td>
                      <td className="text-right py-3 px-4"><button onClick={(e) => { e.stopPropagation(); onSelectReservation?.(r.id); }} className="text-blue-600 hover:text-blue-700 text-xs font-medium">{t('common.view')}</button></td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        </Card>
      )}

      <ReservationFormModal
        open={showForm}
        onClose={() => setShowForm(false)}
        branches={branches}
        rooms={rooms}
        roomTypes={roomTypes}
        guests={guests}
        bookingSources={bookingSources}
        userId={user!.id}
        orgId={user!.organization_id}
        defaultBranchId={selectedBranchId || branches[0]?.id || ''}
        onSaved={() => { setShowForm(false); load(); }}
      />
    </div>
  );
}

export function ReservationFormModal({ open, onClose, branches, rooms, roomTypes, guests, bookingSources, userId, orgId, defaultBranchId, reservation, onSaved }: {
  open: boolean; onClose: () => void;
  branches: Branch[]; rooms: Room[]; roomTypes: RoomType[]; guests: Guest[]; bookingSources: BookingSource[];
  userId: string; orgId: string; defaultBranchId: string;
  reservation?: Reservation | null;
  onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [form, setForm] = useState({
    branch_id: '', guest_id: '', room_type_id: '', room_id: '',
    check_in_date: todayISO(), check_in_time: '14:00', check_out_date: addDays(todayISO(), 1), check_out_time: '12:00',
    adults: '1', children: '0', rate: '0', discount: '0', tax: '0', deposit: '0',
    booking_source_id: '', status: 'tentative', special_requests: '', notes: '',
  });

  useEffect(() => {
    if (reservation) {
      setForm({
        branch_id: reservation.branch_id, guest_id: reservation.primary_guest_id || '', room_type_id: reservation.room_type_id || '', room_id: reservation.room_id || '',
        check_in_date: reservation.check_in_date, check_in_time: reservation.check_in_time.substring(0, 5),
        check_out_date: reservation.check_out_date, check_out_time: reservation.check_out_time.substring(0, 5),
        adults: String(reservation.adults), children: String(reservation.children), rate: String(reservation.rate),
        discount: String(reservation.discount), tax: String(reservation.tax), deposit: String(reservation.deposit),
        booking_source_id: reservation.booking_source_id || '', status: reservation.status, special_requests: reservation.special_requests || '', notes: reservation.notes || '',
      });
    } else {
      setForm((prev) => ({ ...prev, branch_id: defaultBranchId, check_in_date: todayISO(), check_out_date: addDays(todayISO(), 1) }));
    }
  }, [reservation, open, defaultBranchId]);

  const availableRooms = rooms.filter((r) => r.branch_id === form.branch_id && (!form.room_type_id || r.room_type_id === form.room_type_id));
  const nights = nightsBetween(form.check_in_date, form.check_out_date);

  const checkDoubleBooking = async (roomId: string): Promise<boolean> => {
    if (!roomId) return false;
    const { data } = await supabase
      .from('reservations')
      .select('id')
      .eq('room_id', roomId)
      .in('status', ['confirmed', 'checked_in', 'tentative'])
      .lt('check_in_date', form.check_out_date)
      .gt('check_out_date', form.check_in_date)
      .neq('id', reservation?.id || '');
    return (data || []).length > 0;
  };

  const handleSubmit = async () => {
    if (!form.branch_id || !form.guest_id || !form.room_type_id) { showToast('Required fields missing', 'error'); return; }
    if (form.room_id) {
      const isDouble = await checkDoubleBooking(form.room_id);
      if (isDouble) { showToast(t('res.double_book_warning'), 'error'); return; }
    }
    setSaving(true);
    const resNum = reservation?.reservation_number || `RES-${Date.now().toString().slice(-8)}`;
    const payload = {
      branch_id: form.branch_id, organization_id: orgId, reservation_number: resNum,
      primary_guest_id: form.guest_id, room_type_id: form.room_type_id, room_id: form.room_id || null,
      adults: parseInt(form.adults) || 1, children: parseInt(form.children) || 0,
      check_in_date: form.check_in_date, check_in_time: form.check_in_time,
      check_out_date: form.check_out_date, check_out_time: form.check_out_time,
      num_nights: nights, rate: parseFloat(form.rate) || 0, discount: parseFloat(form.discount) || 0,
      tax: parseFloat(form.tax) || 0, deposit: parseFloat(form.deposit) || 0,
      booking_source_id: form.booking_source_id || null, status: form.status,
      special_requests: form.special_requests || null, notes: form.notes || null,
      created_by: userId,
    };
    const { data: newRes, error } = reservation
      ? await supabase.from('reservations').update(payload).eq('id', reservation.id).select().single()
      : await supabase.from('reservations').insert(payload).select().single();
    if (error) { showToast(error.message, 'error'); setSaving(false); return; }

    // Create folio for new reservation
    if (!reservation && newRes) {
      await supabase.from('folios').insert({
        branch_id: form.branch_id, reservation_id: newRes.id, guest_id: form.guest_id,
        folio_number: `FOL-${Date.now().toString().slice(-8)}`, status: 'open',
      });
    }

    // Update room status if assigned
    if (form.room_id) {
      const roomStatus = form.status === 'checked_in' ? 'occupied' : form.status === 'confirmed' || form.status === 'tentative' ? 'reserved' : undefined;
      if (roomStatus) await supabase.from('rooms').update({ status: roomStatus }).eq('id', form.room_id);
    }

    // Audit log
    await supabase.from('audit_logs').insert({
      organization_id: orgId, branch_id: form.branch_id, user_id: userId,
      action: reservation ? 'reservation_modified' : 'reservation_created',
      object_type: 'reservation', object_id: newRes?.id || reservation?.id,
    });

    showToast('Saved', 'success');
    setSaving(false);
    onSaved();
  };

  return (
    <Modal open={open} onClose={onClose} title={reservation ? t('common.edit') : t('res.new_reservation')} size="xl"
      footer={<><Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button><Button loading={saving} onClick={handleSubmit}>{t('common.save')}</Button></>}>
      <form className="space-y-4">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
          <Select label={t('common.branch')} value={form.branch_id} onChange={(e) => setForm({ ...form, branch_id: e.target.value, room_id: '', room_type_id: '' })} required>
            <option value="">--</option>
            {branches.map((b) => <option key={b.id} value={b.id}>{b.name}</option>)}
          </Select>
          <Select label={t('common.guest')} value={form.guest_id} onChange={(e) => setForm({ ...form, guest_id: e.target.value })} required>
            <option value="">--</option>
            {guests.map((g) => <option key={g.id} value={g.id}>{g.full_name} {g.phone ? `(${g.phone})` : ''}</option>)}
          </Select>
          <Select label={t('common.booking_source')} value={form.booking_source_id} onChange={(e) => setForm({ ...form, booking_source_id: e.target.value })}>
            <option value="">--</option>
            {bookingSources.map((bs) => <option key={bs.id} value={bs.id}>{bs.name}</option>)}
          </Select>
        </div>
        <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
          <Input label={t('common.check_in')} type="date" value={form.check_in_date} onChange={(e) => setForm({ ...form, check_in_date: e.target.value })} required />
          <Input label="Time" type="time" value={form.check_in_time} onChange={(e) => setForm({ ...form, check_in_time: e.target.value })} />
          <Input label={t('common.check_out')} type="date" value={form.check_out_date} onChange={(e) => setForm({ ...form, check_out_date: e.target.value })} required />
          <Input label="Time" type="time" value={form.check_out_time} onChange={(e) => setForm({ ...form, check_out_time: e.target.value })} />
        </div>
        <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
          <Select label={t('common.room_type')} value={form.room_type_id} onChange={(e) => setForm({ ...form, room_type_id: e.target.value, room_id: '' })} required>
            <option value="">--</option>
            {roomTypes.filter((rt) => rt.branch_id === form.branch_id).map((rt) => <option key={rt.id} value={rt.id}>{rt.name}</option>)}
          </Select>
          <Select label={t('res.assign_room')} value={form.room_id} onChange={(e) => setForm({ ...form, room_id: e.target.value })}>
            <option value="">--</option>
            {availableRooms.map((r) => <option key={r.id} value={r.id}>{r.room_number} ({r.status})</option>)}
          </Select>
          <Input label={t('common.adults')} type="number" value={form.adults} onChange={(e) => setForm({ ...form, adults: e.target.value })} />
          <Input label={t('common.children')} type="number" value={form.children} onChange={(e) => setForm({ ...form, children: e.target.value })} />
        </div>
        <div className="grid grid-cols-2 md:grid-cols-5 gap-4">
          <Input label={t('common.rate')} type="number" value={form.rate} onChange={(e) => setForm({ ...form, rate: e.target.value })} />
          <Input label={t('common.discount')} type="number" value={form.discount} onChange={(e) => setForm({ ...form, discount: e.target.value })} />
          <Input label={t('common.tax')} type="number" value={form.tax} onChange={(e) => setForm({ ...form, tax: e.target.value })} />
          <Input label={t('common.deposit')} type="number" value={form.deposit} onChange={(e) => setForm({ ...form, deposit: e.target.value })} />
          <Select label={t('common.status')} value={form.status} onChange={(e) => setForm({ ...form, status: e.target.value })}>
            <option value="tentative">{t('res.tentative')}</option>
            <option value="confirmed">{t('res.confirmed')}</option>
            <option value="checked_in">{t('res.checked_in')}</option>
            <option value="checked_out">{t('res.checked_out')}</option>
            <option value="cancelled">{t('res.cancelled')}</option>
            <option value="no_show">{t('res.no_show')}</option>
          </Select>
        </div>
        <div className="bg-slate-50 rounded-lg p-3 text-sm text-slate-600">
          {t('common.nights')}: <span className="font-bold">{nights}</span> · Total: <span className="font-bold">{formatIDR((parseFloat(form.rate) || 0) * nights - (parseFloat(form.discount) || 0) + (parseFloat(form.tax) || 0))}</span>
        </div>
        <Textarea label={t('common.special_requests')} value={form.special_requests} onChange={(e) => setForm({ ...form, special_requests: e.target.value })} rows={2} />
        <Textarea label={t('common.notes')} value={form.notes} onChange={(e) => setForm({ ...form, notes: e.target.value })} rows={2} />
      </form>
    </Modal>
  );
}
