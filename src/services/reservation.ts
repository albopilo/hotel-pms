import { supabase } from '@/lib/supabase';
import { nightsBetween } from '@/lib/format';
import { folioService } from './financial';
import type { Reservation } from '@/types/database';

export class ReservationError extends Error {
  constructor(message: string, public code: string = 'reservation_error') {
    super(message);
    this.name = 'ReservationError';
  }
}

export interface CreateReservationInput {
  branch_id: string;
  organization_id: string;
  primary_guest_id: string;
  room_type_id: string;
  room_id?: string | null;
  adults: number;
  children: number;
  check_in_date: string;
  check_in_time: string;
  check_out_date: string;
  check_out_time: string;
  rate: number;
  discount?: number;
  tax?: number;
  deposit?: number;
  booking_source_id?: string | null;
  status?: string;
  special_requests?: string | null;
  notes?: string | null;
  created_by: string;
}

export const reservationService = {
  async checkAvailability(
    roomId: string,
    checkIn: string,
    checkOut: string,
    excludeReservationId?: string,
  ): Promise<boolean> {
    if (!roomId) return false;
    let query = supabase
      .from('reservations')
      .select('id')
      .eq('room_id', roomId)
      .in('status', ['confirmed', 'checked_in', 'tentative'])
      .lt('check_in_date', checkOut)
      .gt('check_out_date', checkIn);
    if (excludeReservationId) query = query.neq('id', excludeReservationId);
    const { data, error } = await query;
    if (error) throw new ReservationError(error.message, 'db_error');
    return (data || []).length === 0;
  },

  async createReservation(input: CreateReservationInput): Promise<Reservation> {
    if (!input.branch_id || !input.primary_guest_id || !input.room_type_id) {
      throw new ReservationError('Branch, guest, and room type are required', 'missing_field');
    }

    const nights = nightsBetween(input.check_in_date, input.check_out_date);

    if (input.room_id) {
      const available = await this.checkAvailability(
        input.room_id,
        input.check_in_date,
        input.check_out_date,
      );
      if (!available) {
        throw new ReservationError(
          'This room is already booked for the selected dates. Please choose another room.',
          'double_booked',
        );
      }
    }

    const resNum = `RES-${Date.now().toString().slice(-8)}`;
    const payload = {
      branch_id: input.branch_id,
      organization_id: input.organization_id,
      reservation_number: resNum,
      primary_guest_id: input.primary_guest_id,
      room_type_id: input.room_type_id,
      room_id: input.room_id || null,
      adults: input.adults || 1,
      children: input.children || 0,
      check_in_date: input.check_in_date,
      check_in_time: input.check_in_time,
      check_out_date: input.check_out_date,
      check_out_time: input.check_out_time,
      num_nights: nights,
      rate: input.rate || 0,
      discount: input.discount || 0,
      tax: input.tax || 0,
      deposit: input.deposit || 0,
      booking_source_id: input.booking_source_id || null,
      status: input.status || 'tentative',
      special_requests: input.special_requests || null,
      notes: input.notes || null,
      created_by: input.created_by,
    };

    const { data: newRes, error } = await supabase
      .from('reservations')
      .insert(payload)
      .select()
      .single();
    if (error) {
      if (error.message.includes('reservations_no_overlap') || error.message.includes('exclusion')) {
        throw new ReservationError(
          'This room is already booked for the selected dates. Please choose another room.',
          'double_booked',
        );
      }
      if (error.message.includes('row-level security')) {
        throw new ReservationError(
          'You do not have permission to create reservations for this branch.',
          'permission_denied',
        );
      }
      throw new ReservationError(error.message, 'db_error');
    }

    await supabase.from('folios').insert({
      branch_id: input.branch_id,
      reservation_id: newRes.id,
      guest_id: input.primary_guest_id,
      folio_number: `FOL-${Date.now().toString().slice(-8)}`,
      status: 'open',
    });

    if (input.room_id) {
      const roomStatus = input.status === 'checked_in' ? 'occupied' : 'reserved';
      await supabase.from('rooms').update({ status: roomStatus }).eq('id', input.room_id);
    }

    await supabase.from('audit_logs').insert({
      organization_id: input.organization_id,
      branch_id: input.branch_id,
      user_id: input.created_by,
      action: 'reservation_created',
      object_type: 'reservation',
      object_id: newRes.id,
    });

    return newRes as Reservation;
  },

  async checkIn(reservationId: string, branchId: string, checkinTime: string, userId: string, orgId: string, roomId?: string | null): Promise<void> {
    const now = `${new Date().toISOString().split('T')[0]}T${checkinTime}:00`;
    const { error } = await supabase
      .from('reservations')
      .update({ status: 'checked_in', actual_check_in: now, check_in_time: checkinTime })
      .eq('id', reservationId);
    if (error) {
      if (error.message.includes('row-level security')) {
        throw new ReservationError('You do not have permission to check in guests for this branch.', 'permission_denied');
      }
      throw new ReservationError(error.message, 'db_error');
    }

    if (roomId) {
      await supabase.from('rooms').update({ status: 'occupied' }).eq('id', roomId);
    }

    await supabase.from('audit_logs').insert({
      organization_id: orgId,
      branch_id: branchId,
      user_id: userId,
      action: 'check_in',
      object_type: 'reservation',
      object_id: reservationId,
      new_value: { checkin_time: checkinTime },
    });
  },

  async cancelReservation(
    reservationId: string,
    branchId: string,
    userId: string,
    orgId: string,
    reason: string,
  ): Promise<void> {
    const { data: res, error: fetchErr } = await supabase
      .from('reservations')
      .select('id, status, room_id, reservation_number')
      .eq('id', reservationId)
      .maybeSingle();
    if (fetchErr || !res) {
      throw new ReservationError('Reservation not found', 'not_found');
    }
    if (res.status === 'cancelled') {
      throw new ReservationError('Reservation is already cancelled', 'already_cancelled');
    }

    const { error } = await supabase
      .from('reservations')
      .update({ status: 'cancelled' })
      .eq('id', reservationId);
    if (error) {
      if (error.message.includes('row-level security')) {
        throw new ReservationError(
          'You do not have permission to cancel reservations. Only managers and super admins can void reservations.',
          'permission_denied',
        );
      }
      throw new ReservationError(error.message, 'db_error');
    }

    if (res.room_id) {
      await supabase.from('rooms').update({ status: 'available' }).eq('id', res.room_id);
    }

    const { data: folio } = await supabase
      .from('folios')
      .select('id')
      .eq('reservation_id', reservationId)
      .maybeSingle();
    if (folio) {
      await supabase.from('folios').update({ status: 'void' }).eq('id', folio.id);
    }

    const { data: invoices } = await supabase
      .from('invoices')
      .select('id')
      .eq('reservation_id', reservationId);
    if (invoices && invoices.length > 0) {
      for (const inv of invoices) {
        await supabase.from('invoices').update({ status: 'void' }).eq('id', inv.id);
      }
    }

    await supabase.from('audit_logs').insert({
      organization_id: orgId,
      branch_id: branchId,
      user_id: userId,
      action: 'reservation_voided',
      object_type: 'reservation',
      object_id: reservationId,
      previous_value: { status: res.status, reservation_number: res.reservation_number },
      new_value: { status: 'cancelled', reason },
      reason: reason || null,
    });

    if (folio) {
      await supabase.from('audit_logs').insert({
        organization_id: orgId,
        branch_id: branchId,
        user_id: userId,
        action: 'folio_voided',
        object_type: 'folio',
        object_id: folio.id,
        new_value: { reason, reservation_id: reservationId },
        reason: reason || null,
      });
    }

    if (invoices && invoices.length > 0) {
      for (const inv of invoices) {
        await supabase.from('audit_logs').insert({
          organization_id: orgId,
          branch_id: branchId,
          user_id: userId,
          action: 'invoice_voided',
          object_type: 'invoice',
          object_id: inv.id,
          new_value: { reason, reservation_id: reservationId },
          reason: reason || null,
        });
      }
    }
  },

  async voidFolio(
    folioId: string,
    branchId: string,
    userId: string,
    orgId: string,
    reason: string,
  ): Promise<void> {
    const { data: folio, error: fetchErr } = await supabase
      .from('folios')
      .select('id, status, folio_number, reservation_id')
      .eq('id', folioId)
      .maybeSingle();
    if (fetchErr || !folio) {
      throw new ReservationError('Folio not found', 'not_found');
    }

    const { error } = await supabase
      .from('folios')
      .update({ status: 'void' })
      .eq('id', folioId);
    if (error) {
      if (error.message.includes('row-level security')) {
        throw new ReservationError(
          'You do not have permission to void folios. Only managers and super admins can void folios.',
          'permission_denied',
        );
      }
      throw new ReservationError(error.message, 'db_error');
    }

    await supabase.from('audit_logs').insert({
      organization_id: orgId,
      branch_id: branchId,
      user_id: userId,
      action: 'folio_voided',
      object_type: 'folio',
      object_id: folioId,
      previous_value: { status: folio.status, folio_number: folio.folio_number },
      new_value: { status: 'void', reason },
      reason: reason || null,
    });
  },

  async voidInvoice(
    invoiceId: string,
    branchId: string,
    userId: string,
    orgId: string,
    reason: string,
  ): Promise<void> {
    const { data: inv, error: fetchErr } = await supabase
      .from('invoices')
      .select('id, status, invoice_number')
      .eq('id', invoiceId)
      .maybeSingle();
    if (fetchErr || !inv) {
      throw new ReservationError('Invoice not found', 'not_found');
    }

    const { error } = await supabase
      .from('invoices')
      .update({ status: 'void' })
      .eq('id', invoiceId);
    if (error) {
      if (error.message.includes('row-level security')) {
        throw new ReservationError(
          'You do not have permission to void invoices. Only managers and super admins can void invoices.',
          'permission_denied',
        );
      }
      throw new ReservationError(error.message, 'db_error');
    }

    await supabase.from('audit_logs').insert({
      organization_id: orgId,
      branch_id: branchId,
      user_id: userId,
      action: 'invoice_voided',
      object_type: 'invoice',
      object_id: invoiceId,
      previous_value: { status: inv.status, invoice_number: inv.invoice_number },
      new_value: { status: 'void', reason },
      reason: reason || null,
    });
  },

  async checkOut(
    reservationId: string,
    branchId: string,
    checkoutTime: string,
    folioId: string | null,
    roomId: string | null,
    userId: string,
    orgId: string,
    balance: number,
  ): Promise<void> {
    const now = `${new Date().toISOString().split('T')[0]}T${checkoutTime}:00`;
    const { error } = await supabase
      .from('reservations')
      .update({ status: 'checked_out', actual_check_out: now, check_out_time: checkoutTime })
      .eq('id', reservationId);
    if (error) {
      if (error.message.includes('row-level security')) {
        throw new ReservationError('You do not have permission to check out guests for this branch.', 'permission_denied');
      }
      throw new ReservationError(error.message, 'db_error');
    }

    if (roomId) {
      await supabase.from('rooms').update({ status: 'dirty' }).eq('id', roomId);
    }

    if (folioId) {
      try {
        await folioService.finalize(folioId, userId);
      } catch (e) {
        console.warn('Folio finalization failed:', e);
      }
    }

    await supabase.from('audit_logs').insert({
      organization_id: orgId,
      branch_id: branchId,
      user_id: userId,
      action: 'check_out',
      object_type: 'reservation',
      object_id: reservationId,
      new_value: { checkout_time: checkoutTime, balance },
    });
  },
};
