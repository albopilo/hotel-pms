import express from 'express';
import cors from 'cors';
import { loadConfig } from './configLoader.js';
import { scanForDlls } from './dllScanner.js';
import * as encoder from './encoder.js';

const config = loadConfig();
const app = express();

app.use(cors());
app.use(express.json());

// ── Token Authentication Middleware ──────────────────────────────────────────

function authMiddleware(req, res, next) {
  // Health endpoint is always open
  if (req.path === '/health') return next();

  const authHeader = req.headers.authorization;
  if (!authHeader || !authHeader.startsWith('Bearer ')) {
    return res.status(401).json({ error: 'Missing or invalid Authorization header' });
  }

  const token = authHeader.slice(7);
  if (token !== config.authToken) {
    return res.status(401).json({ error: 'Invalid token' });
  }

  next();
}

app.use(authMiddleware);

// ── Endpoints ────────────────────────────────────────────────────────────────

// GET /health — lightweight liveness check (no auth required)
app.get('/health', (req, res) => {
  res.json({ status: 'ok', timestamp: new Date().toISOString() });
});

// GET /status — full bridge + encoder status
app.get('/status', async (req, res) => {
  const encState = await encoder.getStatus();
  res.json({
    bridge: 'running',
    port: config.port,
    ...encState,
  });
});

// POST /initialize — scan for DLLs, detect encoder, return detected paths
app.post('/initialize', async (req, res) => {
  try {
    const { dllSearchPaths, dllNames, encoderPort } = req.body || {};
    const searchPaths = dllSearchPaths || config.dll.searchPaths;
    const names = dllNames || config.dll.dllNames;
    const port = encoderPort || config.encoder.defaultPort;

    const scanResult = scanForDlls(searchPaths, names);

    await encoder.initialize({
      dllPath: scanResult.detectedDllPath,
      port,
    });

    res.json({
      success: true,
      detectedDllPath: scanResult.detectedDllPath,
      searchedPaths: scanResult.searchedPaths,
      found: scanResult.found,
      encoderPort: port,
      message: scanResult.detectedDllPath
        ? `DLL detected at ${scanResult.detectedDllPath}`
        : 'No DLL found in search paths. Please specify a DLL path manually.',
    });
  } catch (err) {
    res.status(500).json({ success: false, error: err.message });
  }
});

// POST /encode-card — encode a new guest card
app.post('/encode-card', async (req, res) => {
  try {
    const {
      roomId,
      roomNumber,
      guestName,
      validFrom,
      validUntil,
      hotelIdentifier,
      encodingProfile,
      encoderPort,
      dllPath,
    } = req.body || {};

    if (!roomNumber || !guestName) {
      return res.status(400).json({ success: false, error: 'roomNumber and guestName are required' });
    }

    // Re-initialize if new DLL/port info is provided
    if (dllPath || encoderPort) {
      await encoder.initialize({ dllPath, port: encoderPort });
    }

    await encoder.connectEncoder();
    const result = await encoder.encodeCard({
      roomId,
      roomNumber,
      guestName,
      validFrom,
      validUntil,
      hotelIdentifier,
      encodingProfile,
    });

    res.json(result);
  } catch (err) {
    res.status(500).json({ success: false, error: err.message });
  }
});

// POST /replace-card — replace an existing guest card
app.post('/replace-card', async (req, res) => {
  try {
    const {
      roomId,
      roomNumber,
      guestName,
      validFrom,
      validUntil,
      cardSequence,
      hotelIdentifier,
      encodingProfile,
      encoderPort,
      dllPath,
    } = req.body || {};

    if (!roomNumber || !guestName) {
      return res.status(400).json({ success: false, error: 'roomNumber and guestName are required' });
    }

    if (dllPath || encoderPort) {
      await encoder.initialize({ dllPath, port: encoderPort });
    }

    await encoder.connectEncoder();
    const result = await encoder.replaceCard({
      roomId,
      roomNumber,
      guestName,
      validFrom,
      validUntil,
      cardSequence: cardSequence || 1,
      hotelIdentifier,
      encodingProfile,
    });

    res.json(result);
  } catch (err) {
    res.status(500).json({ success: false, error: err.message });
  }
});

// POST /invalidate-card — invalidate (kill) a guest card
app.post('/invalidate-card', async (req, res) => {
  try {
    const { cardId, encoderPort, dllPath } = req.body || {};

    if (!cardId) {
      return res.status(400).json({ success: false, error: 'cardId is required' });
    }

    if (dllPath || encoderPort) {
      await encoder.initialize({ dllPath, port: encoderPort });
    }

    await encoder.connectEncoder();
    const result = await encoder.invalidateCard({ cardId });

    res.json(result);
  } catch (err) {
    res.status(500).json({ success: false, error: err.message });
  }
});

// POST /extend-card — extend a card's validity period
app.post('/extend-card', async (req, res) => {
  try {
    const { cardId, newValidUntil, encoderPort, dllPath } = req.body || {};

    if (!cardId || !newValidUntil) {
      return res.status(400).json({ success: false, error: 'cardId and newValidUntil are required' });
    }

    if (dllPath || encoderPort) {
      await encoder.initialize({ dllPath, port: encoderPort });
    }

    await encoder.connectEncoder();
    const result = await encoder.extendCard({ cardId, newValidUntil });

    res.json(result);
  } catch (err) {
    res.status(500).json({ success: false, error: err.message });
  }
});

// POST /read-card — read a card currently inserted in the encoder
app.post('/read-card', async (req, res) => {
  try {
    const { encoderPort, dllPath } = req.body || {};

    if (dllPath || encoderPort) {
      await encoder.initialize({ dllPath, port: encoderPort });
    }

    await encoder.connectEncoder();
    const result = await encoder.readCard();

    res.json(result);
  } catch (err) {
    res.status(500).json({ success: false, error: err.message });
  }
});

// ── Start Server ─────────────────────────────────────────────────────────────

app.listen(config.port, () => {
  console.log(`Hotel Lock Bridge running on http://localhost:${config.port}`);
  console.log(`Auth token: ${config.authToken ? 'configured' : 'NOT SET — please edit config.json'}`);
});
