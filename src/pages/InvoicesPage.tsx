import { useState,useEffect,useCallback,useMemo } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal } from '@/components/ui/Modal';
import { InvoiceStatusBadge } from '@/components/ui/Badge';
import { LoadingPage,EmptyState } from '@/components/ui/States';
import { formatIDR,formatDate,formatDateTime } from '@/lib/format';
import { Receipt,Search,Printer } from 'lucide-react';
import type { Invoice,InvoiceItem,Guest,Branch,Folio,Reservation } from '@/types/database';

export function InvoicesPage({searchQuery}:{searchQuery?:string}) {
  const {branches}=useAuth();
  const {selectedBranchId}=useBranch();
  const {t}=useI18n();

  const [invoices,setInvoices]=useState<Invoice[]>([]);
  const [loading,setLoading]=useState(true);
  const [selected,setSelected]=useState<Invoice|null>(null);
  const [localSearch,setLocalSearch]=useState(searchQuery||'');

  const branchIds=useMemo(()=>selectedBranchId?[selectedBranchId]:branches.map(b=>b.id),[selectedBranchId,branches]);

  const load=useCallback(async()=>{
    if(!branchIds.length){setLoading(false);return;}
    setLoading(true);

    const {data}=await supabase.from('invoices')
      .select('*')
      .in('branch_id',branchIds)
      .order('created_at',{ascending:false})
      .limit(100);

    setInvoices((data as Invoice[])||[]);
    setLoading(false);
  },[branchIds]);

  useEffect(()=>{load()},[load]);

  if(loading)return <LoadingPage message={t('common.loading')}/>;

  const filtered=invoices.filter(i=>!localSearch||i.invoice_number.toLowerCase().includes(localSearch.toLowerCase()));

  return (
  <div className="space-y-6">
    <h1 className="text-2xl font-bold text-slate-900">{t('nav.invoices')}</h1>

    <div className="relative max-w-md">
      <Search size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400"/>
      <input
        value={localSearch}
        onChange={e=>setLocalSearch(e.target.value)}
        placeholder={t('common.search')}
        className="w-full rounded-lg border border-slate-300 pl-10 pr-3 py-2 text-sm outline-none focus:ring-2 focus:ring-blue-500"
      />
    </div>

    {filtered.length===0 ? (
      <EmptyState icon={<Receipt size={48}/>} title={t('invoice.no_invoices')}/>
    ) : (
      <Card noPadding>
        <div className="overflow-x-auto">
          <table className="w-full text-sm">
            <thead>
              <tr className="border-b border-slate-200 text-slate-500">
                <th className="text-left py-3 px-4">{t('invoice.invoice_number')}</th>
                <th className="text-left py-3 px-4">{t('common.guest')}</th>
                <th className="text-right py-3 px-4">{t('common.total')}</th>
                <th className="text-right py-3 px-4">{t('common.balance')}</th>
                <th className="text-center py-3 px-4">{t('common.status')}</th>
              </tr>
            </thead>

            <tbody>
              {filtered.map(inv=>(
                <tr
                  key={inv.id}
                  onClick={()=>setSelected(inv)}
                  className="border-b border-slate-100 hover:bg-slate-50 cursor-pointer"
                >
                  <td className="py-3 px-4 font-medium text-blue-600">
                    {inv.invoice_number}
                  </td>

                  <td className="py-3 px-4">-</td>

                  <td className="text-right py-3 px-4">
                    {formatIDR(inv.total)}
                  </td>

                  <td className="text-right py-3 px-4">
                    {formatIDR(inv.balance)}
                  </td>

                  <td className="text-center py-3 px-4">
                    <InvoiceStatusBadge status={inv.status} label={inv.status}/>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </Card>
    )}

    {selected && (
      <InvoiceDetailModal
        invoice={selected}
        onClose={()=>{
          setSelected(null);
          load();
        }}
      />
    )}
  </div>
);
}

async function createInvoiceSnapshot(invoiceId:string){
  const {data:invoice}=await supabase.from('invoices').select('*').eq('id',invoiceId).single();
  if(!invoice)return;

  const [{data:items},{data:guest},{data:branch},{data:reservation},{data:folio}]=await Promise.all([
    supabase.from('invoice_items').select('*').eq('invoice_id',invoiceId).order('sort_order'),
    supabase.from('guests').select('*').eq('id',invoice.guest_id).maybeSingle(),
    supabase.from('branches').select('*').eq('id',invoice.branch_id).maybeSingle(),
    supabase.from('reservations').select('*').eq('id',invoice.reservation_id).maybeSingle(),
    supabase.from('folios').select('*').eq('id',invoice.folio_id).maybeSingle()
  ]);

  await supabase.from('invoice_snapshots').upsert({
    invoice_id:invoiceId,
    snapshot:{
      invoice,
      items:items||[],
      guest:guest||null,
      branch:branch||null,
      reservation:reservation||null,
      folio:folio||null,
      generated_at:new Date().toISOString()
    }
  });
}

export async function finalizeCheckoutInvoice(invoiceId:string){
  await createInvoiceSnapshot(invoiceId);

  await supabase.from('invoices').update({
    finalized_at:new Date().toISOString()
  }).eq('id',invoiceId);
}

function InvoiceDetailModal({invoice,onClose}:{invoice:Invoice;onClose:()=>void}) {
  const {user}=useAuth();
  const {t}=useI18n();
  const [items,setItems]=useState<InvoiceItem[]>([]);
  const [guest,setGuest]=useState<Guest|null>(null);
  const [branch,setBranch]=useState<Branch|null>(null);
  const [reservation,setReservation]=useState<Reservation|null>(null);
  const [loading,setLoading]=useState(true);

  useEffect(()=>{
    (async()=>{
      const [{data:i},{data:g},{data:b},{data:r}]=await Promise.all([
        supabase.from('invoice_items').select('*').eq('invoice_id',invoice.id).order('sort_order'),
        supabase.from('guests').select('*').eq('id',invoice.guest_id).maybeSingle(),
        supabase.from('branches').select('*').eq('id',invoice.branch_id).maybeSingle(),
        supabase.from('reservations').select('*').eq('id',invoice.reservation_id).maybeSingle()
      ]);

      setItems((i as InvoiceItem[])||[]);
      setGuest(g as Guest);
      setBranch(b as Branch);
      setReservation(r as Reservation);
      setLoading(false);
    })();
  },[invoice]);

  if(loading)return <Modal open onClose={onClose} title={invoice.invoice_number}><LoadingPage/></Modal>;

  return (
    <Modal open onClose={onClose} title={invoice.invoice_number} size="lg">
      <div id="invoice-print-area" className="space-y-4 border rounded-lg p-6">
        <div>
          <h2 className="text-xl font-bold">{branch?.name||'Hotel'}</h2>
          <p>{branch?.address}</p>
        </div>

        <div>
          <b>{guest?.full_name||'-'}</b>
          <p>Room: {reservation?.room_id||'-'}</p>
        </div>

        <table className="w-full text-sm">
          <tbody>
            {items.map(i=>
              <tr key={i.id}>
                <td>{i.description}</td>
                <td>{i.quantity}</td>
                <td>{formatIDR(i.amount)}</td>
              </tr>
            )}
          </tbody>
        </table>

        <div className="text-right font-bold">
          Total: {formatIDR(invoice.total)}
        </div>

        <div className="text-xs text-slate-400">
          Issued by {user?.full_name}<br/>
          {invoice.issued_at&&formatDateTime(invoice.issued_at)}
        </div>

        <Button variant="outline" onClick={()=>window.print()}>
          <Printer size={16}/> {t('common.print')}
        </Button>
      </div>
    </Modal>
  );
}