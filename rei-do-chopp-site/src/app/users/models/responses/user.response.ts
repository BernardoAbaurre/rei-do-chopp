import { RoleResponse } from "../../../roles/models/responses/role.response";

export class UserResponse {
  Id: number;
  FullName: string;
  FirstName: string;
  LastName: string;
  Email: string;
  CreationDate: Date;
  Roles: RoleResponse[];
  Active: boolean;
}
