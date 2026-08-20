import { useState,useEffect,useCallback,useMemo } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal } from '@/components/ui/Modal';
import { Input,Select,Textarea } from '@/components/ui/Form';
import { ResStatusBadge } from '@/components/ui/Badge';
import { LoadingPage,EmptyState } from '@/components/ui/States';
import { formatIDR,formatDate,todayISO,addDays,nightsBetween } from '@/lib/format';
import { Plus,CalendarDays,Search } from 'lucide-react';
import type { Reservation,Guest,Room,RoomType,BookingSource,Branch } from '@/types/database';

export function ReservationsPage({searchQuery='',onSelectReservation}:{searchQuery?:string;onSelectReservation?:(id:string)=>void}){
const {user,branches}=useAuth();const {selectedBranchId}=useBranch();const {t}=useI18n();const {showToast}=useToast();
const [reservations,setReservations]=useState<Reservation[]>([]),[guests,setGuests]=useState<Guest[]>([]),[rooms,setRooms]=useState<Room[]>([]),[roomTypes,setRoomTypes]=useState<RoomType[]>([]),[bookingSources,setBookingSources]=useState<BookingSource[]>([]);
const [loading,setLoading]=useState(true),[showForm,setShowForm]=useState(false),[statusFilter,setStatusFilter]=useState('all'),[localSearch,setLocalSearch]=useState(searchQuery);
const branchIds=useMemo(()=>selectedBranchId?[selectedBranchId]:branches.map(b=>b.id),[selectedBranchId,branches]);

const load=useCallback(async()=>{
if(!branchIds.length){setLoading(false);return}
setLoading(true);
const [r,g,ro,rt,bs]=await Promise.all([
supabase.from('reservations').select('*').in('branch_id',branchIds).order('created_at',{ascending:false}),
supabase.from('guests').select('*').limit(500),
supabase.from('rooms').select('*').in('branch_id',branchIds),
supabase.from('room_types').select('*').in('branch_id',branchIds),
supabase.from('booking_sources').select('*').order('sort_order')
]);
setReservations((r.data||[]).map(x=>({...x,status:x.status==='tentative'?'confirmed':x.status})));
setGuests(g.data||[]);setRooms(ro.data||[]);setRoomTypes(rt.data||[]);setBookingSources(bs.data||[]);
setLoading(false);
},[branchIds]);

useEffect(()=>{load()},[load]);
useEffect(()=>{if(searchQuery!==localSearch)setLocalSearch(searchQuery)},[searchQuery]);

const guestMap=new Map(guests.map(x=>[x.id,x])),roomMap=new Map(rooms.map(x=>[x.id,x]));
const filtered=reservations.filter(r=>{
if(statusFilter!=='all'&&r.status!==statusFilter)return false;
const q=localSearch.toLowerCase().trim();if(!q)return true;
const g=guestMap.get(r.primary_guest_id||''),ro=roomMap.get(r.room_id||'');
return r.reservation_number.toLowerCase().includes(q)||(g?.full_name||'').toLowerCase().includes(q)||(ro?.room_number||'').includes(q)||(g?.phone||'').includes(q)
});

if(loading)return <LoadingPage message={t('common.loading')}/>;

return <div className="space-y-6">
<div className="flex items-center justify-between flex-wrap gap-2"><h1 className="text-2xl font-bold text-slate-900">{t('nav.reservations')}</h1><Button onClick={()=>setShowForm(true)}><Plus size={18}/>{t('action.new_reservation')}</Button></div>
<div className="flex gap-3 flex-wrap"><div className="relative flex-1 min-w-[200px]"><Search size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400"/><input value={localSearch} onChange={e=>setLocalSearch(e.target.value)} placeholder={t('common.search')} className="w-full rounded-lg border border-slate-300 pl-10 pr-3 py-2 text-sm"/></div>
<select value={statusFilter} onChange={e=>setStatusFilter(e.target.value)} className="rounded-lg border px-3 py-2 bg-white"><option value="all">{t('common.all')}</option><option value="confirmed">{t('res.confirmed')}</option><option value="checked_in">{t('res.checked_in')}</option><option value="checked_out">{t('res.checked_out')}</option><option value="cancelled">{t('res.cancelled')}</option><option value="no_show">{t('res.no_show')}</option></select></div>

{!filtered.length?<EmptyState icon={<CalendarDays size={48}/>} title={t('res.no_reservations')}/>:<Card noPadding><div className="overflow-x-auto"><table className="w-full text-sm"><thead><tr className="border-b text-slate-500">
<th className="text-left py-3 px-4">{t('res.reservation_number')}</th><th className="text-left py-3 px-4">{t('common.guest')}</th><th className="text-left py-3 px-4">{t('common.room')}</th><th className="text-left py-3 px-4">{t('common.check_in')}</th><th className="text-left py-3 px-4">{t('common.check_out')}</th><th className="text-center py-3 px-4">{t('common.nights')}</th><th className="text-right py-3 px-4">{t('common.rate')}</th><th className="text-center py-3 px-4">{t('common.status')}</th><th/></tr></thead>
<tbody>{filtered.map(r=>{const g=guestMap.get(r.primary_guest_id||''),ro=roomMap.get(r.room_id||'');return <tr key={r.id} onClick={()=>onSelectReservation?.(r.id)} className="border-b hover:bg-slate-50 cursor-pointer"><td className="py-3 px-4 text-blue-600 font-medium">{r.reservation_number}</td><td className="py-3 px-4">{g?.full_name||'-'}</td><td className="py-3 px-4">{ro?.room_number||'-'}</td><td className="py-3 px-4">{formatDate(r.check_in_date)}</td><td className="py-3 px-4">{formatDate(r.check_out_date)}</td><td className="text-center">{r.num_nights}</td><td className="text-right">{formatIDR(r.rate)}</td><td className="text-center"><ResStatusBadge status={r.status} label={t(`res.${r.status}`)}/></td><td><button onClick={e=>{e.stopPropagation();onSelectReservation?.(r.id)}} className="text-blue-600 text-xs">{t('common.view')}</button></td></tr>})}</tbody></table></div></Card>}

<ReservationFormModal open={showForm} onClose={()=>setShowForm(false)} branches={branches} rooms={rooms} roomTypes={roomTypes} guests={guests} bookingSources={bookingSources} userId={user!.id} orgId={user!.organization_id} defaultBranchId={selectedBranchId||branches[0]?.id||''} onSaved={()=>{setShowForm(false);load()}}/>
</div>
}
export function ReservationFormModal({open,onClose,branches,rooms,roomTypes,guests,bookingSources,userId,orgId,defaultBranchId,reservation,onSaved}:{
open:boolean;onClose:()=>void;branches:Branch[];rooms:Room[];roomTypes:RoomType[];guests:Guest[];bookingSources:BookingSource[];
userId:string;orgId:string;defaultBranchId:string;reservation?:Reservation|null;onSaved:()=>void;
}){
const {t}=useI18n();const {showToast}=useToast();const [saving,setSaving]=useState(false);const [errors,setErrors]=useState<Record<string,string>>({});
const [occupiedWarning,setOccupiedWarning]=useState<any>(null);

const [form,setForm]=useState({
branch_id:'',guest_id:'',room_type_id:'',room_id:'',
check_in_date:todayISO(),check_in_time:'14:00',
check_out_date:addDays(todayISO(),1),check_out_time:'12:00',
adults:'1',children:'0',rate:'',discount:'0',tax:'0',deposit:'',
booking_source_id:'',special_requests:'',notes:''
});

useEffect(()=>{
if(reservation)setForm({
branch_id:reservation.branch_id,
guest_id:reservation.primary_guest_id||'',
room_type_id:reservation.room_type_id||'',
room_id:reservation.room_id||'',
check_in_date:reservation.check_in_date,
check_in_time:reservation.check_in_time?.substring(0,5)||'14:00',
check_out_date:reservation.check_out_date,
check_out_time:reservation.check_out_time?.substring(0,5)||'12:00',
adults:String(reservation.adults||1),
children:String(reservation.children||0),
rate:String(reservation.rate??''),
discount:String(reservation.discount??0),
tax:String(reservation.tax??0),
deposit:String(reservation.deposit??''),
booking_source_id:reservation.booking_source_id||'',
special_requests:reservation.special_requests||'',
notes:reservation.notes||''
});
else setForm(x=>({...x,branch_id:defaultBranchId,check_in_date:todayISO(),check_out_date:addDays(todayISO(),1)}))
},[reservation,open,defaultBranchId]);

const set=(k:string,v:string)=>setForm(x=>({...x,[k]:v}));

const nights=nightsBetween(form.check_in_date,form.check_out_date);
const availableRooms=rooms.filter(r=>r.branch_id===form.branch_id&&(!form.room_type_id||r.room_type_id===form.room_type_id));

const validate=()=>{
const e:any={};
if(!form.branch_id)e.branch_id='Branch is required.';
if(!form.guest_id)e.guest_id='Guest is required.';
if(!form.booking_source_id)e.booking_source_id='Booking source is required.';
if(!form.room_type_id)e.room_type_id='Room type is required.';
if(!form.room_id)e.room_id='Assigned room is required.';
if(!form.check_in_date)e.check_in_date='Check-in date is required.';
if(!form.check_out_date)e.check_out_date='Check-out date is required.';
if(form.check_in_date&&form.check_out_date&&form.check_out_date<=form.check_in_date)e.check_out_date='Check-out must be after check-in.';
if(form.rate===''||isNaN(Number(form.rate))||Number(form.rate)<0)e.rate='Room rate is required.';
if(form.deposit===''||isNaN(Number(form.deposit))||Number(form.deposit)<0)e.deposit='Please enter a deposit amount. Enter Rp0 if no deposit is required.';
setErrors(e);return !Object.keys(e).length;
};

const checkConflict=async()=>{
if(!form.room_id)return false;
const {data}=await supabase.from('reservations').select('id,check_in_date,check_out_date,status,primary_guest_id').eq('room_id',form.room_id).in('status',['confirmed','checked_in']).lt('check_in_date',form.check_out_date).gt('check_out_date',form.check_in_date).neq('id',reservation?.id||'');
return data||[];
};

const checkOccupied=()=>{
const room=rooms.find(r=>r.id===form.room_id);
return room&&room.status==='occupied'?room:null;
};

const save=async(force=false)=>{
if(!validate())return;

const conflict=await checkConflict();
if(conflict.length){
showToast('Room is already reserved for the selected dates.','error');return;
}

const occupied=checkOccupied();
if(occupied&&!force){
setOccupiedWarning(occupied);return;
}

setSaving(true);

const payload={
branch_id:form.branch_id,
organization_id:orgId,
reservation_number:reservation?.reservation_number||`RES-${Date.now().toString().slice(-8)}`,
primary_guest_id:form.guest_id,
room_type_id:form.room_type_id,
room_id:form.room_id,
adults:Number(form.adults)||1,
children:Number(form.children)||0,
check_in_date:form.check_in_date,
check_in_time:form.check_in_time,
check_out_date:form.check_out_date,
check_out_time:form.check_out_time,
num_nights:nights,
rate:Number(form.rate),
discount:Number(form.discount)||0,
tax:Number(form.tax)||0,
deposit:Number(form.deposit),
booking_source_id:form.booking_source_id,
status:'confirmed',
special_requests:form.special_requests||null,
notes:form.notes||null,
created_by:userId
};

const {data,error}=reservation?
await supabase.from('reservations').update(payload).eq('id',reservation.id).select().single():
await supabase.from('reservations').insert(payload).select().single();

if(error){showToast(error.message,'error');setSaving(false);return}

if(!reservation&&data){
await supabase.from('folios').insert({
branch_id:form.branch_id,
reservation_id:data.id,
guest_id:form.guest_id,
folio_number:`FOL-${Date.now().toString().slice(-8)}`,
status:'open'
});

await supabase.from('folio_items').insert({
folio_id:(await supabase.from('folios').select('id').eq('reservation_id',data.id).single()).data?.id,
item_type:'room_charge',
description:'Room charge',
amount:Number(form.rate)*nights
});
}

if(form.room_id)await supabase.from('rooms').update({status:'reserved'}).eq('id',form.room_id);

await supabase.from('audit_logs').insert({
organization_id:orgId,
branch_id:form.branch_id,
user_id:userId,
action:reservation?'reservation_modified':'reservation_created',
object_type:'reservation',
object_id:data?.id||reservation?.id
});

setSaving(false);
showToast('Saved','success');
onSaved();
};

return <Modal open={open} onClose={onClose} title={reservation?t('common.edit'):t('res.new_reservation')} size="xl"
footer={<><Button variant="secondary" onClick={onClose}>{t('common.cancel')}</Button><Button loading={saving} onClick={()=>save(false)}>{t('common.save')}</Button></>}>

<form className="space-y-4">

<div className="grid md:grid-cols-3 gap-4">
<Select label={t('common.branch')} value={form.branch_id} onChange={e=>set('branch_id',e.target.value)}><option value="">--</option>{branches.map(b=><option key={b.id} value={b.id}>{b.name}</option>)}</Select>
<Select label={t('common.guest')} value={form.guest_id} onChange={e=>set('guest_id',e.target.value)}><option value="">--</option>{guests.map(g=><option key={g.id} value={g.id}>{g.full_name}</option>)}</Select>
<Select label={t('common.booking_source')} value={form.booking_source_id} onChange={e=>set('booking_source_id',e.target.value)}><option value="">--</option>{bookingSources.map(b=><option key={b.id} value={b.id}>{b.name}</option>)}</Select>
</div>
<div className="grid md:grid-cols-4 gap-4">
<Input label={t('common.check_in')} type="date" value={form.check_in_date} onChange={e=>set('check_in_date',e.target.value)}/>
<Input label="Time" type="time" value={form.check_in_time} onChange={e=>set('check_in_time',e.target.value)}/>
<Input label={t('common.check_out')} type="date" value={form.check_out_date} onChange={e=>set('check_out_date',e.target.value)}/>
<Input label="Time" type="time" value={form.check_out_time} onChange={e=>set('check_out_time',e.target.value)}/>
</div>

<div className="grid md:grid-cols-4 gap-4">
<Select label={t('common.room_type')} value={form.room_type_id} onChange={e=>{set('room_type_id',e.target.value);set('room_id','')}}>
<option value="">--</option>
{roomTypes.filter(r=>r.branch_id===form.branch_id).map(r=><option key={r.id} value={r.id}>{r.name}</option>)}
</Select>

<Select label={t('res.assign_room')} value={form.room_id} onChange={e=>set('room_id',e.target.value)}>
<option value="">--</option>
{availableRooms.map(r=><option key={r.id} value={r.id}>{r.room_number} ({r.status})</option>)}
</Select>

<Input label={t('common.adults')} type="number" value={form.adults} onChange={e=>set('adults',e.target.value)}/>
<Input label={t('common.children')} type="number" value={form.children} onChange={e=>set('children',e.target.value)}/>
</div>

<div className="grid md:grid-cols-4 gap-4">
<Input label={t('common.rate')} type="number" value={form.rate} onChange={e=>set('rate',e.target.value)}/>
<Input label={t('common.discount')} type="number" value={form.discount} onChange={e=>set('discount',e.target.value)}/>
<Input label={t('common.tax')} type="number" value={form.tax} onChange={e=>set('tax',e.target.value)}/>
<Input label={t('common.deposit')} type="number" value={form.deposit} onChange={e=>set('deposit',e.target.value)}/>
</div>

<div className="bg-slate-50 rounded-lg p-3 text-sm">
{t('common.nights')}: <b>{nights}</b> · Total:
<b>{formatIDR(Number(form.rate||0)*nights-Number(form.discount||0)+Number(form.tax||0))}</b>
</div>

{Object.keys(errors).length>0&&
<div className="rounded bg-red-50 text-red-700 p-3 text-sm">
{Object.entries(errors).map(([k,v])=><div key={k}>{v}</div>)}
</div>}

<Textarea label={t('common.special_requests')} value={form.special_requests} onChange={e=>set('special_requests',e.target.value)} rows={2}/>
<Textarea label={t('common.notes')} value={form.notes} onChange={e=>set('notes',e.target.value)} rows={2}/>

</form>

{occupiedWarning&&
<Modal open={true} onClose={()=>setOccupiedWarning(null)} title="Warning">
<div className="space-y-4">
<p>Room {occupiedWarning.room_number} is currently occupied.</p>
<p>Current room status: Occupied</p>
<div className="flex justify-end gap-2">
<Button variant="secondary" onClick={()=>setOccupiedWarning(null)}>Cancel</Button>
<Button onClick={()=>{setOccupiedWarning(null);save(true)}}>Continue</Button>
</div>
</div>
</Modal>
}

</Modal>
}