export class RestockingAdditionalFeeRequest {
  value: number;
  description: string;

  constructor(init?: Partial<RestockingAdditionalFeeRequest>) {
    Object.assign(this, init);
  }
}
