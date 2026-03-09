export class LoginRequest
{
  Email : string;
  Password: string;

  constructor(params: Partial<LoginRequest>)
  {
    this.Email = params.Email || "";
    this.Password = params.Password || "";
  }
}