import { Component } from "@angular/core";
import { NavigationEnd, Router } from "@angular/router";
import { OidcSecurityService } from "angular-auth-oidc-client";
import { mergeMap, tap } from "rxjs/operators";
import { AuthService } from "./core/services/auth.service";
import { UiService } from "./core/services/ui.service";
import { TOKEN_KEY } from "./core/utils/constants";
@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
})
export class AppComponent {
  title = "app";
  constructor(
    private router: Router,
    // public oidcSecurityService: OidcSecurityService,
    private authSevice: AuthService,
    private uiService: UiService
  ) {
  }

  async ngOnInit() {
    // try {
    //   const { isAuthenticated, userData, accessToken } =
    //     await this.oidcSecurityService.checkAuth().toPromise();
    //   console.log(isAuthenticated, userData, accessToken);
    //   if (accessToken && userData) {
    //     this.uiService.spin$.next(true);
    //     const { email } = userData;
    //     var register = { email, token: accessToken };
    //     var result = await this.authSevice.register(register).toPromise();
    //     var login = { username: email, token: accessToken };
    //     var retultToken = await this.authSevice
    //       .loginDocksite(login)
    //       .toPromise();
    //     this.uiService.spin$.next(false);
    //     localStorage.setItem(TOKEN_KEY, retultToken["accessToken"]);
    //     this.router.navigateByUrl("/modules/welcome");
    //   }
    // } catch (error) {
    //   console.log(error);
    // }
  }
}
