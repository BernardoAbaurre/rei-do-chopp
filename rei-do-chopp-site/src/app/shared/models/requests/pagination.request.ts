import { OrdenationTypeEnum } from "../../enums/ordenation-type.enum";

export class PaginationRequest
{
  Page: number;
  PageSize: number;
  OrdenationField: string;
  OrdenationType: OrdenationTypeEnum

  constructor(params: Partial<PaginationRequest>)
  {
    this.Page = params.Page || 1;
    this.PageSize = params.PageSize || 10;
    this.OrdenationField = params.OrdenationField || "Id";
    this.OrdenationType = params.OrdenationType ?? OrdenationTypeEnum.Descendent;
  }
}
