import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Input, Textarea } from '@/components/ui/Form';
import { LoadingPage } from '@/components/ui/States';
import { Save, Settings } from 'lucide-react';
import type { Organization, SystemSetting } from '@/types/database';

const SETTING_KEYS = [
  { key: 'early_checkin_charge', label: 'settings.early_checkin_charge', type: 'number' },
  { key: 'late_checkout_charge', label: 'settings.late_checkout_charge', type: 'number' },
  { key: 'damage_approval_threshold', label: 'settings.damage_threshold', type: 'number' },
  { key: 'default_tax_rate', label: 'settings.default_tax', type: 'number' },
  { key: 'invoice_prefix', label: 'settings.invoice_prefix', type: 'string' },
  { key: 'reservation_prefix', label: 'settings.reservation_prefix', type: 'string' },
];

export function SystemSettingsPage() {
  const { user } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [org, setOrg] = useState<Organization | null>(null);
  const [settings, setSettings] = useState<SystemSetting[]>([]);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [orgForm, setOrgForm] = useState({ name: '', legal_name: '', address: '', phone: '', email: '', tax_id: '', currency: 'IDR', timezone: 'Asia/Jakarta' });
  const [settingValues, setSettingValues] = useState<Record<string, string>>({});

  const load = useCallback(async () => {
    setLoading(true);
    const [{ data: orgData }, { data: settingData }] = await Promise.all([
      supabase.from('organizations').select('*').eq('id', user!.organization_id).maybeSingle(),
      supabase.from('system_settings').select('*').eq('organization_id', user!.organization_id),
    ]);
    const o = orgData as Organization | null;
    setOrg(o);
    if (o) {
      setOrgForm({ name: o.name, legal_name: o.legal_name || '', address: o.address || '', phone: o.phone || '', email: o.email || '', tax_id: o.tax_id || '', currency: o.currency, timezone: o.timezone });
    }
    const s = (settingData as SystemSetting[]) || [];
    setSettings(s);
    const vals: Record<string, string> = {};
    s.forEach((s) => { vals[s.key] = s.value; });
    // Defaults
    SETTING_KEYS.forEach(({ key }) => {
      if (!vals[key]) vals[key] = '';
    });
    setSettingValues(vals);
    setLoading(false);
  }, [user]);

  useEffect(() => { load(); }, [load]);

  const handleSaveOrg = async () => {
    setSaving(true);
    const { error } = await supabase.from('organizations').update({
      name: orgForm.name, legal_name: orgForm.legal_name || null, address: orgForm.address || null,
      phone: orgForm.phone || null, email: orgForm.email || null, tax_id: orgForm.tax_id || null,
      currency: orgForm.currency, timezone: orgForm.timezone,
    }).eq('id', user!.organization_id);
    if (error) showToast(error.message, 'error');
    else showToast('Organization saved', 'success');
    setSaving(false);
  };

  const handleSaveSettings = async () => {
    setSaving(true);
    for (const { key } of SETTING_KEYS) {
      const existing = settings.find((s) => s.key === key);
      const value = settingValues[key] || '';
      if (existing) {
        await supabase.from('system_settings').update({ value }).eq('id', existing.id);
      } else {
        await supabase.from('system_settings').insert({
          organization_id: user!.organization_id, key, value, value_type: 'string', category: 'general',
        });
      }
    }
    showToast('Settings saved', 'success');
    setSaving(false);
  };

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-slate-900">{t('nav.system_settings')}</h1>

      <Card title="Organization">
        <div className="space-y-4">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <Input label={t('settings.company_name')} value={orgForm.name} onChange={(e) => setOrgForm({ ...orgForm, name: e.target.value })} />
            <Input label="Legal Name" value={orgForm.legal_name} onChange={(e) => setOrgForm({ ...orgForm, legal_name: e.target.value })} />
          </div>
          <Textarea label={t('common.address')} value={orgForm.address} onChange={(e) => setOrgForm({ ...orgForm, address: e.target.value })} rows={2} />
          <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
            <Input label={t('common.phone')} value={orgForm.phone} onChange={(e) => setOrgForm({ ...orgForm, phone: e.target.value })} />
            <Input label={t('common.email')} value={orgForm.email} onChange={(e) => setOrgForm({ ...orgForm, email: e.target.value })} />
            <Input label="Tax ID" value={orgForm.tax_id} onChange={(e) => setOrgForm({ ...orgForm, tax_id: e.target.value })} />
            <Input label="Currency" value={orgForm.currency} onChange={(e) => setOrgForm({ ...orgForm, currency: e.target.value })} />
          </div>
          <div className="flex justify-end">
            <Button loading={saving} onClick={handleSaveOrg}><Save size={16} /> {t('common.save')}</Button>
          </div>
        </div>
      </Card>

      <Card title="Operational Settings">
        <div className="space-y-4">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            {SETTING_KEYS.map(({ key, label, type }) => (
              <Input
                key={key}
                label={t(label)}
                type={type === 'number' ? 'number' : 'text'}
                value={settingValues[key] || ''}
                onChange={(e) => setSettingValues({ ...settingValues, [key]: e.target.value })}
              />
            ))}
          </div>
          <div className="flex justify-end">
            <Button loading={saving} onClick={handleSaveSettings}><Save size={16} /> {t('common.save')}</Button>
          </div>
        </div>
      </Card>
    </div>
  );
}
