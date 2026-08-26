import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { Modal } from '@/components/ui/Modal';
import { Badge } from '@/components/ui/Badge';
import { Button } from '@/components/ui/Button';
import { formatIDR, formatDateTime, todayInTimezone, nowInTimezone } from '@/lib/format';
import { TriangleAlert as AlertTriangle, RefreshCw, Bell, BellRing } from 'lucide-react';
import type { Reservation, Guest, Room, Folio, FolioItem } from '@/types/database';

const OUTSTANDING_BALANCE_THRESHOLD_TIME = '17:00';
const STORAGE_KEY_DISMISSED = 'warning_modal_dismissed_until';

interface OverdueCheckout {
  reservation: Reservation;
  guest: Guest | null;
  room: Room | null;
}

interface OutstandingBalance {
  reservation: Reservation;
  guest: Guest | null;
  room: Room | null;
  folio: Folio | null;
  balance: number;
}

interface ScanResult {
  overdueCheckouts: OverdueCheckout[];
  outstandingBalances: OutstandingBalance[];
}

const DUMMY_ID = '00000000-0000-0000-0000-000000000000';

export function WarningModal() {
  const { branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();

  const [open, setOpen] = useState(false);
  const [scanning, setScanning] = useState(false);
  const [result, setResult] = useState<ScanResult>({ overdueCheckouts: [], outstandingBalances: [] });
  const [lastScan, setLastScan] = useState<string | null>(null);
  const [hasWarnings, setHasWarnings] = useState(false);

  const branchIds = selectedBranchId ? [selectedBranchId] : branches.map((b) => b.id);

  const scan = useCallback(async (): Promise<ScanResult> => {
    if (branchIds.length === 0) return { overdueCheckouts: [], outstandingBalances: [] };

    const today = todayInTimezone('Asia/Jakarta');
    const nowTime = nowInTimezone('Asia/Jakarta');

    const { data: reservationsData } = await supabase
      .from('reservations')
      .select('*')
      .in('branch_id', branchIds)
      .in('status', ['checked_in', 'confirmed', 'checked_out']);

    const reservations = (reservationsData as Reservation[]) || [];
    if (reservations.length === 0) return { overdueCheckouts: [], outstandingBalances: [] };

    const guestIds = Array.from(new Set(reservations.map((r) => r.primary_guest_id).filter(Boolean))) as string[];
    const { data: guestsData } = await supabase.from('guests').select('*').in('id', guestIds.length > 0 ? guestIds : [DUMMY_ID]);
    const guestMap = new Map<string, Guest>(((guestsData as Guest[]) || []).map((g) => [g.id, g]));

    const roomIds = Array.from(new Set(reservations.map((r) => r.room_id).filter(Boolean))) as string[];
    const { data: roomsData } = await supabase.from('rooms').select('*').in('id', roomIds.length > 0 ? roomIds : [DUMMY_ID]);
    const roomMap = new Map<string, Room>(((roomsData as Room[]) || []).map((r) => [r.id, r]));

    const resIds = reservations.map((r) => r.id);
    const { data: foliosData } = await supabase.from('folios').select('*').in('reservation_id', resIds).neq('status', 'void');
    const folioMap = new Map<string, Folio>(((foliosData as Folio[]) || []).map((f) => [f.reservation_id, f]));

    const folioIds = Array.from(new Set((foliosData as Folio[] || []).map((f) => f.id)));
    const { data: folioItemsData } = await supabase.from('folio_items').select('*').in('folio_id', folioIds.length > 0 ? folioIds : [DUMMY_ID]).eq('voided', false);
    const folioItems = (folioItemsData as FolioItem[]) || [];

    const lateChargeResMap = new Map<string, boolean>();
    const folioIdToResId = new Map<string, string>();
    (foliosData as Folio[] || []).forEach((f) => folioIdToResId.set(f.id, f.reservation_id));
    folioItems.forEach((item) => {
      if (item.category === 'late_checkout' && item.item_type === 'charge' && item.amount > 0) {
        const resId = folioIdToResId.get(item.folio_id);
        if (resId) lateChargeResMap.set(resId, true);
      }
    });

    const overdueCheckouts: OverdueCheckout[] = [];
    for (const res of reservations) {
      if (res.status !== 'checked_in' && res.status !== 'confirmed') continue;
      const coDate = res.check_out_date;
      const coTime = res.check_out_time || '12:00';
      let isOverdue = false;
      if (coDate < today) isOverdue = true;
      else if (coDate === today && coTime < nowTime) isOverdue = true;
      if (isOverdue && !lateChargeResMap.has(res.id)) {
        overdueCheckouts.push({
          reservation: res,
          guest: guestMap.get(res.primary_guest_id || '') || null,
          room: roomMap.get(res.room_id || '') || null,
        });
      }
    }

    const outstandingBalances: OutstandingBalance[] = [];
    const pastThreshold = nowTime >= OUTSTANDING_BALANCE_THRESHOLD_TIME;
    if (pastThreshold) {
      for (const res of reservations) {
        const folio = folioMap.get(res.id);
        if (folio && folio.balance > 0) {
          outstandingBalances.push({
            reservation: res,
            guest: guestMap.get(res.primary_guest_id || '') || null,
            room: roomMap.get(res.room_id || '') || null,
            folio,
            balance: folio.balance,
          });
        }
      }
    }

    return { overdueCheckouts, outstandingBalances };
  }, [branchIds]);

  const runScan = useCallback(async () => {
    setScanning(true);
    try {
      const scanResult = await scan();
      setResult(scanResult);
      const now = new Date().toISOString();
      setLastScan(now);
      const hasAny = scanResult.overdueCheckouts.length > 0 || scanResult.outstandingBalances.length > 0;
      setHasWarnings(hasAny);
      if (hasAny) {
        let dismissedUntil: string | null = null;
        try { dismissedUntil = localStorage.getItem(STORAGE_KEY_DISMISSED); } catch { /* ignore */ }
        if (!dismissedUntil || new Date(dismissedUntil) < new Date()) {
          setOpen(true);
        }
      } else {
        try { localStorage.removeItem(STORAGE_KEY_DISMISSED); } catch { /* ignore */ }
      }
    } catch { /* ignore */ }
    setScanning(false);
  }, [scan]);

  useEffect(() => {
    runScan();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [selectedBranchId]);

  const handleDismiss = () => {
    setOpen(false);
  };

  const totalWarnings = result.overdueCheckouts.length + result.outstandingBalances.length;

  return (
    <>
      <button
        onClick={() => setOpen(true)}
        className={`fixed bottom-6 right-6 z-40 w-14 h-14 rounded-full shadow-lg flex items-center justify-center transition-all hover:scale-110 ${
          hasWarnings ? 'bg-red-600 text-white animate-pulse' : 'bg-slate-700 text-white hover:bg-slate-800'
        }`}
        title={t('warning.tooltip')}
      >
        {hasWarnings ? <BellRing size={24} /> : <Bell size={24} />}
        {totalWarnings > 0 && (
          <span className="absolute -top-1 -right-1 bg-red-500 text-white text-xs font-bold rounded-full w-6 h-6 flex items-center justify-center border-2 border-white">
            {totalWarnings}
          </span>
        )}
      </button>

      <Modal
        open={open}
        onClose={handleDismiss}
        title={t('warning.title')}
        size="lg"
        footer={
          <>
            <Button variant="outline" size="sm" onClick={runScan} loading={scanning}>
              <RefreshCw size={14} /> {scanning ? t('warning.scanning') : t('warning.scan_now')}
            </Button>
            <Button variant="secondary" onClick={handleDismiss}>{t('warning.dismiss')}</Button>
          </>
        }
      >
        <div className="space-y-6">
          {lastScan && (
            <p className="text-xs text-slate-400">{t('warning.last_scan')}: {formatDateTime(lastScan)}</p>
          )}

          {totalWarnings === 0 ? (
            <div className="flex flex-col items-center justify-center py-8 gap-2">
              <div className="rounded-full bg-emerald-50 p-3">
                <AlertTriangle size={32} className="text-emerald-500" />
              </div>
              <p className="text-sm font-medium text-slate-700">{t('warning.no_warnings')}</p>
              <p className="text-xs text-slate-400">{t('warning.no_warnings_desc')}</p>
            </div>
          ) : (
            <>
              {result.overdueCheckouts.length > 0 && (
                <div>
                  <div className="flex items-center gap-2 mb-2">
                    <AlertTriangle size={18} className="text-amber-600" />
                    <h3 className="text-sm font-bold text-slate-800">{t('warning.overdue_checkouts')}</h3>
                    <Badge color="amber">{result.overdueCheckouts.length}</Badge>
                  </div>
                  <p className="text-xs text-slate-500 mb-3">{t('warning.overdue_checkouts_desc')}</p>
                  <div className="overflow-x-auto border border-slate-200 rounded-lg">
                    <table className="w-full text-sm">
                      <thead>
                        <tr className="border-b border-slate-200 bg-slate-50 text-slate-500">
                          <th className="text-left py-2 px-3">{t('warning.reservation')}</th>
                          <th className="text-left py-2 px-3">{t('warning.guest')}</th>
                          <th className="text-left py-2 px-3">{t('warning.room')}</th>
                          <th className="text-left py-2 px-3">{t('warning.checkout_time')}</th>
                        </tr>
                      </thead>
                      <tbody>
                        {result.overdueCheckouts.map((oc) => (
                          <tr key={oc.reservation.id} className="border-b border-slate-100 hover:bg-slate-50">
                            <td className="py-2 px-3 font-medium text-blue-600">{oc.reservation.reservation_number}</td>
                            <td className="py-2 px-3">{oc.guest?.full_name || '-'}</td>
                            <td className="py-2 px-3">{oc.room?.room_number || '-'}</td>
                            <td className="py-2 px-3 text-slate-500">{oc.reservation.check_out_date} {oc.reservation.check_out_time || '12:00'}</td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </div>
                </div>
              )}

              {result.outstandingBalances.length > 0 && (
                <div>
                  <div className="flex items-center gap-2 mb-2">
                    <AlertTriangle size={18} className="text-red-600" />
                    <h3 className="text-sm font-bold text-slate-800">{t('warning.outstanding_balances')}</h3>
                    <Badge color="red">{result.outstandingBalances.length}</Badge>
                  </div>
                  <p className="text-xs text-slate-500 mb-3">{t('warning.outstanding_balances_desc')}</p>
                  <div className="overflow-x-auto border border-slate-200 rounded-lg">
                    <table className="w-full text-sm">
                      <thead>
                        <tr className="border-b border-slate-200 bg-slate-50 text-slate-500">
                          <th className="text-left py-2 px-3">{t('warning.reservation')}</th>
                          <th className="text-left py-2 px-3">{t('warning.guest')}</th>
                          <th className="text-left py-2 px-3">{t('warning.room')}</th>
                          <th className="text-right py-2 px-3">{t('warning.balance')}</th>
                        </tr>
                      </thead>
                      <tbody>
                        {result.outstandingBalances.map((ob) => (
                          <tr key={ob.reservation.id} className="border-b border-slate-100 hover:bg-slate-50">
                            <td className="py-2 px-3 font-medium text-blue-600">{ob.reservation.reservation_number}</td>
                            <td className="py-2 px-3">{ob.guest?.full_name || '-'}</td>
                            <td className="py-2 px-3">{ob.room?.room_number || '-'}</td>
                            <td className="text-right py-2 px-3 font-bold text-red-600">{formatIDR(ob.balance)}</td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </div>
                </div>
              )}
            </>
          )}
        </div>
      </Modal>
    </>
  );
}