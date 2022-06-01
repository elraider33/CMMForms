// import { OAuthService } from 'angular-oauth2-oidc';
import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { FormGroup, FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';
import { catchError, finalize } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { EMPTY, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { UiService } from 'src/app/core/services/ui.service';
import { OidcClientNotification, OidcSecurityService, OpenIdConfiguration, UserDataResult } from 'angular-auth-oidc-client';
import { TOKEN_KEY } from 'src/app/core/utils/constants';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });
  errors = false;
  errorsMsg = '';
  configuration: OpenIdConfiguration;
  // userDataChanged$: Observable<OidcClientNotification<any>>;
  userData$: Observable<UserDataResult>;
  isAuthenticated = false;
  constructor(
    private authService: AuthService,
    private router: Router,
    private ui : UiService,
    // public oidcSecurityService: OidcSecurityService
  ) {

  }

  ngOnInit(): void {
    // this.configuration = this.oidcSecurityService.getConfiguration();
    // console.log(this.configuration);
    // this.userData$ = this.oidcSecurityService.userData$;

    // this.oidcSecurityService.isAuthenticated$.subscribe(({ isAuthenticated }) => {
    //   this.isAuthenticated = isAuthenticated;

    //   // console.warn('authenticated: ', isAuthenticated);
    // });
  }
  get username() {return this.loginForm.get('username');}
  get password() {return this.loginForm.get('password');}

  dockSiteLogin(){
    console.log('Login?');
    // this.oidcSecurityService.authorize();
    // this.oidcSecurityService.userData$.subscribe(userData => {
    //   console.log('**************************');
    //   console.log(userData);
    // });
  }
  onSubmit(){
    console.log(this.loginForm.value);
    this.errors = false;
    if(this.loginForm.valid){
      this.ui.spin$.next(true);
      const subs = this.authService.login(this.loginForm.value)
      .pipe(finalize(() => {
        subs.unsubscribe();
        this.ui.spin$.next(false);
      }),catchError((error: HttpErrorResponse) =>{
        console.log(error);
        this.ui.spin$.next(false);
        this.errorsMsg = error.error.message;
        this.errors = true;
        return EMPTY;
      }))
      .subscribe((data:{status:number, accessToken:string, message:string})  => {
        localStorage.setItem(TOKEN_KEY, data.accessToken);
        this.router.navigateByUrl('/modules/welcome');
      });
    }
  }
}
