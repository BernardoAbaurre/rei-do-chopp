import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { LoginRequest } from '../models/requests/login.request';
import { BehaviorSubject, catchError, map, Observable, of, tap } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { LoginResponse } from '../models/responses/login.response';
import { UserResponse } from '../models/responses/user.response';
import { UserListRequest } from '../models/requests/user-list.request';
import { PaginationResponse } from '../../shared/models/responses/pagination.response';
import { UserResetPasswordRequest } from '../models/requests/user-reset-password.request';
import { UserRegisterRequest } from '../models/requests/user-register-request';
import { UserRolesRequest } from '../models/requests/user-roles.request';
import { UserEditRequest } from '../models/requests/user-edit-request';
import { UserForgotPasswordRequest } from '../models/requests/user-forgot-password.request';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient, private cookieService: CookieService) { }

  private readonly baseUrl = environment.apis.reiDoChoppApi + 'users';

  private _currentUser = new BehaviorSubject<UserResponse>(null);
  public readonly currentUser$ = this._currentUser.asObservable();

  public login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.baseUrl + '/logins', request);
  }
  public saveTokenOnCookies(token: string) {
    const expirationDate = new Date();
    expirationDate.setHours(expirationDate.getHours() + 12);

    this.cookieService.set('token', token, {
      expires: expirationDate,
      path: '/',
      secure: false,
      sameSite: 'Lax'
    });
  }
  public getToken() {
    return this.cookieService.get('token');
  }
  public getCurrentUser(): Observable<UserResponse> {
    return this.http.get<UserResponse>(this.baseUrl + '/current').pipe(
      tap((response) => this._currentUser.next(response)),
      catchError((err) => {
        this._currentUser.next(null);
        return of(null);
      })
    );;
  }
  public logout() {
    this.cookieService.delete('token', '/');
    this._currentUser.next(null);
  }

  public list(request: UserListRequest): Observable<PaginationResponse<UserResponse>> {
    return this.http.get<PaginationResponse<UserResponse>>(this.baseUrl, { params: { ...request } as any });
  }

  public changeStatus(userId: number): Observable<UserResponse> {
    return this.http.put<UserResponse>(`${this.baseUrl}/status-changes/${userId}`, null);
  }

  public resetPassword(request: UserResetPasswordRequest): Observable<void> {
    return this.http.post<void>(this.baseUrl + '/passwords-resets', request);
  }

  public forgotPassword(request: UserForgotPasswordRequest): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/forgotten-passwords`, request);
  }

  public register(request: UserRegisterRequest): Observable<UserResponse> {
    return this.http.post<UserResponse>(this.baseUrl + '/registers', request);
  }

  public setUserRoles(id: number, request: UserRolesRequest): Observable<UserResponse> {
    return this.http.put<UserResponse>(`${this.baseUrl}/roles-sets/${id}`, request);
  }

  public edit(userId: number, request: UserEditRequest): Observable<UserResponse> {
    return this.http.put<UserResponse>(`${this.baseUrl}/${userId}`, request);
  }

  public dbTest(): Observable<boolean> {
    return this.http.get(this.baseUrl + '/db-tests').pipe(
      map(response => response['Result'])
    );
  }
}
