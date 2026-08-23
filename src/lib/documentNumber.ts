import { supabase } from '@/lib/supabase';

export async function generateDocumentNumber(
  type:'RES'|'FOL'|'INV'
){

  const year=new Date().getFullYear();

  const {data,error}=await supabase.rpc(
    'next_document_number',
    {
      p_type:type,
      p_year:year
    }
  );

  if(error){
    throw error;
  }

  return `${type}-${year}-${String(data).padStart(6,'0')}`;
}