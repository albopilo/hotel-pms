import { useEffect, useState } from 'react';
import { supabase } from '@/lib/supabase';
import { formatIDR, formatDate, formatTime, formatDateTime } from '@/lib/format';
import { Button } from '@/components/ui/Button';
import { X, Printer } from 'lucide-react';
import type { Branch, Guest, Reservation, Room, RoomType, BookingSource, ReservationRoom } from '@/types/database';

interface Props {
  reservationId: string;
  onClose: () => void;
}

interface GroupRoom {
  id: string;
  rate: number;
  num_nights: number;
  room?: { room_number: string } | null;
  room_type?: { name: string } | null;
}

export function GrcPrintPage({ reservationId, onClose }: Props) {
  const [reservation, setReservation] = useState<Reservation | null>(null);
  const [guest, setGuest] = useState<Guest | null>(null);
  const [branch, setBranch] = useState<Branch | null>(null);
  const [room, setRoom] = useState<Room | null>(null);
  const [roomType, setRoomType] = useState<RoomType | null>(null);
  const [bookingSource, setBookingSource] = useState<BookingSource | null>(null);
  const [groupRooms, setGroupRooms] = useState<GroupRoom[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let cancelled = false;
    (async () => {
      try {
        const { data: res, error } = await supabase
          .from('reservations')
          .select('*, primary_guest:guests(*), room:rooms(*), room_type:room_types(*)')
          .eq('id', reservationId)
          .maybeSingle();
        if (cancelled) return;
        if (error || !res) { if (!cancelled) setLoading(false); return; }

        const r = res as any;
        setReservation(r);
        setGuest(r.primary_guest as Guest);
        setRoom(r.room as Room);
        setRoomType(r.room_type as RoomType);

        const { data: br } = await supabase.from('branches').select('*').eq('id', r.branch_id).maybeSingle();
        setBranch(br as Branch | null);

        if (r.booking_source_id) {
          const { data: bs } = await supabase.from('booking_sources').select('*').eq('id', r.booking_source_id).maybeSingle();
          setBookingSource(bs as BookingSource | null);
        }

        if (r.is_group) {
          const { data: rrData } = await supabase
            .from('reservation_rooms')
            .select('id, rate, num_nights, room:rooms(room_number), room_type:room_types(name)')
            .eq('reservation_id', r.id)
            .eq('status', 'active')
            .order('created_at');
          setGroupRooms((rrData as unknown as GroupRoom[]) || []);
        }

        setLoading(false);
      } catch {
        if (!cancelled) setLoading(false);
      }
    })();
    return () => { cancelled = true; };
  }, [reservationId]);

  if (loading) {
    return (
      <div className="fixed inset-0 z-[60] bg-white flex items-center justify-center">
        <p className="text-slate-500">Loading registration card...</p>
      </div>
    );
  }

  if (!reservation) {
    return (
      <div className="fixed inset-0 z-[60] bg-white flex flex-col items-center justify-center gap-4">
        <p className="text-slate-500">Reservation not found.</p>
        <Button variant="outline" onClick={onClose}>Close</Button>
      </div>
    );
  }

  const roomNumbers = groupRooms.length > 0
    ? groupRooms.map(gr => gr.room?.room_number).filter(Boolean).join(', ')
    : room?.room_number || '-';

  return (
    <div className="print-overlay fixed inset-0 z-[60] overflow-y-auto bg-slate-200 print:bg-white">
      <style>{`
        @page { size: A4; margin: 12mm; }
        @media print {
          body { background: white; }
          .no-print { display: none !important; }
          .grc-shell { max-width: none !important; min-height: auto !important; box-shadow: none !important; }
        }
      `}</style>

      <div className="no-print sticky top-0 z-10 flex items-center justify-between border-b border-slate-200 bg-white px-4 py-3 shadow-sm">
        <h2 className="text-lg font-semibold text-slate-800">Guest Registration Card — {reservation.reservation_number}</h2>
        <div className="flex items-center gap-2">
          <Button variant="outline" size="sm" onClick={() => window.print()}><Printer size={16} /> Print</Button>
          <Button variant="secondary" size="sm" onClick={onClose}><X size={16} /> Close</Button>
        </div>
      </div>

      <main className="grc-shell mx-auto my-6 min-h-[1120px] max-w-[820px] bg-white px-8 py-10 text-[13px] text-slate-900 shadow-xl print:my-0 print:px-0 print:py-0">
        <header className="text-center">
          <h1 className="text-2xl font-bold">{branch?.name || 'Hotel'}</h1>
          {branch?.address && <p className="mt-1 text-sm text-slate-500">{branch.address}</p>}
          {branch?.phone && <p className="text-sm text-slate-500">Tel: {branch.phone}</p>}
          <h2 className="mt-4 text-lg font-semibold tracking-wide">GUEST REGISTRATION CARD</h2>
        </header>

        <section className="mt-7 overflow-hidden rounded-sm border-2 border-slate-300">
          <h3 className="border-b border-slate-200 bg-slate-50 px-4 py-2 text-center text-sm font-bold">Guest Information</h3>
          <div className="grid grid-cols-1 gap-x-8 gap-y-2 px-4 py-4 sm:grid-cols-2">
            <InfoRow label="Name" value={guest?.full_name || '-'} />
            <InfoRow label="ID Type" value={guest?.id_type || '-'} />
            <InfoRow label="ID Number" value={guest?.id_number || '-'} />
            <InfoRow label="Nationality" value={guest?.nationality || '-'} />
            <InfoRow label="Phone" value={guest?.phone || '-'} />
            <InfoRow label="Email" value={guest?.email || '-'} />
            <InfoRow label="Address" value={guest?.address || '-'} />
          </div>
        </section>

        <section className="mt-5 overflow-hidden rounded-sm border-2 border-slate-300">
          <h3 className="border-b border-slate-200 bg-slate-50 px-4 py-2 text-center text-sm font-bold">Stay Details</h3>
          <div className="px-4 py-4">
            <div className="grid grid-cols-1 gap-x-8 gap-y-2 sm:grid-cols-2">
              <InfoRow label="Reservation No." value={reservation.reservation_number} />
              <InfoRow label="Room Number" value={roomNumbers} />
              <InfoRow label="Room Type" value={roomType?.name || '-'} />
              <InfoRow label="Booking Source" value={bookingSource?.name || '-'} />
              <InfoRow label="Check In" value={`${formatDate(reservation.check_in_date)} ${formatTime(reservation.check_in_time)}`} />
              <InfoRow label="Check Out" value={`${formatDate(reservation.check_out_date)} ${formatTime(reservation.check_out_time)}`} />
              <InfoRow label="Nights" value={String(reservation.num_nights)} />
              <InfoRow label="Guests" value={`${reservation.adults} Adults / ${reservation.children} Children`} />
              <InfoRow label="Rate / Night" value={formatIDR(reservation.rate)} />
              <InfoRow label="Deposit" value={formatIDR(reservation.deposit)} />
            </div>

            {groupRooms.length > 1 && (
              <div className="mt-4 overflow-hidden rounded border border-slate-200">
                <div className="border-b border-slate-200 bg-slate-50 px-3 py-2 text-xs font-bold">Group Room Breakdown</div>
                <table className="w-full text-xs">
                  <thead>
                    <tr className="border-b border-slate-200 text-left text-slate-500">
                      <th className="px-3 py-2">Room</th>
                      <th className="px-3 py-2">Room Type</th>
                      <th className="px-3 py-2 text-center">Nights</th>
                      <th className="px-3 py-2 text-right">Rate</th>
                    </tr>
                  </thead>
                  <tbody>
                    {groupRooms.map(gr => (
                      <tr key={gr.id} className="border-b border-slate-100 last:border-0">
                        <td className="px-3 py-2">{gr.room?.room_number || 'Unassigned'}</td>
                        <td className="px-3 py-2">{gr.room_type?.name || '-'}</td>
                        <td className="px-3 py-2 text-center">{gr.num_nights}</td>
                        <td className="px-3 py-2 text-right">{formatIDR(gr.rate)}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}

            {reservation.special_requests && (
              <div className="mt-4 text-sm">
                <span className="font-medium text-slate-600">Special Requests: </span>
                <span>{reservation.special_requests}</span>
              </div>
            )}
          </div>
        </section>

        <section className="mt-5 overflow-hidden rounded-sm border-2 border-slate-300">
          <h3 className="border-b border-slate-200 bg-slate-50 px-4 py-2 text-center text-sm font-bold">Hotel Policies</h3>
          <div className="px-4 py-4 space-y-2 text-sm text-slate-700">
            <div className="flex gap-2">
              <input type="checkbox" className="mt-0.5 flex-shrink-0" />
              <span>Check-in / Check-out Policy</span>
            </div>
            <div className="flex gap-2">
              <input type="checkbox" className="mt-0.5 flex-shrink-0" />
              <span>Payment Policy</span>
            </div>
            <div className="flex gap-2">
              <input type="checkbox" className="mt-0.5 flex-shrink-0" />
              <span>Cancellation Policy</span>
            </div>
            <div className="flex gap-2">
              <input type="checkbox" className="mt-0.5 flex-shrink-0" />
              <span>Liability Disclaimer</span>
            </div>
            <div className="flex gap-2">
              <input type="checkbox" className="mt-0.5 flex-shrink-0" />
              <span>Smoking Policy</span>
            </div>
            <div className="flex gap-2">
              <input type="checkbox" className="mt-0.5 flex-shrink-0" />
              <span>Lost & Found Policy</span>
            </div>
          </div>
        </section>

        <section className="mt-8 grid grid-cols-2 gap-12">
          <div className="text-center">
            <div className="border-b border-slate-400 pb-12" />
            <p className="mt-2 text-sm font-medium">Guest Signature</p>
          </div>
          <div className="text-center">
            <div className="border-b border-slate-400 pb-12" />
            <p className="mt-2 text-sm font-medium">Receptionist Signature</p>
          </div>
        </section>

        <footer className="mt-10 text-center text-xs text-slate-500">
          <p>Printed {formatDateTime(new Date().toISOString())}</p>
        </footer>
      </main>
    </div>
  );
}

function InfoRow({ label, value }: { label: string; value: string }) {
  return (
    <div className="grid grid-cols-[130px_1fr] gap-2">
      <span className="font-medium text-slate-600">{label}</span>
      <span className="break-words"><span className="mr-2">:</span>{value}</span>
    </div>
  );
}
