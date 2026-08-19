import { type ReactNode } from 'react';
import { useAuth } from '@/lib/auth';
import { useBranch } from '@/lib/branch-context';
import { useI18n, type Language } from '@/lib/i18n';
import { supabase } from '@/lib/supabase';
import { useToast } from '@/lib/toast';
import {
  LayoutDashboard, CalendarDays, BedDouble, Users, LogIn, FileText,
  Receipt, Wallet, FileSpreadsheet, Moon, Building2, DoorOpen,
  Settings, ScrollText, CreditCard, Tags, Globe, LogOut, Search,
  ChevronDown, Hotel, KeyRound,
} from 'lucide-react';
import { useState, useRef, useEffect } from 'react';
import type { UserRole } from '@/types/database';

export type PageKey =
  | 'dashboard' | 'reservations' | 'calendar' | 'rooms' | 'guests'
  | 'checkin_checkout' | 'folio' | 'payments' | 'invoices'
  | 'reports' | 'night_audit'
  | 'branches' | 'room_types' | 'users' | 'charge_categories'
  | 'payment_settings' | 'hotel_lock' | 'system_settings' | 'audit_logs'
  | 'booking_sources';

interface NavItem {
  key: PageKey;
  labelKey: string;
  icon: ReactNode;
  roles: UserRole[];
  group: 'main' | 'admin';
}

const navItems: NavItem[] = [
  { key: 'dashboard', labelKey: 'nav.dashboard', icon: <LayoutDashboard size={20} />, roles: ['super_admin', 'manager', 'receptionist'], group: 'main' },
  { key: 'reservations', labelKey: 'nav.reservations', icon: <CalendarDays size={20} />, roles: ['super_admin', 'manager', 'receptionist'], group: 'main' },
  { key: 'calendar', labelKey: 'nav.calendar', icon: <CalendarDays size={20} />, roles: ['super_admin', 'manager', 'receptionist'], group: 'main' },
  { key: 'rooms', labelKey: 'nav.rooms', icon: <BedDouble size={20} />, roles: ['super_admin', 'manager', 'receptionist'], group: 'main' },
  { key: 'guests', labelKey: 'nav.guests', icon: <Users size={20} />, roles: ['super_admin', 'manager', 'receptionist'], group: 'main' },
  { key: 'checkin_checkout', labelKey: 'nav.checkin_checkout', icon: <LogIn size={20} />, roles: ['super_admin', 'manager', 'receptionist'], group: 'main' },
  { key: 'folio', labelKey: 'nav.folio', icon: <FileText size={20} />, roles: ['super_admin', 'manager', 'receptionist'], group: 'main' },
  { key: 'payments', labelKey: 'nav.payments', icon: <Wallet size={20} />, roles: ['super_admin', 'manager', 'receptionist'], group: 'main' },
  { key: 'invoices', labelKey: 'nav.invoices', icon: <Receipt size={20} />, roles: ['super_admin', 'manager', 'receptionist'], group: 'main' },
  { key: 'reports', labelKey: 'nav.reports', icon: <FileSpreadsheet size={20} />, roles: ['super_admin', 'manager'], group: 'main' },
  { key: 'night_audit', labelKey: 'nav.night_audit', icon: <Moon size={20} />, roles: ['super_admin', 'manager'], group: 'main' },
  { key: 'branches', labelKey: 'nav.branches', icon: <Building2 size={20} />, roles: ['super_admin'], group: 'admin' },
  { key: 'room_types', labelKey: 'nav.room_types', icon: <DoorOpen size={20} />, roles: ['super_admin'], group: 'admin' },
  { key: 'booking_sources', labelKey: 'nav.booking_sources', icon: <Tags size={20} />, roles: ['super_admin'], group: 'admin' },
  { key: 'users', labelKey: 'nav.users', icon: <Users size={20} />, roles: ['super_admin'], group: 'admin' },
  { key: 'charge_categories', labelKey: 'nav.charge_categories', icon: <Tags size={20} />, roles: ['super_admin'], group: 'admin' },
  { key: 'payment_settings', labelKey: 'nav.payment_settings', icon: <CreditCard size={20} />, roles: ['super_admin'], group: 'admin' },
  { key: 'hotel_lock', labelKey: 'nav.hotel_lock', icon: <KeyRound size={20} />, roles: ['super_admin', 'manager', 'receptionist'], group: 'admin' },
  { key: 'system_settings', labelKey: 'nav.system_settings', icon: <Settings size={20} />, roles: ['super_admin'], group: 'admin' },
  { key: 'audit_logs', labelKey: 'nav.audit_logs', icon: <ScrollText size={20} />, roles: ['super_admin', 'manager'], group: 'admin' },
];

interface LayoutProps {
  currentPage: PageKey;
  onNavigate: (page: PageKey) => void;
  children: ReactNode;
  searchQuery: string;
  onSearchChange: (q: string) => void;
}

export function AppLayout({ currentPage, onNavigate, children, searchQuery, onSearchChange }: LayoutProps) {
  const { user, branches, signOut } = useAuth();
  const { selectedBranchId, setSelectedBranchId } = useBranch();
  const { t, language, setLanguage } = useI18n();
  const { showToast } = useToast();
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [langOpen, setLangOpen] = useState(false);
  const [userOpen, setUserOpen] = useState(false);
  const langRef = useRef<HTMLDivElement>(null);
  const userRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const handler = (e: MouseEvent) => {
      if (langRef.current && !langRef.current.contains(e.target as Node)) setLangOpen(false);
      if (userRef.current && !userRef.current.contains(e.target as Node)) setUserOpen(false);
    };
    document.addEventListener('mousedown', handler);
    return () => document.removeEventListener('mousedown', handler);
  }, []);

  if (!user) return null;

  const role = user.role;
  const visibleItems = navItems.filter((item) => item.roles.includes(role));
  const mainItems = visibleItems.filter((item) => item.group === 'main');
  const adminItems = visibleItems.filter((item) => item.group === 'admin');

  const handleLanguageChange = async (lang: Language) => {
    setLanguage(lang);
    setLangOpen(false);
    const { error } = await supabase.from('profiles').update({ language: lang }).eq('id', user.id);
    if (error) showToast('Failed to save language preference', 'error');
  };

  return (
    <div className="flex h-screen bg-slate-50">
      {/* Sidebar */}
      <aside className={`${sidebarOpen ? 'translate-x-0' : '-translate-x-full'} md:translate-x-0 fixed md:static z-40 w-64 bg-slate-900 text-slate-300 flex flex-col transition-transform duration-200`}>
        <div className="flex items-center gap-2 px-5 py-4 border-b border-slate-700">
          <div className="w-9 h-9 rounded-lg bg-blue-600 flex items-center justify-center">
            <Hotel size={20} className="text-white" />
          </div>
          <div>
            <p className="font-bold text-white text-sm">Nusa PMS</p>
            <p className="text-xs text-slate-400">Hotel Management</p>
          </div>
        </div>

        <nav className="flex-1 overflow-y-auto py-4">
          <div className="px-3 mb-1">
            {mainItems.map((item) => (
              <NavButton key={item.key} item={item} active={currentPage === item.key} onClick={() => { onNavigate(item.key); setSidebarOpen(false); }} t={t} />
            ))}
          </div>
          {adminItems.length > 0 && (
            <>
              <div className="px-5 pt-4 pb-1">
                <p className="text-xs font-semibold text-slate-500 uppercase tracking-wider">Administration</p>
              </div>
              <div className="px-3">
                {adminItems.map((item) => (
                  <NavButton key={item.key} item={item} active={currentPage === item.key} onClick={() => { onNavigate(item.key); setSidebarOpen(false); }} t={t} />
                ))}
              </div>
            </>
          )}
        </nav>

        <div className="px-3 py-3 border-t border-slate-700">
          <div className="text-xs text-slate-500 px-2">DEMO DATA — For testing only</div>
        </div>
      </aside>

      {sidebarOpen && <div className="fixed inset-0 z-30 bg-black/40 md:hidden" onClick={() => setSidebarOpen(false)} />}

      {/* Main content */}
      <div className="flex-1 flex flex-col overflow-hidden">
        {/* Top bar */}
        <header className="bg-white border-b border-slate-200 px-4 py-3 flex items-center gap-4">
          <button onClick={() => setSidebarOpen(!sidebarOpen)} className="md:hidden text-slate-600">
            <Search size={20} />
          </button>

          {/* Search */}
          <div className="relative flex-1 max-w-md">
            <Search size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
            <input
              type="text"
              value={searchQuery}
              onChange={(e) => onSearchChange(e.target.value)}
              placeholder={`${t('common.search')}...`}
              className="w-full rounded-lg border border-slate-200 pl-10 pr-3 py-2 text-sm outline-none focus:ring-2 focus:ring-blue-500"
            />
          </div>

          {/* Branch switcher */}
          {branches.length > 0 && (
            <select
              value={selectedBranchId || ''}
              onChange={(e) => setSelectedBranchId(e.target.value || null)}
              className="rounded-lg border border-slate-200 px-3 py-2 text-sm text-slate-700 bg-white outline-none focus:ring-2 focus:ring-blue-500"
            >
              <option value="">{t('common.all_branches')}</option>
              {branches.map((b) => (
                <option key={b.id} value={b.id}>{b.name}</option>
              ))}
            </select>
          )}

          {/* Language switcher */}
          <div ref={langRef} className="relative">
            <button onClick={() => setLangOpen(!langOpen)} className="flex items-center gap-1 text-sm text-slate-600 hover:text-slate-900 px-2 py-2">
              <Globe size={18} />
              <span className="uppercase">{language}</span>
              <ChevronDown size={14} />
            </button>
            {langOpen && (
              <div className="absolute right-0 mt-1 bg-white rounded-lg shadow-lg border border-slate-200 py-1 w-32 z-50">
                <button onClick={() => handleLanguageChange('en')} className={`w-full text-left px-3 py-2 text-sm hover:bg-slate-50 ${language === 'en' ? 'text-blue-600 font-medium' : 'text-slate-700'}`}>English</button>
                <button onClick={() => handleLanguageChange('id')} className={`w-full text-left px-3 py-2 text-sm hover:bg-slate-50 ${language === 'id' ? 'text-blue-600 font-medium' : 'text-slate-700'}`}>Bahasa Indonesia</button>
              </div>
            )}
          </div>

          {/* User menu */}
          <div ref={userRef} className="relative">
            <button onClick={() => setUserOpen(!userOpen)} className="flex items-center gap-2 text-sm text-slate-600 hover:text-slate-900">
              <div className="w-8 h-8 rounded-full bg-blue-600 text-white flex items-center justify-center text-xs font-bold">
                {user.full_name.charAt(0).toUpperCase()}
              </div>
              <span className="hidden md:inline">{user.full_name}</span>
              <ChevronDown size={14} />
            </button>
            {userOpen && (
              <div className="absolute right-0 mt-1 bg-white rounded-lg shadow-lg border border-slate-200 py-1 w-48 z-50">
                <div className="px-3 py-2 border-b border-slate-100">
                  <p className="text-sm font-medium text-slate-800">{user.full_name}</p>
                  <p className="text-xs text-slate-500">{user.email}</p>
                  <p className="text-xs text-blue-600 mt-1 capitalize">{user.role.replace('_', ' ')}</p>
                </div>
                <button onClick={() => { signOut(); }} className="w-full text-left px-3 py-2 text-sm text-red-600 hover:bg-red-50 flex items-center gap-2">
                  <LogOut size={16} /> {t('common.logout')}
                </button>
              </div>
            )}
          </div>
        </header>

        {/* Page content */}
        <main className="flex-1 overflow-y-auto p-4 md:p-6">
          {children}
        </main>
      </div>
    </div>
  );
}

function NavButton({ item, active, onClick, t }: { item: NavItem; active: boolean; onClick: () => void; t: (k: string) => string }) {
  return (
    <button
      onClick={onClick}
      className={`w-full flex items-center gap-3 px-3 py-2.5 rounded-lg text-sm font-medium transition-colors mb-0.5 ${
        active ? 'bg-blue-600 text-white' : 'text-slate-300 hover:bg-slate-800 hover:text-white'
      }`}
    >
      {item.icon}
      <span>{t(item.labelKey)}</span>
    </button>
  );
}
