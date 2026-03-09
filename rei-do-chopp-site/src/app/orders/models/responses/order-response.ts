import { OrderAdditionalFeeResponse } from "./order-additional-fee-response";
import { OrderProductResponse } from "./order-product-response";

export interface OrderResponse {
  Id: number;
  CashierName: string;
  AttendantName: string;
  CashierId: number;
  AttendantId: number;
  OrderDate: Date;
  Discount?: number;
  OrderProducts: OrderProductResponse[];
  OrderAdditionalFees: OrderAdditionalFeeResponse[];
}
