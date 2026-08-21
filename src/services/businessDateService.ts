import { supabase } from '@/lib/supabase';


export async function getBusinessDate(
  branchId: string
): Promise<string> {

  const { data: branch, error } = await supabase
    .from('branches')
    .select('business_day_cutoff, timezone')
    .eq('id', branchId)
    .single();


  if(error || !branch){
    throw new Error(
      'Branch business date configuration not found'
    );
  }


  const timezone =
    branch.timezone || 'Asia/Jakarta';


  const cutoff =
    branch.business_day_cutoff || '04:30:00';


  const jakarta =
    new Date(
      new Date().toLocaleString(
        'en-US',
        {
          timeZone: timezone
        }
      )
    );


  const [
    cutoffHour,
    cutoffMinute
  ] =
    cutoff
      .split(':')
      .map(Number);


  const currentMinutes =
    jakarta.getHours() * 60 +
    jakarta.getMinutes();


  const cutoffMinutes =
    cutoffHour * 60 +
    cutoffMinute;


  if(currentMinutes < cutoffMinutes){

    jakarta.setDate(
      jakarta.getDate() - 1
    );

  }


  return jakarta
    .toISOString()
    .slice(0,10);

}