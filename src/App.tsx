import { useState, useRef } from 'react';
import { I18nProvider } from '@/lib/i18n';
import { ToastProvider } from '@/lib/toast';
import { AuthProvider, useAuth } from '@/lib/auth';
import { BranchProvider, useBranch } from '@/lib/branch-context';
import { AppLayout, type PageKey } from '@/components/AppLayout';
import { parsePrintHash } from '@/lib/printRoute';
import { PrintRoute } from '@/pages/PrintRoute';
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
import { GuidePage } from '@/pages/GuidePage';

function AuthenticatedApp() {
  const { user, branches } = useAuth();
  const { setSelectedBranchId } = useBranch();
  const [currentPage, setCurrentPage] = useState<PageKey>('dashboard');
  const [searchQuery, setSearchQuery] = useState('');
  const [selectedReservationId, setSelectedReservationId] = useState<string | null>(null);
  const [selectedGuestId, setSelectedGuestId] = useState<string | null>(null);
  const [newReservationGuestId, setNewReservationGuestId] = useState<string | null>(null);
  const processedNavId = useRef<string | null>(null);

  const handleSelectReservation = (id: string) => {
    setSelectedReservationId(id);
    processedNavId.current = id;
    setCurrentPage('checkin_checkout');
  };

  const handleNavigateToPayment = (id: string) => {
    setSelectedReservationId(id);
    processedNavId.current = id;
    setCurrentPage('payments');
  };

  const handleNavigateToInvoice = (id: string) => {
    setSelectedReservationId(id);
    processedNavId.current = id;
    setCurrentPage('invoices');
  };

  const handleNavigateToGuest = (id: string) => {
    setSelectedGuestId(id);
    setCurrentPage('guests');
  };

  const handleNavigate = (page: PageKey) => {
    setCurrentPage(page);
    setSelectedReservationId(null);
    processedNavId.current = null;
    if (page !== 'guests') {
      setSelectedGuestId(null);
    }
  };

  const renderPage = () => {
    switch (currentPage) {
      case 'dashboard':
        return <DashboardPage />;
      case 'reservations':
        return <ReservationsPage searchQuery={searchQuery} initialGuestId={newReservationGuestId} onInitialGuestIdConsumed={() => setNewReservationGuestId(null)} onSelectReservation={handleSelectReservation} onNavigateToPayment={handleNavigateToPayment} onNavigateToInvoice={handleNavigateToInvoice} />;
      case 'calendar':
        return <CalendarPage onSelectReservation={handleSelectReservation} />;
      case 'rooms':
        return <RoomsPage />;
      case 'guests':
        return <GuestsPage searchQuery={searchQuery} selectedGuestId={selectedGuestId} onSelectReservation={handleSelectReservation} onNavigateToPayment={handleNavigateToPayment} onNavigateToInvoice={handleNavigateToInvoice} onNewReservationForGuest={(guestId) => { setNewReservationGuestId(guestId); setCurrentPage('reservations'); }} />;
      case 'checkin_checkout':
        return <CheckinCheckoutPage initialReservationId={selectedReservationId} searchQuery={searchQuery} onNavigateToPayment={handleNavigateToPayment} onNavigateToInvoice={handleNavigateToInvoice} />;
      case 'payments':
        return <FolioPage searchQuery={searchQuery} reservationId={selectedReservationId} onNavigateToInvoice={handleNavigateToInvoice} onSelectReservation={handleSelectReservation} onNavigateToGuest={handleNavigateToGuest} />;
      case 'invoices':
        return <InvoicesPage searchQuery={searchQuery} reservationId={selectedReservationId} onNavigateToPayment={handleNavigateToPayment} onNavigateToGuest={handleNavigateToGuest} />;
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
      case 'guide':
        return <GuidePage />;
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
  if (parsePrintHash(window.location.hash)) {
    return (
      <I18nProvider>
        <PrintRoute />
      </I18nProvider>
    );
  }
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
