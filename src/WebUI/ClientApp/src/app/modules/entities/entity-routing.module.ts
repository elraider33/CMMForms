import { EntitiesIndexComponent } from './pages/entities-index/entities-index.component';
import { EntitiesComponent } from './components/entities.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { AdminGuard } from 'src/app/core/guards/admin.guard';

const routes : Routes = [
  {
    path: '',
    component: EntitiesComponent,
    canActivate: [AuthGuard],
    children: [
      {path: '', component: EntitiesIndexComponent, canActivate:[AdminGuard]},
      // {path: 'create', component: CreateBulletinComponent, canActivate:[AdminGuard]},
      // {path: ':id', component: BulletinDetailsComponent},
      // {path: ':id/edit', component: UpdateBulletinComponent, canActivate:[AdminGuard]},
      // {path: ':id/bulletinRole', pathMatch: 'full',component: BulletinsByRoleComponent}
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class EntityRoutingModule { }
