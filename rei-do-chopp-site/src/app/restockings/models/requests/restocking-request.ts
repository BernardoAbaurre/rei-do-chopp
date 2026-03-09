import { RestockingProductRequest } from './restocking-product-request';
import { RestockingAdditionalFeeRequest } from './restocking-additional-fee-request';

export class RestockingRequest {
  Date: Date;
  Discount?: number;
  RestockingProducts: RestockingProductRequest[];
  RestockingAdditionalFees: RestockingAdditionalFeeRequest[];

  constructor(params: Partial<RestockingRequest>) {
    this.Date = params.Date || null;
    this.Discount = params.Discount || null;
    this.RestockingProducts = params.RestockingProducts || [];
    this.RestockingAdditionalFees = params.RestockingAdditionalFees || [];
  }
}
