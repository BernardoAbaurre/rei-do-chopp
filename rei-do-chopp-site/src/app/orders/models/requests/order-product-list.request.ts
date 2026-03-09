import { PaginationRequest } from "../../../shared/models/requests/pagination.request";

export class OrderProductListRequest extends PaginationRequest {
  OrderId: number

  constructor(params: Partial<OrderProductListRequest> ) {
    super(params);
    this.OrderId = params.OrderId || null;
  }
}
