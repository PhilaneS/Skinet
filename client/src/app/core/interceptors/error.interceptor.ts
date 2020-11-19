import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private toastrService: ToastrService){}
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(error => {
        if (error)
        {
          if (error.status === 400){
            if (error.error.errors)
            {
              throw error.error;
            }else {
            this.toastrService.error(error.error.message, error.error.statusCode);
            }
          }
          if (error.status === 401){
            this.toastrService.error(error.error.message, error.error.statusCode);
          }
          if (error.status === 404){
            this.router.navigateByUrl('/not-found');
          }
          if (error.status === 500){
            const navigationExtras: NavigationExtras = {state: {error: error.error}};
            this.router.navigateByUrl('/server-error', navigationExtras);
          }
        }
        return throwError(error);
      })
      );
  }
}
