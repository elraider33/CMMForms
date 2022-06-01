import { RolesComponent } from './componensts/roles.component';
import { RoleIndexComponent } from './pages/role-index/role-index.component';
import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { RolesRoutingModule } from './roles-routing.module';
import { RoleDetailsComponent } from './pages/role-details/role-details.component';
import { CreateRoleComponent } from './pages/create-role/create-role.component';
import { UpdateRoleComponent } from './pages/update-role/update-role.component';



@NgModule({
  declarations: [
    RolesComponent,
    RoleIndexComponent,
    RoleDetailsComponent,
    CreateRoleComponent,
    UpdateRoleComponent
  ],
  imports: [
    SharedModule,
    RolesRoutingModule
  ]
})
export class RoleModule { }
