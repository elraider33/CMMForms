import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';
import { StringLiteralLike } from 'typescript';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  msg: string;
  constructor(private router: Router){
    this.msg = this.router.getCurrentNavigation().extras.state?.msg || '';
  }
}
