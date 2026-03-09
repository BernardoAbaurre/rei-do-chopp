export class ProductsInsertRequest
{
  BarCode: string;
  Description: string;
  SellingPrice: number;
  StockQuantity: number;
  AlertQuantity: number;

  constructor(params : Partial<ProductsInsertRequest>)
  {
    this.BarCode = params.BarCode ?? "";
    this.Description = params.Description ?? "";
    this.SellingPrice = params.SellingPrice ?? null;
    this.StockQuantity = params.StockQuantity ?? null;
    this.AlertQuantity = params.AlertQuantity ?? null;
  }
}
