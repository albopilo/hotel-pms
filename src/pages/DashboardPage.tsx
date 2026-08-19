import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { Card, StatCard } from '@/components/ui/Card';
import { LoadingPage } from '@/components/ui/States';
import { formatIDR } from '@/lib/format';
import { BedDouble, CircleCheck as CheckCircle2, LogIn, LogOut, Users, Wallet, TrendingUp, CircleAlert as AlertCircle, Building2 } from 'lucide-react';
import type { RoomStatus, ReservationStatus } from '@/types/database';

interface DashboardStats {
  totalRooms: number;
  occupiedRooms: number;
  availableRooms: number;
  reservedRooms: number;
  dirtyRooms: number;
  cleaningRooms: number;
  outOfOrderRooms: number;
  arrivalsToday: number;
  departuresToday: number;
  inHouseGuests: number;
  roomRevenue: number;
  additionalRevenue: number;
  totalIncome: number;
  todayPayments: number;
  outstandingBalances: number;
}

interface BranchStats extends DashboardStats {
  branchId: string;
  branchName: string;
}

export function DashboardPage() {

  const checkUser = async () => {
    const { data, error } = await supabase.auth.getUser();

    console.log("AUTH USER:", data.user);
    console.log("AUTH ERROR:", error);
  };

  const testRooms = async () => {
  const { data, error } = await supabase
    .from('rooms')
    .select('*');

  console.log("ROOM DATA:", data);
  console.log("ROOM ERROR:", error);
};



  const { user, branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();
  const [stats, setStats] = useState<DashboardStats | null>(null);
  const [branchStats, setBranchStats] = useState<BranchStats[]>([]);
  const [loading, setLoading] = useState(true);

  const isSuperAdmin = user?.role === 'super_admin';
  const showAllBranches = isSuperAdmin && !selectedBranchId;

  const testRole = async () => {
  const { data, error } = await supabase.rpc(
    'current_user_role'
  );

  console.log("ROLE:", data);
  console.log("ROLE ERROR:", error);
};

useEffect(() => {
  checkUser();
  testRooms();
  testRole();
}, []);

  const loadStats = useCallback(async () => {
    setLoading(true);
    const branchIds = selectedBranchId ? [selectedBranchId] : branches.map((b) => b.id);
    if (branchIds.length === 0) { setLoading(false); return; }

    // Get rooms
    const { data: rooms } = await supabase
      .from('rooms')
      .select('id, status, branch_id')
      .in('branch_id', branchIds)
      .eq('is_active', true);

      console.log("USER:", user);
console.log("BRANCHES:", branches);
console.log("SELECTED:", selectedBranchId);

    const today = new Date().toISOString().split('T')[0];

    // Get arrivals today
    const { count: arrivals } = await supabase
      .from('reservations')
      .select('*', { count: 'exact', head: true })
      .in('branch_id', branchIds)
      .eq('check_in_date', today)
      .in('status', ['confirmed', 'tentative']);

    // Get departures today
    const { count: departures } = await supabase
      .from('reservations')
      .select('*', { count: 'exact', head: true })
      .in('branch_id', branchIds)
      .eq('check_out_date', today)
      .in('status', ['checked_in']);

    // In-house guests
    const { count: inHouse } = await supabase
      .from('reservations')
      .select('*', { count: 'exact', head: true })
      .in('branch_id', branchIds)
      .eq('status', 'checked_in');

    // Folio items for revenue
    const { data: folioItems } = await supabase
      .from('folio_items')
      .select('amount, category, item_type, business_date, branch_id')
      .in('branch_id', branchIds)
      .eq('business_date', today)
      .eq('voided', false);

    let roomRevenue = 0;
    let additionalRevenue = 0;
    let todayPayments = 0;

    (folioItems || []).forEach((item: any) => {
      if (item.item_type === 'charge' && item.amount > 0) {
        if (item.category === 'room') roomRevenue += item.amount;
        else additionalRevenue += item.amount;
      } else if (item.item_type === 'tax' && item.amount > 0) {
        additionalRevenue += item.amount;
      } else if (item.item_type === 'payment' && item.amount < 0) {
        todayPayments += Math.abs(item.amount);
      }
    });

    // Outstanding balances
    const { data: folios } = await supabase
      .from('folios')
      .select('balance, branch_id')
      .in('branch_id', branchIds)
      .eq('status', 'open');

    let outstanding = 0;
    (folios || []).forEach((f: any) => { if (f.balance > 0) outstanding += f.balance; });

    const roomList = rooms || [];
    const statusCounts = {
      total: roomList.length,
      occupied: roomList.filter((r) => r.status === 'occupied').length,
      available: roomList.filter((r) => r.status === 'available').length,
      reserved: roomList.filter((r) => r.status === 'reserved').length,
      dirty: roomList.filter((r) => r.status === 'dirty').length,
      cleaning: roomList.filter((r) => r.status === 'cleaning').length,
      outOfOrder: roomList.filter((r) => r.status === 'out_of_order' || r.status === 'out_of_service').length,
    };

    setStats({
      totalRooms: statusCounts.total,
      occupiedRooms: statusCounts.occupied,
      availableRooms: statusCounts.available,
      reservedRooms: statusCounts.reserved,
      dirtyRooms: statusCounts.dirty,
      cleaningRooms: statusCounts.cleaning,
      outOfOrderRooms: statusCounts.outOfOrder,
      arrivalsToday: arrivals || 0,
      departuresToday: departures || 0,
      inHouseGuests: inHouse || 0,
      roomRevenue,
      additionalRevenue,
      totalIncome: roomRevenue + additionalRevenue,
      todayPayments,
      outstandingBalances: outstanding,
    });

    // Per-branch stats for all-branches view
    if (showAllBranches) {
      const perBranch: BranchStats[] = [];
      for (const branch of branches) {
        const branchRooms = roomList.filter((r) => r.branch_id === branch.id);
        const branchFolioItems = (folioItems || []).filter((fi) => fi.branch_id === branch.id);
        let bRoomRev = 0, bAddRev = 0, bPayments = 0;
        branchFolioItems.forEach((item) => {
          if (item.item_type === 'charge' && item.amount > 0) {
            if (item.category === 'room') bRoomRev += item.amount;
            else bAddRev += item.amount;
          } else if (item.item_type === 'tax' && item.amount > 0) {
            bAddRev += item.amount;
          } else if (item.item_type === 'payment' && item.amount < 0) {
            bPayments += Math.abs(item.amount);
          }
        });
        const bFolios = (folios || []).filter((f) => f.branch_id === branch.id);
        let bOutstanding = 0;
        bFolios.forEach((f) => { if (f.balance > 0) bOutstanding += f.balance; });

        perBranch.push({
          branchId: branch.id,
          branchName: branch.name,
          totalRooms: branchRooms.length,
          occupiedRooms: branchRooms.filter((r) => r.status === 'occupied').length,
          availableRooms: branchRooms.filter((r) => r.status === 'available').length,
          reservedRooms: branchRooms.filter((r) => r.status === 'reserved').length,
          dirtyRooms: branchRooms.filter((r) => r.status === 'dirty').length,
          cleaningRooms: branchRooms.filter((r) => r.status === 'cleaning').length,
          outOfOrderRooms: branchRooms.filter((r) => r.status === 'out_of_order' || r.status === 'out_of_service').length,
          arrivalsToday: 0,
          departuresToday: 0,
          inHouseGuests: 0,
          roomRevenue: bRoomRev,
          additionalRevenue: bAddRev,
          totalIncome: bRoomRev + bAddRev,
          todayPayments: bPayments,
          outstandingBalances: bOutstanding,
        });
      }
      setBranchStats(perBranch);
    }

    setLoading(false);
  }, [selectedBranchId, branches, showAllBranches]);

  useEffect(() => { loadStats(); }, [loadStats]);

  if (loading) return <LoadingPage message={t('common.loading')} />;
  if (!stats) return <LoadingPage />;

  const occupancyRate = stats.totalRooms > 0 ? Math.round((stats.occupiedRooms / stats.totalRooms) * 100) : 0;

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-slate-900">{t('dash.title')}</h1>

      {showAllBranches && (
        <Card title={`${t('common.all_branches')} — ${t('dash.occupancy_rate')}: ${occupancyRate}%`}>
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 text-slate-500">
                  <th className="text-left py-2 px-3">{t('common.branch')}</th>
                  <th className="text-center py-2 px-3">{t('dash.total_rooms')}</th>
                  <th className="text-center py-2 px-3">{t('dash.occupied_rooms')}</th>
                  <th className="text-center py-2 px-3">{t('dash.available_rooms')}</th>
                  <th className="text-right py-2 px-3">{t('dash.total_income')}</th>
                  <th className="text-right py-2 px-3">{t('dash.outstanding_balances')}</th>
                </tr>
              </thead>
              <tbody>
                {branchStats.map((bs) => (
                  <tr key={bs.branchId} className="border-b border-slate-100 hover:bg-slate-50">
                    <td className="py-2 px-3 font-medium text-slate-800">{bs.branchName}</td>
                    <td className="text-center py-2 px-3">{bs.totalRooms}</td>
                    <td className="text-center py-2 px-3">{bs.occupiedRooms}</td>
                    <td className="text-center py-2 px-3">{bs.availableRooms}</td>
                    <td className="text-right py-2 px-3 font-medium">{formatIDR(bs.totalIncome)}</td>
                    <td className="text-right py-2 px-3 text-red-600">{bs.outstandingBalances > 0 ? formatIDR(bs.outstandingBalances) : '-'}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </Card>
      )}

      <div className="grid grid-cols-2 md:grid-cols-4 lg:grid-cols-7 gap-3">
        <StatCard label={t('dash.total_rooms')} value={stats.totalRooms} icon={<BedDouble size={24} />} color="slate" />
        <StatCard label={t('dash.occupied_rooms')} value={stats.occupiedRooms} icon={<BedDouble size={24} />} color="red" />
        <StatCard label={t('dash.available_rooms')} value={stats.availableRooms} icon={<CheckCircle2 size={24} />} color="green" />
        <StatCard label={t('dash.reserved_rooms')} value={stats.reservedRooms} icon={<BedDouble size={24} />} color="blue" />
        <StatCard label={t('dash.dirty_rooms')} value={stats.dirtyRooms} icon={<BedDouble size={24} />} color="amber" />
        <StatCard label={t('dash.cleaning_rooms')} value={stats.cleaningRooms} icon={<BedDouble size={24} />} color="teal" />
        <StatCard label={t('dash.out_of_order_rooms')} value={stats.outOfOrderRooms} icon={<AlertCircle size={24} />} color="slate" />
      </div>

      <div className="grid grid-cols-2 md:grid-cols-4 gap-3">
        <StatCard label={t('dash.arrivals_today')} value={stats.arrivalsToday} icon={<LogIn size={24} />} color="blue" />
        <StatCard label={t('dash.departures_today')} value={stats.departuresToday} icon={<LogOut size={24} />} color="amber" />
        <StatCard label={t('dash.in_house_guests')} value={stats.inHouseGuests} icon={<Users size={24} />} color="green" />
        <StatCard label={t('dash.occupancy_rate')} value={`${occupancyRate}%`} icon={<TrendingUp size={24} />} color="teal" />
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-3">
        <StatCard label={t('dash.room_revenue')} value={formatIDR(stats.roomRevenue)} icon={<Wallet size={24} />} color="green" />
        <StatCard label={t('dash.additional_revenue')} value={formatIDR(stats.additionalRevenue)} icon={<Wallet size={24} />} color="blue" />
        <StatCard label={t('dash.total_income')} value={formatIDR(stats.totalIncome)} icon={<TrendingUp size={24} />} color="teal" />
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-3">
        <StatCard label={t('dash.today_payments')} value={formatIDR(stats.todayPayments)} icon={<Wallet size={24} />} color="green" />
        <StatCard label={t('dash.outstanding_balances')} value={formatIDR(stats.outstandingBalances)} icon={<AlertCircle size={24} />} color="red" />
      </div>
    </div>
  );
}
