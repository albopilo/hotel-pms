export type UserRole = 'super_admin' | 'manager' | 'receptionist';

export type RoomStatus =
  | 'available'
  | 'reserved'
  | 'occupied'
  | 'dirty'
  | 'cleaning'
  | 'inspected'
  | 'out_of_service'
  | 'out_of_order';

export type ReservationStatus =
  | 'tentative'
  | 'confirmed'
  | 'checked_in'
  | 'checked_out'
  | 'cancelled'
  | 'no_show';

export type PaymentStatus = 'unpaid' | 'partial' | 'paid';

export type FolioStatus = 'open' | 'finalized' | 'void';

export type InvoiceStatus =
  | 'draft'
  | 'open'
  | 'partial'
  | 'paid'
  | 'void'
  | 'refunded'
  | 'adjusted'
  | 'additional_charge';

export type FolioItemType =
  | 'charge'
  | 'payment'
  | 'discount'
  | 'tax'
  | 'adjustment'
  | 'deposit'
  | 'refund';

export type CardIssuanceType = 'issue' | 'replace' | 'invalidate' | 'extend';
export type CardIssuanceStatus = 'pending' | 'success' | 'failed';
export type LockProviderType = 'mock' | 'production';

export interface Organization {
  id: string;
  name: string;
  legal_name: string | null;
  address: string | null;
  phone: string | null;
  email: string | null;
  tax_id: string | null;
  currency: string;
  timezone: string;
  logo_url: string | null;
  created_at: string;
  updated_at: string;
}

export interface Branch {
  id: string;
  organization_id: string;
  name: string;
  code: string;
  address: string | null;
  phone: string | null;
  email: string | null;
  tax_id: string | null;
  timezone: string;
  standard_checkin_time: string;
  standard_checkout_time: string;
  business_day_cutoff: string;
  is_active: boolean;
  created_at: string;
  updated_at: string;
}

export interface Profile {
  id: string;
  organization_id: string;
  full_name: string;
  email: string;
  role: UserRole;
  phone: string | null;
  is_active: boolean;
  language: 'en' | 'id';
  created_at: string;
  updated_at: string;
}

export interface UserBranchAccess {
  id: string;
  user_id: string;
  branch_id: string;
  created_at: string;
}

export interface RoomType {
  id: string;
  branch_id: string;
  name: string;
  code: string;
  description: string | null;
  base_rate: number;
  max_occupancy: number;
  default_tax_rate: number;
  is_active: boolean;
  sort_order: number;
  created_at: string;
  updated_at: string;
}

export interface Room {
  id: string;
  branch_id: string;
  room_type_id: string;
  room_number: string;
  floor: number;
  base_rate: number;
  max_occupancy: number;
  status: RoomStatus;
  is_active: boolean;
  notes: string | null;
  out_of_service_reason: string | null;
  out_of_service_until: string | null;
  created_at: string;
  updated_at: string;
}

export interface Guest {
  id: string;
  organization_id: string;
  full_name: string;
  id_type: string | null;
  id_number: string | null;
  nationality: string | null;
  gender: string | null;
  date_of_birth: string | null;
  phone: string | null;
  email: string | null;
  address: string | null;
  company: string | null;
  notes: string | null;
  created_at: string;
  updated_at: string;
}

export interface BookingSource {
  id: string;
  organization_id: string;
  name: string;
  code: string;
  is_ota: boolean;
  is_active: boolean;
  sort_order: number;
  created_at: string;
}

export interface Reservation {
  id: string;
  branch_id: string;
  organization_id: string;
  reservation_number: string;
  primary_guest_id: string | null;
  room_type_id: string | null;
  room_id: string | null;
  adults: number;
  children: number;
  check_in_date: string;
  check_in_time: string;
  check_out_date: string;
  check_out_time: string;
  actual_check_in: string | null;
  actual_check_out: string | null;
  num_nights: number;
  rate: number;
  discount: number;
  tax: number;
  deposit: number;
  booking_source_id: string | null;
  payment_status: PaymentStatus;
  status: ReservationStatus;
  special_requests: string | null;
  notes: string | null;
  created_by: string | null;
  parent_reservation_id: string | null;
  is_group: boolean;
  parent_reservation_id: string | null;
  is_group: boolean;
  created_at: string;
  updated_at: string;
export interface ReservationRoom {
  id: string;
  reservation_id: string;
  branch_id: string;
  room_id: string | null;
  room_type_id: string | null;
  rate: number;
  check_in_date: string;
  check_out_date: string;
  num_nights: number;
  status: string;
  created_at: string;
  updated_at: string;
}

}

export interface ReservationRoom {
  id: string;
  reservation_id: string;
  branch_id: string;
  room_id: string | null;
  room_type_id: string | null;
  rate: number;
  check_in_date: string;
  check_out_date: string;
  num_nights: number;
  status: string;
  created_at: string;
  updated_at: string;
}

export interface Folio {
  id: string;
  branch_id: string;
  reservation_id: string;
  guest_id: string | null;
  folio_number: string;
  status: FolioStatus;
  total_charges: number;
  total_payments: number;
  total_discounts: number;
  total_tax: number;
  deposit: number;
  balance: number;
  finalized_at: string | null;
  finalized_by: string | null;
  created_at: string;
  updated_at: string;
}

export interface FolioItem {
  id: string;
  folio_id: string;
  branch_id: string;
  reservation_id: string | null;
  guest_id: string | null;
  room_id: string | null;
  item_type: FolioItemType;
  category: string | null;
  description: string;
  quantity: number;
  unit_amount: number;
  amount: number;
  business_date: string;
  is_post_stay: boolean;
  voided: boolean;
  voided_by: string | null;
  voided_at: string | null;
  approved_by: string | null;
  created_by: string;
  notes: string | null;
  created_at: string;
}

export interface PaymentMethod {
  id: string;
  organization_id: string;
  name: string;
  code: string;
  is_edc: boolean;
  is_ota: boolean;
  is_cash: boolean;
  is_active: boolean;
  sort_order: number;
  created_at: string;
}

export interface ChargeCategory {
  id: string;
  organization_id: string;
  name: string;
  code: string;
  is_damage: boolean;
  requires_approval: boolean;
  approval_threshold: number;
  is_active: boolean;
  sort_order: number;
  created_at: string;
}

export interface Invoice {
  id: string;
  branch_id: string;
  reservation_id: string | null;
  folio_id: string | null;
  guest_id: string | null;
  invoice_number: string;
  status: InvoiceStatus;
  subtotal: number;
  discount: number;
  tax: number;
  total: number;
  amount_paid: number;
  balance: number;
  issued_at: string | null;
  issued_by: string | null;
  finalized_at: string | null;
  finalized_by: string | null;
  notes: string | null;
  created_at: string;
  updated_at: string;
}

export interface InvoiceItem {
  id: string;
  invoice_id: string;
  description: string;
  category: string | null;
  quantity: number;
  unit_amount: number;
  amount: number;
  sort_order: number;
  created_at: string;
}

export interface AuditLog {
  id: string;
  branch_id: string | null;
  organization_id: string;
  user_id: string | null;
  action: string;
  object_type: string | null;
  object_id: string | null;
  previous_value: Record<string, unknown> | null;
  new_value: Record<string, unknown> | null;
  reason: string | null;
  created_at: string;
}

export interface HotelLockIntegration {
  id: string;
  branch_id: string;
  provider_type: LockProviderType;
  lock_system: string;
  lock_model: string;
  card_technology: string;
  bridge_url: string | null;
  bridge_token: string | null;
  is_enabled: boolean;
  connection_status: 'connected' | 'disconnected';
  encoder_status: 'connected' | 'disconnected';
  last_heartbeat: string | null;
  last_success_encoding: string | null;
  last_error: string | null;
  created_at: string;
  updated_at: string;
}

export interface HotelLockEvent {
  id: string;
  integration_id: string | null;
  branch_id: string;
  event_type: string;
  event_data: Record<string, unknown> | null;
  status: 'info' | 'success' | 'warning' | 'error';
  message: string;
  created_at: string;
}

export interface CardIssuance {
  id: string;
  branch_id: string;
  reservation_id: string;
  guest_id: string | null;
  room_id: string | null;
  issuance_type: CardIssuanceType;
  card_sequence: number;
  valid_from: string | null;
  valid_until: string | null;
  status: CardIssuanceStatus;
  failure_reason: string | null;
  provider_type: LockProviderType;
  performed_by: string;
  created_at: string;
}

export interface SystemSetting {
  id: string;
  organization_id: string;
  key: string;
  value: string;
  value_type: 'string' | 'number' | 'boolean' | 'json';
  description: string | null;
  category: string;
  created_at: string;
  updated_at: string;
}

export interface HotelBusinessDate {
  id: string;
  branch_id: string;
  business_date: string;
  status: 'open' | 'closed';
  closed_at: string | null;
  closed_by: string | null;
  created_at: string;
}

export interface NightAudit {
  id: string;
  branch_id: string;
  business_date: string;
  summary: Record<string, unknown>;
  exceptions: unknown[];
  arrivals: number;
  departures: number;
  in_house: number;
  checked_in: number;
  checked_out: number;
  no_shows: number;
  cancellations: number;
  room_charges: number;
  additional_charges: number;
  payments: number;
  cash: number;
  edc: number;
  ota: number;
  refunds: number;
  discounts: number;
  outstanding: number;
  closed_at: string;
  closed_by: string;
  created_at: string;
}

export interface RoomTransfer {
  id: string;
  reservation_id: string;
export interface ReservationRoomWithRelations extends ReservationRoom {
  room?: Room | null;
  room_type?: RoomType | null;
}

  from_room_id: string | null;
  to_room_id: string;
  reason: string | null;
  performed_by: string;
  created_at: string;
}

  reservation_rooms?: ReservationRoomWithRelations[];
export interface RoomStatusHistory {
  id: string;
  room_id: string;
  previous_status: string | null;
  new_status: string;
  changed_by: string | null;
  reason: string | null;
  revert_after_nights: number | null;
  revert_to: string | null;
  created_at: string;
}

export interface ReservationRoomWithRelations extends ReservationRoom {
  room?: Room | null;
  room_type?: RoomType | null;
}

export interface ReservationWithRelations extends Reservation {
  primary_guest?: Guest | null;
  room?: Room | null;
  room_type?: RoomType | null;
  booking_source?: BookingSource | null;
  branch?: Branch | null;
  folio?: Folio | null;
  reservation_rooms?: ReservationRoomWithRelations[];
}

export interface RoomWithRelations extends Room {
  room_type?: RoomType | null;
  branch?: Branch | null;
  current_reservation?: ReservationWithRelations | null;
}
