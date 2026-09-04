import { useEffect, useState } from 'react';
import { parsePrintHash, type PrintParams } from '@/lib/printRoute';
import { PaymentReceiptPrintPage } from '@/pages/PaymentReceiptPrintPage';
import { ChargeSummaryPrintPage } from '@/pages/ChargeSummaryPrintPage';
import { InvoicePrintPage } from '@/pages/InvoicePrintPage';
import { GrcPrintPage } from '@/pages/GrcPrintPage';

export function PrintRoute() {
  const [params, setParams] = useState<PrintParams | null>(null);

  useEffect(() => {
    setParams(parsePrintHash(window.location.hash));
  }, []);

  if (!params) {
    return <div className="min-h-screen flex items-center justify-center text-slate-400">Loading...</div>;
  }

  const handleClose = () => window.close();

  switch (params.type) {
    case 'receipt':
      return <PaymentReceiptPrintPage paymentId={params.paymentId!} onClose={handleClose} autoPrint />;
    case 'charge-summary':
      return <ChargeSummaryPrintPage folioId={params.folioId!} title={params.title || 'Charge Summary'} onClose={handleClose} autoPrint />;
    case 'invoice':
      return <InvoicePrintPage invoiceId={params.invoiceId!} onClose={handleClose} autoPrint />;
    case 'grc':
      return <GrcPrintPage reservationId={params.reservationId!} onClose={handleClose} autoPrint />;
    default:
      return <div className="min-h-screen flex items-center justify-center text-slate-400">Unknown print type</div>;
  }
}
