import { useState, useEffect, useCallback, useMemo } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { Card } from '@/components/ui/Card';
import { Modal } from '@/components/ui/Modal';
import { Badge, ResStatusBadge, InvoiceStatusBadge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState, ErrorState } from '@/components/ui/States';
import { Pagination } from '@/components/ui/Pagination';
import { formatIDR, formatDate } from '@/lib/format';
import { Archive, Search } from 'lucide-react';
import type { Reservation, Folio, Invoice } from '@/types/database';

const PAGE_SIZE = 20;

type ArchivedReservationRow = Reservation & {
  primary_guest?: { full_name: string } | null;
  room?: { room_number: string } | null;
};

type PaymentRow = {
  id: string;
  payment_number: string;
  amount: number;
  payment_method_code: string | null;
  business_date: string | null;
  voided: boolean;
};

function cleanSearchTerm(raw: string): string {
  return raw.replace(/[^\p{L}\p{N} .-]/gu, '').trim();
}

export function ArchivePage() {
  const { branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();
  const [rows, setRows] = useState<ArchivedReservationRow[]>([]);
  const [total, setTotal] = useState(0);
  const [loading, setLoading] = useState(true);
  const [loadFailed, setLoadFailed] = useState(false);
  const [searchInput, setSearchInput] = useState('');
  const [term, setTerm] = useState('');
  const [page, setPage] = useState(1);
  const [selected, setSelected] = useState<ArchivedReservationRow | null>(null);

  const branchIds = useMemo(
    () => (selectedBranchId ? [selectedBranchId] : branches.map((b) => b.id)),
    [selectedBranchId, branches],
  );

  useEffect(() => {
    const handle = setTimeout(() => {
      setTerm(cleanSearchTerm(searchInput));
      setPage(1);
    }, 300);
    return () => clearTimeout(handle);
  }, [searchInput]);

  const load = useCallback(async () => {
    if (branchIds.length === 0) {
      setLoading(false);
      return;
    }
    setLoading(true);
    setLoadFailed(false);
    try {
      let q = supabase
        .from('reservations')
        .select('*, primary_guest:guests(full_name), room:rooms(room_number)', { count: 'exact' })
        .in('branch_id', branchIds)
        .eq('is_archived', true);

      if (term) {
        const { data: guestData, error: guestError } = await supabase
          .from('guests')
          .select('id')
          .ilike('full_name', `%${term}%`)
          .limit(200);
        if (guestError) throw guestError;
        const conditions = [`reservation_number.ilike.*${term}*`];
        const guestIds = (guestData || []).map((g) => g.id);
        if (guestIds.length > 0) conditions.push(`primary_guest_id.in.(${guestIds.join(',')})`);
        q = q.or(conditions.join(','));
      }

      const from = (page - 1) * PAGE_SIZE;
      const { data, count, error } = await q
        .order('check_out_date', { ascending: false })
        .order('id')
        .range(from, from + PAGE_SIZE - 1);
      if (error) throw error;
      setRows((data as ArchivedReservationRow[]) || []);
      setTotal(count ?? 0);
    } catch {
      setLoadFailed(true);
      setRows([]);
      setTotal(0);
    } finally {
      setLoading(false);
    }
  }, [branchIds, term, page]);

  useEffect(() => {
    load();
  }, [load]);

  return (
    <div className="space-y-6">
      <div>
        <h1 className="text-2xl font-bold text-slate-900">{t('archive.title')}</h1>
        <p className="text-sm text-slate-500 mt-1">{t('archive.description')}</p>
      </div>

      <div className="relative max-w-md">
        <Search size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
        <input
          type="text"
          value={searchInput}
          onChange={(e) => setSearchInput(e.target.value)}
          placeholder={t('archive.search_placeholder')}
          className="w-full rounded-lg border border-slate-300 pl-10 pr-3 py-2 text-sm outline-none focus:ring-2 focus:ring-blue-500"
        />
      </div>

      {loading ? (
        <LoadingPage message={t('common.loading')} />
      ) : loadFailed ? (
        <ErrorState message={t('archive.error')} onRetry={load} />
      ) : rows.length === 0 ? (
        <EmptyState icon={<Archive size={48} />} title={t('archive.empty')} />
      ) : (
        <Card noPadding>
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 text-slate-500">
                  <th className="text-left py-3 px-4">{t('res.reservation_number')}</th>
                  <th className="text-left py-3 px-4">{t('common.guest')}</th>
                  <th className="text-left py-3 px-4">{t('common.room')}</th>
                  <th className="text-left py-3 px-4">{t('common.check_in')}</th>
                  <th className="text-left py-3 px-4">{t('common.check_out')}</th>
                  <th className="text-center py-3 px-4">{t('common.status')}</th>
                </tr>
              </thead>
              <tbody>
                {rows.map((r) => (
                  <tr
                    key={r.id}
                    className="border-b border-slate-100 hover:bg-slate-50 cursor-pointer"
                    onClick={() => setSelected(r)}
                  >
                    <td className="py-3 px-4 font-medium text-blue-600">{r.reservation_number}</td>
                    <td className="py-3 px-4">{r.primary_guest?.full_name || '-'}</td>
                    <td className="py-3 px-4">{r.room?.room_number || '-'}</td>
                    <td className="py-3 px-4">{formatDate(r.check_in_date)}</td>
                    <td className="py-3 px-4">{formatDate(r.check_out_date)}</td>
                    <td className="text-center py-3 px-4">
                      <ResStatusBadge status={r.status} label={t(`res.${r.status}`)} />
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
          <Pagination page={page} pageSize={PAGE_SIZE} total={total} onPageChange={setPage} />
        </Card>
      )}

      {selected && <ArchivedReservationDetail reservation={selected} onClose={() => setSelected(null)} />}
    </div>
  );
}

function ArchivedReservationDetail({ reservation, onClose }: { reservation: ArchivedReservationRow; onClose: () => void }) {
  const { t } = useI18n();
  const [folio, setFolio] = useState<Folio | null>(null);
  const [invoices, setInvoices] = useState<Invoice[]>([]);
  const [payments, setPayments] = useState<PaymentRow[]>([]);
  const [loading, setLoading] = useState(true);
  const [failed, setFailed] = useState(false);

  useEffect(() => {
    let cancelled = false;
    (async () => {
      const [f, inv, pay] = await Promise.all([
        supabase.from('folios').select('*').eq('reservation_id', reservation.id).maybeSingle(),
        supabase.from('invoices').select('*').eq('reservation_id', reservation.id),
        supabase
          .from('payments')
          .select('id, payment_number, amount, payment_method_code, business_date, voided')
          .eq('reservation_id', reservation.id)
          .order('created_at'),
      ]);
      if (cancelled) return;
      if (f.error || inv.error || pay.error) {
        setFailed(true);
      } else {
        setFolio((f.data as Folio) || null);
        setInvoices((inv.data as Invoice[]) || []);
        setPayments((pay.data as PaymentRow[]) || []);
      }
      setLoading(false);
    })();
    return () => {
      cancelled = true;
    };
  }, [reservation.id]);

  return (
    <Modal open onClose={onClose} title={reservation.reservation_number} size="lg" footer={
      <button onClick={onClose} className="px-4 py-2 text-sm rounded-lg border border-slate-300 text-slate-700 hover:bg-slate-50">
        {t('common.close')}
      </button>
    }>
      <div className="space-y-6">
        <dl className="grid grid-cols-2 gap-4 text-sm">
          <div>
            <dt className="text-slate-500">{t('common.guest')}</dt>
            <dd className="font-medium text-slate-900">{reservation.primary_guest?.full_name || '-'}</dd>
          </div>
          <div>
            <dt className="text-slate-500">{t('common.room')}</dt>
            <dd className="font-medium text-slate-900">{reservation.room?.room_number || '-'}</dd>
          </div>
          <div>
            <dt className="text-slate-500">{t('common.check_in')}</dt>
            <dd className="font-medium text-slate-900">{formatDate(reservation.check_in_date)}</dd>
          </div>
          <div>
            <dt className="text-slate-500">{t('common.check_out')}</dt>
            <dd className="font-medium text-slate-900">{formatDate(reservation.check_out_date)}</dd>
          </div>
          <div>
            <dt className="text-slate-500">{t('common.nights')}</dt>
            <dd className="font-medium text-slate-900">{reservation.num_nights}</dd>
          </div>
          <div>
            <dt className="text-slate-500">{t('common.status')}</dt>
            <dd><ResStatusBadge status={reservation.status} label={t(`res.${reservation.status}`)} /></dd>
          </div>
        </dl>

        {loading ? (
          <p className="text-sm text-slate-400">{t('common.loading')}</p>
        ) : failed ? (
          <p className="text-sm text-red-600">{t('archive.error')}</p>
        ) : (
          <>
            <section className="space-y-2">
              <h3 className="text-sm font-semibold text-slate-900">{t('nav.payments')}</h3>
              {folio ? (
                <dl className="grid grid-cols-2 gap-4 text-sm">
                  <div>
                    <dt className="text-slate-500">{t('folio.folio_number')}</dt>
                    <dd className="font-medium text-slate-900">{folio.folio_number}</dd>
                  </div>
                  <div>
                    <dt className="text-slate-500">{t('folio.total_charges')}</dt>
                    <dd className="font-medium text-slate-900">{formatIDR(folio.total_charges)}</dd>
                  </div>
                  <div>
                    <dt className="text-slate-500">{t('folio.total_payments')}</dt>
                    <dd className="font-medium text-slate-900">{formatIDR(folio.total_payments)}</dd>
                  </div>
                  <div>
                    <dt className="text-slate-500">{t('common.balance')}</dt>
                    <dd className="font-medium text-slate-900">{formatIDR(folio.balance)}</dd>
                  </div>
                </dl>
              ) : (
                <p className="text-sm text-slate-400">{t('common.no_data')}</p>
              )}
              {payments.length > 0 && (
                <ul className="divide-y divide-slate-100 text-sm">
                  {payments.map((p) => (
                    <li key={p.id} className="flex items-center justify-between py-2">
                      <span className="text-slate-700">
                        {p.payment_number} {p.business_date ? `- ${formatDate(p.business_date)}` : ''}
                        {p.voided && <Badge color="red">void</Badge>}
                      </span>
                      <span className="font-medium text-slate-900">{formatIDR(p.amount)}</span>
                    </li>
                  ))}
                </ul>
              )}
            </section>

            <section className="space-y-2">
              <h3 className="text-sm font-semibold text-slate-900">{t('nav.invoices')}</h3>
              {invoices.length === 0 ? (
                <p className="text-sm text-slate-400">{t('invoice.no_invoices')}</p>
              ) : (
                <ul className="divide-y divide-slate-100 text-sm">
                  {invoices.map((inv) => (
                    <li key={inv.id} className="flex items-center justify-between py-2">
                      <span className="flex items-center gap-2 text-slate-700">
                        {inv.invoice_number}
                        <InvoiceStatusBadge status={inv.status} label={inv.status} />
                      </span>
                      <span className="font-medium text-slate-900">{formatIDR(inv.total)}</span>
                    </li>
                  ))}
                </ul>
              )}
            </section>
          </>
        )}
      </div>
    </Modal>
  );
}
