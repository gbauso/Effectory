import {
    HttpEvent,
    HttpInterceptor,
    HttpHandler,
    HttpRequest,
    HttpErrorResponse
   } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
   import { Observable } from 'rxjs';
   import { retry, catchError } from 'rxjs/operators';

   @Injectable({
    providedIn: 'root',
  })
export class HttpErrorInterceptor implements HttpInterceptor {

    constructor(private route: Router) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      return next.handle(request)
        .pipe(
          retry(1),
          catchError((err, caught) => {
            this.route.navigate(['/error']);
            return caught;
          })
        )
    }
   }
