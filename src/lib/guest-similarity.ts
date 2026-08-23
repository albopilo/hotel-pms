import type { Guest } from '@/types/database';

export interface SimilarGuestMatch {
  guest: Guest;
  score: number;
  matchedFields: string[];
}

export interface DuplicatePair {
  primary: Guest;
  duplicate: Guest;
  score: number;
  matchedFields: string[];
}

type GuestLike = Pick<Guest, 'full_name' | 'phone' | 'email' | 'id_number' | 'address'>;

function normalize(s: string | null | undefined): string {
  return (s || '').trim().toLowerCase().replace(/\s+/g, ' ');
}

function levenshtein(a: string, b: string): number {
  if (a === b) return 0;
  if (!a.length) return b.length;
  if (!b.length) return a.length;
  const prev = new Array(b.length + 1);
  const curr = new Array(b.length + 1);
  for (let j = 0; j <= b.length; j++) prev[j] = j;
  for (let i = 1; i <= a.length; i++) {
    curr[0] = i;
    for (let j = 1; j <= b.length; j++) {
      const cost = a[i - 1] === b[j - 1] ? 0 : 1;
      curr[j] = Math.min(prev[j] + 1, curr[j - 1] + 1, prev[j - 1] + cost);
    }
    for (let j = 0; j <= b.length; j++) prev[j] = curr[j];
  }
  return prev[b.length];
}

function stringSimilarity(a: string, b: string): number {
  const na = normalize(a);
  const nb = normalize(b);
  if (!na && !nb) return 1;
  if (!na || !nb) return 0;
  if (na === nb) return 1;
  const maxLen = Math.max(na.length, nb.length);
  if (maxLen === 0) return 1;
  return 1 - levenshtein(na, nb) / maxLen;
}

function exactMatch(a: string | null | undefined, b: string | null | undefined): boolean {
  const na = normalize(a);
  const nb = normalize(b);
  return na.length > 0 && na === nb;
}

const FIELD_WEIGHTS = {
  full_name: 0.4,
  phone: 0.25,
  email: 0.2,
  id_number: 0.15,
} as const;

function computeScore(input: GuestLike, candidate: Guest): { score: number; matchedFields: string[] } {
  const matchedFields: string[] = [];
  let score = 0;

  const nameSim = stringSimilarity(input.full_name, candidate.full_name);
  score += nameSim * FIELD_WEIGHTS.full_name;
  if (nameSim >= 0.8) matchedFields.push('name');

  const phoneSim = stringSimilarity(input.phone, candidate.phone);
  score += phoneSim * FIELD_WEIGHTS.phone;
  if (exactMatch(input.phone, candidate.phone)) matchedFields.push('phone');

  const emailSim = stringSimilarity(input.email, candidate.email);
  score += emailSim * FIELD_WEIGHTS.email;
  if (exactMatch(input.email, candidate.email)) matchedFields.push('email');

  const idSim = stringSimilarity(input.id_number, candidate.id_number);
  score += idSim * FIELD_WEIGHTS.id_number;
  if (exactMatch(input.id_number, candidate.id_number)) matchedFields.push('ID');

  return { score, matchedFields };
}

export function findSimilarGuests(
  input: GuestLike,
  allGuests: Guest[],
  threshold = 0.5,
): SimilarGuestMatch[] {
  const matches: SimilarGuestMatch[] = [];
  for (const guest of allGuests) {
    const { score, matchedFields } = computeScore(input, guest);
    if (score >= threshold && matchedFields.length > 0) {
      matches.push({ guest, score, matchedFields });
    }
  }
  matches.sort((a, b) => b.score - a.score);
  return matches;
}

export function findDuplicateGuestPairs(
  guests: Guest[],
  threshold = 0.7,
): DuplicatePair[] {
  const pairs: DuplicatePair[] = [];
  const seen = new Set<string>();

  for (let i = 0; i < guests.length; i++) {
    for (let j = i + 1; j < guests.length; j++) {
      const a = guests[i];
      const b = guests[j];
      const key = `${a.id}|${b.id}`;
      if (seen.has(key)) continue;

      const { score, matchedFields } = computeScore(a, b);
      if (score >= threshold) {
        seen.add(key);
        const primary = a.created_at <= b.created_at ? a : b;
        const duplicate = a.created_at <= b.created_at ? b : a;
        pairs.push({ primary, duplicate, score, matchedFields });
      }
    }
  }

  pairs.sort((a, b) => b.score - a.score);
  return pairs;
}
