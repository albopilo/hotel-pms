import { supabase } from './supabase';

export interface AppError {
  message: string;
  code: string;
  recoverable: boolean;
}

export function parseDbError(error: { message?: string; code?: string }): AppError {
  const msg = error.message || 'An unexpected error occurred';

  if (msg.includes('row-level security') || msg.includes('permission') || msg.includes('JWT')) {
    return {
      message: 'You do not have permission to perform this action. Your session may have expired — try signing in again.',
      code: 'permission_denied',
      recoverable: false,
    };
  }

  if (msg.includes('reservations_no_overlap') || msg.includes('exclusion constraint')) {
    return {
      message: 'This room is already booked for the selected dates. Please choose a different room.',
      code: 'double_booked',
      recoverable: true,
    };
  }

  if (msg.includes('duplicate key') || msg.includes('unique constraint')) {
    return {
      message: 'A record with this information already exists. Please check for duplicates.',
      code: 'duplicate',
      recoverable: true,
    };
  }

  if (msg.includes('network') || msg.includes('fetch') || msg.includes('Failed to fetch')) {
    return {
      message: 'Network error — could not reach the server. Please check your connection and try again.',
      code: 'network',
      recoverable: true,
    };
  }

  if (msg.includes('timeout') || msg.includes('Timeout')) {
    return {
      message: 'The request timed out. Please try again.',
      code: 'timeout',
      recoverable: true,
    };
  }

  return { message: msg, code: error.code || 'unknown', recoverable: false };
}

export async function checkSession(): Promise<boolean> {
  const { data: { session } } = await supabase.auth.getSession();
  return !!session;
}

export function isSessionError(error: { message?: string }): boolean {
  const msg = error.message || '';
  return msg.includes('JWT') || msg.includes('session') || msg.includes('token');
}
