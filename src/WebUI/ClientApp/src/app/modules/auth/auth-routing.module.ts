import { LoginCallbackComponent } from './components/login-callback.component';
import { NgModule } from '@angular/core';
import { Route, RouterModule } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { LoginComponent } from './pages/login/login.component';

const routes: Route[] = [
  {
    path: '',
    component: AuthComponent,
    // canActivate: [NoAuthGuard],
    children: [
      {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full'
      },
      {
        path: 'login',
        component: LoginComponent,
        data: { returnUrl: window.location.pathname }
      },
      {path:'signin-oidc', component: LoginCallbackComponent, data:{ title: 'login page'}},
      { path: '', redirectTo: '/login', pathMatch: 'full' },
      { path: '**', redirectTo: '/login', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule {}
