export class UserEditRequest {
  Email: string;
  FirstName: string;
  LastName: string;
  RoleIds: number[];

  constructor(params: Partial<UserEditRequest> = {}) {
    this.Email = params.Email ?? '';
    this.FirstName = params.FirstName ?? '';
    this.LastName = params.LastName ?? '';
    this.RoleIds = params.RoleIds || [];
  }
}
