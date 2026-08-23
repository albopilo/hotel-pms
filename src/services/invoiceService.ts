import { supabase } from '@/lib/supabase';
import type { FolioItem } from '@/types/database';

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
    const { data: folio, error: folioError } = await supabase
      .from('folios')
      .select('*')
      .eq('id', input.folioId)
      .single();
    if (folioError || !folio) {
      throw new InvoiceError('Folio not found');
    }

    const { data: items, error: itemsError } = await supabase
      .from('folio_items')
      .select('*')
      .eq('folio_id', input.folioId)
      .eq('voided', false);
    if (itemsError) {
      throw new InvoiceError(itemsError.message);
    }

    const { data: reservation, error: reservationError } = await supabase
      .from('reservations')
      .select('reservation_number')
      .eq('id', input.reservationId)
      .single();
    if (reservationError || !reservation) {
      throw new InvoiceError('Reservation not found');
    }

    const invoiceNumber = `INV-${reservation.reservation_number.replace('RES-', '')}`;
    const folioItems = (items || []) as FolioItem[];

    const subtotal = folioItems
      .filter((i) => i.item_type === 'charge')
      .reduce((sum, i) => sum + i.amount, 0);
    const tax = folioItems
      .filter((i) => i.item_type === 'tax')
      .reduce((sum, i) => sum + i.amount, 0);
    const discount = folioItems
      .filter((i) => i.item_type === 'discount')
      .reduce((sum, i) => sum + Math.abs(i.amount), 0);
    const payments = folioItems
      .filter((i) => i.item_type === 'payment')
      .reduce((sum, i) => sum + Math.abs(i.amount), 0);
    const total = subtotal + tax - discount;

    const { data: invoice, error } = await supabase
      .from('invoices')
      .insert({
        branch_id: input.branchId,
        reservation_id: input.reservationId,
        guest_id: input.guestId,
        folio_id: input.folioId,
        invoice_number: invoiceNumber,
        subtotal,
        tax,
        discount,
        total,
        amount_paid: payments,
        balance: total - payments,
        status: 'issued',
        issued_by: input.userId,
      })
      .select('id')
      .single();

    if (error || !invoice) {
      throw new InvoiceError(error?.message || 'Invoice creation failed');
    }

    await this.syncFromFolio(invoice.id, input.folioId);

    return invoice.id;
  },

  async syncFromFolio(invoiceId: string, folioId: string): Promise<void> {
    const { data: items, error: itemsError } = await supabase
      .from('folio_items')
      .select('*')
      .eq('folio_id', folioId)
      .eq('voided', false);
    if (itemsError) {
      throw new InvoiceError(itemsError.message);
    }

    const folioItems = (items || []) as FolioItem[];

    await supabase.from('invoice_items').delete().eq('invoice_id', invoiceId).is('folio_item_id', null);

    // Only show charge and tax items as invoice line items; payments are shown as paid amount
    const lineItems = folioItems.filter((i) => i.item_type === 'charge' || i.item_type === 'tax' || i.item_type === 'discount');

    const activeFolioItemIds = new Set(lineItems.map((i) => i.id));

    const { data: existingItems } = await supabase
      .from('invoice_items')
      .select('id, folio_item_id')
      .eq('invoice_id', invoiceId)
      .not('folio_item_id', 'is', null);

    const staleIds = ((existingItems || []) as { id: string; folio_item_id: string }[])
      .filter((ei) => !activeFolioItemIds.has(ei.folio_item_id))
      .map((ei) => ei.id);
    if (staleIds.length) {
      await supabase.from('invoice_items').delete().in('id', staleIds);
    }

    const invoiceItems = lineItems.map((item, index) => ({
      invoice_id: invoiceId,
      folio_item_id: item.id,
      description: item.description,
      category: item.category,
      quantity: item.quantity,
      unit_amount: item.unit_amount,
      amount: item.amount,
      sort_order: index,
    }));

    if (invoiceItems.length) {
      const { error: itemError } = await supabase
        .from('invoice_items')
        .upsert(invoiceItems, { onConflict: 'invoice_id,folio_item_id' });
      if (itemError) {
        throw new InvoiceError(itemError.message);
      }
    }

    const subtotal = folioItems
      .filter((i) => i.item_type === 'charge')
      .reduce((sum, i) => sum + i.amount, 0);
    const tax = folioItems
      .filter((i) => i.item_type === 'tax')
      .reduce((sum, i) => sum + i.amount, 0);
    const discount = folioItems
      .filter((i) => i.item_type === 'discount')
      .reduce((sum, i) => sum + Math.abs(i.amount), 0);
    const payments = folioItems
      .filter((i) => i.item_type === 'payment')
      .reduce((sum, i) => sum + Math.abs(i.amount), 0);
    const total = subtotal + tax - discount;

    const { error: updateError } = await supabase
      .from('invoices')
      .update({
        subtotal,
        tax,
        discount,
        total,
        amount_paid: payments,
        balance: total - payments,
      })
      .eq('id', invoiceId);
    if (updateError) {
      throw new InvoiceError(updateError.message);
    }
  },

  async ensureInvoice(input: InvoiceCreateInput): Promise<string> {
    const { data: existing } = await supabase
      .from('invoices')
      .select('id')
      .eq('folio_id', input.folioId)
      .maybeSingle();

    if (existing) {
      await this.syncFromFolio(existing.id, input.folioId);
      return existing.id;
    }

    return this.createInvoice(input);
  },

  async getInvoice(invoiceId: string) {
    const { data, error } = await supabase
      .from('invoices')
      .select(`*, invoice_items(*)`)
      .eq('id', invoiceId)
      .maybeSingle();
    if (error) {
      throw new InvoiceError(error.message);
    }
    return data;
  },

  async getInvoiceByFolio(folioId: string) {
    const { data, error } = await supabase
      .from('invoices')
      .select(`*, invoice_items(*)`)
      .eq('folio_id', folioId)
      .maybeSingle();
    if (error) {
      throw new InvoiceError(error.message);
    }
    return data;
  },

  async getInvoicesByBranch(branchIds: string[]) {
    const { data, error } = await supabase
      .from('invoices')
      .select(`*, guests(id,full_name,phone,email),branches(id,name,address),reservations(id,reservation_number,room_id,rooms(id,room_number)),invoice_items(id,description,category,quantity,unit_amount,amount)`)
      .in('branch_id', branchIds)
      .order('created_at', { ascending: false })
      .limit(100);
    if (error) throw new InvoiceError(error.message);
    return data;
  },

  async getInvoiceDetail(invoiceId: string) {
    const { data: stub } = await supabase
      .from('invoices')
      .select('folio_id')
      .eq('id', invoiceId)
      .maybeSingle();
    if (stub?.folio_id) {
      try {
        await this.syncFromFolio(invoiceId, stub.folio_id);
      } catch {
        // ignore sync errors so the invoice still renders
      }
    }

    const { data, error } = await supabase
      .from('invoices')
      .select(`*, invoice_items(*), guests(*), branches(*), reservations(*, rooms(*), room_types(*))`)
      .eq('id', invoiceId)
      .single();
    if (error) throw new InvoiceError(error.message);
    return data;
  },
};
