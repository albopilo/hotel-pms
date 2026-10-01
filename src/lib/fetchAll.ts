import { supabase } from '@/lib/supabase';

/**
 * Supabase caps each response at 1,000 rows. This helper uses keyset
 * pagination (cursor-based with `.lt('id', lastId)`) to page through
 * all results without relying on the Range header, which some
 * PostgREST configurations cap or ignore.
 *
 * Results are fetched in descending `id` order, then sorted client-side
 * if a different order is requested.
 */
export async function fetchAll<T = any>(
  table: string,
  options: {
    select?: string;
    filters?: (q: any) => any;
    order?: { column: string; ascending?: boolean };
    pageSize?: number;
  } = {},
): Promise<T[]> {
  const { select = '*', filters, order, pageSize = 1000 } = options;
  const all: T[] = [];
  let lastId: string | null = null;

  while (true) {
    let q: any = supabase.from(table).select(select);
    if (filters) q = filters(q);
    if (lastId) q = q.lt('id', lastId);
    q = q.order('id', { ascending: false }).limit(pageSize);
    const { data, error } = await q;
    if (error) break;
    const rows = (data as T[]) || [];
    all.push(...rows);
    if (rows.length < pageSize) break;
    lastId = (rows[rows.length - 1] as any)?.id;
    if (!lastId) break;
  }

  if (order) {
    all.sort((a: any, b: any) => {
      const aVal = a[order.column];
      const bVal = b[order.column];
      if (aVal == null && bVal == null) return 0;
      if (aVal == null) return 1;
      if (bVal == null) return -1;
      const cmp = String(aVal) < String(bVal) ? -1 : String(aVal) > String(bVal) ? 1 : 0;
      return order.ascending ? cmp : -cmp;
    });
  }

  return all;
}
