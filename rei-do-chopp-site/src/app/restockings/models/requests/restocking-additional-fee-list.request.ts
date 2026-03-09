import { PaginationRequest } from "../../../shared/models/requests/pagination.request";

export class RestockingAdditionalFeeListRequest extends PaginationRequest{
  RestockingId: number;

  constructor(params: Partial<RestockingAdditionalFeeListRequest>) {
    super(params);
    this.RestockingId = params.RestockingId || null;
  }
}
