import { Injectable } from '@angular/core';
import { Observable, of, delay, map } from 'rxjs';
import { InventoryItem } from '../models/inventory-item';
import { BulkJob } from '../models/bulk-job';
import { MOCK_INVENTORY_ITEMS } from '../data/mock-inventory-items';

@Injectable({
  providedIn: 'root',
})
export class InventoryData {
  private items: InventoryItem[] = [...MOCK_INVENTORY_ITEMS];

  private jobs: BulkJob[] = [
    {
      id: 1,
      type: 'price-update',
      status: 'completed',
      startedAt: new Date('2026-03-08T09:00:00'),
      completedAt: new Date('2026-03-08T09:03:00'),
      itemCount: 25,
    },
    {
      id: 2,
      type: 'archive',
      status: 'running',
      startedAt: new Date('2026-03-09T08:15:00'),
      completedAt: null,
      itemCount: 12,
    },
    {
      id: 3,
      type: 'restock-sync',
      status: 'queued',
      startedAt: null,
      completedAt: null,
      itemCount: 40,
    },
  ];

  getInventoryItems(): Observable<InventoryItem[]> {
    return of(this.items).pipe(delay(1200));
  }

  getInventoryItemById(id: number): Observable<InventoryItem | undefined> {
    return of(this.items).pipe(
      delay(800),
      map((items) => items.find((item) => item.id === id)),
    );
  }

  updateInventoryItem(updated: InventoryItem): Observable<InventoryItem> {
    this.items = this.items.map((item) => (item.id === updated.id ? updated : item));
    return of(updated).pipe(delay(1000));
  }

  getJobs(): Observable<BulkJob[]> {
    return of([...this.jobs]).pipe(delay(1500));
  }

  startBulkJob(type: BulkJob['type']): Observable<BulkJob> {
    const newJob: BulkJob = {
      id: this.jobs.length > 0 ? Math.max(...this.jobs.map((job) => job.id)) + 1 : 1,
      type,
      status: 'running',
      startedAt: new Date(),
      completedAt: null,
      itemCount: Math.floor(Math.random() * 50) + 5,
    };

    this.jobs = [newJob, ...this.jobs];

    setTimeout(() => {
      this.jobs = this.jobs.map((job) =>
        job.id === newJob.id
          ? {
              ...job,
              status: 'completed',
              completedAt: new Date(),
            }
          : job,
      );
    }, 5000);

    return of(newJob).pipe(delay(1000));
  }

  deleteInventoryItem(id: number): Observable<number> {
    this.items = this.items.filter((item) => item.id !== id);
    return of(id).pipe(delay(800));
  }
}
