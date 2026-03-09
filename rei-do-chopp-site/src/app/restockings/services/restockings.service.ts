import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ProductsInsertRequest } from '../../products/models/requests/products-insert.request';
import { ProductsListRequest } from '../../products/models/requests/products-list.request';
import { ProductResponse } from '../../products/models/responses/product.response';
import { PaginationResponse } from '../../shared/models/responses/pagination.response';
import { RestockingResponse } from '../models/responses/restocking-response';
import { RestockingRequest } from '../models/requests/restocking-request';
import { OrderResponse } from '../../orders/models/responses/order-response';
import { RestockingProductHistoryResponse } from '../models/responses/restocking-product-history.response';
import { RestockingProductListRequest } from '../models/requests/restocking-product-list.request';
import { RestockingAdditionalFeeListRequest } from '../models/requests/restocking-additional-fee-list.request';
import { RestockingAdditionalFeeResponse } from '../models/responses/restocking-additional-fee-response';
import { RestockingListRequest } from '../models/requests/restocking-list.request';
import { RestockingHistoryResponse } from '../models/responses/restocking-history.response';
import { RestockingTotalsCalculationResponse } from '../models/responses/restocking-totals-calculation.response';

@Injectable({
  providedIn: 'root'
})
export class RestockingsService {

  constructor(private http: HttpClient) { }

  private readonly baseUrl = environment.apis.reiDoChoppApi + 'restockings';

  public list(request: RestockingListRequest): Observable<PaginationResponse<RestockingHistoryResponse>> {
    return this.http.get<PaginationResponse<RestockingHistoryResponse>>(this.baseUrl, { params: { ...request } as any });
  }

  public listRestockingProducts(request: RestockingProductListRequest): Observable<PaginationResponse<RestockingProductHistoryResponse>> {
    return this.http.get<PaginationResponse<RestockingProductHistoryResponse>>(environment.apis.reiDoChoppApi + 'restocking-products', { params: { ...request } as any });
  }

  public listRestockingAdditionalFees(request: RestockingAdditionalFeeListRequest): Observable<PaginationResponse<RestockingAdditionalFeeResponse>> {
    return this.http.get<PaginationResponse<RestockingAdditionalFeeResponse>>(environment.apis.reiDoChoppApi + 'restocking-additional-fees', { params: { ...request } as any });
  }

  public insert(request: RestockingRequest): Observable<RestockingResponse> {
    return this.http.post<RestockingResponse>(this.baseUrl, request);
  }

  public edit(restockingId: number, request: RestockingRequest): Observable<RestockingResponse> {
    return this.http.put<RestockingResponse>(`${this.baseUrl}/${restockingId}`, request);
  }

  public get(restockingId: number): Observable<RestockingResponse> {
    return this.http.get<RestockingResponse>(`${this.baseUrl}/${restockingId}`);
  }

  public calculateTotals(request: RestockingRequest): Observable<RestockingTotalsCalculationResponse> {
    return this.http.post<RestockingTotalsCalculationResponse>(this.baseUrl + '/totals-calculations', request);
  }

  public delete(restockingId: number): Observable<void>
  {
    return this.http.delete<void>(`${this.baseUrl}/${restockingId}`)
  }
}
