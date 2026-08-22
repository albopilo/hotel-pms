import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal, ConfirmModal } from '@/components/ui/Modal';
import { Input, Textarea } from '@/components/ui/Form';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { Plus, CreditCard as Edit, Building2, Trash2 } from 'lucide-react';
import type { Branch } from '@/types/database';
import { saveDraft, loadDraft, clearDraft } from '@/lib/formDraft';

const BRANCH_DRAFT_KEY = 'branch_form_draft';

const initialBranchForm = {
  name: '', code: '', address: '', phone: '', email: '', tax_id: '',
  timezone: 'Asia/Jakarta', standard_checkin_time: '14:00', standard_checkout_time: '12:00',
  business_day_cutoff: '04:30', is_active: true,
};

export function BranchesPage() {
  const { user } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [branches, setBranches] = useState<Branch[]>([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [editing, setEditing] = useState<Branch | null>(null);
  const [deleteTarget, setDeleteTarget] = useState<Branch | null>(null);

  const load = useCallback(async () => {
    setLoading(true);
    const { data } = await supabase.from('branches').select('*').order('name');
    setBranches((data as Branch[]) || []);
    setLoading(false);
  }, []);

  useEffect(() => { load(); }, [load]);

  const handleDelete = async () => {
    if (!deleteTarget) return;
    const { error } = await supabase.from('branches').update({ is_active: false }).eq('id', deleteTarget.id);
    if (error) showToast(error.message, 'error');
    else { showToast('Branch deactivated', 'success'); load(); }
    setDeleteTarget(null);
  };

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-slate-900">{t('nav.branches')}</h1>
        <Button onClick={() => { setEditing(null); setShowForm(true); }}><Plus size={18} /> {t('common.add')}</Button>
      </div>

      {branches.length === 0 ? (
        <EmptyState icon={<Building2 size={48} />} title={t('common.no_data')} />
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          {branches.map((b) => (
            <Card key={b.id}>
              <div className="flex items-start justify-between">
                <div>
                  <h3 className="font-semibold text-slate-800">{b.name}</h3>
                  <p className="text-xs text-slate-400">{b.code}</p>
                </div>
                <div className="flex gap-1">
                  <button onClick={() => { setEditing(b); setShowForm(true); }} className="text-slate-400 hover:text-blue-600"><Edit size={16} /></button>
                  <button onClick={() => setDeleteTarget(b)} className="text-slate-400 hover:text-red-600"><Trash2 size={16} /></button>
                </div>
              </div>
              <div className="mt-3 space-y-1 text-sm text-slate-600">
                {b.address && <p>{b.address}</p>}
                {b.phone && <p>{b.phone}</p>}
                {b.email && <p>{b.email}</p>}
                {b.tax_id && <p className="text-xs text-slate-400">Tax ID: {b.tax_id}</p>}
                <div className="flex gap-3 pt-1 text-xs text-slate-500">
                  <span>CI: {b.standard_checkin_time}</span>
                  <span>CO: {b.standard_checkout_time}</span>
                  <span>Cutoff: {b.business_day_cutoff}</span>
                </div>
                <div className="pt-1"><Badge color={b.is_active ? 'green' : 'gray'}>{b.is_active ? t('common.active') : t('common.inactive')}</Badge></div>
              </div>
            </Card>
          ))}
        </div>
      )}

      <BranchFormModal open={showForm} onClose={() => setShowForm(false)} branch={editing} orgId={user!.organization_id} onSaved={() => { setShowForm(false); load(); }} />
      <ConfirmModal open={!!deleteTarget} onClose={() => setDeleteTarget(null)} onConfirm={handleDelete} title="Deactivate Branch" message={`Deactivate "${deleteTarget?.name}"? This will hide it from active operations.`} confirmLabel={t('common.delete')} variant="danger" />
    </div>
  );
}

function BranchFormModal({ open, onClose, branch, orgId, onSaved }: {
  open: boolean; onClose: () => void; branch: Branch | null; orgId: string; onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [form, setForm] = useState(() => {
    const draft = loadDraft<typeof initialBranchForm>(BRANCH_DRAFT_KEY);
    return draft || { ...initialBranchForm };
  });

  useEffect(() => {
    if (branch) {
      setForm({
        name: branch.name, code: branch.code, address: branch.address || '', phone: branch.phone || '',
        email: branch.email || '', tax_id: branch.tax_id || '', timezone: branch.timezone,
        standard_checkin_time: branch.standard_checkin_time, standard_checkout_time: branch.standard_checkout_time,
        business_day_cutoff: branch.business_day_cutoff, is_active: branch.is_active,
      });
    } else {
      const draft = loadDraft<typeof initialBranchForm>(BRANCH_DRAFT_KEY);
      setForm(draft || { ...initialBranchForm });
    }
  }, [branch, open]);

  useEffect(() => {
    if (open && !branch) saveDraft(BRANCH_DRAFT_KEY, form);
  }, [form, open, branch]);

  const handleSubmit = async () => {
    if (!form.name || !form.code) { showToast('Name and code required', 'error'); return; }
    setSaving(true);
    const payload = {
      organization_id: orgId, name: form.name, code: form.code.toUpperCase(),
      address: form.address || null, phone: form.phone || null, email: form.email || null,
      tax_id: form.tax_id || null, timezone: form.timezone,
      standard_checkin_time: form.standard_checkin_time, standard_checkout_time: form.standard_checkout_time,
      business_day_cutoff: form.business_day_cutoff, is_active: form.is_active,
    };
    const { error } = branch
      ? await supabase.from('branches').update(payload).eq('id', branch.id)
      : await supabase.from('branches').insert(payload);
    if (error) showToast(error.message, 'error');
    else { showToast('Saved', 'success'); clearDraft(BRANCH_DRAFT_KEY); onSaved(); }
    setSaving(false);
  };

  const handleCancel = () => { clearDraft(BRANCH_DRAFT_KEY); onClose(); };

  return (
    <Modal open={open} onClose={handleCancel} title={branch ? t('common.edit') : t('common.add')} size="md"
      footer={<><Button variant="secondary" onClick={handleCancel}>{t('common.cancel')}</Button><Button loading={saving} onClick={handleSubmit}>{t('common.save')}</Button></>}>
      <form className="space-y-4">
        <div className="grid grid-cols-2 gap-4">
          <Input label={t('common.name')} value={form.name} onChange={(e) => setForm({ ...form, name: e.target.value })} required />
          <Input label="Code" value={form.code} onChange={(e) => setForm({ ...form, code: e.target.value })} required />
        </div>
        <Textarea label={t('common.address')} value={form.address} onChange={(e) => setForm({ ...form, address: e.target.value })} rows={2} />
        <div className="grid grid-cols-2 gap-4">
          <Input label={t('common.phone')} value={form.phone} onChange={(e) => setForm({ ...form, phone: e.target.value })} />
          <Input label={t('common.email')} value={form.email} onChange={(e) => setForm({ ...form, email: e.target.value })} />
        </div>
        <Input label="Tax ID" value={form.tax_id} onChange={(e) => setForm({ ...form, tax_id: e.target.value })} />
        <div className="grid grid-cols-3 gap-4">
          <Input label={t('settings.standard_checkin')} type="time" value={form.standard_checkin_time} onChange={(e) => setForm({ ...form, standard_checkin_time: e.target.value })} />
          <Input label={t('settings.standard_checkout')} type="time" value={form.standard_checkout_time} onChange={(e) => setForm({ ...form, standard_checkout_time: e.target.value })} />
          <Input label={t('settings.business_day_cutoff')} type="time" value={form.business_day_cutoff} onChange={(e) => setForm({ ...form, business_day_cutoff: e.target.value })} />
        </div>
      </form>
    </Modal>
  );
}
