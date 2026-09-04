export type PrintDocType = 'receipt' | 'charge-summary' | 'invoice' | 'grc';

export interface PrintParams {
  type: PrintDocType;
  paymentId?: string;
  folioId?: string;
  invoiceId?: string;
  reservationId?: string;
  title?: string;
}

export function openPrintTab(params: PrintParams): void {
  const hash = `#print/${params.type}/${[
    params.paymentId || '',
    params.folioId || '',
    params.invoiceId || '',
    params.reservationId || '',
    encodeURIComponent(params.title || ''),
  ].join('/')}`;
  window.open(hash, '_blank');
}

export function parsePrintHash(hash: string): PrintParams | null {
  const m = hash.match(/^#print\/(receipt|charge-summary|invoice|grc)\/([^/]*)\/([^/]*)\/([^/]*)\/([^/]*)\/(.*)$/);
  if (!m) return null;
  return {
    type: m[1] as PrintDocType,
    paymentId: m[2] || undefined,
    folioId: m[3] || undefined,
    invoiceId: m[4] || undefined,
    reservationId: m[5] || undefined,
    title: decodeURIComponent(m[6]) || undefined,
  };
}
