import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { 
    path: 'auth/login', 
    loadComponent: () => import('./auth/login/login.component').then(c => c.LoginComponent)
  },
  { 
    path: 'auth/register', 
    loadComponent: () => import('./auth/register/register.component').then(c => c.RegisterComponent)
  },
  { 
    path: 'auth/forgot-password', 
    loadComponent: () => import('./auth/forgot-password/forgot-password.component').then(c => c.ForgotPasswordComponent)
  },
  { 
    path: 'auth/reset-password', 
    loadComponent: () => import('./auth/reset-password/reset-password.component').then(c => c.ResetPasswordComponent)
  },
  { 
    path: 'dashboard', 
    loadComponent: () => import('./dashboard/dashboard.component').then(c => c.DashboardComponent),
    canActivate: [authGuard]
  },
  { path: '**', redirectTo: '/dashboard' }
];
