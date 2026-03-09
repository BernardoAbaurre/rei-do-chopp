import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpParams,
  HttpRequest
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class CleanParamsInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.method === 'GET' && req.params.keys().length > 0) {
      let newParams = new HttpParams();

      for (const key of req.params.keys()) {
        let value = req.params.get(key);

        if (
          value !== null &&
          value !== undefined &&
          value !== 'null'&&
          value !== 'undefined'
        ) {
          newParams = newParams.set(key, value);
        }
      }
      const cloned = req.clone({ params: newParams });
      
      return next.handle(cloned);
    }

    return next.handle(req);
  }

}
