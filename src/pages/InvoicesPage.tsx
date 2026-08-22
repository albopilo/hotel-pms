import { useState,useEffect,useCallback,useMemo,useRef } from 'react';
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
import { formatIDR,formatDate,formatDateTime,formatTime } from '@/lib/format';
import { Receipt,Search,Printer,FileText,User as UserIcon } from 'lucide-react';
import type { Invoice,InvoiceItem,Guest,Branch,Folio,Reservation,BookingSource,RoomType,ReservationRoom } from '@/types/database';
import { invoiceService } from '@/services/invoiceService';
import { InvoicePrintPage } from '@/pages/InvoicePrintPage';

export function InvoicesPage({searchQuery,reservationId,onNavigateToPayment,onNavigateToGuest}:{searchQuery?:string;reservationId?:string|null;onNavigateToPayment?:(id:string)=>void;onNavigateToGuest?:(id:string)=>void}) {
  const {branches}=useAuth();
  const {selectedBranchId}=useBranch();
  const {t}=useI18n();

  const [invoices,setInvoices]=useState<Invoice[]>([]);
  const [loading,setLoading]=useState(true);
  const [selected,setSelected]=useState<Invoice|null>(null);
  const [printInvoiceId,setPrintInvoiceId]=useState<string|null>(null);
  const [localSearch,setLocalSearch]=useState(searchQuery||'');
  const processedResId=useRef<string|null>(null);

  const branchIds=useMemo(()=>selectedBranchId?[selectedBranchId]:branches.map(b=>b.id),[selectedBranchId,branches]);

  const load=useCallback(async()=>{
    if(!branchIds.length){setLoading(false);return;}
    setLoading(true);

    const data =
 await invoiceService.getInvoicesByBranch(branchIds);


setInvoices(data || []);
    setLoading(false);
  },[branchIds]);

  useEffect(()=>{load()},[load]);

  useEffect(()=>{
    if(!reservationId)return;
    if(processedResId.current===reservationId)return;
    if(invoices.length===0)return;
    const inv=invoices.find(i=>i.reservation_id===reservationId);
    if(inv){
      processedResId.current=reservationId;
      setSelected(inv);
    }
  },[reservationId,invoices]);

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

                  <td className="py-3 px-4">{inv.guests?.full_name || '-'}</td>

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
        onPrint={() => setPrintInvoiceId(selected.id)}
        onNavigateToPayment={onNavigateToPayment}
        onNavigateToGuest={onNavigateToGuest}
      />
    )}

    {printInvoiceId && (
      <InvoicePrintPage
        invoiceId={printInvoiceId}
        onClose={() => setPrintInvoiceId(null)}
      />
    )}
  </div>
);
}

async function createInvoiceSnapshot(invoiceId:string){
  const invoice = await invoiceService.getInvoiceDetail(invoice.id);

  await supabase.from('invoice_snapshots').upsert({
    invoice_id:invoiceId,
    snapshot:{
      invoice,
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

function InvoiceDetailModal({invoice,onClose,onPrint,onNavigateToPayment,onNavigateToGuest}:{invoice:Invoice;onClose:()=>void;onPrint:()=>void;onNavigateToPayment?:(id:string)=>void;onNavigateToGuest?:(id:string)=>void}) {
  const {user}=useAuth();
  const {t}=useI18n();
  const [items,setItems]=useState<InvoiceItem[]>([]);
  const [guest,setGuest]=useState<Guest|null>(null);
  const [branch,setBranch]=useState<Branch|null>(null);
  const [reservation,setReservation]=useState<Reservation|null>(null);
  const [bookingSource,setBookingSource]=useState<BookingSource|null>(null);
  const [roomType,setRoomType]=useState<RoomType|null>(null);
  const [groupRooms,setGroupRooms]=useState<ReservationRoom[]>([]);
  const [loading,setLoading]=useState(true);

  useEffect(()=>{
    (async()=>{
      const detail = await invoiceService.getInvoiceDetail(invoice.id);

      const res = detail.reservations || null;
      setItems(detail.invoice_items || []);
      setGuest(detail.guests || null);
      setBranch(detail.branches || null);
      setReservation(res);

      let bs: BookingSource|null = null;
      if (res?.booking_source_id) {
        const { data: bsData } = await supabase.from('booking_sources').select('*').eq('id', res.booking_source_id).maybeSingle();
        bs = bsData as BookingSource|null;
      }
      setBookingSource(bs);

      let rt: RoomType|null = null;
      if (res?.room_type_id) {
        const { data: rtData } = await supabase.from('room_types').select('*').eq('id', res.room_type_id).maybeSingle();
        rt = rtData as RoomType|null;
      }
      setRoomType(rt);

      if (res?.is_group) {
        const { data: rrData } = await supabase.from('reservation_rooms')
          .select('*,room:rooms(*)').eq('reservation_id', res.id).eq('status','active').order('created_at');
        setGroupRooms((rrData as (ReservationRoom & { room?: { room_number: string } })[]) || []);
      }

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

        <div className="grid grid-cols-2 md:grid-cols-4 gap-3 text-sm">
          <div><b>{guest?.full_name||'-'}</b></div>
          <div><span className="text-slate-500">{t('common.room')}:</span> <span className="font-medium">{reservation?.rooms?.room_number || '-'}</span></div>
          {roomType && <div><span className="text-slate-500">{t('common.room_type')}:</span> <span className="font-medium">{roomType.name}</span></div>}
          {bookingSource && <div><span className="text-slate-500">{t('common.booking_source')}:</span> <span className="font-medium">{bookingSource.name}</span></div>}
          <div><span className="text-slate-500">{t('common.check_in')}:</span> <span className="font-medium">{reservation ? `${formatDate(reservation.check_in_date)} ${formatTime(reservation.check_in_time)}` : '-'}</span></div>
          <div><span className="text-slate-500">{t('common.check_out')}:</span> <span className="font-medium">{reservation ? `${formatDate(reservation.check_out_date)} ${formatTime(reservation.check_out_time)}` : '-'}</span></div>
          <div><span className="text-slate-500">{t('common.nights')}:</span> <span className="font-medium">{reservation?.num_nights ?? '-'}</span></div>
          <div><span className="text-slate-500">{t('common.adults')} / {t('common.children')}:</span> <span className="font-medium">{reservation ? `${reservation.adults} / ${reservation.children}` : '-'}</span></div>
          {reservation && <div><span className="text-slate-500">{t('common.deposit')}:</span> <span className="font-medium">{formatIDR(reservation.deposit)}</span></div>}
        </div>

        {groupRooms.length > 1 && (
          <div className="border border-slate-200 rounded-lg p-3">
            <p className="text-xs font-semibold text-slate-500 uppercase mb-2">{t('res.group_rooms')} ({groupRooms.length})</p>
            <div className="flex flex-wrap gap-2">
              {groupRooms.map(rr => (
                <span key={rr.id} className="text-sm bg-slate-50 border border-slate-200 rounded px-2 py-1">
                  {(rr as any).room?.room_number || 'Unassigned'} · {formatIDR(rr.rate)}/{t('common.nights')}
                </span>
              ))}
            </div>
          </div>
        )}

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
        
        <div className="flex flex-wrap gap-2">
          <Button variant="outline" onClick={onPrint}>
            <Printer size={16}/> {t('common.print')}
          </Button>
          {onNavigateToPayment&&invoice.folio_id&&(
            <Button variant="outline" onClick={()=>onNavigateToPayment(invoice.reservation_id||'')}><FileText size={14}/> {t('res.view_folio')}</Button>
          )}
          {onNavigateToGuest&&invoice.guest_id&&(
            <Button variant="outline" onClick={()=>onNavigateToGuest(invoice.guest_id!)}><UserIcon size={14}/> {t('nav.guests')}</Button>
          )}
        </div>
      </div>
    </Modal>
  );
}