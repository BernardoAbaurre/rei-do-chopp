export class UserForgotPasswordRequest
{
  Email: string;

  constructor(params: Partial<UserForgotPasswordRequest>)
  {
    this.Email = params.Email || "";
  }
}
