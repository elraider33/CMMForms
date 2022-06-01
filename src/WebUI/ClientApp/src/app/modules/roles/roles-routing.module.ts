import { UpdateRoleComponent } from './pages/update-role/update-role.component';
import { CreateRoleComponent } from './pages/create-role/create-role.component';
import { RoleDetailsComponent } from './pages/role-details/role-details.component';
import { RoleIndexComponent } from './pages/role-index/role-index.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { RolesComponent } from './componensts/roles.component';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { AdminGuard } from 'src/app/core/guards/admin.guard';


const routes : Routes = [
  {
    path: '',
    component: RolesComponent,
    canActivate: [AuthGuard],
    children: [
      {path: '', component: RoleIndexComponent},
      {path: 'create', component: CreateRoleComponent, canActivate:[AdminGuard]},
      {path: ':id', component: RoleDetailsComponent},
      {path: ':id/edit', component: UpdateRoleComponent, canActivate:[AdminGuard]}
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class RolesRoutingModule { }
