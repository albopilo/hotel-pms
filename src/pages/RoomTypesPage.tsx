import { useState, useEffect, useCallback, useMemo } from 'react';
import { supabase } from '@/lib/supabase';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n } from '@/lib/i18n';
import { useToast } from '@/lib/toast';
import { Card } from '@/components/ui/Card';
import { Button } from '@/components/ui/Button';
import { Modal, ConfirmModal } from '@/components/ui/Modal';
import { Input, Select, Textarea, MoneyInput } from '@/components/ui/Form';
import { Badge } from '@/components/ui/Badge';
import { LoadingPage, EmptyState } from '@/components/ui/States';
import { formatIDR, formatDate, todayISO, addDays } from '@/lib/format';
import { Plus, CreditCard as Edit, DoorOpen, Trash2, CalendarClock } from 'lucide-react';
import type { RoomType, Branch, IndonesianHoliday } from '@/types/database';
import { saveDraft, loadDraft, clearDraft } from '@/lib/formDraft';

const ROOM_TYPE_DRAFT_KEY = 'room_type_form_draft';

const initialRoomTypeForm = {
  branch_id: '', name: '', code: '', description: '', base_rate: '0', weekday_rate: '0', weekend_rate: '0', max_occupancy: '2', default_tax_rate: '0', is_active: true, sort_order: '0',
};

export function RoomTypesPage() {
  const { user, branches } = useAuth();
  const { selectedBranchId } = useBranch();
  const { t } = useI18n();
  const { showToast } = useToast();
  const [roomTypes, setRoomTypes] = useState<RoomType[]>([]);
  const [holidays, setHolidays] = useState<IndonesianHoliday[]>([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [editing, setEditing] = useState<RoomType | null>(null);
  const [deleteTarget, setDeleteTarget] = useState<RoomType | null>(null);
  const [showHolidays, setShowHolidays] = useState(false);

  const canManageHolidays = user?.role === 'super_admin' || user?.role === 'manager';

  const branchIds = useMemo(
  () => selectedBranchId ? [selectedBranchId] : branches.map((b) => b.id),
  [selectedBranchId, branches]
);

  const load = useCallback(async () => {
    if (branchIds.length === 0) { setLoading(false); return; }
    setLoading(true);
    const [{ data }, { data: hol }] = await Promise.all([
      supabase.from('room_types').select('*').in('branch_id', branchIds).order('sort_order'),
      supabase.from('indonesian_holidays').select('*').eq('organization_id', user!.organization_id).order('holiday_date'),
    ]);
    setRoomTypes((data as RoomType[]) || []);
    setHolidays((hol as IndonesianHoliday[]) || []);
    setLoading(false);
  }, [branchIds, user]);

  useEffect(() => { load(); }, [load]);

  const handleDelete = async () => {
    if (!deleteTarget) return;
    const { error } = await supabase.from('room_types').delete().eq('id', deleteTarget.id);
    if (error) showToast(error.message, 'error');
    else { showToast('Deleted', 'success'); load(); }
    setDeleteTarget(null);
  };

  if (loading) return <LoadingPage message={t('common.loading')} />;

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between flex-wrap gap-2">
        <h1 className="text-2xl font-bold text-slate-900">{t('nav.room_types')}</h1>
        <div className="flex gap-2">
          {canManageHolidays && (
            <Button variant="outline" onClick={() => setShowHolidays(true)}>
              <CalendarClock size={18} /> {t('holiday.manage')}
            </Button>
          )}
          <Button onClick={() => { setEditing(null); setShowForm(true); }}><Plus size={18} /> {t('common.add')}</Button>
        </div>
      </div>

      {roomTypes.length === 0 ? (
        <EmptyState icon={<DoorOpen size={48} />} title={t('common.no_data')} />
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          {roomTypes.map((rt) => {
            const branch = branches.find((b) => b.id === rt.branch_id);
            return (
              <Card key={rt.id}>
                <div className="flex items-start justify-between">
                  <div>
                    <h3 className="font-semibold text-slate-800">{rt.name}</h3>
                    <p className="text-xs text-slate-400">{branch?.name} · {rt.code}</p>
                  </div>
                  <div className="flex gap-1">
                    <button onClick={() => { setEditing(rt); setShowForm(true); }} className="text-slate-400 hover:text-blue-600"><Edit size={16} /></button>
                    <button onClick={() => setDeleteTarget(rt)} className="text-slate-400 hover:text-red-600"><Trash2 size={16} /></button>
                  </div>
                </div>
                <div className="mt-3 space-y-2 text-sm">
                  <div className="grid grid-cols-3 gap-2">
                    <div className="bg-slate-50 rounded-lg p-2 text-center">
                      <p className="text-xs text-slate-400">{t('rooms.base_rate')}</p>
                      <p className="font-bold text-slate-700">{formatIDR(rt.base_rate)}</p>
                    </div>
                    <div className="bg-blue-50 rounded-lg p-2 text-center">
                      <p className="text-xs text-blue-400">{t('room_types.weekday_rate')}</p>
                      <p className="font-bold text-blue-700">{rt.weekday_rate > 0 ? formatIDR(rt.weekday_rate) : '-'}</p>
                    </div>
                    <div className="bg-amber-50 rounded-lg p-2 text-center">
                      <p className="text-xs text-amber-500">{t('room_types.weekend_rate')}</p>
                      <p className="font-bold text-amber-700">{rt.weekend_rate > 0 ? formatIDR(rt.weekend_rate) : '-'}</p>
                    </div>
                  </div>
                  <div className="flex items-center gap-4 pt-1">
                    <div><span className="text-slate-500">{t('common.max_occupancy')}:</span> <span className="font-medium">{rt.max_occupancy}</span></div>
                    <div><span className="text-slate-500">Tax:</span> <span className="font-medium">{rt.default_tax_rate}%</span></div>
                  </div>
                </div>
                {rt.description && <p className="mt-2 text-sm text-slate-500">{rt.description}</p>}
              </Card>
            );
          })}
        </div>
      )}

      <RoomTypeFormModal open={showForm} onClose={() => setShowForm(false)} roomType={editing} branches={branches} holidays={holidays} onSaved={() => { setShowForm(false); load(); }} />
      <ConfirmModal open={!!deleteTarget} onClose={() => setDeleteTarget(null)} onConfirm={handleDelete} title="Delete Room Type" message={`Delete "${deleteTarget?.name}"? This cannot be undone.`} confirmLabel={t('common.delete')} variant="danger" />
      {showHolidays && <HolidayManagementModal open={showHolidays} onClose={() => { setShowHolidays(false); load(); }} orgId={user!.organization_id} userId={user!.id} onSaved={() => { setShowHolidays(false); load(); }} />}
    </div>
  );
}

function RoomTypeFormModal({ open, onClose, roomType, branches, holidays, onSaved }: {
  open: boolean;
  onClose: () => void;
  roomType: RoomType | null;
  branches: Branch[];
  holidays: IndonesianHoliday[];
  onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [saving, setSaving] = useState(false);
  const [form, setForm] = useState(() => {
    const draft = loadDraft<typeof initialRoomTypeForm>(ROOM_TYPE_DRAFT_KEY);
    return draft || { ...initialRoomTypeForm };
  });

  useEffect(() => {
    if (roomType) {
      setForm({
        branch_id: roomType.branch_id, name: roomType.name, code: roomType.code, description: roomType.description || '',
        base_rate: String(roomType.base_rate), weekday_rate: String(roomType.weekday_rate), weekend_rate: String(roomType.weekend_rate),
        max_occupancy: String(roomType.max_occupancy),
        default_tax_rate: String(roomType.default_tax_rate), is_active: roomType.is_active, sort_order: String(roomType.sort_order),
      });
    } else {
      const draft = loadDraft<typeof initialRoomTypeForm>(ROOM_TYPE_DRAFT_KEY);
      setForm(draft ? { ...initialRoomTypeForm, ...draft, branch_id: draft.branch_id || branches[0]?.id || '' } : { ...initialRoomTypeForm, branch_id: branches[0]?.id || '' });
    }
  }, [roomType, open, branches]);

  useEffect(() => {
    if (open && !roomType) saveDraft(ROOM_TYPE_DRAFT_KEY, form);
  }, [form, open, roomType]);

  // Rate preview for the next 7 days
  const previewDates = useMemo(() => Array.from({ length: 7 }, (_, i) => addDays(todayISO(), i)), []);
  const getRateForPreview = (dateStr: string): { rate: number; type: string } => {
    const date = new Date(dateStr);
    const day = date.getDay();
    const isWeekend = day === 0 || day === 5 || day === 6;
    const dateOnly = dateStr.slice(0, 10);
    const isHoliday = holidays.some((h) => h.is_active && h.holiday_date === dateOnly);
    const nextDay = new Date(dateStr);
    nextDay.setDate(nextDay.getDate() + 1);
    const nextStr = nextDay.toISOString().slice(0, 10);
    const isH1 = holidays.some((h) => h.is_active && h.holiday_date === nextStr);

    if (isHoliday || isH1 || isWeekend) {
      const rate = parseFloat(form.weekend_rate) || 0;
      return { rate: rate > 0 ? rate : parseFloat(form.base_rate) || 0, type: 'weekend' };
    }
    const rate = parseFloat(form.weekday_rate) || 0;
    return { rate: rate > 0 ? rate : parseFloat(form.base_rate) || 0, type: 'weekday' };
  };

  const handleSubmit = async () => {
    if (!form.branch_id || !form.name || !form.code) { showToast('Required fields missing', 'error'); return; }
    setSaving(true);
    const payload = {
      branch_id: form.branch_id, name: form.name, code: form.code.toUpperCase(), description: form.description || null,
      base_rate: parseFloat(form.base_rate) || 0, weekday_rate: parseFloat(form.weekday_rate) || 0, weekend_rate: parseFloat(form.weekend_rate) || 0,
      max_occupancy: parseInt(form.max_occupancy) || 2,
      default_tax_rate: parseFloat(form.default_tax_rate) || 0, is_active: form.is_active, sort_order: parseInt(form.sort_order) || 0,
    };
    const { error } = roomType
      ? await supabase.from('room_types').update(payload).eq('id', roomType.id)
      : await supabase.from('room_types').insert(payload);
    if (error) showToast(error.message, 'error');
    else { showToast('Saved', 'success'); clearDraft(ROOM_TYPE_DRAFT_KEY); onSaved(); }
    setSaving(false);
  };

  const handleCancel = () => { clearDraft(ROOM_TYPE_DRAFT_KEY); onClose(); };

  return (
    <Modal open={open} onClose={handleCancel} title={roomType ? t('common.edit') : t('common.add')} size="lg"
      footer={<><Button variant="secondary" onClick={handleCancel}>{t('common.cancel')}</Button><Button loading={saving} onClick={handleSubmit}>{t('common.save')}</Button></>}>
      <form className="space-y-4">
        <Select label={t('common.branch')} value={form.branch_id} onChange={(e) => setForm({ ...form, branch_id: e.target.value })} required>
          <option value="">--</option>
          {branches.map((b) => <option key={b.id} value={b.id}>{b.name}</option>)}
        </Select>
        <div className="grid grid-cols-2 gap-4">
          <Input label={t('common.name')} value={form.name} onChange={(e) => setForm({ ...form, name: e.target.value })} required />
          <Input label="Code" value={form.code} onChange={(e) => setForm({ ...form, code: e.target.value })} required />
        </div>

        {/* Rates section */}
        <div className="space-y-3 rounded-lg border border-slate-200 p-4 bg-slate-50">
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
            <MoneyInput label={t('rooms.base_rate')} value={form.base_rate} onChange={(v) => setForm({ ...form, base_rate: v })} />
            <MoneyInput label={t('room_types.weekday_rate')} value={form.weekday_rate} onChange={(v) => setForm({ ...form, weekday_rate: v })} />
            <MoneyInput label={t('room_types.weekend_rate')} value={form.weekend_rate} onChange={(v) => setForm({ ...form, weekend_rate: v })} />
          </div>
          <p className="text-xs text-slate-500">{t('room_types.rate_hint')}</p>
          <p className="text-xs text-amber-600">{t('room_types.holiday_rate_note')}</p>

          {/* Rate preview */}
          <div>
            <p className="text-xs font-semibold text-slate-500 uppercase mb-2">{t('room_types.rate_preview')}</p>
            <div className="grid grid-cols-7 gap-1">
              {previewDates.map((d) => {
                const { rate, type } = getRateForPreview(d);
                const dt = new Date(d);
                return (
                  <div key={d} className={`rounded-lg p-2 text-center border ${type === 'weekend' ? 'bg-amber-50 border-amber-200' : 'bg-blue-50 border-blue-200'}`}>
                    <p className="text-xs text-slate-400">{dt.toLocaleDateString('en', { weekday: 'short' })}</p>
                    <p className="text-xs font-medium text-slate-600">{dt.getDate()}/{dt.getMonth() + 1}</p>
                    <p className={`text-xs font-bold ${type === 'weekend' ? 'text-amber-700' : 'text-blue-700'}`}>{formatIDR(rate)}</p>
                  </div>
                );
              })}
            </div>
          </div>
        </div>

        <div className="grid grid-cols-2 gap-4">
          <Input label={t('common.max_occupancy')} type="number" value={form.max_occupancy} onChange={(e) => setForm({ ...form, max_occupancy: e.target.value })} />
          <Input label="Tax Rate (%)" type="number" value={form.default_tax_rate} onChange={(e) => setForm({ ...form, default_tax_rate: e.target.value })} />
        </div>
        <Input label="Sort Order" type="number" value={form.sort_order} onChange={(e) => setForm({ ...form, sort_order: e.target.value })} />
        <Textarea label={t('common.description')} value={form.description} onChange={(e) => setForm({ ...form, description: e.target.value })} rows={2} />
      </form>
    </Modal>
  );
}

function HolidayManagementModal({ open, onClose, orgId, userId, onSaved }: {
  open: boolean;
  onClose: () => void;
  orgId: string;
  userId: string;
  onSaved: () => void;
}) {
  const { t } = useI18n();
  const { showToast } = useToast();
  const [holidays, setHolidays] = useState<IndonesianHoliday[]>([]);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [deleteTarget, setDeleteTarget] = useState<IndonesianHoliday | null>(null);
  const [form, setForm] = useState({ holiday_date: todayISO(), holiday_name: '' });

  const load = useCallback(async () => {
    setLoading(true);
    const { data } = await supabase.from('indonesian_holidays').select('*').eq('organization_id', orgId).order('holiday_date');
    setHolidays((data as IndonesianHoliday[]) || []);
    setLoading(false);
  }, [orgId]);

  useEffect(() => {
    if (open) load();
  }, [open, load]);

  const handleAdd = async () => {
    if (!form.holiday_date || !form.holiday_name.trim()) { showToast('Date and name required', 'error'); return; }
    setSaving(true);
    const { error } = await supabase.from('indonesian_holidays').insert({
      organization_id: orgId,
      holiday_date: form.holiday_date,
      holiday_name: form.holiday_name.trim(),
      is_active: true,
      created_by: userId,
    });
    if (error) {
      if (error.message.includes('duplicate') || error.message.includes('unique')) {
        showToast('A holiday on this date already exists', 'error');
      } else {
        showToast(error.message, 'error');
      }
    } else {
      showToast('Holiday added', 'success');
      setForm({ holiday_date: todayISO(), holiday_name: '' });
      load();
    }
    setSaving(false);
  };

  const handleDelete = async () => {
    if (!deleteTarget) return;
    const { error } = await supabase.from('indonesian_holidays').delete().eq('id', deleteTarget.id);
    if (error) showToast(error.message, 'error');
    else { showToast('Holiday removed', 'success'); load(); }
    setDeleteTarget(null);
  };

  const upcoming = holidays.filter((h) => h.holiday_date >= todayISO());
  const past = holidays.filter((h) => h.holiday_date < todayISO());

  return (
    <>
      <Modal open={open} onClose={onClose} title={`${t('holiday.title')} (${holidays.length})`} size="lg">
        <div className="space-y-4">
          <p className="text-sm text-slate-500">{t('holiday.sync_note')}</p>

          {holidays.length > 0 && (
            <div className="flex items-center gap-2 text-xs text-emerald-700 bg-emerald-50 border border-emerald-200 rounded-lg px-3 py-2">
              <CalendarClock size={14} />
              {holidays.length} holidays loaded — weekend rate logic is active.
            </div>
          )}

          {/* Add form */}
          <div className="rounded-lg border border-slate-200 p-4 bg-slate-50">
            <div className="grid grid-cols-1 md:grid-cols-[1fr_2fr_auto] gap-3 items-end">
              <Input label={t('holiday.date')} type="date" value={form.holiday_date} onChange={(e) => setForm({ ...form, holiday_date: e.target.value })} />
              <Input label={t('holiday.name')} value={form.holiday_name} onChange={(e) => setForm({ ...form, holiday_name: e.target.value })} placeholder="e.g. Independence Day" />
              <Button loading={saving} onClick={handleAdd}><Plus size={16} /> {t('common.add')}</Button>
            </div>
          </div>

          {loading ? (
            <LoadingPage />
          ) : holidays.length === 0 ? (
            <EmptyState title={t('holiday.no_holidays')} />
          ) : (
            <div className="space-y-4">
              {upcoming.length > 0 && (
                <div>
                  <p className="text-xs font-semibold text-slate-500 uppercase mb-2">Upcoming</p>
                  <div className="space-y-1 max-h-48 overflow-y-auto">
                    {upcoming.map((h) => (
                      <div key={h.id} className="flex items-center justify-between border border-slate-100 rounded-lg px-3 py-2 hover:bg-slate-50">
                        <div className="flex items-center gap-3">
                          <Badge color={h.is_active ? 'green' : 'gray'}>{formatDate(h.holiday_date)}</Badge>
                          <span className="text-sm font-medium text-slate-700">{h.holiday_name}</span>
                        </div>
                        <button onClick={() => setDeleteTarget(h)} className="text-slate-400 hover:text-red-600"><Trash2 size={16} /></button>
                      </div>
                    ))}
                  </div>
                </div>
              )}
              {past.length > 0 && (
                <div>
                  <p className="text-xs font-semibold text-slate-500 uppercase mb-2">Past</p>
                  <div className="space-y-1 max-h-48 overflow-y-auto">
                    {past.map((h) => (
                      <div key={h.id} className="flex items-center justify-between border border-slate-100 rounded-lg px-3 py-2 hover:bg-slate-50 opacity-60">
                        <div className="flex items-center gap-3">
                          <Badge color="gray">{formatDate(h.holiday_date)}</Badge>
                          <span className="text-sm font-medium text-slate-700">{h.holiday_name}</span>
                        </div>
                        <button onClick={() => setDeleteTarget(h)} className="text-slate-400 hover:text-red-600"><Trash2 size={16} /></button>
                      </div>
                    ))}
                  </div>
                </div>
              )}
            </div>
          )}
        </div>
      </Modal>
      <ConfirmModal open={!!deleteTarget} onClose={() => setDeleteTarget(null)} onConfirm={handleDelete} title={t('holiday.delete_confirm')} message={`"${deleteTarget?.holiday_name}" (${deleteTarget?.holiday_date})`} confirmLabel={t('common.delete')} variant="danger" />
    </>
  );
}
