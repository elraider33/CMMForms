import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { empty, EMPTY, from, Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class OidcInterceptor implements HttpInterceptor{
  constructor(){}
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    let request = req;
    if(request.url.includes('.well-known/openid-configuration')){
      console.log(request);
      return next.handle(request).pipe(
        catchError((error: HttpErrorResponse) => {
          console.log(error);
          if(error.statusText === 'Unknown Error'){
            console.log('Unknown Error');
            return from(EMPTY);
          }
        })
      );
    }else{
      return next.handle(request);
    }
  }

}
