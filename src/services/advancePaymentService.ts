import { supabase } from '@/lib/supabase';
import { getBusinessDate } from '@/services/businessDateService';
import { folioService, FinancialError } from '@/services/financial';
import type { AdvancePayment, PaymentMethod, Folio } from '@/types/database';

const RPC_ERROR_MESSAGES: Record<string, string> = {
  permission_denied: 'You do not have permission to perform this action.',
  not_found: 'This advance payment could not be found.',
  not_available: 'This advance payment has already been claimed or cancelled.',
  folio_not_found: 'The selected folio could not be found for this branch.',
  folio_not_open: 'This folio is closed and cannot receive payments.',
  not_claimed_to_folio: 'This advance payment is not applied to this folio.',
  not_cancellable: 'Only available advance payments can be cancelled.',
  reason_required: 'A reason is required to cancel an advance payment.',
};

function mapRpcError(message: string): FinancialError {
  const known = Object.keys(RPC_ERROR_MESSAGES)
    .sort((a, b) => b.length - a.length)
    .find((key) => message.includes(key));
  if (known) return new FinancialError(RPC_ERROR_MESSAGES[known], known);
  return new FinancialError(message, 'db_error');
}

export interface CreateAdvancePaymentInput {
  orgId: string;
  branchId: string;
  guestName: string;
  expectedCheckInDate: string;
  amount: number;
  method: PaymentMethod;
  notes?: string;
  userId: string;
}

export const advancePaymentService = {
  async listForBranches(branchIds: string[]): Promise<AdvancePayment[]> {
    const { data, error } = await supabase
      .from('advance_payments')
      .select('*')
      .in('branch_id', branchIds)
      .order('created_at', { ascending: false });
    if (error) throw new FinancialError(error.message, 'db_error');
    return (data || []) as AdvancePayment[];
  },

  async create(input: CreateAdvancePaymentInput): Promise<AdvancePayment> {
    if (input.amount <= 0) {
      throw new FinancialError('Amount must be greater than zero', 'invalid_amount');
    }
    const businessDate = await getBusinessDate(input.branchId);
    const { data, error } = await supabase
      .from('advance_payments')
      .insert({
        organization_id: input.orgId,
        branch_id: input.branchId,
        guest_name: input.guestName.trim(),
        expected_check_in_date: input.expectedCheckInDate,
        amount: input.amount,
        payment_method_id: input.method.id,
        payment_method_code: input.method.code,
        business_date: businessDate,
        notes: input.notes?.trim() || null,
        received_by: input.userId,
      })
      .select('*')
      .single();
    if (error) {
      if (error.message.includes('row-level security')) {
        throw new FinancialError('You do not have permission to record advance payments for this branch.', 'permission_denied');
      }
      throw new FinancialError(error.message, 'db_error');
    }

    const created = data as AdvancePayment;
    await supabase.from('audit_logs').insert({
      organization_id: input.orgId,
      branch_id: input.branchId,
      user_id: input.userId,
      action: 'advance_payment_received',
      object_type: 'advance_payment',
      object_id: created.id,
      new_value: { guest_name: input.guestName, amount: input.amount, method: input.method.code },
    });

    return created;
  },

  async applyToFolio(advance: AdvancePayment, folio: Folio, userId: string): Promise<void> {
    const { error: claimErr } = await supabase.rpc('claim_advance_payment', {
      p_advance_id: advance.id,
      p_folio_id: folio.id,
    });
    if (claimErr) throw mapRpcError(claimErr.message);

    const businessDate = await getBusinessDate(advance.branch_id);
    const { error: itemErr } = await supabase.from('folio_items').insert({
      folio_id: folio.id,
      branch_id: advance.branch_id,
      reservation_id: folio.reservation_id,
      guest_id: folio.guest_id,
      item_type: 'payment',
      category: advance.payment_method_code,
      description: `Advance payment: ${advance.guest_name}`,
      quantity: 1,
      unit_amount: -advance.amount,
      amount: -advance.amount,
      business_date: businessDate,
      created_by: userId,
      notes: `Advance payment received ${advance.business_date}`,
    });
    if (itemErr) {
      await supabase.rpc('release_advance_payment', { p_advance_id: advance.id, p_folio_id: folio.id });
      throw new FinancialError(itemErr.message, 'db_error');
    }

    await supabase.from('audit_logs').insert({
      organization_id: advance.organization_id,
      branch_id: advance.branch_id,
      user_id: userId,
      action: 'advance_payment_applied',
      object_type: 'folio',
      object_id: folio.id,
      new_value: { advance_payment_id: advance.id, amount: advance.amount },
    });

    await folioService.syncFolioTotals(folio.id);
  },

  async cancelAndRefund(advance: AdvancePayment, reason: string, userId: string): Promise<AdvancePayment> {
    const businessDate = await getBusinessDate(advance.branch_id);
    const { data, error } = await supabase.rpc('cancel_advance_payment', {
      p_advance_id: advance.id,
      p_reason: reason.trim(),
      p_business_date: businessDate,
    });
    if (error) throw mapRpcError(error.message);

    await supabase.from('audit_logs').insert({
      organization_id: advance.organization_id,
      branch_id: advance.branch_id,
      user_id: userId,
      action: 'advance_payment_refunded',
      object_type: 'advance_payment',
      object_id: advance.id,
      new_value: { status: 'cancelled', amount: advance.amount, reason: reason.trim() },
    });

    return data as AdvancePayment;
  },
};
