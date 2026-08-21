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
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatIDR } from '@/lib/format';
import { Plus, Edit, DoorOpen, Trash2 } from 'lucide-react';
import type { RoomType, Branch } from '@/types/database';

export function RoomTypesPage() {
  const { user, branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [roomTypes, setRoomTypes] = useState<RoomType[]>([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [editing, setEditing] = useState<RoomType | null>(null);
  const [deleteTarget, setDeleteTarget] = useState<RoomType | null>(null);

  const branchIds = useMemo(
  () => selectedBranchId ? [selectedBranchId] : branches.map((b) => b.id),
  [selectedBranchId, branches]
);

  const load = useCallback(async () => {
    if (branchIds.length === 0) { setLoading(false); return; }
    setLoading(true);
    const { data } = await supabase.from('room_types').select('*').in('branch_id', branchIds).order('sort_order');
    setRoomTypes((data as RoomType[]) || []);
    setLoading(false);
  }, [branchIds]);

  useEffect(() => { load(); }, [load]);

  const handleDelete = async () => {
    if (!deleteTarget) return;
    const { error } = await supabase.from('room_types').delete().eq('id', deleteTarget.id);
    if (error) showToast(error.message, 'error');
    else { showToast('Deleted', 'success'); load(); }
    setDeleteTarget(null);
  };

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-slate-900">{t('nav.room_types')}</h1>
        <Button onClick={() => { setEditing(null); setShowForm(true); }}><Plus size={18} /> {t('common.add')}</Button>
      </div>

      {roomTypes.length === 0 ? (
        <EmptyState icon={<DoorOpen size={48} />} title={t('common.no_data')} />
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          {roomTypes.map((rt) => {
            const branch = branches.find((b) => b.id === rt.branch_id);
            return (
              <Card key={rt.id}>
                <div className="flex items-start justify-between">
                  <div>
                    <h3 className="font-semibold text-slate-800">{rt.name}</h3>
                    <p className="text-xs text-slate-400">{branch?.name} · {rt.code}</p>
                  </div>
                  <div className="flex gap-1">
                    <button onClick={() => { setEditing(rt); setShowForm(true); }} className="text-slate-400 hover:text-blue-600"><Edit size={16} /></button>
                    <button onClick={() => setDeleteTarget(rt)} className="text-slate-400 hover:text-red-600"><Trash2 size={16} /></button>
                  </div>
                </div>
                <div className="mt-3 grid grid-cols-2 gap-2 text-sm">
                  <div><span className="text-slate-500">{t('rooms.base_rate')}:</span> <span className="font-medium">{formatIDR(rt.base_rate)}</span></div>
                  <div><span className="text-slate-500">{t('common.max_occupancy')}:</span> <span className="font-medium">{rt.max_occupancy}</span></div>
                  <div><span className="text-slate-500">Tax:</span> <span className="font-medium">{rt.default_tax_rate}%</span></div>
                </div>
                {rt.description && <p className="mt-2 text-sm text-slate-500">{rt.description}</p>}
              </Card>
            );
          })}
        </div>
      )}

      <RoomTypeFormModal open={showForm} onClose={() => setShowForm(false)} roomType={editing} branches={branches} onSaved={() => { setShowForm(false); load(); }} />
      <ConfirmModal open={!!deleteTarget} onClose={() => setDeleteTarget(null)} onConfirm={handleDelete} title="Delete Room Type" message={`Delete "${deleteTarget?.name}"? This cannot be undone.`} confirmLabel={t('common.delete')} variant="danger" />
    </div>
  );
}

function RoomTypeFormModal({ open, onClose, roomType, branches, onSaved }: {
  open: boolean;
  onClose: () => void;
  roomType: RoomType | null;
  branches: Branch[];
  onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [form, setForm] = useState({
    branch_id: '', name: '', code: '', description: '', base_rate: '0', max_occupancy: '2', default_tax_rate: '0', is_active: true, sort_order: '0',
  });

  useEffect(() => {
    if (roomType) {
      setForm({
        branch_id: roomType.branch_id, name: roomType.name, code: roomType.code, description: roomType.description || '',
        base_rate: String(roomType.base_rate), max_occupancy: String(roomType.max_occupancy),
        default_tax_rate: String(roomType.default_tax_rate), is_active: roomType.is_active, sort_order: String(roomType.sort_order),
      });
    } else {
      setForm({ branch_id: branches[0]?.id || '', name: '', code: '', description: '', base_rate: '0', max_occupancy: '2', default_tax_rate: '0', is_active: true, sort_order: '0' });
    }
  }, [roomType, open, branches]);

  const handleSubmit = async () => {
    if (!form.branch_id || !form.name || !form.code) { showToast('Required fields missing', 'error'); return; }
    setSaving(true);
    const payload = {
      branch_id: form.branch_id, name: form.name, code: form.code.toUpperCase(), description: form.description || null,
      base_rate: parseFloat(form.base_rate) || 0, max_occupancy: parseInt(form.max_occupancy) || 2,
      default_tax_rate: parseFloat(form.default_tax_rate) || 0, is_active: form.is_active, sort_order: parseInt(form.sort_order) || 0,
    };
    const { error } = roomType
      ? await supabase.from('room_types').update(payload).eq('id', roomType.id)
      : await supabase.from('room_types').insert(payload);
    if (error) showToast(error.message, 'error');
    else { showToast('Saved', 'success'); onSaved(); }
    setSaving(false);
  };

  return (
    <Modal open={open} onClose={onClose} title={roomType ? t('common.edit') : t('common.add')} size="md"
      footer={<><Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button><Button loading={saving} onClick={handleSubmit}>{t('common.save')}</Button></>}>
      <form className="space-y-4">
        <Select label={t('common.branch')} value={form.branch_id} onChange={(e) => setForm({ ...form, branch_id: e.target.value })} required>
          <option value="">--</option>
          {branches.map((b) => <option key={b.id} value={b.id}>{b.name}</option>)}
        </Select>
        <div className="grid grid-cols-2 gap-4">
          <Input label={t('common.name')} value={form.name} onChange={(e) => setForm({ ...form, name: e.target.value })} required />
          <Input label="Code" value={form.code} onChange={(e) => setForm({ ...form, code: e.target.value })} required />
          <Input label={t('rooms.base_rate')} type="number" value={form.base_rate} onChange={(e) => setForm({ ...form, base_rate: e.target.value })} />
          <Input label={t('common.max_occupancy')} type="number" value={form.max_occupancy} onChange={(e) => setForm({ ...form, max_occupancy: e.target.value })} />
          <Input label="Tax Rate (%)" type="number" value={form.default_tax_rate} onChange={(e) => setForm({ ...form, default_tax_rate: e.target.value })} />
          <Input label="Sort Order" type="number" value={form.sort_order} onChange={(e) => setForm({ ...form, sort_order: e.target.value })} />
        </div>
        <Textarea label={t('common.description')} value={form.description} onChange={(e) => setForm({ ...form, description: e.target.value })} rows={2} />
      </form>
    </Modal>
  );
}
