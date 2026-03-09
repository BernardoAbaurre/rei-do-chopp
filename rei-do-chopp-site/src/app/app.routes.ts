import { Routes } from '@angular/router';
import { HubPageComponent } from './hub/pages/hub-page/hub-page.component';
import { LoginComponent } from './users/pages/login/login.component';
import { AuthGuard } from './shared/guards/auth.guard';
import { ResetPasswordComponent } from './users/pages/reset-password/reset-password.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'alterar-senha',
    component: ResetPasswordComponent
  },
  {
    path: 'hub',
    component: HubPageComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'produtos',
    loadComponent: () => import('./products/pages/products-list/products-list.component').then(m => m.ProductsListComponent),
    canActivate: [AuthGuard],
  },
  {
    path: 'reabastecimento',
    loadComponent: () => import('./restockings/pages/restocking-register/restocking-register.component').then(m => m.RestockingRegisterComponent),
    canActivate: [AuthGuard],
  },
  {
    path: 'reabastecimento/:restockingId',
    loadComponent: () => import('./restockings/pages/restocking-register/restocking-register.component').then(m => m.RestockingRegisterComponent),
    canActivate: [AuthGuard],
  },
  {
    path: 'vendas',
    loadComponent: () => import('./orders/pages/order-register/order-register.component').then(m => m.OrderRegisterComponent),
    canActivate: [AuthGuard],
  },
  {
    path: 'vendas/:orderId',
    loadComponent: () => import('./orders/pages/order-register/order-register.component').then(m => m.OrderRegisterComponent),
    canActivate: [AuthGuard],
  },
  {
    path: 'finaceiro',
    children: [
      {
        path: 'historico-operacoes',
        loadComponent: () => import('./finances/operation-histories/pages/operation-histories-list/operation-histories-list.component').then(m => m.OperationHistoriesListComponent),
        canActivate: [AuthGuard],
      },
    ]
  },
  {
    path: 'usuarios',
    loadComponent: () => import('./users/pages/users-list/users-list.component').then(m => m.UsersListComponent),
    canActivate: [AuthGuard],
  },
  {
    path: 'configuracoes',
    loadComponent: () => import('./settings/pages/settings/settings.component').then(m => m.SettingsComponent),
    canActivate: [AuthGuard],
  },

];
