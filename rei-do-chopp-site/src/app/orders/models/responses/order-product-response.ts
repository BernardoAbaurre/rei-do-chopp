import { ProductResponse } from "../../../products/models/responses/product.response";

export interface OrderProductResponse {
  Id: number;
  OrderId: number;
  Product: ProductResponse;
  Quantity: number;
  UnitPriceCharged: number;
}
