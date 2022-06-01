import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/shared/shared.module';
import { LayoutComponent } from './components/layout/layout.component';
import { LayoutRoutingModule } from './layout-routing.module';

@NgModule({
  declarations: [
    LayoutComponent
  ],
  imports: [
    SharedModule,
    LayoutRoutingModule
  ]
})
export class LayoutModule { }
