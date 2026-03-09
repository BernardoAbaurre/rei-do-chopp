import { RestockingProductResponse } from './restocking-product-response';
import { RestockingAdditionalFeeResponse } from './restocking-additional-fee-response';

export interface RestockingResponse {
  Id: number;
  Date: Date;
  UserName: string;
  RestockingAdditionalFees: RestockingAdditionalFeeResponse[];
  RestockingProducts: RestockingProductResponse[];
}
