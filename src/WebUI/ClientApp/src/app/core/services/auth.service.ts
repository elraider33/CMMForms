import { LogLevel, OpenIdConfiguration } from 'angular-auth-oidc-client';
import { ApiService } from './api.service';
import { Injectable } from '@angular/core';
import { ADMIN_ROLE, TOKEN_KEY } from '../utils/constants';
import { JwtHelperService } from "@auth0/angular-jwt";
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private api: ApiService) { }

  public login({username, password}){
    return this.api.post('Account/login', {username, password});
  }

  public register({email, token}){
    return this.api.post('Account/Register', {email, token});
  }

  public loginDocksite({username, token}){
    return this.api.post('Account/LoginDocksite', {username, token});
  }

  public getUserDocksiteInfo(token: string){
    return this.api.getUserDocksiteInfo(token);
  }

  isTokenExpired() {
    const helper = new JwtHelperService();
    const token = localStorage.getItem(TOKEN_KEY);
    return helper.isTokenExpired(token);
  }
  public getDisplayName (){
    const token = localStorage.getItem(TOKEN_KEY);
    const decoded = this.getTokenDecoded(token);
    return decoded['unique_name'];
  }
  public getTokenDecoded(token: string){
    const helper = new JwtHelperService();
    const decoded = helper.decodeToken(token);
    return decoded;
  }
  public isAdmin(): boolean {
    const token = localStorage.getItem(TOKEN_KEY);
    const helper = new JwtHelperService();
    const decoded = helper.decodeToken(token);
    return decoded['role'] === ADMIN_ROLE;
  }
  public getToken(){
    const token = localStorage.getItem(TOKEN_KEY);
    return token;
  }
}
