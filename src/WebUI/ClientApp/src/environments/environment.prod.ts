export const environment = {
  production: true,
  // authConfig: {
  //   authorityUrl: 'https://localhost:44322',
  //   redirectUrl: `${window.location.origin}/signin-oidc`,
  //   postLogoutRedirectUri: `${window.location.origin}/signout-callback-oidc/`,
  //   clientId: '14',
  // }
  authConfig: {
    authorityUrl: 'http://sgai-sqlts2/',
    redirectUrl: `${window.location.origin}/signin-oidc`,
    postLogoutRedirectUri: `${window.location.origin}/signout-callback-oidc/`,
    clientId: '24',
  }
};
