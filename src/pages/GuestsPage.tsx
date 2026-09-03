import { useState, useEffect, useCallback } from 'react';
import { supabase } from '@/lib/supabase';
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
import { Plus, Search, CreditCard as Edit, Users, Phone, Mail, FileText, Receipt, CalendarPlus, CircleAlert as AlertCircle, GitMerge, CircleCheck } from 'lucide-react';
import type { Guest, Reservation } from '@/types/database';
import { saveDraft, loadDraft, clearDraft } from '@/lib/formDraft';
import { findSimilarGuests, findDuplicateGuestPairs, type SimilarGuestMatch, type DuplicatePair } from '@/lib/guest-similarity';
import { guestMergeService, type MergePreview } from '@/services/guestMergeService';
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
    const { data } = await supabase.from('guests').select('*').order('full_name', { ascending: true });
    setGuests((data as Guest[]) || []);
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
        {selectedGuest && <GuestDetail guest={selectedGuest} onEdit={() => { setEditing(selectedGuest); setShowForm(true); setSelectedGuest(null); }} onSelectReservation={onSelectReservation} onNavigateToPayment={onNavigateToPayment} onNavigateToInvoice={onNavigateToInvoice} onNewReservationForGuest={onNewReservationForGuest} />}
      </Modal>

      <GuestFormModal open={showForm} onClose={() => setShowForm(false)} guest={editing} allGuests={guests} orgId={user!.organization_id} onSaved={() => { setShowForm(false); load(); }} />

      <MergeGuestsModal open={showMerge} onClose={() => setShowMerge(false)} guests={guests} userId={user!.id} orgId={user!.organization_id} onMerged={() => { setShowMerge(false); load(); }} />
    </div>
  );
}

interface GuestDetailProps {
  guest: Guest;
  onEdit: () => void;
  onSelectReservation?: (id: string) => void;
  onNavigateToPayment?: (id: string) => void;
  onNavigateToInvoice?: (id: string) => void;
  onNewReservationForGuest?: (guestId: string) => void;
}

function GuestDetail({ guest, onEdit, onSelectReservation, onNavigateToPayment, onNavigateToInvoice, onNewReservationForGuest }: GuestDetailProps) {
  const { t } = useI18n();
  const [reservations, setReservations] = useState<Reservation[]>([]);
  const [stats, setStats] = useState({ totalStays: 0, totalSpending: 0, outstanding: 0 });

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

      <div className="flex justify-end">
        <Button variant="outline" size="sm" onClick={onEdit}><Edit size={14} /> {t('common.edit')}</Button>
      </div>
    </div>
  );
}

function GuestFormModal({ open, onClose, guest, allGuests, orgId, onSaved }: {
  open: boolean; onClose: () => void; guest: Guest | null; allGuests: Guest[]; orgId: string; onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
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
    const { error } = guest
      ? await supabase.from('guests').update(payload).eq('id', guest.id)
      : await supabase.from('guests').insert(payload);
    if (error) showToast(error.message, 'error');
    else { showToast('Saved', 'success'); clearDraft(GUEST_DRAFT_KEY); onSaved(); }
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

  useEffect(() => {
    if (open && guests.length > 0) {
      const pairs = findDuplicateGuestPairs(guests, 0.7);
      setDuplicatePairs(pairs);
      setSelectedPairIdx(pairs.length > 0 ? 0 : null);
    } else if (open) {
      setDuplicatePairs([]);
      setSelectedPairIdx(null);
    }
    setPreview(null);
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

  return (
    <Modal open={open} onClose={onClose} title="Merge Duplicate Guests" size="lg"
      footer={currentPair ? (
        <>
          <Button variant="secondary" onClick={handleSkip}>Skip</Button>
          <Button variant="danger" loading={merging} onClick={handleMerge}>
            <GitMerge size={14} /> Merge & Delete Duplicate
          </Button>
        </>
      ) : (
        <Button variant="secondary" onClick={onClose}>{t('common.close')}</Button>
      )}>
      <div className="space-y-4">
        {duplicatePairs.length === 0 ? (
          <div className="text-center py-8">
            <CircleCheck size={40} className="mx-auto text-emerald-500 mb-3" />
            <p className="text-sm text-slate-600 font-medium">No duplicate guests found</p>
            <p className="text-xs text-slate-400 mt-1">All guests appear to be unique based on name, phone, ID, and address similarity.</p>
          </div>
        ) : (
          <>
            {/* Pair selector */}
            <div className="flex items-center gap-2 flex-wrap">
              {duplicatePairs.map((pair, idx) => (
                <button
                  key={`${pair.primary.id}-${pair.duplicate.id}`}
                  onClick={() => setSelectedPairIdx(idx)}
                  className={`px-3 py-1.5 rounded-lg text-xs font-medium border transition-colors ${
                    selectedPairIdx === idx ? 'bg-blue-600 text-white border-blue-600' : 'bg-white text-slate-600 border-slate-300 hover:bg-slate-50'
                  }`}
                >
                  {pair.primary.full_name} / {pair.duplicate.full_name}
                  <span className="ml-1 opacity-70">({Math.round(pair.score * 100)}%)</span>
                </button>
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
