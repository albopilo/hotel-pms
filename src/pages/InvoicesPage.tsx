import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal, ConfirmModal } from '@/components/ui/Modal';
import { InvoiceStatusBadge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatIDR, formatDate, formatDateTime } from '@/lib/format';
import { Receipt, Search, Printer } from 'lucide-react';
import type { Invoice, InvoiceItem, Guest, Branch, Folio, Reservation } from '@/types/database';

export function InvoicesPage({ searchQuery }: { searchQuery?: string }) {
  const { user, branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();
  const [invoices, setInvoices] = useState<Invoice[]>([]);
  const [loading, setLoading] = useState(true);
  const [selected, setSelected] = useState<Invoice | null>(null);
  const [localSearch, setLocalSearch] = useState(searchQuery || '');

  const branchIds = selectedBranchId ? [selectedBranchId] : branches.map((b) => b.id);

  const load = useCallback(async () => {
    if (branchIds.length === 0) { setLoading(false); return; }
    setLoading(true);
    const { data } = await supabase.from('invoices').select('*').in('branch_id', branchIds).order('created_at', { ascending: false }).limit(100);
    setInvoices((data as Invoice[]) || []);
    setLoading(false);
  }, [branchIds]);

  useEffect(() => { load(); }, [load]);

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-slate-900">{t('nav.invoices')}</h1>

      <div className="relative max-w-md">
        <Search size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
        <input type="text" value={localSearch} onChange={(e) => setLocalSearch(e.target.value)} placeholder={t('common.search')} className="w-full rounded-lg border border-slate-300 pl-10 pr-3 py-2 text-sm outline-none focus:ring-2 focus:ring-blue-500" />
      </div>

      {invoices.length === 0 ? (
        <EmptyState icon={<Receipt size={48} />} title={t('invoice.no_invoices')} />
      ) : (
        <Card noPadding>
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 text-slate-500">
                  <th className="text-left py-3 px-4">{t('invoice.invoice_number')}</th>
                  <th className="text-left py-3 px-4">{t('common.guest')}</th>
                  <th className="text-right py-3 px-4">{t('common.total')}</th>
                  <th className="text-right py-3 px-4">{t('common.balance')}</th>
                  <th className="text-center py-3 px-4">{t('common.status')}</th>
                  <th className="text-right py-3 px-4">{t('common.actions')}</th>
                </tr>
              </thead>
              <tbody>
                {invoices.filter((inv) => !localSearch || inv.invoice_number.toLowerCase().includes(localSearch.toLowerCase())).map((inv) => (
                  <tr key={inv.id} className="border-b border-slate-100 hover:bg-slate-50 cursor-pointer" onClick={() => setSelected(inv)}>
                    <td className="py-3 px-4 font-medium text-blue-600">{inv.invoice_number}</td>
                    <td className="py-3 px-4">-</td>
                    <td className="text-right py-3 px-4">{formatIDR(inv.total)}</td>
                    <td className="text-right py-3 px-4">{formatIDR(inv.balance)}</td>
                    <td className="text-center py-3 px-4"><InvoiceStatusBadge status={inv.status} label={inv.status} /></td>
                    <td className="text-right py-3 px-4"><button className="text-blue-600 text-xs font-medium">{t('common.view')}</button></td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </Card>
      )}

      {selected && <InvoiceDetailModal invoice={selected} onClose={() => { setSelected(null); load(); }} />}
    </div>
  );
}

function InvoiceDetailModal({ invoice, onClose }: { invoice: Invoice; onClose: () => void }) {
  const { user, branches } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [items, setItems] = useState<InvoiceItem[]>([]);
  const [guest, setGuest] = useState<Guest | null>(null);
  const [branch, setBranch] = useState<Branch | null>(null);
  const [folio, setFolio] = useState<Folio | null>(null);
  const [reservation, setReservation] = useState<Reservation | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    (async () => {
      setLoading(true);
      const [{ data: ii }, { data: g }, { data: b }, { data: f }, { data: r }] = await Promise.all([
        supabase.from('invoice_items').select('*').eq('invoice_id', invoice.id).order('sort_order'),
        supabase.from('guests').select('*').eq('id', invoice.guest_id).maybeSingle(),
        supabase.from('branches').select('*').eq('id', invoice.branch_id).maybeSingle(),
        supabase.from('folios').select('*').eq('id', invoice.folio_id).maybeSingle(),
        supabase.from('reservations').select('*').eq('id', invoice.reservation_id).maybeSingle(),
      ]);
      setItems((ii as InvoiceItem[]) || []);
      setGuest(g as Guest);
      setBranch(b as Branch);
      setFolio(f as Folio);
      setReservation(r as Reservation);
      setLoading(false);
    })();
  }, [invoice]);

  const isFinalized = !!invoice.finalized_at;

  const handlePrint = () => {
    window.print();
  };

  if (loading) return <Modal open onClose={onClose} title={t('invoice.invoice_number')}><LoadingPage /></Modal>;

  return (
    <Modal open onClose={onClose} title={`${t('invoice.invoice_number')}: ${invoice.invoice_number}`} size="lg">
      <div className="space-y-4">
        {/* Invoice preview */}
        <div className="border border-slate-200 rounded-lg p-6 bg-white" id="invoice-print-area">
          <div className="flex items-start justify-between mb-6">
            <div>
              <h2 className="text-xl font-bold text-slate-900">{branch?.name || 'Hotel'}</h2>
              <p className="text-sm text-slate-500">{branch?.address}</p>
              <p className="text-sm text-slate-500">{branch?.phone} {branch?.email ? `· ${branch?.email}` : ''}</p>
              {branch?.tax_id && <p className="text-sm text-slate-500">Tax ID: {branch.tax_id}</p>}
            </div>
            <div className="text-right">
              <p className="font-bold text-lg">INVOICE</p>
              <p className="text-sm text-slate-500">{invoice.invoice_number}</p>
              <p className="text-sm text-slate-500">{formatDate(invoice.created_at)}</p>
              <InvoiceStatusBadge status={invoice.status} label={invoice.status} />
            </div>
          </div>

          <div className="grid grid-cols-2 gap-4 mb-4 text-sm">
            <div>
              <p className="text-slate-400 font-medium">Billed To:</p>
              <p className="font-medium">{guest?.full_name || '-'}</p>
              <p className="text-slate-500">{guest?.phone || ''}</p>
              <p className="text-slate-500">{guest?.email || ''}</p>
              {guest?.company && <p className="text-slate-500">{guest.company}</p>}
            </div>
            <div>
              <p className="text-slate-400 font-medium">Stay Details:</p>
              <p className="text-slate-500">Room: {reservation?.room_id || '-'}</p>
              <p className="text-slate-500">Check-in: {reservation ? formatDate(reservation.check_in_date) : '-'}</p>
              <p className="text-slate-500">Check-out: {reservation ? formatDate(reservation.check_out_date) : '-'}</p>
            </div>
          </div>

          <table className="w-full text-sm mb-4">
            <thead>
              <tr className="border-b border-slate-200 text-slate-500">
                <th className="text-left py-2">Description</th>
                <th className="text-center py-2">Qty</th>
                <th className="text-right py-2">Unit Price</th>
                <th className="text-right py-2">Amount</th>
              </tr>
            </thead>
            <tbody>
              {items.map((item) => (
                <tr key={item.id} className="border-b border-slate-100">
                  <td className="py-2">{item.description}</td>
                  <td className="text-center py-2">{item.quantity}</td>
                  <td className="text-right py-2">{formatIDR(item.unit_amount)}</td>
                  <td className="text-right py-2 font-medium">{formatIDR(item.amount)}</td>
                </tr>
              ))}
            </tbody>
          </table>

          <div className="flex justify-end">
            <div className="w-64 space-y-1 text-sm">
              <div className="flex justify-between"><span className="text-slate-500">Subtotal:</span><span>{formatIDR(invoice.subtotal)}</span></div>
              <div className="flex justify-between"><span className="text-slate-500">Discount:</span><span className="text-red-600">-{formatIDR(invoice.discount)}</span></div>
              <div className="flex justify-between"><span className="text-slate-500">Tax:</span><span>{formatIDR(invoice.tax)}</span></div>
              <div className="flex justify-between font-bold border-t border-slate-200 pt-1"><span>Total:</span><span>{formatIDR(invoice.total)}</span></div>
              <div className="flex justify-between"><span className="text-slate-500">Paid:</span><span className="text-emerald-600">{formatIDR(invoice.amount_paid)}</span></div>
              <div className="flex justify-between font-bold"><span>Balance:</span><span className={invoice.balance > 0 ? 'text-red-600' : 'text-emerald-600'}>{formatIDR(invoice.balance)}</span></div>
            </div>
          </div>

          <div className="mt-6 pt-4 border-t border-slate-100 text-xs text-slate-400">
            <p>Issued by: {user?.full_name}</p>
            <p>Issued at: {invoice.issued_at ? formatDateTime(invoice.issued_at) : '-'}</p>
            {invoice.notes && <p>Notes: {invoice.notes}</p>}
          </div>
        </div>

        {isFinalized && (
          <div className="bg-amber-50 border border-amber-200 rounded-lg p-3 text-sm text-amber-700">{t('invoice.finalized_warning')}</div>
        )}

        <div className="flex justify-end gap-2">
          <Button variant="outline" onClick={handlePrint}><Printer size={16} /> {t('common.print')}</Button>
        </div>
      </div>
    </Modal>
  );
}
