import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from 'src/app/core/services/auth.service';


@Injectable({
  providedIn: 'root'
})
export class TokenInterceptor implements HttpInterceptor{
  constructor(
    private authService: AuthService,
    private router: Router
  ){}
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.authService.getToken();
    let request = req;
    if(token){
      request = req.clone({
        setHeaders: {
          'Authorization': `Bearer ${token}`
        }
      });
    }
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if(error.status === 401){
          this.router.navigate(['auth', 'login']);
        }
        return throwError(error);
      })
    );
  }

}
