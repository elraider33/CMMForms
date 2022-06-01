import { LayoutComponent } from './components/layout/layout.component';
import { NgModule } from '@angular/core';
import { Route, RouterModule } from '@angular/router';
import { AuthGuard } from 'src/app/core/guards/auth.guard';

const routes: Route[] = [
  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {path: 'welcome', loadChildren: () => import('../home/home.module').then(m => m.HomeModule), },
      {path: 'service-bulletins', loadChildren: () => import('../bulletins/bulletin.module').then(m => m.BulletinModule)},
      {path: 'manuals', loadChildren: () => import('../cmm/cmm.module').then(m => m.CmmModule)},
      {path: 'entities', loadChildren: () => import('../entities/entity.module').then(m => m.EntityModule)},
      {path: 'roles', loadChildren: () => import('../roles/role.module').then(m => m.RoleModule)}
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule {}
