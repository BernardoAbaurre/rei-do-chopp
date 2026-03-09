import { OrderAdditionalFeeRequest } from "./order-additional-fee.request";
import { OrderProductRequest } from "./order-product.request";


export class OrderRequest {
  AttendantId?: number;
  OrderDate: Date;
  Discount?: number;
  OrderAdditionalFees: OrderAdditionalFeeRequest[];
  OrderProducts: OrderProductRequest[];

  constructor(params?: Partial<OrderRequest>) {
    this.AttendantId = params.AttendantId ?? null;
    this.OrderDate = params.OrderDate || null;
    this.Discount = params.Discount || null;
    this.OrderAdditionalFees = params.OrderAdditionalFees || [];
    this.OrderProducts = params.OrderProducts || [];
  }
}
