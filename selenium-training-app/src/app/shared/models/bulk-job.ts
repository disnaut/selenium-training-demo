export interface BulkJob {
  id: number;
  type: 'price-update' | 'archive' | 'restock-sync';
  status: 'queued' | 'running' | 'completed' | 'failed';
  startedAt: Date | null;
  completedAt: Date | null;
  itemCount: number;
}
