import { useEffect, useMemo, useState } from 'react';
import { supabase } from '@/lib/supabase';
import { invoiceService } from '@/services/invoiceService';
import type { Branch, BookingSource, FolioItem, Guest, Invoice, InvoiceItem, RoomType } from '@/types/database';
import { formatIDR, formatDate, formatDateTime } from '@/lib/format';
import { useI18n } from '@/lib/i18n';
import { Button } from '@/components/ui/Button';
import { X } from 'lucide-react';

interface Props {
  invoiceId: string;
  onClose: () => void;
}

interface GroupRoom {
  id: string;
  rate: number;
  num_nights: number;
  room?: { room_number: string } | null;
  room_type?: { name: string } | null;
}

interface PaymentSummary {
  label: string;
  amount: number;
  count: number;
}

function paymentLabel(item: FolioItem): string {
  const description = item.description.replace(/^payment:\s*/i, '').trim();
  const withoutSubtype = description.replace(/\s*\((debit|credit|qris)\)\s*$/i, '').trim();
  return withoutSubtype || item.category || 'Payment';
}

function isTaxItem(item: InvoiceItem): boolean {
  return item.category?.toLowerCase() === 'tax' || item.description.toLowerCase().includes('tax');
}

export function InvoicePrintPage({ invoiceId, onClose }: Props) {
  const { t } = useI18n();
  const [invoice, setInvoice] = useState<Invoice | null>(null);
  const [items, setItems] = useState<InvoiceItem[]>([]);
  const [payments, setPayments] = useState<FolioItem[]>([]);
  const [guest, setGuest] = useState<Guest | null>(null);
  const [branch, setBranch] = useState<Branch | null>(null);
  const [reservation, setReservation] = useState<any>(null);
  const [bookingSource, setBookingSource] = useState<BookingSource | null>(null);
  const [roomType, setRoomType] = useState<RoomType | null>(null);
  const [groupRooms, setGroupRooms] = useState<GroupRoom[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let cancelled = false;
    (async () => {
      try {
        const detail = await invoiceService.getInvoiceDetail(invoiceId);
        if (cancelled) return;

        setInvoice(detail as Invoice);
        setItems(detail.invoice_items || []);
        setGuest(detail.guests || null);
        setBranch(detail.branches || null);
        setReservation(detail.reservations || null);

        const res = detail.reservations;
        const [{ data: folioItems }, { data: bookingSourceData }, { data: roomTypeData }] = await Promise.all([
          detail.folio_id
            ? supabase.from('folio_items').select('*').eq('folio_id', detail.folio_id).eq('voided', false).eq('item_type', 'payment')
            : Promise.resolve({ data: [] as FolioItem[] }),
          res?.booking_source_id
            ? supabase.from('booking_sources').select('*').eq('id', res.booking_source_id).maybeSingle()
            : Promise.resolve({ data: null }),
          res?.room_type_id
            ? supabase.from('room_types').select('*').eq('id', res.room_type_id).maybeSingle()
            : Promise.resolve({ data: null }),
        ]);

        setPayments((folioItems as FolioItem[]) || []);
        setBookingSource(bookingSourceData as BookingSource | null);
        setRoomType(roomTypeData as RoomType | null);

        if (res?.is_group) {
          const { data: groupRoomData } = await supabase
            .from('reservation_rooms')
            .select('id, rate, num_nights, room:rooms(room_number), room_type:room_types(name)')
            .eq('reservation_id', res.id)
            .eq('status', 'active')
            .order('created_at');
          setGroupRooms((groupRoomData as GroupRoom[]) || []);
        }

        setLoading(false);
      } catch {
        if (!cancelled) setLoading(false);
      }
    })();
    return () => { cancelled = true; };
  }, [invoiceId]);

  const paymentSummaries = useMemo<PaymentSummary[]>(() => {
    const grouped = new Map<string, PaymentSummary>();
    payments.forEach((item) => {
      const label = paymentLabel(item);
      const existing = grouped.get(label);
      if (existing) {
        existing.amount += Math.abs(item.amount);
        existing.count += 1;
      } else {
        grouped.set(label, { label, amount: Math.abs(item.amount), count: 1 });
      }
    });
    return Array.from(grouped.values());
  }, [payments]);

  if (loading) {
    return (
      <div className="fixed inset-0 z-[60] bg-white flex items-center justify-center">
        <p className="text-slate-500">{t('invoice.loading_invoice')}</p>
      </div>
    );
  }

  if (!invoice) {
    return (
      <div className="fixed inset-0 z-[60] bg-white flex flex-col items-center justify-center gap-4">
        <p className="text-slate-500">{t('invoice.not_found')}</p>
        <Button variant="outline" onClick={onClose}>{t('common.close')}</Button>
      </div>
    );
  }

  const roomNumbers = groupRooms.length > 0
    ? groupRooms.map((room) => room.room?.room_number).filter(Boolean).join(', ')
    : reservation?.rooms?.room_number || '-';
  const invoiceTitle = reservation?.status === 'checked_out' ? t('invoice.final_checkout_invoice') : t('invoice.invoice_label');
  const transactionItems = items.filter((item) => !isTaxItem(item));
  const paymentTotal = paymentSummaries.reduce((sum, payment) => sum + payment.amount, 0);
  const subtotal = Math.max(0, invoice.subtotal - invoice.discount);
  const total = Math.max(0, subtotal - paymentTotal);

  return (
    <div className="fixed inset-0 z-[60] overflow-y-auto bg-slate-200 print:bg-white">
      <style>{`
        @page { size: A4; margin: 12mm; }
        @media print {
          body { background: white; }
          .no-print { display: none !important; }
          .invoice-shell { max-width: none !important; min-height: auto !important; box-shadow: none !important; }
          .invoice-section { break-inside: avoid; }
        }
      `}</style>

      <div className="no-print sticky top-0 z-10 flex items-center justify-between border-b border-slate-200 bg-white px-4 py-3 shadow-sm">
        <h2 className="text-lg font-semibold text-slate-800">{t('invoice.preview')} — {invoice.invoice_number}</h2>
        <div className="flex items-center gap-2">
          <Button variant="outline" size="sm" onClick={() => window.print()}>{t('common.print')}</Button>
          <Button variant="secondary" size="sm" onClick={onClose}><X size={16} /> {t('common.close')}</Button>
        </div>
      </div>

      <main className="invoice-shell mx-auto my-6 min-h-[1120px] max-w-[820px] bg-white px-8 py-10 text-[13px] text-slate-900 shadow-xl print:my-0 print:px-0 print:py-0">
        <header className="text-center">
          <h1 className="text-2xl font-bold">{branch?.name || 'Hotel'}</h1>
          <p className="mt-1 text-sm text-slate-500">{invoiceTitle}</p>
          <p className="mt-1 text-sm text-slate-500">{invoice.invoice_number}</p>
        </header>

        <section className="invoice-section mt-7 overflow-hidden rounded-sm border-2 border-red-500">
          <SectionTitle>{t('invoice.guest_information')}</SectionTitle>
          <div className="grid grid-cols-1 gap-x-8 gap-y-2 px-4 py-4 sm:grid-cols-2">
            <InfoRow label={t('invoice.booking_id')} value={reservation?.reservation_number || invoice.invoice_number} />
            <InfoRow label={t('common.name')} value={guest?.full_name || '-'} />
            <InfoRow label={t('common.id_type')} value={guest?.id_type || '-'} />
            <InfoRow label={t('common.id_number')} value={guest?.id_number || '-'} />
            <InfoRow label={t('common.email')} value={guest?.email || '-'} />
            <InfoRow label={t('common.phone_number')} value={guest?.phone || '-'} />
            <InfoRow label={t('common.nationality')} value={guest?.nationality || '-'} />
          </div>
        </section>

        <section className="invoice-section mt-5 overflow-hidden rounded-sm border-2 border-red-500">
          <SectionTitle>{t('invoice.booking_details')}</SectionTitle>
          <div className="px-4 py-4">
            <div className="mb-4">
              <p className="font-bold">{branch?.name || 'Hotel'}</p>
              {branch?.address && <p className="text-slate-600">{branch.address}</p>}
            </div>
            <div className="grid grid-cols-1 gap-x-8 gap-y-2 sm:grid-cols-2">
              <InfoRow label={t('common.room_type')} value={roomType?.name || reservation?.room_types?.name || '-'} />
              <InfoRow label={t('common.check_in')} value={reservation?.check_in_date ? formatDate(reservation.check_in_date) : '-'} />
              <InfoRow label={t('res.total_rooms')} value={groupRooms.length > 1 ? `${groupRooms.length} ${t('common.room')}s` : `1 ${t('common.room')}`} />
              <InfoRow label={t('common.check_out')} value={reservation?.check_out_date ? formatDate(reservation.check_out_date) : '-'} />
              <InfoRow label={t('rooms.room_number')} value={roomNumbers} />
              <InfoRow label={t('common.nights')} value={reservation?.num_nights ? String(reservation.num_nights) : '-'} />
              {bookingSource && <InfoRow label={t('common.booking_source')} value={bookingSource.name} />}
            </div>

            {groupRooms.length > 1 && (
              <div className="mt-4 overflow-hidden rounded border border-slate-200">
                <div className="border-b border-slate-200 bg-slate-50 px-3 py-2 text-xs font-bold">{t('invoice.group_room_breakdown')}</div>
                <table className="w-full text-xs">
                  <thead>
                    <tr className="border-b border-slate-200 text-left text-slate-500">
                      <th className="px-3 py-2">{t('common.room')}</th>
                      <th className="px-3 py-2">{t('common.room_type')}</th>
                      <th className="px-3 py-2 text-center">{t('common.nights')}</th>
                      <th className="px-3 py-2 text-right">{t('common.rate')}</th>
                    </tr>
                  </thead>
                  <tbody>
                    {groupRooms.map((room) => (
                      <tr key={room.id} className="border-b border-slate-100 last:border-0">
                        <td className="px-3 py-2">{room.room?.room_number || t('common.unassigned')}</td>
                        <td className="px-3 py-2">{room.room_type?.name || roomType?.name || '-'}</td>
                        <td className="px-3 py-2 text-center">{room.num_nights}</td>
                        <td className="px-3 py-2 text-right">{formatIDR(room.rate)}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </div>
        </section>

        <section className="invoice-section mt-5 overflow-hidden rounded-sm border-2 border-red-500">
          <SectionTitle>{t('invoice.booking_transaction')}</SectionTitle>
          <div className="px-2 py-3 sm:px-4">
            <div className="overflow-x-auto">
              <table className="w-full min-w-[560px] border-collapse text-left">
                <thead>
                  <tr className="border-b border-slate-300 text-xs font-bold">
                    <th className="px-2 py-2">{t('common.date')}</th>
                    <th className="px-2 py-2">{t('common.items')}</th>
                    <th className="px-2 py-2">{t('common.description')}</th>
                    <th className="px-2 py-2 text-right">{t('common.total')}</th>
                  </tr>
                </thead>
                <tbody>
                  {transactionItems.map((item) => (
                    <tr key={item.id} className="border-b border-slate-200">
                      <td className="whitespace-nowrap px-2 py-2">{formatDate(item.created_at)}</td>
                      <td className="px-2 py-2">{item.category || item.description}</td>
                      <td className="px-2 py-2">{item.description}</td>
                      <td className="whitespace-nowrap px-2 py-2 text-right">{formatIDR(Math.abs(item.amount))}</td>
                    </tr>
                  ))}
                  {paymentSummaries.map((payment) => (
                    <tr key={`payment-${payment.label}`} className="border-b border-slate-200">
                      <td className="whitespace-nowrap px-2 py-2">-</td>
                      <td className="px-2 py-2">{t('common.payment')}</td>
                      <td className="px-2 py-2">{payment.label}{payment.count > 1 ? ` (${payment.count} ${t('invoice.transactions')})` : ''}</td>
                      <td className="whitespace-nowrap px-2 py-2 text-right">{formatIDR(payment.amount)}</td>
                    </tr>
                  ))}
                  {transactionItems.length === 0 && paymentSummaries.length === 0 && (
                    <tr><td colSpan={4} className="px-2 py-6 text-center text-slate-400">{t('invoice.no_transaction_items')}</td></tr>
                  )}
                </tbody>
              </table>
            </div>

            <div className="mt-5 flex justify-end">
              <div className="w-full max-w-xs space-y-2 text-sm">
                <SummaryRow label={t('invoice.subtotal')} value={formatIDR(subtotal)} />
                <SummaryRow label={t('common.payments')} value={formatIDR(paymentTotal)} tone="positive" />
                <div className="flex items-center justify-between border-t border-slate-300 pt-3 text-base font-bold">
                  <span>{t('common.total')}</span><span>{formatIDR(total)}</span>
                </div>
              </div>
            </div>
          </div>
        </section>

        <footer className="mt-10 text-center text-xs text-slate-500">
          <p>{t('invoice.issued')} {invoice.issued_at ? formatDateTime(invoice.issued_at) : '-'}</p>
          <p className="mt-2 font-medium text-slate-700">{t('invoice.thank_you')}</p>
        </footer>
      </main>
    </div>
  );
}

function SectionTitle({ children }: { children: string }) {
  return <h2 className="border-b border-red-200 px-4 py-2 text-center text-sm font-bold">{children}</h2>;
}

function InfoRow({ label, value }: { label: string; value: string }) {
  return (
    <div className="grid grid-cols-[120px_1fr] gap-2">
      <span className="font-medium text-slate-600">{label}</span>
      <span className="break-words"><span className="mr-2">:</span>{value}</span>
    </div>
  );
}

function SummaryRow({ label, value, tone }: { label: string; value: string; tone?: 'negative' | 'positive' }) {
  return (
    <div className={`flex items-center justify-between ${tone === 'negative' ? 'text-red-600' : tone === 'positive' ? 'text-emerald-600' : ''}`}>
      <span>{label}</span><span className="font-medium">{value}</span>
    </div>
  );
}
