export interface HotelLockProvider {
  connect(): Promise<boolean>;
  getStatus(): Promise<{ connected: boolean; encoderConnected: boolean }>;
  encodeGuestCard(params: {
    roomId: string;
    roomNumber: string;
    guestName: string;
    validFrom: string;
    validUntil: string;
  }): Promise<{ success: boolean; message: string }>;
  replaceGuestCard(params: {
    roomId: string;
    roomNumber: string;
    guestName: string;
    validFrom: string;
    validUntil: string;
    cardSequence: number;
  }): Promise<{ success: boolean; message: string }>;
  invalidateGuestCard(params: { cardId: string }): Promise<{ success: boolean; message: string }>;
  extendGuestCard(params: {
    cardId: string;
    newValidUntil: string;
  }): Promise<{ success: boolean; message: string }>;
  readEncoderStatus(): Promise<{ connected: boolean; status: string }>;
  getLockEvents(): Promise<LockEvent[]>;
  disconnect(): Promise<void>;
}

export interface LockEvent {
  event_type: string;
  status: 'info' | 'success' | 'warning' | 'error';
  message: string;
  timestamp: string;
  data?: Record<string, unknown>;
}

export class MockHotelLockProvider implements HotelLockProvider {
  private connected = false;
  private encoderConnected = false;
  private events: LockEvent[] = [];
  private shouldFail = false;

  constructor() {
    this.events.push({
      event_type: 'init',
      status: 'info',
      message: 'Mock hotel lock provider initialized (DEVELOPMENT / MOCK MODE)',
      timestamp: new Date().toISOString(),
    });
  }

  async connect(): Promise<boolean> {
    await delay(300);
    this.connected = true;
    this.encoderConnected = true;
    this.logEvent('connect', 'success', 'Mock bridge connected');
    return true;
  }

  async getStatus(): Promise<{ connected: boolean; encoderConnected: boolean }> {
    return { connected: this.connected, encoderConnected: this.encoderConnected };
  }

  async encodeGuestCard(params: {
    roomId: string;
    roomNumber: string;
    guestName: string;
    validFrom: string;
    validUntil: string;
  }): Promise<{ success: boolean; message: string }> {
    if (!this.connected || !this.encoderConnected) {
      this.logEvent('encode_card', 'error', `Card encoding failed: bridge not connected (Room ${params.roomNumber})`);
      return { success: false, message: 'Hotel lock system unavailable' };
    }
    await delay(500);
    if (this.shouldFail) {
      this.logEvent('encode_card', 'error', `Card encoding failed: simulated error (Room ${params.roomNumber})`);
      return { success: false, message: 'Card encoding failed' };
    }
    this.logEvent('encode_card', 'success', `Card encoded successfully for Room ${params.roomNumber}, Guest: ${params.guestName}`);
    return { success: true, message: 'Card successfully encoded' };
  }

  async replaceGuestCard(params: {
    roomId: string;
    roomNumber: string;
    guestName: string;
    validFrom: string;
    validUntil: string;
    cardSequence: number;
  }): Promise<{ success: boolean; message: string }> {
    if (!this.connected || !this.encoderConnected) {
      return { success: false, message: 'Hotel lock system unavailable' };
    }
    await delay(500);
    this.logEvent('replace_card', 'success', `Card replaced (#${params.cardSequence}) for Room ${params.roomNumber}, Guest: ${params.guestName}`);
    return { success: true, message: 'Card successfully replaced' };
  }

  async invalidateGuestCard(params: { cardId: string }): Promise<{ success: boolean; message: string }> {
    if (!this.connected) {
      return { success: false, message: 'Hotel lock system unavailable' };
    }
    await delay(300);
    this.logEvent('invalidate_card', 'success', `Card invalidated (ID: ${params.cardId})`);
    return { success: true, message: 'Card invalidated successfully' };
  }

  async extendGuestCard(params: { cardId: string; newValidUntil: string }): Promise<{ success: boolean; message: string }> {
    if (!this.connected) {
      return { success: false, message: 'Hotel lock system unavailable' };
    }
    await delay(300);
    this.logEvent('extend_card', 'success', `Card extended until ${params.newValidUntil} (ID: ${params.cardId})`);
    return { success: true, message: 'Card validity extended successfully' };
  }

  async readEncoderStatus(): Promise<{ connected: boolean; status: string }> {
    return { connected: this.encoderConnected, status: this.encoderConnected ? 'ready' : 'disconnected' };
  }

  async getLockEvents(): Promise<LockEvent[]> {
    return [...this.events].reverse();
  }

  async disconnect(): Promise<void> {
    this.connected = false;
    this.encoderConnected = false;
    this.logEvent('disconnect', 'info', 'Mock bridge disconnected');
  }

  setShouldFail(fail: boolean) {
    this.shouldFail = fail;
  }

  private logEvent(event_type: string, status: LockEvent['status'], message: string) {
    this.events.push({ event_type, status, message, timestamp: new Date().toISOString() });
  }
}

function delay(ms: number): Promise<void> {
  return new Promise((resolve) => setTimeout(resolve, ms));
}

let providerInstance: HotelLockProvider | null = null;

export function getLockProvider(): HotelLockProvider {
  if (!providerInstance) {
    providerInstance = new MockHotelLockProvider();
  }
  return providerInstance;
}
