# Hotel Lock Bridge

A local Node.js REST service that bridges the **Millennium PMS** frontend to
ZKTeco / ZKBiolock hotel lock encoder hardware running on the front-desk
computer.

The PMS (a browser app) cannot talk to native DLLs or serial ports directly.
Instead it sends HTTP requests to this bridge, which runs on `localhost:8765`
and mediates between the web app and the encoder hardware.

## Quick Start

```bash
cd bridge
npm install
npm start
```

The server listens on `http://localhost:8765`.

## Configuration

Edit `config.json`:

| Key | Description |
|-----|-------------|
| `port` | Port to listen on (default `8765`). |
| `authToken` | Shared secret token. The PMS must send this as a `Bearer` token in the `Authorization` header. **Change this before production use.** |
| `dll.searchPaths` | Folders scanned by the `/initialize` endpoint for vendor DLLs. Add or remove paths as needed. |
| `dll.dllNames` | DLL filenames to look for inside the search paths. |
| `encoder.defaultPort` | Fallback COM port if the PMS does not supply one. |
| `encoder.timeoutMs` | Max wait for an encoder operation before timing out. |

## Authentication

Every request must include an `Authorization` header:

```
Authorization: Bearer <authToken>
```

Requests without a valid token receive `401 Unauthorized`.

## CORS

CORS is enabled for all origins so the browser-based PMS can call the bridge
without proxy issues.

## Endpoints

| Method | Path | Description |
|--------|------|-------------|
| `GET`  | `/health` | Lightweight liveness check. |
| `GET`  | `/status` | Full bridge + encoder status. |
| `POST` | `/initialize` | Scan for DLLs, detect encoder, return detected paths. |
| `POST` | `/encode-card` | Encode a new guest card. |
| `POST` | `/replace-card` | Replace an existing guest card. |
| `POST` | `/invalidate-card` | Invalidate (kill) a guest card. |
| `POST` | `/extend-card` | Extend a card's validity period. |
| `POST` | `/read-card` | Read a card currently inserted in the encoder. |

### `/initialize` Response

```json
{
  "success": true,
  "detectedDllPath": "C:\\Program Files\\ZKTeco\\ZKBiolock\\zkemkeeper.dll",
  "searchedPaths": [ "C:\\Program Files\\ZKTeco\\ZKBiolock", "..." ],
  "encoderPort": "COM3",
  "message": "DLL detected at ..."
}
```

### `/encode-card` Request Body

```json
{
  "roomId": "uuid",
  "roomNumber": "101",
  "guestName": "John Doe",
  "validFrom": "2026-08-23T14:00:00Z",
  "validUntil": "2026-08-25T12:00:00Z",
  "hotelIdentifier": "HOTEL-001",
  "encodingProfile": "default",
  "encoderPort": "COM3",
  "dllPath": "C:\\Program Files\\ZKTeco\\ZKBiolock\\zkemkeeper.dll"
}
```

## Production Notes

- This bridge is intended to run on the **same machine** as the encoder
  hardware. It is not meant to be exposed to the internet.
- In a real deployment, replace the mock encoder logic in `encoder.js` with
  calls to the vendor DLL via `ffi-napi` or a native addon.
- The `/initialize` endpoint performs real filesystem scanning for DLL files
  so the PMS can auto-discover the vendor library path.
