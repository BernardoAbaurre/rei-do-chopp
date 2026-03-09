import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { catchError, Observable, throwError } from "rxjs";
import { UsersService } from "../../users/services/users.service";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor
{
  constructor(private toastr: ToastrService, private router: Router, private userService: UsersService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = 'Something went wrong';

        if (error.error instanceof ErrorEvent) {
          errorMessage = `${error.error.message}`;
        } else {
          if (error.status === 500) {
            const regex = /:(.*?)\r\n/;
            const match = error.error.match(regex);

            if (match && match[1]) {
              errorMessage = match[1].trim();
            }
          }
          else if (error.status === 400) {
            errorMessage = error.error?.message || 'Invalid request';
          }
          else if (error.status === 401 ) {
            if(window.location.href.includes('login') )
            {
              return null;
            }

            errorMessage = 'Usuário não autorizado';
            this.userService.logout();
            this.router.navigateByUrl('/login');
          }
        }

        this.toastr.error(errorMessage, `Erro ${error.status}`);
        return throwError(() => error);
      })
    );
  }
}
