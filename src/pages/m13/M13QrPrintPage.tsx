import { useEffect, useState } from 'react';
import QRCode from 'qrcode';
import { Button } from '@/components/ui/Button';
import { Printer, QrCode as QrCodeIcon, Hotel } from 'lucide-react';

const M13_URL = 'https://millennium-pms.netlify.app/#/m13';

export function M13QrPrintPage() {
  const [qrDataUrl, setQrDataUrl] = useState<string>('');

  useEffect(() => {
    QRCode.toDataURL(M13_URL, {
      width: 400,
      margin: 2,
      color: { dark: '#0f172a', light: '#ffffff' },
      errorCorrectionLevel: 'M',
    }).then(setQrDataUrl).catch(() => setQrDataUrl(''));
  }, []);

  return (
    <div className="print-overlay min-h-screen overflow-y-auto bg-slate-200 print:bg-white">
      <style>{`
        @page { size: A4; margin: 12mm; }
        @media print {
          body { background: white; }
          .no-print { display: none !important; }
          .qr-shell { max-width: none !important; min-height: auto !important; box-shadow: none !important; }
        }
      `}</style>

      <div className="no-print sticky top-0 z-10 flex items-center justify-between border-b border-slate-200 bg-white px-4 py-3 shadow-sm">
        <h2 className="text-lg font-semibold text-slate-800">M13 Club — QR Code Printout</h2>
        <div className="flex items-center gap-2">
          <Button variant="outline" size="sm" onClick={() => window.print()}><Printer size={16} /> Print</Button>
        </div>
      </div>

      <main className="qr-shell mx-auto my-6 max-w-[620px] bg-white px-10 py-12 shadow-xl print:my-0 print:px-0 print:py-0">
        {/* Header */}
        <header className="text-center border-b-2 border-slate-800 pb-6 mb-8">
          <div className="inline-flex items-center gap-3 mb-3">
            <div className="w-14 h-14 rounded-2xl bg-amber-500 flex items-center justify-center">
              <Hotel size={28} className="text-slate-900" />
            </div>
            <div className="text-left">
              <p className="text-3xl font-bold text-slate-900 tracking-tight">M13 Club</p>
              <p className="text-sm text-slate-500">Membership Rewards</p>
            </div>
          </div>
        </header>

        {/* QR Code */}
        <section className="flex flex-col items-center mb-8">
          <p className="text-lg font-semibold text-slate-800 mb-4">
            Scan QR Code di Bawah Ini
          </p>
          {qrDataUrl ? (
            <div className="p-4 border-2 border-slate-200 rounded-xl bg-white">
              <img src={qrDataUrl} alt="QR Code M13 Club" className="w-64 h-64" />
            </div>
          ) : (
            <div className="w-64 h-64 flex items-center justify-center bg-slate-100 rounded-xl">
              <QrCodeIcon size={48} className="text-slate-300" />
            </div>
          )}
          <p className="mt-4 text-sm text-slate-500 break-all text-center max-w-sm">
            {M13_URL}
          </p>
        </section>

        {/* Instructions */}
        <section className="space-y-5">
          <h2 className="text-xl font-bold text-slate-900 border-b border-slate-200 pb-2">
            Cara Mengakses M13 Club
          </h2>

          <div className="space-y-4">
            <InstructionStep
              number={1}
              title="Scan QR Code"
              desc="Buka aplikasi kamera atau QR scanner pada ponsel Anda, lalu arahkan kamera ke QR code di atas."
            />
            <InstructionStep
              number={2}
              title="Buka Tautan"
              desc="Setelah QR code terbaca, ketuk tautan yang muncul di layar ponsel Anda untuk membuka halaman M13 Club."
            />
            <InstructionStep
              number={3}
              title="Masuk / Login"
              desc="Masukkan email dan kata sandi akun M13 Club Anda. Jika belum punya akun, hubungi resepsionis hotel untuk pendaftaran."
            />
            <InstructionStep
              number={4}
              title="Lihat Poin & Hadiah"
              desc="Setelah masuk, Anda dapat melihat saldo poin, menukar hadiah, dan melihat riwayat transaksi poin Anda."
            />
          </div>

          {/* Info Box */}
          <div className="mt-6 rounded-lg border-2 border-amber-300 bg-amber-50 px-5 py-4">
            <h3 className="font-bold text-amber-900 text-sm mb-2">
              Informasi Penting
            </h3>
            <ul className="space-y-1.5 text-sm text-amber-800">
              <li className="flex gap-2">
                <span className="font-bold">•</span>
                <span>Poin M13 Club berlaku untuk setiap pengeluaran yang tercatat di hotel.</span>
              </li>
              <li className="flex gap-2">
                <span className="font-bold">•</span>
                <span>Poin dapat ditukar dengan berbagai hadiah menarik yang tersedia di aplikasi.</span>
              </li>
              <li className="flex gap-2">
                <span className="font-bold">•</span>
                <span>Jika lupa kata sandi, gunakan fitur "Lupa Kata Sandi" pada halaman masuk.</span>
              </li>
              <li className="flex gap-2">
                <span className="font-bold">•</span>
                <span>Untuk pertanyaan dan bantuan, silakan hubungi resepsionis hotel.</span>
              </li>
            </ul>
          </div>
        </section>

        {/* Footer */}
        <footer className="mt-10 pt-6 border-t border-slate-200 text-center">
          <p className="text-sm font-medium text-slate-700">
            Selamat Bergabung di M13 Club!
          </p>
          <p className="mt-1 text-xs text-slate-400">
            Gandakan &amp; bagikan QR code ini kepada tamu hotel.
          </p>
        </footer>
      </main>
    </div>
  );
}

function InstructionStep({ number, title, desc }: { number: number; title: string; desc: string }) {
  return (
    <div className="flex gap-4">
      <div className="flex-shrink-0 w-9 h-9 rounded-full bg-slate-900 text-white flex items-center justify-center font-bold text-sm">
        {number}
      </div>
      <div className="flex-1">
        <h3 className="font-semibold text-slate-900 text-sm">{title}</h3>
        <p className="text-sm text-slate-600 mt-0.5 leading-relaxed">{desc}</p>
      </div>
    </div>
  );
}
