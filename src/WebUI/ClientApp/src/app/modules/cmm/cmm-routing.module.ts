import { CmmByRoleComponent } from './pages/cmm-by-role/cmm-by-role.component';
import { UpdateCmmComponent } from './pages/update-cmm/update-cmm.component';
import { CreateCmmComponent } from './pages/create-cmm/create-cmm.component';
import { CmmDetailsComponent } from './pages/cmm-details/cmm-details.component';
import { CmmIndexComponent } from './pages/cmm-index/cmm-index.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CmmsComponent } from './componensts/cmms.component';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { AdminGuard } from 'src/app/core/guards/admin.guard';


const routes : Routes = [
  {
    path: '',
    component: CmmsComponent,
    canActivate: [AuthGuard],
    children: [
      {path: '', component: CmmIndexComponent},
      {path: 'create', component: CreateCmmComponent, canActivate:[AdminGuard]},
      {path: ':id', component: CmmDetailsComponent},
      {path: ':id/edit', component: UpdateCmmComponent, canActivate:[AdminGuard]},
      {path: ':id/cmmRole', pathMatch: 'full',component: CmmByRoleComponent}
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class CmmRoutingModule { }
