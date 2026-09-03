import { useEffect, useState } from 'react';
import { supabase } from '@/lib/supabase';
import { formatIDR, formatDate, formatDateTime } from '@/lib/format';
import { Button } from '@/components/ui/Button';
import { X, Printer } from 'lucide-react';
import type { Branch, Guest, Reservation, Room, Folio, FolioItem } from '@/types/database';

interface Props {
  folioId: string;
  title?: string;
  onClose: () => void;
}

export function ChargeSummaryPrintPage({ folioId, title = 'Charge Summary', onClose }: Props) {
  const [folio, setFolio] = useState<Folio | null>(null);
  const [items, setItems] = useState<FolioItem[]>([]);
  const [guest, setGuest] = useState<Guest | null>(null);
  const [branch, setBranch] = useState<Branch | null>(null);
  const [reservation, setReservation] = useState<Reservation | null>(null);
  const [room, setRoom] = useState<Room | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let cancelled = false;
    (async () => {
      try {
        const { data: f } = await supabase.from('folios').select('*').eq('id', folioId).maybeSingle();
        if (cancelled || !f) return;
        setFolio(f as Folio);

        const [itemsRes, guestRes, branchRes, resRes] = await Promise.all([
          supabase.from('folio_items').select('*').eq('folio_id', folioId).eq('voided', false).order('created_at'),
          (f as Folio).guest_id ? supabase.from('guests').select('*').eq('id', (f as Folio).guest_id!).maybeSingle() : Promise.resolve({ data: null }),
          supabase.from('branches').select('*').eq('id', (f as Folio).branch_id).maybeSingle(),
          (f as Folio).reservation_id ? supabase.from('reservations').select('*, room:rooms(*)').eq('id', (f as Folio).reservation_id!).maybeSingle() : Promise.resolve({ data: null }),
        ]);

        setItems((itemsRes.data as FolioItem[]) || []);
        setGuest(guestRes.data as Guest | null);
        setBranch(branchRes.data as Branch | null);
        const r = resRes.data as any;
        setReservation(r);
        setRoom(r?.room as Room | null);

        setLoading(false);
      } catch {
        if (!cancelled) setLoading(false);
      }
    })();
    return () => { cancelled = true; };
  }, [folioId]);

  if (loading) {
    return (
      <div className="fixed inset-0 z-[60] bg-white flex items-center justify-center">
        <p className="text-slate-500">Loading charge summary...</p>
      </div>
    );
  }

  if (!folio) {
    return (
      <div className="fixed inset-0 z-[60] bg-white flex flex-col items-center justify-center gap-4">
        <p className="text-slate-500">Folio not found.</p>
        <Button variant="outline" onClick={onClose}>Close</Button>
      </div>
    );
  }

  const charges = items.filter(i => i.item_type === 'charge' && i.amount > 0);
  const payments = items.filter(i => i.item_type === 'payment' && !i.voided);
  const discounts = items.filter(i => i.item_type === 'discount' && !i.voided);
  const taxes = items.filter(i => i.item_type === 'tax' && !i.voided);
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
          .summary-shell { max-width: none !important; min-height: auto !important; box-shadow: none !important; }
        }
      `}</style>

      <div className="no-print sticky top-0 z-10 flex items-center justify-between border-b border-slate-200 bg-white px-4 py-3 shadow-sm">
        <h2 className="text-lg font-semibold text-slate-800">{title} — {folio.folio_number}</h2>
        <div className="flex items-center gap-2">
          <Button variant="outline" size="sm" onClick={() => window.print()}><Printer size={16} /> Print</Button>
          <Button variant="secondary" size="sm" onClick={onClose}><X size={16} /> Close</Button>
        </div>
      </div>

      <main className="summary-shell mx-auto my-6 min-h-[500px] max-w-[480px] bg-white px-6 py-8 text-[13px] text-slate-900 shadow-xl print:my-0 print:px-0 print:py-0">
        <header className="text-center">
          <h1 className="text-xl font-bold">{branch?.name || 'Hotel'}</h1>
          {branch?.address && <p className="mt-0.5 text-xs text-slate-500">{branch.address}</p>}
          <h2 className="mt-3 text-sm font-semibold tracking-wide uppercase">{title}</h2>
          <p className="text-xs text-slate-500">{folio.folio_number}</p>
        </header>

        <section className="mt-5 space-y-1.5 text-sm">
          <Row label="Date" value={formatDateTime(new Date().toISOString())} />
          <Row label="Guest" value={guest?.full_name || '-'} />
          <Row label="Room" value={room?.room_number || '-'} />
          {reservation && <Row label="Reservation" value={reservation.reservation_number} />}
          {reservation && <Row label="Stay" value={`${formatDate(reservation.check_in_date)} — ${formatDate(reservation.check_out_date)}`} />}
        </section>

        <section className="mt-4 border-t border-slate-200 pt-3">
          <p className="mb-2 text-xs font-bold uppercase text-slate-500">Charges</p>
          <table className="w-full text-xs">
            <tbody>
              {charges.map(item => (
                <tr key={item.id} className="border-b border-slate-100">
                  <td className="py-1.5 pr-2">{item.description}</td>
                  <td className="py-1.5 text-right font-medium">{formatIDR(item.amount)}</td>
                </tr>
              ))}
              {charges.length === 0 && (
                <tr><td className="py-2 text-center text-slate-400">No charges</td></tr>
              )}
            </tbody>
          </table>
        </section>

        <section className="mt-3 border-t border-slate-200 pt-3 space-y-1 text-sm">
          <Row label="Total Charges" value={formatIDR(totalCharges)} />
          {totalDiscounts > 0 && <Row label="Discounts" value={`-${formatIDR(totalDiscounts)}`} />}
          {totalTax > 0 && <Row label="Tax" value={formatIDR(totalTax)} />}
          <Row label="Total Paid" value={formatIDR(totalPayments)} />
          <div className="flex items-center justify-between font-bold pt-1 border-t border-slate-200">
            <span>Balance</span>
            <span className={balance > 0 ? 'text-red-600' : 'text-emerald-600'}>{formatIDR(Math.abs(balance))}{balance > 0 ? ' due' : ' settled'}</span>
          </div>
        </section>

        <footer className="mt-8 text-center text-xs text-slate-500">
          <p>Printed {formatDateTime(new Date().toISOString())}</p>
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
