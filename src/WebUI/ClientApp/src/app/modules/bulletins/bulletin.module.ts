import { NgModule } from '@angular/core';
import { SharedModule } from './../../shared/shared.module';
import { BulletinIndexComponent } from './pages/bulletinIndex/bulletinIndex.component';
import { BulletinRoutingModule } from './bulletin-routing.module';
import { BulletinsComponent } from './components/bulletins.component';
import { CreateBulletinComponent } from './pages/create-bulletin/create-bulletin.component';
import { BulletinDetailsComponent } from './pages/bulletin-details/bulletin-details.component';
import { UpdateBulletinComponent } from './pages/update-bulletin/update-bulletin.component';
import { BulletinsByRoleComponent } from './pages/bulletins-by-role/bulletins-by-role.component';

@NgModule({
  declarations: [
    BulletinsComponent,
    BulletinIndexComponent,
    CreateBulletinComponent,
    BulletinDetailsComponent,
    UpdateBulletinComponent,
    BulletinsByRoleComponent
  ],
  imports: [
    SharedModule,
    BulletinRoutingModule
  ]
})
export class BulletinModule { }
