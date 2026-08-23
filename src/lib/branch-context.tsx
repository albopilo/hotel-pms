import { createContext, useContext, useState, useCallback, type ReactNode } from 'react';
import type { Branch } from '@/types/database';

const BRANCH_STORAGE_KEY = 'selected_branch_id';

interface BranchContextValue {
  selectedBranchId: string | null;
  selectedBranch: Branch | null;
  setSelectedBranchId: (id: string | null) => void;
}

const BranchContext = createContext<BranchContextValue | null>(null);

export function BranchProvider({ children, branches }: { children: ReactNode; branches: Branch[] }) {
  const [selectedBranchId, setSelectedBranchIdState] = useState<string | null>(() => {
    try {
      const stored = localStorage.getItem(BRANCH_STORAGE_KEY);
      if (stored && branches.some((b) => b.id === stored)) return stored;
    } catch { /* ignore */ }
    return null;
  });

  const setSelectedBranchId = useCallback((id: string | null) => {
    setSelectedBranchIdState(id);
    try {
      if (id) localStorage.setItem(BRANCH_STORAGE_KEY, id);
      else localStorage.removeItem(BRANCH_STORAGE_KEY);
    } catch { /* ignore */ }
  }, []);

  const selectedBranch = branches.find((b) => b.id === selectedBranchId) || null;

  return (
    <BranchContext.Provider value={{ selectedBranchId, selectedBranch, setSelectedBranchId }}>
      {children}
    </BranchContext.Provider>
  );
}

export function useBranch() {
  const ctx = useContext(BranchContext);
  if (!ctx) throw new Error('useBranch must be used within BranchProvider');
  return ctx;
}
