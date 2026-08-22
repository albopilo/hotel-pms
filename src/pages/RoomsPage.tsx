import { useState, useEffect, useCallback, useMemo } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal, ConfirmModal } from '@/components/ui/Modal';
import { Input, Select, Textarea } from '@/components/ui/Form';
import { RoomStatusBadge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatIDR, todayISO, addDays } from '@/lib/format';
import { Plus, CreditCard as Edit, BedDouble, Sparkles } from 'lucide-react';
import type { Room, RoomType, RoomStatus, Branch } from '@/types/database';
import { saveDraft, loadDraft, clearDraft } from '@/lib/formDraft';

const ROOM_DRAFT_KEY = 'room_form_draft';

const initialRoomForm = {
  branch_id: '',
  room_type_id: '',
  room_number: '',
  floor: '1',
  base_rate: '0',
  max_occupancy: '2',
  status: 'available',
  is_active: true,
  notes: '',
};

const STATUSES: RoomStatus[] = ['available', 'reserved', 'occupied', 'dirty', 'cleaning', 'inspected', 'out_of_service', 'out_of_order'];

const HOUSEKEEPING_TARGETS: RoomStatus[] = ['available', 'dirty', 'cleaning', 'inspected', 'out_of_service', 'out_of_order'];

export function RoomsPage() {
  const { user, branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [rooms, setRooms] = useState<Room[]>([]);
  const [roomTypes, setRoomTypes] = useState<RoomType[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedRoom, setSelectedRoom] = useState<Room | null>(null);
  const [showForm, setShowForm] = useState(false);
  const [editingRoom, setEditingRoom] = useState<Room | null>(null);
  const [showHousekeeping, setShowHousekeeping] = useState(false);

  const branchIds = useMemo(
    () => selectedBranchId ? [selectedBranchId] : branches.map((b) => b.id),
    [selectedBranchId, branches]
  );
  const isSuperAdmin = user?.role === 'super_admin';

  const load = useCallback(async () => {
    if (branchIds.length === 0) { setLoading(false); return; }
    setLoading(true);
    const [{ data: roomData }, { data: typeData }] = await Promise.all([
      supabase.from('rooms').select('*').in('branch_id', branchIds).eq('is_active', true).order('room_number'),
      supabase.from('room_types').select('*')
    ]);
    setRooms((roomData as Room[]) || []);
    setRoomTypes((typeData as RoomType[]) || []);
    setLoading(false);
  }, [branchIds]);

  useEffect(() => { load(); }, [load]);

  const grouped = roomTypes.map((rt) => ({
    type: rt,
    rooms: rooms.filter((r) => r.room_type_id === rt.id),
  })).filter((g) => g.rooms.length > 0);

  const untyped = rooms.filter((r) => !roomTypes.find((rt) => rt.id === r.room_type_id));

  const changeStatus = async (room: Room, newStatus: RoomStatus) => {
    await supabase
      .from('rooms')
      .update({
        status: newStatus,
        out_of_service_reason: null,
        out_of_service_until: null,
      })
      .eq('id', room.id);
    const { error: historyError } = await supabase
      .from('room_status_history')
      .insert({
        room_id: room.id,
        previous_status: room.status,
        new_status: newStatus,
        changed_by: user?.id,
      });
    if (historyError) console.error(historyError);
    showToast(`${t('common.status')}: ${t(`room.${newStatus}`)}`, 'success');
    setSelectedRoom({ ...room, status: newStatus, out_of_service_reason: null, out_of_service_until: null });
    load();
  };

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-slate-900">{t('rooms.title')}</h1>
        <div className="flex gap-2">
          <Button variant="outline" onClick={() => setShowHousekeeping(true)}>
            <Sparkles size={18} /> {t('rooms.housekeeping')}
          </Button>
          {(isSuperAdmin || user?.role === 'manager') && (
            <Button onClick={() => { setEditingRoom(null); setShowForm(true); }}>
              <Plus size={18} /> {t('common.add')}
            </Button>
          )}
        </div>
      </div>

      {rooms.length === 0 ? (
        <EmptyState icon={<BedDouble size={48} />} title={t('rooms.no_rooms')} />
      ) : (
        <div className="space-y-4">
          {grouped.map((group) => (
            <Card key={group.type.id} title={group.type.name}>
              <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-6 gap-3">
                {group.rooms.map((room) => (
                  <button
                    key={room.id}
                    onClick={() => setSelectedRoom(room)}
                    className="flex flex-col items-center gap-1 p-3 rounded-lg border-2 border-slate-200 hover:border-blue-400 transition-colors"
                  >
                    <span className="font-bold text-lg text-slate-800">{room.room_number}</span>
                    <RoomStatusBadge status={room.status} label={t(`room.${room.status}`)} />
                    <span className="text-xs text-slate-400">Fl {room.floor}</span>
                  </button>
                ))}
              </div>
            </Card>
          ))}
          {untyped.length > 0 && (
            <Card title="Other">
              <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-6 gap-3">
                {untyped.map((room) => (
                  <button key={room.id} onClick={() => setSelectedRoom(room)} className="flex flex-col items-center gap-1 p-3 rounded-lg border-2 border-slate-200 hover:border-blue-400">
                    <span className="font-bold text-lg text-slate-800">{room.room_number}</span>
                    <RoomStatusBadge status={room.status} label={t(`room.${room.status}`)} />
                  </button>
                ))}
              </div>
            </Card>
          )}
        </div>
      )}

      {/* Room detail modal */}
      <Modal
        open={!!selectedRoom}
        onClose={() => setSelectedRoom(null)}
        title={selectedRoom ? `${t('common.room')}: ${selectedRoom.room_number}` : ''}
        size="md"
      >
        {selectedRoom && (
          <RoomDetail
            room={selectedRoom}
            roomType={roomTypes.find((rt) => rt.id === selectedRoom.room_type_id)}
            branch={branches.find((b) => b.id === selectedRoom.branch_id)}
            onStatusChange={(s) => changeStatus(selectedRoom, s)}
            onEdit={() => { setEditingRoom(selectedRoom); setShowForm(true); setSelectedRoom(null); }}
            canEdit={isSuperAdmin || user?.role === 'manager'}
          />
        )}
      </Modal>

      {/* Housekeeping modal */}
      <HousekeepingModal
        open={showHousekeeping}
        onClose={() => setShowHousekeeping(false)}
        rooms={rooms}
        userId={user?.id || ''}
        onSaved={() => { setShowHousekeeping(false); load(); }}
      />

      {/* Room form */}
      <RoomFormModal
        open={showForm}
        onClose={() => setShowForm(false)}
        room={editingRoom}
        branches={branches}
        roomTypes={roomTypes}
        userId={user?.id || ''}
        onSaved={() => { setShowForm(false); load(); }}
      />
    </div>
  );
}

function HousekeepingModal({ open, onClose, rooms, userId, onSaved }: {
  open: boolean;
  onClose: () => void;
  rooms: Room[];
  userId: string;
  onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [selectedRoomIds, setSelectedRoomIds] = useState<string[]>([]);
  const [newStatus, setNewStatus] = useState<RoomStatus>('available');
  const [reason, setReason] = useState('');
  const [revertNights, setRevertNights] = useState('1');
  const [saving, setSaving] = useState(false);

  const selectedRooms = rooms.filter((r) => selectedRoomIds.includes(r.id));
  const hasOccupied = selectedRooms.some((r) => r.status === 'occupied');
  const isOutOfService = newStatus === 'out_of_order' || newStatus === 'out_of_service';

  useEffect(() => {
    if (open) {
      setSelectedRoomIds([]);
      setNewStatus('available');
      setReason('');
      setRevertNights('1');
    }
  }, [open]);

  const toggleRoom = (roomId: string) => {
    setSelectedRoomIds((prev) =>
      prev.includes(roomId) ? prev.filter((id) => id !== roomId) : [...prev, roomId]
    );
  };

  const handleSubmit = async () => {
    if (selectedRoomIds.length === 0) { showToast('Select at least one room', 'error'); return; }
    if (hasOccupied) {
      showToast(t('rooms.cannot_change_occupied'), 'error');
      return;
    }
    if (isOutOfService && !reason.trim()) {
      showToast(t('rooms.reason_required'), 'error');
      return;
    }

    setSaving(true);
    const update: Record<string, unknown> = { status: newStatus };
    if (isOutOfService) {
      update.out_of_service_reason = reason.trim();
      const nights = parseInt(revertNights) || 1;
      update.out_of_service_until = addDays(todayISO(), nights);
    } else {
      update.out_of_service_reason = null;
      update.out_of_service_until = null;
    }

    for (const room of selectedRooms) {
      const { error } = await supabase.from('rooms').update(update).eq('id', room.id);
      if (error) {
        showToast(error.message, 'error');
        setSaving(false);
        return;
      }

      await supabase.from('room_status_history').insert({
        room_id: room.id,
        previous_status: room.status,
        new_status: newStatus,
        changed_by: userId,
        reason: isOutOfService ? reason.trim() : null,
        revert_after_nights: isOutOfService ? parseInt(revertNights) || 1 : null,
        revert_to: isOutOfService ? 'dirty' : null,
      });
    }

    showToast(`${selectedRoomIds.length} room(s): ${t(`room.${newStatus}`)}`, 'success');
    setSaving(false);
    onSaved();
  };

  return (
    <Modal
      open={open}
      onClose={onClose}
      title={t('rooms.housekeeping')}
      size="lg"
      footer={
        <>
          <Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button>
          <Button loading={saving} onClick={handleSubmit} disabled={hasOccupied}>
            {t('common.save')} {selectedRoomIds.length > 0 && `(${selectedRoomIds.length})`}
          </Button>
        </>
      }
    >
      <div className="space-y-4">
        <p className="text-sm text-slate-500">Select one or more rooms to update. Click rooms to toggle selection.</p>

        {/* Room grid with multi-select */}
        <div className="max-h-64 overflow-y-auto border border-slate-200 rounded-lg p-3">
          <div className="grid grid-cols-3 sm:grid-cols-4 md:grid-cols-5 gap-2">
            {rooms.map((r) => {
              const isSelected = selectedRoomIds.includes(r.id);
              const isOcc = r.status === 'occupied';
              return (
                <button
                  key={r.id}
                  type="button"
                  disabled={isOcc}
                  onClick={() => toggleRoom(r.id)}
                  className={`flex flex-col items-center gap-1 p-2 rounded-lg border-2 transition-colors ${
                    isSelected
                      ? 'border-blue-500 bg-blue-50'
                      : isOcc
                        ? 'border-slate-200 bg-slate-100 opacity-50 cursor-not-allowed'
                        : 'border-slate-200 hover:border-blue-400'
                  }`}
                >
                  <span className="font-bold text-sm text-slate-800">{r.room_number}</span>
                  <RoomStatusBadge status={r.status} label={t(`room.${r.status}`)} />
                </button>
              );
            })}
          </div>
        </div>

        {selectedRooms.length > 0 && (
          <div className="text-sm text-slate-600">
            <span className="font-medium">{selectedRooms.length} room(s) selected:</span>{' '}
            {selectedRooms.map((r) => r.room_number).join(', ')}
          </div>
        )}

        {/* Occupied warning */}
        {hasOccupied && (
          <div className="rounded-lg bg-amber-50 border border-amber-200 p-3 text-sm text-amber-700">
            {t('rooms.cannot_change_occupied')}
          </div>
        )}

        {/* New status */}
        <div>
          <label className="text-sm font-medium text-slate-700 mb-1.5 block">{t('rooms.new_status')}</label>
          <div className="flex flex-wrap gap-2">
            {HOUSEKEEPING_TARGETS.map((s) => (
              <button
                key={s}
                type="button"
                disabled={hasOccupied}
                onClick={() => setNewStatus(s)}
                className={`px-3 py-1.5 rounded-lg text-xs font-medium border transition-colors ${
                  s === newStatus
                    ? 'bg-blue-600 text-white border-blue-600'
                    : 'bg-white text-slate-600 border-slate-300 hover:bg-slate-50 disabled:opacity-50 disabled:cursor-not-allowed'
                }`}
              >
                {t(`room.${s}`)}
              </button>
            ))}
          </div>
        </div>

        {/* Reason + revert nights for out of order / out of service */}
        {isOutOfService && (
          <div className="space-y-3 rounded-lg bg-slate-50 border border-slate-200 p-3">
            <Textarea
              label={t('rooms.reason')}
              value={reason}
              onChange={(e) => setReason(e.target.value)}
              rows={2}
              placeholder={t('rooms.reason_placeholder')}
            />
            <Input
              label={t('rooms.revert_nights')}
              type="number"
              value={revertNights}
              onChange={(e) => setRevertNights(e.target.value)}
              min="1"
            />
            <p className="text-xs text-slate-500">
              {t('rooms.revert_hint')}
            </p>
          </div>
        )}
      </div>
    </Modal>
  );
}

function RoomDetail({ room, roomType, branch, onStatusChange, onEdit, canEdit }: {
  room: Room;
  roomType?: RoomType;
  branch?: Branch;
  onStatusChange: (s: RoomStatus) => void;
  onEdit: () => void;
  canEdit: boolean;
}) {
  const { t } = useI18n();
  return (
    <div className="space-y-4">
      <div className="grid grid-cols-2 gap-4 text-sm">
        <div><span className="text-slate-500">{t('common.branch')}:</span> <span className="font-medium">{branch?.name || '-'}</span></div>
        <div><span className="text-slate-500">{t('common.room_type')}:</span> <span className="font-medium">{roomType?.name || '-'}</span></div>
        <div><span className="text-slate-500">{t('common.floor')}:</span> <span className="font-medium">{room.floor}</span></div>
        <div><span className="text-slate-500">{t('common.max_occupancy')}:</span> <span className="font-medium">{room.max_occupancy}</span></div>
        <div><span className="text-slate-500">{t('rooms.base_rate')}:</span> <span className="font-medium">{formatIDR(room.base_rate)}</span></div>
        <div><span className="text-slate-500">{t('common.status')}:</span> <RoomStatusBadge status={room.status} label={t(`room.${room.status}`)} /></div>
      </div>
      {room.out_of_service_reason && (
        <div className="text-sm bg-amber-50 border border-amber-200 rounded-lg p-2">
          <span className="font-medium text-amber-700">{t('rooms.reason')}:</span> <span className="text-amber-800">{room.out_of_service_reason}</span>
          {room.out_of_service_until && (
            <span className="block text-xs text-amber-600 mt-0.5">{t('rooms.revert_until')}: {room.out_of_service_until}</span>
          )}
        </div>
      )}
      {room.notes && <div className="text-sm"><span className="text-slate-500">{t('common.notes')}:</span> <span>{room.notes}</span></div>}

      {canEdit && (
        <>
          <div>
            <p className="text-sm font-medium text-slate-700 mb-2">{t('rooms.change_status')}</p>
            <div className="flex flex-wrap gap-2">
              {STATUSES.map((s) => (
                <button
                  key={s}
                  onClick={() => onStatusChange(s)}
                  disabled={s === room.status}
                  className={`px-3 py-1.5 rounded-lg text-xs font-medium border transition-colors ${
                    s === room.status ? 'bg-blue-600 text-white border-blue-600' : 'bg-white text-slate-600 border-slate-300 hover:bg-slate-50'
                  }`}
                >
                  {t(`room.${s}`)}
                </button>
              ))}
            </div>
          </div>
          <div className="flex justify-end">
            <Button variant="outline" size="sm" onClick={onEdit}><Edit size={14} /> {t('common.edit')}</Button>
          </div>
        </>
      )}
    </div>
  );
}

function RoomFormModal({ open, onClose, room, branches, roomTypes, userId, onSaved }: {
  open: boolean;
  onClose: () => void;
  room: Room | null;
  branches: Branch[];
  roomTypes: RoomType[];
  userId: string;
  onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [form, setForm] = useState(() => {
    const draft = loadDraft<typeof initialRoomForm>(ROOM_DRAFT_KEY);
    return draft || { ...initialRoomForm };
  });

  useEffect(() => {
    if (room) {
      setForm({
        branch_id: room.branch_id,
        room_type_id: room.room_type_id,
        room_number: room.room_number,
        floor: String(room.floor),
        base_rate: String(room.base_rate),
        max_occupancy: String(room.max_occupancy),
        status: room.status,
        is_active: room.is_active,
        notes: room.notes || '',
      });
    } else {
      const draft = loadDraft<typeof initialRoomForm>(ROOM_DRAFT_KEY);
      setForm(draft ? { ...initialRoomForm, ...draft, branch_id: draft.branch_id || branches[0]?.id || '' } : { ...initialRoomForm, branch_id: branches[0]?.id || '' });
    }
  }, [room, open, branches]);

  useEffect(() => {
    if (open && !room) saveDraft(ROOM_DRAFT_KEY, form);
  }, [form, open, room]);

  const availableTypes = roomTypes.filter((rt) => rt.branch_id === form.branch_id);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!form.branch_id || !form.room_type_id || !form.room_number) { showToast('Required fields missing', 'error'); return; }
    setSaving(true);
    const payload = {
      branch_id: form.branch_id,
      room_type_id: form.room_type_id,
      room_number: form.room_number,
      floor: parseInt(form.floor),
      base_rate: parseFloat(form.base_rate) || 0,
      max_occupancy: parseInt(form.max_occupancy) || 2,
      status: form.status,
      is_active: form.is_active,
      notes: form.notes || null,
    };
    const { error } = room
      ? await supabase.from('rooms').update(payload).eq('id', room.id)
      : await supabase.from('rooms').insert(payload);
    if (error) showToast(error.message, 'error');
    else { showToast('Saved', 'success'); clearDraft(ROOM_DRAFT_KEY); onSaved(); }
    setSaving(false);
  };

  const handleCancel = () => { clearDraft(ROOM_DRAFT_KEY); onClose(); };

  return (
    <Modal open={open} onClose={handleCancel} title={room ? t('common.edit') : t('common.add')} size="md"
      footer={<><Button variant="secondary" onClick={handleCancel}>{t('common.cancel')}</Button><Button loading={saving} onClick={handleSubmit}>{t('common.save')}</Button></>}>
      <form onSubmit={handleSubmit} className="space-y-4">
        <Select label={t('common.branch')} value={form.branch_id} onChange={(e) => setForm({ ...form, branch_id: e.target.value, room_type_id: '' })} required>
          <option value="">--</option>
          {branches.map((b) => <option key={b.id} value={b.id}>{b.name}</option>)}
        </Select>
        <Select label={t('common.room_type')} value={form.room_type_id} onChange={(e) => setForm({ ...form, room_type_id: e.target.value })} required>
          <option value="">--</option>
          {availableTypes.map((rt) => <option key={rt.id} value={rt.id}>{rt.name} ({formatIDR(rt.base_rate)})</option>)}
        </Select>
        <div className="grid grid-cols-2 gap-4">
          <Input label={t('rooms.room_number')} value={form.room_number} onChange={(e) => setForm({ ...form, room_number: e.target.value })} required />
          <Input label={t('common.floor')} type="number" value={form.floor} onChange={(e) => setForm({ ...form, floor: e.target.value })} required />
          <Input label={t('rooms.base_rate')} type="number" value={form.base_rate} onChange={(e) => setForm({ ...form, base_rate: e.target.value })} />
          <Input label={t('common.max_occupancy')} type="number" value={form.max_occupancy} onChange={(e) => setForm({ ...form, max_occupancy: e.target.value })} />
        </div>
        <Select label={t('common.status')} value={form.status} onChange={(e) => setForm({ ...form, status: e.target.value })}>
          {STATUSES.map((s) => <option key={s} value={s}>{t(`room.${s}`)}</option>)}
        </Select>
        <Textarea label={t('common.notes')} value={form.notes} onChange={(e) => setForm({ ...form, notes: e.target.value })} rows={2} />
      </form>
    </Modal>
  );
}
