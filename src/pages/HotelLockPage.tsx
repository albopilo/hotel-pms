import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatDateTime } from '@/lib/format';
import { getLockProvider, type LockEvent } from '@/lib/hotel-lock/provider';
import { KeyRound, Wifi, Usb, Activity, AlertCircle, CheckCircle2 } from 'lucide-react';
import type { HotelLockIntegration, HotelLockEvent, CardIssuance, Reservation, Guest, Room } from '@/types/database';

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

  const branchId = selectedBranchId || branches[0]?.id;

  const load = useCallback(async () => {
    if (!branchId) { setLoading(false); return; }
    setLoading(true);
    const [{ data: integ }, { data: evts }, { data: cards }] = await Promise.all([
      supabase.from('hotel_lock_integrations').select('*').eq('branch_id', branchId).maybeSingle(),
      supabase.from('hotel_lock_events').select('*').eq('branch_id', branchId).order('created_at', { ascending: false }).limit(20),
      supabase.from('card_issuances').select('*, reservation:reservations(*), guest:guests(*), room:rooms(*)').eq('branch_id', branchId).order('created_at', { ascending: false }).limit(10),
    ]);
    setIntegration(integ as HotelLockIntegration);
    setEvents((evts as HotelLockEvent[]) || []);
    setCardIssuances((cards as CardIssuance[]) || []);
    const provider = getLockProvider();
    const me = await provider.getLockEvents();
    setMockEvents(me);
    setLoading(false);
  }, [branchId]);

  useEffect(() => { load(); }, [load]);

  const handleTestConnection = async () => {
    setTesting(true);
    const provider = getLockProvider();
    const connected = await provider.connect();
    if (connected) {
      showToast('Mock bridge connected (DEVELOPMENT / MOCK MODE)', 'success');
      if (integration) {
        await supabase.from('hotel_lock_integrations').update({ connection_status: 'connected', last_heartbeat: new Date().toISOString() }).eq('id', integration.id);
      }
      await supabase.from('hotel_lock_events').insert({
        branch_id: branchId!, integration_id: integration?.id || null,
        event_type: 'test_connection', status: 'success', message: 'Test connection successful (mock)',
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

      {/* Details */}
      <Card title="Integration Details">
        <div className="grid grid-cols-2 md:grid-cols-3 gap-4 text-sm">
          <div><span className="text-slate-500">Lock System:</span> <span className="font-medium">{integration?.lock_system || 'ZKBiolock'}</span></div>
          <div><span className="text-slate-500">Lock Model:</span> <span className="font-medium">{integration?.lock_model || 'SOLUTION HL400'}</span></div>
          <div><span className="text-slate-500">Card Technology:</span> <span className="font-medium">{integration?.card_technology || 'MIFARE / ISO14443 Type-A'}</span></div>
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
