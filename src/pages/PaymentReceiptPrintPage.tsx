import { useEffect, useState } from 'react';
import { supabase } from '@/lib/supabase';
import { formatIDR, formatDate, formatDateTime } from '@/lib/format';
import { useI18n } from '@/lib/i18n';
import { Button } from '@/components/ui/Button';
import { X, Printer } from 'lucide-react';
import type { Branch, Guest, Reservation, Room, Folio, FolioItem } from '@/types/database';

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
  const { t } = useI18n();
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
        <p className="text-slate-500">{t('receipt.loading_receipt')}</p>
      </div>
    );
  }

  if (!payment) {
    return (
      <div className="fixed inset-0 z-[60] bg-white flex flex-col items-center justify-center gap-4">
        <p className="text-slate-500">{t('invoice.payment_not_found')}</p>
        <Button variant="outline" onClick={onClose}>{t('common.close')}</Button>
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
        @page { size: 210mm 165mm; margin: 5mm; }
        .receipt-paper {
          width: 200mm;
          min-height: 155mm;
          background: white;
          box-sizing: border-box;
          padding: 6mm;
          display: flex;
          flex-direction: column;
        }
        @media screen {
          .receipt-paper {
            box-shadow: 0 10px 30px rgba(0,0,0,0.15);
            margin: 24px auto;
          }
        }
        @media print {
          body { background: white; margin: 0; padding: 0; }
          .no-print { display: none !important; }
          .receipt-paper { box-shadow: none !important; margin: 0 !important; width: 200mm; min-height: 155mm; }
        }
      `}</style>

      <div className="no-print sticky top-0 z-10 flex items-center justify-between border-b border-slate-200 bg-white px-4 py-3 shadow-sm">
        <h2 className="text-lg font-semibold text-slate-800">{t('receipt.title')} — {folio?.folio_number || payment.payment_number}</h2>
        <div className="flex items-center gap-2">
          <Button variant="outline" size="sm" onClick={() => window.print()}><Printer size={16} /> {t('common.print')}</Button>
          <Button variant="secondary" size="sm" onClick={onClose}><X size={16} /> {t('common.close')}</Button>
        </div>
      </div>

      <div className="receipt-paper">
        {/* Header — compact, two columns: hotel info left, receipt title right */}
        <header className="flex items-start justify-between border-b border-slate-300 pb-2">
          <div>
            <h1 className="text-lg font-bold leading-tight">{branch?.name || 'Hotel'}</h1>
            {branch?.address && <p className="text-[10px] text-slate-500 leading-tight">{branch.address}</p>}
            {branch?.phone && <p className="text-[10px] text-slate-500 leading-tight">{t('common.tel')}: {branch.phone}</p>}
          </div>
          <div className="text-right">
            <h2 className="text-sm font-bold uppercase tracking-wide">{t('receipt.title')}</h2>
            {folio && <p className="text-[10px] text-slate-500">{folio.folio_number}</p>}
            <p className="text-[10px] text-slate-400">{formatDate(new Date().toISOString())}</p>
          </div>
        </header>

        {/* Guest info — compact column layout instead of row-by-row */}
        <section className="mt-2 grid grid-cols-2 gap-x-4 gap-y-0.5 text-[11px]">
          <InfoCol label={t('common.date')} value={formatDate(new Date().toISOString())} />
          <InfoCol label={t('common.guest')} value={guest?.full_name || '-'} />
          <InfoCol label={t('common.room')} value={room?.room_number || '-'} />
          {reservation ? (
            <InfoCol label={t('common.reservation')} value={reservation.reservation_number} />
          ) : (
            <InfoCol label={t('common.reservation')} value="-" />
          )}
          {reservation ? (
            <InfoCol label={t('res.stay')} value={`${formatDate(reservation.check_in_date)} — ${formatDate(reservation.check_out_date)}`} />
          ) : (
            <InfoCol label={t('res.stay')} value="-" />
          )}
          {guest?.phone ? (
            <InfoCol label={t('common.phone')} value={guest.phone} />
          ) : null}
        </section>

        {/* Charges table — compact */}
        <section className="mt-2">
          <p className="mb-1 text-[10px] font-bold uppercase text-slate-500">{t('common.charges')}</p>
          <table className="w-full text-[11px]">
            <thead>
              <tr className="border-b border-slate-300 text-slate-500">
                <th className="text-left py-1 pr-2 font-medium">{t('common.date')}</th>
                <th className="text-left py-1 pr-2 font-medium">{t('common.description')}</th>
                <th className="text-right py-1 font-medium">{t('common.amount')}</th>
              </tr>
            </thead>
            <tbody>
              {charges.length === 0 ? (
                <tr><td colSpan={3} className="py-1 text-center text-slate-400 text-[10px]">{t('receipt.no_charges')}</td></tr>
              ) : charges.map((c) => (
                <tr key={c.id} className="border-b border-slate-100">
                  <td className="py-0.5 pr-2 whitespace-nowrap text-slate-500">{formatDate(c.created_at)}</td>
                  <td className="py-0.5 pr-2">{c.description}</td>
                  <td className="py-0.5 text-right font-medium">{formatIDR(c.amount)}</td>
                </tr>
              ))}
              {totalTax > 0 && (
                <tr className="border-b border-slate-100">
                  <td className="py-0.5 pr-2"></td>
                  <td className="py-0.5 pr-2">{t('common.tax')}</td>
                  <td className="py-0.5 text-right font-medium">{formatIDR(totalTax)}</td>
                </tr>
              )}
              {totalDiscounts > 0 && (
                <tr className="border-b border-slate-100">
                  <td className="py-0.5 pr-2"></td>
                  <td className="py-0.5 pr-2 text-red-600">{t('common.discount')}</td>
                  <td className="py-0.5 text-right font-medium text-red-600">-{formatIDR(totalDiscounts)}</td>
                </tr>
              )}
            </tbody>
            {charges.length > 0 && (
              <tfoot>
                <tr className="border-t border-slate-300 font-bold">
                  <td colSpan={2} className="py-1">{t('receipt.total_charges')}</td>
                  <td className="py-1 text-right">{formatIDR(totalCharges + totalTax - totalDiscounts)}</td>
                </tr>
              </tfoot>
            )}
          </table>
        </section>

        {/* Payments table — compact */}
        <section className="mt-2">
          <p className="mb-1 text-[10px] font-bold uppercase text-slate-500">{t('common.payments')}</p>
          <table className="w-full text-[11px]">
            <thead>
              <tr className="border-b border-slate-300 text-slate-500">
                <th className="text-left py-1 pr-2 font-medium">{t('common.date')}</th>
                <th className="text-left py-1 pr-2 font-medium">{t('common.method')}</th>
                <th className="text-left py-1 pr-2 font-medium">{t('receipt.reference_no')}</th>
                <th className="text-right py-1 font-medium">{t('common.amount')}</th>
              </tr>
            </thead>
            <tbody>
              {allPayments.length === 0 ? (
                <tr><td colSpan={4} className="py-1 text-center text-slate-400 text-[10px]">{t('receipt.no_payments')}</td></tr>
              ) : allPayments.map((p) => (
                <tr key={p.payment_number} className="border-b border-slate-100">
                  <td className="py-0.5 pr-2 whitespace-nowrap text-slate-500">{formatDate(p.created_at)}</td>
                  <td className="py-0.5 pr-2">
                    {p.method_name}
                    {p.subtype && <span className="text-slate-400"> ({p.subtype})</span>}
                  </td>
                  <td className="py-0.5 pr-2 text-slate-500">{p.reference_number || '-'}</td>
                  <td className="py-0.5 text-right font-medium text-emerald-700">{formatIDR(p.amount)}</td>
                </tr>
              ))}
            </tbody>
            {allPayments.length > 0 && (
              <tfoot>
                <tr className="border-t border-slate-300 font-bold">
                  <td colSpan={3} className="py-1">{t('receipt.total_paid')}</td>
                  <td className="py-1 text-right text-emerald-700">{formatIDR(totalPayments)}</td>
                </tr>
              </tfoot>
            )}
          </table>
        </section>

        {/* Balance summary — compact inline */}
        <section className="mt-2 border-t border-slate-300 pt-1">
          <div className="flex items-center justify-between text-sm font-bold">
            <span>{t('common.balance')}</span>
            <span className={balance > 0 ? 'text-red-600' : 'text-emerald-600'}>{formatIDR(Math.abs(balance))}{balance > 0 ? ` ${t('receipt.balance_due')}` : ` ${t('receipt.balance_settled')}`}</span>
          </div>
        </section>

        {/* Footer — minimal */}
        <footer className="mt-auto pt-3 text-center text-[10px] text-slate-400">
          <p>{t('receipt.thank_you')}</p>
          <p className="mt-0.5">{t('common.printed')} {formatDateTime(new Date().toISOString())}</p>
        </footer>
      </div>
    </div>
  );
}

function InfoCol({ label, value }: { label: string; value: string }) {
  return (
    <div className="flex items-baseline gap-1.5">
      <span className="text-slate-500 whitespace-nowrap">{label}:</span>
      <span className="font-medium text-slate-800 truncate">{value}</span>
    </div>
  );
}
