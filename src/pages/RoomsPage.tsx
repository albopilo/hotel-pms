import { useState, useEffect, useCallback } from 'react';
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
import { formatIDR } from '@/lib/format';
import { Plus, Edit, BedDouble, X } from 'lucide-react';
import type { Room, RoomType, RoomStatus, Branch } from '@/types/database';

const STATUSES: RoomStatus[] = ['available', 'reserved', 'occupied', 'dirty', 'cleaning', 'inspected', 'out_of_service', 'out_of_order'];

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

  const branchIds = selectedBranchId ? [selectedBranchId] : branches.map((b) => b.id);
  const isSuperAdmin = user?.role === 'super_admin';

  const load = useCallback(async () => {
    if (branchIds.length === 0) { setLoading(false); return; }
    setLoading(true);
    const [{ data: roomData }, { data: typeData }] = await Promise.all([
      supabase.from('rooms').select('*').in('branch_id', branchIds).eq('is_active', true).order('room_number'),
      supabase.from('room_types').select('*').in('branch_id', branchIds).order('sort_order'),
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
    const { error } = await supabase.from('rooms').update({ status: newStatus }).eq('id', room.id);
    if (error) { showToast(error.message, 'error'); return; }
    await supabase.from('room_status_history').insert({
      room_id: room.id,
      previous_status: room.status,
      new_status: newStatus,
      changed_by: user?.id,
    });
    showToast(`${t('common.status')}: ${t(`room.${newStatus}`)}`, 'success');
    setSelectedRoom({ ...room, status: newStatus });
    load();
  };

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-slate-900">{t('rooms.title')}</h1>
        {(isSuperAdmin || user?.role === 'manager') && (
          <Button onClick={() => { setEditingRoom(null); setShowForm(true); }}>
            <Plus size={18} /> {t('common.add')}
          </Button>
        )}
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

      {/* Room form */}
      <RoomFormModal
        open={showForm}
        onClose={() => setShowForm(false)}
        room={editingRoom}
        branches={branches}
        roomTypes={roomTypes}
        userId={user!.id}
        onSaved={() => { setShowForm(false); load(); }}
      />
    </div>
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
  const [form, setForm] = useState({
    branch_id: '',
    room_type_id: '',
    room_number: '',
    floor: '1',
    base_rate: '0',
    max_occupancy: '2',
    status: 'available',
    is_active: true,
    notes: '',
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
      setForm({ branch_id: branches[0]?.id || '', room_type_id: '', room_number: '', floor: '1', base_rate: '0', max_occupancy: '2', status: 'available', is_active: true, notes: '' });
    }
  }, [room, open, branches]);

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
    else { showToast('Saved', 'success'); onSaved(); }
    setSaving(false);
  };

  return (
    <Modal open={open} onClose={onClose} title={room ? t('common.edit') : t('common.add')} size="md"
      footer={<><Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button><Button loading={saving} onClick={handleSubmit}>{t('common.save')}</Button></>}>
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
