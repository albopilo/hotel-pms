import { createContext, useContext, useState, useCallback, type ReactNode } from 'react';
import type { Branch } from '@/types/database';

interface BranchContextValue {
  selectedBranchId: string | null;
  selectedBranch: Branch | null;
  setSelectedBranchId: (id: string | null) => void;
}

const BranchContext = createContext<BranchContextValue | null>(null);

export function BranchProvider({ children, branches }: { children: ReactNode; branches: Branch[] }) {
  const [selectedBranchId, setSelectedBranchIdState] = useState<string | null>(
    branches.length > 0 ? null : null
  );

  const setSelectedBranchId = useCallback((id: string | null) => {
    setSelectedBranchIdState(id);
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
