import { useEffect, useMemo, useState } from 'react';
import { supabase } from '@/lib/supabase';
import { invoiceService } from '@/services/invoiceService';
import type { Branch, BookingSource, FolioItem, Guest, Invoice, InvoiceItem, RoomType } from '@/types/database';
import { formatIDR, formatDate, formatDateTime } from '@/lib/format';
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
        <p className="text-slate-500">Loading invoice...</p>
      </div>
    );
  }

  if (!invoice) {
    return (
      <div className="fixed inset-0 z-[60] bg-white flex flex-col items-center justify-center gap-4">
        <p className="text-slate-500">Invoice not found.</p>
        <Button variant="outline" onClick={onClose}>Close</Button>
      </div>
    );
  }

  const roomNumbers = groupRooms.length > 0
    ? groupRooms.map((room) => room.room?.room_number).filter(Boolean).join(', ')
    : reservation?.rooms?.room_number || '-';
  const invoiceTitle = reservation?.status === 'checked_out' ? 'Final Check-out Invoice' : 'Invoice';
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
        <h2 className="text-lg font-semibold text-slate-800">Invoice Preview — {invoice.invoice_number}</h2>
        <div className="flex items-center gap-2">
          <Button variant="outline" size="sm" onClick={() => window.print()}>Print</Button>
          <Button variant="secondary" size="sm" onClick={onClose}><X size={16} /> Close</Button>
        </div>
      </div>

      <main className="invoice-shell mx-auto my-6 min-h-[1120px] max-w-[820px] bg-white px-8 py-10 text-[13px] text-slate-900 shadow-xl print:my-0 print:px-0 print:py-0">
        <header className="text-center">
          <h1 className="text-2xl font-bold">{branch?.name || 'Hotel'}</h1>
          <p className="mt-1 text-sm text-slate-500">{invoiceTitle}</p>
          <p className="mt-1 text-sm text-slate-500">{invoice.invoice_number}</p>
        </header>

        <section className="invoice-section mt-7 overflow-hidden rounded-sm border-2 border-red-500">
          <SectionTitle>Guest Information</SectionTitle>
          <div className="grid grid-cols-1 gap-x-8 gap-y-2 px-4 py-4 sm:grid-cols-2">
            <InfoRow label="Booking ID" value={reservation?.reservation_number || invoice.invoice_number} />
            <InfoRow label="Name" value={guest?.full_name || '-'} />
            <InfoRow label="ID Type" value={guest?.id_type || '-'} />
            <InfoRow label="ID Number" value={guest?.id_number || '-'} />
            <InfoRow label="Email" value={guest?.email || '-'} />
            <InfoRow label="Phone Number" value={guest?.phone || '-'} />
            <InfoRow label="Nationality" value={guest?.nationality || '-'} />
          </div>
        </section>

        <section className="invoice-section mt-5 overflow-hidden rounded-sm border-2 border-red-500">
          <SectionTitle>Booking Details</SectionTitle>
          <div className="px-4 py-4">
            <div className="mb-4">
              <p className="font-bold">{branch?.name || 'Hotel'}</p>
              {branch?.address && <p className="text-slate-600">{branch.address}</p>}
            </div>
            <div className="grid grid-cols-1 gap-x-8 gap-y-2 sm:grid-cols-2">
              <InfoRow label="Room Type" value={roomType?.name || reservation?.room_types?.name || '-'} />
              <InfoRow label="Check In Date" value={reservation?.check_in_date ? formatDate(reservation.check_in_date) : '-'} />
              <InfoRow label="Total Rooms" value={groupRooms.length > 1 ? `${groupRooms.length} Rooms` : '1 Room'} />
              <InfoRow label="Check Out Date" value={reservation?.check_out_date ? formatDate(reservation.check_out_date) : '-'} />
              <InfoRow label="Room Number" value={roomNumbers} />
              <InfoRow label="Nights" value={reservation?.num_nights ? String(reservation.num_nights) : '-'} />
              {bookingSource && <InfoRow label="Booking Source" value={bookingSource.name} />}
            </div>

            {groupRooms.length > 1 && (
              <div className="mt-4 overflow-hidden rounded border border-slate-200">
                <div className="border-b border-slate-200 bg-slate-50 px-3 py-2 text-xs font-bold">Group Room Breakdown</div>
                <table className="w-full text-xs">
                  <thead>
                    <tr className="border-b border-slate-200 text-left text-slate-500">
                      <th className="px-3 py-2">Room</th>
                      <th className="px-3 py-2">Room Type</th>
                      <th className="px-3 py-2 text-center">Nights</th>
                      <th className="px-3 py-2 text-right">Rate</th>
                    </tr>
                  </thead>
                  <tbody>
                    {groupRooms.map((room) => (
                      <tr key={room.id} className="border-b border-slate-100 last:border-0">
                        <td className="px-3 py-2">{room.room?.room_number || 'Unassigned'}</td>
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
          <SectionTitle>Booking Transaction</SectionTitle>
          <div className="px-2 py-3 sm:px-4">
            <div className="overflow-x-auto">
              <table className="w-full min-w-[560px] border-collapse text-left">
                <thead>
                  <tr className="border-b border-slate-300 text-xs font-bold">
                    <th className="px-2 py-2">Date</th>
                    <th className="px-2 py-2">Items</th>
                    <th className="px-2 py-2">Description</th>
                    <th className="px-2 py-2 text-right">Total</th>
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
                      <td className="px-2 py-2">Payment</td>
                      <td className="px-2 py-2">{payment.label}{payment.count > 1 ? ` (${payment.count} transactions)` : ''}</td>
                      <td className="whitespace-nowrap px-2 py-2 text-right">{formatIDR(payment.amount)}</td>
                    </tr>
                  ))}
                  {transactionItems.length === 0 && paymentSummaries.length === 0 && (
                    <tr><td colSpan={4} className="px-2 py-6 text-center text-slate-400">No transaction items</td></tr>
                  )}
                </tbody>
              </table>
            </div>

            <div className="mt-5 flex justify-end">
              <div className="w-full max-w-xs space-y-2 text-sm">
                <SummaryRow label="Subtotal" value={formatIDR(subtotal)} />
                <SummaryRow label="Payments" value={formatIDR(paymentTotal)} tone="positive" />
                <div className="flex items-center justify-between border-t border-slate-300 pt-3 text-base font-bold">
                  <span>Total</span><span>{formatIDR(total)}</span>
                </div>
              </div>
            </div>
          </div>
        </section>

        <footer className="mt-10 text-center text-xs text-slate-500">
          <p>Issued {invoice.issued_at ? formatDateTime(invoice.issued_at) : '-'}</p>
          <p className="mt-2 font-medium text-slate-700">Thank you for staying with us.</p>
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
