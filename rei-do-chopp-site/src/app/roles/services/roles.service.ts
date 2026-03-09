import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { RoleListRequest } from '../models/requests/role-list.request';
import { Observable } from 'rxjs';
import { PaginationResponse } from '../../shared/models/responses/pagination.response';
import { RoleResponse } from '../models/responses/role.response';

@Injectable({
  providedIn: 'root'
})
export class RolesService {
constructor(private http: HttpClient) { }

  private readonly baseUrl = environment.apis.reiDoChoppApi + 'roles';

  public list(request: RoleListRequest): Observable<PaginationResponse<RoleResponse>> {
    return this.http.get<PaginationResponse<RoleResponse>>(this.baseUrl, { params: { ...request } as any });
  }
}
