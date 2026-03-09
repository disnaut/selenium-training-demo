import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth-guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'login',
    loadComponent: () => import('./features/auth/login/login').then((m) => m.Login),
  },
  {
    path: '',
    canActivate: [authGuard],
    loadComponent: () => import('./core/layout/layout').then((m) => m.Layout),
    children: [
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
    redirectTo: 'login',
  },
];
