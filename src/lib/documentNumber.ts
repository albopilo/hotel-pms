import { supabase } from '@/lib/supabase';

import { jakartaYear } from '@/lib/format';

export async function generateDocumentNumber(
  type:'RES'|'FOL'|'INV'
){

  const year=jakartaYear();

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