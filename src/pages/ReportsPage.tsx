import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Input, Select } from '@/components/ui/Form';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatIDR, formatDate, todayISO, addDays } from '@/lib/format';
import { FileSpreadsheet, Download } from 'lucide-react';

type ReportCategory = 'front_office' | 'financial' | 'management';

interface ReportDef {
  key: string;
  labelKey: string;
  category: ReportCategory;
}

const REPORTS: ReportDef[] = [
  { key: 'arrival_report', labelKey: 'reports.arrival_report', category: 'front_office' },
  { key: 'departure_report', labelKey: 'reports.departure_report', category: 'front_office' },
  { key: 'inhouse_guest_report', labelKey: 'reports.inhouse_guest_report', category: 'front_office' },
  { key: 'guest_detail_report', labelKey: 'reports.guest_detail_report', category: 'front_office' },
  { key: 'room_availability_report', labelKey: 'reports.room_availability_report', category: 'front_office' },
  { key: 'room_occupancy_report', labelKey: 'reports.room_occupancy_report', category: 'front_office' },
  { key: 'reservation_report', labelKey: 'reports.reservation_report', category: 'front_office' },
  { key: 'cancellation_report', labelKey: 'reports.cancellation_report', category: 'front_office' },
  { key: 'noshow_report', labelKey: 'reports.noshow_report', category: 'front_office' },
  { key: 'room_transfer_report', labelKey: 'reports.room_transfer_report', category: 'front_office' },
  { key: 'daily_income_report', labelKey: 'reports.daily_income_report', category: 'financial' },
  { key: 'monthly_income_report', labelKey: 'reports.monthly_income_report', category: 'financial' },
  { key: 'revenue_by_branch', labelKey: 'reports.revenue_by_branch', category: 'financial' },
  { key: 'revenue_by_room_type', labelKey: 'reports.revenue_by_room_type', category: 'financial' },
  { key: 'revenue_by_booking_source', labelKey: 'reports.revenue_by_booking_source', category: 'financial' },
  { key: 'revenue_by_payment_method', labelKey: 'reports.revenue_by_payment_method', category: 'financial' },
  { key: 'cash_report', labelKey: 'reports.cash_report', category: 'financial' },
  { key: 'edc_report', labelKey: 'reports.edc_report', category: 'financial' },
  { key: 'ota_report', labelKey: 'reports.ota_report', category: 'financial' },
  { key: 'outstanding_balance_report', labelKey: 'reports.outstanding_balance_report', category: 'financial' },
  { key: 'deposit_report', labelKey: 'reports.deposit_report', category: 'financial' },
  { key: 'refund_report', labelKey: 'reports.refund_report', category: 'financial' },
  { key: 'discount_report', labelKey: 'reports.discount_report', category: 'financial' },
  { key: 'additional_charge_report', labelKey: 'reports.additional_charge_report', category: 'financial' },
  { key: 'damage_charge_report', labelKey: 'reports.damage_charge_report', category: 'financial' },
  { key: 'tax_report', labelKey: 'reports.tax_report', category: 'financial' },
  { key: 'reconciliation_report', labelKey: 'reports.reconciliation_report', category: 'financial' },
  { key: 'occupancy_pct', labelKey: 'reports.occupancy_pct', category: 'management' },
  { key: 'adr', labelKey: 'reports.adr', category: 'management' },
  { key: 'revpar', labelKey: 'reports.revpar', category: 'management' },
  { key: 'room_nights_sold', labelKey: 'reports.room_nights_sold', category: 'management' },
  { key: 'available_room_nights', labelKey: 'reports.available_room_nights', category: 'management' },
  { key: 'avg_length_of_stay', labelKey: 'reports.avg_length_of_stay', category: 'management' },
  { key: 'booking_source_stats', labelKey: 'reports.booking_source_stats', category: 'management' },
  { key: 'guest_statistics', labelKey: 'reports.guest_statistics', category: 'management' },
];

export function ReportsPage() {
  const { user, branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();
  const [activeReport, setActiveReport] = useState<ReportDef | null>(null);
  const [dateFrom, setDateFrom] = useState(todayISO());
  const [dateTo, setDateTo] = useState(addDays(todayISO(), 30));
  const [reportData, setReportData] = useState<any[]>([]);
  const [reportSummary, setReportSummary] = useState<any>(null);
  const [loading, setLoading] = useState(false);

  const branchIds = selectedBranchId ? [selectedBranchId] : branches.map((b) => b.id);

  const runReport = useCallback(async (report: ReportDef) => {
    setLoading(true);
    setReportData([]);
    setReportSummary(null);

    if (report.category === 'front_office') {
      let query = supabase.from('reservations').select('*, primary_guest:guests(*), room:rooms(*), branch:branches(*)').in('branch_id', branchIds);
      if (report.key === 'arrival_report') query = query.eq('status', 'confirmed').gte('check_in_date', dateFrom).lte('check_in_date', dateTo);
      else if (report.key === 'departure_report') query = query.eq('status', 'checked_in').gte('check_out_date', dateFrom).lte('check_out_date', dateTo);
      else if (report.key === 'inhouse_guest_report') query = query.eq('status', 'checked_in');
      else if (report.key === 'guest_detail_report') query = query.gte('check_in_date', dateFrom).lte('check_out_date', dateTo);
      else if (report.key === 'reservation_report') query = query.gte('check_in_date', dateFrom).lte('check_in_date', dateTo);
      else if (report.key === 'cancellation_report') query = query.eq('status', 'cancelled').gte('check_in_date', dateFrom).lte('check_in_date', dateTo);
      else if (report.key === 'noshow_report') query = query.eq('status', 'no_show').gte('check_in_date', dateFrom).lte('check_in_date', dateTo);
      const { data } = await query.order('check_in_date');
      setReportData(data || []);
    } else if (report.key === 'room_availability_report' || report.key === 'room_occupancy_report') {
      const { data: rooms } = await supabase.from('rooms').select('*, room_type:room_types(*)').in('branch_id', branchIds).eq('is_active', true).order('room_number');
      setReportData(rooms || []);
      const occupied = (rooms || []).filter((r: any) => r.status === 'occupied').length;
      setReportSummary({ total: rooms?.length || 0, occupied, rate: rooms?.length ? Math.round((occupied / rooms.length) * 100) : 0 });
    } else if (report.key === 'room_transfer_report') {
      const { data } = await supabase.from('room_transfers').select('*, reservation:reservations(*), from_room:rooms!from_room_id(*), to_room:rooms!to_room_id(*)').gte('created_at', dateFrom).lte('created_at', dateTo + 'T23:59:59');
      setReportData(data || []);
    } else if (report.category === 'financial' || report.category === 'management') {
      const { data: items } = await supabase.from('folio_items').select('*, folio:folios(*)').in('branch_id', branchIds).eq('voided', false).gte('business_date', dateFrom).lte('business_date', dateTo);
      const { data: payments } = await supabase.from('payments').select('*').in('branch_id', branchIds).eq('voided', false).gte('business_date', dateFrom).lte('business_date', dateTo);
      const fi = (items || []) as any[];
      const pays = (payments || []) as any[];

      if (report.key === 'daily_income_report' || report.key === 'monthly_income_report') {
        const roomRev = fi.filter((i) => i.item_type === 'charge' && i.category === 'room').reduce((s, i) => s + i.amount, 0);
        const addCharges = fi.filter((i) => i.item_type === 'charge' && !['room', 'early_checkin', 'late_checkout', 'damage'].includes(i.category)).reduce((s, i) => s + i.amount, 0);
        const amenities = fi.filter((i) => i.item_type === 'charge' && i.category === 'amenity').reduce((s, i) => s + i.amount, 0);
        const earlyCharges = fi.filter((i) => i.item_type === 'charge' && i.category === 'early_checkin').reduce((s, i) => s + i.amount, 0);
        const lateCharges = fi.filter((i) => i.item_type === 'charge' && i.category === 'late_checkout').reduce((s, i) => s + i.amount, 0);
        const damageCharges = fi.filter((i) => i.item_type === 'charge' && i.category === 'damage').reduce((s, i) => s + i.amount, 0);
        const discounts = fi.filter((i) => i.item_type === 'discount').reduce((s, i) => s + Math.abs(i.amount), 0);
        const tax = fi.filter((i) => i.item_type === 'tax').reduce((s, i) => s + i.amount, 0);
        const refunds = fi.filter((i) => i.item_type === 'refund' || (i.item_type === 'charge' && i.amount < 0)).reduce((s, i) => s + Math.abs(i.amount), 0);
        const cash = pays.filter((p) => p.payment_method_code === 'cash').reduce((s, p) => s + p.amount, 0);
        const edcDebit = pays.filter((p) => p.payment_method_code === 'edc' && p.payment_subtype === 'debit').reduce((s, p) => s + p.amount, 0);
        const edcCredit = pays.filter((p) => p.payment_method_code === 'edc' && p.payment_subtype === 'credit').reduce((s, p) => s + p.amount, 0);
        const edcQris = pays.filter((p) => p.payment_method_code === 'edc' && p.payment_subtype === 'qris').reduce((s, p) => s + p.amount, 0);
        const ota = pays.filter((p) => p.is_ota).reduce((s, p) => s + p.amount, 0);
        const gross = roomRev + addCharges + amenities + earlyCharges + lateCharges + damageCharges + tax;
        const net = gross - discounts - refunds;
        const collected = cash + edcDebit + edcCredit + edcQris + ota;
        setReportSummary({ roomRev, addCharges, amenities, earlyCharges, lateCharges, damageCharges, discounts, tax, refunds, net, cash, edcDebit, edcCredit, edcQris, ota, gross, collected });
      } else if (report.key === 'cash_report') {
        const cash = pays.filter((p) => p.payment_method_code === 'cash');
        setReportData(cash);
        setReportSummary({ total: cash.reduce((s, p) => s + p.amount, 0) });
      } else if (report.key === 'edc_report') {
        const edc = pays.filter((p) => p.payment_method_code === 'edc');
        setReportData(edc);
        setReportSummary({ total: edc.reduce((s, p) => s + p.amount, 0) });
      } else if (report.key === 'ota_report') {
        const ota = pays.filter((p) => p.is_ota);
        setReportData(ota);
        const settled = ota.filter((p) => p.ota_settled).reduce((s, p) => s + p.amount, 0);
        const outstanding = ota.filter((p) => !p.ota_settled).reduce((s, p) => s + p.amount, 0);
        setReportSummary({ total: ota.reduce((s, p) => s + p.amount, 0), settled, outstanding });
      } else if (report.key === 'outstanding_balance_report') {
        const { data: folios } = await supabase.from('folios').select('*, guest:guests(*), branch:branches(*)').in('branch_id', branchIds).gt('balance', 0);
        setReportData(folios || []);
        setReportSummary({ total: (folios || []).reduce((s: number, f: any) => s + f.balance, 0) });
      } else if (report.key === 'revenue_by_branch' || report.key === 'revenue_by_room_type' || report.key === 'revenue_by_booking_source' || report.key === 'revenue_by_payment_method') {
        // Aggregate from folio_items
        const grouped = new Map<string, number>();
        for (const item of fi.filter((i) => i.item_type === 'charge' && i.amount > 0)) {
          let key = 'Unknown';
          if (report.key === 'revenue_by_branch') key = item.branch_id;
          else if (report.key === 'revenue_by_room_type') key = item.category || 'Unknown';
          else if (report.key === 'revenue_by_booking_source') key = item.category || 'Unknown';
          else if (report.key === 'revenue_by_payment_method') key = item.category || 'Unknown';
          grouped.set(key, (grouped.get(key) || 0) + item.amount);
        }
        setReportData(Array.from(grouped.entries()).map(([key, amount]) => ({ key, amount })));
      } else if (report.key === 'occupancy_pct' || report.key === 'adr' || report.key === 'revpar' || report.key === 'room_nights_sold' || report.key === 'available_room_nights') {
        const { data: rooms } = await supabase.from('rooms').select('*').in('branch_id', branchIds).eq('is_active', true);
        const totalRooms = rooms?.length || 0;
        const days = Math.ceil((new Date(dateTo).getTime() - new Date(dateFrom).getTime()) / (1000 * 60 * 60 * 24)) + 1;
        const roomNights = fi.filter((i) => i.category === 'room').reduce((s, i) => s + i.quantity, 0);
        const roomRev = fi.filter((i) => i.category === 'room').reduce((s, i) => s + i.amount, 0);
        const availableRoomNights = totalRooms * days;
        const occupancy = availableRoomNights > 0 ? Math.round((roomNights / availableRoomNights) * 100) : 0;
        const adr = roomNights > 0 ? Math.round(roomRev / roomNights) : 0;
        const revpar = availableRoomNights > 0 ? Math.round(roomRev / availableRoomNights) : 0;
        setReportSummary({ totalRooms, days, roomNights, availableRoomNights, occupancy, adr, revpar, roomRev });
      } else {
        // Generic financial: show folio items
        setReportData(fi.filter((i) => i.item_type === 'charge'));
      }
    }
    setLoading(false);
  }, [branchIds, dateFrom, dateTo]);

  useEffect(() => {
    if (activeReport) runReport(activeReport);
  }, [activeReport, runReport]);

  const exportCSV = () => {
    if (reportData.length === 0 && !reportSummary) return;
    let csv = '';
    if (reportSummary && !Array.isArray(reportSummary)) {
      csv = Object.entries(reportSummary).map(([k, v]) => `${k},${v}`).join('\n') + '\n\n';
    }
    if (reportData.length > 0) {
      const headers = Object.keys(reportData[0]).filter((k) => typeof reportData[0][k] !== 'object');
      csv += headers.join(',') + '\n';
      csv += reportData.map((row) => headers.map((h) => row[h] ?? '').join(',')).join('\n');
    }
    const blob = new Blob([csv], { type: 'text/csv' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url; a.download = `${activeReport?.key || 'report'}.csv`; a.click();
    URL.revokeObjectURL(url);
  };

  const frontOffice = REPORTS.filter((r) => r.category === 'front_office');
  const financial = REPORTS.filter((r) => r.category === 'financial');
  const management = REPORTS.filter((r) => r.category === 'management');

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-slate-900">{t('reports.title')}</h1>

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        {/* Report list */}
        <div className="space-y-4">
          <ReportGroup title={t('reports.front_office')} reports={frontOffice} activeKey={activeReport?.key} onSelect={setActiveReport} t={t} />
          <ReportGroup title={t('reports.financial')} reports={financial} activeKey={activeReport?.key} onSelect={setActiveReport} t={t} />
          <ReportGroup title={t('reports.management')} reports={management} activeKey={activeReport?.key} onSelect={setActiveReport} t={t} />
        </div>

        {/* Report content */}
        <div className="lg:col-span-2">
          {activeReport && (
            <Card title={t(activeReport.labelKey)} actions={<Button size="sm" variant="outline" onClick={exportCSV}><Download size={14} /> {t('reports.export_csv')}</Button>}>
              <div className="flex gap-3 mb-4">
                <Input label={t('common.from')} type="date" value={dateFrom} onChange={(e) => setDateFrom(e.target.value)} />
                <Input label={t('common.to')} type="date" value={dateTo} onChange={(e) => setDateTo(e.target.value)} />
              </div>

              {loading ? (
                <LoadingPage />
              ) : reportSummary && Object.keys(reportSummary).length > 0 ? (
                <div className="space-y-4">
                  <ReportSummary summary={reportSummary} reportKey={activeReport.key} />
                  {reportData.length > 0 && <ReportTable data={reportData} />}
                </div>
              ) : reportData.length > 0 ? (
                <ReportTable data={reportData} />
              ) : (
                <EmptyState icon={<FileSpreadsheet size={48} />} title={t('common.no_data')} />
              )}
            </Card>
          )}
          {!activeReport && (
            <Card><EmptyState icon={<FileSpreadsheet size={48} />} title={t('reports.title')} message="Select a report from the left" /></Card>
          )}
        </div>
      </div>
    </div>
  );
}

function ReportGroup({ title, reports, activeKey, onSelect, t }: { title: string; reports: ReportDef[]; activeKey?: string; onSelect: (r: ReportDef) => void; t: (k: string) => string }) {
  return (
    <div>
      <h3 className="text-xs font-semibold text-slate-500 uppercase tracking-wider mb-2">{title}</h3>
      <div className="space-y-1">
        {reports.map((r) => (
          <button key={r.key} onClick={() => onSelect(r)} className={`w-full text-left px-3 py-2 rounded-lg text-sm font-medium transition-colors ${activeKey === r.key ? 'bg-blue-600 text-white' : 'text-slate-600 hover:bg-slate-100'}`}>
            {t(r.labelKey)}
          </button>
        ))}
      </div>
    </div>
  );
}

function ReportSummary({ summary, reportKey }: { summary: any; reportKey: string }) {
  const entries = Object.entries(summary);
  return (
    <div className="grid grid-cols-2 md:grid-cols-3 gap-3">
      {entries.map(([key, value]) => (
        <div key={key} className="bg-slate-50 rounded-lg p-3">
          <p className="text-xs text-slate-500 capitalize">{key.replace(/_/g, ' ')}</p>
          <p className="text-lg font-bold text-slate-800">{typeof value === 'number' && value > 1000 ? formatIDR(value) : String(value)}</p>
        </div>
      ))}
    </div>
  );
}

function ReportTable({ data }: { data: any[] }) {
  if (data.length === 0) return null;
  const headers = Object.keys(data[0]).filter((k) => typeof data[0][k] !== 'object');
  return (
    <div className="overflow-x-auto border border-slate-200 rounded-lg">
      <table className="w-full text-sm">
        <thead>
          <tr className="border-b border-slate-200 bg-slate-50 text-slate-500">
            {headers.map((h) => <th key={h} className="text-left py-2 px-3 capitalize">{h.replace(/_/g, ' ')}</th>)}
          </tr>
        </thead>
        <tbody>
          {data.slice(0, 50).map((row, i) => (
            <tr key={i} className="border-b border-slate-100">
              {headers.map((h) => <td key={h} className="py-2 px-3">{String(row[h] ?? '-')}</td>)}
            </tr>
          ))}
        </tbody>
      </table>
      {data.length > 50 && <p className="text-xs text-slate-400 p-2">Showing 50 of {data.length} rows</p>}
    </div>
  );
}
