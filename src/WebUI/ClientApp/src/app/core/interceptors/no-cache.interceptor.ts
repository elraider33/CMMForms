import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from 'src/app/core/services/auth.service';


@Injectable({
  providedIn: 'root'
})
export class NoCacheHeadersInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler) {
      const authReq = req.clone({
        setHeaders: {
          'Cache-Control': 'no-cache',
          Pragma: 'no-cache'
        }
      });
      return next.handle(authReq);
    }
  }
