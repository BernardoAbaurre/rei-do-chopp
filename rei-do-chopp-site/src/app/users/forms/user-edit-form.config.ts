import { Validators } from "@angular/forms";

export const USER_EDIT_FORM_CONFIG = {
  'Email': ['', [Validators.required, Validators.email]],
  'RoleIds': [, Validators.required],
  'FirstName': ['', [Validators.required]],
  'LastName': ['', [Validators.required]],
}
