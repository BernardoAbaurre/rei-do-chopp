import { Validators } from "@angular/forms";
import { PasswordValidator } from "../validators/password.validator";

export const USER_REGISTER_FORM_CONFIG = {
  'Email': ['', [Validators.required, Validators.email]],
  'RoleIds': [, Validators.required],
  'FirstName': ['', [Validators.required]],
  'LastName': ['', [Validators.required]],
  'Password': ['', [Validators.required, PasswordValidator]],
  'CheckPassword': ['', [Validators.required]],
}
