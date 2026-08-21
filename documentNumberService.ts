import { supabase } from '@/lib/supabase';


export async function generateNumber(
 type:'reservation'|'folio'|'invoice'
){

 const year =
 new Date().getFullYear();


 const prefix =
 {
   reservation:'RES',
   folio:'FOL',
   invoice:'INV'
 }[type];


 const {data,error}=await supabase
 .rpc(
   'next_document_number',
   {
    p_type:type,
    p_year:year
   }
 );


 if(error)
   throw error;


 return `${prefix}-${year}-${String(data).padStart(6,'0')}`;

}