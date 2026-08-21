import { useState,useEffect,useCallback,useMemo } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Input } from '@/components/ui/Form';
import { LoadingPage,EmptyState } from '@/components/ui/States';
import { formatIDR,todayISO,addDays } from '@/lib/format';
import { FileSpreadsheet,Download } from 'lucide-react';

type ReportCategory='front_office'|'financial'|'management';

type FieldType='text'|'date'|'money'|'number';

interface ReportField {
  key:string;
  label:string;
  type?:FieldType;
}

interface ReportDef {
  key:string;
  labelKey:string;
  category:ReportCategory;
  fields:ReportField[];
}

const REPORTS:ReportDef[]=[
 {key:'arrival_report',labelKey:'reports.arrival_report',category:'front_office',fields:[
  {key:'reservation_number',label:'Reservation'},
  {key:'primary_guest.full_name',label:'Guest'},
  {key:'room.room_number',label:'Room'},
  {key:'check_in_date',label:'Check In',type:'date'}
 ]},
 {key:'departure_report',labelKey:'reports.departure_report',category:'front_office',fields:[
  {key:'reservation_number',label:'Reservation'},
  {key:'primary_guest.full_name',label:'Guest'},
  {key:'room.room_number',label:'Room'},
  {key:'check_out_date',label:'Check Out',type:'date'}
 ]},
 {key:'inhouse_guest_report',labelKey:'reports.inhouse_guest_report',category:'front_office',fields:[
  {key:'primary_guest.full_name',label:'Guest'},
  {key:'room.room_number',label:'Room'},
  {key:'check_out_date',label:'Departure',type:'date'}
 ]},
 {key:'reservation_report',labelKey:'reports.reservation_report',category:'front_office',fields:[
  {key:'reservation_number',label:'Reservation'},
  {key:'primary_guest.full_name',label:'Guest'},
  {key:'status',label:'Status'},
  {key:'total_amount',label:'Total',type:'money'}
 ]},
 {key:'cancellation_report',labelKey:'reports.cancellation_report',category:'front_office',fields:[
  {key:'reservation_number',label:'Reservation'},
  {key:'primary_guest.full_name',label:'Guest'},
  {key:'cancelled_at',label:'Cancelled',type:'date'}
 ]},
 {key:'noshow_report',labelKey:'reports.noshow_report',category:'front_office',fields:[
  {key:'reservation_number',label:'Reservation'},
  {key:'primary_guest.full_name',label:'Guest'},
  {key:'check_in_date',label:'Date',type:'date'}
 ]},
 {key:'daily_income_report',labelKey:'reports.daily_income_report',category:'financial',fields:[
  {key:'category',label:'Category'},
  {key:'amount',label:'Amount',type:'money'}
 ]},
 {key:'cash_report',labelKey:'reports.cash_report',category:'financial',fields:[
  {key:'payment_number',label:'Payment'},
  {key:'amount',label:'Amount',type:'money'}
 ]},
 {key:'edc_report',labelKey:'reports.edc_report',category:'financial',fields:[
  {key:'payment_subtype',label:'Type'},
  {key:'amount',label:'Amount',type:'money'}
 ]},
 {key:'ota_report',labelKey:'reports.ota_report',category:'financial',fields:[
  {key:'amount',label:'Amount',type:'money'},
  {key:'ota_settled',label:'Settled'}
 ]},
 {key:'outstanding_balance_report',labelKey:'reports.outstanding_balance_report',category:'financial',fields:[
  {key:'guest.full_name',label:'Guest'},
  {key:'balance',label:'Balance',type:'money'}
 ]},
 {key:'occupancy_pct',labelKey:'reports.occupancy_pct',category:'management',fields:[
  {key:'occupancy',label:'Occupancy %'}
 ]},
 {key:'adr',labelKey:'reports.adr',category:'management',fields:[
  {key:'adr',label:'ADR',type:'money'}
 ]},
 {key:'revpar',labelKey:'reports.revpar',category:'management',fields:[
  {key:'revpar',label:'RevPAR',type:'money'}
 ]}
];

export function ReportsPage(){
 const {branches}=useAuth();
 const {selectedBranchId}=useBranch();
 const {t}=useI18n();

 const [activeReport,setActiveReport]=useState<ReportDef|null>(null);
 const [dateFrom,setDateFrom]=useState(todayISO());
 const [dateTo,setDateTo]=useState(addDays(todayISO(),30));
 const [data,setData]=useState<any[]>([]);
 const [summary,setSummary]=useState<any>(null);
 const [loading,setLoading]=useState(false);

 const branchIds=useMemo(()=>selectedBranchId?[selectedBranchId]:branches.map(b=>b.id),[selectedBranchId,branches]);

 const runReport=useCallback(async(report:ReportDef)=>{
  setLoading(true);
  setData([]);
  setSummary(null);

  if(report.category==='front_office'){
   let q=supabase.from('reservations')
   .select('*,primary_guest:guests(*),room:rooms(*)')
   .in('branch_id',branchIds);

   if(report.key==='arrival_report')q=q.eq('status','confirmed').gte('check_in_date',dateFrom).lte('check_in_date',dateTo);
   if(report.key==='departure_report')q=q.eq('status','checked_in').gte('check_out_date',dateFrom).lte('check_out_date',dateTo);
   if(report.key==='inhouse_guest_report')q=q.eq('status','checked_in');
   if(report.key==='reservation_report')q=q.gte('check_in_date',dateFrom).lte('check_in_date',dateTo);
   if(report.key==='cancellation_report')q=q.eq('status','cancelled').gte('check_in_date',dateFrom).lte('check_in_date',dateTo);
   if(report.key==='noshow_report')q=q.eq('status','no_show').gte('check_in_date',dateFrom).lte('check_in_date',dateTo);

   const {data:r}=await q.order('check_in_date');
   setData(r||[]);
  }
    else{
   const {data:items}=await supabase.from('folio_items').select('*')
   .in('branch_id',branchIds).eq('voided',false)
   .gte('business_date',dateFrom).lte('business_date',dateTo);

   const {data:payments}=await supabase.from('payments').select('*')
   .in('branch_id',branchIds).eq('voided',false)
   .gte('business_date',dateFrom).lte('business_date',dateTo);

   const fi=items||[];
   const pay=payments||[];

   if(report.key==='daily_income_report'){
    const rows=fi.filter((x:any)=>x.item_type==='charge');
    setData(rows);
    setSummary({
     gross:rows.reduce((s:any,x:any)=>s+x.amount,0),
     tax:fi.filter((x:any)=>x.item_type==='tax').reduce((s:any,x:any)=>s+x.amount,0),
     discount:fi.filter((x:any)=>x.item_type==='discount').reduce((s:any,x:any)=>s+Math.abs(x.amount),0)
    });
   }

   else if(report.key==='cash_report'){
    const rows=pay.filter((x:any)=>x.payment_method_code==='cash');
    setData(rows);
    setSummary({total:rows.reduce((s:any,x:any)=>s+x.amount,0)});
   }

   else if(report.key==='edc_report'){
    const rows=pay.filter((x:any)=>x.payment_method_code==='edc');
    setData(rows);
    setSummary({total:rows.reduce((s:any,x:any)=>s+x.amount,0)});
   }

   else if(report.key==='ota_report'){
    const rows=pay.filter((x:any)=>x.is_ota);
    setData(rows);
    setSummary({
     total:rows.reduce((s:any,x:any)=>s+x.amount,0),
     settled:rows.filter((x:any)=>x.ota_settled).reduce((s:any,x:any)=>s+x.amount,0)
    });
   }

   else if(report.key==='outstanding_balance_report'){
    const {data:r}=await supabase.from('folios')
    .select('*,guest:guests(*)')
    .in('branch_id',branchIds).gt('balance',0);
    setData(r||[]);
    setSummary({total:(r||[]).reduce((s:any,x:any)=>s+x.balance,0)});
   }

   else if(['occupancy_pct','adr','revpar'].includes(report.key)){
    const {data:rooms}=await supabase.from('rooms')
    .select('*').in('branch_id',branchIds).eq('is_active',true);

    const days=Math.max(1,Math.ceil((new Date(dateTo).getTime()-new Date(dateFrom).getTime())/86400000)+1);
    const available=(rooms?.length||0)*days;
    const roomNights=fi.filter((x:any)=>x.category==='room').reduce((s:any,x:any)=>s+x.quantity,0);
    const revenue=fi.filter((x:any)=>x.category==='room').reduce((s:any,x:any)=>s+x.amount,0);

    setSummary({
     occupancy:available?Math.round(roomNights/available*100):0,
     adr:roomNights?Math.round(revenue/roomNights):0,
     revpar:available?Math.round(revenue/available):0
    });
   }

   else setData(fi);
  }

  setLoading(false);
 },[branchIds,dateFrom,dateTo]);

 useEffect(()=>{
  if(activeReport)runReport(activeReport);
 },[activeReport,runReport]);

 const exportCSV=()=>{
  if(!data.length&&!summary)return;
  let csv='';

  if(summary)csv+=Object.entries(summary).map(([k,v])=>`${k},${v}`).join('\n')+'\n\n';

  if(data.length){
   const headers=activeReport?.fields.map(f=>f.key)||Object.keys(data[0]);
   csv+=headers.join(',')+'\n';
   csv+=data.map(row=>headers.map(h=>getValue(row,h)).join(',')).join('\n');
  }

  const blob=new Blob([csv],{type:'text/csv'});
  const url=URL.createObjectURL(blob);
  const a=document.createElement('a');
  a.href=url;
  a.download=`${activeReport?.key||'report'}.csv`;
  a.click();
  URL.revokeObjectURL(url);
 };

 const groups={
  front_office:REPORTS.filter(r=>r.category==='front_office'),
  financial:REPORTS.filter(r=>r.category==='financial'),
  management:REPORTS.filter(r=>r.category==='management')
 };

 return (
  <div className="space-y-6">
   <h1 className="text-2xl font-bold text-slate-900">{t('reports.title')}</h1>

   <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
    <div className="space-y-4">
     {Object.entries(groups).map(([k,v])=>
      <ReportGroup key={k} title={k} reports={v} activeKey={activeReport?.key} onSelect={setActiveReport} t={t}/>
     )}
    </div>

    <div className="lg:col-span-2">
     {activeReport?
      <Card title={t(activeReport.labelKey)} actions={
       <Button size="sm" variant="outline" onClick={exportCSV}>
        <Download size={14}/> {t('reports.export_csv')}
       </Button>
      }>
       <div className="flex gap-3 mb-4">
        <Input label={t('common.from')} type="date" value={dateFrom} onChange={e=>setDateFrom(e.target.value)}/>
        <Input label={t('common.to')} type="date" value={dateTo} onChange={e=>setDateTo(e.target.value)}/>
       </div>

       {loading?<LoadingPage/>:
        summary?
        <div className="space-y-4">
         <ReportSummary summary={summary}/>
         {data.length>0&&<ReportTable data={data} fields={activeReport.fields}/>}
        </div>:
        data.length?
        <ReportTable data={data} fields={activeReport.fields}/>:
        <EmptyState icon={<FileSpreadsheet size={48}/>} title={t('common.no_data')}/>
       }
      </Card>
      :
      <Card>
       <EmptyState icon={<FileSpreadsheet size={48}/>} title={t('reports.title')}/>
      </Card>
     }
    </div>
   </div>
  </div>
 );
}
function ReportGroup({title,reports,activeKey,onSelect,t}:{
 title:string;
 reports:ReportDef[];
 activeKey?:string;
 onSelect:(r:ReportDef)=>void;
 t:(k:string)=>string
}){
 return (
  <div>
   <h3 className="text-xs font-semibold text-slate-500 uppercase mb-2">{title}</h3>
   <div className="space-y-1">
    {reports.map(r=>
     <button key={r.key} onClick={()=>onSelect(r)}
      className={`w-full text-left px-3 py-2 rounded-lg text-sm font-medium ${activeKey===r.key?'bg-blue-600 text-white':'text-slate-600 hover:bg-slate-100'}`}>
      {t(r.labelKey)}
     </button>
    )}
   </div>
  </div>
 );
}

function getValue(obj:any,path:string){
 return path.split('.').reduce((a,k)=>a?.[k],obj)??'-';
}

function ReportSummary({summary}:{summary:any}){
 return (
  <div className="grid grid-cols-2 md:grid-cols-3 gap-3">
   {Object.entries(summary).map(([k,v])=>
    <div key={k} className="bg-slate-50 rounded-lg p-3">
     <p className="text-xs text-slate-500 capitalize">{k.replace(/_/g,' ')}</p>
     <p className="text-lg font-bold text-slate-800">
      {typeof v==='number'&&v>1000?formatIDR(v):String(v)}
     </p>
    </div>
   )}
  </div>
 );
}

function ReportTable({data,fields}:{data:any[];fields:ReportField[]}){
 if(!data.length)return null;

 const cols=fields.length?fields:Object.keys(data[0]).map(k=>({key:k,label:k}));

 return (
  <div className="overflow-x-auto border border-slate-200 rounded-lg">
   <table className="w-full text-sm">
    <thead>
     <tr className="border-b border-slate-200 bg-slate-50 text-slate-500">
      {cols.map((f:any)=>
       <th key={f.key} className="text-left py-2 px-3">
        {f.label}
       </th>
      )}
     </tr>
    </thead>

    <tbody>
     {data.slice(0,50).map((row,i)=>
      <tr key={i} className="border-b border-slate-100">
       {cols.map((f:any)=>{
        const value=getValue(row,f.key);
        return (
         <td key={f.key} className="py-2 px-3">
          {f.type==='money'&&typeof value==='number'
           ?formatIDR(value)
           :f.type==='date'&&value
           ?String(value).slice(0,10)
           :String(value)}
         </td>
        );
       })}
      </tr>
     )}
    </tbody>
   </table>

   {data.length>50&&
    <p className="text-xs text-slate-400 p-2">
     Showing 50 of {data.length} rows
    </p>
   }
  </div>
 );
}