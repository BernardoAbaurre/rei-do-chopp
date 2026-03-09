export class UserRolesRequest {
  RoleIds: number[];

  constructor(params: Partial<UserRolesRequest> = {}) {
    this.RoleIds = params.RoleIds ?? [];
  }
}
