export class SettingsAuthRequest
{
  Password: string

  constructor(params: Partial<SettingsAuthRequest>)
  {
    this.Password = params.Password || "";
  }
}
