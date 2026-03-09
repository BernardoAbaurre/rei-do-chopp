import { PaginationRequest } from "../../../shared/models/requests/pagination.request";

export class RestockingListRequest extends PaginationRequest {
  InitialDate: string;
  FinalDate: string;
  ProductsIds: Array<number>

  constructor(params: Partial<{ InitialDate: Date, FinalDate: Date, ProductsIds: Array<number> }> & Partial<PaginationRequest>) {
    super(params);
    this.InitialDate = params.InitialDate.toISOString() || null;
    this.FinalDate = params.FinalDate.toISOString() || null;
    this.ProductsIds = params.ProductsIds || [];
  }
}
