import { useState, useEffect } from 'react';
import { I18nProvider } from '@/lib/i18n';
import { ToastProvider } from '@/lib/toast';
import { AuthProvider, useAuth } from '@/lib/auth';
import { BranchProvider, useBranch } from '@/lib/branch-context';
import { AppLayout, type PageKey } from '@/components/AppLayout';
import { LoginPage } from '@/pages/LoginPage';
import { DashboardPage } from '@/pages/DashboardPage';
import { ReservationsPage } from '@/pages/ReservationsPage';
import { CalendarPage } from '@/pages/CalendarPage';
import { RoomsPage } from '@/pages/RoomsPage';
import { GuestsPage } from '@/pages/GuestsPage';
import { CheckinCheckoutPage } from '@/pages/CheckinCheckoutPage';
import { FolioPage } from '@/pages/FolioPage';
import { InvoicesPage } from '@/pages/InvoicesPage';
import { ReportsPage } from '@/pages/ReportsPage';
import { NightAuditPage } from '@/pages/NightAuditPage';
import { HotelLockPage } from '@/pages/HotelLockPage';
import { RoomTypesPage } from '@/pages/RoomTypesPage';
import { BranchesPage } from '@/pages/BranchesPage';
import { BookingSourcesPage } from '@/pages/BookingSourcesPage';
import { UsersPage } from '@/pages/UsersPage';
import { ChargeCategoriesPage } from '@/pages/ChargeCategoriesPage';
import { PaymentSettingsPage } from '@/pages/PaymentSettingsPage';
import { SystemSettingsPage } from '@/pages/SystemSettingsPage';
import { AuditLogsPage } from '@/pages/AuditLogsPage';

function AuthenticatedApp() {
  const { user, branches } = useAuth();
  const { setSelectedBranchId } = useBranch();
  const [currentPage, setCurrentPage] = useState<PageKey>('dashboard');
  const [searchQuery, setSearchQuery] = useState('');
  const [selectedReservationId, setSelectedReservationId] = useState<string | null>(null);

  useEffect(() => {
    if (branches.length > 0) {
      setSelectedBranchId(null);
    }
  }, [branches, setSelectedBranchId]);

  const handleSelectReservation = (id: string) => {
    setSelectedReservationId(id);
    setCurrentPage('checkin_checkout');
  };

  const handleNavigate = (page: PageKey) => {
    setCurrentPage(page);
    if (page !== 'checkin_checkout') {
      setSelectedReservationId(null);
    }
  };

  const renderPage = () => {
    switch (currentPage) {
      case 'dashboard':
        return <DashboardPage />;
      case 'reservations':
        return <ReservationsPage searchQuery={searchQuery} onSelectReservation={handleSelectReservation} />;
      case 'calendar':
        return <CalendarPage onSelectReservation={handleSelectReservation} />;
      case 'rooms':
        return <RoomsPage />;
      case 'guests':
        return <GuestsPage searchQuery={searchQuery} />;
      case 'checkin_checkout':
        return <CheckinCheckoutPage initialReservationId={selectedReservationId} searchQuery={searchQuery} />;
      case 'payments':
        return <FolioPage searchQuery={searchQuery} reservationId={selectedReservationId} />;
      case 'invoices':
        return <InvoicesPage searchQuery={searchQuery} />;
      case 'reports':
        return <ReportsPage />;
      case 'night_audit':
        return <NightAuditPage />;
      case 'hotel_lock':
        return <HotelLockPage />;
      case 'room_types':
        return <RoomTypesPage />;
      case 'branches':
        return <BranchesPage />;
      case 'booking_sources':
        return <BookingSourcesPage />;
      case 'users':
        return <UsersPage />;
      case 'charge_categories':
        return <ChargeCategoriesPage />;
      case 'payment_settings':
        return <PaymentSettingsPage />;
      case 'system_settings':
        return <SystemSettingsPage />;
      case 'audit_logs':
        return <AuditLogsPage />;
      default:
        return <DashboardPage />;
    }
  };

  return (
    <AppLayout
      currentPage={currentPage}
      onNavigate={handleNavigate}
      searchQuery={searchQuery}
      onSearchChange={setSearchQuery}
    >
      {renderPage()}
    </AppLayout>
  );
}

function AppInner() {
  const { user, branches, loading } = useAuth();

  if (loading) {
    return (
      <div className="min-h-screen bg-slate-50 flex items-center justify-center">
        <div className="animate-pulse text-slate-400">Loading...</div>
      </div>
    );
  }

  if (!user) {
    return <LoginPage />;
  }

  return (
    <BranchProvider branches={branches}>
      <AuthenticatedApp />
    </BranchProvider>
  );
}

export default function App() {
  return (
    <I18nProvider>
      <ToastProvider>
        <AuthProvider>
          <AppInner />
        </AuthProvider>
      </ToastProvider>
    </I18nProvider>
  );
}
