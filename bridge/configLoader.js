import fs from 'node:fs';
import path from 'node:path';
import { fileURLToPath } from 'node:url';

const __dirname = path.dirname(fileURLToPath(import.meta.url));

export function loadConfig() {
  const configPath = path.join(__dirname, 'config.json');
  const raw = fs.readFileSync(configPath, 'utf-8');
  return JSON.parse(raw);
}

export function saveConfig(updates) {
  const configPath = path.join(__dirname, 'config.json');
  const current = loadConfig();
  const merged = { ...current, ...updates };
  fs.writeFileSync(configPath, JSON.stringify(merged, null, 2) + '\n', 'utf-8');
  return merged;
}
