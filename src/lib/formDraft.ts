/**
 * Lightweight localStorage-based draft persistence for form modals.
 * Drafts are preserved when the user navigates away temporarily and restored
 * when the form is reopened. Drafts are cleared only after:
 *   a. Data is successfully saved
 *   b. The user manually cancels/closes the form
 */

export function saveDraft<T>(key: string, data: T): void {
  try {
    localStorage.setItem(key, JSON.stringify(data));
  } catch {
    // ignore quota errors
  }
}

export function loadDraft<T>(key: string): T | null {
  try {
    const raw = localStorage.getItem(key);
    if (!raw) return null;
    return JSON.parse(raw) as T;
  } catch {
    return null;
  }
}

export function clearDraft(key: string): void {
  try {
    localStorage.removeItem(key);
  } catch {
    // ignore
  }
}
