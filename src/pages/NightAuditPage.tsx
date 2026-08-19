import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { ConfirmModal } from '@/components/ui/Modal';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatIDR, formatDate, todayISO } from '@/lib/format';
import { Moon, AlertCircle, CheckCircle2, Lock } from 'lucide-react';
import type { Reservation, Folio, HotelBusinessDate, NightAudit, Branch } from '@/types/database';

export function NightAuditPage() {
  const { user, branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [loading, setLoading] = useState(true);
  const [businessDate, setBusinessDate] = useState<HotelBusinessDate | null>(null);
  const [reservations, setReservations] = useState<Reservation[]>([]);
  const [folios, setFolios] = useState<Folio[]>([]);
  const [folioItems, setFolioItems] = useState<any[]>([]);
  const [payments, setPayments] = useState<any[]>([]);
  const [rooms, setRooms] = useState<any[]>([]);
  const [exceptions, setExceptions] = useState<string[]>([]);
  const [closing, setClosing] = useState(false);
  const [showCloseConfirm, setShowCloseConfirm] = useState(false);

  const branchId = selectedBranchId || branches[0]?.id;

  const load = useCallback(async () => {
    if (!branchId) { setLoading(false); return; }
    setLoading(true);
    const today = todayISO();

    // Get current business date
    const { data: bd } = await supabase.from('hotel_business_dates').select('*').eq('branch_id', branchId).order('business_date', { ascending: false }).limit(1).maybeSingle();
    const currentBD = bd as HotelBusinessDate | null;
    setBusinessDate(currentBD);
    const auditDate = currentBD?.business_date || today;

    const [{ data: res }, { data: fol }, { data: fi }, { data: pays }, { data: rms }] = await Promise.all([
      supabase.from('reservations').select('*').eq('branch_id', branchId),
      supabase.from('folios').select('*').eq('branch_id', branchId),
      supabase.from('folio_items').select('*').eq('branch_id', branchId).eq('voided', false).eq('business_date', auditDate),
      supabase.from('payments').select('*').eq('branch_id', branchId).eq('voided', false).eq('business_date', auditDate),
      supabase.from('rooms').select('*').eq('branch_id', branchId).eq('is_active', true),
    ]);
    setReservations((res as Reservation[]) || []);
    setFolios((fol as Folio[]) || []);
    setFolioItems(fi || []);
    setPayments(pays || []);
    setRooms(rms || []);

    // Detect exceptions
    const exc: string[] = [];
    const unpaidFolios = (fol as Folio[] || []).filter((f) => f.status === 'open' && f.balance > 0);
    if (unpaidFolios.length > 0) exc.push(`${unpaidFolios.length} ${t('night_audit.unpaid_folios')}`);
    const occupiedPastCheckout = (res as Reservation[] || []).filter((r) => r.status === 'checked_in' && r.check_out_date < today);
    if (occupiedPastCheckout.length > 0) exc.push(`${occupiedPastCheckout.length} ${t('night_audit.room_occupied_past_checkout')}`);
    const dirtyRooms = (rms || []).filter((r) => r.status === 'dirty').length;
    if (dirtyRooms > 5) exc.push(`${dirtyRooms} dirty rooms need cleaning`);
    setExceptions(exc);
    setLoading(false);
  }, [branchId, t]);

  useEffect(() => { load(); }, [load]);

  const arrivals = reservations.filter((r) => r.check_in_date === (businessDate?.business_date || todayISO()) && r.status === 'confirmed');
  const departures = reservations.filter((r) => r.check_out_date === (businessDate?.business_date || todayISO()) && r.status === 'checked_in');
  const inHouse = reservations.filter((r) => r.status === 'checked_in');
  const checkedInToday = reservations.filter((r) => r.actual_check_in && r.actual_check_in.startsWith(businessDate?.business_date || todayISO()));
  const checkedOutToday = reservations.filter((r) => r.actual_check_out && r.actual_check_out.startsWith(businessDate?.business_date || todayISO()));
  const noShows = reservations.filter((r) => r.status === 'no_show');
  const cancellations = reservations.filter((r) => r.status === 'cancelled');
  const openFolios = folios.filter((f) => f.status === 'open');
  const unpaidFolios = folios.filter((f) => f.status === 'open' && f.balance > 0);

  const roomCharges = folioItems.filter((i) => i.category === 'room' && i.amount > 0).reduce((s, i) => s + i.amount, 0);
  const additionalCharges = folioItems.filter((i) => i.item_type === 'charge' && i.category !== 'room' && i.amount > 0).reduce((s, i) => s + i.amount, 0);
  const cashPayments = payments.filter((p) => p.payment_method_code === 'cash').reduce((s, p) => s + p.amount, 0);
  const edcPayments = payments.filter((p) => p.payment_method_code === 'edc').reduce((s, p) => s + p.amount, 0);
  const otaPayments = payments.filter((p) => p.is_ota).reduce((s, p) => s + p.amount, 0);
  const totalPayments = payments.reduce((s, p) => s + p.amount, 0);
  const discounts = folioItems.filter((i) => i.item_type === 'discount').reduce((s, i) => s + Math.abs(i.amount), 0);
  const outstanding = unpaidFolios.reduce((s, f) => s + f.balance, 0);

  const handleClose = async () => {
    setClosing(true);
    const bd = businessDate?.business_date || todayISO();
    const summary = { roomCharges, additionalCharges, totalPayments, cashPayments, edcPayments, otaPayments, discounts, outstanding };

    // Insert night audit record
    const { error } = await supabase.from('night_audits').insert({
      branch_id: branchId!, business_date: bd, summary, exceptions,
      arrivals: arrivals.length, departures: departures.length, in_house: inHouse.length,
      checked_in: checkedInToday.length, checked_out: checkedOutToday.length,
      no_shows: noShows.length, cancellations: cancellations.length,
      room_charges: roomCharges, additional_charges: additionalCharges, payments: totalPayments,
      cash: cashPayments, edc: edcPayments, ota: otaPayments, discounts, outstanding,
      closed_by: user!.id, closed_at: new Date().toISOString(),
    });
    if (error) { showToast(error.message, 'error'); setClosing(false); return; }

    // Close current business date
    if (businessDate) {
      await supabase.from('hotel_business_dates').update({ status: 'closed', closed_at: new Date().toISOString(), closed_by: user!.id }).eq('id', businessDate.id);
    }

    // Open next business date
    const nextDate = new Date(bd);
    nextDate.setDate(nextDate.getDate() + 1);
    await supabase.from('hotel_business_dates').insert({
      branch_id: branchId!, business_date: nextDate.toISOString().split('T')[0], status: 'open',
    });

    // Audit log
    await supabase.from('audit_logs').insert({
      organization_id: user!.organization_id, branch_id: branchId, user_id: user!.id,
      action: 'business_day_closed', object_type: 'night_audit',
      new_value: { business_date: bd, summary },
    });

    showToast('Business day closed successfully', 'success');
    setClosing(false);
    setShowCloseConfirm(false);
    load();
  };

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-slate-900">{t('night_audit.title')}</h1>
        <Button variant="danger" onClick={() => setShowCloseConfirm(true)} disabled={closing}><Lock size={16} /> {t('night_audit.close_business_day')}</Button>
      </div>

      <Card title={t('night_audit.business_date')}>
        <div className="flex items-center gap-4">
          <div className="text-3xl font-bold text-slate-900">{formatDate(businessDate?.business_date || todayISO())}</div>
          <Badge color={businessDate?.status === 'open' ? 'green' : 'gray'}>{businessDate?.status || 'open'}</Badge>
        </div>
      </Card>

      {exceptions.length > 0 && (
        <Card title={t('night_audit.exceptions')}>
          <div className="space-y-2">
            {exceptions.map((exc, i) => (
              <div key={i} className="flex items-center gap-2 bg-amber-50 border border-amber-200 rounded-lg px-3 py-2 text-amber-700">
                <AlertCircle size={16} /> <span className="text-sm font-medium">{exc}</span>
              </div>
            ))}
          </div>
        </Card>
      )}
      {exceptions.length === 0 && (
        <Card><div className="flex items-center gap-2 text-emerald-600"><CheckCircle2 size={18} /> <span className="font-medium">{t('night_audit.no_exceptions')}</span></div></Card>
      )}

      <div className="grid grid-cols-2 md:grid-cols-4 gap-3">
        <Card><div className="text-center"><p className="text-sm text-slate-500">{t('dash.arrivals_today')}</p><p className="text-2xl font-bold text-blue-600">{arrivals.length}</p></div></Card>
        <Card><div className="text-center"><p className="text-sm text-slate-500">{t('dash.departures_today')}</p><p className="text-2xl font-bold text-amber-600">{departures.length}</p></div></Card>
        <Card><div className="text-center"><p className="text-sm text-slate-500">{t('dash.in_house_guests')}</p><p className="text-2xl font-bold text-emerald-600">{inHouse.length}</p></div></Card>
        <Card><div className="text-center"><p className="text-sm text-slate-500">No Shows</p><p className="text-2xl font-bold text-red-600">{noShows.length}</p></div></Card>
      </div>

      <Card title="Financial Summary">
        <div className="grid grid-cols-2 md:grid-cols-4 gap-3 text-sm">
          <div className="bg-slate-50 rounded-lg p-3"><p className="text-slate-500">{t('checkout.room_charges')}</p><p className="font-bold">{formatIDR(roomCharges)}</p></div>
          <div className="bg-slate-50 rounded-lg p-3"><p className="text-slate-500">{t('checkout.additional_charges')}</p><p className="font-bold">{formatIDR(additionalCharges)}</p></div>
          <div className="bg-slate-50 rounded-lg p-3"><p className="text-slate-500">{t('common.discount')}</p><p className="font-bold text-red-600">{formatIDR(discounts)}</p></div>
          <div className="bg-slate-50 rounded-lg p-3"><p className="text-slate-500">{t('common.outstanding')}</p><p className="font-bold text-red-600">{formatIDR(outstanding)}</p></div>
          <div className="bg-emerald-50 rounded-lg p-3"><p className="text-slate-500">{t('payment.cash')}</p><p className="font-bold text-emerald-700">{formatIDR(cashPayments)}</p></div>
          <div className="bg-emerald-50 rounded-lg p-3"><p className="text-slate-500">EDC</p><p className="font-bold text-emerald-700">{formatIDR(edcPayments)}</p></div>
          <div className="bg-emerald-50 rounded-lg p-3"><p className="text-slate-500">OTA</p><p className="font-bold text-emerald-700">{formatIDR(otaPayments)}</p></div>
          <div className="bg-emerald-50 rounded-lg p-3"><p className="text-slate-500">{t('common.total')}</p><p className="font-bold text-emerald-700">{formatIDR(totalPayments)}</p></div>
        </div>
      </Card>

      <Card title="Folios">
        <div className="grid grid-cols-2 gap-3 text-sm">
          <div className="bg-blue-50 rounded-lg p-3"><p className="text-slate-500">Open Folios</p><p className="font-bold text-blue-700">{openFolios.length}</p></div>
          <div className="bg-red-50 rounded-lg p-3"><p className="text-slate-500">Unpaid Folios</p><p className="font-bold text-red-700">{unpaidFolios.length}</p></div>
        </div>
      </Card>

      <ConfirmModal
        open={showCloseConfirm}
        onClose={() => setShowCloseConfirm(false)}
        onConfirm={handleClose}
        title={t('night_audit.close_business_day')}
        message={t('night_audit.close_confirm')}
        confirmLabel={t('night_audit.close_business_day')}
        variant="danger"
      />
    </div>
  );
}
