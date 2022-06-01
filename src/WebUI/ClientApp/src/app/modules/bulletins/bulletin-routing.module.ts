import { UpdateBulletinComponent } from './pages/update-bulletin/update-bulletin.component';
import { BulletinIndexComponent } from './pages/bulletinIndex/bulletinIndex.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { BulletinsComponent } from './components/bulletins.component';
import { CreateBulletinComponent } from './pages/create-bulletin/create-bulletin.component';
import { BulletinDetailsComponent } from './pages/bulletin-details/bulletin-details.component';
import { BulletinsByRoleComponent } from './pages/bulletins-by-role/bulletins-by-role.component';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { AdminGuard } from 'src/app/core/guards/admin.guard';

const routes : Routes = [
  {
    path: '',
    component: BulletinsComponent,
    canActivate: [AuthGuard],
    children: [
      {path: '', component: BulletinIndexComponent},
      {path: 'create', component: CreateBulletinComponent, canActivate:[AdminGuard]},
      {path: ':id', component: BulletinDetailsComponent},
      {path: ':id/edit', component: UpdateBulletinComponent, canActivate:[AdminGuard]},
      {path: ':id/bulletinRole', pathMatch: 'full',component: BulletinsByRoleComponent}
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class BulletinRoutingModule { }
