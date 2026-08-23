import { useState } from 'react';
import { useAuth } from '@/lib/auth';
import { useI18n } from '@/lib/i18n';
import { Card } from '@/components/ui/Card';
import { Badge } from '@/components/ui/Badge';
import { BookOpen, LayoutDashboard, CalendarDays, BedDouble, Users, LogIn, Wallet, Receipt, FileSpreadsheet, Moon, Building2, DoorOpen, Settings, ScrollText, CreditCard, Tags, KeyRound, Users as UsersIcon, ChevronDown, ChevronUp, Lightbulb, CircleCheck as CheckCircle2, CircleAlert as AlertCircle, Info } from 'lucide-react';
import type { UserRole } from '@/types/database';

interface GuideSection {
  id: string;
  icon: React.ReactNode;
  titleEn: string;
  titleId: string;
  roles: UserRole[];
  steps: { en: string; id: string }[];
  tip?: { en: string; id: string };
  visual: React.ReactNode;
}

const SECTIONS: GuideSection[] = [
  {
    id: 'dashboard',
    icon: <LayoutDashboard size={20} />,
    titleEn: 'Dashboard',
    titleId: 'Dasbor',
    roles: ['super_admin', 'manager', 'receptionist'],
    steps: [
      { en: 'The dashboard is your home screen. It shows a live snapshot of your hotel: total rooms, occupied rooms, available rooms, and rooms needing cleaning.', id: 'Dasbor adalah layar utama Anda. Ini menampilkan snapshot langsung hotel Anda: total kamar, kamar terisi, kamar tersedia, dan kamar yang perlu dibersihkan.' },
      { en: 'You also see today\'s arrivals, departures, in-house guests, and revenue figures including room revenue, additional revenue, and total income.', id: 'Anda juga melihat kedatangan, keberangkatan, tamu menginap, dan angka pendapatan hari ini termasuk pendapatan kamar, pendapatan tambahan, dan total pendapatan.' },
      { en: 'If you are a Super Admin viewing "All Branches", a comparison table shows each branch side by side with occupancy and revenue figures.', id: 'Jika Anda adalah Super Admin yang melihat "Semua Cabang", tabel perbandingan menampilkan setiap cabang secara berdampingan dengan angka hunian dan pendapatan.' },
    ],
    tip: { en: 'Use the branch selector in the top bar to switch between branches or view all at once. The dashboard updates automatically when you switch.', id: 'Gunakan pemilih cabang di bilah atas untuk beralih antar cabang atau melihat semua sekaligus. Dasbor diperbarui secara otomatis saat Anda beralih.' },
    visual: <DashboardVisual />,
  },
  {
    id: 'reservations',
    icon: <CalendarDays size={20} />,
    titleEn: 'Reservations',
    titleId: 'Reservasi',
    roles: ['super_admin', 'manager', 'receptionist'],
    steps: [
      { en: 'Click "New Reservation" to create a booking. Select the branch, guest, and booking source (e.g. Walk-in, Booking.com, Agoda).', id: 'Klik "Reservasi Baru" untuk membuat pemesanan. Pilih cabang, tamu, dan sumber pemesanan (mis. Walk-in, Booking.com, Agoda).' },
      { en: 'Choose check-in and check-out dates and times, then assign a room type and specific room. The system auto-calculates the number of nights.', id: 'Pilih tanggal dan waktu check-in serta check-out, lalu tetapkan jenis kamar dan kamar spesifik. Sistem menghitung jumlah malam secara otomatis.' },
      { en: 'For group bookings, click "Add Room" to assign multiple rooms under one reservation. Each room can have its own rate.', id: 'Untuk pemesanan grup, klik "Tambah Kamar" untuk menetapkan beberapa kamar dalam satu reservasi. Setiap kamar dapat memiliki tarif tersendiri.' },
      { en: 'Enter the room rate, discount, tax, and deposit. The system auto-calculates the total including all charges.', id: 'Masukkan tarif kamar, diskon, pajak, dan deposit. Sistem menghitung total secara otomatis termasuk semua biaya.' },
      { en: 'Click any reservation row to view details or proceed to check-in. Use the status filter to narrow down by confirmed, checked in, checked out, cancelled, or no-show.', id: 'Klik baris reservasi mana pun untuk melihat detail atau melanjutkan ke check-in. Gunakan filter status untuk mempersempit berdasarkan dikonfirmasi, sudah check-in, sudah check-out, dibatalkan, atau tidak hadir.' },
    ],
    tip: { en: 'The system warns you if a room is already booked for the selected dates or is currently occupied. You can still proceed with a confirmation override.', id: 'Sistem memperingatkan Anda jika kamar sudah dipesan untuk tanggal terpilih atau sedang terisi. Anda tetap dapat melanjutkan dengan konfirmasi lewati.' },
    visual: <ReservationVisual />,
  },
  {
    id: 'calendar',
    icon: <CalendarDays size={20} />,
    titleEn: 'Calendar',
    titleId: 'Kalender',
    roles: ['super_admin', 'manager', 'receptionist'],
    steps: [
      { en: 'The calendar shows a 14-day view of all rooms. Each colored bar represents a reservation spanning its check-in to check-out dates.', id: 'Kalender menampilkan tampilan 14 hari untuk semua kamar. Setiap batang berwarna mewakili reservasi yang membentang dari tanggal check-in hingga check-out.' },
      { en: 'Green = checked in, Blue = confirmed, Gray = checked out, Amber = tentative. The header shows occupancy count per day.', id: 'Hijau = sudah check-in, Biru = dikonfirmasi, Abu-abu = sudah check-out, Kuning = sementara. Header menampilkan jumlah hunian per hari.' },
      { en: 'Use the left/right arrows to navigate weeks, or click "Today" to jump back to the current week.', id: 'Gunakan panah kiri/kanan untuk menavigasi minggu, atau klik "Hari Ini" untuk kembali ke minggu saat ini.' },
      { en: 'Click any reservation bar to jump to the check-in/check-out page for that guest.', id: 'Klik batang reservasi mana pun untuk melompat ke halaman check-in/check-out tamu tersebut.' },
    ],
    tip: { en: 'The calendar is the fastest way to see your hotel\'s occupancy at a glance and identify gaps for new bookings.', id: 'Kalender adalah cara tercepat untuk melihat hunian hotel Anda secara sekilas dan mengidentifikasi celah untuk pemesanan baru.' },
    visual: <CalendarVisual />,
  },
  {
    id: 'rooms',
    icon: <BedDouble size={20} />,
    titleEn: 'Rooms',
    titleId: 'Kamar',
    roles: ['super_admin', 'manager', 'receptionist'],
    steps: [
      { en: 'The room grid displays all rooms grouped by room type. Each tile shows the room number and current status.', id: 'Kisi kamar menampilkan semua kamar dikelompokkan berdasarkan jenis kamar. Setiap ubin menunjukkan nomor kamar dan status saat ini.' },
      { en: 'Click any room to view details and change its status: Available, Reserved, Occupied, Dirty, Cleaning, Inspected, Out of Service, or Out of Order.', id: 'Klik kamar mana pun untuk melihat detail dan mengubah statusnya: Tersedia, Dipesan, Terisi, Kotor, Pembersihan, Diperiksa, Tidak Beroperasi, atau Rusak.' },
      { en: 'Managers and Super Admins can add or edit rooms and room types, including base rates and max occupancy.', id: 'Manajer dan Super Admin dapat menambah atau mengedit kamar dan jenis kamar, termasuk tarif dasar dan kapasitas maksimum.' },
    ],
    tip: { en: 'After a guest checks out, the room is automatically set to "Dirty" so housekeeping knows to clean it. Set it to "Inspected" when ready for the next guest.', id: 'Setelah tamu check-out, kamar otomatis diatur ke "Kotor" sehingga housekeeping tahu untuk membersihkannya. Atur ke "Diperiksa" saat siap untuk tamu berikutnya.' },
    visual: <RoomGridVisual />,
  },
  {
    id: 'guests',
    icon: <Users size={20} />,
    titleEn: 'Guests',
    titleId: 'Tamu',
    roles: ['super_admin', 'manager', 'receptionist'],
    steps: [
      { en: 'Search for existing guests by name, phone, ID number, or email using the search bar at the top of the page.', id: 'Cari tamu yang ada berdasarkan nama, telepon, nomor ID, atau email menggunakan bilah pencarian di bagian atas halaman.' },
      { en: 'Click "New Guest" to add a guest. Fill in their full name (required), ID type and number, phone, and other details like nationality and company.', id: 'Klik "Tamu Baru" untuk menambahkan tamu. Isi nama lengkap (wajib), jenis dan nomor ID, telepon, dan detail lain seperti kewarganegaraan dan perusahaan.' },
      { en: 'Click any guest row to see their full profile, previous stays, total spending, and any outstanding balances.', id: 'Klik baris tamu mana pun untuk melihat profil lengkap, menginap sebelumnya, total pengeluaran, dan saldo tertunggak apa pun.' },
    ],
    tip: { en: 'Always search for an existing guest first to avoid creating duplicates. The system matches by name, phone, ID number, and email.', id: 'Selalu cari tamu yang ada terlebih dahulu untuk menghindari duplikat. Sistem mencocokkan berdasarkan nama, telepon, nomor ID, dan email.' },
    visual: <GuestVisual />,
  },
  {
    id: 'checkin_checkout',
    icon: <LogIn size={20} />,
    titleEn: 'Check-in / Check-out',
    titleId: 'Check-in / Check-out',
    roles: ['super_admin', 'manager', 'receptionist'],
    steps: [
      { en: 'The left column shows arrivals (confirmed reservations ready for check-in). Click "Check In" to process a guest.', id: 'Kolom kiri menampilkan kedatangan (reservasi dikonfirmasi siap untuk check-in). Klik "Check In" untuk memproses tamu.' },
      { en: 'During check-in, set the actual arrival time. The system compares the full date and time against the scheduled check-in (date + standard time). If the actual arrival is earlier, an early check-in charge can be added.', id: 'Saat check-in, atur waktu kedatangan aktual. Sistem membandingkan tanggal dan waktu lengkap dengan jadwal check-in (tanggal + waktu standar). Jika kedatangan lebih awal, biaya check-in awal dapat ditambahkan.' },
      { en: 'You can encode a room key card (in development/mock mode for now). Click "Encode Room Card" before completing check-in.', id: 'Anda dapat mengenkoded kartu kunci kamar (dalam mode pengembangan/mock untuk saat ini). Klik "Enkoded Kartu Kamar" sebelum menyelesaikan check-in.' },
      { en: 'The right column shows today\'s departures. Click "Check Out" to process. The system shows a full charge summary and balance.', id: 'Kolom kanan menampilkan keberangkatan hari ini. Klik "Check Out" untuk memproses. Sistem menampilkan ringkasan biaya lengkap dan saldo.' },
      { en: 'If the guest checks out late (past the standard time or past the checkout date), a late checkout warning appears with an option to add a charge.', id: 'Jika tamu check-out terlambat (melewati waktu standar atau melewati tanggal checkout), peringatan check-out terlambat muncul dengan opsi untuk menambah biaya.' },
      { en: 'A "Departures Later" section shows guests checking out on future dates, so you can plan ahead.', id: 'Bagian "Keberangkatan Nanti" menampilkan tamu yang check-out pada tanggal mendatang, sehingga Anda dapat merencanakan ke depan.' },
      { en: 'Use "Extend Stay" to add extra nights. Use "Split Room" for group reservations to separate a room into its own reservation with its own folio.', id: 'Gunakan "Perpanjang Menginap" untuk menambah malam tambahan. Gunakan "Pisah Kamar" untuk reservasi grup untuk memisahkan kamar menjadi reservasi sendiri dengan folio tersendiri.' },
    ],
    tip: { en: 'If a guest has an unpaid balance at checkout, you must confirm an override to proceed. This is logged in the audit trail for accountability.', id: 'Jika tamu memiliki saldo belum dibayar saat checkout, Anda harus mengonfirmasi lewati untuk melanjutkan. Ini dicatat dalam jejak audit untuk akuntabilitas.' },
    visual: <CheckinVisual />,
  },
  {
    id: 'payments',
    icon: <Wallet size={20} />,
    titleEn: 'Payments (Folio)',
    titleId: 'Pembayaran (Folio)',
    roles: ['super_admin', 'manager', 'receptionist'],
    steps: [
      { en: 'The Payments page lists all folios with the guest name and room number for easy identification.', id: 'Halaman Pembayaran mencantumkan semua folio dengan nama tamu dan nomor kamar untuk identifikasi mudah.' },
      { en: 'Click any folio to open the detail view. You\'ll see all charges and payments in the transaction history, with running totals.', id: 'Klik folio mana pun untuk membuka tampilan detail. Anda akan melihat semua biaya dan pembayaran dalam riwayat transaksi, dengan total berjalan.' },
      { en: 'Click "Add Charge" to post an additional charge (amenities, damage, etc.). Select a category and enter the amount and quantity.', id: 'Klik "Tambah Biaya" untuk memposting biaya tambahan (fasilitas, kerusakan, dll.). Pilih kategori dan masukkan jumlah dan kuantitas.' },
      { en: 'Click "Take Payment" to record a payment. Choose the method (Cash, EDC, OTA/Xendit). For EDC, enter the terminal, reference, and approval code.', id: 'Klik "Terima Pembayaran" untuk mencatat pembayaran. Pilih metode (Tunai, EDC, OTA/Xendit). Untuk EDC, masukkan terminal, referensi, dan kode persetujuan.' },
      { en: 'Use "Transfer Room" to move a guest to a different room. The old room is marked dirty and the new room becomes occupied.', id: 'Gunakan "Pindah Kamar" untuk memindahkan tamu ke kamar berbeda. Kamar lama ditandai kotor dan kamar baru menjadi terisi.' },
      { en: 'For finalized folios, use "Post-Stay Charge" to add charges after checkout without modifying the original invoice.', id: 'Untuk folio yang sudah difinalisasi, gunakan "Biaya Tambahan Pasca-Menginap" untuk menambah biaya setelah checkout tanpa mengubah faktur asli.' },
    ],
    tip: { en: 'Voided items show with a line-through. Only managers can void charges. All voids are logged in the audit trail.', id: 'Item yang dibatalkan ditampilkan dengan coretan. Hanya manajer yang dapat membatalkan biaya. Semua pembatalan dicatat dalam jejak audit.' },
    visual: <FolioVisual />,
  },
  {
    id: 'invoices',
    icon: <Receipt size={20} />,
    titleEn: 'Invoices',
    titleId: 'Faktur',
    roles: ['super_admin', 'manager', 'receptionist'],
    steps: [
      { en: 'Invoices are generated automatically at check-in and finalized at check-out. They sync with the folio to reflect all charges and payments.', id: 'Faktur dibuat secara otomatis saat check-in dan difinalisasi saat check-out. Faktur tersinkron dengan folio untuk mencerminkan semua biaya dan pembayaran.' },
      { en: 'Click any invoice to view its details, including line items and totals (subtotal, tax, discount, and final total).', id: 'Klik faktur mana pun untuk melihat detailnya, termasuk item baris dan total (subtotal, pajak, diskon, dan total akhir).' },
      { en: 'Click "Print" to open a print-ready preview of the invoice. You can print directly from your browser.', id: 'Klik "Cetak" untuk membuka pratinjau siap cetak dari faktur. Anda dapat mencetak langsung dari browser Anda.' },
    ],
    tip: { en: 'The invoice shows a "CHECK IN INVOICE" label during the stay and a "FINAL CHECK OUT INVOICE" label after checkout.', id: 'Faktur menampilkan label "FAKTUR CHECK IN" selama menginap dan label "FAKTUR CHECK OUT FINAL" setelah checkout.' },
    visual: <InvoiceVisual />,
  },
  {
    id: 'reports',
    icon: <FileSpreadsheet size={20} />,
    titleEn: 'Reports',
    titleId: 'Laporan',
    roles: ['super_admin', 'manager'],
    steps: [
      { en: 'Choose a report category: Front Office (arrivals, departures, in-house guests, reservations, cancellations, no-shows), Financial (daily income, cash, EDC, OTA, outstanding balances), or Management (occupancy %, ADR, RevPAR).', id: 'Pilih kategori laporan: Front Office (kedatangan, keberangkatan, tamu menginap, reservasi, pembatalan, tidak hadir), Keuangan (pendapatan harian, tunai, EDC, OTA, saldo tertunggak), atau Manajemen (persentase hunian, ADR, RevPAR).' },
      { en: 'Set the date range using the From and To date pickers. Reports filter by business date within the selected range.', id: 'Atur rentang tanggal menggunakan pemilih tanggal Dari dan Ke. Laporan difilter berdasarkan tanggal bisnis dalam rentang terpilih.' },
      { en: 'Click a report from the left panel to generate it. Results appear on the right with summary cards and a detail table.', id: 'Klik laporan dari panel kiri untuk membuatnya. Hasil muncul di kanan dengan kartu ringkasan dan tabel detail.' },
      { en: 'Click "Export CSV" to download the report data as a spreadsheet file for further analysis.', id: 'Klik "Ekspor CSV" untuk mengunduh data laporan sebagai file spreadsheet untuk analisis lebih lanjut.' },
    ],
    tip: { en: 'The Daily Income Report shows payments by method and charges by category in separate blocks, giving you a clear picture of your revenue breakdown.', id: 'Laporan Pendapatan Harian menampilkan pembayaran per metode dan biaya per kategori dalam blok terpisah, memberikan gambaran jelas tentang rincian pendapatan Anda.' },
    visual: <ReportVisual />,
  },
  {
    id: 'night_audit',
    icon: <Moon size={20} />,
    titleEn: 'Night Audit',
    titleId: 'Audit Malam',
    roles: ['super_admin', 'manager'],
    steps: [
      { en: 'The night audit runs automatically at the business day cutoff (default 4:30 AM). It captures the day\'s activity and closes the business date.', id: 'Audit malam berjalan secara otomatis pada batas hari bisnis (default 04:30). Ini menangkap aktivitas hari ini dan menutup tanggal bisnis.' },
      { en: 'It records arrivals, departures, in-house guests, room charges, additional charges, payments, and outstanding balances.', id: 'Ini mencatat kedatangan, keberangkatan, tamu menginap, biaya kamar, biaya tambahan, pembayaran, dan saldo tertunggak.' },
      { en: 'Exceptions (like unpaid folios or rooms still occupied past checkout) are flagged for review so you can take action.', id: 'Pengecualian (seperti folio belum dibayar atau kamar masih terisi setelah checkout) ditandai untuk ditinjau sehingga Anda dapat mengambil tindakan.' },
      { en: 'Recent audit history is shown in a table at the bottom of the page, with financial summaries for each closed business day.', id: 'Riwayat audit terbaru ditampilkan dalam tabel di bagian bawah halaman, dengan ringkasan keuangan untuk setiap hari bisnis yang ditutup.' },
    ],
    tip: { en: 'You don\'t need to manually trigger the night audit — it runs automatically. Just review the results and any exceptions each morning.', id: 'Anda tidak perlu memicu audit malam secara manual — ini berjalan otomatis. Cukup tinjau hasil dan pengecualian setiap pagi.' },
    visual: <NightAuditVisual />,
  },
  {
    id: 'hotel_lock',
    icon: <KeyRound size={20} />,
    titleEn: 'Hotel Lock Integration',
    titleId: 'Integrasi Kunci Hotel',
    roles: ['super_admin', 'manager', 'receptionist'],
    steps: [
      { en: 'This page shows the status of the hotel lock system. You can see the bridge connection, encoder status, and last heartbeat. The system supports two modes: Mock (for testing) and Production (connects to a real bridge device).', id: 'Halaman ini menunjukkan status sistem kunci hotel. Anda dapat melihat koneksi bridge, status enkoder, dan heartbeat terakhir. Sistem mendukung dua mode: Mock (untuk pengujian) dan Produksi (terhubung ke perangkat bridge sungguhan).' },
      { en: 'Setup: A Super Admin must configure the integration per branch. Go to Administration → Branches, select a branch, and set the lock provider type (Mock or Production). For Production mode, enter the bridge URL (e.g. http://192.168.1.100:3000) and API key.', id: 'Pengaturan: Super Admin harus mengkonfigurasi integrasi per cabang. Buka Administrasi → Cabang, pilih cabang, dan atur jenis provider kunci (Mock atau Produksi). Untuk mode Produksi, masukkan URL bridge (mis. http://192.168.1.100:3000) dan API key.' },
      { en: 'The bridge is a small application that runs on a computer connected to the card encoder hardware. It receives encoding commands from the hotel system and sends them to the encoder via a DLL/SDK.', id: 'Bridge adalah aplikasi kecil yang berjalan di komputer yang terhubung ke perangkat enkoder kartu. Bridge menerima perintah enkoding dari sistem hotel dan mengirimkannya ke enkoder melalui DLL/SDK.' },
      { en: 'Use "Test Connection" to check if the bridge is reachable. Use "Test Encoder" to check the card encoder hardware status.', id: 'Gunakan "Tes Koneksi" untuk memeriksa apakah bridge dapat dijangkau. Gunakan "Tes Enkoder" untuk memeriksa status perangkat enkoder kartu.' },
      { en: 'Use "Test Card" to perform a test card encoding. This verifies the full workflow from connection to card write.', id: 'Gunakan "Tes Kartu" untuk melakukan enkoding kartu uji. Ini memverifikasi alur kerja lengkap dari koneksi hingga penulisan kartu.' },
      { en: 'During check-in, click "Encode Room Card" to write a guest key card. The system communicates with the bridge to encode the card with the room number and stay dates.', id: 'Saat check-in, klik "Enkoded Kartu Kamar" untuk menulis kartu kunci tamu. Sistem berkomunikasi dengan bridge untuk mengenkoded kartu dengan nomor kamar dan tanggal menginap.' },
      { en: 'At check-out, the guest key card is automatically invalidated so it no longer opens the room. When a room is transferred, the old card is also invalidated.', id: 'Saat check-out, kartu kunci tamu otomatis dibatalkan sehingga tidak dapat lagi membuka kamar. Saat kamar dipindahkan, kartu lama juga dibatalkan.' },
      { en: 'Recent card issuances and integration logs are shown at the bottom, so you can track all key card activity.', id: 'Penerbitan kartu terbaru dan log integrasi ditampilkan di bagian bawah, sehingga Anda dapat melacak semua aktivitas kartu kunci.' },
    ],
    tip: { en: 'Troubleshooting: If "Test Connection" fails, check that the bridge application is running on the encoder computer and that the bridge URL is correct. If "Test Encoder" fails, verify the encoder hardware is connected and the correct DLL is configured. If card encoding fails, ensure a blank card is placed on the encoder before clicking encode.', id: 'Pemecahan Masalah: Jika "Tes Koneksi" gagal, periksa bahwa aplikasi bridge berjalan di komputer enkoder dan URL bridge sudah benar. Jika "Tes Enkoder" gagal, verifikasi perangkat enkoder terhubung dan DLL yang benar dikonfigurasi. Jika enkoding kartu gagal, pastikan kartu kosong diletakkan di enkoder sebelum mengklik enkoded.' },
    visual: <LockVisual />,
  },
  {
    id: 'admin',
    icon: <Settings size={20} />,
    titleEn: 'Administration (Super Admin Only)',
    titleId: 'Administrasi (Super Admin Saja)',
    roles: ['super_admin'],
    steps: [
      { en: 'Branches: Create and manage hotel branches. Set standard check-in/out times and business day cutoff for each branch.', id: 'Cabang: Buat dan kelola cabang hotel. Atur waktu check-in/out standar dan batas hari bisnis untuk setiap cabang.' },
      { en: 'Room Types: Define room categories (e.g. Deluxe, Suite) with base rates, max occupancy, and default tax rates.', id: 'Jenis Kamar: Tentukan kategori kamar (mis. Deluxe, Suite) dengan tarif dasar, kapasitas maksimum, dan tarif pajak default.' },
      { en: 'Booking Sources: Track where reservations come from (Walk-in, Booking.com, Agoda, etc.). Mark OTA sources for financial reporting.', id: 'Sumber Pemesanan: Lacak dari mana reservasi berasal (Walk-in, Booking.com, Agoda, dll.). Tandai sumber OTA untuk pelaporan keuangan.' },
      { en: 'Users: Create staff accounts with roles (Receptionist, Manager, Super Admin). Assign branch access per user.', id: 'Pengguna: Buat akun staf dengan peran (Resepsionis, Manajer, Super Admin). Tetapkan akses cabang per pengguna.' },
      { en: 'Charge Categories: Define charge types (amenities, damage, etc.). Set approval thresholds for damage charges that require manager sign-off.', id: 'Kategori Biaya: Tentukan jenis biaya (fasilitas, kerusakan, dll.). Atur batas persetujuan untuk biaya kerusakan yang memerlukan persetujuan manajer.' },
      { en: 'Payment Settings: Configure payment methods (Cash, EDC, OTA/Xendit). Each method can be flagged as cash, EDC, or OTA for reporting.', id: 'Pengaturan Pembayaran: Konfigurasi metode pembayaran (Tunai, EDC, OTA/Xendit). Setiap metode dapat ditandai sebagai tunai, EDC, atau OTA untuk pelaporan.' },
      { en: 'System Settings: Set company info, default charges (early check-in, late checkout), tax rates, and document prefixes.', id: 'Pengaturan Sistem: Atur info perusahaan, biaya default (check-in awal, checkout terlambat), tarif pajak, dan prefix dokumen.' },
      { en: 'Audit Logs: View a detailed trail of all actions taken in the system, with before/after values. Search by guest name, reservation number, folio number, or invoice number.', id: 'Log Audit: Lihat jejak detail semua tindakan yang diambil dalam sistem, dengan nilai sebelum/sesudah. Cari berdasarkan nama tamu, nomor reservasi, nomor folio, atau nomor faktur.' },
    ],
    visual: <AdminVisual />,
  },
  {
    id: 'audit_logs',
    icon: <ScrollText size={20} />,
    titleEn: 'Audit Logs',
    titleId: 'Log Audit',
    roles: ['super_admin', 'manager'],
    steps: [
      { en: 'The Audit Logs page shows a chronological trail of every important action in the system, including who did it and when.', id: 'Halaman Log Audit menampilkan jejak kronologis setiap tindakan penting dalam sistem, termasuk siapa yang melakukannya dan kapan.' },
      { en: 'Each row shows the reference number (RES-, FOL-, INV-), room number, and guest name so you can quickly identify the related booking.', id: 'Setiap baris menampilkan nomor referensi (RES-, FOL-, INV-), nomor kamar, dan nama tamu sehingga Anda dapat dengan cepat mengidentifikasi pemesanan terkait.' },
      { en: 'Use the date pickers to narrow results to a specific date range. Use the action dropdown to filter by action type (check-in, payment, charge, etc.).', id: 'Gunakan pemilih tanggal untuk mempersempit hasil ke rentang tanggal tertentu. Gunakan dropdown tindakan untuk memfilter berdasarkan jenis tindakan (check-in, pembayaran, biaya, dll).' },
      { en: 'Use the search box to filter by guest name, reservation number (RES-), folio number (FOL-), invoice number (INV-), or room number.', id: 'Gunakan kotak pencarian untuk memfilter berdasarkan nama tamu, nomor reservasi (RES-), nomor folio (FOL-), nomor faktur (INV-), atau nomor kamar.' },
      { en: 'Click any row with details to expand and see the previous and new values, plus any reason provided for the action.', id: 'Klik baris mana pun dengan detail untuk memperluas dan melihat nilai sebelum dan baru, ditambah alasan yang diberikan untuk tindakan tersebut.' },
    ],
    tip: { en: 'Early check-in and late checkout decisions (with or without charge) are logged here for accountability. You can review who approved each override.', id: 'Keputusan check-in awal dan check-out terlambat (dengan atau tanpa biaya) dicatat di sini untuk akuntabilitas. Anda dapat meninjau siapa yang menyetujui setiap lewati.' },
    visual: <AuditVisual />,
  },
];

export function GuidePage() {
  const { user } = useAuth();
  const { t, language } = useI18n();
  const [expandedId, setExpandedId] = useState<string | null>('dashboard');

  const isEn = language === 'en';

  const userRole = user?.role || 'receptionist';
  const visibleSections = SECTIONS.filter((s) => s.roles.includes(userRole));

  const roleLabel = isEn
    ? { super_admin: 'Super Admin', manager: 'Manager', receptionist: 'Receptionist' }[userRole]
    : { super_admin: 'Super Admin', manager: 'Manajer', receptionist: 'Resepsionis' }[userRole];

  return (
    <div className="space-y-6 max-w-5xl mx-auto">
      <div className="flex items-center gap-3">
        <div className="rounded-xl bg-blue-600 p-3 text-white">
          <BookOpen size={28} />
        </div>
        <div>
          <h1 className="text-2xl font-bold text-slate-900">
            {isEn ? 'User Guide' : 'Panduan Pengguna'}
          </h1>
          <p className="text-sm text-slate-500">
            {isEn
              ? `Learn how to use the hotel management system — tailored for your role (${roleLabel}).`
              : `Pelajari cara menggunakan sistem manajemen hotel — disesuaikan untuk peran Anda (${roleLabel}).`}
          </p>
        </div>
      </div>

      {/* Sections */}
      <div className="space-y-3">
        {visibleSections.map((section) => {
          const isExpanded = expandedId === section.id;
          return (
            <Card key={section.id} noPadding>
              <button
                onClick={() => setExpandedId(isExpanded ? null : section.id)}
                className="w-full flex items-center gap-3 px-5 py-4 hover:bg-slate-50 transition-colors text-left"
              >
                <div className="rounded-lg bg-blue-50 p-2 text-blue-600">
                  {section.icon}
                </div>
                <div className="flex-1">
                  <h3 className="font-semibold text-slate-800">
                    {isEn ? section.titleEn : section.titleId}
                  </h3>
                </div>
                {isExpanded ? <ChevronUp size={20} className="text-slate-400" /> : <ChevronDown size={20} className="text-slate-400" />}
              </button>

              {isExpanded && (
                <div className="px-5 pb-5 space-y-4">
                  {/* Steps */}
                  <ol className="space-y-3">
                    {section.steps.map((step, i) => (
                      <li key={i} className="flex gap-3">
                        <span className="flex-shrink-0 w-6 h-6 rounded-full bg-blue-100 text-blue-700 flex items-center justify-center text-xs font-bold">
                          {i + 1}
                        </span>
                        <span className="text-sm text-slate-600 pt-0.5">
                          {isEn ? step.en : step.id}
                        </span>
                      </li>
                    ))}
                  </ol>

                  {/* Tip */}
                  {section.tip && (
                    <div className="flex items-start gap-2 bg-amber-50 border border-amber-200 rounded-lg p-3">
                      <Lightbulb size={16} className="text-amber-600 flex-shrink-0 mt-0.5" />
                      <span className="text-sm text-amber-700">
                        <strong>{isEn ? 'Tip: ' : 'Tips: '}</strong>
                        {isEn ? section.tip.en : section.tip.id}
                      </span>
                    </div>
                  )}

                  {/* Visual example */}
                  <div>
                    <p className="text-xs font-semibold text-slate-500 uppercase tracking-wide mb-2">
                      {isEn ? 'Visual Example' : 'Contoh Visual'}
                    </p>
                    {section.visual}
                  </div>
                </div>
              )}
            </Card>
          );
        })}
      </div>

      {/* Footer note */}
      <div className="flex items-start gap-2 bg-blue-50 border border-blue-200 rounded-lg p-4">
        <Info size={18} className="text-blue-600 flex-shrink-0 mt-0.5" />
        <div className="text-sm text-blue-700">
          <p className="font-medium mb-1">
            {isEn ? 'Need more help?' : 'Butuh bantuan lebih?'}
          </p>
          <p>
            {isEn
              ? 'This system uses demo data for testing. All financial transactions are simulated. Contact your system administrator for production setup.'
              : 'Sistem ini menggunakan data demo untuk pengujian. Semua transaksi keuangan disimulasikan. Hubungi administrator sistem Anda untuk pengaturan produksi.'}
          </p>
        </div>
      </div>
    </div>
  );
}

/* ── Visual Components ── */

function DashboardVisual() {
  return (
    <div className="border border-slate-200 rounded-lg p-4 bg-slate-50">
      <div className="grid grid-cols-4 gap-2 mb-3">
        {['Total: 50', 'Occupied: 32', 'Available: 12', 'Dirty: 6'].map((label, i) => (
          <div key={i} className="bg-white rounded-lg p-2 text-center border border-slate-200">
            <p className="text-xs text-slate-500">{label.split(':')[0]}</p>
            <p className="text-lg font-bold text-slate-800">{label.split(':')[1]}</p>
          </div>
        ))}
      </div>
      <div className="grid grid-cols-3 gap-2">
        <div className="bg-blue-50 rounded-lg p-2 text-center"><p className="text-xs text-slate-500">Arrivals</p><p className="font-bold text-blue-700">8</p></div>
        <div className="bg-amber-50 rounded-lg p-2 text-center"><p className="text-xs text-slate-500">Departures</p><p className="font-bold text-amber-700">5</p></div>
        <div className="bg-emerald-50 rounded-lg p-2 text-center"><p className="text-xs text-slate-500">In-House</p><p className="font-bold text-emerald-700">32</p></div>
      </div>
    </div>
  );
}

function ReservationVisual() {
  return (
    <div className="border border-slate-200 rounded-lg p-4 bg-slate-50">
      <div className="bg-white rounded-lg p-3 border border-slate-200 space-y-2">
        <div className="flex gap-2">
          <div className="flex-1 bg-slate-100 rounded px-2 py-1 text-xs text-slate-600">Branch: Jakarta</div>
          <div className="flex-1 bg-slate-100 rounded px-2 py-1 text-xs text-slate-600">Guest: John Doe</div>
        </div>
        <div className="flex gap-2">
          <div className="flex-1 bg-slate-100 rounded px-2 py-1 text-xs text-slate-600">Check-in: 21 Aug</div>
          <div className="flex-1 bg-slate-100 rounded px-2 py-1 text-xs text-slate-600">Check-out: 23 Aug</div>
        </div>
        <div className="flex items-center justify-between pt-1">
          <span className="text-xs text-slate-500">Room 101 · Deluxe</span>
          <span className="text-sm font-bold text-blue-700">Rp 1.500.000</span>
        </div>
        <div className="bg-blue-600 text-white text-xs text-center rounded-lg py-1.5 font-medium">Save Reservation</div>
      </div>
    </div>
  );
}

function CalendarVisual() {
  return (
    <div className="border border-slate-200 rounded-lg p-4 bg-slate-50">
      <div className="bg-white rounded-lg p-2 border border-slate-200">
        <div className="flex gap-1 mb-1">
          <div className="w-12 text-xs text-slate-400 font-medium">Room</div>
          {['21', '22', '23', '24'].map((d) => (
            <div key={d} className="flex-1 text-center text-xs text-slate-500">{d}</div>
          ))}
        </div>
        {[
          { room: '101', bars: [{ left: 0, width: 50, color: 'bg-emerald-500', label: 'John D.' }] },
          { room: '102', bars: [{ left: 25, width: 50, color: 'bg-blue-500', label: 'Jane S.' }] },
          { room: '103', bars: [{ left: 0, width: 100, color: 'bg-emerald-500', label: 'Bob M.' }] },
        ].map((row) => (
          <div key={row.room} className="flex gap-1 mb-1 items-center">
            <div className="w-12 text-xs text-slate-600 font-medium">{row.room}</div>
            <div className="flex-1 relative h-6 bg-slate-50 rounded">
              {row.bars.map((bar, i) => (
                <div
                  key={i}
                  className={`absolute top-0 h-6 rounded px-1 text-xs text-white flex items-center ${bar.color}`}
                  style={{ left: `${bar.left}%`, width: `${bar.width}%` }}
                >
                  {bar.label}
                </div>
              ))}
            </div>
          </div>
        ))}
      </div>
      <div className="flex gap-3 mt-2 text-xs">
        <span className="flex items-center gap-1"><span className="w-3 h-3 rounded bg-emerald-500"></span> Checked In</span>
        <span className="flex items-center gap-1"><span className="w-3 h-3 rounded bg-blue-500"></span> Confirmed</span>
      </div>
    </div>
  );
}

function RoomGridVisual() {
  const rooms = [
    { num: '101', status: 'available', color: 'border-emerald-400 bg-emerald-50' },
    { num: '102', status: 'occupied', color: 'border-red-400 bg-red-50' },
    { num: '103', status: 'dirty', color: 'border-amber-400 bg-amber-50' },
    { num: '104', status: 'cleaning', color: 'border-teal-400 bg-teal-50' },
    { num: '105', status: 'available', color: 'border-emerald-400 bg-emerald-50' },
    { num: '106', status: 'out_of_order', color: 'border-slate-400 bg-slate-100' },
  ];
  return (
    <div className="border border-slate-200 rounded-lg p-4 bg-slate-50">
      <p className="text-xs font-semibold text-slate-600 mb-2">Deluxe Rooms</p>
      <div className="grid grid-cols-3 sm:grid-cols-6 gap-2">
        {rooms.map((r) => (
          <div key={r.num} className={`flex flex-col items-center gap-1 p-2 rounded-lg border-2 ${r.color}`}>
            <span className="font-bold text-sm text-slate-800">{r.num}</span>
            <span className="text-xs text-slate-500 capitalize">{r.status.replace(/_/g, ' ')}</span>
          </div>
        ))}
      </div>
    </div>
  );
}

function GuestVisual() {
  return (
    <div className="border border-slate-200 rounded-lg p-4 bg-slate-50">
      <div className="bg-white rounded-lg p-3 border border-slate-200">
        <div className="flex items-center justify-between mb-2">
          <span className="font-medium text-slate-800">John Doe</span>
          <Badge color="blue">Checked In</Badge>
        </div>
        <div className="grid grid-cols-3 gap-2 text-center">
          <div className="bg-blue-50 rounded p-2"><p className="text-xs text-slate-500">Total Stays</p><p className="font-bold text-blue-700">3</p></div>
          <div className="bg-emerald-50 rounded p-2"><p className="text-xs text-slate-500">Total Spending</p><p className="font-bold text-emerald-700 text-xs">Rp 4.5M</p></div>
          <div className="bg-red-50 rounded p-2"><p className="text-xs text-slate-500">Outstanding</p><p className="font-bold text-red-700 text-xs">Rp 0</p></div>
        </div>
      </div>
    </div>
  );
}

function CheckinVisual() {
  return (
    <div className="border border-slate-200 rounded-lg p-4 bg-slate-50">
      <div className="grid grid-cols-2 gap-3">
        <div className="bg-white rounded-lg p-3 border border-slate-200">
          <p className="text-xs font-semibold text-slate-600 mb-2">Arrivals</p>
          <div className="flex items-center justify-between border border-slate-100 rounded p-2">
            <div>
              <p className="text-sm font-medium text-slate-800">John Doe</p>
              <p className="text-xs text-slate-500">RES-001 · Room 101</p>
            </div>
            <div className="bg-blue-600 text-white text-xs rounded px-2 py-1">Check In</div>
          </div>
        </div>
        <div className="bg-white rounded-lg p-3 border border-slate-200">
          <p className="text-xs font-semibold text-slate-600 mb-2">Departures Today</p>
          <div className="flex items-center justify-between border border-slate-100 rounded p-2">
            <div>
              <p className="text-sm font-medium text-slate-800">Jane Smith</p>
              <p className="text-xs text-slate-500">RES-002 · Room 102</p>
            </div>
            <div className="bg-amber-500 text-white text-xs rounded px-2 py-1">Check Out</div>
          </div>
        </div>
      </div>
      <div className="mt-2 bg-amber-50 border border-amber-200 rounded-lg p-2 flex items-center gap-2">
        <AlertCircle size={14} className="text-amber-600" />
        <span className="text-xs text-amber-700">Late check-out detected — 2h 30m past standard time</span>
      </div>
    </div>
  );
}

function FolioVisual() {
  return (
    <div className="border border-slate-200 rounded-lg p-4 bg-slate-50">
      <div className="bg-white rounded-lg p-3 border border-slate-200">
        <table className="w-full text-xs">
          <thead>
            <tr className="border-b border-slate-200 text-slate-500">
              <th className="text-left py-1 px-2">Folio</th>
              <th className="text-left py-1 px-2">Guest</th>
              <th className="text-left py-1 px-2">Room</th>
              <th className="text-left py-1 px-2">Balance</th>
            </tr>
          </thead>
          <tbody>
            <tr className="border-b border-slate-100">
              <td className="py-1 px-2 text-blue-600 font-medium">FOL-001</td>
              <td className="py-1 px-2">John Doe</td>
              <td className="py-1 px-2">101</td>
              <td className="py-1 px-2 text-red-600">Rp 500K</td>
            </tr>
            <tr>
              <td className="py-1 px-2 text-blue-600 font-medium">FOL-002</td>
              <td className="py-1 px-2">Jane Smith</td>
              <td className="py-1 px-2">102</td>
              <td className="py-1 px-2 text-emerald-600">Rp 0</td>
            </tr>
          </tbody>
        </table>
      </div>
      <div className="flex gap-2 mt-2">
        <div className="bg-blue-600 text-white text-xs rounded px-2 py-1">+ Add Charge</div>
        <div className="bg-emerald-600 text-white text-xs rounded px-2 py-1">+ Take Payment</div>
        <div className="bg-white border border-slate-300 text-slate-600 text-xs rounded px-2 py-1">Transfer Room</div>
      </div>
    </div>
  );
}

function InvoiceVisual() {
  return (
    <div className="border border-slate-200 rounded-lg p-4 bg-slate-50">
      <div className="bg-white rounded-lg p-4 border border-slate-200">
        <div className="flex justify-between mb-3">
          <div>
            <p className="font-bold text-slate-800">Grand Hotel Jakarta</p>
            <p className="text-xs text-slate-500">Jl. Sudirman No. 1</p>
          </div>
          <div className="text-right">
            <p className="text-xs text-slate-500">Invoice</p>
            <p className="font-medium text-blue-600">INV-001</p>
          </div>
        </div>
        <div className="border-t border-slate-200 pt-2 space-y-1 text-xs">
          <div className="flex justify-between"><span>Room charge (2 nights)</span><span>Rp 3.000.000</span></div>
          <div className="flex justify-between"><span>Tax</span><span>Rp 300.000</span></div>
          <div className="flex justify-between font-bold pt-1 border-t border-slate-100"><span>Total</span><span>Rp 3.300.000</span></div>
        </div>
        <div className="mt-2 bg-slate-100 text-center text-xs rounded py-1.5">Print</div>
      </div>
    </div>
  );
}

function ReportVisual() {
  return (
    <div className="border border-slate-200 rounded-lg p-4 bg-slate-50">
      <div className="flex gap-3">
        <div className="w-1/3 space-y-1">
          <div className="bg-blue-600 text-white text-xs rounded px-2 py-1.5">Daily Income</div>
          <div className="bg-white border border-slate-200 text-slate-600 text-xs rounded px-2 py-1.5">Cash Report</div>
          <div className="bg-white border border-slate-200 text-slate-600 text-xs rounded px-2 py-1.5">EDC Report</div>
          <div className="bg-white border border-slate-200 text-slate-600 text-xs rounded px-2 py-1.5">Occupancy %</div>
        </div>
        <div className="flex-1 bg-white rounded-lg p-3 border border-slate-200">
          <p className="text-xs font-semibold text-slate-600 mb-2">Payments by Method</p>
          <div className="space-y-1 text-xs">
            <div className="flex justify-between"><span>Cash</span><span className="text-emerald-700 font-medium">Rp 2.5M</span></div>
            <div className="flex justify-between"><span>EDC</span><span className="text-emerald-700 font-medium">Rp 5.0M</span></div>
            <div className="flex justify-between border-t border-slate-100 pt-1 font-bold"><span>Total</span><span className="text-emerald-700">Rp 7.5M</span></div>
          </div>
        </div>
      </div>
    </div>
  );
}

function NightAuditVisual() {
  return (
    <div className="border border-slate-200 rounded-lg p-4 bg-slate-50">
      <div className="bg-white rounded-lg p-3 border border-slate-200">
        <div className="flex items-center gap-2 mb-2">
          <CheckCircle2 size={16} className="text-emerald-600" />
          <span className="text-sm font-medium text-slate-700">No exceptions detected</span>
        </div>
        <div className="grid grid-cols-4 gap-2 text-center">
          <div className="bg-blue-50 rounded p-2"><p className="text-xs text-slate-500">Arrivals</p><p className="font-bold text-blue-700">8</p></div>
          <div className="bg-amber-50 rounded p-2"><p className="text-xs text-slate-500">Departures</p><p className="font-bold text-amber-700">5</p></div>
          <div className="bg-emerald-50 rounded p-2"><p className="text-xs text-slate-500">In-House</p><p className="font-bold text-emerald-700">32</p></div>
          <div className="bg-red-50 rounded p-2"><p className="text-xs text-slate-500">No Shows</p><p className="font-bold text-red-700">1</p></div>
        </div>
      </div>
    </div>
  );
}

function AdminVisual() {
  return (
    <div className="border border-slate-200 rounded-lg p-4 bg-slate-50">
      <div className="grid grid-cols-2 sm:grid-cols-4 gap-2">
        {[
          { icon: <Building2 size={16} />, label: 'Branches' },
          { icon: <DoorOpen size={16} />, label: 'Room Types' },
          { icon: <Tags size={16} />, label: 'Booking Sources' },
          { icon: <UsersIcon size={16} />, label: 'Users' },
          { icon: <Tags size={16} />, label: 'Charge Categories' },
          { icon: <CreditCard size={16} />, label: 'Payment Methods' },
          { icon: <Settings size={16} />, label: 'System Settings' },
          { icon: <ScrollText size={16} />, label: 'Audit Logs' },
        ].map((item) => (
          <div key={item.label} className="bg-white rounded-lg p-2 border border-slate-200 flex items-center gap-2">
            <span className="text-blue-600">{item.icon}</span>
            <span className="text-xs text-slate-600 font-medium">{item.label}</span>
          </div>
        ))}
      </div>
    </div>
  );
}

function AuditVisual() {
  return (
    <div className="border border-slate-200 rounded-lg p-4 bg-slate-50">
      <div className="bg-white rounded-lg p-3 border border-slate-200 space-y-2">
        <div className="flex gap-2 items-end">
          <div className="bg-slate-100 rounded px-2 py-1 text-xs text-slate-600">From: 01 Aug</div>
          <div className="bg-slate-100 rounded px-2 py-1 text-xs text-slate-600">To: 21 Aug</div>
          <div className="flex-1 bg-slate-100 rounded px-2 py-1 text-xs text-slate-500">Search: RES-001...</div>
        </div>
        <div className="border border-slate-100 rounded p-2 flex items-center gap-2">
          <Badge color="teal">Check In</Badge>
          <span className="text-xs text-slate-700 font-medium">John Doe</span>
          <span className="text-xs text-slate-400">·</span>
          <span className="text-xs text-slate-500">RES-001</span>
          <span className="text-xs text-slate-400">·</span>
          <span className="text-xs text-slate-500">Room 106</span>
          <span className="text-xs text-slate-400 ml-auto">21 Aug 14:30</span>
        </div>
        <div className="border border-slate-100 rounded p-2 flex items-center gap-2">
          <Badge color="green">Payment</Badge>
          <span className="text-xs text-slate-700 font-medium">Jane Smith</span>
          <span className="text-xs text-slate-400">·</span>
          <span className="text-xs text-slate-500">FOL-002 · Rp 500K</span>
          <span className="text-xs text-slate-400 ml-auto">21 Aug 15:00</span>
        </div>
      </div>
    </div>
  );
}

function LockVisual() {
  return (
    <div className="border border-slate-200 rounded-lg p-4 bg-slate-50">
      <div className="grid grid-cols-3 gap-2">
        <div className="bg-white rounded-lg p-3 border border-slate-200">
          <div className="flex items-center gap-2">
            <div className="rounded p-1.5 bg-emerald-50 text-emerald-600"><KeyRound size={16} /></div>
            <div>
              <p className="text-xs text-slate-500">Bridge</p>
              <p className="text-sm font-bold text-emerald-700">Connected</p>
            </div>
          </div>
        </div>
        <div className="bg-white rounded-lg p-3 border border-slate-200">
          <div className="flex items-center gap-2">
            <div className="rounded p-1.5 bg-emerald-50 text-emerald-600"><KeyRound size={16} /></div>
            <div>
              <p className="text-xs text-slate-500">Encoder</p>
              <p className="text-sm font-bold text-emerald-700">Connected</p>
            </div>
          </div>
        </div>
        <div className="bg-white rounded-lg p-3 border border-slate-200">
          <div className="flex items-center gap-2">
            <div className="rounded p-1.5 bg-blue-50 text-blue-600"><Info size={16} /></div>
            <div>
              <p className="text-xs text-slate-500">Mode</p>
              <p className="text-sm font-bold text-amber-700">Mock</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
