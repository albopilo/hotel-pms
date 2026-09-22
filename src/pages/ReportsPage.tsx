import { useState, useEffect, useCallback, useMemo } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal } from '@/components/ui/Modal';
import { Badge } from '@/components/ui/Badge';
import { Input } from '@/components/ui/Form';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatIDR, todayISO, formatDate, formatDateTime, addDays } from '@/lib/format';
import { getBusinessDate } from '@/services/businessDateService';
import { FileSpreadsheet, Download, ChevronDown, ChevronUp, FileText, User as UserIcon, Printer, TrendingUp, TrendingDown, Minus } from 'lucide-react';
import type { Folio, FolioItem, Guest, Room, Reservation } from '@/types/database';

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
  } catch { /* ignore */ }
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
    { key: 'reservation_number', label: 'Reservation' },
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
  { key: 'forward_booking_report', labelKey: 'reports.forward_booking_report', category: 'front_office', fields: [
    { key: 'reservation_number', label: 'Reservation' },
    { key: 'primary_guest.full_name', label: 'Guest' },
    { key: 'room.room_number', label: 'Room' },
    { key: 'check_in_date', label: 'Check In', type: 'date' },
    { key: 'check_out_date', label: 'Check Out', type: 'date' },
    { key: 'num_nights', label: 'Nights', type: 'number' },
    { key: 'rate', label: 'Rate', type: 'money' },
  ]},
  { key: 'pickup_report', labelKey: 'reports.pickup_report', category: 'front_office', fields: [
    { key: 'type', label: 'Type' },
    { key: 'reservation_number', label: 'Reservation' },
    { key: 'primary_guest.full_name', label: 'Guest' },
    { key: 'check_in_date', label: 'Check In', type: 'date' },
    { key: 'created_at', label: 'Created', type: 'date' },
  ]},
  { key: 'room_transfer_report', labelKey: 'reports.room_transfer_report', category: 'front_office', fields: [
    { key: 'reservation_id', label: 'Reservation' },
    { key: 'from_room_id', label: 'From Room' },
    { key: 'to_room_id', label: 'To Room' },
    { key: 'reason', label: 'Reason' },
    { key: 'created_at', label: 'Date', type: 'date' },
  ]},
  { key: 'cashier_shift_report', labelKey: 'reports.cashier_shift_report', category: 'financial', fields: [
    { key: 'type', label: 'Type' },
    { key: 'description', label: 'Description' },
    { key: 'amount', label: 'Amount', type: 'money' },
    { key: 'posted_at', label: 'Posted At', type: 'date' },
  ]},
  { key: 'daily_income_report', labelKey: 'reports.daily_income_report', category: 'financial', fields: [
    { key: 'category', label: 'Category' },
    { key: 'amount', label: 'Amount', type: 'money' }
  ]},
  { key: 'monthly_income_report', labelKey: 'reports.monthly_income_report', category: 'financial', fields: [
    { key: 'month', label: 'Month' },
    { key: 'room_revenue', label: 'Room Revenue', type: 'money' },
    { key: 'ancillary_revenue', label: 'Ancillary Revenue', type: 'money' },
    { key: 'total_revenue', label: 'Total Revenue', type: 'money' },
    { key: 'total_payments', label: 'Payments', type: 'money' },
  ]},
  { key: 'revenue_by_branch', labelKey: 'reports.revenue_by_branch', category: 'financial', fields: [
    { key: 'branch_name', label: 'Branch' },
    { key: 'room_revenue', label: 'Room Revenue', type: 'money' },
    { key: 'ancillary_revenue', label: 'Ancillary Revenue', type: 'money' },
    { key: 'total_revenue', label: 'Total Revenue', type: 'money' },
    { key: 'occupancy_pct', label: 'Occupancy %', type: 'number' },
  ]},
  { key: 'revenue_by_room_type', labelKey: 'reports.revenue_by_room_type', category: 'financial', fields: [
    { key: 'room_type', label: 'Room Type' },
    { key: 'room_nights', label: 'Room Nights', type: 'number' },
    { key: 'revenue', label: 'Revenue', type: 'money' },
    { key: 'adr', label: 'ADR', type: 'money' },
  ]},
  { key: 'revenue_by_booking_source', labelKey: 'reports.revenue_by_booking_source', category: 'financial', fields: [
    { key: 'booking_source', label: 'Booking Source' },
    { key: 'reservations', label: 'Reservations', type: 'number' },
    { key: 'room_nights', label: 'Room Nights', type: 'number' },
    { key: 'revenue', label: 'Revenue', type: 'money' },
    { key: 'adr', label: 'ADR', type: 'money' },
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
    { key: 'ota_settlement_date', label: 'Settlement Date', type: 'date' },
    { key: 'business_date', label: 'Business Date', type: 'date' }
  ]},
  { key: 'outstanding_balance_report', labelKey: 'reports.outstanding_balance_report', category: 'financial', fields: [
    { key: 'folio_number', label: 'Folio' },
    { key: 'guest.full_name', label: 'Guest' },
    { key: 'balance', label: 'Balance', type: 'money' }
  ]},
  { key: 'deposit_report', labelKey: 'reports.deposit_report', category: 'financial', fields: [
    { key: 'reservation_id', label: 'Reservation' },
    { key: 'amount', label: 'Amount', type: 'money' },
    { key: 'movement_type', label: 'Movement Type' },
    { key: 'business_date', label: 'Business Date', type: 'date' },
  ]},
  { key: 'refund_report', labelKey: 'reports.refund_report', category: 'financial', fields: [
    { key: 'reservation_id', label: 'Reservation' },
    { key: 'amount', label: 'Amount', type: 'money' },
    { key: 'reason', label: 'Reason' },
    { key: 'business_date', label: 'Business Date', type: 'date' },
  ]},
  { key: 'discount_report', labelKey: 'reports.discount_report', category: 'financial', fields: [
    { key: 'description', label: 'Description' },
    { key: 'amount', label: 'Amount', type: 'money' },
    { key: 'business_date', label: 'Business Date', type: 'date' },
  ]},
  { key: 'tax_report', labelKey: 'reports.tax_report', category: 'financial', fields: [
    { key: 'description', label: 'Description' },
    { key: 'amount', label: 'Amount', type: 'money' },
    { key: 'business_date', label: 'Business Date', type: 'date' },
  ]},
  { key: 'additional_charge_report', labelKey: 'reports.additional_charge_report', category: 'financial', fields: [
    { key: 'description', label: 'Description' },
    { key: 'category_code', label: 'Category' },
    { key: 'amount', label: 'Amount', type: 'money' },
    { key: 'is_damage', label: 'Damage' },
    { key: 'status', label: 'Status' },
    { key: 'business_date', label: 'Business Date', type: 'date' },
  ]},
  { key: 'damage_charge_report', labelKey: 'reports.damage_charge_report', category: 'financial', fields: [
    { key: 'description', label: 'Description' },
    { key: 'amount', label: 'Amount', type: 'money' },
    { key: 'status', label: 'Status' },
    { key: 'business_date', label: 'Business Date', type: 'date' },
  ]},
  { key: 'reconciliation_report', labelKey: 'reports.reconciliation_report', category: 'financial', fields: [
    { key: 'transaction_type', label: 'Transaction Type' },
    { key: 'description', label: 'Description' },
    { key: 'amount', label: 'Amount', type: 'money' },
    { key: 'debit_credit', label: 'Dr/Cr' },
    { key: 'business_date', label: 'Business Date', type: 'date' },
  ]},
  { key: 'occupancy_pct', labelKey: 'reports.occupancy_pct', category: 'management', fields: [
    { key: 'occupancy', label: 'Occupancy %' }
  ]},
  { key: 'adr', labelKey: 'reports.adr', category: 'management', fields: [
    { key: 'adr', label: 'ADR', type: 'money' }
  ]},
  { key: 'revpar', labelKey: 'reports.revpar', category: 'management', fields: [
    { key: 'revpar', label: 'RevPAR', type: 'money' }
  ]},
  { key: 'kpi_summary', labelKey: 'reports.kpi_summary', category: 'management', fields: [
    { key: 'metric', label: 'Metric' },
    { key: 'current', label: 'Current Period' },
    { key: 'previous', label: 'Previous Period' },
    { key: 'change_pct', label: 'Change %' },
  ]},
  { key: 'guest_statistics', labelKey: 'reports.guest_statistics', category: 'management', fields: [
    { key: 'metric', label: 'Metric' },
    { key: 'value', label: 'Value' },
  ]},
  { key: 'housekeeping_report', labelKey: 'reports.housekeeping_report', category: 'management', fields: [
    { key: 'room_number', label: 'Room' },
    { key: 'status', label: 'Status' },
    { key: 'floor', label: 'Floor' },
  ]},
];

const DEPOSIT_CATEGORY = 'deposit';

const FRONT_OFFICE_REPORT_KEYS = new Set([
  'arrival_report', 'departure_report', 'inhouse_guest_report', 'reservation_report',
  'cancellation_report', 'noshow_report', 'forward_booking_report', 'pickup_report',
  'room_transfer_report',
]);

const CASHIER_REPORT_KEY = 'cashier_shift_report';

// ── Detailed row types for the expandable daily income report ──────────────

interface PaymentDetailRow {
  payment_number: string;
  amount: number;
  reservation_id: string;
  folio_id: string;
  guest_id: string | null;
  guest_name: string;
  room_number: string;
  business_date: string;
}

interface ChargeDetailRow {
  description: string;
  amount: number;
  reservation_id: string;
  folio_id: string;
  guest_id: string | null;
  guest_name: string;
  room_number: string;
  business_date: string;
}

interface PaymentBlockRow {
  label: string;
  amount: number;
  details: PaymentDetailRow[];
}

interface ChargeBlockRow {
  label: string;
  amount: number;
  details: ChargeDetailRow[];
}

// ── Cashier Shift Report types ──────────────────────────────────────────────

interface CashierDetailRow {
  description: string;
  amount: number;
  reservation_id: string;
  folio_id: string;
  guest_name: string;
  room_number: string;
  posted_at: string;
  business_date: string;
}

interface CashierBlockRow {
  label: string;
  amount: number;
  details: CashierDetailRow[];
}

interface CashierShiftBlocks {
  payments: CashierBlockRow[];
  charges: CashierBlockRow[];
}

// ── Date presets ────────────────────────────────────────────────────────────

interface DatePreset {
  labelKey: string;
  getRange: (businessDate: string) => { from: string; to: string };
}

const DATE_PRESETS: DatePreset[] = [
  { labelKey: 'reports.preset_today', getRange: (bd) => ({ from: bd, to: bd }) },
  { labelKey: 'reports.preset_yesterday', getRange: (bd) => { const d = addDays(bd, -1); return { from: d, to: d }; } },
  { labelKey: 'reports.preset_this_week', getRange: (bd) => { const [y, m, d] = bd.slice(0, 10).split('-').map(Number); const day = new Date(y, m - 1, d).getDay(); const monday = addDays(bd, -(day === 0 ? 6 : day - 1)); return { from: monday, to: addDays(monday, 6) }; } },
  { labelKey: 'reports.preset_last_week', getRange: (bd) => { const [y, m, d] = bd.slice(0, 10).split('-').map(Number); const day = new Date(y, m - 1, d).getDay(); const monday = addDays(bd, -(day === 0 ? 6 : day - 1) - 7); return { from: monday, to: addDays(monday, 6) }; } },
  { labelKey: 'reports.preset_this_month', getRange: (bd) => { const [y, m] = bd.slice(0, 10).split('-').map(Number); const first = `${y}-${String(m).padStart(2, '0')}-01`; const lastDay = new Date(y, m, 0).getDate(); const last = `${y}-${String(m).padStart(2, '0')}-${String(lastDay).padStart(2, '0')}`; return { from: first, to: last }; } },
  { labelKey: 'reports.preset_last_month', getRange: (bd) => { const [y, m] = bd.slice(0, 10).split('-').map(Number); const pm = m - 1; const py = pm < 1 ? y - 1 : y; const rm = pm < 1 ? 12 : pm; const first = `${py}-${String(rm).padStart(2, '0')}-01`; const lastDay = new Date(py, rm, 0).getDate(); const last = `${py}-${String(rm).padStart(2, '0')}-${String(lastDay).padStart(2, '0')}`; return { from: first, to: last }; } },
  { labelKey: 'reports.preset_last_7_days', getRange: (bd) => ({ from: addDays(bd, -6), to: bd }) },
  { labelKey: 'reports.preset_last_30_days', getRange: (bd) => ({ from: addDays(bd, -29), to: bd }) },
];

export function ReportsPage() {
  const { user, branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();

  const isReceptionist = user?.role === 'receptionist';

  const draft = useMemo(() => loadReportDraft(), []);
  const [activeReport, setActiveReport] = useState<ReportDef | null>(null);
  const [dateFrom, setDateFrom] = useState(draft?.dateFrom || '');
  const [dateTo, setDateTo] = useState(draft?.dateTo || '');
  const [businessDateResolved, setBusinessDateResolved] = useState(false);
  const [resolvedBusinessDate, setResolvedBusinessDate] = useState(todayISO());

  // Receptionists get front office operational reports + daily income
  const accessibleReports = isReceptionist
    ? REPORTS.filter(r => r.key === 'daily_income_report' || r.key === CASHIER_REPORT_KEY || FRONT_OFFICE_REPORT_KEYS.has(r.key))
    : REPORTS;

  // Resolve the current business date on mount and use it as the default date range
  useEffect(() => {
    (async () => {
      if (draft?.dateFrom && draft?.dateTo) {
        setBusinessDateResolved(true);
        const branchId = selectedBranchId || branches[0]?.id;
        if (branchId) { try { setResolvedBusinessDate(await getBusinessDate(branchId)); } catch { /* */ } }
        return;
      }
      const branchId = selectedBranchId || branches[0]?.id;
      let bd = todayISO();
      if (branchId) {
        try { bd = await getBusinessDate(branchId); } catch { /* fall back to today */ }
      }
      setResolvedBusinessDate(bd);
      setDateFrom(bd);
      setDateTo(bd);
      setBusinessDateResolved(true);
    })();
  }, []); // run once on mount

  // Restore the active report from draft, or auto-select Daily Income for receptionists
  useEffect(() => {
    if (activeReport) return;
    if (draft?.reportKey) {
      const found = accessibleReports.find(r => r.key === draft.reportKey);
      if (found) { setActiveReport(found); return; }
    }
    if (isReceptionist) {
      const found = accessibleReports.find(r => r.key === CASHIER_REPORT_KEY);
      if (found) setActiveReport(found);
      return;
    }
    const first = accessibleReports[0];
    if (first) setActiveReport(first);
  }, []); // run once on mount

  const [data, setData] = useState<any[]>([]);
  const [summary, setSummary] = useState<any>(null);
  const [dailyBlocks, setDailyBlocks] = useState<{ payments: PaymentBlockRow[]; charges: ChargeBlockRow[] } | null>(null);
  const [cashierBlocks, setCashierBlocks] = useState<CashierShiftBlocks | null>(null);
  const [kpiBlocks, setKpiBlocks] = useState<any>(null);
  const [loading, setLoading] = useState(false);

  const branchIds = useMemo(() => selectedBranchId ? [selectedBranchId] : branches.map(b => b.id), [selectedBranchId, branches]);

  // ── Shared data fetcher for financial reports ─────────────────────────────
  const fetchFinancialData = useCallback(async () => {
    const { data: items } = await supabase.from('folio_items').select('*')
      .in('branch_id', branchIds).eq('voided', false)
      .gte('business_date', dateFrom).lte('business_date', dateTo);

    const { data: payments } = await supabase.from('payments').select('*')
      .in('branch_id', branchIds).eq('voided', false)
      .gte('business_date', dateFrom).lte('business_date', dateTo);

    const { data: voidedFolios } = await supabase
      .from('folios').select('id').in('branch_id', branchIds).eq('status', 'void');
    const voidedFolioIds = new Set((voidedFolios || []).map((f: any) => f.id));

    const { data: cancelledRes } = await supabase
      .from('reservations').select('id').in('branch_id', branchIds).eq('status', 'cancelled');
    const cancelledResIds = new Set((cancelledRes || []).map((r: any) => r.id));

    const fi = ((items || []) as any[]).filter(
      (x) => !voidedFolioIds.has(x.folio_id) && !cancelledResIds.has(x.reservation_id)
    );
    const pay = ((payments || []) as any[]).filter(
      (x) => !voidedFolioIds.has(x.folio_id) && !cancelledResIds.has(x.reservation_id)
    );

    return { fi, pay };
  }, [branchIds, dateFrom, dateTo]);

  // ── KPI computation helper ─────────────────────────────────────────────────
  const computeKPIs = useCallback((fi: any[], roomsCount: number, from: string, to: string) => {
    const days = Math.max(1, Math.ceil((new Date(to).getTime() - new Date(from).getTime()) / 86400000) + 1);
    const available = roomsCount * days;
    const roomChargeItems = fi.filter((x) => x.category === 'room' && x.item_type === 'charge');
    const roomNights = roomChargeItems.reduce((s, x) => s + Number(x.quantity), 0);
    const roomRevenue = roomChargeItems.reduce((s, x) => s + Number(x.amount), 0);
    const allCharges = fi.filter((x) => x.item_type === 'charge' && x.amount > 0);
    const ancillaryRevenue = allCharges.filter((x) => x.category !== 'room').reduce((s, x) => s + Number(x.amount), 0);
    const tax = fi.filter((x) => x.item_type === 'tax').reduce((s, x) => s + Number(x.amount), 0);
    const totalRevenue = roomRevenue + ancillaryRevenue + tax;

    return {
      occupancy: available ? Math.round((roomNights / available) * 100) : 0,
      adr: roomNights ? Math.round(roomRevenue / roomNights) : 0,
      revpar: available ? Math.round(roomRevenue / available) : 0,
      roomNights,
      availableRoomNights: available,
      roomRevenue,
      ancillaryRevenue,
      totalRevenue,
    };
  }, []);

  const runReport = useCallback(async (report: ReportDef) => {
    setLoading(true);
    setData([]);
    setSummary(null);
    setDailyBlocks(null);
    setCashierBlocks(null);
    setKpiBlocks(null);

    if (report.category === 'front_office') {
      if (report.key === 'forward_booking_report') {
        const { data: r } = await supabase.from('reservations')
          .select('*,primary_guest:guests(*),room:rooms(*)')
          .in('branch_id', branchIds)
          .in('status', ['confirmed', 'tentative', 'checked_in'])
          .gte('check_in_date', dateFrom)
          .lte('check_out_date', dateTo)
          .order('check_in_date');
        const rows = (r || []) as any[];
        const expectedRevenue = rows.reduce((s, x) => s + Number(x.rate || 0) * Number(x.num_nights || 1), 0);
        const expectedRoomNights = rows.reduce((s, x) => s + Number(x.num_nights || 0), 0);
        const { data: rooms } = await supabase.from('rooms').select('id').in('branch_id', branchIds).eq('is_active', true);
        const days = Math.max(1, Math.ceil((new Date(dateTo).getTime() - new Date(dateFrom).getTime()) / 86400000) + 1);
        const available = (rooms?.length || 0) * days;
        setData(rows);
        setSummary({
          expected_revenue: expectedRevenue,
          expected_room_nights: expectedRoomNights,
          expected_occupancy: available ? Math.round((expectedRoomNights / available) * 100) : 0,
          reservations_count: rows.length,
        });
      } else if (report.key === 'pickup_report') {
        const { data: newRes } = await supabase.from('reservations')
          .select('*,primary_guest:guests(*)')
          .in('branch_id', branchIds)
          .gte('created_at', dateFrom + 'T00:00:00+07:00')
          .lte('created_at', dateTo + 'T23:59:59+07:00')
          .order('created_at', { ascending: false });
        const { data: cancelledRes } = await supabase.from('reservations')
          .select('*,primary_guest:guests(*)')
          .in('branch_id', branchIds)
          .eq('status', 'cancelled')
          .gte('updated_at', dateFrom + 'T00:00:00+07:00')
          .lte('updated_at', dateTo + 'T23:59:59+07:00')
          .order('updated_at', { ascending: false });
        const newRows = ((newRes || []) as any[]).map((r) => ({ ...r, type: 'New' }));
        const cancelRows = ((cancelledRes || []) as any[]).map((r) => ({ ...r, type: 'Cancellation' }));
        const allRows = [...newRows, ...cancelRows];
        setData(allRows);
        setSummary({
          new_reservations: newRows.length,
          cancelled_reservations: cancelRows.length,
          net_pickup: newRows.length - cancelRows.length,
        });
      } else if (report.key === 'room_transfer_report') {
        const { data: transfers } = await supabase.from('room_transfers')
          .select('*,reservation:reservations(reservation_number,primary_guest:guests(full_name))')
          .in('branch_id', branchIds)
          .gte('created_at', dateFrom + 'T00:00:00+07:00')
          .lte('created_at', dateTo + 'T23:59:59+07:00')
          .order('created_at', { ascending: false });
        const transferRows = (transfers || []) as any[];
        // Resolve room numbers
        const roomIds = new Set<string>();
        transferRows.forEach((tr) => { if (tr.from_room_id) roomIds.add(tr.from_room_id); if (tr.to_room_id) roomIds.add(tr.to_room_id); });
        let roomMap: Record<string, string> = {};
        if (roomIds.size > 0) {
          const { data: roomsData } = await supabase.from('rooms').select('id,room_number').in('id', Array.from(roomIds));
          (roomsData || []).forEach((r: any) => { roomMap[r.id] = r.room_number; });
        }
        const rows = transferRows.map((tr) => ({
          reservation_id: tr.reservation?.reservation_number || tr.reservation_id,
          guest_name: tr.reservation?.primary_guest?.full_name || '-',
          from_room_id: tr.from_room_id ? roomMap[tr.from_room_id] || '-' : '-',
          to_room_id: tr.to_room_id ? roomMap[tr.to_room_id] || '-' : '-',
          reason: tr.reason || '-',
          created_at: tr.created_at,
        }));
        setData(rows);
        setSummary({ total_transfers: rows.length });
      } else {
        let q = supabase.from('reservations')
          .select('*,primary_guest:guests(*),room:rooms(*)')
          .in('branch_id', branchIds);

        if (report.key === 'arrival_report')
          q = q.eq('status', 'confirmed').gte('check_in_date', dateFrom).lte('check_in_date', dateTo);
        if (report.key === 'departure_report')
          q = q.eq('status', 'checked_out').gte('actual_check_out', dateFrom + 'T00:00:00+07:00').lte('actual_check_out', dateTo + 'T23:59:59+07:00');
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
      }
    } else if (report.category === 'financial') {
      const { fi, pay } = await fetchFinancialData();

      if (report.key === CASHIER_REPORT_KEY) {
        // Fetch by created_at (actual transaction time), adjusted for the branch's
        // business day cutoff so e.g. a 01:00 AM transaction on Sep 19 counts as Sep 18.
        const branchIdForCutoff = selectedBranchId || branches[0]?.id;
        let cutoffTime = '00:00:00';
        let timezone = 'Asia/Jakarta';
        if (branchIdForCutoff) {
          const { data: branchInfo } = await supabase
            .from('branches').select('business_day_cutoff, timezone').eq('id', branchIdForCutoff).maybeSingle();
          if (branchInfo) {
            cutoffTime = branchInfo.business_day_cutoff || '04:30:00';
            timezone = branchInfo.timezone || 'Asia/Jakarta';
          }
        }

        // The cutoff defines when a business date starts. For dateFrom, the window
        // opens at cutoff time on the previous calendar day. For dateTo, it closes
        // at cutoff time on that calendar day (exclusive — so we use the end of
        // the previous second, i.e. cutoff - 1 sec, but simpler: use lte cutoff
        // on dateTo which is exclusive in practice because the next day starts there).
        // Business date D starts at cutoff on calendar day D and ends just before cutoff on calendar day D+1.
        // e.g. with cutoff 04:30: business date Sep 21 = Sep 21 04:30 to Sep 22 04:30 (Jakarta time).
        // Append +07:00 so Postgres interprets these as Jakarta time, not UTC.
        const fromTs = dateFrom + 'T' + cutoffTime + '+07:00';
        const toTs = addDays(dateTo, 1) + 'T' + cutoffTime + '+07:00';

        const { data: itemsByTime } = await supabase.from('folio_items').select('*')
          .in('branch_id', branchIds).eq('voided', false)
          .gte('created_at', fromTs).lt('created_at', toTs)
          .order('created_at', { ascending: false });

        const { data: paymentsByTime } = await supabase.from('payments').select('*')
          .in('branch_id', branchIds).eq('voided', false)
          .gte('created_at', fromTs).lt('created_at', toTs)
          .order('created_at', { ascending: false });

        const { data: voidedFolios } = await supabase
          .from('folios').select('id').in('branch_id', branchIds).eq('status', 'void');
        const voidedFolioIds = new Set((voidedFolios || []).map((f: any) => f.id));
        const { data: cancelledRes } = await supabase
          .from('reservations').select('id').in('branch_id', branchIds).eq('status', 'cancelled');
        const cancelledResIds = new Set((cancelledRes || []).map((r: any) => r.id));

        const timeFi = ((itemsByTime || []) as any[]).filter(
          (x) => !voidedFolioIds.has(x.folio_id) && !cancelledResIds.has(x.reservation_id)
        );
        const timePay = ((paymentsByTime || []) as any[]).filter(
          (x) => !voidedFolioIds.has(x.folio_id) && !cancelledResIds.has(x.reservation_id)
        );

        // Build reservation map for guest/room info
        const resIds = new Set<string>();
        timePay.forEach((p) => { if (p.reservation_id) resIds.add(p.reservation_id); });
        timeFi.forEach((x) => { if (x.reservation_id) resIds.add(x.reservation_id); });
        let resMap: Record<string, any> = {};
        if (resIds.size > 0) {
          const { data: resData } = await supabase
            .from('reservations')
            .select('*,primary_guest:guests(full_name),room:rooms(room_number)')
            .in('id', Array.from(resIds));
          (resData || []).forEach((r: any) => { resMap[r.id] = r; });
        }

        // Group payments by method
        const payByMethod: Record<string, { amount: number; details: CashierDetailRow[] }> = {};
        timePay.forEach((p) => {
          const code = (p.payment_method_code || 'OTHER').toUpperCase();
          const label = p.is_ota ? 'OTA / Xendit' : code === 'CASH' ? 'Cash' : code === 'EDC' ? 'EDC' : code;
          if (!payByMethod[label]) payByMethod[label] = { amount: 0, details: [] };
          payByMethod[label].amount += Number(p.amount);
          const res = p.reservation_id ? resMap[p.reservation_id] : null;
          payByMethod[label].details.push({
            description: `Payment ${p.payment_number || ''}`.trim(),
            amount: Number(p.amount),
            reservation_id: p.reservation_id || '',
            folio_id: p.folio_id || '',
            guest_name: res?.primary_guest?.full_name || '-',
            room_number: res?.room?.room_number || '-',
            posted_at: p.created_at,
            business_date: p.business_date,
          });
        });
        const paymentRows: CashierBlockRow[] = Object.entries(payByMethod).map(([label, val]) => ({
          label, amount: val.amount, details: val.details,
        }));
        const totalPayments = paymentRows.reduce((s, r) => s + r.amount, 0);

        // Group charges by category (only charges posted during this time, regardless of business_date)
        const chargeItems = timeFi.filter((x) => x.item_type === 'charge' && x.amount > 0 && x.category !== DEPOSIT_CATEGORY);
        const chargeByCat: Record<string, { amount: number; details: CashierDetailRow[] }> = {};
        chargeItems.forEach((x) => {
          const cat = x.category || 'miscellaneous';
          const label = cat === 'room' ? 'Room Charges' : cat === 'early_checkin' ? 'Early Check-in' : cat === 'late_checkout' ? 'Late Check-out' : cat === 'amenity' ? 'Amenities' : cat === 'damage' ? 'Damage' : cat.charAt(0).toUpperCase() + cat.slice(1);
          if (!chargeByCat[label]) chargeByCat[label] = { amount: 0, details: [] };
          chargeByCat[label].amount += Number(x.amount);
          const res = x.reservation_id ? resMap[x.reservation_id] : null;
          chargeByCat[label].details.push({
            description: x.description,
            amount: Number(x.amount),
            reservation_id: x.reservation_id || '',
            folio_id: x.folio_id || '',
            guest_name: res?.primary_guest?.full_name || '-',
            room_number: res?.room?.room_number || '-',
            posted_at: x.created_at,
            business_date: x.business_date,
          });
        });
        const chargeRows: CashierBlockRow[] = Object.entries(chargeByCat).map(([label, val]) => ({
          label, amount: val.amount, details: val.details,
        }));
        const totalCharges = chargeRows.reduce((s, r) => s + r.amount, 0);

        setCashierBlocks({ payments: paymentRows, charges: chargeRows });
        setData([]);
        setSummary({
          totalPayments,
          totalCharges,
          netCash: totalPayments - totalCharges,
        });
      }

      else if (report.key === 'daily_income_report') {
        const resIds = new Set<string>();
        pay.forEach((p) => { if (p.reservation_id) resIds.add(p.reservation_id); if (p.folio_id) resIds.add(p.folio_id); });
        fi.forEach((x) => { if (x.reservation_id) resIds.add(x.reservation_id); if (x.folio_id) resIds.add(x.folio_id); });

        let resMap: Record<string, any> = {};
        if (resIds.size > 0) {
          const { data: resData } = await supabase
            .from('reservations')
            .select('*,primary_guest:guests(full_name),room:rooms(room_number)')
            .in('id', Array.from(resIds));
          (resData || []).forEach((r: any) => { resMap[r.id] = r; });
        }

        const payByMethod: Record<string, { amount: number; details: PaymentDetailRow[] }> = {};
        pay.forEach((p) => {
          const code = (p.payment_method_code || 'OTHER').toUpperCase();
          const label = p.is_ota ? 'OTA / Xendit' : code === 'CASH' ? 'Cash' : code === 'EDC' ? 'EDC' : code;
          if (!payByMethod[label]) payByMethod[label] = { amount: 0, details: [] };
          payByMethod[label].amount += Number(p.amount);
          const res = p.reservation_id ? resMap[p.reservation_id] : null;
          payByMethod[label].details.push({
            payment_number: p.payment_number, amount: Number(p.amount),
            reservation_id: p.reservation_id || '', folio_id: p.folio_id || '',
            guest_id: p.guest_id || null, guest_name: res?.primary_guest?.full_name || '-',
            room_number: res?.room?.room_number || '-', business_date: p.business_date,
          });
        });
        const paymentRows: PaymentBlockRow[] = Object.entries(payByMethod).map(([label, val]) => ({ label, amount: val.amount, details: val.details }));
        const totalPayments = paymentRows.reduce((s, r) => s + r.amount, 0);

        const chargeItems = fi.filter((x) => x.item_type === 'charge' && x.amount > 0 && x.category !== DEPOSIT_CATEGORY);
        const chargeByCat: Record<string, { amount: number; details: ChargeDetailRow[] }> = {};
        chargeItems.forEach((x) => {
          const cat = x.category || 'miscellaneous';
          const label = cat === 'room' ? 'Room Charges' : cat === 'early_checkin' ? 'Early Check-in' : cat === 'late_checkout' ? 'Late Check-out' : cat === 'amenity' ? 'Amenities' : cat === 'damage' ? 'Damage' : cat.charAt(0).toUpperCase() + cat.slice(1);
          if (!chargeByCat[label]) chargeByCat[label] = { amount: 0, details: [] };
          chargeByCat[label].amount += Number(x.amount);
          const res = x.reservation_id ? resMap[x.reservation_id] : null;
          chargeByCat[label].details.push({
            description: x.description, amount: Number(x.amount),
            reservation_id: x.reservation_id || '', folio_id: x.folio_id || '',
            guest_id: x.guest_id || null, guest_name: res?.primary_guest?.full_name || '-',
            room_number: res?.room?.room_number || '-', business_date: x.business_date,
          });
        });
        const chargeRows: ChargeBlockRow[] = Object.entries(chargeByCat).map(([label, val]) => ({ label, amount: val.amount, details: val.details }));
        const totalCharges = chargeRows.reduce((s, r) => s + r.amount, 0);

        const discounts = fi.filter((x) => x.item_type === 'discount').reduce((s, x) => s + Math.abs(Number(x.amount)), 0);
        const tax = fi.filter((x) => x.item_type === 'tax').reduce((s, x) => s + Number(x.amount), 0);

        setDailyBlocks({ payments: paymentRows, charges: chargeRows });
        setData(chargeItems);
        setSummary({ totalPayments, totalCharges, netIncome: totalCharges - discounts + tax, discounts, tax });
      }

      else if (report.key === 'monthly_income_report') {
        const monthMap: Record<string, { room_revenue: number; ancillary_revenue: number; total_revenue: number; total_payments: number; tax: number; discounts: number }> = {};
        fi.forEach((x) => {
          const month = String(x.business_date).slice(0, 7);
          if (!monthMap[month]) monthMap[month] = { room_revenue: 0, ancillary_revenue: 0, total_revenue: 0, total_payments: 0, tax: 0, discounts: 0 };
          if (x.item_type === 'charge' && x.amount > 0) {
            if (x.category === 'room') monthMap[month].room_revenue += Number(x.amount);
            else if (x.category !== DEPOSIT_CATEGORY) monthMap[month].ancillary_revenue += Number(x.amount);
          } else if (x.item_type === 'tax') {
            monthMap[month].tax += Number(x.amount);
          } else if (x.item_type === 'discount') {
            monthMap[month].discounts += Math.abs(Number(x.amount));
          }
        });
        pay.forEach((p) => {
          const month = String(p.business_date).slice(0, 7);
          if (!monthMap[month]) monthMap[month] = { room_revenue: 0, ancillary_revenue: 0, total_revenue: 0, total_payments: 0, tax: 0, discounts: 0 };
          monthMap[month].total_payments += Number(p.amount);
        });
        const rows = Object.entries(monthMap).map(([month, v]) => ({
          month, room_revenue: v.room_revenue, ancillary_revenue: v.ancillary_revenue + v.tax,
          total_revenue: v.room_revenue + v.ancillary_revenue + v.tax - v.discounts,
          total_payments: v.total_payments,
        })).sort((a, b) => a.month.localeCompare(b.month));
        setData(rows);
        setSummary({
          total_revenue: rows.reduce((s, r) => s + r.total_revenue, 0),
          total_payments: rows.reduce((s, r) => s + r.total_payments, 0),
          months: rows.length,
        });
      }

      else if (report.key === 'revenue_by_branch') {
        const branchMap: Record<string, { room_revenue: number; ancillary_revenue: number; total_revenue: number; room_nights: number }> = {};
        branches.forEach((b) => { branchMap[b.id] = { room_revenue: 0, ancillary_revenue: 0, total_revenue: 0, room_nights: 0 }; });
        fi.forEach((x) => {
          if (!branchMap[x.branch_id]) return;
          if (x.item_type === 'charge' && x.amount > 0) {
            if (x.category === 'room') { branchMap[x.branch_id].room_revenue += Number(x.amount); branchMap[x.branch_id].room_nights += Number(x.quantity); }
            else if (x.category !== DEPOSIT_CATEGORY) branchMap[x.branch_id].ancillary_revenue += Number(x.amount);
          }
        });
        const { data: roomsData } = await supabase.from('rooms').select('id,branch_id').in('branch_id', branchIds).eq('is_active', true);
        const roomCountByBranch: Record<string, number> = {};
        (roomsData || []).forEach((r: any) => { roomCountByBranch[r.branch_id] = (roomCountByBranch[r.branch_id] || 0) + 1; });
        const days = Math.max(1, Math.ceil((new Date(dateTo).getTime() - new Date(dateFrom).getTime()) / 86400000) + 1);
        const rows = branches.map((b) => {
          const v = branchMap[b.id] || { room_revenue: 0, ancillary_revenue: 0, total_revenue: 0, room_nights: 0 };
          const available = (roomCountByBranch[b.id] || 0) * days;
          return {
            branch_name: b.name,
            room_revenue: v.room_revenue,
            ancillary_revenue: v.ancillary_revenue,
            total_revenue: v.room_revenue + v.ancillary_revenue,
            occupancy_pct: available ? Math.round((v.room_nights / available) * 100) : 0,
          };
        });
        setData(rows);
        setSummary({ total_revenue: rows.reduce((s, r) => s + r.total_revenue, 0) });
      }

      else if (report.key === 'revenue_by_room_type') {
        const { data: roomsData } = await supabase.from('rooms').select('id,room_type_id,room_type:room_types(name)').in('branch_id', branchIds).eq('is_active', true);
        const roomToType: Record<string, string> = {};
        (roomsData || []).forEach((r: any) => { roomToType[r.id] = r.room_type?.name || 'Unknown'; });
        const typeMap: Record<string, { room_nights: number; revenue: number }> = {};
        fi.forEach((x) => {
          if (x.category === 'room' && x.item_type === 'charge') {
            const typeName = x.room_id ? (roomToType[x.room_id] || 'Unknown') : 'Unknown';
            if (!typeMap[typeName]) typeMap[typeName] = { room_nights: 0, revenue: 0 };
            typeMap[typeName].room_nights += Number(x.quantity);
            typeMap[typeName].revenue += Number(x.amount);
          }
        });
        const rows = Object.entries(typeMap).map(([room_type, v]) => ({
          room_type, room_nights: v.room_nights, revenue: v.revenue,
          adr: v.room_nights ? Math.round(v.revenue / v.room_nights) : 0,
        })).sort((a, b) => b.revenue - a.revenue);
        setData(rows);
        setSummary({ total_revenue: rows.reduce((s, r) => s + r.revenue, 0), total_room_nights: rows.reduce((s, r) => s + r.room_nights, 0) });
      }

      else if (report.key === 'revenue_by_booking_source') {
        const resIds = new Set<string>();
        fi.forEach((x) => { if (x.reservation_id) resIds.add(x.reservation_id); });
        const { data: resData } = await supabase.from('reservations')
          .select('id,booking_source_id,booking_source:booking_sources(name),num_nights,rate')
          .in('id', Array.from(resIds));
        const resMap: Record<string, any> = {};
        (resData || []).forEach((r: any) => { resMap[r.id] = r; });
        const sourceMap: Record<string, { reservations: number; room_nights: number; revenue: number }> = {};
        const countedResBySource: Record<string, Set<string>> = {};
        fi.forEach((x) => {
          if (x.category === 'room' && x.item_type === 'charge' && x.reservation_id) {
            const res = resMap[x.reservation_id];
            const sourceName = res?.booking_source?.name || 'Direct / Walk-in';
            if (!sourceMap[sourceName]) { sourceMap[sourceName] = { reservations: 0, room_nights: 0, revenue: 0 }; countedResBySource[sourceName] = new Set(); }
            if (!countedResBySource[sourceName].has(x.reservation_id)) { sourceMap[sourceName].reservations++; countedResBySource[sourceName].add(x.reservation_id); }
            sourceMap[sourceName].room_nights += Number(x.quantity);
            sourceMap[sourceName].revenue += Number(x.amount);
          }
        });
        const rows = Object.entries(sourceMap).map(([booking_source, v]: [string, any]) => ({
          booking_source, reservations: v.reservations, room_nights: v.room_nights,
          revenue: v.revenue, adr: v.room_nights ? Math.round(v.revenue / v.room_nights) : 0,
        })).sort((a, b) => b.revenue - a.revenue);
        setData(rows);
        setSummary({ total_revenue: rows.reduce((s, r) => s + r.revenue, 0) });
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
          settled: rows.filter((x) => x.ota_settled).reduce((s, x) => s + Number(x.amount), 0),
          pending: rows.filter((x) => !x.ota_settled).reduce((s, x) => s + Number(x.amount), 0),
        });
      }

      else if (report.key === 'outstanding_balance_report') {
        const { data: r } = await supabase.from('folios')
          .select('*,guest:guests(*)')
          .in('branch_id', branchIds).gt('balance', 0)
          .neq('status', 'void');
        setData(r || []);
        setSummary({ total: (r || []).reduce((s, x) => s + Number(x.balance), 0) });
      }

      else if (report.key === 'deposit_report') {
        const { data: deposits } = await supabase.from('deposits').select('*')
          .in('branch_id', branchIds)
          .gte('business_date', dateFrom).lte('business_date', dateTo)
          .order('created_at', { ascending: false });
        setData(deposits || []);
        setSummary({ total: (deposits || []).reduce((s, x: any) => s + Number(x.amount), 0) });
      }

      else if (report.key === 'refund_report') {
        const { data: refunds } = await supabase.from('refunds').select('*')
          .in('branch_id', branchIds)
          .gte('business_date', dateFrom).lte('business_date', dateTo)
          .order('created_at', { ascending: false });
        setData(refunds || []);
        setSummary({ total: (refunds || []).reduce((s, x: any) => s + Number(x.amount), 0) });
      }

      else if (report.key === 'discount_report') {
        const rows = fi.filter((x) => x.item_type === 'discount');
        setData(rows);
        setSummary({ total: rows.reduce((s, x) => s + Math.abs(Number(x.amount)), 0) });
      }

      else if (report.key === 'tax_report') {
        const rows = fi.filter((x) => x.item_type === 'tax');
        setData(rows);
        setSummary({ total: rows.reduce((s, x) => s + Number(x.amount), 0) });
      }

      else if (report.key === 'additional_charge_report') {
        const { data: charges } = await supabase.from('additional_charges').select('*')
          .in('branch_id', branchIds)
          .gte('business_date', dateFrom).lte('business_date', dateTo)
          .order('created_at', { ascending: false });
        setData(charges || []);
        setSummary({ total: (charges || []).reduce((s, x: any) => s + Number(x.amount), 0) });
      }

      else if (report.key === 'damage_charge_report') {
        const { data: charges } = await supabase.from('additional_charges').select('*')
          .in('branch_id', branchIds).eq('is_damage', true)
          .gte('business_date', dateFrom).lte('business_date', dateTo)
          .order('created_at', { ascending: false });
        setData(charges || []);
        setSummary({ total: (charges || []).reduce((s, x: any) => s + Number(x.amount), 0) });
      }

      else if (report.key === 'reconciliation_report') {
        const { data: txns } = await supabase.from('transactions').select('*')
          .in('branch_id', branchIds)
          .gte('business_date', dateFrom).lte('business_date', dateTo)
          .order('business_date', { ascending: true }).order('created_at', { ascending: true });
        const rows = (txns || []) as any[];
        let runningBalance = 0;
        const rowsWithBalance = rows.map((r) => {
          if (r.debit_credit === 'debit') runningBalance += Number(r.amount);
          else runningBalance -= Number(r.amount);
          return { ...r, running_balance: runningBalance };
        });
        setData(rowsWithBalance);
        const totalDebits = rows.filter((r) => r.debit_credit === 'debit').reduce((s, r) => s + Number(r.amount), 0);
        const totalCredits = rows.filter((r) => r.debit_credit === 'credit').reduce((s, r) => s + Number(r.amount), 0);
        setSummary({ total_debits: totalDebits, total_credits: totalCredits, net_balance: totalDebits - totalCredits });
      }

      else setData(fi);
    } else if (report.category === 'management') {
      if (report.key === 'kpi_summary') {
        const { fi } = await fetchFinancialData();
        const { data: rooms } = await supabase.from('rooms').select('id').in('branch_id', branchIds).eq('is_active', true);
        const roomsCount = rooms?.length || 0;

        // Current period KPIs
        const current = computeKPIs(fi, roomsCount, dateFrom, dateTo);

        // Previous period KPIs (same length, immediately before)
        const periodDays = Math.max(1, Math.ceil((new Date(dateTo).getTime() - new Date(dateFrom).getTime()) / 86400000) + 1);
        const prevTo = addDays(dateFrom, -1);
        const prevFrom = addDays(prevTo, -(periodDays - 1));

        const { data: prevItems } = await supabase.from('folio_items').select('*')
          .in('branch_id', branchIds).eq('voided', false)
          .gte('business_date', prevFrom).lte('business_date', prevTo);
        const { data: prevVoidedFolios } = await supabase.from('folios').select('id').in('branch_id', branchIds).eq('status', 'void');
        const prevVoidedFolioIds = new Set((prevVoidedFolios || []).map((f: any) => f.id));
        const { data: prevCancelledRes } = await supabase.from('reservations').select('id').in('branch_id', branchIds).eq('status', 'cancelled');
        const prevCancelledResIds = new Set((prevCancelledRes || []).map((r: any) => r.id));
        const prevFi = ((prevItems || []) as any[]).filter(
          (x) => !prevVoidedFolioIds.has(x.folio_id) && !prevCancelledResIds.has(x.reservation_id)
        );

        const previous = computeKPIs(prevFi, roomsCount, prevFrom, prevTo);

        const metrics = [
          { metric: 'Occupancy %', current: current.occupancy, previous: previous.occupancy, change_pct: previous.occupancy ? Math.round(((current.occupancy - previous.occupancy) / previous.occupancy) * 100) : 0 },
          { metric: 'ADR', current: current.adr, previous: previous.adr, change_pct: previous.adr ? Math.round(((current.adr - previous.adr) / previous.adr) * 100) : 0 },
          { metric: 'RevPAR', current: current.revpar, previous: previous.revpar, change_pct: previous.revpar ? Math.round(((current.revpar - previous.revpar) / previous.revpar) * 100) : 0 },
          { metric: 'Room Nights Sold', current: current.roomNights, previous: previous.roomNights, change_pct: previous.roomNights ? Math.round(((current.roomNights - previous.roomNights) / previous.roomNights) * 100) : 0 },
          { metric: 'Room Revenue', current: current.roomRevenue, previous: previous.roomRevenue, change_pct: previous.roomRevenue ? Math.round(((current.roomRevenue - previous.roomRevenue) / previous.roomRevenue) * 100) : 0 },
          { metric: 'Ancillary Revenue', current: current.ancillaryRevenue, previous: previous.ancillaryRevenue, change_pct: previous.ancillaryRevenue ? Math.round(((current.ancillaryRevenue - previous.ancillaryRevenue) / previous.ancillaryRevenue) * 100) : 0 },
          { metric: 'Total Revenue', current: current.totalRevenue, previous: previous.totalRevenue, change_pct: previous.totalRevenue ? Math.round(((current.totalRevenue - previous.totalRevenue) / previous.totalRevenue) * 100) : 0 },
        ];

        setKpiBlocks({ metrics, prevFrom, prevTo });
        setData(metrics);
      } else if (report.key === 'guest_statistics') {
        const { data: guestsData } = await supabase.from('guests').select('id,nationality,gender,created_at');
        const allGuests = (guestsData || []) as any[];

        // Nationality breakdown
        const nationalityMap: Record<string, number> = {};
        allGuests.forEach((g) => { const n = g.nationality || 'Unknown'; nationalityMap[n] = (nationalityMap[n] || 0) + 1; });
        const nationalityRows = Object.entries(nationalityMap).map(([nationality, count]) => ({ metric: nationality, value: count })).sort((a, b) => b.value - a.value);

        // Gender breakdown
        const genderMap: Record<string, number> = {};
        allGuests.forEach((g) => { const gn = g.gender || 'Unknown'; genderMap[gn] = (genderMap[gn] || 0) + 1; });
        const genderRows = Object.entries(genderMap).map(([gender, count]) => ({ metric: `Gender: ${gender}`, value: count }));

        // Repeat vs new guests (based on reservations)
        const { data: resData } = await supabase.from('reservations').select('primary_guest_id').neq('status', 'cancelled');
        const guestStayCount: Record<string, number> = {};
        (resData || []).forEach((r: any) => { if (r.primary_guest_id) guestStayCount[r.primary_guest_id] = (guestStayCount[r.primary_guest_id] || 0) + 1; });
        const repeatGuests = Object.values(guestStayCount).filter((c) => c > 1).length;
        const newGuests = Object.values(guestStayCount).filter((c) => c === 1).length;

        // Avg length of stay
        const { data: checkedOutRes } = await supabase.from('reservations').select('num_nights').eq('status', 'checked_out');
        const avgStay = checkedOutRes && checkedOutRes.length > 0
          ? (checkedOutRes.reduce((s, r: any) => s + Number(r.num_nights), 0) / checkedOutRes.length).toFixed(1)
          : '0';

        const rows = [
          ...nationalityRows,
          ...genderRows,
          { metric: 'Repeat Guests', value: repeatGuests },
          { metric: 'New Guests', value: newGuests },
          { metric: 'Total Guests', value: allGuests.length },
          { metric: 'Avg Length of Stay (nights)', value: avgStay },
        ];
        setData(rows);
        setSummary({ total_guests: allGuests.length, repeat_guests: repeatGuests, new_guests: newGuests });
      } else if (report.key === 'housekeeping_report') {
        const { data: roomsData } = await supabase.from('rooms').select('id,room_number,status,floor,room_type:room_types(name)')
          .in('branch_id', branchIds).eq('is_active', true).order('room_number');
        const rooms = (roomsData || []) as any[];
        const statusCounts: Record<string, number> = {};
        rooms.forEach((r) => { statusCounts[r.status] = (statusCounts[r.status] || 0) + 1; });
        const statusRows = Object.entries(statusCounts).map(([status, count]) => ({ metric: status.replace(/_/g, ' '), value: count }));
        setData(rooms);
        setSummary({ total_rooms: rooms.length, status_distribution: statusRows });
      } else if (['occupancy_pct', 'adr', 'revpar'].includes(report.key)) {
        const { fi } = await fetchFinancialData();
        const { data: rooms } = await supabase.from('rooms').select('id').in('branch_id', branchIds).eq('is_active', true);
        const kpis = computeKPIs(fi, rooms?.length || 0, dateFrom, dateTo);

        // Previous period for comparison
        const periodDays = Math.max(1, Math.ceil((new Date(dateTo).getTime() - new Date(dateFrom).getTime()) / 86400000) + 1);
        const prevTo = addDays(dateFrom, -1);
        const prevFrom = addDays(prevTo, -(periodDays - 1));
        const { data: prevItems } = await supabase.from('folio_items').select('*')
          .in('branch_id', branchIds).eq('voided', false)
          .gte('business_date', prevFrom).lte('business_date', prevTo);
        const { data: prevVoidedFolios } = await supabase.from('folios').select('id').in('branch_id', branchIds).eq('status', 'void');
        const prevVoidedFolioIds = new Set((prevVoidedFolios || []).map((f: any) => f.id));
        const { data: prevCancelledRes } = await supabase.from('reservations').select('id').in('branch_id', branchIds).eq('status', 'cancelled');
        const prevCancelledResIds = new Set((prevCancelledRes || []).map((r: any) => r.id));
        const prevFi = ((prevItems || []) as any[]).filter(
          (x) => !prevVoidedFolioIds.has(x.folio_id) && !prevCancelledResIds.has(x.reservation_id)
        );
        const prevKpis = computeKPIs(prevFi, rooms?.length || 0, prevFrom, prevTo);

        const currentVal = report.key === 'occupancy_pct' ? kpis.occupancy : report.key === 'adr' ? kpis.adr : kpis.revpar;
        const prevVal = report.key === 'occupancy_pct' ? prevKpis.occupancy : report.key === 'adr' ? prevKpis.adr : prevKpis.revpar;
        const changePct = prevVal ? Math.round(((currentVal - prevVal) / prevVal) * 100) : 0;

        setSummary({
          current: currentVal,
          previous: prevVal,
          change_pct: changePct,
          prevFrom,
          prevTo,
          roomNights: kpis.roomNights,
          availableRoomNights: kpis.availableRoomNights,
          roomRevenue: kpis.roomRevenue,
          ancillaryRevenue: kpis.ancillaryRevenue,
          totalRevenue: kpis.totalRevenue,
        });
      }
    }

    setLoading(false);
  }, [branchIds, dateFrom, dateTo, fetchFinancialData, computeKPIs]);

  useEffect(() => {
    if (activeReport && businessDateResolved) runReport(activeReport);
  }, [activeReport, runReport, businessDateResolved]);

  // Persist the current selection + date range so it survives navigation
  useEffect(() => {
    if (businessDateResolved && dateFrom && dateTo) {
      saveReportDraft({ reportKey: activeReport?.key || null, dateFrom, dateTo });
    }
  }, [activeReport, dateFrom, dateTo, businessDateResolved]);

  const applyPreset = (preset: DatePreset) => {
    const { from, to } = preset.getRange(resolvedBusinessDate);
    setDateFrom(from);
    setDateTo(to);
  };

  const exportCSV = () => {
    if (!data.length && !summary && !dailyBlocks && !kpiBlocks) return;
    let csv = '';
    const reportTitle = activeReport ? t(activeReport.labelKey) : 'Report';
    const branchLabel = selectedBranchId ? branches.find(b => b.id === selectedBranchId)?.name || 'All' : 'All Branches';

    csv += `# ${reportTitle}\n`;
    csv += `# Date Range: ${dateFrom} to ${dateTo}\n`;
    csv += `# Branch: ${branchLabel}\n`;
    csv += `# Generated: ${new Date().toISOString()}\n\n`;

    if (cashierBlocks) {
      csv += 'PAYMENTS RECEIVED (by posted time)\n';
      csv += 'Method,Amount\n';
      cashierBlocks.payments.forEach(r => { csv += `${r.label},${r.amount}\n`; });
      csv += `Total,${cashierBlocks.payments.reduce((s, r) => s + r.amount, 0)}\n\n`;
      csv += 'CHARGES POSTED (by posted time)\n';
      csv += 'Category,Amount\n';
      cashierBlocks.charges.forEach(r => { csv += `${r.label},${r.amount}\n`; });
      csv += `Total,${cashierBlocks.charges.reduce((s, r) => s + r.amount, 0)}\n\n`;
    }

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

    if (kpiBlocks) {
      csv += 'KPI Summary\n';
      csv += 'Metric,Current Period,Previous Period,Change %\n';
      kpiBlocks.metrics.forEach((m: any) => { csv += `${m.metric},${m.current},${m.previous},${m.change_pct}\n`; });
      csv += '\n';
    }

    if (summary && !dailyBlocks && !kpiBlocks) {
      csv += 'SUMMARY\n';
      csv += Object.entries(summary).map(([k, v]) => `${k.replace(/_/g, ' ')},${v}`).join('\n') + '\n\n';
    }

    if (data.length) {
      const headers = activeReport?.fields.map(f => f.label) || Object.keys(data[0]);
      csv += headers.join(',') + '\n';
      csv += data.map(row => {
        const fields = activeReport?.fields || Object.keys(data[0]).map(k => ({ key: k, label: k }));
        return fields.map((f: any) => {
          const value = getValue(row, f.key);
          if (f.type === 'money' && typeof value === 'number') return value;
          if (f.type === 'date' && value && value !== '-') return String(value).slice(0, 10);
          return value;
        }).join(',');
      }).join('\n');
    }

    const blob = new Blob([csv], { type: 'text/csv' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    const filename = `${activeReport?.key || 'report'}-${dateFrom}-to-${dateTo}.csv`;
    a.href = url;
    a.download = filename;
    a.click();
    URL.revokeObjectURL(url);
  };

  const printReport = () => {
    window.print();
  };

  const groups = isReceptionist
    ? {
        cashier: accessibleReports.filter(r => r.key === CASHIER_REPORT_KEY),
        daily_income: accessibleReports.filter(r => r.key === 'daily_income_report'),
        front_office: accessibleReports.filter(r => r.category === 'front_office'),
      }
    : {
        front_office: accessibleReports.filter(r => r.category === 'front_office'),
        financial: accessibleReports.filter(r => r.category === 'financial'),
        management: accessibleReports.filter(r => r.category === 'management')
      };

  const isKpiReport = activeReport?.key === 'kpi_summary';
  const isSingleKpi = activeReport && ['occupancy_pct', 'adr', 'revpar'].includes(activeReport.key);
  const isHousekeeping = activeReport?.key === 'housekeeping_report';
  const isGuestStats = activeReport?.key === 'guest_statistics';

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
              <div className="flex gap-2">
                <Button size="sm" variant="outline" onClick={printReport}>
                  <Printer size={14} /> {t('reports.print_report')}
                </Button>
                <Button size="sm" variant="outline" onClick={exportCSV}>
                  <Download size={14} /> {t('reports.export_csv')}
                </Button>
              </div>
            }>
              {/* Date range + presets */}
              <div className="flex flex-wrap gap-3 mb-3">
                <Input label={t('common.from')} type="date" value={dateFrom} onChange={e => setDateFrom(e.target.value)} />
                <Input label={t('common.to')} type="date" value={dateTo} onChange={e => setDateTo(e.target.value)} />
              </div>
              <div className="flex flex-wrap gap-1.5 mb-4">
                {DATE_PRESETS.map((preset) => (
                  <button
                    key={preset.labelKey}
                    onClick={() => applyPreset(preset)}
                    className="px-2.5 py-1 rounded-lg text-xs font-medium border border-slate-200 text-slate-600 hover:bg-blue-50 hover:border-blue-300 hover:text-blue-700 transition-colors"
                  >
                    {t(preset.labelKey)}
                  </button>
                ))}
              </div>

              {loading ? <LoadingPage /> :
                cashierBlocks ? (
                  <CashierShiftReport blocks={cashierBlocks} summary={summary} />
                ) : dailyBlocks ? (
                  <DailyIncomeReport blocks={dailyBlocks} summary={summary} />
                ) : isKpiReport && kpiBlocks ? (
                  <KpiSummaryReport kpiBlocks={kpiBlocks} />
                ) : isSingleKpi && summary ? (
                  <SingleKpiReport reportKey={activeReport.key} summary={summary} t={t} />
                ) : isHousekeeping && summary ? (
                  <HousekeepingReport data={data} summary={summary} t={t} />
                ) : isGuestStats ? (
                  <div className="space-y-4">
                    {summary && <ReportSummary summary={summary} />}
                    {data.length > 0 && <ReportTable data={data} fields={activeReport.fields} />}
                  </div>
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

// ════════════════════════════════════════════════════════════════════════════
// Cashier Shift Report — transactions by actual posted time (created_at)
// ════════════════════════════════════════════════════════════════════════════

function CashierShiftReport({ blocks, summary }: { blocks: CashierShiftBlocks; summary: any }) {
  const totalPayments = blocks.payments.reduce((s, r) => s + r.amount, 0);
  const totalCharges = blocks.charges.reduce((s, r) => s + r.amount, 0);
  const [expandedPayments, setExpandedPayments] = useState<Set<string>>(new Set());
  const [expandedCharges, setExpandedCharges] = useState<Set<string>>(new Set());
  const [folioReservationId, setFolioReservationId] = useState<string | null>(null);

  const togglePayment = (label: string) => {
    setExpandedPayments((prev) => {
      const next = new Set(prev);
      if (next.has(label)) next.delete(label);
      else next.add(label);
      return next;
    });
  };

  const toggleCharge = (label: string) => {
    setExpandedCharges((prev) => {
      const next = new Set(prev);
      if (next.has(label)) next.delete(label);
      else next.add(label);
      return next;
    });
  };

  return (
    <div className="space-y-6">
      <div className="bg-amber-50 border border-amber-200 rounded-lg p-3 text-sm text-amber-800">
        This report shows transactions by the actual date and time they were posted, using the branch's business day cutoff. Transactions after midnight but before the cutoff still count as the previous business day — matching what the cashier physically received.
      </div>

      {/* Payments Block */}
      <div>
        <h3 className="text-sm font-bold text-slate-700 uppercase tracking-wide mb-2 pb-2 border-b border-slate-200">Payments Received by Method</h3>
        <div className="overflow-x-auto border border-slate-200 rounded-lg">
          <table className="w-full text-sm">
            <thead>
              <tr className="border-b border-slate-200 bg-slate-50 text-slate-500">
                <th className="text-left py-2 px-3 w-8"></th>
                <th className="text-left py-2 px-3">Payment Method</th>
                <th className="text-right py-2 px-3">Amount</th>
              </tr>
            </thead>
            <tbody>
              {blocks.payments.length === 0 ? (
                <tr><td colSpan={3} className="text-center py-4 text-slate-400">No payments</td></tr>
              ) : blocks.payments.map((r) => {
                const isOpen = expandedPayments.has(r.label);
                return (
                  <CashierRowGroup key={r.label} row={r} isOpen={isOpen} onToggle={() => togglePayment(r.label)} onViewFolio={(resId) => setFolioReservationId(resId)} isPayment />
                );
              })}
            </tbody>
            <tfoot>
              <tr className="bg-emerald-50 font-bold">
                <td colSpan={2} className="py-2 px-3">Total Payments Received</td>
                <td className="text-right py-2 px-3 text-emerald-700">{formatIDR(totalPayments)}</td>
              </tr>
            </tfoot>
          </table>
        </div>
      </div>

      {/* Charges Block */}
      <div>
        <h3 className="text-sm font-bold text-slate-700 uppercase tracking-wide mb-2 pb-2 border-b border-slate-200">Charges Posted by Category</h3>
        <div className="overflow-x-auto border border-slate-200 rounded-lg">
          <table className="w-full text-sm">
            <thead>
              <tr className="border-b border-slate-200 bg-slate-50 text-slate-500">
                <th className="text-left py-2 px-3 w-8"></th>
                <th className="text-left py-2 px-3">Charge Category</th>
                <th className="text-right py-2 px-3">Amount</th>
              </tr>
            </thead>
            <tbody>
              {blocks.charges.length === 0 ? (
                <tr><td colSpan={3} className="text-center py-4 text-slate-400">No charges</td></tr>
              ) : blocks.charges.map((r) => {
                const isOpen = expandedCharges.has(r.label);
                return (
                  <CashierRowGroup key={r.label} row={r} isOpen={isOpen} onToggle={() => toggleCharge(r.label)} onViewFolio={(resId) => setFolioReservationId(resId)} />
                );
              })}
            </tbody>
            <tfoot>
              <tr className="bg-blue-50 font-bold">
                <td colSpan={2} className="py-2 px-3">Total Charges Posted</td>
                <td className="text-right py-2 px-3 text-blue-700">{formatIDR(totalCharges)}</td>
              </tr>
            </tfoot>
          </table>
        </div>
      </div>

      {/* Net cash summary */}
      {summary && (
        <div className="grid grid-cols-1 md:grid-cols-3 gap-3">
          <div className="bg-emerald-50 rounded-lg p-3">
            <p className="text-xs text-slate-500">Total Payments Received</p>
            <p className="text-lg font-bold text-emerald-700">{formatIDR(summary.totalPayments)}</p>
          </div>
          <div className="bg-blue-50 rounded-lg p-3">
            <p className="text-xs text-slate-500">Total Charges Posted</p>
            <p className="text-lg font-bold text-blue-700">{formatIDR(summary.totalCharges)}</p>
          </div>
          <div className="bg-slate-100 rounded-lg p-3 border border-slate-300">
            <p className="text-xs text-slate-500">Net Cash Position (Received - Posted)</p>
            <p className={`text-lg font-bold ${summary.netCash >= 0 ? 'text-slate-900' : 'text-red-600'}`}>
              {summary.netCash >= 0 ? '+' : ''}{formatIDR(summary.netCash)}
            </p>
          </div>
        </div>
      )}

      {folioReservationId && (
        <FolioDetailModal reservationId={folioReservationId} onClose={() => setFolioReservationId(null)} />
      )}
    </div>
  );
}

function CashierRowGroup({ row, isOpen, onToggle, onViewFolio, isPayment }: {
  row: CashierBlockRow;
  isOpen: boolean;
  onToggle: () => void;
  onViewFolio: (reservationId: string) => void;
  isPayment?: boolean;
}) {
  return (
    <>
      <tr className="border-b border-slate-100 cursor-pointer hover:bg-slate-50" onClick={onToggle}>
        <td className="py-2 px-3 text-slate-400">{isOpen ? <ChevronUp size={14} /> : <ChevronDown size={14} />}</td>
        <td className="py-2 px-3 font-medium text-slate-700">{row.label}</td>
        <td className={`text-right py-2 px-3 font-medium ${isPayment ? 'text-emerald-700' : 'text-blue-700'}`}>{formatIDR(row.amount)}</td>
      </tr>
      {isOpen && row.details.length > 0 && (
        <tr className="bg-slate-50/50">
          <td colSpan={3} className="px-3 pb-3 pt-1">
            <div className="overflow-x-auto border border-slate-200 rounded-lg">
              <table className="w-full text-xs">
                <thead>
                  <tr className="border-b border-slate-200 bg-white text-slate-500">
                    <th className="text-left py-1.5 px-3">Description</th>
                    <th className="text-left py-1.5 px-3">Guest</th>
                    <th className="text-left py-1.5 px-3">Room</th>
                    <th className="text-right py-1.5 px-3">Amount</th>
                    <th className="text-left py-1.5 px-3">Posted At</th>
                    <th className="text-left py-1.5 px-3">Business Date</th>
                    <th className="text-center py-1.5 px-3">Folio</th>
                  </tr>
                </thead>
                <tbody>
                  {row.details.map((d, i) => (
                    <tr key={i} className="border-b border-slate-100 hover:bg-blue-50/50">
                      <td className="py-1.5 px-3 text-slate-700">{d.description}</td>
                      <td className="py-1.5 px-3 text-slate-700">{d.guest_name}</td>
                      <td className="py-1.5 px-3 text-slate-600">{d.room_number}</td>
                      <td className={`text-right py-1.5 px-3 font-medium ${isPayment ? 'text-emerald-700' : 'text-blue-700'}`}>{formatIDR(d.amount)}</td>
                      <td className="py-1.5 px-3 text-slate-500">{formatDateTime(d.posted_at)}</td>
                      <td className="py-1.5 px-3 text-slate-400">{formatDate(d.business_date)}</td>
                      <td className="text-center py-1.5 px-3">
                        {d.reservation_id && (
                          <button onClick={(e) => { e.stopPropagation(); onViewFolio(d.reservation_id); }} className="text-blue-600 hover:text-blue-700 font-medium inline-flex items-center gap-1">
                            <FileText size={12} /> View
                          </button>
                        )}
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </td>
        </tr>
      )}
    </>
  );
}

// ════════════════════════════════════════════════════════════════════════════
// Daily Income Report — expandable rows with reservation details + folio modal
// ════════════════════════════════════════════════════════════════════════════

function DailyIncomeReport({ blocks, summary }: { blocks: { payments: PaymentBlockRow[]; charges: ChargeBlockRow[] }; summary: any }) {
  const totalPayments = blocks.payments.reduce((s, r) => s + r.amount, 0);
  const totalCharges = blocks.charges.reduce((s, r) => s + r.amount, 0);
  const [expandedPayments, setExpandedPayments] = useState<Set<string>>(new Set());
  const [expandedCharges, setExpandedCharges] = useState<Set<string>>(new Set());
  const [folioReservationId, setFolioReservationId] = useState<string | null>(null);

  const togglePayment = (label: string) => {
    setExpandedPayments((prev) => {
      const next = new Set(prev);
      if (next.has(label)) next.delete(label);
      else next.add(label);
      return next;
    });
  };

  const toggleCharge = (label: string) => {
    setExpandedCharges((prev) => {
      const next = new Set(prev);
      if (next.has(label)) next.delete(label);
      else next.add(label);
      return next;
    });
  };

  return (
    <div className="space-y-6">
      {/* Payments Block */}
      <div>
        <h3 className="text-sm font-bold text-slate-700 uppercase tracking-wide mb-2 pb-2 border-b border-slate-200">Payments by Method</h3>
        <div className="overflow-x-auto border border-slate-200 rounded-lg">
          <table className="w-full text-sm">
            <thead>
              <tr className="border-b border-slate-200 bg-slate-50 text-slate-500">
                <th className="text-left py-2 px-3 w-8"></th>
                <th className="text-left py-2 px-3">Payment Method</th>
                <th className="text-right py-2 px-3">Amount</th>
              </tr>
            </thead>
            <tbody>
              {blocks.payments.length === 0 ? (
                <tr><td colSpan={3} className="text-center py-4 text-slate-400">No payments</td></tr>
              ) : blocks.payments.map((r) => {
                const isOpen = expandedPayments.has(r.label);
                return (
                  <PaymentRowGroup key={r.label} row={r} isOpen={isOpen} onToggle={() => togglePayment(r.label)} onViewFolio={(resId) => setFolioReservationId(resId)} />
                );
              })}
            </tbody>
            <tfoot>
              <tr className="bg-emerald-50 font-bold">
                <td colSpan={2} className="py-2 px-3">Total Payments</td>
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
                <th className="text-left py-2 px-3 w-8"></th>
                <th className="text-left py-2 px-3">Charge Category</th>
                <th className="text-right py-2 px-3">Amount</th>
              </tr>
            </thead>
            <tbody>
              {blocks.charges.length === 0 ? (
                <tr><td colSpan={3} className="text-center py-4 text-slate-400">No charges</td></tr>
              ) : blocks.charges.map((r) => {
                const isOpen = expandedCharges.has(r.label);
                return (
                  <ChargeRowGroup key={r.label} row={r} isOpen={isOpen} onToggle={() => toggleCharge(r.label)} onViewFolio={(resId) => setFolioReservationId(resId)} />
                );
              })}
            </tbody>
            <tfoot>
              <tr className="bg-blue-50 font-bold">
                <td colSpan={2} className="py-2 px-3">Total Charges (excl. deposits)</td>
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

      {folioReservationId && (
        <FolioDetailModal reservationId={folioReservationId} onClose={() => setFolioReservationId(null)} />
      )}
    </div>
  );
}

function PaymentRowGroup({ row, isOpen, onToggle, onViewFolio }: {
  row: PaymentBlockRow;
  isOpen: boolean;
  onToggle: () => void;
  onViewFolio: (reservationId: string) => void;
}) {
  return (
    <>
      <tr className="border-b border-slate-100 cursor-pointer hover:bg-slate-50" onClick={onToggle}>
        <td className="py-2 px-3 text-slate-400">{isOpen ? <ChevronUp size={14} /> : <ChevronDown size={14} />}</td>
        <td className="py-2 px-3 font-medium text-slate-700">{row.label}</td>
        <td className="text-right py-2 px-3 font-medium text-emerald-700">{formatIDR(row.amount)}</td>
      </tr>
      {isOpen && row.details.length > 0 && (
        <tr className="bg-slate-50/50">
          <td colSpan={3} className="px-3 pb-3 pt-1">
            <div className="overflow-x-auto border border-slate-200 rounded-lg">
              <table className="w-full text-xs">
                <thead>
                  <tr className="border-b border-slate-200 bg-white text-slate-500">
                    <th className="text-left py-1.5 px-3">Payment #</th>
                    <th className="text-left py-1.5 px-3">Guest</th>
                    <th className="text-left py-1.5 px-3">Room</th>
                    <th className="text-right py-1.5 px-3">Amount</th>
                    <th className="text-left py-1.5 px-3">Business Date</th>
                    <th className="text-center py-1.5 px-3">Folio</th>
                  </tr>
                </thead>
                <tbody>
                  {row.details.map((d, i) => (
                    <tr key={i} className="border-b border-slate-100 hover:bg-blue-50/50">
                      <td className="py-1.5 px-3 font-mono text-slate-600">{d.payment_number}</td>
                      <td className="py-1.5 px-3 text-slate-700">{d.guest_name}</td>
                      <td className="py-1.5 px-3 text-slate-600">{d.room_number}</td>
                      <td className="text-right py-1.5 px-3 font-medium text-emerald-700">{formatIDR(d.amount)}</td>
                      <td className="py-1.5 px-3 text-slate-400">{formatDate(d.business_date)}</td>
                      <td className="text-center py-1.5 px-3">
                        {d.reservation_id && (
                          <button onClick={(e) => { e.stopPropagation(); onViewFolio(d.reservation_id); }} className="text-blue-600 hover:text-blue-700 font-medium inline-flex items-center gap-1">
                            <FileText size={12} /> View
                          </button>
                        )}
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </td>
        </tr>
      )}
    </>
  );
}

function ChargeRowGroup({ row, isOpen, onToggle, onViewFolio }: {
  row: ChargeBlockRow;
  isOpen: boolean;
  onToggle: () => void;
  onViewFolio: (reservationId: string) => void;
}) {
  return (
    <>
      <tr className="border-b border-slate-100 cursor-pointer hover:bg-slate-50" onClick={onToggle}>
        <td className="py-2 px-3 text-slate-400">{isOpen ? <ChevronUp size={14} /> : <ChevronDown size={14} />}</td>
        <td className="py-2 px-3 font-medium text-slate-700">{row.label}</td>
        <td className="text-right py-2 px-3 font-medium text-blue-700">{formatIDR(row.amount)}</td>
      </tr>
      {isOpen && row.details.length > 0 && (
        <tr className="bg-slate-50/50">
          <td colSpan={3} className="px-3 pb-3 pt-1">
            <div className="overflow-x-auto border border-slate-200 rounded-lg">
              <table className="w-full text-xs">
                <thead>
                  <tr className="border-b border-slate-200 bg-white text-slate-500">
                    <th className="text-left py-1.5 px-3">Description</th>
                    <th className="text-left py-1.5 px-3">Guest</th>
                    <th className="text-left py-1.5 px-3">Room</th>
                    <th className="text-right py-1.5 px-3">Amount</th>
                    <th className="text-left py-1.5 px-3">Business Date</th>
                    <th className="text-center py-1.5 px-3">Folio</th>
                  </tr>
                </thead>
                <tbody>
                  {row.details.map((d, i) => (
                    <tr key={i} className="border-b border-slate-100 hover:bg-blue-50/50">
                      <td className="py-1.5 px-3 text-slate-700">{d.description}</td>
                      <td className="py-1.5 px-3 text-slate-700">{d.guest_name}</td>
                      <td className="py-1.5 px-3 text-slate-600">{d.room_number}</td>
                      <td className="text-right py-1.5 px-3 font-medium text-blue-700">{formatIDR(d.amount)}</td>
                      <td className="py-1.5 px-3 text-slate-400">{formatDate(d.business_date)}</td>
                      <td className="text-center py-1.5 px-3">
                        {d.reservation_id && (
                          <button onClick={(e) => { e.stopPropagation(); onViewFolio(d.reservation_id); }} className="text-blue-600 hover:text-blue-700 font-medium inline-flex items-center gap-1">
                            <FileText size={12} /> View
                          </button>
                        )}
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </td>
        </tr>
      )}
    </>
  );
}

// ════════════════════════════════════════════════════════════════════════════
// KPI Summary Report — period-over-period comparison table
// ════════════════════════════════════════════════════════════════════════════

function KpiSummaryReport({ kpiBlocks }: { kpiBlocks: any }) {
  return (
    <div className="space-y-4">
      <div className="flex items-center gap-4 text-sm text-slate-500">
        <span>{kpiBlocks.prevFrom} → {kpiBlocks.prevTo} vs {kpiBlocks.prevTo}</span>
      </div>
      <div className="overflow-x-auto border border-slate-200 rounded-lg">
        <table className="w-full text-sm">
          <thead>
            <tr className="border-b border-slate-200 bg-slate-50 text-slate-500">
              <th className="text-left py-2 px-3">Metric</th>
              <th className="text-right py-2 px-3">Current Period</th>
              <th className="text-right py-2 px-3">Previous Period</th>
              <th className="text-right py-2 px-3">Change %</th>
            </tr>
          </thead>
          <tbody>
            {kpiBlocks.metrics.map((m: any) => {
              const isMoney = ['ADR', 'RevPAR', 'Room Revenue', 'Ancillary Revenue', 'Total Revenue'].includes(m.metric);
              const formatVal = (v: number) => isMoney ? formatIDR(v) : String(v);
              const trendIcon = m.change_pct > 0 ? <TrendingUp size={14} className="text-emerald-500" /> : m.change_pct < 0 ? <TrendingDown size={14} className="text-red-500" /> : <Minus size={14} className="text-slate-400" />;
              const trendColor = m.change_pct > 0 ? 'text-emerald-600' : m.change_pct < 0 ? 'text-red-600' : 'text-slate-500';
              return (
                <tr key={m.metric} className="border-b border-slate-100">
                  <td className="py-2 px-3 font-medium text-slate-700">{m.metric}</td>
                  <td className="text-right py-2 px-3 font-bold text-slate-800">{formatVal(m.current)}</td>
                  <td className="text-right py-2 px-3 text-slate-500">{formatVal(m.previous)}</td>
                  <td className="text-right py-2 px-3">
                    <span className={`inline-flex items-center gap-1 ${trendColor}`}>
                      {trendIcon}
                      {m.change_pct > 0 ? '+' : ''}{m.change_pct}%
                    </span>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
    </div>
  );
}

// ════════════════════════════════════════════════════════════════════════════
// Single KPI Report — Occupancy / ADR / RevPAR with period comparison
// ════════════════════════════════════════════════════════════════════════════

function SingleKpiReport({ reportKey, summary, t }: { reportKey: string; summary: any; t: (k: string) => string }) {
  const isMoney = reportKey === 'adr' || reportKey === 'revpar';
  const formatVal = (v: number) => isMoney ? formatIDR(v) : `${v}%`;
  const trendIcon = summary.change_pct > 0 ? <TrendingUp size={20} className="text-emerald-500" /> : summary.change_pct < 0 ? <TrendingDown size={20} className="text-red-500" /> : <Minus size={20} className="text-slate-400" />;
  const trendColor = summary.change_pct > 0 ? 'text-emerald-600' : summary.change_pct < 0 ? 'text-red-600' : 'text-slate-500';

  return (
    <div className="space-y-4">
      {/* Big number with comparison */}
      <div className="bg-slate-50 rounded-xl p-6 text-center">
        <p className="text-sm text-slate-500 mb-1">{t(`reports.${reportKey}`)}</p>
        <p className="text-4xl font-bold text-slate-900">{formatVal(summary.current)}</p>
        <div className={`inline-flex items-center gap-1.5 mt-2 text-sm font-medium ${trendColor}`}>
          {trendIcon}
          {summary.change_pct > 0 ? '+' : ''}{summary.change_pct}% vs previous period
        </div>
        <p className="text-xs text-slate-400 mt-1">
          Previous: {formatVal(summary.previous)} ({summary.prevFrom} to {summary.prevTo})
        </p>
      </div>

      {/* Supporting metrics */}
      <div className="grid grid-cols-2 md:grid-cols-4 gap-3">
        <div className="bg-slate-50 rounded-lg p-3">
          <p className="text-xs text-slate-500">{t('reports.room_nights_short')}</p>
          <p className="text-lg font-bold text-slate-800">{summary.roomNights}</p>
        </div>
        <div className="bg-slate-50 rounded-lg p-3">
          <p className="text-xs text-slate-500">{t('reports.available_room_nights')}</p>
          <p className="text-lg font-bold text-slate-800">{summary.availableRoomNights}</p>
        </div>
        <div className="bg-slate-50 rounded-lg p-3">
          <p className="text-xs text-slate-500">{t('reports.room_revenue')}</p>
          <p className="text-lg font-bold text-slate-800">{formatIDR(summary.roomRevenue)}</p>
        </div>
        <div className="bg-slate-50 rounded-lg p-3">
          <p className="text-xs text-slate-500">{t('reports.ancillary_revenue')}</p>
          <p className="text-lg font-bold text-slate-800">{formatIDR(summary.ancillaryRevenue)}</p>
        </div>
      </div>
      <div className="bg-slate-100 rounded-lg p-3 border border-slate-300">
        <p className="text-xs text-slate-500">{t('reports.total_revenue')}</p>
        <p className="text-xl font-bold text-slate-900">{formatIDR(summary.totalRevenue)}</p>
      </div>
    </div>
  );
}

// ════════════════════════════════════════════════════════════════════════════
// Housekeeping Report
// ════════════════════════════════════════════════════════════════════════════

function HousekeepingReport({ data, summary, t }: { data: any[]; summary: any; t: (k: string) => string }) {
  return (
    <div className="space-y-4">
      {/* Status distribution summary */}
      <div className="grid grid-cols-2 md:grid-cols-4 gap-3">
        <div className="bg-slate-50 rounded-lg p-3">
          <p className="text-xs text-slate-500">{t('reports.total_guests') === 'Total Guests' ? 'Total Rooms' : 'Total Kamar'}</p>
          <p className="text-lg font-bold text-slate-800">{summary.total_rooms}</p>
        </div>
        {summary.status_distribution?.map((s: any) => (
          <div key={s.metric} className="bg-slate-50 rounded-lg p-3">
            <p className="text-xs text-slate-500 capitalize">{s.metric}</p>
            <p className="text-lg font-bold text-slate-800">{s.value}</p>
          </div>
        ))}
      </div>

      {/* Room list */}
      {data.length > 0 && (
        <div className="overflow-x-auto border border-slate-200 rounded-lg">
          <table className="w-full text-sm">
            <thead>
              <tr className="border-b border-slate-200 bg-slate-50 text-slate-500">
                <th className="text-left py-2 px-3">Room</th>
                <th className="text-left py-2 px-3">Type</th>
                <th className="text-center py-2 px-3">Status</th>
                <th className="text-center py-2 px-3">Floor</th>
              </tr>
            </thead>
            <tbody>
              {data.map((room) => (
                <tr key={room.id} className="border-b border-slate-100">
                  <td className="py-2 px-3 font-medium text-slate-800">{room.room_number}</td>
                  <td className="py-2 px-3 text-slate-600">{room.room_type?.name || '-'}</td>
                  <td className="text-center py-2 px-3">
                    <Badge color={
                      room.status === 'available' ? 'green' :
                      room.status === 'occupied' ? 'red' :
                      room.status === 'dirty' ? 'amber' :
                      room.status === 'cleaning' ? 'teal' :
                      room.status === 'reserved' ? 'blue' : 'gray'
                    }>{room.status.replace(/_/g, ' ')}</Badge>
                  </td>
                  <td className="text-center py-2 px-3 text-slate-500">{room.floor}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}

// ════════════════════════════════════════════════════════════════════════════
// Folio Detail Modal (shared by Daily Income Report)
// ════════════════════════════════════════════════════════════════════════════

function FolioDetailModal({ reservationId, onClose }: { reservationId: string; onClose: () => void }) {
  const { t } = useI18n();
  const [loading, setLoading] = useState(true);
  const [folio, setFolio] = useState<Folio | null>(null);
  const [items, setItems] = useState<FolioItem[]>([]);
  const [reservation, setReservation] = useState<Reservation | null>(null);
  const [guest, setGuest] = useState<Guest | null>(null);
  const [room, setRoom] = useState<Room | null>(null);

  useEffect(() => {
    (async () => {
      setLoading(true);
      const { data: folioData } = await supabase
        .from('folios').select('*').eq('reservation_id', reservationId).maybeSingle();
      if (!folioData) { setLoading(false); return; }
      const f = folioData as Folio;
      setFolio(f);

      const { data: itemData } = await supabase
        .from('folio_items').select('*').eq('folio_id', f.id).order('created_at');
      setItems((itemData as FolioItem[]) || []);

      const { data: resData } = await supabase
        .from('reservations').select('*,primary_guest:guests(*),room:rooms(*)').eq('id', reservationId).maybeSingle();
      const res = resData as any;
      setReservation(res);
      setGuest(res?.primary_guest as Guest || null);
      setRoom(res?.room as Room || null);

      setLoading(false);
    })();
  }, [reservationId]);

  if (loading) return <Modal open onClose={onClose} title="Folio Details" size="lg"><LoadingPage /></Modal>;
  if (!folio) return <Modal open onClose={onClose} title="Folio Details" size="md"><p className="text-sm text-slate-500 text-center py-4">No folio found for this reservation.</p></Modal>;

  const charges = items.filter((i) => i.item_type === 'charge' && !i.voided && i.amount > 0);
  const payments = items.filter((i) => i.item_type === 'payment' && !i.voided);
  const discounts = items.filter((i) => i.item_type === 'discount' && !i.voided);
  const taxes = items.filter((i) => i.item_type === 'tax' && !i.voided);
  const totalCharges = charges.reduce((s, i) => s + i.amount, 0);
  const totalPayments = payments.reduce((s, i) => s + Math.abs(i.amount), 0);
  const totalDiscounts = discounts.reduce((s, i) => s + Math.abs(i.amount), 0);
  const totalTax = taxes.reduce((s, i) => s + i.amount, 0);
  const netBalance = totalCharges + totalTax - totalDiscounts - totalPayments;

  return (
    <Modal open onClose={onClose} title={`${t('folio.title')} — ${folio.folio_number}`} size="xl">
      <div className="space-y-4">
        <div className="grid grid-cols-2 md:grid-cols-4 gap-3 text-sm">
          <div><span className="text-slate-500">{t('common.guest')}:</span> <span className="font-medium text-slate-800 flex items-center gap-1"><UserIcon size={12} />{guest?.full_name || '-'}</span></div>
          <div><span className="text-slate-500">{t('common.room')}:</span> <span className="font-medium">{room?.room_number || '-'}</span></div>
          <div><span className="text-slate-500">{t('common.reservation')}:</span> <span className="font-medium text-blue-600">{reservation?.reservation_number || '-'}</span></div>
          <div><span className="text-slate-500">{t('common.status')}:</span> <Badge color={folio.status === 'open' ? 'blue' : 'gray'}>{folio.status}</Badge></div>
        </div>

        <div className="border border-slate-200 rounded-lg overflow-hidden">
          <table className="w-full text-sm">
            <thead>
              <tr className="border-b border-slate-200 bg-slate-50 text-slate-500">
                <th className="text-left py-2 px-3">{t('common.description')}</th>
                <th className="text-left py-2 px-3">{t('common.category')}</th>
                <th className="text-center py-2 px-3">Type</th>
                <th className="text-right py-2 px-3">{t('common.amount')}</th>
                <th className="text-left py-2 px-3">{t('common.created_at')}</th>
              </tr>
            </thead>
            <tbody>
              {items.map((item) => (
                <tr key={item.id} className={`border-b border-slate-100 ${item.voided ? 'opacity-40 line-through' : ''}`}>
                  <td className="py-2 px-3">{item.description}{item.is_post_stay && <span className="ml-2 text-xs text-amber-600 font-medium">POST-STAY</span>}</td>
                  <td className="py-2 px-3">{item.category || '-'}</td>
                  <td className="text-center py-2 px-3"><Badge color={item.item_type === 'charge' ? 'red' : item.item_type === 'payment' ? 'green' : 'gray'}>{item.item_type}</Badge></td>
                  <td className={`text-right py-2 px-3 font-medium ${item.amount > 0 ? 'text-red-600' : 'text-emerald-600'}`}>{item.amount > 0 ? '+' : ''}{formatIDR(item.amount)}</td>
                  <td className="py-2 px-3 text-xs text-slate-400">{formatDateTime(item.created_at)}</td>
                </tr>
              ))}
              {items.length === 0 && <tr><td colSpan={5} className="text-center py-4 text-slate-400">{t('common.no_data')}</td></tr>}
            </tbody>
            <tfoot>
              <tr className="bg-slate-50 font-bold">
                <td colSpan={3} className="py-2 px-3">{t('folio.total_charges')}</td>
                <td className="text-right py-2 px-3 text-red-600">{formatIDR(totalCharges + totalTax)}</td>
                <td></td>
              </tr>
              <tr className="bg-slate-50 font-bold">
                <td colSpan={3} className="py-2 px-3">{t('folio.total_payments')}</td>
                <td className="text-right py-2 px-3 text-emerald-600">{formatIDR(totalPayments)}</td>
                <td></td>
              </tr>
              <tr className="bg-slate-100 font-bold">
                <td colSpan={3} className="py-2 px-3">{t('folio.net_balance')}</td>
                <td className={`text-right py-2 px-3 ${netBalance > 0 ? 'text-red-600' : 'text-emerald-600'}`}>{formatIDR(Math.abs(netBalance))}</td>
                <td></td>
              </tr>
            </tfoot>
          </table>
        </div>
      </div>
    </Modal>
  );
}

// ════════════════════════════════════════════════════════════════════════════
// Shared components
// ════════════════════════════════════════════════════════════════════════════

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
    management: 'Management',
    daily_income: 'Daily Income',
    cashier: 'Cashier'
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
              <th key={f.key} className="text-left py-2 px-3">{f.label}</th>
            )}
          </tr>
        </thead>
        <tbody>
          {data.slice(0, 100).map((row, i) =>
            <tr key={i} className="border-b border-slate-100">
              {cols.map((f: any) => {
                const value = getValue(row, f.key);
                return (
                  <td key={f.key} className="py-2 px-3">
                    {f.type === 'money' && typeof value === 'number'
                      ? formatIDR(value)
                      : f.type === 'date' && value && value !== '-'
                      ? String(value).slice(0, 10)
                      : typeof value === 'boolean'
                      ? value ? 'Yes' : 'No'
                      : String(value)}
                  </td>
                );
              })}
            </tr>
          )}
        </tbody>
      </table>
      {data.length > 100 &&
        <p className="text-xs text-slate-400 p-2">Showing 100 of {data.length} rows</p>
      }
    </div>
  );
}
