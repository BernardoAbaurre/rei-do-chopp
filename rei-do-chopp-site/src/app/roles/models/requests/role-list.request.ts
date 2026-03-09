import { PaginationRequest } from "../../../shared/models/requests/pagination.request";

export class RoleListRequest extends PaginationRequest
{
  Id: number;
  Name: string;

  constructor(params: Partial<RoleListRequest>)
  {
    super(params);
    this.Id = params.Id || null;
    this.Name = params.Name || '';
  }
}
