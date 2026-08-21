export function formatIDR(amount: number): string {
  return new Intl.NumberFormat('id-ID', {
    style: 'currency',
    currency: 'IDR',
    minimumFractionDigits: 0,
    maximumFractionDigits: 0,
  }).format(amount);
}

export function formatDate(date: string | Date): string {
  const d = typeof date === 'string' ? new Date(date) : date;
  return d.toLocaleDateString('en-GB', {
    day: '2-digit',
    month: 'short',
    year: 'numeric',
  });
}

export function formatDateTime(date: string | Date): string {
  const d = typeof date === 'string' ? new Date(date) : date;
  return d.toLocaleDateString('en-GB', {
    day: '2-digit',
    month: 'short',
    year: 'numeric',
  }) + ' ' + d.toLocaleTimeString('en-GB', {
    hour: '2-digit',
    minute: '2-digit',
  });
}

export function formatTime(time: string): string {
  if (!time) return '';
  return time.substring(0, 5);
}

export function nightsBetween(checkIn: string, checkOut: string): number {
  const ci = new Date(checkIn);
  const co = new Date(checkOut);
  const diff = co.getTime() - ci.getTime();
  return Math.max(1, Math.round(diff / (1000 * 60 * 60 * 24)));
}

export function todayISO(): string {
  return new Date().toISOString().split('T')[0];
}

export function addDays(dateStr: string, days: number): string {
  const d = new Date(dateStr);
  d.setDate(d.getDate() + days);
  return d.toISOString().split('T')[0];
}

export function calcTax(amount: number, taxRate: number): number {
  return Math.round((amount * taxRate) / 100);
}

export function timeDiffHours(standard: string, actual: string): number {
  const [sh, sm] = standard.split(':').map(Number);
  const [ah, am] = actual.split(':').map(Number);
  return (ah * 60 + am - sh * 60 - sm) / 60;
}

export function isEarlyCheckin(standardTime: string, actualTime: string): boolean {
  return timeDiffHours(standardTime, actualTime) < 0;
}

export function isLateCheckout(standardTime: string, actualTime: string): boolean {
  return timeDiffHours(standardTime, actualTime) > 0;
}

export function formatHoursShort(hours: number): string {
  const absH = Math.abs(hours);
  const h = Math.floor(absH);
  const m = Math.round((absH - h) * 60);
  const sign = hours < 0 ? '-' : '+';
  return `${sign}${h}h${m > 0 ? ` ${m}m` : ''}`;
}
