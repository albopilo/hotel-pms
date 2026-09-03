import { useEffect, useState } from 'react';
import { supabase } from '@/lib/supabase';
import { formatIDR, formatDate, formatDateTime } from '@/lib/format';
import { Button } from '@/components/ui/Button';
import { X, Printer } from 'lucide-react';
import type { Branch, Guest, Reservation, Room, Folio, FolioItem, PaymentMethod } from '@/types/database';

interface Props {
  paymentId: string;
  onClose: () => void;
}

export function PaymentReceiptPrintPage({ paymentId, onClose }: Props) {
  const [payment, setPayment] = useState<any>(null);
  const [guest, setGuest] = useState<Guest | null>(null);
  const [branch, setBranch] = useState<Branch | null>(null);
  const [reservation, setReservation] = useState<Reservation | null>(null);
  const [room, setRoom] = useState<Room | null>(null);
  const [folio, setFolio] = useState<Folio | null>(null);
  const [folioItems, setFolioItems] = useState<FolioItem[]>([]);
  const [method, setMethod] = useState<PaymentMethod | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let cancelled = false;
    (async () => {
      try {
        const { data: pay } = await supabase.from('payments').select('*').eq('id', paymentId).maybeSingle();
        if (cancelled || !pay) return;
        setPayment(pay);

        const [guestRes, branchRes, resRes, folioRes, methodRes] = await Promise.all([
          pay.guest_id ? supabase.from('guests').select('*').eq('id', pay.guest_id).maybeSingle() : Promise.resolve({ data: null }),
          supabase.from('branches').select('*').eq('id', pay.branch_id).maybeSingle(),
          pay.reservation_id ? supabase.from('reservations').select('*, room:rooms(*)').eq('id', pay.reservation_id).maybeSingle() : Promise.resolve({ data: null }),
          pay.folio_id ? supabase.from('folios').select('*').eq('id', pay.folio_id).maybeSingle() : Promise.resolve({ data: null }),
          pay.payment_method_id ? supabase.from('payment_methods').select('*').eq('id', pay.payment_method_id).maybeSingle() : Promise.resolve({ data: null }),
        ]);

        setGuest(guestRes.data as Guest | null);
        setBranch(branchRes.data as Branch | null);
        const r = resRes.data as any;
        setReservation(r);
        setRoom(r?.room as Room | null);
        setFolio(folioRes.data as Folio | null);
        setMethod(methodRes.data as PaymentMethod | null);

        if (pay.folio_id) {
          const { data: items } = await supabase.from('folio_items').select('*').eq('folio_id', pay.folio_id).eq('voided', false).order('created_at');
          setFolioItems((items as FolioItem[]) || []);
        }

        setLoading(false);
      } catch {
        if (!cancelled) setLoading(false);
      }
    })();
    return () => { cancelled = true; };
  }, [paymentId]);

  if (loading) {
    return (
      <div className="fixed inset-0 z-[60] bg-white flex items-center justify-center">
        <p className="text-slate-500">Loading receipt...</p>
      </div>
    );
  }

  if (!payment) {
    return (
      <div className="fixed inset-0 z-[60] bg-white flex flex-col items-center justify-center gap-4">
        <p className="text-slate-500">Payment not found.</p>
        <Button variant="outline" onClick={onClose}>Close</Button>
      </div>
    );
  }

  const charges = folioItems.filter(i => i.item_type === 'charge' && i.amount > 0);
  const payments = folioItems.filter(i => i.item_type === 'payment' && !i.voided);
  const discounts = folioItems.filter(i => i.item_type === 'discount' && !i.voided);
  const taxes = folioItems.filter(i => i.item_type === 'tax' && !i.voided);
  const totalCharges = charges.reduce((s, i) => s + i.amount, 0);
  const totalPayments = payments.reduce((s, i) => s + Math.abs(i.amount), 0);
  const totalDiscounts = discounts.reduce((s, i) => s + Math.abs(i.amount), 0);
  const totalTax = taxes.reduce((s, i) => s + i.amount, 0);
  const balance = totalCharges + totalTax - totalDiscounts - totalPayments;

  return (
    <div className="fixed inset-0 z-[60] overflow-y-auto bg-slate-200 print:bg-white">
      <style>{`
        @page { size: A5; margin: 10mm; }
        @media print {
          body { background: white; }
          .no-print { display: none !important; }
          .receipt-shell { max-width: none !important; min-height: auto !important; box-shadow: none !important; }
        }
      `}</style>

      <div className="no-print sticky top-0 z-10 flex items-center justify-between border-b border-slate-200 bg-white px-4 py-3 shadow-sm">
        <h2 className="text-lg font-semibold text-slate-800">Payment Receipt — {payment.payment_number}</h2>
        <div className="flex items-center gap-2">
          <Button variant="outline" size="sm" onClick={() => window.print()}><Printer size={16} /> Print</Button>
          <Button variant="secondary" size="sm" onClick={onClose}><X size={16} /> Close</Button>
        </div>
      </div>

      <main className="receipt-shell mx-auto my-6 min-h-[500px] max-w-[480px] bg-white px-6 py-8 text-[13px] text-slate-900 shadow-xl print:my-0 print:px-0 print:py-0">
        <header className="text-center">
          <h1 className="text-xl font-bold">{branch?.name || 'Hotel'}</h1>
          {branch?.address && <p className="mt-0.5 text-xs text-slate-500">{branch.address}</p>}
          {branch?.phone && <p className="text-xs text-slate-500">Tel: {branch.phone}</p>}
          <h2 className="mt-3 text-sm font-semibold tracking-wide uppercase">Payment Receipt</h2>
          <p className="text-xs text-slate-500">{payment.payment_number}</p>
        </header>

        <section className="mt-5 space-y-1.5 text-sm">
          <Row label="Date" value={formatDateTime(payment.created_at)} />
          <Row label="Guest" value={guest?.full_name || '-'} />
          <Row label="Room" value={room?.room_number || '-'} />
          {reservation && <Row label="Reservation" value={reservation.reservation_number} />}
          {folio && <Row label="Folio" value={folio.folio_number} />}
        </section>

        <section className="mt-4 border-t border-slate-200 pt-3 space-y-1.5 text-sm">
          <Row label="Payment Method" value={method?.name || payment.payment_method_code || '-'} />
          {payment.payment_subtype && <Row label="Subtype" value={payment.payment_subtype} />}
          {payment.edc_terminal && <Row label="EDC Terminal" value={payment.edc_terminal} />}
          {payment.reference_number && <Row label="Reference No." value={payment.reference_number} />}
          {payment.approval_code && <Row label="Approval Code" value={payment.approval_code} />}
        </section>

        <section className="mt-4 border-t-2 border-slate-300 pt-3">
          <div className="flex items-center justify-between text-lg font-bold">
            <span>Amount Paid</span>
            <span className="text-emerald-700">{formatIDR(Number(payment.amount))}</span>
          </div>
        </section>

        <section className="mt-4 border-t border-slate-200 pt-3 space-y-1 text-sm">
          <Row label="Total Charges" value={formatIDR(totalCharges + totalTax)} />
          <Row label="Total Payments" value={formatIDR(totalPayments)} />
          <div className="flex items-center justify-between font-bold pt-1 border-t border-slate-200">
            <span>Balance</span>
            <span className={balance > 0 ? 'text-red-600' : 'text-emerald-600'}>{formatIDR(Math.abs(balance))}{balance > 0 ? ' due' : ' settled'}</span>
          </div>
        </section>

        {payment.notes && (
          <section className="mt-4 text-sm">
            <span className="font-medium text-slate-600">Notes: </span>
            <span>{payment.notes}</span>
          </section>
        )}

        <footer className="mt-8 text-center text-xs text-slate-500">
          <p>Thank you for your payment.</p>
          <p className="mt-1">Printed {formatDateTime(new Date().toISOString())}</p>
        </footer>
      </main>
    </div>
  );
}

function Row({ label, value }: { label: string; value: string }) {
  return (
    <div className="flex items-center justify-between">
      <span className="text-slate-500">{label}</span>
      <span className="font-medium text-slate-800">{value}</span>
    </div>
  );
}
