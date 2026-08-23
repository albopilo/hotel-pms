import { supabase } from '@/lib/supabase';
import type { Folio, FolioItem, PaymentMethod, ChargeCategory } from '@/types/database';
import { getBusinessDate } from '@/services/businessDateService';

export interface FolioTotals {
  totalCharges: number;
  totalPayments: number;
  totalDiscounts: number;
  totalTax: number;
  netBalance: number;
}

export interface PaymentInput {
  folioId: string;
  branchId: string;
  reservationId: string;
  guestId: string | null;
  methodId: string;
  amount: number;
  subtype?: string;
  edcTerminal?: string;
  referenceNumber?: string;
  approvalCode?: string;
  notes?: string;
  userId: string;
  orgId: string;
}

export interface ChargeInput {
  folioId: string;
  branchId: string;
  reservationId: string;
  guestId: string | null;
  roomId: string | null;
  categoryId?: string;
  description: string;
  amount: number;
  quantity?: number;
  notes?: string;
  userId: string;
  orgId: string;
}

export class FinancialError extends Error {
  constructor(message: string, public code: string = 'financial_error') {
    super(message);
    this.name = 'FinancialError';
  }
}

function assertDefined(value: unknown, field: string): asserts value is string {
  if (value === null || value === undefined || value === '') {
    throw new FinancialError(`${field} is required`, 'missing_field');
  }
}

export const folioService = {
  async syncFolioTotals(folioId: string): Promise<FolioTotals> {
    const totals = await this.getTotals(folioId);
    await supabase.from('folios').update({
      total_charges: totals.totalCharges,
      total_payments: totals.totalPayments,
      total_discounts: totals.totalDiscounts,
      total_tax: totals.totalTax,
      balance: totals.netBalance,
    }).eq('id', folioId);
    return totals;
  },

  async getTotals(folioId: string): Promise<FolioTotals> {
    const { data, error } = await supabase
      .from('folio_items')
      .select('item_type, amount, voided')
      .eq('folio_id', folioId);
    if (error) throw new FinancialError(error.message, 'db_error');

    const items = (data || []) as FolioItem[];
    const active = items.filter((i) => !i.voided);
    const charges = active.filter((i) => i.item_type === 'charge' && i.amount > 0);
    const payments = active.filter((i) => i.item_type === 'payment');
    const discounts = active.filter((i) => i.item_type === 'discount');
    const taxes = active.filter((i) => i.item_type === 'tax');

    const totalCharges = charges.reduce((s, i) => s + i.amount, 0);
    const totalPayments = payments.reduce((s, i) => s + Math.abs(i.amount), 0);
    const totalDiscounts = discounts.reduce((s, i) => s + Math.abs(i.amount), 0);
    const totalTax = taxes.reduce((s, i) => s + i.amount, 0);
    const netBalance = totalCharges + totalTax - totalDiscounts - totalPayments;

    return { totalCharges, totalPayments, totalDiscounts, totalTax, netBalance };
  },

  async getFolioWithItems(folioId: string): Promise<{ folio: Folio; items: FolioItem[] } | null> {
    const { data: folio, error: folioErr } = await supabase
      .from('folios')
      .select('*')
      .eq('id', folioId)
      .maybeSingle();
    if (folioErr) throw new FinancialError(folioErr.message, 'db_error');
    if (!folio) return null;

    const { data: items, error: itemsErr } = await supabase
      .from('folio_items')
      .select('*')
      .eq('folio_id', folioId)
      .order('created_at');
    if (itemsErr) throw new FinancialError(itemsErr.message, 'db_error');

    return { folio: folio as Folio, items: (items || []) as FolioItem[] };
  },

  async voidItem(itemId: string, folioId: string, userId: string, orgId: string, branchId: string): Promise<void> {
    const { data: item, error: fetchErr } = await supabase
      .from('folio_items')
      .select('item_type, payment_id, additional_charge_id, description, amount')
      .eq('id', itemId)
      .maybeSingle();
    if (fetchErr) throw new FinancialError(fetchErr.message, 'db_error');
    if (!item) throw new FinancialError('Folio item not found', 'not_found');

    const voidedAt = new Date().toISOString();
    const { error } = await supabase
      .from('folio_items')
      .update({ voided: true, voided_by: userId, voided_at: voidedAt })
      .eq('id', itemId);
    if (error) {
      if (error.message.includes('row-level security')) {
        throw new FinancialError('You do not have permission to void this item. Only managers can void charges.', 'permission_denied');
      }
      throw new FinancialError(error.message, 'db_error');
    }

    if (item.payment_id) {
      await supabase
        .from('payments')
        .update({ voided: true, voided_by: userId, voided_at: voidedAt })
        .eq('id', item.payment_id);
    }

    if (item.additional_charge_id) {
      await supabase
        .from('additional_charges')
        .update({ status: 'voided' })
        .eq('id', item.additional_charge_id);
    }

    const actionLabel = item.item_type === 'payment' ? 'payment_voided' : 'charge_voided';
    await supabase.from('audit_logs').insert({
      organization_id: orgId,
      branch_id: branchId,
      user_id: userId,
      action: actionLabel,
      object_type: 'folio_item',
      object_id: itemId,
      previous_value: { folio_id: folioId, description: item.description, amount: item.amount },
    });

    await this.syncFolioTotals(folioId);
  },

  async finalize(folioId: string, userId: string): Promise<void> {
    const totals = await this.getTotals(folioId);
    const { error } = await supabase
      .from('folios')
      .update({
        status: 'finalized',
        finalized_at: new Date().toISOString(),
        finalized_by: userId,
        total_charges: totals.totalCharges,
        total_payments: totals.totalPayments,
        total_discounts: totals.totalDiscounts,
        total_tax: totals.totalTax,
        balance: totals.netBalance,
      })
      .eq('id', folioId);
    if (error) {
      if (error.message.includes('row-level security')) {
        throw new FinancialError('Cannot finalize this folio. Ensure you have manager access.', 'permission_denied');
      }
      throw new FinancialError(error.message, 'db_error');
    }
  },
};

export const paymentService = {
  async recordPayment(input: PaymentInput, methods: PaymentMethod[]): Promise<void> {
    assertDefined(input.methodId, 'Payment method');
    assertDefined(input.folioId, 'Folio');
    if (input.amount <= 0) {
      throw new FinancialError('Payment amount must be greater than zero', 'invalid_amount');
    }

    const method = methods.find((m) => m.id === input.methodId);
    if (!method) throw new FinancialError('Invalid payment method', 'invalid_method');

    const payNum = `PAY-${new Date().getFullYear()}-${crypto.randomUUID().slice(0,8).toUpperCase()}`;

    const { data: payRow, error: payErr } = await supabase.from('payments').insert({
      branch_id: input.branchId,
      reservation_id: input.reservationId,
      folio_id: input.folioId,
      guest_id: input.guestId,
      payment_number: payNum,
      amount: input.amount,
      payment_method_id: method.id,
      payment_method_code: method.code,
      payment_subtype: input.subtype || null,
      edc_terminal: input.edcTerminal || null,
      reference_number: input.referenceNumber || null,
      approval_code: input.approvalCode || null,
      is_ota: method.is_ota,
      business_date: await getBusinessDate(input.branchId),
      created_by: input.userId,
      notes: input.notes || null,
    }).select('id').single();
    if (payErr) {
      if (payErr.message.includes('row-level security')) {
        throw new FinancialError('You do not have permission to record payments for this branch.', 'permission_denied');
      }
      throw new FinancialError(payErr.message, 'db_error');
    }

    const { error: folioErr } = await supabase.from('folio_items').insert({
      folio_id: input.folioId,
      branch_id: input.branchId,
      reservation_id: input.reservationId,
      guest_id: input.guestId,
      item_type: 'payment',
      category: method.code,
      description: `Payment: ${method.name}${input.subtype ? ` (${input.subtype})` : ''}`,
      quantity: 1,
      unit_amount: -input.amount,
      amount: -input.amount,
      business_date: await getBusinessDate(input.branchId),
      created_by: input.userId,
      notes: input.notes || null,
      payment_id: payRow.id,
    });
    if (folioErr) throw new FinancialError(folioErr.message, 'db_error');

    const { error: txnErr } = await supabase.from('transactions').insert({
      branch_id: input.branchId,
      organization_id: input.orgId,
      reservation_id: input.reservationId,
      guest_id: input.guestId,
      folio_id: input.folioId,
      transaction_type: 'payment',
      description: `Payment ${method.name}${input.subtype ? ` ${input.subtype}` : ''}`,
      amount: input.amount,
      debit_credit: 'credit',
      payment_method_code: method.code,
      reference_number: input.referenceNumber || null,
      business_date: await getBusinessDate(input.branchId),
      created_by: input.userId,
    });
    if (txnErr) throw new FinancialError(txnErr.message, 'db_error');

    await supabase.from('audit_logs').insert({
      organization_id: input.orgId,
      branch_id: input.branchId,
      user_id: input.userId,
      action: 'payment',
      object_type: 'folio',
      object_id: input.folioId,
      new_value: { amount: input.amount, method: method.code, subtype: input.subtype },
    });

    await folioService.syncFolioTotals(input.folioId);
  },
};

export const chargeService = {
  async addCharge(input: ChargeInput, categories: ChargeCategory[]): Promise<void> {
    assertDefined(input.description, 'Description');
    if (input.amount <= 0) {
      throw new FinancialError('Charge amount must be greater than zero', 'invalid_amount');
    }

    const cat = categories.find((c) => c.id === input.categoryId);
    const amount = input.amount * (input.quantity || 1);
    const needsApproval = cat?.requires_approval && amount > (cat?.approval_threshold || 0);

    const { data: acRow, error: acErr } = await supabase.from('additional_charges').insert({
      branch_id: input.branchId,
      reservation_id: input.reservationId,
      folio_id: input.folioId,
      guest_id: input.guestId,
      room_id: input.roomId,
      charge_category_id: cat?.id || null,
      category_code: cat?.code || 'miscellaneous',
      description: input.description,
      amount: input.amount,
      quantity: input.quantity || 1,
      is_damage: cat?.is_damage || false,
      is_post_stay: false,
      requires_approval: cat?.requires_approval || false,
      approved_by: needsApproval ? null : input.userId,
      approved_at: needsApproval ? null : new Date().toISOString(),
      status: needsApproval ? 'pending_approval' : 'posted',
      business_date: await getBusinessDate(input.branchId),
      created_by: input.userId,
      notes: input.notes || null,
    }).select('id').single();
    if (acErr) throw new FinancialError(acErr.message, 'db_error');

    const { error } = await supabase.from('folio_items').insert({
      folio_id: input.folioId,
      branch_id: input.branchId,
      reservation_id: input.reservationId,
      guest_id: input.guestId,
      room_id: input.roomId,
      item_type: 'charge',
      category: cat?.code || 'miscellaneous',
      description: input.description,
      quantity: input.quantity || 1,
      unit_amount: input.amount,
      amount,
      business_date: await getBusinessDate(input.branchId),
      created_by: input.userId,
      notes: input.notes || null,
      approved_by: needsApproval ? null : input.userId,
      additional_charge_id: acRow.id,
    });
    if (error) {
      if (error.message.includes('row-level security')) {
        throw new FinancialError('You do not have permission to post charges to this folio.', 'permission_denied');
      }
      throw new FinancialError(error.message, 'db_error');
    }

    const { error: txnErr } = await supabase.from('transactions').insert({
      branch_id: input.branchId,
      organization_id: input.orgId,
      reservation_id: input.reservationId,
      guest_id: input.guestId,
      folio_id: input.folioId,
      transaction_type: cat?.is_damage ? 'damage_charge' : 'additional_charge',
      description: input.description,
      amount,
      debit_credit: 'debit',
      business_date: await getBusinessDate(input.branchId),
      created_by: input.userId,
    });
    if (txnErr) throw new FinancialError(txnErr.message, 'db_error');

    await supabase.from('audit_logs').insert({
      organization_id: input.orgId,
      branch_id: input.branchId,
      user_id: input.userId,
      action: cat?.is_damage ? 'damage_charge' : 'additional_charge',
      object_type: 'folio',
      object_id: input.folioId,
      new_value: { description: input.description, amount, category: cat?.code },
    });

    await folioService.syncFolioTotals(input.folioId);
  },

  async addPostStayCharge(input: ChargeInput, categories: ChargeCategory[]): Promise<void> {
    assertDefined(input.description, 'Description');
    if (input.amount <= 0) {
      throw new FinancialError('Charge amount must be greater than zero', 'invalid_amount');
    }

    const cat = categories.find((c) => c.id === input.categoryId);

    const { data: acRow, error: acErr } = await supabase.from('additional_charges').insert({
      branch_id: input.branchId,
      reservation_id: input.reservationId,
      folio_id: input.folioId,
      guest_id: input.guestId,
      room_id: input.roomId,
      charge_category_id: cat?.id || null,
      category_code: cat?.code || 'post_stay',
      description: input.description,
      amount: input.amount,
      quantity: 1,
      is_post_stay: true,
      status: 'posted',
      business_date: await getBusinessDate(input.branchId),
      created_by: input.userId,
      notes: input.notes || null,
    }).select('id').single();
    if (acErr) throw new FinancialError(acErr.message, 'db_error');

    const { error: fiErr } = await supabase.from('folio_items').insert({
      folio_id: input.folioId,
      branch_id: input.branchId,
      reservation_id: input.reservationId,
      guest_id: input.guestId,
      room_id: input.roomId,
      item_type: 'charge',
      category: cat?.code || 'post_stay',
      description: `POST-STAY: ${input.description}`,
      quantity: 1,
      unit_amount: input.amount,
      amount: input.amount,
      business_date: await getBusinessDate(input.branchId),
      is_post_stay: true,
      created_by: input.userId,
      notes: input.notes || null,
      additional_charge_id: acRow.id,
    });
    if (fiErr) {
      if (fiErr.message.includes('row-level security')) {
        throw new FinancialError('Cannot add post-stay charge. Manager access may be required for finalized folios.', 'permission_denied');
      }
      throw new FinancialError(fiErr.message, 'db_error');
    }

    const { error: txnErr } = await supabase.from('transactions').insert({
      branch_id: input.branchId,
      organization_id: input.orgId,
      reservation_id: input.reservationId,
      guest_id: input.guestId,
      folio_id: input.folioId,
      transaction_type: 'post_stay_charge',
      description: `Post-stay: ${input.description}`,
      amount: input.amount,
      debit_credit: 'debit',
      business_date: await getBusinessDate(input.branchId),
      created_by: input.userId,
    });
    if (txnErr) throw new FinancialError(txnErr.message, 'db_error');

    await supabase.from('audit_logs').insert({
      organization_id: input.orgId,
      branch_id: input.branchId,
      user_id: input.userId,
      action: 'post_stay_charge',
      object_type: 'folio',
      object_id: input.folioId,
      new_value: { description: input.description, amount: input.amount },
    });

    await folioService.syncFolioTotals(input.folioId);
  },
};
