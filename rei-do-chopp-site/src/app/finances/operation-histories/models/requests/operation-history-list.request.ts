import { PaginationRequest } from '../../../../shared/models/requests/pagination.request';
export class OperationHistoryListRequest {
  InitialDate: string;
  FinalDate: string;
  ProductsIds: Array<number>;

  constructor(params: Partial<{ InitialDate: Date, FinalDate: Date, ProductsIds: Array<number>}>) {
    this.InitialDate = params.InitialDate.toISOString() || null;
    this.FinalDate = params.FinalDate.toISOString() || null;
    this.ProductsIds = params.ProductsIds || [];
  }
}
