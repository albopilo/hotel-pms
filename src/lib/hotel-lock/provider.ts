import type { HotelLockIntegration } from '@/types/database';

export interface HotelLockProviderConfig {
  bridgeUrl: string | null;
  bridgeToken: string | null;
  encoderPort: string | null;
  dllPath: string | null;
  hotelIdentifier: string | null;
  encodingProfile: string | null;
  autoPollEnabled: boolean;
  providerType: 'mock' | 'production';
}

export interface EncoderStatusResult {
  connected: boolean;
  status: string;
  dllPath?: string | null;
  encoderPort?: string | null;
}

export interface InitializeResult {
  success: boolean;
  detectedDllPath?: string | null;
  encoderPort?: string | null;
  message: string;
}

export interface HotelLockProvider {
  configure(config: HotelLockProviderConfig): void;
  getConfig(): HotelLockProviderConfig;
  connect(): Promise<boolean>;
  getStatus(): Promise<{ connected: boolean; encoderConnected: boolean }>;
  initialize(): Promise<InitializeResult>;
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
  readEncoderStatus(): Promise<EncoderStatusResult>;
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

const DEFAULT_CONFIG: HotelLockProviderConfig = {
  bridgeUrl: null,
  bridgeToken: null,
  encoderPort: null,
  dllPath: null,
  hotelIdentifier: null,
  encodingProfile: null,
  autoPollEnabled: false,
  providerType: 'mock',
};

export function integrationToConfig(integ: HotelLockIntegration | null): HotelLockProviderConfig {
  if (!integ) return { ...DEFAULT_CONFIG };
  return {
    bridgeUrl: integ.bridge_url,
    bridgeToken: integ.bridge_token,
    encoderPort: integ.encoder_port,
    dllPath: integ.dll_path,
    hotelIdentifier: integ.hotel_identifier,
    encodingProfile: integ.encoding_profile,
    autoPollEnabled: integ.auto_poll_enabled,
    providerType: integ.provider_type,
  };
}

function delay(ms: number): Promise<void> {
  return new Promise((resolve) => setTimeout(resolve, ms));
}

// ── Mock Provider ────────────────────────────────────────────────────────────

export class MockHotelLockProvider implements HotelLockProvider {
  private connected = false;
  private encoderConnected = false;
  private events: LockEvent[] = [];
  private shouldFail = false;
  private config: HotelLockProviderConfig = { ...DEFAULT_CONFIG };

  constructor() {
    this.events.push({
      event_type: 'init',
      status: 'info',
      message: 'Mock hotel lock provider initialized (DEVELOPMENT / MOCK MODE)',
      timestamp: new Date().toISOString(),
    });
  }

  configure(config: HotelLockProviderConfig): void {
    this.config = { ...config };
    this.logEvent('configure', 'info', `Provider configured (type=${config.providerType}, port=${config.encoderPort || '-'}, hotel=${config.hotelIdentifier || '-'})`);
  }

  getConfig(): HotelLockProviderConfig {
    return { ...this.config };
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

  async initialize(): Promise<InitializeResult> {
    await delay(400);
    this.connected = true;
    this.encoderConnected = true;
    this.logEvent('initialize', 'success', 'Mock initialization complete');
    return {
      success: true,
      detectedDllPath: this.config.dllPath || 'mock://zkemkeeper.dll',
      encoderPort: this.config.encoderPort || 'COM3',
      message: 'Mock initialization complete',
    };
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

  async readEncoderStatus(): Promise<EncoderStatusResult> {
    return {
      connected: this.encoderConnected,
      status: this.encoderConnected ? 'ready' : 'disconnected',
      dllPath: this.config.dllPath,
      encoderPort: this.config.encoderPort,
    };
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

// ── Local Bridge (Production) Provider ───────────────────────────────────────

export class LocalBridgeHotelLockProvider implements HotelLockProvider {
  private connected = false;
  private encoderConnected = false;
  private events: LockEvent[] = [];
  private config: HotelLockProviderConfig = { ...DEFAULT_CONFIG };
  private detectedDllPath: string | null = null;

  constructor() {
    this.events.push({
      event_type: 'init',
      status: 'info',
      message: 'Local bridge hotel lock provider initialized (PRODUCTION MODE)',
      timestamp: new Date().toISOString(),
    });
  }

  configure(config: HotelLockProviderConfig): void {
    this.config = { ...config };
    this.logEvent('configure', 'info', `Provider configured (bridge=${config.bridgeUrl || '-'}, port=${config.encoderPort || '-'}, hotel=${config.hotelIdentifier || '-'})`);
  }

  getConfig(): HotelLockProviderConfig {
    return { ...this.config };
  }

  private getBaseUrl(): string {
    const url = this.config.bridgeUrl || 'http://localhost:8765';
    return url.replace(/\/+$/, '');
  }

  private getHeaders(): Record<string, string> {
    const headers: Record<string, string> = { 'Content-Type': 'application/json' };
    if (this.config.bridgeToken) {
      headers['Authorization'] = `Bearer ${this.config.bridgeToken}`;
    }
    return headers;
  }

  async connect(): Promise<boolean> {
    try {
      const res = await fetch(`${this.getBaseUrl()}/health`, {
        headers: this.getHeaders(),
        signal: AbortSignal.timeout(5000),
      });
      if (!res.ok) {
        this.connected = false;
        this.logEvent('connect', 'error', `Bridge health check failed: HTTP ${res.status}`);
        return false;
      }
      this.connected = true;
      this.logEvent('connect', 'success', `Bridge connected at ${this.getBaseUrl()}`);
      return true;
    } catch (err) {
      this.connected = false;
      this.encoderConnected = false;
      const msg = err instanceof Error ? err.message : 'Unknown error';
      this.logEvent('connect', 'error', `Bridge offline: ${msg}`);
      return false;
    }
  }

  async getStatus(): Promise<{ connected: boolean; encoderConnected: boolean }> {
    return { connected: this.connected, encoderConnected: this.encoderConnected };
  }

  async initialize(): Promise<InitializeResult> {
    try {
      const res = await fetch(`${this.getBaseUrl()}/initialize`, {
        method: 'POST',
        headers: this.getHeaders(),
        body: JSON.stringify({
          dllSearchPaths: undefined,
          dllNames: undefined,
          encoderPort: this.config.encoderPort || undefined,
          dllPath: this.config.dllPath || undefined,
        }),
        signal: AbortSignal.timeout(10000),
      });

      if (!res.ok) {
        const text = await res.text().catch(() => '');
        this.logEvent('initialize', 'error', `Initialization failed: HTTP ${res.status} ${text}`);
        return { success: false, message: `Initialization failed: HTTP ${res.status}` };
      }

      const data = await res.json();
      if (data.success) {
        this.connected = true;
        this.detectedDllPath = data.detectedDllPath || this.config.dllPath || null;
        if (data.encoderPort) {
          this.config = { ...this.config, encoderPort: data.encoderPort };
        }
        this.logEvent('initialize', 'success', data.message || 'Initialization successful');
        return {
          success: true,
          detectedDllPath: data.detectedDllPath || null,
          encoderPort: data.encoderPort || this.config.encoderPort,
          message: data.message || 'Initialization successful',
        };
      } else {
        this.logEvent('initialize', 'error', data.message || 'Initialization failed');
        return { success: false, message: data.message || 'Initialization failed' };
      }
    } catch (err) {
      const msg = err instanceof Error ? err.message : 'Unknown error';
      this.logEvent('initialize', 'error', `Initialization failed: ${msg}`);
      return { success: false, message: `Initialization failed: ${msg}` };
    }
  }

  async encodeGuestCard(params: {
    roomId: string;
    roomNumber: string;
    guestName: string;
    validFrom: string;
    validUntil: string;
  }): Promise<{ success: boolean; message: string }> {
    if (!this.connected) {
      const ok = await this.connect();
      if (!ok) return { success: false, message: 'Bridge offline — cannot encode card' };
    }

    try {
      const res = await fetch(`${this.getBaseUrl()}/encode-card`, {
        method: 'POST',
        headers: this.getHeaders(),
        body: JSON.stringify({
          roomId: params.roomId,
          roomNumber: params.roomNumber,
          guestName: params.guestName,
          validFrom: params.validFrom,
          validUntil: params.validUntil,
          hotelIdentifier: this.config.hotelIdentifier || undefined,
          encodingProfile: this.config.encodingProfile || undefined,
          encoderPort: this.config.encoderPort || undefined,
          dllPath: this.detectedDllPath || this.config.dllPath || undefined,
        }),
        signal: AbortSignal.timeout(15000),
      });

      const data = await res.json().catch(() => ({}));
      if (data.success) {
        this.encoderConnected = true;
        this.logEvent('encode_card', 'success', data.message || `Card encoded for Room ${params.roomNumber}`);
        return { success: true, message: data.message || 'Card successfully encoded' };
      } else {
        this.encoderConnected = false;
        this.logEvent('encode_card', 'error', data.message || data.error || 'Card encoding failed');
        return { success: false, message: data.message || data.error || 'Card encoding failed' };
      }
    } catch (err) {
      const msg = err instanceof Error ? err.message : 'Unknown error';
      this.logEvent('encode_card', 'error', `Card encoding failed: ${msg}`);
      return { success: false, message: `Card encoding failed: ${msg}` };
    }
  }

  async replaceGuestCard(params: {
    roomId: string;
    roomNumber: string;
    guestName: string;
    validFrom: string;
    validUntil: string;
    cardSequence: number;
  }): Promise<{ success: boolean; message: string }> {
    if (!this.connected) {
      const ok = await this.connect();
      if (!ok) return { success: false, message: 'Bridge offline — cannot replace card' };
    }

    try {
      const res = await fetch(`${this.getBaseUrl()}/replace-card`, {
        method: 'POST',
        headers: this.getHeaders(),
        body: JSON.stringify({
          roomId: params.roomId,
          roomNumber: params.roomNumber,
          guestName: params.guestName,
          validFrom: params.validFrom,
          validUntil: params.validUntil,
          cardSequence: params.cardSequence,
          hotelIdentifier: this.config.hotelIdentifier || undefined,
          encodingProfile: this.config.encodingProfile || undefined,
          encoderPort: this.config.encoderPort || undefined,
          dllPath: this.detectedDllPath || this.config.dllPath || undefined,
        }),
        signal: AbortSignal.timeout(15000),
      });

      const data = await res.json().catch(() => ({}));
      if (data.success) {
        this.logEvent('replace_card', 'success', data.message || 'Card replaced');
        return { success: true, message: data.message || 'Card successfully replaced' };
      } else {
        this.logEvent('replace_card', 'error', data.message || data.error || 'Card replacement failed');
        return { success: false, message: data.message || data.error || 'Card replacement failed' };
      }
    } catch (err) {
      const msg = err instanceof Error ? err.message : 'Unknown error';
      this.logEvent('replace_card', 'error', `Card replacement failed: ${msg}`);
      return { success: false, message: `Card replacement failed: ${msg}` };
    }
  }

  async invalidateGuestCard(params: { cardId: string }): Promise<{ success: boolean; message: string }> {
    if (!this.connected) {
      const ok = await this.connect();
      if (!ok) return { success: false, message: 'Bridge offline — cannot invalidate card' };
    }

    try {
      const res = await fetch(`${this.getBaseUrl()}/invalidate-card`, {
        method: 'POST',
        headers: this.getHeaders(),
        body: JSON.stringify({ cardId: params.cardId }),
        signal: AbortSignal.timeout(10000),
      });

      const data = await res.json().catch(() => ({}));
      if (data.success) {
        this.logEvent('invalidate_card', 'success', data.message || 'Card invalidated');
        return { success: true, message: data.message || 'Card invalidated successfully' };
      } else {
        this.logEvent('invalidate_card', 'error', data.message || data.error || 'Card invalidation failed');
        return { success: false, message: data.message || data.error || 'Card invalidation failed' };
      }
    } catch (err) {
      const msg = err instanceof Error ? err.message : 'Unknown error';
      this.logEvent('invalidate_card', 'error', `Card invalidation failed: ${msg}`);
      return { success: false, message: `Card invalidation failed: ${msg}` };
    }
  }

  async extendGuestCard(params: { cardId: string; newValidUntil: string }): Promise<{ success: boolean; message: string }> {
    if (!this.connected) {
      const ok = await this.connect();
      if (!ok) return { success: false, message: 'Bridge offline — cannot extend card' };
    }

    try {
      const res = await fetch(`${this.getBaseUrl()}/extend-card`, {
        method: 'POST',
        headers: this.getHeaders(),
        body: JSON.stringify({ cardId: params.cardId, newValidUntil: params.newValidUntil }),
        signal: AbortSignal.timeout(10000),
      });

      const data = await res.json().catch(() => ({}));
      if (data.success) {
        this.logEvent('extend_card', 'success', data.message || 'Card extended');
        return { success: true, message: data.message || 'Card validity extended successfully' };
      } else {
        this.logEvent('extend_card', 'error', data.message || data.error || 'Card extension failed');
        return { success: false, message: data.message || data.error || 'Card extension failed' };
      }
    } catch (err) {
      const msg = err instanceof Error ? err.message : 'Unknown error';
      this.logEvent('extend_card', 'error', `Card extension failed: ${msg}`);
      return { success: false, message: `Card extension failed: ${msg}` };
    }
  }

  async readEncoderStatus(): Promise<EncoderStatusResult> {
    try {
      const res = await fetch(`${this.getBaseUrl()}/status`, {
        headers: this.getHeaders(),
        signal: AbortSignal.timeout(5000),
      });

      if (!res.ok) {
        this.encoderConnected = false;
        this.logEvent('encoder_status', 'error', `Encoder status check failed: HTTP ${res.status}`);
        return { connected: false, status: 'unavailable' };
      }

      const data = await res.json();
      const encConnected = data.encoder?.connected ?? data.connected ?? false;
      this.encoderConnected = encConnected;
      this.logEvent('encoder_status', encConnected ? 'success' : 'warning', `Encoder: ${encConnected ? 'connected' : 'disconnected'}`);
      return {
        connected: encConnected,
        status: encConnected ? 'ready' : 'disconnected',
        dllPath: data.encoder?.detectedDllPath || data.detectedDllPath || this.detectedDllPath,
        encoderPort: data.encoder?.encoderPort || data.encoderPort || this.config.encoderPort,
      };
    } catch (err) {
      this.encoderConnected = false;
      const msg = err instanceof Error ? err.message : 'Unknown error';
      this.logEvent('encoder_status', 'error', `Encoder unavailable: ${msg}`);
      return { connected: false, status: 'unavailable' };
    }
  }

  async getLockEvents(): Promise<LockEvent[]> {
    return [...this.events].reverse();
  }

  async disconnect(): Promise<void> {
    this.connected = false;
    this.encoderConnected = false;
    this.logEvent('disconnect', 'info', 'Bridge disconnected');
  }

  private logEvent(event_type: string, status: LockEvent['status'], message: string) {
    this.events.push({ event_type, status, message, timestamp: new Date().toISOString() });
  }
}

// ── Provider Factory ─────────────────────────────────────────────────────────

let mockProvider: MockHotelLockProvider | null = null;
let bridgeProvider: LocalBridgeHotelLockProvider | null = null;
let currentProvider: HotelLockProvider | null = null;
let currentProviderType: 'mock' | 'production' | null = null;

export function getLockProvider(): HotelLockProvider {
  if (!mockProvider) mockProvider = new MockHotelLockProvider();
  if (!bridgeProvider) bridgeProvider = new LocalBridgeHotelLockProvider();
  return currentProvider || mockProvider;
}

export function getLockProviderByType(type: 'mock' | 'production'): HotelLockProvider {
  if (!mockProvider) mockProvider = new MockHotelLockProvider();
  if (!bridgeProvider) bridgeProvider = new LocalBridgeHotelLockProvider();

  if (type === 'production') {
    if (currentProviderType !== 'production') {
      currentProvider = bridgeProvider;
      currentProviderType = 'production';
    }
  } else {
    if (currentProviderType !== 'mock') {
      currentProvider = mockProvider;
      currentProviderType = 'mock';
    }
  }
  return currentProvider || mockProvider;
}
