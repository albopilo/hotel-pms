import { type ReactNode } from 'react';

interface BadgeProps {
  children: ReactNode;
  color?: 'green' | 'red' | 'blue' | 'amber' | 'gray' | 'purple' | 'orange' | 'teal';
  size?: 'sm' | 'md';
}

const colors: Record<string, string> = {
  green: 'bg-emerald-100 text-emerald-700 border-emerald-200',
  red: 'bg-red-100 text-red-700 border-red-200',
  blue: 'bg-blue-100 text-blue-700 border-blue-200',
  amber: 'bg-amber-100 text-amber-700 border-amber-200',
  gray: 'bg-slate-100 text-slate-600 border-slate-200',
  purple: 'bg-purple-100 text-purple-700 border-purple-200',
  orange: 'bg-orange-100 text-orange-700 border-orange-200',
  teal: 'bg-teal-100 text-teal-700 border-teal-200',
};

export function Badge({ children, color = 'gray', size = 'sm' }: BadgeProps) {
  return (
    <span className={`inline-flex items-center rounded-full border font-medium ${colors[color]} ${size === 'sm' ? 'px-2.5 py-0.5 text-xs' : 'px-3 py-1 text-sm'}`}>
      {children}
    </span>
  );
}

const roomStatusColors: Record<string, 'green' | 'red' | 'blue' | 'amber' | 'gray' | 'orange' | 'teal'> = {
  available: 'green',
  reserved: 'blue',
  occupied: 'red',
  dirty: 'amber',
  cleaning: 'teal',
  inspected: 'green',
  out_of_service: 'gray',
  out_of_order: 'gray',
};

const resStatusColors: Record<string, 'gray' | 'blue' | 'green' | 'amber' | 'red'> = {
  tentative: 'gray',
  confirmed: 'blue',
  checked_in: 'green',
  checked_out: 'gray',
  cancelled: 'red',
  no_show: 'amber',
};

const invoiceStatusColors: Record<string, 'gray' | 'blue' | 'green' | 'red' | 'amber'> = {
  draft: 'gray',
  open: 'blue',
  partial: 'amber',
  paid: 'green',
  void: 'red',
  refunded: 'amber',
  adjusted: 'blue',
  additional_charge: 'orange' as never,
};

export function RoomStatusBadge({ status, label }: { status: string; label: string }) {
  return <Badge color={roomStatusColors[status] || 'gray'}>{label}</Badge>;
}

export function ResStatusBadge({ status, label }: { status: string; label: string }) {
  return <Badge color={resStatusColors[status] || 'gray'}>{label}</Badge>;
}

export function InvoiceStatusBadge({ status, label }: { status: string; label: string }) {
  const color = (invoiceStatusColors[status] || 'gray') as BadgeProps['color'];
  return <Badge color={color}>{label}</Badge>;
}
