export class OrderAdditionalFeeRequest {
  Value!: number;
  Description!: string;

  constructor(init?: Partial<OrderAdditionalFeeRequest>) {
    Object.assign(this, init);
  }
}
