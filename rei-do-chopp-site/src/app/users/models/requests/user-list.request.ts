import { PaginationRequest } from "../../../shared/models/requests/pagination.request";

export class UserListRequest extends PaginationRequest
{
  Id: number;
  Name: string;
  Active?: boolean;
  RoleIds: number[];

  constructor(params: Partial<UserListRequest>)
  {
    super(params);
    this.Id = params.Id || null;
    this.Name = params.Name || ""
    this.Active = params.Active ?? null;
    this.RoleIds = params.RoleIds || [];
  }
}
