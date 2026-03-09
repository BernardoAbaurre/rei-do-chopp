import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { UsersService } from '../../users/services/users.service';

export const AuthGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const userService = inject(UsersService);

  return userService.getCurrentUser().pipe(
    map((response) => {
      if(response)
      {
        return true;
      }
      else{
        router.navigate(['/login']);
        return false;
      }
    }),
    catchError((err) => {
      router.navigate(['/login']);
      return of(false);
    })
  );
};
