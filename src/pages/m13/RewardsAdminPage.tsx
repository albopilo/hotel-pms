import { useState, useEffect, useCallback } from 'react';
import { useAuth } from '@/lib/auth';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal, ConfirmModal } from '@/components/ui/Modal';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { Input, Textarea } from '@/components/ui/Form';
import { loyaltyService } from '@/services/loyaltyService';
import { formatDate } from '@/lib/format';
import { Plus, Edit, Trash2, Gift, Star } from 'lucide-react';
import type { M13Reward } from '@/types/database';

export function RewardsAdminPage() {
  const { user } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [rewards, setRewards] = useState<M13Reward[]>([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [editing, setEditing] = useState<M13Reward | null>(null);
  const [deleteTarget, setDeleteTarget] = useState<M13Reward | null>(null);

  const load = useCallback(async () => {
    if (!user) return;
    setLoading(true);
    const data = await loyaltyService.getAllRewards(user.organization_id);
    setRewards(data);
    setLoading(false);
  }, [user]);

  useEffect(() => { load(); }, [load]);

  const handleDelete = async () => {
    if (!deleteTarget) return;
    const { error } = await loyaltyService.deleteReward(deleteTarget.id);
    if (error) {
      showToast(t('m13.reward_save_failed') + ': ' + error, 'error');
    } else {
      showToast(t('m13.reward_deleted'), 'success');
      load();
    }
    setDeleteTarget(null);
  };

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-slate-900">{t('m13.rewards_title')}</h1>
        <Button onClick={() => { setEditing(null); setShowForm(true); }}><Plus size={16} /> {t('m13.new_reward')}</Button>
      </div>

      {rewards.length === 0 ? (
        <EmptyState icon={<Gift size={48} />} title={t('m13.no_rewards')} />
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          {rewards.map((reward) => {
            const remaining = reward.total_redemption_limit - reward.total_redeemed;
            const today = new Date().toISOString().slice(0, 10);
            const expired = reward.redemption_deadline && reward.redemption_deadline < today;
            const full = remaining <= 0;
            return (
              <Card key={reward.id}>
                <div className="space-y-3">
                  <div className="flex items-start justify-between">
                    <div className="flex-1">
                      <h3 className="font-semibold text-slate-800">{reward.name}</h3>
                      {reward.description && <p className="text-slate-500 text-sm mt-1">{reward.description}</p>}
                    </div>
                    <div className="flex items-center gap-1 text-amber-600 font-bold ml-3">
                      <Star size={16} />
                      {reward.points_required}
                    </div>
                  </div>
                  <div className="flex items-center gap-3 text-xs text-slate-500">
                    {reward.redemption_deadline && <span>{t('m13.deadline')}: {formatDate(reward.redemption_deadline)}</span>}
                    <span>{t('m13.remaining')}: {remaining}/{reward.total_redemption_limit}</span>
                  </div>
                  <div className="flex items-center gap-2">
                    {expired && <Badge color="red">{t('m13.expired')}</Badge>}
                    {full && <Badge color="gray">{t('m13.fully_redeemed')}</Badge>}
                    {!expired && !full && reward.is_active && <Badge color="green">{t('common.active')}</Badge>}
                    {!reward.is_active && <Badge color="gray">{t('common.inactive')}</Badge>}
                  </div>
                  <div className="flex justify-end gap-2">
                    <Button size="sm" variant="outline" onClick={() => { setEditing(reward); setShowForm(true); }}><Edit size={14} /> {t('common.edit')}</Button>
                    <Button size="sm" variant="danger" onClick={() => setDeleteTarget(reward)}><Trash2 size={14} /> {t('common.delete')}</Button>
                  </div>
                </div>
              </Card>
            );
          })}
        </div>
      )}

      {showForm && (
        <RewardFormModal
          reward={editing}
          orgId={user!.organization_id}
          userId={user!.id}
          onClose={() => { setShowForm(false); setEditing(null); }}
          onSaved={() => { setShowForm(false); setEditing(null); load(); }}
        />
      )}

      <ConfirmModal
        open={!!deleteTarget}
        onClose={() => setDeleteTarget(null)}
        onConfirm={handleDelete}
        title={t('m13.delete_reward_confirm')}
        message={deleteTarget?.name || ''}
        confirmLabel={t('common.delete')}
        variant="danger"
      />
    </div>
  );
}

function RewardFormModal({ reward, orgId, userId, onClose, onSaved }: {
  reward: M13Reward | null;
  orgId: string;
  userId: string;
  onClose: () => void;
  onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [form, setForm] = useState({
    name: reward?.name || '',
    description: reward?.description || '',
    points_required: reward?.points_required?.toString() || '100',
    redemption_deadline: reward?.redemption_deadline || '',
    terms_conditions: reward?.terms_conditions || '',
    total_redemption_limit: reward?.total_redemption_limit?.toString() || '100',
    is_active: reward?.is_active ?? true,
  });

  const handleSave = async () => {
    if (!form.name.trim()) { showToast(t('m13.reward_save_failed'), 'error'); return; }
    setSaving(true);
    const payload = {
      organization_id: orgId,
      name: form.name,
      description: form.description || null,
      points_required: parseInt(form.points_required) || 100,
      redemption_deadline: form.redemption_deadline || null,
      terms_conditions: form.terms_conditions || null,
      total_redemption_limit: parseInt(form.total_redemption_limit) || 100,
      is_active: form.is_active,
      created_by: userId,
    };
    const { error } = reward
      ? await loyaltyService.updateReward(reward.id, payload)
      : await loyaltyService.createReward(payload);
    if (error) {
      showToast(t('m13.reward_save_failed') + ': ' + error, 'error');
    } else {
      showToast(reward ? t('m13.reward_updated') : t('m13.reward_created'), 'success');
      onSaved();
    }
    setSaving(false);
  };

  return (
    <Modal open onClose={onClose} title={reward ? t('m13.edit_reward') : t('m13.new_reward')} size="lg"
      footer={<><Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button><Button onClick={handleSave} loading={saving}>{t('common.save')}</Button></>}>
      <div className="space-y-4">
        <Input label={t('m13.reward_name')} value={form.name} onChange={(e) => setForm({ ...form, name: e.target.value })} />
        <Textarea label={t('m13.reward_description')} value={form.description} onChange={(e) => setForm({ ...form, description: e.target.value })} rows={2} />
        <div className="grid grid-cols-2 gap-4">
          <Input label={t('m13.points_required')} type="number" value={form.points_required} onChange={(e) => setForm({ ...form, points_required: e.target.value })} />
          <Input label={t('m13.redemption_deadline')} type="date" value={form.redemption_deadline} onChange={(e) => setForm({ ...form, redemption_deadline: e.target.value })} />
        </div>
        <Input label={t('m13.total_limit')} type="number" value={form.total_redemption_limit} onChange={(e) => setForm({ ...form, total_redemption_limit: e.target.value })} />
        <Textarea label={t('m13.terms_conditions')} value={form.terms_conditions} onChange={(e) => setForm({ ...form, terms_conditions: e.target.value })} rows={4} />
        <label className="flex items-center gap-2 text-sm text-slate-700">
          <input type="checkbox" checked={form.is_active} onChange={(e) => setForm({ ...form, is_active: e.target.checked })} className="rounded" />
          {t('common.active')}
        </label>
      </div>
    </Modal>
  );
}
