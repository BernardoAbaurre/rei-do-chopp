export class RestockingProductRequest {
  ProductId: number;
  Quantity: number;
  UnitPricePaid: number;

  constructor(init?: Partial<RestockingProductRequest>) {
    Object.assign(this, init);
  }
}
