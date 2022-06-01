import { CmmsComponent } from './componensts/cmms.component';
import { CmmIndexComponent } from './pages/cmm-index/cmm-index.component';
import { NgModule } from '@angular/core';
import { SharedModule } from './../../shared/shared.module';
import { CmmRoutingModule } from './cmm-routing.module';
import { CmmDetailsComponent } from './pages/cmm-details/cmm-details.component';
import { CreateCmmComponent } from './pages/create-cmm/create-cmm.component';
import { UpdateCmmComponent } from './pages/update-cmm/update-cmm.component';
import { CmmByRoleComponent } from './pages/cmm-by-role/cmm-by-role.component';



@NgModule({
  declarations: [
    CmmsComponent,
    CmmIndexComponent,
    CmmDetailsComponent,
    CreateCmmComponent,
    UpdateCmmComponent,
    CmmByRoleComponent
  ],
  imports: [
    SharedModule,
    CmmRoutingModule
  ]
})
export class CmmModule { }
