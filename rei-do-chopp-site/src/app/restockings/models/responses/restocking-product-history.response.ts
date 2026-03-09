import { ProductResponse } from "../../../products/models/responses/product.response";

export class RestockingProductHistoryResponse {
  Id: number;
  RestockingId: number;
  ProductId: number;
  Product: ProductResponse;
  Quantity: number;
  UnitPricePaid: number;
  TotalPricePaid: number;
}
