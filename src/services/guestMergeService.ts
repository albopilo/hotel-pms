import { supabase } from '@/lib/supabase';
import type { Guest } from '@/types/database';

export class GuestMergeError extends Error {
  constructor(message: string, public code: string = 'merge_error') {
    super(message);
    this.name = 'GuestMergeError';
  }
}

export interface MergePreview {
  duplicateGuest: Guest;
  primaryGuest: Guest;
  reservationCount: number;
  folioCount: number;
  folioItemCount: number;
  invoiceCount: number;
  paymentCount: number;
  cardIssuanceCount: number;
}

export const guestMergeService = {
  async previewMerge(primaryId: string, duplicateId: string): Promise<MergePreview> {
    const { data: primaryGuest, error: pErr } = await supabase
      .from('guests')
      .select('*')
      .eq('id', primaryId)
      .maybeSingle();
    if (pErr || !primaryGuest) throw new GuestMergeError('Primary guest not found');

    const { data: duplicateGuest, error: dErr } = await supabase
      .from('guests')
      .select('*')
      .eq('id', duplicateId)
      .maybeSingle();
    if (dErr || !duplicateGuest) throw new GuestMergeError('Duplicate guest not found');

    const [res, folios, folioItems, invoices, payments, cards] = await Promise.all([
      supabase.from('reservations').select('id', { count: 'exact', head: true }).eq('primary_guest_id', duplicateId),
      supabase.from('folios').select('id', { count: 'exact', head: true }).eq('guest_id', duplicateId),
      supabase.from('folio_items').select('id', { count: 'exact', head: true }).eq('guest_id', duplicateId),
      supabase.from('invoices').select('id', { count: 'exact', head: true }).eq('guest_id', duplicateId),
      supabase.from('payments').select('id', { count: 'exact', head: true }).eq('guest_id', duplicateId),
      supabase.from('card_issuances').select('id', { count: 'exact', head: true }).eq('guest_id', duplicateId),
    ]);

    return {
      primaryGuest: primaryGuest as Guest,
      duplicateGuest: duplicateGuest as Guest,
      reservationCount: res.count || 0,
      folioCount: folios.count || 0,
      folioItemCount: folioItems.count || 0,
      invoiceCount: invoices.count || 0,
      paymentCount: payments.count || 0,
      cardIssuanceCount: cards.count || 0,
    };
  },

  async mergeGuests(primaryId: string, duplicateId: string, userId: string, orgId: string): Promise<void> {
    if (primaryId === duplicateId) throw new GuestMergeError('Cannot merge a guest with itself');

    // Reassign reservations
    const { error: resErr } = await supabase
      .from('reservations')
      .update({ primary_guest_id: primaryId })
      .eq('primary_guest_id', duplicateId);
    if (resErr) throw new GuestMergeError(`Failed to reassign reservations: ${resErr.message}`);

    // Reassign folios
    const { error: folioErr } = await supabase
      .from('folios')
      .update({ guest_id: primaryId })
      .eq('guest_id', duplicateId);
    if (folioErr) throw new GuestMergeError(`Failed to reassign folios: ${folioErr.message}`);

    // Reassign folio_items
    const { error: fiErr } = await supabase
      .from('folio_items')
      .update({ guest_id: primaryId })
      .eq('guest_id', duplicateId);
    if (fiErr) throw new GuestMergeError(`Failed to reassign folio items: ${fiErr.message}`);

    // Reassign invoices
    const { error: invErr } = await supabase
      .from('invoices')
      .update({ guest_id: primaryId })
      .eq('guest_id', duplicateId);
    if (invErr) throw new GuestMergeError(`Failed to reassign invoices: ${invErr.message}`);

    // Reassign payments
    const { error: payErr } = await supabase
      .from('payments')
      .update({ guest_id: primaryId })
      .eq('guest_id', duplicateId);
    if (payErr) throw new GuestMergeError(`Failed to reassign payments: ${payErr.message}`);

    // Reassign card_issuances
    const { error: cardErr } = await supabase
      .from('card_issuances')
      .update({ guest_id: primaryId })
      .eq('guest_id', duplicateId);
    if (cardErr) throw new GuestMergeError(`Failed to reassign card issuances: ${cardErr.message}`);

    // Reassign reservation_guests (if any link to the duplicate)
    await supabase
      .from('reservation_guests')
      .update({ guest_id: primaryId })
      .eq('guest_id', duplicateId);

    // Reassign guest_documents
    await supabase
      .from('guest_documents')
      .update({ guest_id: primaryId })
      .eq('guest_id', duplicateId);

    // Log the merge in audit_logs
    await supabase.from('audit_logs').insert({
      organization_id: orgId,
      user_id: userId,
      action: 'guest_merged',
      object_type: 'guest',
      object_id: primaryId,
      previous_value: { duplicate_guest_id: duplicateId },
      new_value: { merged_into: primaryId },
    });

    // Delete the duplicate guest
    const { error: delErr } = await supabase
      .from('guests')
      .delete()
      .eq('id', duplicateId);
    if (delErr) throw new GuestMergeError(`Failed to delete duplicate guest: ${delErr.message}`);
  },
};
