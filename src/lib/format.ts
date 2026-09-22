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
    timeZone: 'Asia/Jakarta',
  });
}

export function formatDateTime(date: string | Date): string {
  const d = typeof date === 'string' ? new Date(date) : date;
  return d.toLocaleDateString('en-GB', {
    day: '2-digit',
    month: 'short',
    year: 'numeric',
    timeZone: 'Asia/Jakarta',
  }) + ' ' + d.toLocaleTimeString('en-GB', {
    hour: '2-digit',
    minute: '2-digit',
    timeZone: 'Asia/Jakarta',
  });
}

export function formatTime(time: string): string {
  if (!time) return '';
  return time.substring(0, 5);
}

export function nightsBetween(checkIn: string, checkOut: string): number {
  const [cy, cm, cd] = checkIn.slice(0, 10).split('-').map(Number);
  const [oy, om, od] = checkOut.slice(0, 10).split('-').map(Number);
  const ci = new Date(cy, cm - 1, cd);
  const co = new Date(oy, om - 1, od);
  const diff = co.getTime() - ci.getTime();
  return Math.max(1, Math.round(diff / (1000 * 60 * 60 * 24)));
}

export function todayISO(): string {
  return todayInTimezone('Asia/Jakarta');
}

export function todayInTimezone(timezone: string = 'Asia/Jakarta'): string {
  const now = new Date();
  const localStr = now.toLocaleString('en-CA', { timeZone: timezone, year: 'numeric', month: '2-digit', day: '2-digit' });
  return localStr; // en-CA gives YYYY-MM-DD
}

export function nowInTimezone(timezone: string = 'Asia/Jakarta'): string {
  const now = new Date();
  return now.toLocaleTimeString('en-GB', { timeZone: timezone, hour: '2-digit', minute: '2-digit', hour12: false });
}

export function jakartaYear(timezone: string = 'Asia/Jakarta'): number {
  const now = new Date();
  return Number(now.toLocaleString('en-CA', { timeZone: timezone, year: 'numeric' }));
}

export function jakartaTodayWithOffset(timezone: string = 'Asia/Jakarta'): string {
  return todayInTimezone(timezone) + 'T00:00:00+07:00';
}

export function jakartaEndOfDayWithOffset(dateStr: string): string {
  return dateStr + 'T23:59:59+07:00';
}

export function addDays(dateStr: string, days: number): string {
  const [y, m, d] = dateStr.slice(0, 10).split('-').map(Number);
  const dt = new Date(y, m - 1, d);
  dt.setDate(dt.getDate() + days);
  const yy = dt.getFullYear();
  const mm = String(dt.getMonth() + 1).padStart(2, '0');
  const dd = String(dt.getDate()).padStart(2, '0');
  return `${yy}-${mm}-${dd}`;
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
