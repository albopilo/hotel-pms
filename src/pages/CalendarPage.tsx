import { useState, useEffect, useCallback, useMemo } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { Card } from '@/components/ui/Card';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatDate, todayISO, addDays } from '@/lib/format';
import { CalendarDays, ChevronLeft, ChevronRight } from 'lucide-react';
import type { Reservation, Room, Guest } from '@/types/database';

const DAY_WIDTH=120;

export function CalendarPage({onSelectReservation}:{onSelectReservation?: (id:string)=>void}) {
  const {branches}=useAuth();
  const {selectedBranchId}=useBranch();
  const {t}=useI18n();

  const [rooms,setRooms]=useState<Room[]>([]);
  const [reservations,setReservations]=useState<Reservation[]>([]);
  const [guests,setGuests]=useState<Guest[]>([]);
  const [loading,setLoading]=useState(true);
  const [startDate,setStartDate]=useState(todayISO());

  const numDays=14;

  const branchIds=useMemo(()=>selectedBranchId?[selectedBranchId]:branches.map(b=>b.id),[selectedBranchId,branches]);

  const dates=useMemo(()=>Array.from({length:numDays},(_,i)=>addDays(startDate,i)),[startDate]);

  const normalizeDate=(d:string)=>d.slice(0,10);

  const load=useCallback(async()=>{
    if(!branchIds.length){setLoading(false);return;}

    setLoading(true);

    const endDate=addDays(startDate,numDays);

    const [{data:r},{data:res},{data:g}]=await Promise.all([
      supabase.from('rooms').select('*').in('branch_id',branchIds).eq('is_active',true).order('room_number'),
      supabase.from('reservations').select('*').in('branch_id',branchIds).in('status',['confirmed','checked_in','checked_out','tentative']).lt('check_in_date',endDate).gt('check_out_date',startDate),
      supabase.from('guests').select('*')
    ]);

    setRooms((r as Room[])||[]);
    setReservations((res as Reservation[])||[]);
    setGuests((g as Guest[])||[]);
    setLoading(false);
  },[branchIds,startDate]);

  useEffect(()=>{load()},[load]);

  const guestMap=useMemo(()=>new Map(guests.map(g=>[g.id,g])),[guests]);

  const occupiedCountByDate=useMemo(()=>{
    const counts=new Map<string,number>();
    dates.forEach(d=>counts.set(d,0));
    visibleReservations.forEach(res=>{
      const ci=normalizeDate(res.check_in_date);
      const co=normalizeDate(res.check_out_date);
      dates.forEach(d=>{
        if(d>=ci&&d<co){
          counts.set(d,(counts.get(d)||0)+1);
        }
      });
    });
    return counts;
  },[visibleReservations,dates]);

  const visibleReservations=useMemo(()=>reservations.filter(r=>{
    const ci=normalizeDate(r.check_in_date);
    const co=normalizeDate(r.check_out_date);
    return ci<addDays(startDate,numDays)&&co>startDate;
  }),[reservations,startDate]);

  const reservationMap=useMemo(()=>{
    const map=new Map<string,Reservation[]>();

    rooms.forEach(room=>{
      map.set(room.id,visibleReservations.filter(r=>r.room_id===room.id));
    });

    return map;
  },[rooms,visibleReservations]);

  if(loading)return <LoadingPage message={t('common.loading')}/>;

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between flex-wrap gap-2">
        <h1 className="text-2xl font-bold text-slate-900">{t('nav.calendar')}</h1>

        <div className="flex items-center gap-2">
          <button onClick={()=>setStartDate(addDays(startDate,-7))} className="p-2 rounded-lg border border-slate-300 hover:bg-slate-50">
            <ChevronLeft size={18}/>
          </button>

          <button onClick={()=>setStartDate(todayISO())} className="px-3 py-1.5 rounded-lg border border-slate-300 text-sm font-medium hover:bg-slate-50">
            {t('common.today')}
          </button>

          <button onClick={()=>setStartDate(addDays(startDate,7))} className="p-2 rounded-lg border border-slate-300 hover:bg-slate-50">
            <ChevronRight size={18}/>
          </button>
        </div>
      </div>

      {rooms.length===0?
        <EmptyState icon={<CalendarDays size={48}/>} title={t('rooms.no_rooms')}/> : (
        <Card noPadding>
          <div className="overflow-x-auto">
            <div className="inline-block min-w-full">
                            <div className="flex border-b border-slate-200 bg-slate-50 sticky top-0 z-10">
                <div className="flex-shrink-0 w-24 border-r border-slate-200 px-3 py-2 text-xs font-semibold text-slate-500">
                  Room
                </div>

                {dates.map(d=>{
                  const dt=new Date(d);
                  const isToday=d===todayISO();
                  const occupied=occupiedCountByDate.get(d)||0;

                  return (
                    <div key={d} className={`flex-shrink-0 px-2 py-2 text-center text-xs border-r border-slate-100 ${isToday?'bg-blue-50':''}`} style={{width:DAY_WIDTH}}>
                      <div className={isToday?'font-bold text-blue-600':'font-medium text-slate-600'}>
                        {dt.toLocaleDateString('en',{weekday:'short'})}
                      </div>
                      <div className={isToday?'text-blue-600 font-bold':'text-slate-500'}>
                        {dt.getDate()}/{dt.getMonth()+1}
                      </div>
                      <div className={`mt-0.5 font-medium ${occupied>0?'text-emerald-600':'text-slate-300'}`}>
                        {occupied}/{rooms.length}
                      </div>
                    </div>
                  );
                })}
              </div>

              {rooms.map(room=>(
                <div key={room.id} className="flex border-b border-slate-100 hover:bg-slate-50/50">

                  <div className="flex-shrink-0 w-24 border-r border-slate-200 bg-white px-3 py-2 text-sm font-medium text-slate-700">
                    {room.room_number}
                  </div>

                  <div className="relative flex-shrink-0" style={{width:numDays*DAY_WIDTH,height:48}}>

                   {dates.map((d,i)=>(
  <div key={d} className="absolute top-0 bottom-0 border-r border-slate-100" style={{left:i*DAY_WIDTH,width:DAY_WIDTH}}/>
))}

                    {(reservationMap.get(room.id)||[]).map(res=>{

                      const ci=normalizeDate(res.check_in_date);
                      const co=normalizeDate(res.check_out_date);

                      const start=Math.max(
                        0,
                        Math.floor((new Date(ci).getTime()-new Date(startDate).getTime())/86400000)
                      );

                      const end=Math.min(
                        numDays,
                        Math.ceil((new Date(co).getTime()-new Date(startDate).getTime())/86400000)
                      );

                      const width=(end-start)*DAY_WIDTH;

                      if(width<=0)return null;

                      const guest=guestMap.get(res.primary_guest_id||'');

                      const color=res.status==='checked_in'
                        ?'bg-emerald-500'
                        :res.status==='confirmed'
                        ?'bg-blue-500'
                        :res.status==='checked_out'
                        ?'bg-slate-400'
                        :'bg-amber-400';

                      return (
                        <button
                          key={res.id}
                          onClick={()=>onSelectReservation?.(res.id)}
                          className={`absolute top-1 h-10 rounded px-2 text-left text-xs text-white overflow-hidden hover:opacity-90 ${color}`}
                          style={{
                            left:start*DAY_WIDTH+4,
                            width:Math.max(width-8,20)
                          }}
                          title={`${guest?.full_name||'-'} ${formatDate(res.check_in_date)} - ${formatDate(res.check_out_date)}`}
                        >
                          <div className="font-medium truncate">
                            {guest?.full_name||'Guest'}
                          </div>

                          <div className="truncate opacity-90">
                            {res.reservation_number}
                          </div>
                        </button>
                      );
                    })}

                  </div>
                </div>
                            ))}
                       </div>
          </div>
        </Card>
      )}
    </div>
  );
}