import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { TOKEN_KEY } from 'src/app/core/utils/constants';
import {AuthService} from 'src/app/core/services/auth.service';
import { UiService } from 'src/app/core/services/ui.service';
@Component({
  template: ''
})
export class LoginCallbackComponent implements OnInit {
  constructor(
    private router: Router,
    public oidcSecurityService: OidcSecurityService,
    private authService: AuthService,
    private uiService: UiService){

  }
  async ngOnInit() {
    this.uiService.spin$.next(true);
    const configuration = this.oidcSecurityService.getConfiguration();
    const { isAuthenticated, userData, accessToken } = await this.oidcSecurityService.checkAuth().toPromise();
    var userDatas = this.oidcSecurityService.getUserData();
    if(isAuthenticated && userData ) {
      const { email } = userData;
      var register = { email, token: accessToken};
      var result = await this.authService.register(register).toPromise();
      var login = {username: email, token: accessToken}
      var retultToken = await this.authService.loginDocksite(login).toPromise();
      this.uiService.spin$.next(false);
      localStorage.setItem(TOKEN_KEY, retultToken['accessToken']);
      this.router.navigateByUrl('/modules/welcome');
      var userInfo = await this.authService.getUserDocksiteInfo(accessToken).toPromise();
      this.router.navigate(['modules','welcome']);
    }else{
      const data = await this.oidcSecurityService.checkAuth().toPromise();
      this.uiService.spin$.next(false);
      this.router.navigateByUrl('/modules/welcome');
    }
  }

}


