import type { RoomType, IndonesianHoliday } from '@/types/database';

export type RateType = 'weekday' | 'weekend' | 'base';

export interface RateResult {
  rate: number;
  rateType: RateType;
}

export function isHolidayDate(dateStr: string, holidays: IndonesianHoliday[]): boolean {
  const date = dateStr.slice(0, 10);
  return holidays.some((h) => h.is_active && h.holiday_date === date);
}

export function isDayBeforeHoliday(dateStr: string, holidays: IndonesianHoliday[]): boolean {
  const [y, m, d] = dateStr.slice(0, 10).split('-').map(Number);
  const date = new Date(y, m - 1, d);
  date.setDate(date.getDate() + 1);
  const nextDay = `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, '0')}-${String(date.getDate()).padStart(2, '0')}`;
  return holidays.some((h) => h.is_active && h.holiday_date === nextDay);
}

export function isWeekendDate(dateStr: string): boolean {
  const [y, m, d] = dateStr.slice(0, 10).split('-').map(Number);
  const day = new Date(y, m - 1, d).getDay();
  return day === 0 || day === 5 || day === 6;
}

export function getRateTypeForDate(
  dateStr: string,
  holidays: IndonesianHoliday[]
): RateType {
  const date = dateStr.slice(0, 10);
  if (isHolidayDate(date, holidays) || isDayBeforeHoliday(date, holidays)) {
    return 'weekend';
  }
  if (isWeekendDate(date)) {
    return 'weekend';
  }
  return 'weekday';
}

export function getRateForDate(
  dateStr: string,
  roomType: RoomType,
  holidays: IndonesianHoliday[]
): RateResult {
  const rateType = getRateTypeForDate(dateStr, holidays);
  if (rateType === 'weekend' && roomType.weekend_rate > 0) {
    return { rate: roomType.weekend_rate, rateType };
  }
  if (rateType === 'weekday' && roomType.weekday_rate > 0) {
    return { rate: roomType.weekday_rate, rateType };
  }
  return { rate: roomType.base_rate, rateType: 'base' };
}

export function calculateTotalRate(
  checkInDate: string,
  checkOutDate: string,
  roomType: RoomType,
  holidays: IndonesianHoliday[]
): { total: number; breakdown: { date: string; rate: number; rateType: RateType }[] } {
  const breakdown: { date: string; rate: number; rateType: RateType }[] = [];
  let total = 0;
  const start = new Date(checkInDate.slice(0, 10));
  const end = new Date(checkOutDate.slice(0, 10));
  const cursor = new Date(start);

  while (cursor < end) {
    const dateStr = `${cursor.getFullYear()}-${String(cursor.getMonth() + 1).padStart(2, '0')}-${String(cursor.getDate()).padStart(2, '0')}`;
    const { rate, rateType } = getRateForDate(dateStr, roomType, holidays);
    breakdown.push({ date: dateStr, rate, rateType });
    total += rate;
    cursor.setDate(cursor.getDate() + 1);
  }

  return { total, breakdown };
}

export function getRateTypeLabel(rateType: RateType, language: 'en' | 'id' = 'en'): string {
  if (language === 'id') {
    return { weekday: 'Hari Kerja', weekend: 'Akhir Pekan', base: 'Tarif Dasar' }[rateType];
  }
  return { weekday: 'Weekday', weekend: 'Weekend', base: 'Base Rate' }[rateType];
}
