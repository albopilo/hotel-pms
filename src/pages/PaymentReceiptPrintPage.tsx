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

interface PaymentRow {
  payment_number: string;
  amount: number;
  method_name: string;
  method_code: string;
  subtype: string | null;
  edc_terminal: string | null;
  reference_number: string | null;
  approval_code: string | null;
  created_at: string;
  notes: string | null;
}

export function PaymentReceiptPrintPage({ paymentId, onClose }: Props) {
  const [payment, setPayment] = useState<any>(null);
  const [allPayments, setAllPayments] = useState<PaymentRow[]>([]);
  const [guest, setGuest] = useState<Guest | null>(null);
  const [branch, setBranch] = useState<Branch | null>(null);
  const [reservation, setReservation] = useState<Reservation | null>(null);
  const [room, setRoom] = useState<Room | null>(null);
  const [folio, setFolio] = useState<Folio | null>(null);
  const [folioItems, setFolioItems] = useState<FolioItem[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let cancelled = false;
    (async () => {
      try {
        const { data: pay } = await supabase.from('payments').select('*').eq('id', paymentId).maybeSingle();
        if (cancelled || !pay) return;
        setPayment(pay);

        const [guestRes, branchRes, resRes, folioRes] = await Promise.all([
          pay.guest_id ? supabase.from('guests').select('*').eq('id', pay.guest_id).maybeSingle() : Promise.resolve({ data: null }),
          supabase.from('branches').select('*').eq('id', pay.branch_id).maybeSingle(),
          pay.reservation_id ? supabase.from('reservations').select('*, room:rooms(*)').eq('id', pay.reservation_id).maybeSingle() : Promise.resolve({ data: null }),
          pay.folio_id ? supabase.from('folios').select('*').eq('id', pay.folio_id).maybeSingle() : Promise.resolve({ data: null }),
        ]);

        setGuest(guestRes.data as Guest | null);
        setBranch(branchRes.data as Branch | null);
        const r = resRes.data as any;
        setReservation(r);
        setRoom(r?.room as Room | null);
        setFolio(folioRes.data as Folio | null);

        // Load ALL payments for this folio (not just the single one)
        const folioId = pay.folio_id;
        if (folioId) {
          const { data: paymentsData } = await supabase
            .from('payments')
            .select('*, payment_method:payment_methods(name, code)')
            .eq('folio_id', folioId)
            .eq('voided', false)
            .order('created_at');

          const rows: PaymentRow[] = ((paymentsData as any[]) || []).map((p) => ({
            payment_number: p.payment_number,
            amount: Number(p.amount),
            method_name: p.payment_method?.name || p.payment_method_code || '-',
            method_code: p.payment_method?.code || p.payment_method_code || '-',
            subtype: p.payment_subtype || null,
            edc_terminal: p.edc_terminal || null,
            reference_number: p.reference_number || null,
            approval_code: p.approval_code || null,
            created_at: p.created_at,
            notes: p.notes || null,
          }));
          setAllPayments(rows);

          const { data: items } = await supabase.from('folio_items').select('*').eq('folio_id', folioId).eq('voided', false).order('created_at');
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
  const discounts = folioItems.filter(i => i.item_type === 'discount' && !i.voided);
  const taxes = folioItems.filter(i => i.item_type === 'tax' && !i.voided);
  const totalCharges = charges.reduce((s, i) => s + i.amount, 0);
  const totalPayments = allPayments.reduce((s, p) => s + p.amount, 0);
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
        <h2 className="text-lg font-semibold text-slate-800">Payment Receipt — {folio?.folio_number || payment.payment_number}</h2>
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
          {folio && <p className="text-xs text-slate-500">{folio.folio_number}</p>}
        </header>

        <section className="mt-5 space-y-1.5 text-sm">
          <Row label="Date" value={formatDateTime(new Date().toISOString())} />
          <Row label="Guest" value={guest?.full_name || '-'} />
          <Row label="Room" value={room?.room_number || '-'} />
          {reservation && <Row label="Reservation" value={reservation.reservation_number} />}
          {reservation && <Row label="Stay" value={`${formatDate(reservation.check_in_date)} — ${formatDate(reservation.check_out_date)}`} />}
        </section>

        {/* Charges table */}
        <section className="mt-4 border-t border-slate-200 pt-3">
          <p className="mb-2 text-xs font-bold uppercase text-slate-500">Charges</p>
          <table className="w-full text-xs">
            <thead>
              <tr className="border-b border-slate-200 text-slate-500">
                <th className="text-left py-1.5 pr-2">Date</th>
                <th className="text-left py-1.5 pr-2">Description</th>
                <th className="text-right py-1.5">Amount</th>
              </tr>
            </thead>
            <tbody>
              {charges.length === 0 ? (
                <tr><td colSpan={3} className="py-2 text-center text-slate-400">No charges</td></tr>
              ) : charges.map((c) => (
                <tr key={c.id} className="border-b border-slate-100">
                  <td className="py-1.5 pr-2 whitespace-nowrap">{formatDate(c.created_at)}</td>
                  <td className="py-1.5 pr-2">{c.description}</td>
                  <td className="py-1.5 text-right font-medium">{formatIDR(c.amount)}</td>
                </tr>
              ))}
              {totalTax > 0 && (
                <tr className="border-b border-slate-100">
                  <td className="py-1.5 pr-2"></td>
                  <td className="py-1.5 pr-2">Tax</td>
                  <td className="py-1.5 text-right font-medium">{formatIDR(totalTax)}</td>
                </tr>
              )}
              {totalDiscounts > 0 && (
                <tr className="border-b border-slate-100">
                  <td className="py-1.5 pr-2"></td>
                  <td className="py-1.5 pr-2 text-red-600">Discount</td>
                  <td className="py-1.5 text-right font-medium text-red-600">-{formatIDR(totalDiscounts)}</td>
                </tr>
              )}
            </tbody>
            {charges.length > 0 && (
              <tfoot>
                <tr className="border-t border-slate-300 font-bold">
                  <td colSpan={2} className="py-2">Total Charges</td>
                  <td className="py-2 text-right">{formatIDR(totalCharges + totalTax - totalDiscounts)}</td>
                </tr>
              </tfoot>
            )}
          </table>
        </section>

        {/* All payments table */}
        <section className="mt-4 border-t border-slate-200 pt-3">
          <p className="mb-2 text-xs font-bold uppercase text-slate-500">Payments</p>
          <table className="w-full text-xs">
            <thead>
              <tr className="border-b border-slate-200 text-slate-500">
                <th className="text-left py-1.5 pr-2">Date</th>
                <th className="text-left py-1.5 pr-2">Method</th>
                <th className="text-left py-1.5 pr-2">Ref No.</th>
                <th className="text-right py-1.5">Amount</th>
              </tr>
            </thead>
            <tbody>
              {allPayments.length === 0 ? (
                <tr><td colSpan={4} className="py-2 text-center text-slate-400">No payments</td></tr>
              ) : allPayments.map((p) => (
                <tr key={p.payment_number} className="border-b border-slate-100">
                  <td className="py-1.5 pr-2 whitespace-nowrap">{formatDate(p.created_at)}</td>
                  <td className="py-1.5 pr-2">
                    {p.method_name}
                    {p.subtype && <span className="text-slate-400"> ({p.subtype})</span>}
                  </td>
                  <td className="py-1.5 pr-2 text-slate-500">{p.reference_number || '-'}</td>
                  <td className="py-1.5 text-right font-medium text-emerald-700">{formatIDR(p.amount)}</td>
                </tr>
              ))}
            </tbody>
            {allPayments.length > 0 && (
              <tfoot>
                <tr className="border-t border-slate-300 font-bold">
                  <td colSpan={3} className="py-2">Total Paid</td>
                  <td className="py-2 text-right text-emerald-700">{formatIDR(totalPayments)}</td>
                </tr>
              </tfoot>
            )}
          </table>
        </section>

        {/* Balance summary */}
        <section className="mt-4 border-t-2 border-slate-300 pt-3">
          <div className="flex items-center justify-between text-base font-bold">
            <span>Balance</span>
            <span className={balance > 0 ? 'text-red-600' : 'text-emerald-600'}>{formatIDR(Math.abs(balance))}{balance > 0 ? ' due' : ' settled'}</span>
          </div>
        </section>

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
