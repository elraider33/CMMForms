import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { AuthService } from 'src/app/core/services/auth.service';
import { NavService } from 'src/app/core/services/nav.service';

@Component({
  selector: 'app-top-nav',
  templateUrl: './top-nav.component.html',
  styleUrls: ['./top-nav.component.scss']
})
export class TopNavComponent implements OnInit {
  isOpen = true;
  @Output() open = new EventEmitter<boolean>();
  constructor(
    public navService: NavService,
    private router: Router,
    // public oidcSecurityService: OidcSecurityService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.open.emit(this.isOpen);
  }
  onOpen(){
    this.isOpen = !this.isOpen;
    this.open.emit(this.isOpen);
  }
  logOut() {
    // if is docksiteuser
    // const isAuth = this.oidcSecurityService.isAuthenticated();
    // // this.oidcSecurityService.logoff();
    // try{
    //   if(isAuth){
    //     this.oidcSecurityService.logoffLocal();
    //   }
    // }catch(error){
    //   console.log(error);
    // }
    localStorage.clear();
    this.router.navigate(['auth','login']);

  }
  get username(){
    return this.authService.getDisplayName();
  }
}
