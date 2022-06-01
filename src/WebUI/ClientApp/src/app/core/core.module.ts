import { TokenInterceptor } from './interceptors/token.interceptor';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from './services/api.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    ApiService,
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ]
})
export class CoreModule { }
