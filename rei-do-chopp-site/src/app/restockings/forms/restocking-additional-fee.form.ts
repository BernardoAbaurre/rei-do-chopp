import { Validators } from "@angular/forms";

export const RESTOCKING_ADDITIONAL_FEE_FORM_CONFIG =
{
  'Description': ['', [Validators.required]],
  'Value': ['', [Validators.required]],
}
