/**
 * Encoder abstraction.
 *
 * In production this module would load the vendor DLL via ffi-napi or a
 * native addon and call real hardware functions.  For now it provides a
 * structured mock that respects the configuration so the PMS can develop
 * and test the full workflow end-to-end.
 */

let initialized = false;
let detectedDllPath = null;
let encoderPort = null;
let connected = false;

export function getEncoderState() {
  return { initialized, detectedDllPath, encoderPort, connected };
}

export async function initialize({ dllPath, port } = {}) {
  detectedDllPath = dllPath || null;
  encoderPort = port || null;
  initialized = true;
  connected = !!detectedDllPath && !!encoderPort;
  return { initialized, detectedDllPath, encoderPort, connected };
}

export async function connectEncoder() {
  if (!initialized) return false;
  // In production: open COM port via vendor DLL
  connected = true;
  return true;
}

export async function disconnectEncoder() {
  connected = false;
}

export async function encodeCard(params) {
  if (!connected) {
    return { success: false, message: 'Encoder not connected' };
  }
  // In production: call DLL encode function with params
  return {
    success: true,
    message: `Card encoded for Room ${params.roomNumber}, Guest ${params.guestName}`,
  };
}

export async function replaceCard(params) {
  if (!connected) {
    return { success: false, message: 'Encoder not connected' };
  }
  return {
    success: true,
    message: `Card replaced (#${params.cardSequence}) for Room ${params.roomNumber}`,
  };
}

export async function invalidateCard(params) {
  if (!connected) {
    return { success: false, message: 'Encoder not connected' };
  }
  return {
    success: true,
    message: `Card invalidated (ID: ${params.cardId})`,
  };
}

export async function extendCard(params) {
  if (!connected) {
    return { success: false, message: 'Encoder not connected' };
  }
  return {
    success: true,
    message: `Card extended until ${params.newValidUntil} (ID: ${params.cardId})`,
  };
}

export async function readCard() {
  if (!connected) {
    return { success: false, message: 'Encoder not connected', data: null };
  }
  // In production: read card data from encoder
  return {
    success: true,
    message: 'No card detected',
    data: null,
  };
}

export async function getStatus() {
  return {
    initialized,
    detectedDllPath,
    encoderPort,
    connected,
  };
}
