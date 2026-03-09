import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { ProductsListRequest } from '../models/requests/products-list.request';
import { Observable } from 'rxjs';
import { ProductResponse } from '../models/responses/product.response';
import { PaginationResponse } from '../../shared/models/responses/pagination.response';
import { ProductsInsertRequest } from '../models/requests/products-insert.request';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  constructor(private http: HttpClient) { }

  private readonly baseUrl = environment.apis.reiDoChoppApi + 'products';

  public list(request: ProductsListRequest): Observable<PaginationResponse<ProductResponse>> {
    return this.http.get<PaginationResponse<ProductResponse>>(this.baseUrl, { params: { ...request } as any });
  }

  public insert(request: ProductsInsertRequest): Observable<ProductResponse> {
    return this.http.post<ProductResponse>(this.baseUrl, request);
  }

  public edit(productId: number, request: ProductsInsertRequest): Observable<ProductResponse> {
    return this.http.put<ProductResponse>(`${this.baseUrl}/${productId}`, request);
  }

  public changeStatus(productId: number,): Observable<ProductResponse> {
    return this.http.put<ProductResponse>(`${this.baseUrl}/status-changes/${productId}`, null);
  }
}
