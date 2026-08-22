import { useState, useEffect, useCallback, useMemo } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Input } from '@/components/ui/Form';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatIDR, todayISO } from '@/lib/format';
import { getBusinessDate } from '@/services/businessDateService';
import { FileSpreadsheet, Download } from 'lucide-react';

const REPORT_DRAFT_KEY = 'reports_form_draft';

interface ReportDraft {
  reportKey: string | null;
  dateFrom: string;
  dateTo: string;
}

function loadReportDraft(): ReportDraft | null {
  try {
    const raw = localStorage.getItem(REPORT_DRAFT_KEY);
    if (!raw) return null;
    const parsed = JSON.parse(raw) as ReportDraft;
    if (!parsed.dateFrom || !parsed.dateTo) return null;
    return parsed;
  } catch {
    return null;
  }
}

function saveReportDraft(draft: ReportDraft) {
  try {
    localStorage.setItem(REPORT_DRAFT_KEY, JSON.stringify(draft));
  } catch {
    // ignore quota errors
  }
}

type ReportCategory = 'front_office' | 'financial' | 'management';

type FieldType = 'text' | 'date' | 'money' | 'number';

interface ReportField {
  key: string;
  label: string;
  type?: FieldType;
}

interface ReportDef {
  key: string;
  labelKey: string;
  category: ReportCategory;
  fields: ReportField[];
}

const REPORTS: ReportDef[] = [
  { key: 'arrival_report', labelKey: 'reports.arrival_report', category: 'front_office', fields: [
    { key: 'reservation_number', label: 'Reservation' },
    { key: 'primary_guest.full_name', label: 'Guest' },
    { key: 'room.room_number', label: 'Room' },
    { key: 'check_in_date', label: 'Check In', type: 'date' }
  ]},
  { key: 'departure_report', labelKey: 'reports.departure_report', category: 'front_office', fields: [
    { key: 'reservation_number', label: 'Reservation' },
    { key: 'primary_guest.full_name', label: 'Guest' },
    { key: 'room.room_number', label: 'Room' },
    { key: 'actual_check_out', label: 'Checked Out', type: 'date' },
    { key: 'check_out_date', label: 'Scheduled CO', type: 'date' }
  ]},
  { key: 'inhouse_guest_report', labelKey: 'reports.inhouse_guest_report', category: 'front_office', fields: [
    { key: 'primary_guest.full_name', label: 'Guest' },
    { key: 'room.room_number', label: 'Room' },
    { key: 'check_out_date', label: 'Departure', type: 'date' }
  ]},
  { key: 'reservation_report', labelKey: 'reports.reservation_report', category: 'front_office', fields: [
    { key: 'reservation_number', label: 'Reservation' },
    { key: 'primary_guest.full_name', label: 'Guest' },
    { key: 'status', label: 'Status' },
    { key: 'rate', label: 'Rate', type: 'money' }
  ]},
  { key: 'cancellation_report', labelKey: 'reports.cancellation_report', category: 'front_office', fields: [
    { key: 'reservation_number', label: 'Reservation' },
    { key: 'primary_guest.full_name', label: 'Guest' },
    { key: 'check_in_date', label: 'Date', type: 'date' }
  ]},
  { key: 'noshow_report', labelKey: 'reports.noshow_report', category: 'front_office', fields: [
    { key: 'reservation_number', label: 'Reservation' },
    { key: 'primary_guest.full_name', label: 'Guest' },
    { key: 'check_in_date', label: 'Date', type: 'date' }
  ]},
  { key: 'daily_income_report', labelKey: 'reports.daily_income_report', category: 'financial', fields: [
    { key: 'category', label: 'Category' },
    { key: 'amount', label: 'Amount', type: 'money' }
  ]},
  { key: 'cash_report', labelKey: 'reports.cash_report', category: 'financial', fields: [
    { key: 'payment_number', label: 'Payment' },
    { key: 'amount', label: 'Amount', type: 'money' },
    { key: 'business_date', label: 'Business Date', type: 'date' }
  ]},
  { key: 'edc_report', labelKey: 'reports.edc_report', category: 'financial', fields: [
    { key: 'payment_number', label: 'Payment' },
    { key: 'payment_subtype', label: 'Type' },
    { key: 'amount', label: 'Amount', type: 'money' },
    { key: 'business_date', label: 'Business Date', type: 'date' }
  ]},
  { key: 'ota_report', labelKey: 'reports.ota_report', category: 'financial', fields: [
    { key: 'payment_number', label: 'Payment' },
    { key: 'amount', label: 'Amount', type: 'money' },
    { key: 'ota_settled', label: 'Settled' },
    { key: 'business_date', label: 'Business Date', type: 'date' }
  ]},
  { key: 'outstanding_balance_report', labelKey: 'reports.outstanding_balance_report', category: 'financial', fields: [
    { key: 'folio_number', label: 'Folio' },
    { key: 'guest.full_name', label: 'Guest' },
    { key: 'balance', label: 'Balance', type: 'money' }
  ]},
  { key: 'occupancy_pct', labelKey: 'reports.occupancy_pct', category: 'management', fields: [
    { key: 'occupancy', label: 'Occupancy %' }
  ]},
  { key: 'adr', labelKey: 'reports.adr', category: 'management', fields: [
    { key: 'adr', label: 'ADR', type: 'money' }
  ]},
  { key: 'revpar', labelKey: 'reports.revpar', category: 'management', fields: [
    { key: 'revpar', label: 'RevPAR', type: 'money' }
  ]}
];

const DEPOSIT_CATEGORY = 'deposit';

export function ReportsPage() {
  const { branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();

  const draft = useMemo(() => loadReportDraft(), []);
  const [activeReport, setActiveReport] = useState<ReportDef | null>(null);
  const [dateFrom, setDateFrom] = useState(draft?.dateFrom || '');
  const [dateTo, setDateTo] = useState(draft?.dateTo || '');
  const [businessDateResolved, setBusinessDateResolved] = useState(false);

  // Resolve the current business date on mount and use it as the default date range
  useEffect(() => {
    (async () => {
      if (draft?.dateFrom && draft?.dateTo) {
        setBusinessDateResolved(true);
        return;
      }
      const branchId = selectedBranchId || branches[0]?.id;
      let bd = todayISO();
      if (branchId) {
        try { bd = await getBusinessDate(branchId); } catch { /* fall back to today */ }
      }
      setDateFrom(bd);
      setDateTo(bd);
      setBusinessDateResolved(true);
    })();
  }, []); // run once on mount

  // Restore the active report from draft after REPORTS is available
  useEffect(() => {
    if (draft?.reportKey && !activeReport) {
      const found = REPORTS.find(r => r.key === draft.reportKey);
      if (found) setActiveReport(found);
    }
  }, []); // run once on mount
  const [data, setData] = useState<any[]>([]);
  const [summary, setSummary] = useState<any>(null);
  const [dailyBlocks, setDailyBlocks] = useState<{ payments: PaymentBlockRow[]; charges: ChargeBlockRow[] } | null>(null);
  const [loading, setLoading] = useState(false);

  const branchIds = useMemo(() => selectedBranchId ? [selectedBranchId] : branches.map(b => b.id), [selectedBranchId, branches]);

  const runReport = useCallback(async (report: ReportDef) => {
    setLoading(true);
    setData([]);
    setSummary(null);
    setDailyBlocks(null);

    if (report.category === 'front_office') {
      let q = supabase.from('reservations')
        .select('*,primary_guest:guests(*),room:rooms(*)')
        .in('branch_id', branchIds);

      if (report.key === 'arrival_report')
        q = q.eq('status', 'confirmed').gte('check_in_date', dateFrom).lte('check_in_date', dateTo);
      if (report.key === 'departure_report')
        q = q.eq('status', 'checked_out').gte('actual_check_out', dateFrom + 'T00:00:00').lte('actual_check_out', dateTo + 'T23:59:59');
      if (report.key === 'inhouse_guest_report')
        q = q.eq('status', 'checked_in');
      if (report.key === 'reservation_report')
        q = q.gte('check_in_date', dateFrom).lte('check_in_date', dateTo);
      if (report.key === 'cancellation_report')
        q = q.eq('status', 'cancelled').gte('check_in_date', dateFrom).lte('check_in_date', dateTo);
      if (report.key === 'noshow_report')
        q = q.eq('status', 'no_show').gte('check_in_date', dateFrom).lte('check_in_date', dateTo);

      const { data: r } = await q.order('check_in_date');
      setData(r || []);
    } else {
      // Financial reports — always filter by business_date
      const { data: items } = await supabase.from('folio_items').select('*')
        .in('branch_id', branchIds).eq('voided', false)
        .gte('business_date', dateFrom).lte('business_date', dateTo);

      const { data: payments } = await supabase.from('payments').select('*')
        .in('branch_id', branchIds).eq('voided', false)
        .gte('business_date', dateFrom).lte('business_date', dateTo);

      const fi = (items || []) as any[];
      const pay = (payments || []) as any[];

      if (report.key === 'daily_income_report') {
        // --- PAYMENTS BLOCK ---
        // Group payments by method code (CASH, EDC, OTA). Deposits are NOT income.
        const payByMethod: Record<string, number> = {};
        pay.forEach((p) => {
          const code = (p.payment_method_code || 'OTHER').toUpperCase();
          const label = p.is_ota ? 'OTA / Xendit' : code === 'CASH' ? 'Cash' : code === 'EDC' ? 'EDC' : code;
          payByMethod[label] = (payByMethod[label] || 0) + Number(p.amount);
        });
        const paymentRows: PaymentBlockRow[] = Object.entries(payByMethod).map(([label, amount]) => ({ label, amount }));
        const totalPayments = paymentRows.reduce((s, r) => s + r.amount, 0);

        // --- CHARGES BLOCK ---
        // Group charge items by category. Exclude deposits (not income). Exclude payments/discounts/tax from charges.
        const chargeItems = fi.filter((x) => x.item_type === 'charge' && x.amount > 0 && x.category !== DEPOSIT_CATEGORY);
        const chargeByCat: Record<string, number> = {};
        chargeItems.forEach((x) => {
          const cat = x.category || 'miscellaneous';
          const label = cat === 'room' ? 'Room Charges' : cat === 'early_checkin' ? 'Early Check-in' : cat === 'late_checkout' ? 'Late Check-out' : cat === 'amenity' ? 'Amenities' : cat === 'damage' ? 'Damage' : cat.charAt(0).toUpperCase() + cat.slice(1);
          chargeByCat[label] = (chargeByCat[label] || 0) + Number(x.amount);
        });
        const chargeRows: ChargeBlockRow[] = Object.entries(chargeByCat).map(([label, amount]) => ({ label, amount }));
        const totalCharges = chargeRows.reduce((s, r) => s + r.amount, 0);

        const discounts = fi.filter((x) => x.item_type === 'discount').reduce((s, x) => s + Math.abs(Number(x.amount)), 0);
        const tax = fi.filter((x) => x.item_type === 'tax').reduce((s, x) => s + Number(x.amount), 0);

        setDailyBlocks({ payments: paymentRows, charges: chargeRows });
        setData(chargeItems);
        setSummary({
          totalPayments,
          totalCharges,
          netIncome: totalCharges - discounts + tax,
          discounts,
          tax,
        });
      }

      else if (report.key === 'cash_report') {
        const rows = pay.filter((x) => (x.payment_method_code || '').toUpperCase() === 'CASH');
        setData(rows);
        setSummary({ total: rows.reduce((s, x) => s + Number(x.amount), 0) });
      }

      else if (report.key === 'edc_report') {
        const rows = pay.filter((x) => (x.payment_method_code || '').toUpperCase() === 'EDC');
        setData(rows);
        setSummary({ total: rows.reduce((s, x) => s + Number(x.amount), 0) });
      }

      else if (report.key === 'ota_report') {
        const rows = pay.filter((x) => x.is_ota);
        setData(rows);
        setSummary({
          total: rows.reduce((s, x) => s + Number(x.amount), 0),
          settled: rows.filter((x) => x.ota_settled).reduce((s, x) => s + Number(x.amount), 0)
        });
      }

      else if (report.key === 'outstanding_balance_report') {
        const { data: r } = await supabase.from('folios')
          .select('*,guest:guests(*)')
          .in('branch_id', branchIds).gt('balance', 0);
        setData(r || []);
        setSummary({ total: (r || []).reduce((s, x) => s + Number(x.balance), 0) });
      }

      else if (['occupancy_pct', 'adr', 'revpar'].includes(report.key)) {
        const { data: rooms } = await supabase.from('rooms')
          .select('*').in('branch_id', branchIds).eq('is_active', true);

        const days = Math.max(1, Math.ceil((new Date(dateTo).getTime() - new Date(dateFrom).getTime()) / 86400000) + 1);
        const available = (rooms?.length || 0) * days;
        const roomChargeItems = fi.filter((x) => x.category === 'room' && x.item_type === 'charge');
        const roomNights = roomChargeItems.reduce((s, x) => s + Number(x.quantity), 0);
        const revenue = roomChargeItems.reduce((s, x) => s + Number(x.amount), 0);

        setSummary({
          occupancy: available ? Math.round(roomNights / available * 100) : 0,
          adr: roomNights ? Math.round(revenue / roomNights) : 0,
          revpar: available ? Math.round(revenue / available) : 0
        });
      }

      else setData(fi);
    }

    setLoading(false);
  }, [branchIds, dateFrom, dateTo]);

  useEffect(() => {
    if (activeReport && businessDateResolved) runReport(activeReport);
  }, [activeReport, runReport, businessDateResolved]);

  // Persist the current selection + date range so it survives navigation
  useEffect(() => {
    if (businessDateResolved && dateFrom && dateTo) {
      saveReportDraft({ reportKey: activeReport?.key || null, dateFrom, dateTo });
    }
  }, [activeReport, dateFrom, dateTo, businessDateResolved]);

  const exportCSV = () => {
    if (!data.length && !summary && !dailyBlocks) return;
    let csv = '';

    if (dailyBlocks) {
      csv += 'PAYMENTS\n';
      csv += 'Category,Amount\n';
      dailyBlocks.payments.forEach(r => { csv += `${r.label},${r.amount}\n`; });
      csv += `Total,${dailyBlocks.payments.reduce((s, r) => s + r.amount, 0)}\n\n`;
      csv += 'CHARGES\n';
      csv += 'Category,Amount\n';
      dailyBlocks.charges.forEach(r => { csv += `${r.label},${r.amount}\n`; });
      csv += `Total,${dailyBlocks.charges.reduce((s, r) => s + r.amount, 0)}\n\n`;
    }

    if (summary && !dailyBlocks) {
      csv += Object.entries(summary).map(([k, v]) => `${k},${v}`).join('\n') + '\n\n';
    }

    if (data.length) {
      const headers = activeReport?.fields.map(f => f.key) || Object.keys(data[0]);
      csv += headers.join(',') + '\n';
      csv += data.map(row => headers.map(h => getValue(row, h)).join(',')).join('\n');
    }

    const blob = new Blob([csv], { type: 'text/csv' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = `${activeReport?.key || 'report'}.csv`;
    a.click();
    URL.revokeObjectURL(url);
  };

  const groups = {
    front_office: REPORTS.filter(r => r.category === 'front_office'),
    financial: REPORTS.filter(r => r.category === 'financial'),
    management: REPORTS.filter(r => r.category === 'management')
  };

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-slate-900">{t('reports.title')}</h1>

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        <div className="space-y-4">
          {Object.entries(groups).map(([k, v]) =>
            <ReportGroup key={k} title={k} reports={v} activeKey={activeReport?.key} onSelect={setActiveReport} t={t} />
          )}
        </div>

        <div className="lg:col-span-2">
          {activeReport ?
            <Card title={t(activeReport.labelKey)} actions={
              <Button size="sm" variant="outline" onClick={exportCSV}>
                <Download size={14} /> {t('reports.export_csv')}
              </Button>
            }>
              <div className="flex gap-3 mb-4">
                <Input label={t('common.from')} type="date" value={dateFrom} onChange={e => setDateFrom(e.target.value)} />
                <Input label={t('common.to')} type="date" value={dateTo} onChange={e => setDateTo(e.target.value)} />
              </div>

              {loading ? <LoadingPage /> :
                dailyBlocks ? (
                  <DailyIncomeReport blocks={dailyBlocks} summary={summary} />
                ) : summary ? (
                  <div className="space-y-4">
                    <ReportSummary summary={summary} />
                    {data.length > 0 && <ReportTable data={data} fields={activeReport.fields} />}
                  </div>
                ) : data.length ? (
                  <ReportTable data={data} fields={activeReport.fields} />
                ) : (
                  <EmptyState icon={<FileSpreadsheet size={48} />} title={t('common.no_data')} />
                )
              }
            </Card>
            :
            <Card>
              <EmptyState icon={<FileSpreadsheet size={48} />} title={t('reports.title')} />
            </Card>
          }
        </div>
      </div>
    </div>
  );
}

interface PaymentBlockRow { label: string; amount: number; }
interface ChargeBlockRow { label: string; amount: number; }

function DailyIncomeReport({ blocks, summary }: { blocks: { payments: PaymentBlockRow[]; charges: ChargeBlockRow[] }; summary: any }) {
  const totalPayments = blocks.payments.reduce((s, r) => s + r.amount, 0);
  const totalCharges = blocks.charges.reduce((s, r) => s + r.amount, 0);

  return (
    <div className="space-y-6">
      {/* Payments Block */}
      <div>
        <h3 className="text-sm font-bold text-slate-700 uppercase tracking-wide mb-2 pb-2 border-b border-slate-200">Payments by Method</h3>
        <div className="overflow-x-auto border border-slate-200 rounded-lg">
          <table className="w-full text-sm">
            <thead>
              <tr className="border-b border-slate-200 bg-slate-50 text-slate-500">
                <th className="text-left py-2 px-3">Payment Method</th>
                <th className="text-right py-2 px-3">Amount</th>
              </tr>
            </thead>
            <tbody>
              {blocks.payments.length === 0 ? (
                <tr><td colSpan={2} className="text-center py-4 text-slate-400">No payments</td></tr>
              ) : blocks.payments.map((r, i) => (
                <tr key={i} className="border-b border-slate-100">
                  <td className="py-2 px-3 font-medium text-slate-700">{r.label}</td>
                  <td className="text-right py-2 px-3 font-medium text-emerald-700">{formatIDR(r.amount)}</td>
                </tr>
              ))}
            </tbody>
            <tfoot>
              <tr className="bg-emerald-50 font-bold">
                <td className="py-2 px-3">Total Payments</td>
                <td className="text-right py-2 px-3 text-emerald-700">{formatIDR(totalPayments)}</td>
              </tr>
            </tfoot>
          </table>
        </div>
      </div>

      {/* Charges Block */}
      <div>
        <h3 className="text-sm font-bold text-slate-700 uppercase tracking-wide mb-2 pb-2 border-b border-slate-200">Charges by Category</h3>
        <div className="overflow-x-auto border border-slate-200 rounded-lg">
          <table className="w-full text-sm">
            <thead>
              <tr className="border-b border-slate-200 bg-slate-50 text-slate-500">
                <th className="text-left py-2 px-3">Charge Category</th>
                <th className="text-right py-2 px-3">Amount</th>
              </tr>
            </thead>
            <tbody>
              {blocks.charges.length === 0 ? (
                <tr><td colSpan={2} className="text-center py-4 text-slate-400">No charges</td></tr>
              ) : blocks.charges.map((r, i) => (
                <tr key={i} className="border-b border-slate-100">
                  <td className="py-2 px-3 font-medium text-slate-700">{r.label}</td>
                  <td className="text-right py-2 px-3 font-medium text-blue-700">{formatIDR(r.amount)}</td>
                </tr>
              ))}
            </tbody>
            <tfoot>
              <tr className="bg-blue-50 font-bold">
                <td className="py-2 px-3">Total Charges (excl. deposits)</td>
                <td className="text-right py-2 px-3 text-blue-700">{formatIDR(totalCharges)}</td>
              </tr>
            </tfoot>
          </table>
        </div>
      </div>

      {/* Net summary */}
      {summary && (
        <div className="grid grid-cols-2 md:grid-cols-4 gap-3">
          <div className="bg-slate-50 rounded-lg p-3">
            <p className="text-xs text-slate-500">Total Charges</p>
            <p className="text-lg font-bold text-slate-800">{formatIDR(summary.totalCharges)}</p>
          </div>
          <div className="bg-slate-50 rounded-lg p-3">
            <p className="text-xs text-slate-500">Discounts</p>
            <p className="text-lg font-bold text-red-600">-{formatIDR(summary.discounts)}</p>
          </div>
          <div className="bg-slate-50 rounded-lg p-3">
            <p className="text-xs text-slate-500">Tax</p>
            <p className="text-lg font-bold text-slate-800">{formatIDR(summary.tax)}</p>
          </div>
          <div className="bg-slate-100 rounded-lg p-3 border border-slate-300">
            <p className="text-xs text-slate-500">Net Income</p>
            <p className="text-lg font-bold text-slate-900">{formatIDR(summary.netIncome)}</p>
          </div>
        </div>
      )}
    </div>
  );
}

function ReportGroup({ title, reports, activeKey, onSelect, t }: {
  title: string;
  reports: ReportDef[];
  activeKey?: string;
  onSelect: (r: ReportDef) => void;
  t: (k: string) => string
}) {
  const groupLabels: Record<string, string> = {
    front_office: 'Front Office',
    financial: 'Financial',
    management: 'Management'
  };
  return (
    <div>
      <h3 className="text-xs font-semibold text-slate-500 uppercase mb-2">{groupLabels[title] || title}</h3>
      <div className="space-y-1">
        {reports.map(r =>
          <button key={r.key} onClick={() => onSelect(r)}
            className={`w-full text-left px-3 py-2 rounded-lg text-sm font-medium ${activeKey === r.key ? 'bg-blue-600 text-white' : 'text-slate-600 hover:bg-slate-100'}`}>
            {t(r.labelKey)}
          </button>
        )}
      </div>
    </div>
  );
}

function getValue(obj: any, path: string) {
  return path.split('.').reduce((a, k) => a?.[k], obj) ?? '-';
}

function ReportSummary({ summary }: { summary: any }) {
  return (
    <div className="grid grid-cols-2 md:grid-cols-3 gap-3">
      {Object.entries(summary).map(([k, v]) =>
        <div key={k} className="bg-slate-50 rounded-lg p-3">
          <p className="text-xs text-slate-500 capitalize">{k.replace(/_/g, ' ')}</p>
          <p className="text-lg font-bold text-slate-800">
            {typeof v === 'number' && v > 1000 ? formatIDR(v) : String(v)}
          </p>
        </div>
      )}
    </div>
  );
}

function ReportTable({ data, fields }: { data: any[]; fields: ReportField[] }) {
  if (!data.length) return null;

  const cols = fields.length ? fields : Object.keys(data[0]).map(k => ({ key: k, label: k }));

  return (
    <div className="overflow-x-auto border border-slate-200 rounded-lg">
      <table className="w-full text-sm">
        <thead>
          <tr className="border-b border-slate-200 bg-slate-50 text-slate-500">
            {cols.map((f: any) =>
              <th key={f.key} className="text-left py-2 px-3">
                {f.label}
              </th>
            )}
          </tr>
        </thead>

        <tbody>
          {data.slice(0, 50).map((row, i) =>
            <tr key={i} className="border-b border-slate-100">
              {cols.map((f: any) => {
                const value = getValue(row, f.key);
                return (
                  <td key={f.key} className="py-2 px-3">
                    {f.type === 'money' && typeof value === 'number'
                      ? formatIDR(value)
                      : f.type === 'date' && value && value !== '-'
                      ? String(value).slice(0, 10)
                      : String(value)}
                  </td>
                );
              })}
            </tr>
          )}
        </tbody>
      </table>

      {data.length > 50 &&
        <p className="text-xs text-slate-400 p-2">
          Showing 50 of {data.length} rows
        </p>
      }
    </div>
  );
}
