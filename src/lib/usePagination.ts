import { useState, useEffect, useCallback } from 'react';

export interface PaginationState {
  page: number;
  pageSize: number;
}

const DEFAULT_PAGE_SIZE = 10;
const PAGE_SIZE_OPTIONS = [10, 25, 50, 100];

export function usePagination(storageKey: string, defaultPageSize: number = DEFAULT_PAGE_SIZE) {
  const [page, setPage] = useState<number>(() => {
    try {
      const stored = localStorage.getItem(`${storageKey}_page`);
      return stored ? Math.max(1, parseInt(stored)) : 1;
    } catch {
      return 1;
    }
  });

  const [pageSize, setPageSize] = useState<number>(() => {
    try {
      const stored = localStorage.getItem(`${storageKey}_pageSize`);
      return stored ? parseInt(stored) : defaultPageSize;
    } catch {
      return defaultPageSize;
    }
  });

  useEffect(() => {
    try {
      localStorage.setItem(`${storageKey}_page`, String(page));
    } catch { /* ignore */ }
  }, [page, storageKey]);

  useEffect(() => {
    try {
      localStorage.setItem(`${storageKey}_pageSize`, String(pageSize));
    } catch { /* ignore */ }
  }, [pageSize, storageKey]);

  const handlePageChange = useCallback((newPage: number) => {
    setPage(Math.max(1, newPage));
  }, []);

  const handlePageSizeChange = useCallback((newSize: number) => {
    setPageSize(newSize);
    setPage(1);
  }, []);

  const resetPage = useCallback(() => {
    setPage(1);
  }, []);

  return {
    page,
    pageSize,
    setPage: handlePageChange,
    setPageSize: handlePageSizeChange,
    resetPage,
    pageSizeOptions: PAGE_SIZE_OPTIONS,
  };
}

export function paginate<T>(items: T[], page: number, pageSize: number): { data: T[]; totalPages: number; totalItems: number; startIndex: number; endIndex: number } {
  const totalItems = items.length;
  const totalPages = Math.max(1, Math.ceil(totalItems / pageSize));
  const currentPage = Math.min(page, totalPages);
  const startIndex = (currentPage - 1) * pageSize;
  const endIndex = Math.min(startIndex + pageSize, totalItems);
  const data = items.slice(startIndex, endIndex);
  return { data, totalPages, totalItems, startIndex, endIndex };
}
