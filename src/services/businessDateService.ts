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


  const nowParts = new Date().toLocaleString('en-CA', {
    timeZone: timezone,
    year: 'numeric',
    month: '2-digit',
    day: '2-digit',
    hour: '2-digit',
    minute: '2-digit',
    hour12: false,
  }).split(/[-, :]+/);

  const jakartaYear = Number(nowParts[0]);
  const jakartaMonth = Number(nowParts[1]);
  const jakartaDay = Number(nowParts[2]);
  const jakartaHour = Number(nowParts[3]);
  const jakartaMinute = Number(nowParts[4]);

  const [cutoffHour, cutoffMinute] = cutoff.split(':').map(Number);

  const currentMinutes = jakartaHour * 60 + jakartaMinute;
  const cutoffMinutes = cutoffHour * 60 + cutoffMinute;

  let businessDay = jakartaDay;
  let businessMonth = jakartaMonth;
  let businessYear = jakartaYear;

  if (currentMinutes < cutoffMinutes) {
    const d = new Date(jakartaYear, jakartaMonth - 1, jakartaDay);
    d.setDate(d.getDate() - 1);
    businessDay = d.getDate();
    businessMonth = d.getMonth() + 1;
    businessYear = d.getFullYear();
  }

  return `${businessYear}-${String(businessMonth).padStart(2, '0')}-${String(businessDay).padStart(2, '0')}`;

}