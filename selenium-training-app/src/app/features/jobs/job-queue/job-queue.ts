import { CommonModule } from '@angular/common';
import { Component, OnInit, OnDestroy, inject, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Subject, interval } from 'rxjs';
import { startWith, switchMap, takeUntil } from 'rxjs/operators';

import { InventoryData } from '../../../shared/services/inventory-data';
import { BulkJob } from '../../../shared/models/bulk-job';

@Component({
  selector: 'app-job-queue',
  standalone: true,
  templateUrl: './job-queue.html',
  styleUrl: './job-queue.scss',
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatTableModule,
    MatSnackBarModule,
  ],
})
export class JobQueue implements OnInit, OnDestroy {
  private readonly inventoryService = inject(InventoryData);
  private readonly snackBar = inject(MatSnackBar);
  private readonly destroy$ = new Subject<void>();

  protected readonly jobs = signal<BulkJob[]>([]);
  protected readonly loading = signal(false);

  protected readonly displayedColumns = [
    'id',
    'type',
    'status',
    'itemCount',
    'startedAt',
    'completedAt',
  ];

  public ngOnInit(): void {
    this.startPolling();
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  protected loadJobs(showLoader: boolean = true): void {
    if (showLoader) {
      this.loading.set(true);
    }

    this.inventoryService.getJobs().subscribe({
      next: (jobs) => {
        this.jobs.set(jobs);
        if (showLoader) {
          this.loading.set(false);
        }
      },
      error: () => {
        if (showLoader) {
          this.loading.set(false);
        }

        this.snackBar.open('Failed to load bulk jobs.', 'Dismiss', {
          duration: 3000,
        });
      },
    });
  }

  protected startJob(type: BulkJob['type']): void {
    this.loading.set(true);

    this.inventoryService.startBulkJob(type).subscribe({
      next: () => {
        this.snackBar.open(`Started ${type} job.`, 'Dismiss', {
          duration: 3000,
        });

        this.loadJobs(true);
      },
      error: () => {
        this.loading.set(false);

        this.snackBar.open('Failed to start job.', 'Dismiss', {
          duration: 3000,
        });
      },
    });
  }

  private startPolling(): void {
    interval(3000)
      .pipe(
        startWith(0),
        switchMap(() => this.inventoryService.getJobs()),
        takeUntil(this.destroy$),
      )
      .subscribe({
        next: (jobs) => {
          this.jobs.set(jobs);
          this.loading.set(false);
        },
        error: () => {
          this.loading.set(false);

          this.snackBar.open('Failed to refresh bulk jobs.', 'Dismiss', {
            duration: 3000,
          });
        },
      });
  }
}
