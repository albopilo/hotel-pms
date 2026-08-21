import { useState, useEffect, useCallback, useMemo } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal } from '@/components/ui/Modal';
import { Input, Select, Textarea } from '@/components/ui/Form';
import { ResStatusBadge, Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatIDR, formatDate, todayISO, addDays, nightsBetween } from '@/lib/format';
import { Plus, CalendarDays, Search, Trash2, Users } from 'lucide-react';
import type { Reservation, Guest, Room, RoomType, BookingSource, Branch } from '@/types/database';
import { generateDocumentNumber } from '@/lib/documentNumber';

interface RoomRow {
  room_type_id: string;
  room_id: string;
  rate: string;
}

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
  const branchIds = useMemo(() => selectedBranchId ? [selectedBranchId] : branches.map(b => b.id), [selectedBranchId, branches]);

  const load = useCallback(async () => {
    if (!branchIds.length) { setLoading(false); return; }
    setLoading(true);
    const [r, g, ro, rt, bs] = await Promise.all([
      supabase.from('reservations').select('*').in('branch_id', branchIds).order('created_at', { ascending: false }),
      supabase.from('guests').select('*').limit(500),
      supabase.from('rooms').select('*').in('branch_id', branchIds),
      supabase.from('room_types').select('*').in('branch_id', branchIds),
      supabase.from('booking_sources').select('*').order('sort_order')
    ]);
    setReservations((r.data || []).map((x: any) => ({ ...x, status: x.status === 'tentative' ? 'confirmed' : x.status })));
    setGuests(g.data || []);
    setRooms(ro.data || []);
    setRoomTypes(rt.data || []);
    setBookingSources(bs.data || []);
    setLoading(false);
  }, [branchIds]);

  useEffect(() => { load(); }, [load]);
  useEffect(() => { if (searchQuery !== localSearch) setLocalSearch(searchQuery); }, [searchQuery]);

  const guestMap = new Map(guests.map(x => [x.id, x]));
  const roomMap = new Map(rooms.map(x => [x.id, x]));
  const filtered = reservations.filter(r => {
    if (statusFilter !== 'all' && r.status !== statusFilter) return false;
    const q = localSearch.toLowerCase().trim();
    if (!q) return true;
    const g = guestMap.get(r.primary_guest_id || '');
    const ro = roomMap.get(r.room_id || '');
    return r.reservation_number.toLowerCase().includes(q) || (g?.full_name || '').toLowerCase().includes(q) || (ro?.room_number || '').includes(q) || (g?.phone || '').includes(q);
  });

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between flex-wrap gap-2">
        <h1 className="text-2xl font-bold text-slate-900">{t('nav.reservations')}</h1>
        <Button onClick={() => setShowForm(true)}><Plus size={18} />{t('action.new_reservation')}</Button>
      </div>
      <div className="flex gap-3 flex-wrap">
        <div className="relative flex-1 min-w-[200px]">
          <Search size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
          <input value={localSearch} onChange={e => setLocalSearch(e.target.value)} placeholder={t('common.search')} className="w-full rounded-lg border border-slate-300 pl-10 pr-3 py-2 text-sm" />
        </div>
        <select value={statusFilter} onChange={e => setStatusFilter(e.target.value)} className="rounded-lg border px-3 py-2 bg-white">
          <option value="all">{t('common.all')}</option>
          <option value="confirmed">{t('res.confirmed')}</option>
          <option value="checked_in">{t('res.checked_in')}</option>
          <option value="checked_out">{t('res.checked_out')}</option>
          <option value="cancelled">{t('res.cancelled')}</option>
          <option value="no_show">{t('res.no_show')}</option>
        </select>
      </div>

      {!filtered.length ? (
        <EmptyState icon={<CalendarDays size={48} />} title={t('res.no_reservations')} />
      ) : (
        <Card noPadding>
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b text-slate-500">
                  <th className="text-left py-3 px-4">{t('res.reservation_number')}</th>
                  <th className="text-left py-3 px-4">{t('common.guest')}</th>
                  <th className="text-left py-3 px-4">{t('common.room')}</th>
                  <th className="text-left py-3 px-4">{t('common.check_in')}</th>
                  <th className="text-left py-3 px-4">{t('common.check_out')}</th>
                  <th className="text-center py-3 px-4">{t('common.nights')}</th>
                  <th className="text-right py-3 px-4">{t('common.rate')}</th>
                  <th className="text-center py-3 px-4">{t('common.status')}</th>
                  <th />
                </tr>
              </thead>
              <tbody>
                {filtered.map(r => {
                  const g = guestMap.get(r.primary_guest_id || '');
                  const ro = roomMap.get(r.room_id || '');
                  return (
                    <tr key={r.id} onClick={() => onSelectReservation?.(r.id)} className="border-b hover:bg-slate-50 cursor-pointer">
                      <td className="py-3 px-4 text-blue-600 font-medium">
                        {r.reservation_number}
                        {r.is_group && <Badge color="purple" size="sm">Group</Badge>}
                      </td>
                      <td className="py-3 px-4">{g?.full_name || '-'}</td>
                      <td className="py-3 px-4">{ro?.room_number || '-'}</td>
                      <td className="py-3 px-4">{formatDate(r.check_in_date)}</td>
                      <td className="py-3 px-4">{formatDate(r.check_out_date)}</td>
                      <td className="text-center">{r.num_nights}</td>
                      <td className="text-right">{formatIDR(r.rate)}</td>
                      <td className="text-center"><ResStatusBadge status={r.status} label={t(`res.${r.status}`)} /></td>
                      <td><button onClick={e => { e.stopPropagation(); onSelectReservation?.(r.id); }} className="text-blue-600 text-xs">{t('common.view')}</button></td>
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
  open: boolean;
  onClose: () => void;
  branches: Branch[];
  rooms: Room[];
  roomTypes: RoomType[];
  guests: Guest[];
  bookingSources: BookingSource[];
  userId: string;
  orgId: string;
  defaultBranchId: string;
  reservation?: Reservation | null;
  onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [errors, setErrors] = useState<Record<string, string>>({});
  const [occupiedWarning, setOccupiedWarning] = useState<any>(null);
  const [dirtyWarning, setDirtyWarning] = useState<any>(null);

  const [form, setForm] = useState({
    branch_id: '', guest_id: '',
    check_in_date: todayISO(), check_in_time: '14:00',
    check_out_date: addDays(todayISO(), 1), check_out_time: '12:00',
    adults: '1', children: '0', discount: '0', tax: '0', deposit: '',
    booking_source_id: '', special_requests: '', notes: ''
  });

  const [roomRows, setRoomRows] = useState<RoomRow[]>([{ room_type_id: '', room_id: '', rate: '' }]);

  useEffect(() => {
    if (reservation) {
      setForm({
        branch_id: reservation.branch_id,
        guest_id: reservation.primary_guest_id || '',
        check_in_date: reservation.check_in_date,
        check_in_time: reservation.check_in_time?.substring(0, 5) || '14:00',
        check_out_date: reservation.check_out_date,
        check_out_time: reservation.check_out_time?.substring(0, 5) || '12:00',
        adults: String(reservation.adults || 1),
        children: String(reservation.children || 0),
        discount: String(reservation.discount ?? 0),
        tax: String(reservation.tax ?? 0),
        deposit: String(reservation.deposit ?? ''),
        booking_source_id: reservation.booking_source_id || '',
        special_requests: reservation.special_requests || '',
        notes: reservation.notes || ''
      });
      setRoomRows([{ room_type_id: reservation.room_type_id || '', room_id: reservation.room_id || '', rate: String(reservation.rate ?? '') }]);
    } else {
      setForm(x => ({ ...x, branch_id: defaultBranchId, check_in_date: todayISO(), check_out_date: addDays(todayISO(), 1) }));
      setRoomRows([{ room_type_id: '', room_id: '', rate: '' }]);
    }
  }, [reservation, open, defaultBranchId]);

  const set = (k: string, v: string) => setForm(x => ({ ...x, [k]: v }));

  const nights = nightsBetween(form.check_in_date, form.check_out_date);
  const isGroup = roomRows.length > 1;

  const availableRoomsForRow = (rowIdx: number) => {
    const usedRoomIds = roomRows.filter((_, i) => i !== rowIdx).map(r => r.room_id).filter(Boolean);
    return rooms.filter(r =>
      r.branch_id === form.branch_id &&
      (!roomRows[rowIdx].room_type_id || r.room_type_id === roomRows[rowIdx].room_type_id) &&
      !usedRoomIds.includes(r.id) &&
      r.status !== 'out_of_order' &&
      r.status !== 'out_of_service'
    );
  };

  const updateRoomRow = (idx: number, field: keyof RoomRow, value: string) => {
    setRoomRows(prev => prev.map((r, i) => i === idx ? { ...r, [field]: value } : r));
    if (field === 'room_type_id') {
      setRoomRows(prev => prev.map((r, i) => i === idx ? { ...r, room_id: '' } : r));
    }
    if (field === 'room_id') {
      const room = rooms.find(r => r.id === value);
      if (room && !roomRows[idx].rate) {
        setRoomRows(prev => prev.map((r, i) => i === idx ? { ...r, rate: String(room.base_rate) } : r));
      }
    }
  };

  const addRoomRow = () => {
    setRoomRows(prev => [...prev, { room_type_id: '', room_id: '', rate: '' }]);
  };

  const removeRoomRow = (idx: number) => {
    if (roomRows.length <= 1) return;
    setRoomRows(prev => prev.filter((_, i) => i !== idx));
  };

  const validate = () => {
    const e: Record<string, string> = {};
    if (!form.branch_id) e.branch_id = 'Branch is required.';
    if (!form.guest_id) e.guest_id = 'Guest is required.';
    if (!form.booking_source_id) e.booking_source_id = 'Booking source is required.';
    if (!form.check_in_date) e.check_in_date = 'Check-in date is required.';
    if (!form.check_out_date) e.check_out_date = 'Check-out date is required.';
    if (form.check_in_date && form.check_out_date && form.check_out_date <= form.check_in_date) e.check_out_date = 'Check-out must be after check-in.';
    if (form.deposit === '' || isNaN(Number(form.deposit)) || Number(form.deposit) < 0) e.deposit = 'Please enter a deposit amount. Enter Rp0 if no deposit is required.';

    roomRows.forEach((row, idx) => {
      if (!row.room_type_id) e[`room_${idx}_type`] = 'Room type is required.';
      if (!row.room_id) e[`room_${idx}_id`] = 'Assigned room is required.';
      if (row.rate === '' || isNaN(Number(row.rate)) || Number(row.rate) < 0) e[`room_${idx}_rate`] = 'Room rate is required.';
    });

    setErrors(e);
    return Object.keys(e).length === 0;
  };

  const checkConflict = async (roomId: string, excludeId?: string) => {
    if (!roomId) return [];
    let query = supabase
      .from('reservations')
      .select('id,check_in_date,check_out_date,status,primary_guest_id')
      .eq('room_id', roomId)
      .in('status', ['confirmed', 'checked_in'])
      .lt('check_in_date', form.check_out_date)
      .gt('check_out_date', form.check_in_date);
    if (excludeId) query = query.neq('id', excludeId);
    const { data } = await query;
    return data || [];
  };

  const checkOccupied = (roomId: string) => {
    const room = rooms.find(r => r.id === roomId);
    return room && room.status === 'occupied' ? room : null;
  };

  const checkDirty = (roomId: string) => {
    const room = rooms.find(r => r.id === roomId);
    return room && room.status === 'dirty' ? room : null;
  };

  const checkOutOfService = (roomId: string) => {
    const room = rooms.find(r => r.id === roomId);
    return room && (room.status === 'out_of_order' || room.status === 'out_of_service') ? room : null;
  };

  const save = async (force = false) => {
    if (!validate()) return;

    // Check conflicts for all rooms
    for (const row of roomRows) {
      const conflict = await checkConflict(row.room_id, reservation?.id);
      if (conflict.length) {
        showToast(`Room ${rooms.find(r => r.id === row.room_id)?.room_number} is already reserved for the selected dates.`, 'error');
        return;
      }
    }

    // Check out-of-order / out-of-service for all rooms (hard block)
    for (const row of roomRows) {
      const oos = checkOutOfService(row.room_id);
      if (oos) {
        showToast(`Room ${oos.room_number} is ${t(`room.${oos.status}`)} and cannot be reserved.`, 'error');
        return;
      }
    }

    // Check occupied for all rooms
    if (!force) {
      for (const row of roomRows) {
        const occupied = checkOccupied(row.room_id);
        if (occupied) {
          setOccupiedWarning(occupied);
          return;
        }
      }
      // Check dirty for all rooms
      for (const row of roomRows) {
        const dirty = checkDirty(row.room_id);
        if (dirty) {
          setDirtyWarning(dirty);
          return;
        }
      }
    }

    setSaving(true);

    const firstRow = roomRows[0];
    const totalRoomCharges = roomRows.reduce((sum, row) => sum + Number(row.rate) * nights, 0);

    const payload: any = {
      branch_id: form.branch_id,
      organization_id: orgId,
      reservation_number: reservation?.reservation_number || await generateDocumentNumber('RES'),
      primary_guest_id: form.guest_id,
      room_type_id: firstRow.room_type_id,
      room_id: firstRow.room_id,
      adults: Number(form.adults) || 1,
      children: Number(form.children) || 0,
      check_in_date: form.check_in_date,
      check_in_time: form.check_in_time,
      check_out_date: form.check_out_date,
      check_out_time: form.check_out_time,
      num_nights: nights,
      rate: Number(firstRow.rate),
      discount: Number(form.discount) || 0,
      tax: Number(form.tax) || 0,
      deposit: Number(form.deposit),
      booking_source_id: form.booking_source_id,
      status: 'confirmed',
      is_group: isGroup,
      special_requests: form.special_requests || null,
      notes: form.notes || null,
      created_by: userId
    };

    const { data, error } = reservation
      ? await supabase.from('reservations').update(payload).eq('id', reservation.id).select().single()
      : await supabase.from('reservations').insert(payload).select().single();

    if (error) { showToast(error.message, 'error'); setSaving(false); return; }

    // Create reservation_rooms entries for group reservations
    if (isGroup) {
      // Delete existing reservation_rooms if editing
      if (reservation) {
        await supabase.from('reservation_rooms').delete().eq('reservation_id', reservation.id);
      }
      const rrRows = roomRows.map(row => ({
        reservation_id: data.id,
        branch_id: form.branch_id,
        room_id: row.room_id,
        room_type_id: row.room_type_id,
        rate: Number(row.rate),
        check_in_date: form.check_in_date,
        check_out_date: form.check_out_date,
        num_nights: nights,
        status: 'active'
      }));
      const { error: rrError } = await supabase.from('reservation_rooms').insert(rrRows);
      if (rrError) {
        showToast(`Reservation created but room linking failed: ${rrError.message}`, 'error');
      }
    }

    if (!reservation && data) {
      const { data: folio, error: folioError } = await supabase
        .from('folios')
        .insert({
          branch_id: form.branch_id,
          reservation_id: data.id,
          guest_id: form.guest_id,
          folio_number: `FOL-${data.reservation_number.replace('RES-', '')}`,
          status: 'open'
        })
        .select()
        .single();

      if (folioError) {
        showToast(folioError.message, 'error');
      } else if (folio) {
        const items: any[] = [];

        // Room charges for each room
        roomRows.forEach((row, idx) => {
          const room = rooms.find(r => r.id === row.room_id);
          items.push({
            folio_id: folio.id,
            branch_id: form.branch_id,
            reservation_id: data.id,
            guest_id: form.guest_id,
            room_id: row.room_id,
            item_type: 'charge',
            category: 'room',
            description: isGroup ? `Room charge - Room ${room?.room_number || idx + 1}` : 'Room charge',
            quantity: Number(nights) || 1,
            unit_amount: Number(row.rate),
            amount: Number(row.rate) * (Number(nights) || 1),
            created_by: userId
          });
        });

        if (Number(form.discount) > 0) {
          items.push({
            folio_id: folio.id, branch_id: form.branch_id, reservation_id: data.id, guest_id: form.guest_id,
            item_type: 'discount', category: 'discount', description: 'Discount',
            quantity: 1, unit_amount: -Number(form.discount), amount: -Number(form.discount), created_by: userId
          });
        }

        if (Number(form.tax) > 0) {
          items.push({
            folio_id: folio.id, branch_id: form.branch_id, reservation_id: data.id, guest_id: form.guest_id,
            item_type: 'tax', category: 'tax', description: 'Tax',
            quantity: 1, unit_amount: Number(form.tax), amount: Number(form.tax), created_by: userId
          });
        }

        if (Number(form.deposit) > 0) {
          items.push({
            folio_id: folio.id, branch_id: form.branch_id, reservation_id: data.id, guest_id: form.guest_id,
            item_type: 'charge', category: 'deposit', description: 'Security deposit',
            quantity: 1, unit_amount: Number(form.deposit), amount: Number(form.deposit), created_by: userId
          });
        }

        const { error: itemError } = await supabase.from('folio_items').insert(items);

        if (itemError) {
          showToast(itemError.message, 'error');
        } else {
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
            deposit: totals.total_payments,
            balance: totals.total_charges - totals.total_discounts + totals.total_tax - totals.total_payments
          }).eq('id', folio.id);
        }
      }
    }

    // Update room status for all rooms
    for (const row of roomRows) {
      if (row.room_id) {
        await supabase.from('rooms').update({ status: 'reserved' }).eq('id', row.room_id);
      }
    }

    await supabase.from('audit_logs').insert({
      organization_id: orgId, branch_id: form.branch_id, user_id: userId,
      action: reservation ? 'reservation_modified' : 'reservation_created',
      object_type: 'reservation', object_id: data?.id || reservation?.id
    });

    setSaving(false);
    showToast('Saved', 'success');
    onSaved();
  };

  const totalRoomCharges = roomRows.reduce((sum, row) => sum + (Number(row.rate) || 0) * nights, 0);
  const grandTotal = totalRoomCharges - Number(form.discount || 0) + Number(form.tax || 0);

  return (
    <Modal open={open} onClose={onClose} title={reservation ? t('common.edit') : t('res.new_reservation')} size="xl"
      footer={<><Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button><Button loading={saving} onClick={() => save(false)}>{t('common.save')}</Button></>}>

      <form className="space-y-4">
        <div className="grid md:grid-cols-3 gap-4">
          <Select label={t('common.branch')} value={form.branch_id} onChange={e => set('branch_id', e.target.value)}>
            <option value="">--</option>
            {branches.map(b => <option key={b.id} value={b.id}>{b.name}</option>)}
          </Select>
          <Select label={t('common.guest')} value={form.guest_id} onChange={e => set('guest_id', e.target.value)}>
            <option value="">--</option>
            {guests.map(g => <option key={g.id} value={g.id}>{g.full_name}</option>)}
          </Select>
          <Select label={t('common.booking_source')} value={form.booking_source_id} onChange={e => set('booking_source_id', e.target.value)}>
            <option value="">--</option>
            {bookingSources.map(b => <option key={b.id} value={b.id}>{b.name}</option>)}
          </Select>
        </div>

        <div className="grid md:grid-cols-4 gap-4">
          <Input label={t('common.check_in')} type="date" value={form.check_in_date} onChange={e => set('check_in_date', e.target.value)} />
          <Input label="Time" type="time" value={form.check_in_time} onChange={e => set('check_in_time', e.target.value)} />
          <Input label={t('common.check_out')} type="date" value={form.check_out_date} onChange={e => set('check_out_date', e.target.value)} />
          <Input label="Time" type="time" value={form.check_out_time} onChange={e => set('check_out_time', e.target.value)} />
        </div>

        <div className="grid md:grid-cols-2 gap-4">
          <Input label={t('common.adults')} type="number" value={form.adults} onChange={e => set('adults', e.target.value)} />
          <Input label={t('common.children')} type="number" value={form.children} onChange={e => set('children', e.target.value)} />
        </div>

        {/* Room rows */}
        <div className="space-y-3">
          <div className="flex items-center justify-between">
            <label className="text-sm font-medium text-slate-700">
              {isGroup ? `${t('res.group_rooms')} (${roomRows.length})` : t('res.assign_room')}
            </label>
            <Button type="button" size="sm" variant="outline" onClick={addRoomRow}>
              <Plus size={14} /> {t('res.add_room')}
            </Button>
          </div>

          {roomRows.map((row, idx) => (
            <div key={idx} className="grid grid-cols-1 md:grid-cols-[1fr_1fr_1fr_auto] gap-3 items-end p-3 rounded-lg border border-slate-200 bg-slate-50">
              <Select label={t('common.room_type')} value={row.room_type_id} onChange={e => updateRoomRow(idx, 'room_type_id', e.target.value)}>
                <option value="">--</option>
                {roomTypes.filter(r => r.branch_id === form.branch_id).map(r => <option key={r.id} value={r.id}>{r.name}</option>)}
              </Select>
              <Select label={t('common.room')} value={row.room_id} onChange={e => updateRoomRow(idx, 'room_id', e.target.value)}>
                <option value="">--</option>
                {availableRoomsForRow(idx).map(r => <option key={r.id} value={r.id}>{r.room_number} ({r.status})</option>)}
              </Select>
              <Input label={t('common.rate')} type="number" value={row.rate} onChange={e => updateRoomRow(idx, 'rate', e.target.value)} />
              {roomRows.length > 1 && (
                <Button type="button" size="sm" variant="ghost" onClick={() => removeRoomRow(idx)} className="mb-1">
                  <Trash2 size={16} className="text-red-500" />
                </Button>
              )}
              {errors[`room_${idx}_type`] && <p className="text-xs text-red-500 md:col-span-4">{errors[`room_${idx}_type`]}</p>}
              {errors[`room_${idx}_id`] && <p className="text-xs text-red-500 md:col-span-4">{errors[`room_${idx}_id`]}</p>}
              {errors[`room_${idx}_rate`] && <p className="text-xs text-red-500 md:col-span-4">{errors[`room_${idx}_rate`]}</p>}
            </div>
          ))}
        </div>

        <div className="grid md:grid-cols-3 gap-4">
          <Input label={t('common.discount')} type="number" value={form.discount} onChange={e => set('discount', e.target.value)} />
          <Input label={t('common.tax')} type="number" value={form.tax} onChange={e => set('tax', e.target.value)} />
          <Input label={t('common.deposit')} type="number" value={form.deposit} onChange={e => set('deposit', e.target.value)} />
        </div>

        <div className="bg-slate-50 rounded-lg p-3 text-sm space-y-1">
          <div>{t('common.nights')}: <b>{nights}</b></div>
          <div>{t('common.room')} charges: <b>{formatIDR(totalRoomCharges)}</b></div>
          {isGroup && <div className="text-xs text-slate-500">{roomRows.length} rooms × {nights} nights</div>}
          <div className="pt-1 border-t border-slate-200">Total: <b>{formatIDR(grandTotal)}</b></div>
        </div>

        {Object.keys(errors).length > 0 &&
          <div className="rounded bg-red-50 text-red-700 p-3 text-sm">
            {Object.entries(errors).map(([k, v]) => <div key={k}>{v}</div>)}
          </div>}

        <Textarea label={t('common.special_requests')} value={form.special_requests} onChange={e => set('special_requests', e.target.value)} rows={2} />
        <Textarea label={t('common.notes')} value={form.notes} onChange={e => set('notes', e.target.value)} rows={2} />
      </form>

      {occupiedWarning &&
        <Modal open={true} onClose={() => setOccupiedWarning(null)} title="Warning">
          <div className="space-y-4">
            <p>Room {occupiedWarning.room_number} is currently occupied.</p>
            <p>Current room status: Occupied</p>
            <div className="flex justify-end gap-2">
              <Button variant="secondary" onClick={() => setOccupiedWarning(null)}>Cancel</Button>
              <Button onClick={() => { setOccupiedWarning(null); save(true); }}>Continue</Button>
            </div>
          </div>
        </Modal>
      }
      {dirtyWarning &&
        <Modal open={true} onClose={() => setDirtyWarning(null)} title={t('rooms.housekeeping')}>
          <div className="space-y-4">
            <div className="flex items-start gap-2">
              <AlertCircle size={20} className="text-amber-600 flex-shrink-0 mt-0.5" />
              <div>
                <p className="font-medium text-slate-800">Room {dirtyWarning.room_number} — {t('room.dirty')}</p>
                <p className="text-sm text-slate-600 mt-1">{t('rooms.dirty_warning')}</p>
              </div>
            </div>
            <div className="flex justify-end gap-2">
              <Button variant="secondary" onClick={() => setDirtyWarning(null)}>{t('common.cancel')}</Button>
              <Button onClick={() => { setDirtyWarning(null); save(true); }}>{t('rooms.dirty_warning_continue')}</Button>
            </div>
          </div>
        </Modal>
      }
    </Modal>
  );
}
