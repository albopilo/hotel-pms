import { useEffect, useState } from 'react';
import { supabase } from '@/lib/supabase';
import { invoiceService } from '@/services/invoiceService';
import type { Invoice, InvoiceItem, Guest, Branch, BookingSource, RoomType, ReservationRoom } from '@/types/database';
import { formatIDR, formatDateTime, formatDate } from '@/lib/format';
import { Button } from '@/components/ui/Button';
import { X } from 'lucide-react';

interface Props {
  invoiceId: string;
  onClose: () => void;
}

export function InvoicePrintPage({ invoiceId, onClose }: Props) {
  const [invoice, setInvoice] = useState<Invoice | null>(null);
  const [items, setItems] = useState<InvoiceItem[]>([]);
  const [guest, setGuest] = useState<Guest | null>(null);
  const [branch, setBranch] = useState<Branch | null>(null);
  const [reservation, setReservation] = useState<any>(null);
  const [bookingSource, setBookingSource] = useState<BookingSource | null>(null);
  const [roomType, setRoomType] = useState<RoomType | null>(null);
  const [groupRooms, setGroupRooms] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let cancelled = false;
    (async () => {
      try {
        const detail = await invoiceService.getInvoiceDetail(invoiceId);
        if (cancelled) return;
        setInvoice(detail);
        setItems(detail.invoice_items || []);
        setGuest(detail.guests || null);
        setBranch(detail.branches || null);
        setReservation(detail.reservations || null);

        const res = detail.reservations;
        if (res?.booking_source_id) {
          const { data: bsData } = await supabase.from('booking_sources').select('*').eq('id', res.booking_source_id).maybeSingle();
          setBookingSource(bsData as BookingSource | null);
        }
        if (res?.room_type_id) {
          const { data: rtData } = await supabase.from('room_types').select('*').eq('id', res.room_type_id).maybeSingle();
          setRoomType(rtData as RoomType | null);
        }
        if (res?.is_group) {
          const { data: rrData } = await supabase.from('reservation_rooms')
            .select('*,room:rooms(*)').eq('reservation_id', res.id).eq('status','active').order('created_at');
          setGroupRooms(rrData || []);
        }

        setLoading(false);
      } catch {
        if (!cancelled) setLoading(false);
      }
    })();
    return () => { cancelled = true; };
  }, [invoiceId]);

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

  return (
    <div className="fixed inset-0 z-[60] bg-white overflow-y-auto">
      <style>{`
        @media print {
          body { background: white; }
          .no-print { display: none !important; }
        }
      `}</style>

      <div className="no-print sticky top-0 bg-white border-b border-slate-200 px-4 py-3 flex items-center justify-between z-10">
        <h2 className="text-lg font-semibold text-slate-800">Invoice Preview — {invoice.invoice_number}</h2>
        <div className="flex items-center gap-2">
          <Button variant="outline" size="sm" onClick={() => window.print()}>Print</Button>
          <Button variant="secondary" size="sm" onClick={onClose}><X size={16} /> Close</Button>
        </div>
      </div>

      <div className="max-w-3xl mx-auto p-10 bg-white text-black">
        <div className="border-b pb-5 mb-6">
          <h1 className="text-3xl font-bold">{branch?.name || 'Hotel'}</h1>
          <p>{branch?.address || ''}</p>
        </div>

        {reservation?.status === 'checked_in' && (
          <div className="mb-4">
            <h2 className="text-xl font-bold uppercase tracking-wide text-blue-700">CHECK IN INVOICE</h2>
          </div>
        )}

        {reservation?.status === 'checked_out' && (
          <div className="mb-4">
            <h2 className="text-xl font-bold uppercase tracking-wide text-emerald-700">FINAL CHECK OUT INVOICE</h2>
          </div>
        )}

        <div className="grid grid-cols-2 mb-8">
          <div>
            <h2 className="font-bold">Bill To</h2>
            <p>{guest?.full_name || '-'}</p>
          </div>
          <div className="text-right">
            <p>Invoice: <b>{invoice.invoice_number}</b></p>
            <p>{invoice.issued_at ? formatDateTime(invoice.issued_at) : '-'}</p>
          </div>
        </div>

        <div className="grid grid-cols-2 gap-4 mb-8 border border-slate-300 rounded-lg p-4 text-sm">
          <div>
            <p><span className="font-semibold">Check-in:</span> {reservation?.check_in_date ? formatDate(reservation.check_in_date) : '-'}</p>
            <p><span className="font-semibold">Check-out:</span> {reservation?.check_out_date ? formatDate(reservation.check_out_date) : '-'}</p>
            <p><span className="font-semibold">Nights:</span> {reservation?.num_nights || '-'}</p>
            <p><span className="font-semibold">Adults / Children:</span> {reservation ? `${reservation.adults} / ${reservation.children}` : '-'}</p>
          </div>
          <div>
            <p><span className="font-semibold">Room Number:</span> {groupRooms.length > 1 ? groupRooms.map((rr:any) => rr.room?.room_number).filter(Boolean).join(', ') : (reservation?.rooms?.room_number || '-')}</p>
            <p><span className="font-semibold">Room Type:</span> {roomType?.name || reservation?.room_types?.name || '-'}</p>
            {bookingSource && <p><span className="font-semibold">Booking Source:</span> {bookingSource.name}</p>}
            {reservation?.deposit > 0 && <p><span className="font-semibold">Deposit:</span> {formatIDR(reservation.deposit)}</p>}
          </div>
        </div>

        <table className="w-full border-collapse">
          <thead>
            <tr className="border-b">
              <th className="text-left py-2">Description</th>
              <th>Qty</th>
              <th className="text-right">Amount</th>
            </tr>
          </thead>
          <tbody>
            {items.map(item => (
              <tr key={item.id} className="border-b">
                <td className="py-2">{item.description}</td>
                <td className="text-center">{item.quantity}</td>
                <td className="text-right">{formatIDR(item.amount)}</td>
              </tr>
            ))}
          </tbody>
        </table>

        <div className="mt-8 text-right space-y-1">
          <p>Subtotal: {formatIDR(invoice.subtotal)}</p>
          {invoice.discount > 0 && <p className="text-red-600">Discount: -{formatIDR(invoice.discount)}</p>}
          <p>Tax: {formatIDR(invoice.tax)}</p>
          <h2 className="text-xl font-bold mt-2">Total: {formatIDR(invoice.total)}</h2>
          {invoice.amount_paid > 0 && <p className="text-emerald-600">Paid: {formatIDR(invoice.amount_paid)}</p>}
          <p className={`font-bold ${invoice.balance > 0 ? 'text-red-600' : 'text-emerald-600'}`}>
            {invoice.balance > 0 ? `Balance Due: ${formatIDR(invoice.balance)}` : 'Fully Paid'}
          </p>
        </div>

        <div className="mt-12 text-center text-xs">Thank you for staying with us.</div>
      </div>
    </div>
  );
}
