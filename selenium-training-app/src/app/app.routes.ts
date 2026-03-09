import { Routes } from '@angular/router';
import { Layout } from './core/layout/layout';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./features/auth/login/login').then((m) => m.Login),
  },

  {
    path: '',
    component: Layout,
    children: [
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full',
      },
      {
        path: 'dashboard',
        loadComponent: () => import('./features/dashboard/dashboard').then((m) => m.Dashboard),
      },
      {
        path: 'inventory',
        loadComponent: () =>
          import('./features/inventory/inventory-list/inventory-list').then((m) => m.InventoryList),
      },
      {
        path: 'inventory/:id',
        loadComponent: () =>
          import('./features/inventory/inventory-detail/inventory-detail').then(
            (m) => m.InventoryDetail,
          ),
      },
      {
        path: 'jobs',
        loadComponent: () => import('./features/jobs/job-queue/job-queue').then((m) => m.JobQueue),
      },
    ],
  },

  {
    path: '**',
    redirectTo: 'dashboard',
  },
];
