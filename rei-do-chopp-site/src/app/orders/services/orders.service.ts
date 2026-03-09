import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { OrderResponse } from '../models/responses/order-response';
import { OrderRequest } from '../models/requests/order.request';
import { OperationHistoryListRequest } from '../../finances/operation-histories/models/requests/operation-history-list.request';
import { PaginationResponse } from '../../shared/models/responses/pagination.response';
import { OrderHistoryResponse } from '../models/responses/order-history.response';
import { OrderListRequest } from '../models/requests/order-list.request';
import { OrderProductListRequest } from '../models/requests/order-product-list.request';
import { OrderProductHistoryResponse } from '../models/responses/order-product-history.response';
import { OrderAdditionalFeeListRequest } from '../models/requests/order-additional-fee-list.request';
import { OrderAdditionalFeeResponse } from '../models/responses/order-additional-fee-response';
import { OrderTotalsCalculationResponse } from '../models/responses/order-totals-calculation.response';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(private http: HttpClient) { }

  private readonly baseUrl = environment.apis.reiDoChoppApi + 'orders';

  public list(request: OrderListRequest): Observable<PaginationResponse<OrderHistoryResponse>> {
    return this.http.get<PaginationResponse<OrderHistoryResponse>>(this.baseUrl, { params: { ...request } as any });
  }

  public insert(request: OrderRequest): Observable<OrderResponse> {
    return this.http.post<OrderResponse>(this.baseUrl, request);
  }

  public edit(orderId: number, request: OrderRequest): Observable<OrderResponse> {
    return this.http.put<OrderResponse>(`${this.baseUrl}/${orderId}`, request);
  }

  public get(orderId: number): Observable<OrderResponse> {
    return this.http.get<OrderResponse>(`${this.baseUrl}/${orderId}`);
  }

  public listOrderProducts(request: OrderProductListRequest): Observable<PaginationResponse<OrderProductHistoryResponse>> {
    return this.http.get<PaginationResponse<OrderProductHistoryResponse>>(environment.apis.reiDoChoppApi + 'orders-products', { params: { ...request } as any });
  }

  public listOrderAdditionalFees(request: OrderAdditionalFeeListRequest): Observable<PaginationResponse<OrderAdditionalFeeResponse>> {
    return this.http.get<PaginationResponse<OrderAdditionalFeeResponse>>(environment.apis.reiDoChoppApi + 'orders-additional-fees', { params: { ...request } as any });
  }

  public calculateTotals(request: OrderRequest): Observable<OrderTotalsCalculationResponse> {
    return this.http.post<OrderTotalsCalculationResponse>(this.baseUrl + '/totals-calculations', request);
  }

  public print(orderId: number): Observable<boolean> {
    return this.http.post<boolean>(`${this.baseUrl}/prints/${orderId}`, null);
  }

  public delete(orderId: number): Observable<void>
  {
    return this.http.delete<void>(`${this.baseUrl}/${orderId}`)
  }
}
