import { useState, useEffect, useCallback } from 'react';
import { useAuth } from '@/lib/auth';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { Input } from '@/components/ui/Form';
import { loyaltyService } from '@/services/loyaltyService';
import { formatIDR } from '@/lib/format';
import { Settings, Star, Save } from 'lucide-react';
import type { M13LoyaltySettings, ChargeCategory } from '@/types/database';
import { supabase } from '@/lib/supabase';

export function LoyaltySettingsPage() {
  const { user } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [settings, setSettings] = useState<M13LoyaltySettings[]>([]);
  const [categories, setCategories] = useState<ChargeCategory[]>([]);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);

  const load = useCallback(async () => {
    if (!user) return;
    setLoading(true);
    const [existing, cats] = await Promise.all([
      loyaltyService.getLoyaltySettings(user.organization_id),
      supabase.from('charge_categories').select('*').eq('organization_id', user.organization_id).order('code'),
    ]);
    setSettings(existing);
    setCategories((cats.data as ChargeCategory[]) || []);
    setLoading(false);
  }, [user]);

  useEffect(() => { load(); }, [load]);

  const getSetting = (code: string) => {
    const s = settings.find((x) => x.charge_category_code === code);
    if (s) return s;
    return {
      id: '',
      organization_id: user!.organization_id,
      charge_category_code: code,
      spending_threshold: 20000,
      points_awarded: 1,
      is_active: true,
      created_at: '',
      updated_at: '',
    } as M13LoyaltySettings;
  };

  const updateSetting = (code: string, field: keyof M13LoyaltySettings, value: string | number | boolean) => {
    const existing = settings.find((x) => x.charge_category_code === code);
    if (existing) {
      setSettings(settings.map((s) => s.charge_category_code === code ? { ...s, [field]: value } : s));
    } else {
      setSettings([...settings, {
        id: '',
        organization_id: user!.organization_id,
        charge_category_code: code,
        spending_threshold: 20000,
        points_awarded: 1,
        is_active: true,
        created_at: '',
        updated_at: '',
        [field]: value,
      } as M13LoyaltySettings]);
    }
  };

  const handleSave = async () => {
    setSaving(true);
    let hasError = false;
    for (const s of settings) {
      const { error } = await loyaltyService.upsertLoyaltySetting({
        organization_id: s.organization_id,
        charge_category_code: s.charge_category_code,
        spending_threshold: s.spending_threshold,
        points_awarded: s.points_awarded,
        is_active: s.is_active,
      });
      if (error) hasError = true;
    }
    if (hasError) {
      showToast(t('m13.settings_save_failed'), 'error');
    } else {
      showToast(t('m13.settings_saved'), 'success');
    }
    setSaving(false);
  };

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-slate-900">{t('m13.settings_title')}</h1>
          <p className="text-sm text-slate-500 mt-1">{t('m13.settings_desc')}</p>
        </div>
        <Button onClick={handleSave} loading={saving}><Save size={16} /> {t('common.save')}</Button>
      </div>

      {categories.length === 0 ? (
        <EmptyState icon={<Settings size={48} />} title={t('common.no_data')} />
      ) : (
        <Card noPadding>
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 text-slate-500">
                  <th className="text-left py-3 px-4">{t('m13.category')}</th>
                  <th className="text-left py-3 px-4">{t('m13.spending_threshold')}</th>
                  <th className="text-left py-3 px-4">{t('m13.points_awarded')}</th>
                  <th className="text-left py-3 px-4">{t('m13.earning_rate')}</th>
                  <th className="text-center py-3 px-4">{t('m13.earning_active')}</th>
                </tr>
              </thead>
              <tbody>
                {categories.map((cat) => {
                  const s = getSetting(cat.code);
                  return (
                    <tr key={cat.id} className="border-b border-slate-100">
                      <td className="py-3 px-4">
                        <p className="font-medium text-slate-800">{cat.name}</p>
                        <p className="text-xs text-slate-400">{cat.code}</p>
                      </td>
                      <td className="py-3 px-4">
                        <input
                          type="number"
                          value={s.spending_threshold}
                          onChange={(e) => updateSetting(cat.code, 'spending_threshold', parseInt(e.target.value) || 0)}
                          className="w-32 rounded-lg border border-slate-300 px-2 py-1.5 text-sm outline-none focus:ring-2 focus:ring-blue-500"
                        />
                      </td>
                      <td className="py-3 px-4">
                        <input
                          type="number"
                          value={s.points_awarded}
                          onChange={(e) => updateSetting(cat.code, 'points_awarded', parseInt(e.target.value) || 1)}
                          className="w-20 rounded-lg border border-slate-300 px-2 py-1.5 text-sm outline-none focus:ring-2 focus:ring-blue-500"
                        />
                      </td>
                      <td className="py-3 px-4 text-slate-600">
                        <span className="inline-flex items-center gap-1">
                          <Star size={12} className="text-amber-500" />
                          {s.points_awarded} {t('m13.points')} {t('m13.per_spend')} {formatIDR(s.spending_threshold)} {t('m13.spent')}
                        </span>
                      </td>
                      <td className="py-3 px-4 text-center">
                        <button
                          onClick={() => updateSetting(cat.code, 'is_active', !s.is_active)}
                          className={`relative inline-flex h-6 w-11 items-center rounded-full transition-colors ${s.is_active ? 'bg-emerald-500' : 'bg-slate-300'}`}
                        >
                          <span className={`inline-block h-4 w-4 transform rounded-full bg-white transition-transform ${s.is_active ? 'translate-x-6' : 'translate-x-1'}`} />
                        </button>
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        </Card>
      )}
    </div>
  );
}
