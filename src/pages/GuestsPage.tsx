import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
import { fetchAll } from '@/lib/fetchAll';
import { useAuth } from '@/lib/auth';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal } from '@/components/ui/Modal';
import { Input, Select, Textarea, SearchableSelect } from '@/components/ui/Form';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { Pagination } from '@/components/ui/Pagination';
import { formatIDR, formatDate } from '@/lib/format';
import { Plus, Search, CreditCard as Edit, Users, Phone, Mail, FileText, Receipt, CalendarPlus, CircleAlert as AlertCircle, GitMerge, CircleCheck, Award, Ticket, Star, History as HistoryIcon, Minus, Trash2 } from 'lucide-react';
import type { Guest, Reservation, M13Member, M13PointLedger, M13RewardRedemptionWithReward, UserRole } from '@/types/database';
import { saveDraft, loadDraft, clearDraft } from '@/lib/formDraft';
import { findSimilarGuests, findDuplicateGuestPairs, type SimilarGuestMatch, type DuplicatePair } from '@/lib/guest-similarity';
import { guestMergeService, type MergePreview } from '@/services/guestMergeService';
import { loyaltyService } from '@/services/loyaltyService';
import { NATIONALITIES } from '@/lib/nationalities';

const GUEST_DRAFT_KEY = 'guest_form_draft';

const initialForm = {
  full_name: '', id_type: '', id_number: '', nationality: '', gender: '', date_of_birth: '', phone: '', email: '', address: '', company: '', notes: '',
};

const ID_TYPES = ['KTP', 'Passport', 'SIM', 'Other'];

interface GuestsPageProps {
  searchQuery?: string;
  selectedGuestId?: string | null;
  onSelectReservation?: (id: string) => void;
  onNavigateToPayment?: (id: string) => void;
  onNavigateToInvoice?: (id: string) => void;
  onNewReservationForGuest?: (guestId: string) => void;
}

export function GuestsPage({ searchQuery = '', selectedGuestId, onSelectReservation, onNavigateToPayment, onNavigateToInvoice, onNewReservationForGuest }: GuestsPageProps) {
  const { user } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [guests, setGuests] = useState<Guest[]>([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [editing, setEditing] = useState<Guest | null>(null);
  const [selectedGuest, setSelectedGuest] = useState<Guest | null>(null);
  const [localSearch, setLocalSearch] = useState(searchQuery);
  const [showMerge, setShowMerge] = useState(false);
  const [page, setPage] = useState(1);
  const PAGE_SIZE = 20;

  const load = useCallback(async () => {
    setLoading(true);
    const data = await fetchAll<Guest>('guests', { order: { column: 'full_name', ascending: true } });
    setGuests(data);
    setLoading(false);
  }, []);

  useEffect(() => { load(); }, [load]);

  useEffect(() => {
    if (searchQuery !== localSearch) setLocalSearch(searchQuery);
  }, [searchQuery]);

  useEffect(() => {
    if (selectedGuestId && guests.length > 0) {
      const g = guests.find((x) => x.id === selectedGuestId);
      if (g) setSelectedGuest(g);
    }
  }, [selectedGuestId, guests]);

  const filtered = guests.filter((g) => {
    const q = localSearch.toLowerCase().trim();
    if (!q) return true;
    return g.full_name.toLowerCase().includes(q) || (g.phone || '').includes(q) || (g.id_number || '').includes(q) || (g.email || '').toLowerCase().includes(q);
  });

  const paged = filtered.slice((page - 1) * PAGE_SIZE, page * PAGE_SIZE);
  useEffect(() => { setPage(1); }, [localSearch]);

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-slate-900">{t('nav.guests')}</h1>
        <div className="flex gap-2">
          <Button variant="outline" onClick={() => setShowMerge(true)}><GitMerge size={18} /> Merge Duplicates</Button>
          <Button onClick={() => { setEditing(null); setShowForm(true); }}><Plus size={18} /> {t('guest.new_guest')}</Button>
        </div>
      </div>

      <div className="relative max-w-md">
        <Search size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
        <input
          type="text"
          value={localSearch}
          onChange={(e) => setLocalSearch(e.target.value)}
          placeholder={t('guest.search_guests')}
          className="w-full rounded-lg border border-slate-300 pl-10 pr-3 py-2 text-sm outline-none focus:ring-2 focus:ring-blue-500"
        />
      </div>

      {filtered.length === 0 ? (
        <EmptyState icon={<Users size={48} />} title={t('guest.no_guests')} />
      ) : (
        <Card noPadding>
          <div className="overflow-x-auto">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-slate-200 text-slate-500">
                  <th className="text-left py-3 px-4">{t('guest.full_name')}</th>
                  <th className="text-left py-3 px-4">{t('common.phone')}</th>
                  <th className="text-left py-3 px-4">{t('common.id_type')}</th>
                  <th className="text-left py-3 px-4">{t('common.nationality')}</th>
                  <th className="text-left py-3 px-4">{t('common.company')}</th>
                  <th className="text-right py-3 px-4">{t('common.actions')}</th>
                </tr>
              </thead>
              <tbody>
                {paged.map((g) => (
                  <tr key={g.id} className="border-b border-slate-100 hover:bg-slate-50 cursor-pointer" onClick={() => setSelectedGuest(g)}>
                    <td className="py-3 px-4 font-medium text-slate-800">{g.full_name}</td>
                    <td className="py-3 px-4">{g.phone || '-'}</td>
                    <td className="py-3 px-4">{g.id_type || '-'}</td>
                    <td className="py-3 px-4">{g.nationality || '-'}</td>
                    <td className="py-3 px-4">{g.company || '-'}</td>
                    <td className="py-3 px-4 text-right">
                      <div className="flex justify-end gap-1">
                        {onNewReservationForGuest && (
                          <button onClick={(e) => { e.stopPropagation(); onNewReservationForGuest(g.id); }} className="text-slate-400 hover:text-blue-600" title={t('action.new_reservation')}><CalendarPlus size={16} /></button>
                        )}
                        <button onClick={(e) => { e.stopPropagation(); setEditing(g); setShowForm(true); }} className="text-slate-400 hover:text-blue-600"><Edit size={16} /></button>
                      </div>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
          <Pagination page={page} pageSize={PAGE_SIZE} total={filtered.length} onPageChange={setPage} />
        </Card>
      )}

      {/* Guest detail */}
      <Modal open={!!selectedGuest} onClose={() => setSelectedGuest(null)} title={selectedGuest?.full_name || ''} size="lg">
        {selectedGuest && <GuestDetail guest={selectedGuest} onEdit={() => { setEditing(selectedGuest); setShowForm(true); setSelectedGuest(null); }} onDelete={() => { setSelectedGuest(null); load(); }} onSelectReservation={onSelectReservation} onNavigateToPayment={onNavigateToPayment} onNavigateToInvoice={onNavigateToInvoice} onNewReservationForGuest={onNewReservationForGuest} />}
      </Modal>

      <GuestFormModal open={showForm} onClose={() => setShowForm(false)} guest={editing} allGuests={guests} orgId={user!.organization_id} userId={user!.id} onSaved={() => { setShowForm(false); load(); }} />

      <MergeGuestsModal open={showMerge} onClose={() => setShowMerge(false)} guests={guests} userId={user!.id} orgId={user!.organization_id} onMerged={() => { setShowMerge(false); load(); }} />
    </div>
  );
}

interface GuestDetailProps {
  guest: Guest;
  onEdit: () => void;
  onDelete: () => void;
  onSelectReservation?: (id: string) => void;
  onNavigateToPayment?: (id: string) => void;
  onNavigateToInvoice?: (id: string) => void;
  onNewReservationForGuest?: (guestId: string) => void;
}

function GuestDetail({ guest, onEdit, onDelete, onSelectReservation, onNavigateToPayment, onNavigateToInvoice, onNewReservationForGuest }: GuestDetailProps) {
  const { user } = useAuth();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [reservations, setReservations] = useState<Reservation[]>([]);
  const [stats, setStats] = useState({ totalStays: 0, totalSpending: 0, outstanding: 0 });
  const [m13Member, setM13Member] = useState<M13Member | null>(null);
  const [m13History, setM13History] = useState<M13PointLedger[]>([]);
  const [m13Redemptions, setM13Redemptions] = useState<M13RewardRedemptionWithReward[]>([]);
  const [showM13Detail, setShowM13Detail] = useState(false);
  const [showAdjust, setShowAdjust] = useState(false);
  const [adjustPoints, setAdjustPoints] = useState('');
  const [adjustReason, setAdjustReason] = useState('');
  const [adjusting, setAdjusting] = useState(false);
  const [showDeleteConfirm, setShowDeleteConfirm] = useState(false);
  const [deleting, setDeleting] = useState(false);
  const [deleteError, setDeleteError] = useState<string | null>(null);

  const isSuperAdmin = (user?.role as UserRole) === 'super_admin';

  const handleDelete = async () => {
    setDeleting(true);
    setDeleteError(null);
    const { error } = await supabase.rpc('safe_delete_guest', { p_guest_id: guest.id, p_force: isSuperAdmin });
    if (error) {
      setDeleteError(error.message);
      setDeleting(false);
      return;
    }
    showToast(t('guest.delete_success'), 'success');
    setDeleting(false);
    setShowDeleteConfirm(false);
    onDelete();
  };

  useEffect(() => {
    (async () => {
      const { data: res } = await supabase
        .from('reservations')
        .select('*')
        .eq('primary_guest_id', guest.id)
        .neq('status', 'cancelled')
        .order('created_at', { ascending: false });
      const reservationList = (res as Reservation[]) || [];
      setReservations(reservationList);

      // Get the guest's folios, excluding voided ones
      const { data: folios } = await supabase
        .from('folios')
        .select('id, status')
        .eq('guest_id', guest.id)
        .neq('status', 'void');
      const folioIds = (folios || []).map((f) => f.id);
      if (folioIds.length === 0) {
        setStats({ totalStays: 0, totalSpending: 0, outstanding: 0 });
        return;
      }

      // Calculate from actual folio_items, excluding voided items
      const { data: items } = await supabase
        .from('folio_items')
        .select('item_type, amount, voided')
        .in('folio_id', folioIds);
      let charges = 0, payments = 0, discounts = 0, tax = 0;
      (items || []).forEach((item) => {
        if (item.voided) return;
        if (item.item_type === 'charge') charges += Number(item.amount);
        else if (item.item_type === 'payment') payments += Math.abs(Number(item.amount));
        else if (item.item_type === 'discount') discounts += Math.abs(Number(item.amount));
        else if (item.item_type === 'tax') tax += Number(item.amount);
      });
      const netBalance = charges + tax - discounts - payments;
      const stays = reservationList.filter((r) => r.status === 'checked_out').length;
      setStats({
        totalStays: stays || reservationList.length,
        totalSpending: charges + tax - discounts,
        outstanding: netBalance > 0 ? netBalance : 0,
      });

      // Load M13 membership
      const { data: m13 } = await supabase.from('m13_members').select('*').eq('pms_guest_id', guest.id).maybeSingle();
      if (m13) {
        const m = m13 as M13Member;
        setM13Member(m);
        const [h, r] = await Promise.all([
          loyaltyService.getMemberPointHistory(m.id),
          loyaltyService.getMemberRedemptions(m.id),
        ]);
        setM13History(h);
        setM13Redemptions(r);
      }
    })();
  }, [guest.id]);

  return (
    <div className="space-y-4">
      <div className="grid grid-cols-2 gap-4 text-sm">
        <div><span className="text-slate-500">{t('common.id_type')}:</span> <span className="font-medium">{guest.id_type || '-'}</span></div>
        <div><span className="text-slate-500">{t('common.id_number')}:</span> <span className="font-medium">{guest.id_number || '-'}</span></div>
        <div><span className="text-slate-500">{t('common.nationality')}:</span> <span className="font-medium">{guest.nationality || '-'}</span></div>
        <div><span className="text-slate-500">{t('common.gender')}:</span> <span className="font-medium">{guest.gender || '-'}</span></div>
        <div><span className="text-slate-500">{t('common.date_of_birth')}:</span> <span className="font-medium">{guest.date_of_birth ? formatDate(guest.date_of_birth) : '-'}</span></div>
        <div><span className="text-slate-500">{t('common.company')}:</span> <span className="font-medium">{guest.company || '-'}</span></div>
        <div className="flex items-center gap-1"><Phone size={14} className="text-slate-400" /> <span className="font-medium">{guest.phone || '-'}</span></div>
        <div className="flex items-center gap-1"><Mail size={14} className="text-slate-400" /> <span className="font-medium">{guest.email || '-'}</span></div>
      </div>
      {guest.address && <div className="text-sm"><span className="text-slate-500">{t('common.address')}:</span> <span>{guest.address}</span></div>}
      {guest.notes && <div className="text-sm bg-amber-50 rounded-lg p-3"><span className="text-slate-500">{t('common.notes')}:</span> <span>{guest.notes}</span></div>}

      <div className="grid grid-cols-3 gap-3">
        <div className="bg-blue-50 rounded-lg p-3 text-center"><p className="text-xs text-slate-500">{t('guest.total_stays')}</p><p className="text-xl font-bold text-blue-700">{stats.totalStays}</p></div>
        <div className="bg-emerald-50 rounded-lg p-3 text-center"><p className="text-xs text-slate-500">{t('guest.total_spending')}</p><p className="text-lg font-bold text-emerald-700">{formatIDR(stats.totalSpending)}</p></div>
        <div className="bg-red-50 rounded-lg p-3 text-center"><p className="text-xs text-slate-500">{t('common.outstanding')}</p><p className="text-lg font-bold text-red-700">{formatIDR(stats.outstanding)}</p></div>
      </div>

      <div>
        <div className="flex items-center justify-between mb-2">
          <h4 className="font-semibold text-slate-700">{t('guest.previous_stays')}</h4>
          {onNewReservationForGuest && (
            <Button size="sm" variant="outline" onClick={() => onNewReservationForGuest(guest.id)}><CalendarPlus size={14} /> {t('action.new_reservation')}</Button>
          )}
        </div>
        {reservations.length === 0 ? (
          <p className="text-sm text-slate-400">{t('common.no_data')}</p>
        ) : (
          <div className="space-y-2 max-h-48 overflow-y-auto">
            {reservations.map((r) => (
              <div key={r.id} className="flex items-center justify-between text-sm border border-slate-100 rounded-lg px-3 py-2 hover:bg-slate-50">
                <div className="flex items-center gap-2">
                  <span className="font-medium">{r.reservation_number}</span>
                  <span className="text-slate-400">{formatDate(r.check_in_date)} → {formatDate(r.check_out_date)}</span>
                </div>
                <div className="flex items-center gap-2">
                  {onSelectReservation && (
                    <button onClick={() => onSelectReservation(r.id)} className="text-blue-600 text-xs font-medium hover:text-blue-700">{t('common.view')}</button>
                  )}
                  {onNavigateToPayment && (
                    <button onClick={() => onNavigateToPayment(r.id)} className="text-emerald-600 text-xs font-medium hover:text-emerald-700 flex items-center gap-1"><FileText size={12} /> {t('res.view_folio')}</button>
                  )}
                  {onNavigateToInvoice && r.status !== 'tentative' && (
                    <button onClick={() => onNavigateToInvoice(r.id)} className="text-slate-600 text-xs font-medium hover:text-slate-800 flex items-center gap-1"><Receipt size={12} /> {t('res.view_invoice')}</button>
                  )}
                  <Badge color={r.status === 'checked_out' ? 'gray' : r.status === 'checked_in' ? 'green' : 'blue'}>{t(`res.${r.status}`)}</Badge>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>

      {/* M13 Club Panel */}
      <div className="border border-slate-200 rounded-lg p-4">
        <div className="flex items-center gap-2 mb-3">
          <Award size={18} className="text-amber-500" />
          <span className="font-medium text-slate-700">M13 Club</span>
        </div>
        {m13Member ? (
          <div className="space-y-3">
            <div className="flex items-center justify-between">
              <div className="flex items-center gap-4 text-sm">
                <div><span className="text-slate-500">{t('m13.member_id')}:</span> <span className="font-mono font-medium">{m13Member.member_number}</span></div>
                <div><span className="text-slate-500">{t('m13.points')}:</span> <span className="font-bold text-amber-600">{m13Member.points_balance}</span></div>
                <Badge color={m13Member.status === 'active' ? 'green' : 'gray'}>{m13Member.status === 'active' ? t('common.active') : t('common.inactive')}</Badge>
              </div>
              <Button size="sm" variant="outline" onClick={() => setShowM13Detail(!showM13Detail)}>{t('m13.view_history')}</Button>
            </div>
            {showM13Detail && (
              <div className="space-y-3 border-t border-slate-100 pt-3">
                <div>
                  <p className="text-xs font-semibold text-slate-500 uppercase mb-2">{t('m13.point_history')}</p>
                  {m13History.length === 0 ? <p className="text-sm text-slate-400">{t('m13.no_history')}</p> : (
                    <div className="space-y-1 max-h-32 overflow-y-auto">
                      {m13History.map((e) => (
                        <div key={e.id} className="flex items-center justify-between text-xs border border-slate-100 rounded px-2 py-1">
                          <div><span className="text-slate-700">{e.description || e.type}</span> <span className="text-slate-400">{formatDate(e.created_at)}</span></div>
                          <span className={`font-bold ${(e.type === 'EARN' || e.type === 'ADJUSTMENT_CREDIT') ? 'text-emerald-600' : 'text-red-600'}`}>{(e.type === 'EARN' || e.type === 'ADJUSTMENT_CREDIT') ? '+' : '-'}{e.points}</span>
                        </div>
                      ))}
                    </div>
                  )}
                </div>
                <div>
                  <p className="text-xs font-semibold text-slate-500 uppercase mb-2">{t('m13.redemptions')}</p>
                  {m13Redemptions.length === 0 ? <p className="text-sm text-slate-400">{t('m13.no_redemptions')}</p> : (
                    <div className="space-y-1 max-h-32 overflow-y-auto">
                      {m13Redemptions.map((r) => (
                        <div key={r.id} className="flex items-center justify-between text-xs border border-slate-100 rounded px-2 py-1">
                          <div><span className="text-slate-700">{r.reward?.name || '-'}</span> <span className="text-slate-400">{r.redemption_code}</span></div>
                          <Badge color={r.status === 'UNUSED' ? 'green' : 'gray'} size="sm">{r.status}</Badge>
                        </div>
                      ))}
                    </div>
                  )}
                </div>
                {user && (() => {
                  const role = user.role as UserRole;
                  const canAdd = role === 'super_admin';
                  const canDeduct = role === 'super_admin' || role === 'manager' || role === 'receptionist';
                  if (!canDeduct) return null;
                  const handleAdjust = async (isAdd: boolean) => {
                    const pts = parseInt(adjustPoints, 10);
                    if (!pts || pts <= 0) { showToast('Invalid points', 'error'); return; }
                    if (!adjustReason.trim()) { showToast(t('m13.adjust_reason_required'), 'error'); return; }
                    setAdjusting(true);
                    const { error } = await loyaltyService.adjustPoints(m13Member.id, isAdd ? pts : -pts, adjustReason, user.id);
                    if (error) { showToast(t('m13.adjust_failed'), 'error'); } else {
                      showToast(t('m13.adjust_success'), 'success');
                      setShowAdjust(false); setAdjustPoints(''); setAdjustReason('');
                      const { data: updated } = await supabase.from('m13_members').select('*').eq('id', m13Member.id).maybeSingle();
                      setM13Member(updated as M13Member | null);
                      const h = await loyaltyService.getMemberPointHistory(m13Member.id);
                      setM13History(h);
                    }
                    setAdjusting(false);
                  };
                  return (
                    <div className="border-t border-slate-100 pt-3">
                      {!showAdjust ? (
                        <div className="flex gap-2">
                          {canAdd && <Button size="sm" variant="success" onClick={() => setShowAdjust(true)}><Plus size={12} /> {t('m13.add_points')}</Button>}
                          <Button size="sm" variant="danger" onClick={() => setShowAdjust(true)}><Minus size={12} /> {t('m13.deduct_points')}</Button>
                        </div>
                      ) : (
                        <div className="space-y-2">
                          <Input type="number" placeholder={canAdd ? t('m13.points_to_add') + ' / ' + t('m13.points_to_deduct') : t('m13.points_to_deduct')} value={adjustPoints} onChange={(e) => setAdjustPoints(e.target.value)} />
                          <Textarea placeholder={t('common.reason')} value={adjustReason} onChange={(e) => setAdjustReason(e.target.value)} rows={2} />
                          <div className="flex gap-2">
                            {canAdd && <Button size="sm" variant="success" loading={adjusting} onClick={() => handleAdjust(true)}>{t('m13.add_points')}</Button>}
                            <Button size="sm" variant="danger" loading={adjusting} onClick={() => handleAdjust(false)}>{t('m13.deduct_points')}</Button>
                            <Button size="sm" variant="secondary" onClick={() => setShowAdjust(false)}>{t('common.cancel')}</Button>
                          </div>
                        </div>
                      )}
                    </div>
                  );
                })()}
              </div>
            )}
          </div>
        ) : (
          <p className="text-sm text-slate-400">{t('m13.not_member')}</p>
        )}
      </div>

      <div className="flex justify-between items-center">
        {isSuperAdmin && (
          <Button variant="danger" size="sm" onClick={() => { setDeleteError(null); setShowDeleteConfirm(true); }}><Trash2 size={14} /> {t('guest.delete')}</Button>
        )}
        <div className="flex gap-2 ml-auto">
          <Button variant="outline" size="sm" onClick={onEdit}><Edit size={14} /> {t('common.edit')}</Button>
        </div>
      </div>

      {showDeleteConfirm && (
        <Modal open onClose={() => setShowDeleteConfirm(false)} title={t('guest.delete_confirm_title')} size="sm">
          <div className="space-y-4">
            {deleteError ? (
              <div className="rounded-lg border border-red-200 bg-red-50 p-4 space-y-3">
                <div className="flex items-start gap-2">
                  <AlertCircle size={18} className="text-red-600 flex-shrink-0 mt-0.5" />
                  <div>
                    <p className="text-sm font-medium text-red-800">{t('guest.delete_blocked')}</p>
                    <p className="text-xs text-red-700 mt-1">{deleteError}</p>
                  </div>
                </div>
                <p className="text-xs text-slate-600">{t('guest.delete_blocked_desc')}</p>
                <div className="flex justify-end gap-2">
                  <Button size="sm" variant="secondary" onClick={() => setShowDeleteConfirm(false)}>{t('common.close')}</Button>
                </div>
              </div>
            ) : (
              <>
                <div className="flex items-start gap-2">
                  <AlertCircle size={18} className="text-amber-500 flex-shrink-0 mt-0.5" />
                  <p className="text-sm text-slate-700">{t('guest.delete_confirm_desc')}</p>
                </div>
                {isSuperAdmin && (
                  <p className="text-xs text-amber-700 bg-amber-50 rounded-lg p-2">
                    As super admin, this guest will be deleted regardless of reservation status. All related records will have their guest reference removed.
                  </p>
                )}
                <div className="flex justify-end gap-2">
                  <Button size="sm" variant="secondary" onClick={() => setShowDeleteConfirm(false)}>{t('common.cancel')}</Button>
                  <Button size="sm" variant="danger" loading={deleting} onClick={handleDelete}><Trash2 size={14} /> {t('guest.delete')}</Button>
                </div>
              </>
            )}
          </div>
        </Modal>
      )}
    </div>
  );
}

function GuestFormModal({ open, onClose, guest, allGuests, orgId, userId, onSaved }: {
  open: boolean; onClose: () => void; guest: Guest | null; allGuests: Guest[]; orgId: string; userId: string; onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [createM13, setCreateM13] = useState(false);
  const [m13Creating, setM13Creating] = useState(false);
  const [form, setForm] = useState(() => {
    const draft = loadDraft<typeof initialForm>(GUEST_DRAFT_KEY);
    return draft || { ...initialForm };
  });
  const [duplicateMatches, setDuplicateMatches] = useState<SimilarGuestMatch[]>([]);
  const [hasCheckedDuplicates, setHasCheckedDuplicates] = useState(false);
  const [confirmedOverride, setConfirmedOverride] = useState(false);

  useEffect(() => {
    if (guest) {
      setForm({
        full_name: guest.full_name, id_type: guest.id_type || '', id_number: guest.id_number || '', nationality: guest.nationality || '',
        gender: guest.gender || '', date_of_birth: guest.date_of_birth || '', phone: guest.phone || '', email: guest.email || '',
        address: guest.address || '', company: guest.company || '', notes: guest.notes || '',
      });
      setDuplicateMatches([]);
      setHasCheckedDuplicates(false);
      setConfirmedOverride(false);
    } else {
      const draft = loadDraft<typeof initialForm>(GUEST_DRAFT_KEY);
      setForm(draft || { ...initialForm });
      setDuplicateMatches([]);
      setHasCheckedDuplicates(false);
      setConfirmedOverride(false);
    }
  }, [guest, open]);

  useEffect(() => {
    if (open && !guest) saveDraft(GUEST_DRAFT_KEY, form);
  }, [form, open, guest]);

  // Check for duplicates whenever the form changes (only for new guests)
  useEffect(() => {
    if (!open || guest) return;
    if (!form.full_name.trim() && !form.phone.trim() && !form.email.trim() && !form.id_number.trim()) {
      setDuplicateMatches([]);
      setHasCheckedDuplicates(false);
      return;
    }
    const matches = findSimilarGuests(
      { full_name: form.full_name, phone: form.phone, email: form.email, id_number: form.id_number, address: form.address },
      allGuests,
    );
    setDuplicateMatches(matches);
    setHasCheckedDuplicates(true);
    if (matches.length === 0) setConfirmedOverride(false);
  }, [form.full_name, form.phone, form.email, form.id_number, allGuests, open, guest]);

  const [errors, setErrors] = useState<Record<string, string>>({});

  const validate = () => {
    const e: Record<string, string> = {};
    if (!form.full_name.trim()) e.full_name = `${t('guest.full_name')} ${t('common.required').toLowerCase()}`;
    if (!form.id_type) e.id_type = `${t('common.id_type')} ${t('common.required').toLowerCase()}`;
    if (!form.id_number.trim()) e.id_number = `${t('common.id_number')} ${t('common.required').toLowerCase()}`;
    if (!form.phone.trim()) e.phone = `${t('common.phone')} ${t('common.required').toLowerCase()}`;
    setErrors(e);
    return Object.keys(e).length === 0;
  };

  const handleSubmit = async () => {
    if (!validate()) { showToast('Please fill in all required fields', 'error'); return; }
    if (!guest && duplicateMatches.length > 0 && !confirmedOverride) {
      showToast('A similar guest may already exist. Please confirm to proceed.', 'warning');
      return;
    }
    setSaving(true);
    const payload = { ...form, organization_id: orgId, date_of_birth: form.date_of_birth || null };
    let savedGuestId: string | null = null;
    if (guest) {
      const { error } = await supabase.from('guests').update(payload).eq('id', guest.id);
      if (error) { showToast(error.message, 'error'); setSaving(false); return; }
      savedGuestId = guest.id;
    } else {
      const { data, error } = await supabase.from('guests').insert(payload).select().single();
      if (error) { showToast(error.message, 'error'); setSaving(false); return; }
      savedGuestId = (data as Guest)?.id || null;
    }
    showToast('Saved', 'success');
    clearDraft(GUEST_DRAFT_KEY);

    // M13 member creation (non-blocking)
    if (createM13 && savedGuestId) {
      const emailToUse = form.email || '';
      if (!emailToUse) {
        showToast(t('m13.member_create_failed') + ': ' + t('m13.email_required'), 'warning');
      } else {
        setM13Creating(true);
        try {
          const { member, error: m13Err } = await loyaltyService.createMember(savedGuestId, emailToUse, userId);
          if (m13Err) {
            showToast(t('m13.member_create_failed') + ': ' + m13Err, 'warning');
          } else if (member?.auth_user_id) {
            showToast(t('m13.member_exists'), 'info');
          } else {
            showToast(t('m13.member_created'), 'success');
          }
        } catch (e: any) {
          showToast(t('m13.member_create_failed') + ': ' + (e.message || ''), 'warning');
        }
        setM13Creating(false);
      }
    }

    onSaved();
    setSaving(false);
  };

  const handleCancel = () => { clearDraft(GUEST_DRAFT_KEY); onClose(); };

  return (
    <Modal open={open} onClose={handleCancel} title={guest ? t('common.edit') : t('guest.new_guest')} size="lg"
      footer={<><Button variant="secondary" onClick={handleCancel}>{t('common.cancel')}</Button><Button loading={saving} onClick={handleSubmit}>{t('common.save')}</Button></>}>
      <form className="space-y-4">
        {/* Duplicate warning */}
        {!guest && duplicateMatches.length > 0 && (
          <div className="rounded-lg border border-amber-200 bg-amber-50 p-3 space-y-2">
            <div className="flex items-start gap-2">
              <AlertCircle size={18} className="text-amber-600 flex-shrink-0 mt-0.5" />
              <div className="flex-1">
                <p className="text-sm font-medium text-amber-800">{t('guest.duplicate_warning')}</p>
                <p className="text-xs text-amber-700 mt-0.5">{t('guest.duplicate_warning_desc')}</p>
              </div>
            </div>
            <div className="space-y-1 ml-7">
              {duplicateMatches.slice(0, 3).map((m) => (
                <div key={m.guest.id} className="text-xs text-amber-800 bg-white/60 rounded px-2 py-1.5 flex items-center justify-between">
                  <div>
                    <span className="font-medium">{m.guest.full_name}</span>
                    {m.guest.phone && <span className="text-amber-600 ml-2">{m.guest.phone}</span>}
                    {m.guest.id_number && <span className="text-amber-600 ml-2">ID: {m.guest.id_number}</span>}
                  </div>
                  <span className="text-amber-500 capitalize">{m.matchedFields.join(', ')}</span>
                </div>
              ))}
            </div>
            <label className="flex items-center gap-2 text-sm text-amber-800 ml-7 cursor-pointer">
              <input type="checkbox" checked={confirmedOverride} onChange={(e) => setConfirmedOverride(e.target.checked)} />
              {t('guest.duplicate_warning_continue')}
            </label>
          </div>
        )}
        <Input label={t('guest.full_name')} value={form.full_name} onChange={(e) => { setForm({ ...form, full_name: e.target.value }); if (errors.full_name) setErrors({ ...errors, full_name: '' }); }} required error={errors.full_name} />
        <div className="grid grid-cols-2 gap-4">
          <Select label={t('common.id_type')} value={form.id_type} onChange={(e) => { setForm({ ...form, id_type: e.target.value }); if (errors.id_type) setErrors({ ...errors, id_type: '' }); }} required error={errors.id_type}>
            <option value="">--</option>
            {ID_TYPES.map((id) => <option key={id} value={id}>{id}</option>)}
          </Select>
          <Input label={t('common.id_number')} value={form.id_number} onChange={(e) => { setForm({ ...form, id_number: e.target.value }); if (errors.id_number) setErrors({ ...errors, id_number: '' }); }} required error={errors.id_number} />
          <SearchableSelect label={t('common.nationality')} value={form.nationality} onChange={(v) => setForm({ ...form, nationality: v })} options={NATIONALITIES.map((n) => ({ value: n.name, label: n.name }))} placeholder="--" searchPlaceholder="Search nationality..." />
          <Select label={t('common.gender')} value={form.gender} onChange={(e) => setForm({ ...form, gender: e.target.value })}>
            <option value="">--</option>
            <option value="male">Male</option>
            <option value="female">Female</option>
          </Select>
          <Input label={t('common.date_of_birth')} type="date" value={form.date_of_birth} onChange={(e) => setForm({ ...form, date_of_birth: e.target.value })} />
          <Input label={t('common.phone')} value={form.phone} onChange={(e) => { setForm({ ...form, phone: e.target.value }); if (errors.phone) setErrors({ ...errors, phone: '' }); }} required error={errors.phone} />
          <Input label={t('common.email')} type="email" value={form.email} onChange={(e) => setForm({ ...form, email: e.target.value })} />
          <Input label={t('common.company')} value={form.company} onChange={(e) => setForm({ ...form, company: e.target.value })} />
        </div>
        <Textarea label={t('common.address')} value={form.address} onChange={(e) => setForm({ ...form, address: e.target.value })} rows={2} />
        <Textarea label={t('common.notes')} value={form.notes} onChange={(e) => setForm({ ...form, notes: e.target.value })} rows={2} />

        {!guest && (
          <div className="border border-slate-200 rounded-lg p-4">
            <div className="flex items-center gap-2 mb-3">
              <Award size={18} className="text-amber-500" />
              <span className="font-medium text-slate-700">M13 Club</span>
            </div>
            <label className="flex items-center gap-2 text-sm text-slate-700">
              <input type="checkbox" checked={createM13} onChange={(e) => setCreateM13(e.target.checked)} className="rounded" />
              {t('m13.create_member')}
            </label>
            <p className="text-xs text-slate-400 mt-1">{t('m13.create_member_desc')}</p>
            {createM13 && !form.email && (
              <p className="text-xs text-red-500 mt-2">{t('m13.email_required')}</p>
            )}
          </div>
        )}
      </form>
    </Modal>
  );
}

function MergeGuestsModal({ open, onClose, guests, userId, orgId, onMerged }: {
  open: boolean; onClose: () => void; guests: Guest[]; userId: string; orgId: string; onMerged: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [duplicatePairs, setDuplicatePairs] = useState<DuplicatePair[]>([]);
  const [selectedPairIdx, setSelectedPairIdx] = useState<number | null>(null);
  const [preview, setPreview] = useState<MergePreview | null>(null);
  const [previewLoading, setPreviewLoading] = useState(false);
  const [merging, setMerging] = useState(false);
  const [keepPrimary, setKeepPrimary] = useState(true);
  const [selectedForBatch, setSelectedForBatch] = useState<Set<number>>(new Set());
  const [batchMerging, setBatchMerging] = useState(false);
  const [batchProgress, setBatchProgress] = useState({ done: 0, total: 0, errors: 0 });

  useEffect(() => {
    if (open && guests.length > 0) {
      const pairs = findDuplicateGuestPairs(guests, 0.7);
      setDuplicatePairs(pairs);
      setSelectedPairIdx(pairs.length > 0 ? 0 : null);
      setSelectedForBatch(new Set(pairs.map((_, i) => i)));
    } else if (open) {
      setDuplicatePairs([]);
      setSelectedPairIdx(null);
      setSelectedForBatch(new Set());
    }
    setPreview(null);
    setBatchProgress({ done: 0, total: 0, errors: 0 });
  }, [open, guests]);

  useEffect(() => {
    if (selectedPairIdx === null || !duplicatePairs[selectedPairIdx]) {
      setPreview(null);
      return;
    }
    const pair = duplicatePairs[selectedPairIdx];
    setPreviewLoading(true);
    const primaryId = keepPrimary ? pair.primary.id : pair.duplicate.id;
    const duplicateId = keepPrimary ? pair.duplicate.id : pair.primary.id;
    guestMergeService.previewMerge(primaryId, duplicateId)
      .then((p) => setPreview(p))
      .catch(() => setPreview(null))
      .finally(() => setPreviewLoading(false));
  }, [selectedPairIdx, duplicatePairs, keepPrimary]);

  const currentPair = selectedPairIdx !== null ? duplicatePairs[selectedPairIdx] : null;

  const handleMerge = async () => {
    if (!currentPair) return;
    const primaryId = keepPrimary ? currentPair.primary.id : currentPair.duplicate.id;
    const duplicateId = keepPrimary ? currentPair.duplicate.id : currentPair.primary.id;
    setMerging(true);
    try {
      await guestMergeService.mergeGuests(primaryId, duplicateId, userId, orgId);
      showToast('Guests merged successfully', 'success');
      setMerging(false);
      onMerged();
    } catch (e: any) {
      showToast(e.message || 'Merge failed', 'error');
      setMerging(false);
    }
  };

  const handleSkip = () => {
    if (selectedPairIdx === null) return;
    if (selectedPairIdx < duplicatePairs.length - 1) {
      setSelectedPairIdx(selectedPairIdx + 1);
    } else {
      setSelectedPairIdx(null);
    }
  };

  const toggleBatchSelection = (idx: number) => {
    setSelectedForBatch((prev) => {
      const next = new Set(prev);
      if (next.has(idx)) next.delete(idx);
      else next.add(idx);
      return next;
    });
  };

  const selectAllBatch = () => setSelectedForBatch(new Set(duplicatePairs.map((_, i) => i)));
  const clearBatchSelection = () => setSelectedForBatch(new Set());

  const handleBatchMerge = async () => {
    const indices = Array.from(selectedForBatch).sort((a, b) => a - b);
    if (indices.length === 0) {
      showToast('No pairs selected for batch merge', 'warning');
      return;
    }
    setBatchMerging(true);
    setBatchProgress({ done: 0, total: indices.length, errors: 0 });
    let errors = 0;
    for (let i = 0; i < indices.length; i++) {
      const pairIdx = indices[i];
      const pair = duplicatePairs[pairIdx];
      if (!pair) continue;
      const primaryId = pair.primary.id;
      const duplicateId = pair.duplicate.id;
      try {
        await guestMergeService.mergeGuests(primaryId, duplicateId, userId, orgId);
      } catch (e: any) {
        errors++;
        console.error(`Merge failed for pair ${pair.primary.full_name} / ${pair.duplicate.full_name}:`, e.message);
      }
      setBatchProgress({ done: i + 1, total: indices.length, errors });
    }
    setBatchMerging(false);
    if (errors === 0) {
      showToast(`Batch merge complete: ${indices.length} pairs merged`, 'success');
    } else {
      showToast(`Batch merge complete: ${indices.length - errors} succeeded, ${errors} failed`, 'warning');
    }
    onMerged();
  };

  const batchSelectedCount = selectedForBatch.size;

  return (
    <Modal open={open} onClose={onClose} title="Merge Duplicate Guests" size="lg"
      footer={
        batchMerging ? (
          <div className="flex items-center gap-3 w-full">
            <div className="flex-1">
              <div className="h-2 rounded-full bg-slate-200 overflow-hidden">
                <div className="h-full bg-blue-600 transition-all" style={{ width: `${batchProgress.total > 0 ? (batchProgress.done / batchProgress.total) * 100 : 0}%` }} />
              </div>
              <p className="text-xs text-slate-500 mt-1">Merging {batchProgress.done} / {batchProgress.total}{batchProgress.errors > 0 ? ` (${batchProgress.errors} failed)` : ''}</p>
            </div>
          </div>
        ) : duplicatePairs.length > 0 ? (
          <div className="flex items-center justify-between w-full">
            <div className="flex items-center gap-2">
              <Button size="sm" variant="outline" onClick={selectAllBatch}>Select All</Button>
              <Button size="sm" variant="outline" onClick={clearBatchSelection}>Clear</Button>
              <span className="text-xs text-slate-500">{batchSelectedCount} selected</span>
            </div>
            <div className="flex gap-2">
              {currentPair && (
                <>
                  <Button variant="secondary" onClick={handleSkip}>Skip</Button>
                  <Button variant="danger" loading={merging} onClick={handleMerge}>
                    <GitMerge size={14} /> Merge Selected Pair
                  </Button>
                </>
              )}
              <Button variant="danger" loading={batchMerging} onClick={handleBatchMerge} disabled={batchSelectedCount === 0}>
                <GitMerge size={14} /> Batch Merge ({batchSelectedCount})
              </Button>
            </div>
          </div>
        ) : (
          <Button variant="secondary" onClick={onClose}>{t('common.close')}</Button>
        )
      }>
      <div className="space-y-4">
        {duplicatePairs.length === 0 ? (
          <div className="text-center py-8">
            <CircleCheck size={40} className="mx-auto text-emerald-500 mb-3" />
            <p className="text-sm text-slate-600 font-medium">No duplicate guests found</p>
            <p className="text-xs text-slate-400 mt-1">All guests appear to be unique based on name, phone, ID, and address similarity.</p>
          </div>
        ) : (
          <>
            {/* Batch info banner */}
            <div className="rounded-lg border border-blue-200 bg-blue-50 p-3">
              <div className="flex items-start gap-2">
                <GitMerge size={16} className="text-blue-600 flex-shrink-0 mt-0.5" />
                <div>
                  <p className="text-sm font-medium text-blue-800">Batch Merge</p>
                  <p className="text-xs text-blue-700 mt-0.5">Select multiple duplicate pairs and click "Batch Merge" to merge them all at once. Each pair merges the older record as primary by default. Use individual merge if you need to choose which record to keep.</p>
                </div>
              </div>
            </div>

            {/* Pair selector with checkboxes */}
            <div className="flex items-center gap-2 flex-wrap">
              {duplicatePairs.map((pair, idx) => (
                <div key={`${pair.primary.id}-${pair.duplicate.id}`} className="flex items-center gap-1">
                  <input
                    type="checkbox"
                    checked={selectedForBatch.has(idx)}
                    onChange={() => toggleBatchSelection(idx)}
                    className="rounded border-slate-300 text-blue-600 focus:ring-blue-500"
                    title="Include in batch merge"
                  />
                  <button
                    onClick={() => setSelectedPairIdx(idx)}
                    className={`px-3 py-1.5 rounded-lg text-xs font-medium border transition-colors ${
                      selectedPairIdx === idx ? 'bg-blue-600 text-white border-blue-600' : 'bg-white text-slate-600 border-slate-300 hover:bg-slate-50'
                    }`}
                  >
                    {pair.primary.full_name} / {pair.duplicate.full_name}
                    <span className="ml-1 opacity-70">({Math.round(pair.score * 100)}%)</span>
                  </button>
                </div>
              ))}
            </div>

            {currentPair && (
              <>
                <div className="grid grid-cols-2 gap-4">
                  {/* Primary candidate */}
                  <div className={`rounded-lg border-2 p-4 cursor-pointer transition-colors ${keepPrimary ? 'border-emerald-400 bg-emerald-50' : 'border-slate-200 hover:border-slate-300'}`} onClick={() => setKeepPrimary(true)}>
                    <div className="flex items-center justify-between mb-2">
                      <span className="text-xs font-semibold text-emerald-600 uppercase">Keep (Primary)</span>
                      {keepPrimary && <CircleCheck size={18} className="text-emerald-600" />}
                    </div>
                    <p className="font-bold text-slate-800">{currentPair.primary.full_name}</p>
                    <div className="mt-2 space-y-1 text-xs text-slate-600">
                      <div>Phone: {currentPair.primary.phone || '-'}</div>
                      <div>ID: {currentPair.primary.id_number || '-'}</div>
                      <div>Email: {currentPair.primary.email || '-'}</div>
                      <div>Address: {currentPair.primary.address || '-'}</div>
                    </div>
                  </div>

                  {/* Duplicate candidate */}
                  <div className={`rounded-lg border-2 p-4 cursor-pointer transition-colors ${!keepPrimary ? 'border-emerald-400 bg-emerald-50' : 'border-red-200 bg-red-50/50 hover:border-red-300'}`} onClick={() => setKeepPrimary(false)}>
                    <div className="flex items-center justify-between mb-2">
                      <span className="text-xs font-semibold text-red-600 uppercase">Delete (Duplicate)</span>
                      {!keepPrimary && <CircleCheck size={18} className="text-emerald-600" />}
                    </div>
                    <p className="font-bold text-slate-800">{currentPair.duplicate.full_name}</p>
                    <div className="mt-2 space-y-1 text-xs text-slate-600">
                      <div>Phone: {currentPair.duplicate.phone || '-'}</div>
                      <div>ID: {currentPair.duplicate.id_number || '-'}</div>
                      <div>Email: {currentPair.duplicate.email || '-'}</div>
                      <div>Address: {currentPair.duplicate.address || '-'}</div>
                    </div>
                  </div>
                </div>

                {/* Similarity info */}
                <div className="flex items-center gap-2 text-sm">
                  <Badge color="amber">Similarity: {Math.round(currentPair.score * 100)}%</Badge>
                  <span className="text-xs text-slate-500">Matched fields: {currentPair.matchedFields.join(', ')}</span>
                </div>

                {/* Preview */}
                {previewLoading ? (
                  <div className="text-center py-4 text-sm text-slate-400">Loading preview...</div>
                ) : preview ? (
                  <div className="rounded-lg border border-slate-200 bg-slate-50 p-4">
                    <p className="text-sm font-semibold text-slate-700 mb-3">Records to be reassigned from the duplicate to the primary guest:</p>
                    <div className="grid grid-cols-2 md:grid-cols-3 gap-3 text-sm">
                      <div className="bg-white rounded-lg p-3 border border-slate-200">
                        <p className="text-xs text-slate-500">Reservations</p>
                        <p className="text-lg font-bold text-blue-700">{preview.reservationCount}</p>
                      </div>
                      <div className="bg-white rounded-lg p-3 border border-slate-200">
                        <p className="text-xs text-slate-500">Folios</p>
                        <p className="text-lg font-bold text-blue-700">{preview.folioCount}</p>
                      </div>
                      <div className="bg-white rounded-lg p-3 border border-slate-200">
                        <p className="text-xs text-slate-500">Folio Items</p>
                        <p className="text-lg font-bold text-blue-700">{preview.folioItemCount}</p>
                      </div>
                      <div className="bg-white rounded-lg p-3 border border-slate-200">
                        <p className="text-xs text-slate-500">Invoices</p>
                        <p className="text-lg font-bold text-blue-700">{preview.invoiceCount}</p>
                      </div>
                      <div className="bg-white rounded-lg p-3 border border-slate-200">
                        <p className="text-xs text-slate-500">Payments</p>
                        <p className="text-lg font-bold text-blue-700">{preview.paymentCount}</p>
                      </div>
                      <div className="bg-white rounded-lg p-3 border border-slate-200">
                        <p className="text-xs text-slate-500">Card Issuances</p>
                        <p className="text-lg font-bold text-blue-700">{preview.cardIssuanceCount}</p>
                      </div>
                    </div>
                    <p className="text-xs text-amber-600 mt-3">
                      The duplicate guest record will be permanently deleted after all records are reassigned.
                    </p>
                  </div>
                ) : null}
              </>
            )}
          </>
        )}
      </div>
    </Modal>
  );
}
