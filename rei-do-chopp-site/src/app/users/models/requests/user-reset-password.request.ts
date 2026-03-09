export class UserResetPasswordRequest {
  Email: string;
  Token: string;
  Password: string;
  CheckPassword: string;

  constructor(params: Partial<UserResetPasswordRequest> = {}) {
    this.Email = params.Email ?? "";
    this.Token = params.Token ?? '';
    this.Password = params.Password ?? '';
    this.CheckPassword = params.CheckPassword ?? '';
  }
}
