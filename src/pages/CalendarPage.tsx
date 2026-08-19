import { useState, useEffect, useCallback, useRef } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { Card } from '@/components/ui/Card';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatDate, todayISO, addDays } from '@/lib/format';
import { CalendarDays, ChevronLeft, ChevronRight } from 'lucide-react';
import type { Reservation, Room, Guest } from '@/types/database';

const DAY_WIDTH = 120;

export function CalendarPage({ onSelectReservation }: { onSelectReservation?: (id: string) => void }) {
  const { user, branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();
  const [rooms, setRooms] = useState<Room[]>([]);
  const [reservations, setReservations] = useState<Reservation[]>([]);
  const [guests, setGuests] = useState<Guest[]>([]);
  const [loading, setLoading] = useState(true);
  const [startDate, setStartDate] = useState(todayISO());
  const scrollRef = useRef<HTMLDivElement>(null);

  const branchIds = selectedBranchId ? [selectedBranchId] : branches.map((b) => b.id);
  const numDays = 14;
  const dates: string[] = [];
  for (let i = 0; i < numDays; i++) dates.push(addDays(startDate, i));

  const load = useCallback(async () => {
    if (branchIds.length === 0) { setLoading(false); return; }
    setLoading(true);
    const endDate = addDays(startDate, numDays);
    const [{ data: r }, { data: res }, { data: g }] = await Promise.all([
      supabase.from('rooms').select('*').in('branch_id', branchIds).eq('is_active', true).order('room_number'),
      supabase.from('reservations').select('*').in('branch_id', branchIds).in('status', ['confirmed', 'checked_in', 'tentative']).lt('check_in_date', endDate).gt('check_out_date', startDate),
      supabase.from('guests').select('*'),
    ]);
    setRooms((r as Room[]) || []);
    setReservations((res as Reservation[]) || []);
    setGuests((g as Guest[]) || []);
    setLoading(false);
  }, [branchIds, startDate]);

  useEffect(() => { load(); }, [load]);

  const guestMap = new Map(guests.map((g) => [g.id, g]));

  const getResForRoomOnDate = (roomId: string, date: string): Reservation | null => {
    return reservations.find((r) => r.room_id === roomId && r.check_in_date <= date && r.check_out_date > date) || null;
  };

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between flex-wrap gap-2">
        <h1 className="text-2xl font-bold text-slate-900">{t('nav.calendar')}</h1>
        <div className="flex items-center gap-2">
          <button onClick={() => setStartDate(addDays(startDate, -7))} className="p-2 rounded-lg border border-slate-300 hover:bg-slate-50"><ChevronLeft size={18} /></button>
          <button onClick={() => setStartDate(todayISO())} className="px-3 py-1.5 rounded-lg border border-slate-300 text-sm font-medium hover:bg-slate-50">{t('common.today')}</button>
          <button onClick={() => setStartDate(addDays(startDate, 7))} className="p-2 rounded-lg border border-slate-300 hover:bg-slate-50"><ChevronRight size={18} /></button>
        </div>
      </div>

      {rooms.length === 0 ? (
        <EmptyState icon={<CalendarDays size={48} />} title={t('rooms.no_rooms')} />
      ) : (
        <Card noPadding>
          <div className="overflow-x-auto" ref={scrollRef}>
            <div className="inline-block min-w-full">
              {/* Header row */}
              <div className="flex border-b border-slate-200 bg-slate-50 sticky top-0 z-10">
                <div className="flex-shrink-0 w-24 border-r border-slate-200 px-3 py-2 text-xs font-semibold text-slate-500">Room</div>
                {dates.map((d) => {
                  const dt = new Date(d);
                  const isToday = d === todayISO();
                  return (
                    <div key={d} className={`flex-shrink-0 px-2 py-2 text-center text-xs border-r border-slate-100 ${isToday ? 'bg-blue-50' : ''}`} style={{ width: DAY_WIDTH }}>
                      <div className={isToday ? 'font-bold text-blue-600' : 'font-medium text-slate-600'}>{dt.toLocaleDateString('en', { weekday: 'short' })}</div>
                      <div className={isToday ? 'text-blue-600 font-bold' : 'text-slate-500'}>{dt.getDate()}/{dt.getMonth() + 1}</div>
                    </div>
                  );
                })}
              </div>
              {/* Room rows */}
              {rooms.map((room) => (
                <div key={room.id} className="flex border-b border-slate-100 hover:bg-slate-50/50">
                  <div className="flex-shrink-0 w-24 border-r border-slate-200 px-3 py-2 text-sm font-medium text-slate-700">{room.room_number}</div>
                  {dates.map((d) => {
                    const res = getResForRoomOnDate(room.id, d);
                    if (!res) return <div key={d} className="flex-shrink-0 border-r border-slate-100" style={{ width: DAY_WIDTH }} />;
                    const guest = guestMap.get(res.primary_guest_id || '');
                    const isStart = res.check_in_date === d;
                    const isEnd = res.check_out_date === addDays(d, 1);
                    const color = res.status === 'checked_in' ? 'bg-emerald-500' : res.status === 'confirmed' ? 'bg-blue-500' : 'bg-amber-400';
                    return (
                      <div key={d} className="flex-shrink-0 border-r border-slate-100 relative" style={{ width: DAY_WIDTH }}>
                        {isStart && (
                          <button
                            onClick={() => onSelectReservation?.(res.id)}
                            className={`absolute top-1 left-1 right-1 bottom-1 ${color} text-white text-xs rounded px-2 py-1 truncate hover:opacity-90 transition-opacity`}
                            title={`${guest?.full_name || ''} · ${formatDate(res.check_in_date)} → ${formatDate(res.check_out_date)}`}
                          >
                            {guest?.full_name || 'Guest'}
                          </button>
                        )}
                      </div>
                    );
                  })}
                </div>
              ))}
            </div>
          </div>
        </Card>
      )}
    </div>
  );
}
