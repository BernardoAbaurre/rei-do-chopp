import { PaginationRequest } from "../../../shared/models/requests/pagination.request";

export class ProductsListRequest extends PaginationRequest
{
  Id: number;
  BarCode: string;
  Description: string;
  SellingPrice: number;
  StockQuantity: number;
  DescriptionOrBarCode: string;
  Active?: boolean;
  Alert?:boolean;

  constructor(params : Partial<ProductsListRequest>)
  {
    super(params)
    this.Id = params.Id || null;
    this.BarCode = params.BarCode || "";
    this.Description = params.Description || "";
    this.SellingPrice = params.SellingPrice || null;
    this.StockQuantity = params.StockQuantity || null;
    this.DescriptionOrBarCode = params.DescriptionOrBarCode || "";
    this.Active = params.Active ?? null;
    this.Alert = params.Alert ?? null;
  }
}
