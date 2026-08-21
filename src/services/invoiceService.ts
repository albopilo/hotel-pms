import { supabase } from '@/lib/supabase';
import type { FolioItem } from '@/types/database';
import { getBusinessDate } from '@/services/businessDateService';


export interface InvoiceCreateInput {
  folioId: string;
  branchId: string;
  organizationId: string;
  reservationId: string;
  guestId: string | null;
  userId: string;
}


export class InvoiceError extends Error {
  constructor(
    message: string,
    public code = 'invoice_error'
  ) {
    super(message);
    this.name = 'InvoiceError';
  }
}


export const invoiceService = {


  async createInvoice(input: InvoiceCreateInput): Promise<string> {
    // Get folio
    const { data: folio, error: folioError } =await supabase .from('folios').select('*').eq('id', input.folioId).single();
    if (folioError || !folio) {throw new InvoiceError('Folio not found');}
    // Get folio items
    const { data: items, error: itemsError } =await supabase.from('folio_items').select('*').eq('folio_id', input.folioId).eq('voided', false);
    if (itemsError) {throw new InvoiceError(itemsError.message);}
    const { data: reservation, error: reservationError } =await supabase.from('reservations').select('reservation_number').eq('id', input.reservationId).single();
    if(reservationError || !reservation){throw new InvoiceError('Reservation not found');}
    const invoiceNumber =`INV-${reservation.reservation_number.replace('RES-','')}`;
    const subtotal =(items || []).filter((i: FolioItem)=>i.item_type === 'charge').reduce((sum,i)=>sum + i.amount,0);
    const tax =(items || []).filter((i: FolioItem)=>i.item_type === 'tax').reduce((sum,i)=>sum+i.amount,0);
    const discount =(items || []).filter((i: FolioItem)=>i.item_type === 'discount').reduce((sum,i)=>sum+i.amount,0);
    const total =subtotal + tax - Math.abs(discount);



    // create invoice header

    const { data: invoice, error } =
      await supabase
        .from('invoices')
        .insert({
          branch_id: input.branchId,
          reservation_id: input.reservationId,
          guest_id: input.guestId,
          folio_id: input.folioId,
          invoice_number: invoiceNumber,
          subtotal,
          tax: tax,
          discount: discount,
          total: total,
          amount_paid: 0,
          balance: total,
          status:'issued',
          issued_by: input.userId
        })
        .select('id')
        .single();

    if(error || !invoice){
      throw new InvoiceError(
        error?.message ||
        'Invoice creation failed'
      );
    }



    // copy folio items into invoice items

const invoiceItems =
(items || []).map(
(item:any,index)=>({
    invoice_id: invoice.id,
    folio_item_id: item.id,
    description: item.description,
    category: item.category,
    quantity: item.quantity,
    unit_amount: item.unit_amount,
    amount: item.amount,
    sort_order: index
})
);



    const {error:itemError} =
      await supabase
        .from('invoice_items')
        .insert(invoiceItems);



    if(itemError){
      throw new InvoiceError(
        itemError.message
      );
    }



    return invoice.id;

  },

  async getInvoice(invoiceId:string){

   const {data,error}=await supabase
     .from('invoices')
     .select(`
        *,
        invoice_items(*)
     `)
     .eq('id',invoiceId)
     .maybeSingle();


   if(error){
     throw new InvoiceError(error.message);
   }


   return data;

 },


 async getInvoiceByFolio(folioId:string){

   const {data,error}=await supabase
     .from('invoices')
     .select(`
        *,
        invoice_items(*)
     `)
     .eq('folio_id',folioId)
     .maybeSingle();


   if(error){
     throw new InvoiceError(error.message);
   }


   return data;

 },

 async getInvoicesByBranch(branchIds:string[]){

 const {data,error}=await supabase
 .from('invoices')
 .select(`*, guests(id,full_name,phone,email),branches(id,name,address),reservations(id,reservation_number,room_id,rooms(id,room_number)),invoice_items(id,description,category,quantity,unit_amount,amount)`)
 .in('branch_id',branchIds)
 .order('created_at',{ascending:false})
 .limit(100);


 if(error)
   throw new InvoiceError(error.message);


 return data;

},

async getInvoiceDetail(invoiceId:string){

const {data,error}=await supabase
.from('invoices')
.select(`
 *,
 invoice_items(*),
 guests(*),
 branches(*),
 reservations(*)
`)
.eq('id',invoiceId)
.single();


if(error)
 throw new InvoiceError(error.message);


return data;

}

};