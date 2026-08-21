import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal, ConfirmModal } from '@/components/ui/Modal';
import { Input } from '@/components/ui/Form';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { Plus, Edit, Tags, Trash2 } from 'lucide-react';
import { formatIDR } from '@/lib/format';
import type { ChargeCategory } from '@/types/database';

export function ChargeCategoriesPage() {
  const { user } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [cats, setCats] = useState<ChargeCategory[]>([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [editing, setEditing] = useState<ChargeCategory | null>(null);
  const [deleteTarget, setDeleteTarget] = useState<ChargeCategory | null>(null);

  const load = useCallback(async () => {
    setLoading(true);
    const { data } = await supabase.from('charge_categories').select('*').order('sort_order');
    setCats((data as ChargeCategory[]) || []);
    setLoading(false);
  }, []);

  useEffect(() => { load(); }, [load]);

  const handleDelete = async () => {
    if (!deleteTarget) return;
    const { error } = await supabase.from('charge_categories').delete().eq('id', deleteTarget.id);
    if (error) showToast(error.message, 'error');
    else { showToast('Deleted', 'success'); load(); }
    setDeleteTarget(null);
  };

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-slate-900">{t('nav.charge_categories')}</h1>
        <Button onClick={() => { setEditing(null); setShowForm(true); }}><Plus size={18} /> {t('common.add')}</Button>
      </div>

      {cats.length === 0 ? (
        <EmptyState icon={<Tags size={48} />} title={t('common.no_data')} />
      ) : (
        <Card noPadding>
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 text-slate-500">
                  <th className="text-left py-3 px-4">{t('common.name')}</th>
                  <th className="text-left py-3 px-4">Code</th>
                  <th className="text-center py-3 px-4">Damage</th>
                  <th className="text-center py-3 px-4">Approval</th>
                  <th className="text-right py-3 px-4">Threshold</th>
                  <th className="text-center py-3 px-4">{t('common.status')}</th>
                  <th className="text-right py-3 px-4">{t('common.actions')}</th>
                </tr>
              </thead>
              <tbody>
                {cats.map((c) => (
                  <tr key={c.id} className="border-b border-slate-100 hover:bg-slate-50">
                    <td className="py-3 px-4 font-medium text-slate-800">{c.name}</td>
                    <td className="py-3 px-4 text-slate-500">{c.code}</td>
                    <td className="text-center py-3 px-4">{c.is_damage ? <Badge color="red">Damage</Badge> : '-'}</td>
                    <td className="text-center py-3 px-4">{c.requires_approval ? <Badge color="amber">Required</Badge> : '-'}</td>
                    <td className="text-right py-3 px-4 text-slate-500">{c.approval_threshold > 0 ? formatIDR(c.approval_threshold) : '-'}</td>
                    <td className="text-center py-3 px-4"><Badge color={c.is_active ? 'green' : 'gray'}>{c.is_active ? t('common.active') : t('common.inactive')}</Badge></td>
                    <td className="text-right py-3 px-4">
                      <div className="flex justify-end gap-2">
                        <button onClick={() => { setEditing(c); setShowForm(true); }} className="text-slate-400 hover:text-blue-600"><Edit size={16} /></button>
                        <button onClick={() => setDeleteTarget(c)} className="text-slate-400 hover:text-red-600"><Trash2 size={16} /></button>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </Card>
      )}

      <CatFormModal open={showForm} onClose={() => setShowForm(false)} cat={editing} orgId={user!.organization_id} onSaved={() => { setShowForm(false); load(); }} />
      <ConfirmModal open={!!deleteTarget} onClose={() => setDeleteTarget(null)} onConfirm={handleDelete} title="Delete Category" message={`Delete "${deleteTarget?.name}"?`} confirmLabel={t('common.delete')} variant="danger" />
    </div>
  );
}

function CatFormModal({ open, onClose, cat, orgId, onSaved }: {
  open: boolean; onClose: () => void; cat: ChargeCategory | null; orgId: string; onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [form, setForm] = useState({ name: '', code: '', is_damage: false, requires_approval: false, approval_threshold: '0', is_active: true, sort_order: '0' });

  useEffect(() => {
    if (cat) {
      setForm({ name: cat.name, code: cat.code, is_damage: cat.is_damage, requires_approval: cat.requires_approval, approval_threshold: String(cat.approval_threshold), is_active: cat.is_active, sort_order: String(cat.sort_order) });
    } else {
      setForm({ name: '', code: '', is_damage: false, requires_approval: false, approval_threshold: '0', is_active: true, sort_order: '0' });
    }
  }, [cat, open]);

  const handleSubmit = async () => {
    if (!form.name || !form.code) { showToast('Name and code required', 'error'); return; }
    setSaving(true);
    const payload = {
      organization_id: orgId, name: form.name, code: form.code.toUpperCase(),
      is_damage: form.is_damage, requires_approval: form.requires_approval,
      approval_threshold: parseFloat(form.approval_threshold) || 0, is_active: form.is_active,
      sort_order: parseInt(form.sort_order) || 0,
    };
    const { error } = cat
      ? await supabase.from('charge_categories').update(payload).eq('id', cat.id)
      : await supabase.from('charge_categories').insert(payload);
    if (error) showToast(error.message, 'error');
    else { showToast('Saved', 'success'); onSaved(); }
    setSaving(false);
  };

  return (
    <Modal open={open} onClose={onClose} title={cat ? t('common.edit') : t('common.add')} size="sm"
      footer={<><Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button><Button loading={saving} onClick={handleSubmit}>{t('common.save')}</Button></>}>
      <form className="space-y-4">
        <Input label={t('common.name')} value={form.name} onChange={(e) => setForm({ ...form, name: e.target.value })} required />
        <Input label="Code" value={form.code} onChange={(e) => setForm({ ...form, code: e.target.value })} required />
        <label className="flex items-center gap-2 text-sm text-slate-700">
          <input type="checkbox" checked={form.is_damage} onChange={(e) => setForm({ ...form, is_damage: e.target.checked })} />
          Damage Category
        </label>
        <label className="flex items-center gap-2 text-sm text-slate-700">
          <input type="checkbox" checked={form.requires_approval} onChange={(e) => setForm({ ...form, requires_approval: e.target.checked })} />
          Requires Manager Approval
        </label>
        <Input label={t('settings.damage_threshold')} type="number" value={form.approval_threshold} onChange={(e) => setForm({ ...form, approval_threshold: e.target.value })} />
        <Input label="Sort Order" type="number" value={form.sort_order} onChange={(e) => setForm({ ...form, sort_order: e.target.value })} />
      </form>
    </Modal>
  );
}
