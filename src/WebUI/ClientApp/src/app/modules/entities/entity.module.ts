import { EntitiesComponent } from './components/entities.component';
import { NgModule } from '@angular/core';
import { SharedModule } from './../../shared/shared.module';
import { EntityRoutingModule } from './entity-routing.module';
import { EntitiesIndexComponent } from './pages/entities-index/entities-index.component';


@NgModule({
  declarations: [
    EntitiesComponent,
    EntitiesIndexComponent,
  ],
  imports: [
    SharedModule,
    EntityRoutingModule
  ]
})
export class EntityModule { }
