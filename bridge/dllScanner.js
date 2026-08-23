import fs from 'node:fs';
import path from 'node:path';

/**
 * Scans the configured search paths for known ZKTeco / ZKBiolock DLL files.
 *
 * @param {string[]} searchPaths - Directories to scan.
 * @param {string[]} dllNames - DLL filenames to look for.
 * @returns {{ detectedDllPath: string|null, searchedPaths: string[], found: {path:string,dll:string}[] }}
 */
export function scanForDlls(searchPaths = [], dllNames = []) {
  const found = [];
  const searchedPaths = [];

  for (const dir of searchPaths) {
    searchedPaths.push(dir);
    if (!fs.existsSync(dir)) continue;

    // Check the directory itself
    for (const dll of dllNames) {
      const fullPath = path.join(dir, dll);
      if (fs.existsSync(fullPath)) {
        found.push({ path: fullPath, dll });
      }
    }

    // Scan one level of subdirectories (common for versioned install folders)
    let entries = [];
    try {
      entries = fs.readdirSync(dir, { withFileTypes: true });
    } catch {
      continue;
    }

    for (const entry of entries) {
      if (!entry.isDirectory()) continue;
      const subDir = path.join(dir, entry.name);
      for (const dll of dllNames) {
        const fullPath = path.join(subDir, dll);
        try {
          if (fs.existsSync(fullPath)) {
            found.push({ path: fullPath, dll });
          }
        } catch {
          // ignore permission errors
        }
      }
    }
  }

  const detectedDllPath = found.length > 0 ? found[0].path : null;

  return { detectedDllPath, searchedPaths, found };
}
