import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { SettingsAuthRequest } from '../models/settings-auth.request';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  constructor(private http: HttpClient) { }

  private readonly baseUrl = environment.apis.reiDoChoppApi + 'settings';

  public resetStock(request: SettingsAuthRequest): Observable<null> {
    return this.http.put<null>(`${this.baseUrl}/stock-reset`, request);
  }

  public resetBase(request: SettingsAuthRequest): Observable<null> {
    return this.http.put<null>(`${this.baseUrl}/base-reset`, request);
  }

}
