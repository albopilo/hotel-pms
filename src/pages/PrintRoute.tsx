import { lazy, Suspense, useEffect, useState } from 'react';
import { parsePrintHash, type PrintParams } from '@/lib/printRoute';

const PaymentReceiptPrintPage = lazy(() => import('@/pages/PaymentReceiptPrintPage').then(m => ({ default: m.PaymentReceiptPrintPage })));
const ChargeSummaryPrintPage = lazy(() => import('@/pages/ChargeSummaryPrintPage').then(m => ({ default: m.ChargeSummaryPrintPage })));
const InvoicePrintPage = lazy(() => import('@/pages/InvoicePrintPage').then(m => ({ default: m.InvoicePrintPage })));
const GrcPrintPage = lazy(() => import('@/pages/GrcPrintPage').then(m => ({ default: m.GrcPrintPage })));

export function PrintRoute() {
  const [params, setParams] = useState<PrintParams | null>(null);

  useEffect(() => {
    setParams(parsePrintHash(window.location.hash));
  }, []);

  if (!params) {
    return <div className="min-h-screen flex items-center justify-center text-slate-400">Loading...</div>;
  }

  const handleClose = () => window.close();

  return (
    <Suspense fallback={<div className="min-h-screen flex items-center justify-center text-slate-400">Loading...</div>}>
      {params.type === 'receipt' && <PaymentReceiptPrintPage paymentId={params.paymentId!} onClose={handleClose} />}
      {params.type === 'charge-summary' && <ChargeSummaryPrintPage folioId={params.folioId!} title={params.title || 'Charge Summary'} onClose={handleClose} />}
      {params.type === 'invoice' && <InvoicePrintPage invoiceId={params.invoiceId!} onClose={handleClose} />}
      {params.type === 'grc' && <GrcPrintPage reservationId={params.reservationId!} onClose={handleClose} />}
    </Suspense>
  );
}
