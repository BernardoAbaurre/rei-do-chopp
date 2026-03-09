import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function PasswordMatchValidator(passwordField: string, checkPasswordField: string): ValidatorFn {
  return (group: AbstractControl): ValidationErrors | null => {
    const password = group.get(passwordField)?.value;
    const checkPassword = group.get(checkPasswordField)?.value;

    if (password !== checkPassword) {
      group.get(checkPasswordField)?.setErrors({ passwordsMismatch: 'As senhas são diferentes.' });
      return { passwordsMismatch: true };
    } else {
      const errors = group.get(checkPasswordField)?.errors;
      if (errors) {
        delete errors['passwordsMismatch'];
        if (Object.keys(errors).length === 0) {
          group.get(checkPasswordField)?.setErrors(null);
        }
      }
      return null;
    }
  };
}
