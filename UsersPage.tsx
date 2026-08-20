import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal, ConfirmModal } from '@/components/ui/Modal';
import { Input, Select } from '@/components/ui/Form';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { Plus, Edit, Users, Trash2 } from 'lucide-react';
import type { Profile, Branch, UserRole, UserBranchAccess } from '@/types/database';

export function UsersPage() {
  const { user } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [profiles, setProfiles] = useState<Profile[]>([]);
  const [branches, setBranches] = useState<Branch[]>([]);
  const [accessMap, setAccessMap] = useState<Record<string, string[]>>({});
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [editing, setEditing] = useState<Profile | null>(null);
  const [deleteTarget, setDeleteTarget] = useState<Profile | null>(null);

  const load = useCallback(async () => {
    setLoading(true);
    const [{ data: prof }, { data: brs }, { data: access }] = await Promise.all([
      supabase.from('profiles').select('*').order('full_name'),
      supabase.from('branches').select('*').eq('is_active', true).order('name'),
      supabase.from('user_branch_access').select('*'),
    ]);
    setProfiles((prof as Profile[]) || []);
    setBranches((brs as Branch[]) || []);
    const map: Record<string, string[]> = {};
    (access as UserBranchAccess[] || []).forEach((a) => {
      if (!map[a.user_id]) map[a.user_id] = [];
      map[a.user_id].push(a.branch_id);
    });
    setAccessMap(map);
    setLoading(false);
  }, []);

  useEffect(() => { load(); }, [load]);

  const handleDelete = async () => {
    if (!deleteTarget) return;
    const { error } = await supabase.from('profiles').update({ is_active: false }).eq('id', deleteTarget.id);
    if (error) showToast(error.message, 'error');
    else { showToast('User deactivated', 'success'); load(); }
    setDeleteTarget(null);
  };

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-slate-900">{t('nav.users')}</h1>
        <Button onClick={() => { setEditing(null); setShowForm(true); }}><Plus size={18} /> {t('common.add')}</Button>
      </div>

      {profiles.length === 0 ? (
        <EmptyState icon={<Users size={48} />} title={t('common.no_data')} />
      ) : (
        <Card noPadding>
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 text-slate-500">
                  <th className="text-left py-3 px-4">{t('common.name')}</th>
                  <th className="text-left py-3 px-4">{t('common.email')}</th>
                  <th className="text-left py-3 px-4">{t('common.role')}</th>
                  <th className="text-left py-3 px-4">{t('common.branch')}</th>
                  <th className="text-center py-3 px-4">{t('common.status')}</th>
                  <th className="text-right py-3 px-4">{t('common.actions')}</th>
                </tr>
              </thead>
              <tbody>
                {profiles.map((p) => (
                  <tr key={p.id} className="border-b border-slate-100 hover:bg-slate-50">
                    <td className="py-3 px-4 font-medium text-slate-800">{p.full_name}</td>
                    <td className="py-3 px-4 text-slate-500">{p.email}</td>
                    <td className="py-3 px-4"><Badge color={p.role === 'super_admin' ? 'red' : p.role === 'manager' ? 'blue' : 'gray'}>{p.role.replace('_', ' ')}</Badge></td>
                    <td className="py-3 px-4 text-slate-500">
                      {p.role === 'super_admin' ? t('common.all_branches') : (accessMap[p.id] || []).map((bid) => branches.find((b) => b.id === bid)?.name).filter(Boolean).join(', ') || '-'}
                    </td>
                    <td className="text-center py-3 px-4"><Badge color={p.is_active ? 'green' : 'gray'}>{p.is_active ? t('common.active') : t('common.inactive')}</Badge></td>
                    <td className="text-right py-3 px-4">
                      <div className="flex justify-end gap-2">
                        <button onClick={() => { setEditing(p); setShowForm(true); }} className="text-slate-400 hover:text-blue-600"><Edit size={16} /></button>
                        {p.id !== user?.id && <button onClick={() => setDeleteTarget(p)} className="text-slate-400 hover:text-red-600"><Trash2 size={16} /></button>}
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </Card>
      )}

      <UserFormModal open={showForm} onClose={() => setShowForm(false)} profile={editing} branches={branches} currentAccess={editing ? accessMap[editing.id] || [] : []} orgId={user!.organization_id} onSaved={() => { setShowForm(false); load(); }} />
      <ConfirmModal open={!!deleteTarget} onClose={() => setDeleteTarget(null)} onConfirm={handleDelete} title="Deactivate User" message={`Deactivate "${deleteTarget?.full_name}"?`} confirmLabel={t('common.delete')} variant="danger" />
    </div>
  );
}

function UserFormModal({ open, onClose, profile, branches, currentAccess, orgId, onSaved }: {
  open: boolean; onClose: () => void; profile: Profile | null; branches: Branch[]; currentAccess: string[]; orgId: string; onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [form, setForm] = useState({ full_name: '', email: '', role: 'receptionist' as UserRole, is_active: true, branchIds: [] as string[] });

  useEffect(() => {
    if (profile) {
      setForm({ full_name: profile.full_name, email: profile.email, role: profile.role, is_active: profile.is_active, branchIds: currentAccess });
    } else {
      setForm({ full_name: '', email: '', role: 'receptionist', is_active: true, branchIds: [] });
    }
  }, [profile, open, currentAccess]);

  const handleSubmit = async () => {
    if (!form.full_name || !form.email) { showToast('Name and email required', 'error'); return; }
    setSaving(true);
    const payload = {
      organization_id: orgId, full_name: form.full_name, email: form.email,
      role: form.role, is_active: form.is_active,
    };
    if (profile) {
      const { error } = await supabase.from('profiles').update(payload).eq('id', profile.id);
      if (error) { showToast(error.message, 'error'); setSaving(false); return; }
      // Update branch access
      await supabase.from('user_branch_access').delete().eq('user_id', profile.id);
      if (form.role !== 'super_admin' && form.branchIds.length > 0) {
        await supabase.from('user_branch_access').insert(form.branchIds.map((bid) => ({ user_id: profile.id, branch_id: bid })));
      }
      showToast('Saved', 'success');
    } else {
      showToast('Note: New users must be created in Supabase Auth first, then their profile can be edited here.', 'warning');
    }
    setSaving(false);
    onSaved();
  };

  return (
    <Modal open={open} onClose={onClose} title={profile ? t('common.edit') : t('common.add')} size="md"
      footer={<><Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button><Button loading={saving} onClick={handleSubmit}>{t('common.save')}</Button></>}>
      <form className="space-y-4">
        <Input label={t('common.name')} value={form.full_name} onChange={(e) => setForm({ ...form, full_name: e.target.value })} required />
        <Input label={t('common.email')} type="email" value={form.email} onChange={(e) => setForm({ ...form, email: e.target.value })} required disabled={!!profile} />
        <Select label={t('common.role')} value={form.role} onChange={(e) => setForm({ ...form, role: e.target.value as UserRole })}>
          <option value="receptionist">{t('auth.receptionist')}</option>
          <option value="manager">{t('auth.manager')}</option>
          <option value="super_admin">Super Admin</option>
        </Select>
        {form.role !== 'super_admin' && (
          <div>
            <label className="text-sm font-medium text-slate-700 mb-2 block">{t('common.branch')} Access</label>
            <div className="space-y-2 max-h-48 overflow-y-auto border border-slate-200 rounded-lg p-3">
              {branches.map((b) => (
                <label key={b.id} className="flex items-center gap-2 text-sm text-slate-700">
                  <input
                    type="checkbox"
                    checked={form.branchIds.includes(b.id)}
                    onChange={(e) => {
                      if (e.target.checked) setForm({ ...form, branchIds: [...form.branchIds, b.id] });
                      else setForm({ ...form, branchIds: form.branchIds.filter((id) => id !== b.id) });
                    }}
                  />
                  {b.name}
                </label>
              ))}
            </div>
          </div>
        )}
        <label className="flex items-center gap-2 text-sm text-slate-700">
          <input type="checkbox" checked={form.is_active} onChange={(e) => setForm({ ...form, is_active: e.target.checked })} />
          {t('common.active')}
        </label>
      </form>
    </Modal>
  );
}
