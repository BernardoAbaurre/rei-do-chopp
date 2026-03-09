import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class DateInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const body = this.convertDates(req.body);
    const clonedRequest = req.clone({ body });
    return next.handle(clonedRequest);
  }

  private convertDates(obj: any): any {
    if (obj === null || obj === undefined) {
      return obj;
    }

    if (obj instanceof Date) {
      return this.toISOStringWithoutTimezone(obj);
    }

    if (Array.isArray(obj)) {
      return obj.map(item => this.convertDates(item));
    }

    if (typeof obj === 'object') {
      const newObj: any = {};
      for (const key of Object.keys(obj)) {
        newObj[key] = this.convertDates(obj[key]);
      }
      return newObj;
    }

    return obj;
  }

  private toISOStringWithoutTimezone(date: Date): string {
    const pad = (n: number) => n.toString().padStart(2, '0');
    return `${date.getFullYear()}-${pad(date.getMonth() + 1)}-${pad(date.getDate())}T${pad(date.getHours())}:${pad(date.getMinutes())}:${pad(date.getSeconds())}`;
  }
}
