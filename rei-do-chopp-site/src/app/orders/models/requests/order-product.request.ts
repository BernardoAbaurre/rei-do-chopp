export class OrderProductRequest {
  ProductId!: number;
  Quantity!: number;
  UnitPriceCharged!: number;
  UnitPrice!: number;

  constructor(init?: Partial<OrderProductRequest>) {
    Object.assign(this, init);
  }
}
