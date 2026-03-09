import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatDividerModule } from '@angular/material/divider';

interface DashboardStat {
  title: string;
  value: string;
  description: string;
  testId: string;
  icon: string;
}

interface ActivityItem {
  title: string;
  description: string;
  timestamp: string;
  testId: string;
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatDividerModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class Dashboard {
  protected readonly stats: DashboardStat[] = [
    {
      title: 'Open Inventory Items',
      value: '128',
      description: 'Items currently available in the system',
      testId: 'dashboard-stat-open-items',
      icon: 'inventory_2'
    },
    {
      title: 'Pending Reviews',
      value: '7',
      description: 'Records waiting for approval',
      testId: 'dashboard-stat-pending-reviews',
      icon: 'assignment_late'
    },
    {
      title: 'Queued Jobs',
      value: '3',
      description: 'Background jobs still processing',
      testId: 'dashboard-stat-queued-jobs',
      icon: 'schedule'
    }
  ];

  protected readonly recentActivity: ActivityItem[] = [
    {
      title: 'Inventory sync completed',
      description: 'Nightly synchronization finished successfully.',
      timestamp: 'Today at 8:14 AM',
      testId: 'dashboard-activity-sync'
    },
    {
      title: 'Item INV-104 updated',
      description: 'Quantity adjusted from 16 to 21.',
      timestamp: 'Today at 9:42 AM',
      testId: 'dashboard-activity-item-update'
    },
    {
      title: 'Review submitted',
      description: 'Safety review entered for chemical cabinet stock.',
      timestamp: 'Today at 10:05 AM',
      testId: 'dashboard-activity-review'
    }
  ];
}