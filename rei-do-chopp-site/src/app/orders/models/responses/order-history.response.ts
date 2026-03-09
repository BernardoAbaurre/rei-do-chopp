export class OrderHistoryResponse {
  Id: number;
  Cashier: string;
  Attendant: string;
  OrderDate: Date;
  ExpectedProductsSumValue: number;
  RealProductsSumValue: number;
  Discount?: number;
  TotalFees?: number;
  TotalValue: number;
}
