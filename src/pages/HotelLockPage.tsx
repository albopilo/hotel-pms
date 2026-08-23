import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Badge } from '@/components/ui/Badge';
import { Input } from '@/components/ui/Form';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatDateTime } from '@/lib/format';
import { getLockProvider, integrationToConfig, type LockEvent } from '@/lib/hotel-lock/provider';
import { KeyRound, Wifi, Usb, Activity, AlertCircle, CheckCircle2, Save } from 'lucide-react';
import type { HotelLockIntegration, HotelLockEvent, CardIssuance } from '@/types/database';

const defaultConfigForm = {
  provider_type: 'mock' as 'mock' | 'production',
  bridge_url: '',
  bridge_token: '',
  encoder_port: '',
  dll_path: '',
  hotel_identifier: '',
  encoding_profile: 'default',
  auto_poll_enabled: false,
  is_enabled: true,
  lock_system: 'ZKBiolock',
  lock_model: 'SOLUTION HL400',
  card_technology: 'MIFARE / ISO14443 Type-A',
};

export function HotelLockPage() {
  const { user, branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [integration, setIntegration] = useState<HotelLockIntegration | null>(null);
  const [events, setEvents] = useState<HotelLockEvent[]>([]);
  const [cardIssuances, setCardIssuances] = useState<CardIssuance[]>([]);
  const [loading, setLoading] = useState(true);
  const [testing, setTesting] = useState(false);
  const [mockEvents, setMockEvents] = useState<LockEvent[]>([]);
  const [configForm, setConfigForm] = useState({ ...defaultConfigForm });
  const [saving, setSaving] = useState(false);

  const branchId = selectedBranchId || branches[0]?.id;
  const isSuperAdmin = user?.role === 'super_admin';

  const load = useCallback(async () => {
    if (!branchId) { setLoading(false); return; }
    setLoading(true);
    const [{ data: integ }, { data: evts }, { data: cards }] = await Promise.all([
      supabase.from('hotel_lock_integrations').select('*').eq('branch_id', branchId).maybeSingle(),
      supabase.from('hotel_lock_events').select('*').eq('branch_id', branchId).order('created_at', { ascending: false }).limit(20),
      supabase.from('card_issuances').select('*, reservation:reservations(*), guest:guests(*), room:rooms(*)').eq('branch_id', branchId).order('created_at', { ascending: false }).limit(10),
    ]);
    const integRow = integ as HotelLockIntegration | null;
    setIntegration(integRow);
    setEvents((evts as HotelLockEvent[]) || []);
    setCardIssuances((cards as CardIssuance[]) || []);

    if (integRow) {
      setConfigForm({
        provider_type: integRow.provider_type,
        bridge_url: integRow.bridge_url || '',
        bridge_token: integRow.bridge_token || '',
        encoder_port: integRow.encoder_port || '',
        dll_path: integRow.dll_path || '',
        hotel_identifier: integRow.hotel_identifier || '',
        encoding_profile: integRow.encoding_profile || 'default',
        auto_poll_enabled: integRow.auto_poll_enabled,
        is_enabled: integRow.is_enabled,
        lock_system: integRow.lock_system,
        lock_model: integRow.lock_model,
        card_technology: integRow.card_technology,
      });
    } else {
      setConfigForm({ ...defaultConfigForm });
    }

    const provider = getLockProvider();
    provider.configure(integrationToConfig(integRow));
    const me = await provider.getLockEvents();
    setMockEvents(me);
    setLoading(false);
  }, [branchId]);

  useEffect(() => { load(); }, [load]);

  const handleSaveConfig = async () => {
    if (!branchId) return;
    setSaving(true);
    const payload = {
      branch_id: branchId,
      provider_type: configForm.provider_type,
      bridge_url: configForm.bridge_url || null,
      bridge_token: configForm.bridge_token || null,
      encoder_port: configForm.encoder_port || null,
      dll_path: configForm.dll_path || null,
      hotel_identifier: configForm.hotel_identifier || null,
      encoding_profile: configForm.encoding_profile || null,
      auto_poll_enabled: configForm.auto_poll_enabled,
      is_enabled: configForm.is_enabled,
      lock_system: configForm.lock_system,
      lock_model: configForm.lock_model,
      card_technology: configForm.card_technology,
    };

    const { error } = integration
      ? await supabase.from('hotel_lock_integrations').update(payload).eq('id', integration.id)
      : await supabase.from('hotel_lock_integrations').insert(payload);

    if (error) {
      showToast(error.message, 'error');
    } else {
      showToast('Configuration saved', 'success');
      const provider = getLockProvider();
      provider.configure({
        bridgeUrl: payload.bridge_url,
        bridgeToken: payload.bridge_token,
        encoderPort: payload.encoder_port,
        dllPath: payload.dll_path,
        hotelIdentifier: payload.hotel_identifier,
        encodingProfile: payload.encoding_profile,
        autoPollEnabled: payload.auto_poll_enabled,
        providerType: payload.provider_type,
      });
      load();
    }
    setSaving(false);
  };

  const handleTestConnection = async () => {
    setTesting(true);
    const provider = getLockProvider();
    provider.configure(integrationToConfig(integration));
    const connected = await provider.connect();
    if (connected) {
      showToast(`Bridge connected (${integration?.provider_type || 'mock'} mode)`, 'success');
      if (integration) {
        await supabase.from('hotel_lock_integrations').update({ connection_status: 'connected', last_heartbeat: new Date().toISOString() }).eq('id', integration.id);
      }
      await supabase.from('hotel_lock_events').insert({
        branch_id: branchId!, integration_id: integration?.id || null,
        event_type: 'test_connection', status: 'success', message: `Test connection successful (${integration?.provider_type || 'mock'})`,
      });
    } else {
      showToast('Connection failed', 'error');
    }
    setTesting(false);
    load();
  };

  const handleTestEncoder = async () => {
    setTesting(true);
    const provider = getLockProvider();
    const status = await provider.readEncoderStatus();
    showToast(`Encoder: ${status.connected ? 'Connected' : 'Disconnected'} (${status.status})`, status.connected ? 'success' : 'warning');
    if (integration) {
      await supabase.from('hotel_lock_integrations').update({ encoder_status: status.connected ? 'connected' : 'disconnected' }).eq('id', integration.id);
    }
    await supabase.from('hotel_lock_events').insert({
      branch_id: branchId!, integration_id: integration?.id || null,
      event_type: 'test_encoder', status: status.connected ? 'success' : 'warning',
      message: `Encoder test: ${status.status}`,
    });
    setTesting(false);
    load();
  };

  const handleTestCard = async () => {
    setTesting(true);
    const provider = getLockProvider();
    const result = await provider.encodeGuestCard({
      roomId: 'test', roomNumber: 'TEST', guestName: 'Test Guest',
      validFrom: new Date().toISOString(), validUntil: new Date(Date.now() + 86400000).toISOString(),
    });
    showToast(result.message, result.success ? 'success' : 'error');
    await supabase.from('hotel_lock_events').insert({
      branch_id: branchId!, integration_id: integration?.id || null,
      event_type: 'test_card', status: result.success ? 'success' : 'error',
      message: result.message,
    });
    setTesting(false);
    load();
  };

  if (loading) return <LoadingPage message={t('common.loading')} />;

  const isMock = !integration || integration.provider_type === 'mock';

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-slate-900">{t('lock.title')}</h1>
        {isMock && <span className="bg-amber-100 text-amber-700 font-bold text-sm px-3 py-1.5 rounded-lg">{t('lock.mock_mode')}</span>}
        {!isMock && <span className="bg-emerald-100 text-emerald-700 font-bold text-sm px-3 py-1.5 rounded-lg">PRODUCTION MODE</span>}
      </div>

      {/* Status cards */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        <Card>
          <div className="flex items-center gap-3">
            <div className={`rounded-lg p-3 ${integration?.connection_status === 'connected' ? 'bg-emerald-50 text-emerald-600' : 'bg-red-50 text-red-600'}`}>
              <Wifi size={24} />
            </div>
            <div>
              <p className="text-sm text-slate-500">{t('lock.integration_status')}</p>
              <p className="font-bold text-slate-800">{integration?.connection_status === 'connected' ? t('lock.connected') : t('lock.disconnected')}</p>
            </div>
          </div>
        </Card>
        <Card>
          <div className="flex items-center gap-3">
            <div className={`rounded-lg p-3 ${integration?.encoder_status === 'connected' ? 'bg-emerald-50 text-emerald-600' : 'bg-red-50 text-red-600'}`}>
              <Usb size={24} />
            </div>
            <div>
              <p className="text-sm text-slate-500">{t('lock.encoder_status')}</p>
              <p className="font-bold text-slate-800">{integration?.encoder_status === 'connected' ? t('lock.connected') : t('lock.disconnected')}</p>
            </div>
          </div>
        </Card>
        <Card>
          <div className="flex items-center gap-3">
            <div className="rounded-lg p-3 bg-blue-50 text-blue-600"><Activity size={24} /></div>
            <div>
              <p className="text-sm text-slate-500">{t('lock.last_heartbeat')}</p>
              <p className="font-bold text-slate-800">{integration?.last_heartbeat ? formatDateTime(integration.last_heartbeat) : '-'}</p>
            </div>
          </div>
        </Card>
      </div>

      {/* Configuration */}
      <Card title="Integration Configuration">
        <div className="space-y-4">
          {isSuperAdmin ? (
            <>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div className="flex flex-col gap-1">
                  <label className="text-sm font-medium text-slate-700">Provider Type</label>
                  <select
                    value={configForm.provider_type}
                    onChange={(e) => setConfigForm({ ...configForm, provider_type: e.target.value as 'mock' | 'production' })}
                    className="rounded-lg border border-slate-300 px-3 py-2 text-sm bg-white outline-none focus:ring-2 focus:ring-blue-500"
                  >
                    <option value="mock">Mock (Development)</option>
                    <option value="production">Production</option>
                  </select>
                </div>
                <Input label="Lock System" value={configForm.lock_system} onChange={(e) => setConfigForm({ ...configForm, lock_system: e.target.value })} />
                <Input label="Lock Model" value={configForm.lock_model} onChange={(e) => setConfigForm({ ...configForm, lock_model: e.target.value })} />
                <Input label="Card Technology" value={configForm.card_technology} onChange={(e) => setConfigForm({ ...configForm, card_technology: e.target.value })} />
              </div>

              <div className="rounded-lg border border-slate-200 p-4 bg-slate-50 space-y-4">
                <p className="text-xs font-semibold text-slate-500 uppercase">Bridge & Encoder Settings</p>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                  <Input label="Bridge URL" value={configForm.bridge_url} onChange={(e) => setConfigForm({ ...configForm, bridge_url: e.target.value })} placeholder="http://localhost:8080" />
                  <Input label="Bridge Token" value={configForm.bridge_token} onChange={(e) => setConfigForm({ ...configForm, bridge_token: e.target.value })} placeholder="Authentication token" />
                  <Input label="Encoder COM Port" value={configForm.encoder_port} onChange={(e) => setConfigForm({ ...configForm, encoder_port: e.target.value })} placeholder="COM3 or /dev/ttyUSB0" />
                  <Input label="DLL Path" value={configForm.dll_path} onChange={(e) => setConfigForm({ ...configForm, dll_path: e.target.value })} placeholder="C:\LockSystem\encoder.dll" />
                  <Input label="Hotel Identifier" value={configForm.hotel_identifier} onChange={(e) => setConfigForm({ ...configForm, hotel_identifier: e.target.value })} placeholder="Vendor-issued hotel ID" />
                  <Input label="Encoding Profile" value={configForm.encoding_profile} onChange={(e) => setConfigForm({ ...configForm, encoding_profile: e.target.value })} placeholder="default" />
                </div>
                <div className="flex flex-wrap gap-6">
                  <label className="flex items-center gap-2 text-sm text-slate-700">
                    <input type="checkbox" checked={configForm.auto_poll_enabled} onChange={(e) => setConfigForm({ ...configForm, auto_poll_enabled: e.target.checked })} />
                    Auto Poll Encoder
                  </label>
                  <label className="flex items-center gap-2 text-sm text-slate-700">
                    <input type="checkbox" checked={configForm.is_enabled} onChange={(e) => setConfigForm({ ...configForm, is_enabled: e.target.checked })} />
                    Integration Enabled
                  </label>
                </div>
              </div>

              <div className="flex justify-end">
                <Button loading={saving} onClick={handleSaveConfig}><Save size={16} /> {t('common.save')}</Button>
              </div>
            </>
          ) : (
            <div className="grid grid-cols-2 md:grid-cols-3 gap-4 text-sm">
              <div><span className="text-slate-500">Lock System:</span> <span className="font-medium">{integration?.lock_system || 'ZKBiolock'}</span></div>
              <div><span className="text-slate-500">Lock Model:</span> <span className="font-medium">{integration?.lock_model || 'SOLUTION HL400'}</span></div>
              <div><span className="text-slate-500">Card Technology:</span> <span className="font-medium">{integration?.card_technology || 'MIFARE / ISO14443 Type-A'}</span></div>
              <div><span className="text-slate-500">Bridge URL:</span> <span className="font-medium">{integration?.bridge_url || '-'}</span></div>
              <div><span className="text-slate-500">Encoder Port:</span> <span className="font-medium">{integration?.encoder_port || '-'}</span></div>
              <div><span className="text-slate-500">Hotel ID:</span> <span className="font-medium">{integration?.hotel_identifier || '-'}</span></div>
              <div><span className="text-slate-500">Provider:</span> <span className="font-medium">{integration?.provider_type || 'mock'}</span></div>
              <div><span className="text-slate-500">Auto Poll:</span> <span className="font-medium">{integration?.auto_poll_enabled ? 'Yes' : 'No'}</span></div>
              <div><span className="text-slate-500">Enabled:</span> <span className="font-medium">{integration?.is_enabled ? 'Yes' : 'No'}</span></div>
            </div>
          )}
        </div>
      </Card>

      {/* Details */}
      <Card title="Integration Details">
        <div className="grid grid-cols-2 md:grid-cols-3 gap-4 text-sm">
          <div><span className="text-slate-500">{t('lock.last_success')}:</span> <span className="font-medium">{integration?.last_success_encoding ? formatDateTime(integration.last_success_encoding) : '-'}</span></div>
          <div><span className="text-slate-500">{t('lock.last_error')}:</span> <span className="font-medium text-red-600">{integration?.last_error || '-'}</span></div>
          <div><span className="text-slate-500">Provider:</span> <span className="font-medium">{integration?.provider_type || 'mock'}</span></div>
        </div>
      </Card>

      {/* Test buttons */}
      <Card title="Diagnostics">
        <div className="flex flex-wrap gap-3">
          <Button onClick={handleTestConnection} loading={testing}><Wifi size={16} /> {t('lock.test_connection')}</Button>
          <Button variant="outline" onClick={handleTestEncoder} loading={testing}><Usb size={16} /> {t('lock.test_encoder')}</Button>
          <Button variant="outline" onClick={handleTestCard} loading={testing}><KeyRound size={16} /> {t('lock.test_card')}</Button>
        </div>
      </Card>

      {/* Card issuances */}
      <Card title="Recent Card Issuances">
        {cardIssuances.length === 0 ? (
          <EmptyState title={t('common.no_data')} />
        ) : (
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 text-slate-500">
                  <th className="text-left py-2 px-3">Type</th>
                  <th className="text-left py-2 px-3">Guest</th>
                  <th className="text-left py-2 px-3">Room</th>
                  <th className="text-center py-2 px-3">Seq</th>
                  <th className="text-center py-2 px-3">Status</th>
                  <th className="text-left py-2 px-3">Date</th>
                </tr>
              </thead>
              <tbody>
                {cardIssuances.map((c) => (
                  <tr key={c.id} className="border-b border-slate-100">
                    <td className="py-2 px-3 capitalize">{c.issuance_type}</td>
                    <td className="py-2 px-3">{(c as any).guest?.full_name || '-'}</td>
                    <td className="py-2 px-3">{(c as any).room?.room_number || '-'}</td>
                    <td className="text-center py-2 px-3">#{c.card_sequence}</td>
                    <td className="text-center py-2 px-3">
                      <Badge color={c.status === 'success' ? 'green' : c.status === 'failed' ? 'red' : 'amber'}>{c.status}</Badge>
                    </td>
                    <td className="py-2 px-3 text-xs text-slate-400">{formatDateTime(c.created_at)}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </Card>

      {/* Integration logs */}
      <Card title="Integration Logs">
        <div className="space-y-2 max-h-64 overflow-y-auto">
          {events.length === 0 && mockEvents.length === 0 ? (
            <EmptyState title={t('common.no_data')} />
          ) : (
            <>
              {mockEvents.map((evt, i) => (
                <div key={`mock-${i}`} className="flex items-center gap-2 text-sm border border-slate-100 rounded-lg px-3 py-2">
                  {evt.status === 'success' && <CheckCircle2 size={14} className="text-emerald-500" />}
                  {evt.status === 'error' && <AlertCircle size={14} className="text-red-500" />}
                  {evt.status === 'info' && <Activity size={14} className="text-blue-500" />}
                  {evt.status === 'warning' && <AlertCircle size={14} className="text-amber-500" />}
                  <span className="text-slate-600 flex-1">{evt.message}</span>
                  <span className="text-xs text-slate-400">{formatDateTime(evt.timestamp)}</span>
                </div>
              ))}
              {events.map((evt) => (
                <div key={evt.id} className="flex items-center gap-2 text-sm border border-slate-100 rounded-lg px-3 py-2">
                  {evt.status === 'success' && <CheckCircle2 size={14} className="text-emerald-500" />}
                  {evt.status === 'error' && <AlertCircle size={14} className="text-red-500" />}
                  {evt.status === 'info' && <Activity size={14} className="text-blue-500" />}
                  {evt.status === 'warning' && <AlertCircle size={14} className="text-amber-500" />}
                  <span className="text-slate-600 flex-1">{evt.message}</span>
                  <span className="text-xs text-slate-400">{formatDateTime(evt.created_at)}</span>
                </div>
              ))}
            </>
          )}
        </div>
      </Card>
    </div>
  );
}
