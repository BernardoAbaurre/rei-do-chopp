import { ApplicationConfig, importProvidersFrom, inject, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';
import { routes } from './app.routes';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { AuthInterceptor } from './shared/interceptors/auth.interceptor';
import { UsersService } from './users/services/users.service';
import { CleanParamsInterceptor } from './shared/interceptors/clean-params.interceptor';
import { BsModalService } from 'ngx-bootstrap/modal';
import { provideNgxMask } from 'ngx-mask';
import { ErrorInterceptor } from './shared/interceptors/error-interceptor';
import { provideToastr } from 'ngx-toastr';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { DateInterceptor } from './shared/interceptors/date.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    // importProvidersFrom(HttpClientModule),
    CookieService,
    BsModalService,
    provideNgxMask(),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CleanParamsInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: DateInterceptor,
      multi: true
    },
    provideHttpClient(withInterceptorsFromDi()),
    provideToastr(),
    provideAnimations(),
  ]
};
