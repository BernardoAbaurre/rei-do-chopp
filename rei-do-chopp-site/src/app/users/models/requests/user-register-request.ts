export class UserRegisterRequest {
  Email: string;
  FirstName: string;
  LastName: string;
  Password: string;
  CheckPassword: string;
  RoleIds;

  constructor(params: Partial<UserRegisterRequest> = {}) {
    this.Email = params.Email ?? '';
    this.FirstName = params.FirstName ?? '';
    this.LastName = params.LastName ?? '';
    this.Password = params.Password ?? '';
    this.CheckPassword = params.CheckPassword ?? '';
    this.RoleIds = params.RoleIds || [];
  }
}
