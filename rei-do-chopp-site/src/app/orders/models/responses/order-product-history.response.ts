import { ProductResponse } from "../../../products/models/responses/product.response";

export class OrderProductHistoryResponse {
  Id: number;
  OrderId: number;
  ProductId: number;
  Product: ProductResponse;
  Quantity: number;
  UnitPriceCharged: number;
  ExpectedUnitPrice: number;
  TotalPriceCharged: number;
  ExpectedTotalPrice: number;
}
