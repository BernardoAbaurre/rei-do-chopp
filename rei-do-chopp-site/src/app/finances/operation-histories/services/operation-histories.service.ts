import { OperationHistoryListRequest } from './../models/requests/operation-history-list.request';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { OperationHistoryResponse } from '../models/responses/operatin-history.response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OperationHistoriesService {
  constructor(private http: HttpClient) { }

  private readonly baseUrl = environment.apis.reiDoChoppApi + 'operations-histories';

  public list(request: OperationHistoryListRequest): Observable<OperationHistoryResponse> {
    return this.http.get<OperationHistoryResponse>(this.baseUrl, { params: { ...request } as any });
  }
}
