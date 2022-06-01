import { LoginCallbackComponent } from './components/login-callback.component';
import { AuthRoutingModule } from './auth-routing.module';
import { NgModule } from '@angular/core';
import { AuthComponent } from './components/auth/auth.component';
import { LoginComponent } from './pages/login/login.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { UnauthorizedComponent } from './pages/unauthorized/unauthorized.component';

@NgModule({
  declarations: [
    AuthComponent,
    LoginComponent,
    UnauthorizedComponent,
    LoginCallbackComponent
  ],
  imports: [
    SharedModule,
    AuthRoutingModule
  ]
})
export class AuthModule { }
