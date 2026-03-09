import { PaginationRequest } from "../../../shared/models/requests/pagination.request";

export class RestockingProductListRequest extends PaginationRequest {
  RestockingId: number

  constructor(params: Partial<RestockingProductListRequest> ) {
    super(params);
    this.RestockingId = params.RestockingId || null;
  }
}
