
import { NgModule } from '@angular/core';
import { AuthModule, LogLevel, OpenIdConfiguration } from 'angular-auth-oidc-client';
import { environment } from 'src/environments/environment';

const configuration: OpenIdConfiguration = {
  configId: 'CMMConf',
  authority: environment.authConfig.authorityUrl,
  redirectUrl: environment.authConfig.redirectUrl,
  postLogoutRedirectUri: environment.authConfig.postLogoutRedirectUri,
  clientId: environment.authConfig.clientId,
  scope: 'openid profile email roles offline_access',
  responseType: 'id_token token',
  // responseType: 'code',
  silentRenew: true,
  useRefreshToken: true,
  logLevel: LogLevel.Debug
};
@NgModule({
  imports: [
    AuthModule.forRoot({
      config: configuration
    })
  ],
  exports: [AuthModule],
})
export class AuthConfigModule {}
