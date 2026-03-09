import { PaginationRequest } from "../../../shared/models/requests/pagination.request";

export class OrderAdditionalFeeListRequest extends PaginationRequest{
  OrderId: number;

  constructor(params: Partial<OrderAdditionalFeeListRequest>) {
    super(params);
    this.OrderId = params.OrderId || null;
  }
}
