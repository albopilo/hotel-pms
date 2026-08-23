import type { Guest } from '@/types/database';

export interface SimilarGuestMatch {
  guest: Guest;
  score: number;
  matchedFields: string[];
}

function normalize(str: string): string {
  return (str || '').toLowerCase().trim().replace(/\s+/g, ' ');
}

function levenshtein(a: string, b: string): number {
  const m = a.length;
  const n = b.length;
  if (m === 0) return n;
  if (n === 0) return m;
  const dp: number[][] = Array.from({ length: m + 1 }, () => new Array(n + 1).fill(0));
  for (let i = 0; i <= m; i++) dp[i][0] = i;
  for (let j = 0; j <= n; j++) dp[0][j] = j;
  for (let i = 1; i <= m; i++) {
    for (let j = 1; j <= n; j++) {
      const cost = a[i - 1] === b[j - 1] ? 0 : 1;
      dp[i][j] = Math.min(dp[i - 1][j] + 1, dp[i][j - 1] + 1, dp[i - 1][j - 1] + cost);
    }
  }
  return dp[m][n];
}

function similarity(a: string, b: string): number {
  const na = normalize(a);
  const nb = normalize(b);
  if (!na || !nb) return 0;
  if (na === nb) return 1;
  const maxLen = Math.max(na.length, nb.length);
  if (maxLen === 0) return 0;
  return 1 - levenshtein(na, nb) / maxLen;
}

function digitsOnly(str: string): string {
  return (str || '').replace(/\D/g, '');
}

/**
 * Weighted duplicate detection with prioritised requirements:
 *   1. Full name must be similar (hard prerequisite — if name doesn't pass the
 *      threshold the guest is never considered a duplicate).
 *   2. Phone number — strongest secondary signal.
 *   3. ID number — tertiary signal.
 *
 * The overall score is a weighted average of the three fields.  Weights are
 * only counted for fields that have data on at least one side, so a guest with
 * no phone or ID is judged primarily on name.
 *
 * Weights: name 50 %, phone 30 %, ID 20 %.
 */
export function findSimilarGuests(
  input: { full_name: string; phone: string; email: string; id_number: string },
  existingGuests: Guest[],
  threshold = 0.7,
): SimilarGuestMatch[] {
  const matches: SimilarGuestMatch[] = [];

  const NAME_WEIGHT = 0.5;
  const PHONE_WEIGHT = 0.3;
  const ID_WEIGHT = 0.2;

  for (const guest of existingGuests) {
    const matchedFields: string[] = [];

    // ── Requirement 1 (mandatory): full name must be similar ──────────────
    const nameSim = similarity(input.full_name, guest.full_name);
    if (nameSim < threshold) {
      continue; // name is the hard gate — skip entirely
    }
    matchedFields.push('name');

    // ── Requirement 2: phone number ──────────────────────────────────────
    let phoneScore = 0;
    const inputPhone = digitsOnly(input.phone);
    const guestPhone = digitsOnly(guest.phone || '');
    const hasPhoneData = inputPhone.length >= 6 || guestPhone.length >= 6;
    if (inputPhone.length >= 6 && guestPhone.length >= 6) {
      if (inputPhone === guestPhone) {
        phoneScore = 1;
        matchedFields.push('phone');
      } else if (inputPhone.includes(guestPhone) || guestPhone.includes(inputPhone)) {
        phoneScore = 0.85;
        matchedFields.push('phone');
      }
    }

    // ── Requirement 3: ID number ───────────────────────────────────────────
    let idScore = 0;
    const hasIdData = !!(input.id_number) || !!(guest.id_number);
    if (input.id_number && guest.id_number) {
      const idSim = similarity(input.id_number, guest.id_number);
      if (idSim >= threshold) {
        idScore = idSim;
        matchedFields.push('id_number');
      }
    }

    // ── Weighted score ────────────────────────────────────────────────────
    let totalWeight = NAME_WEIGHT;
    let weightedSum = nameSim * NAME_WEIGHT;

    if (hasPhoneData) {
      totalWeight += PHONE_WEIGHT;
      weightedSum += phoneScore * PHONE_WEIGHT;
    }

    if (hasIdData) {
      totalWeight += ID_WEIGHT;
      weightedSum += idScore * ID_WEIGHT;
    }

    const score = totalWeight > 0 ? weightedSum / totalWeight : 0;

    if (score >= threshold) {
      matches.push({ guest, score, matchedFields });
    }
  }

  return matches.sort((a, b) => b.score - a.score);
}

export interface DuplicatePair {
  primary: Guest;
  duplicate: Guest;
  score: number;
  matchedFields: string[];
}

export function findDuplicateGuestPairs(guests: Guest[], threshold = 0.7): DuplicatePair[] {
  const pairs: DuplicatePair[] = [];
  const seen = new Set<string>();

  for (let i = 0; i < guests.length; i++) {
    for (let j = i + 1; j < guests.length; j++) {
      const key = `${guests[i].id}|${guests[j].id}`;
      if (seen.has(key)) continue;
      seen.add(key);

      const matches = findSimilarGuests(
        {
          full_name: guests[i].full_name,
          phone: guests[i].phone || '',
          email: guests[i].email || '',
          id_number: guests[i].id_number || '',
        },
        [guests[j]],
        threshold,
      );

      if (matches.length > 0) {
        const m = matches[0];
        pairs.push({
          primary: guests[i],
          duplicate: guests[j],
          score: m.score,
          matchedFields: m.matchedFields,
        });
      }
    }
  }

  return pairs.sort((a, b) => b.score - a.score);
}
