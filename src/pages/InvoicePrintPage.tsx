import { useEffect, useState } from 'react';
import { invoiceService } from '@/services/invoiceService';
import type { Invoice, InvoiceItem, Guest, Branch } from '@/types/database';
import { formatIDR, formatDateTime } from '@/lib/format';
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
            <p>Room: {reservation?.rooms?.room_number || reservation?.room_number || reservation?.room_id || '-'}</p>
          </div>
          <div className="text-right">
            <p>Invoice: <b>{invoice.invoice_number}</b></p>
            <p>{invoice.issued_at ? formatDateTime(invoice.issued_at) : '-'}</p>
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

        <div className="mt-8 text-right">
          <p>Subtotal: {formatIDR(invoice.subtotal)}</p>
          <p>Tax: {formatIDR(invoice.tax)}</p>
          <h2 className="text-xl font-bold mt-2">Total: {formatIDR(invoice.total)}</h2>
        </div>

        <div className="mt-12 text-center text-xs">Thank you for staying with us.</div>
      </div>
    </div>
  );
}
