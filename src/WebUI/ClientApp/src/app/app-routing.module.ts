import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router'

const routes: Routes = [
  {
    path: '',
    redirectTo: '/modules/welcome',
    pathMatch: 'full'
  },
  { path: 'signin-oidc', redirectTo: '/auth/signin-oidc', pathMatch: 'full' },
  {
    path: 'auth',
    loadChildren: () => import('./modules/auth/auth.module').then((m) => m.AuthModule)
  },
  {
    path: 'modules',
    canActivate: [AuthGuard],
    loadChildren: () => import('./modules/layout/layout.module').then(m => m.LayoutModule )},
];
@NgModule({
  imports: [
    RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy', preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule  { }
