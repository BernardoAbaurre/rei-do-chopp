import { AbstractControl, ValidationErrors } from "@angular/forms";

export function PasswordValidator(control: AbstractControl): ValidationErrors | null
{

  return null;
  // if(!/\d/.test(control.value || ''))
  // {
  //   return { Message: 'A senha deve conter pelo menos um número.' };
  // }
  // else if (!/[a-z]/.test(control.value || ''))
  // {
  //   return { Message: 'A senha deve conter pelo menos uma letra minúscula.' };
  // }
  // else if (!/[A-Z]/.test(control.value || ''))
  // {
  //   return { Message: 'A senha deve conter pelo menos uma letra maiúscula.' };
  // }
  // else if (!/[^a-zA-Z0-9]/.test(control.value || ''))
  // {
  //   return { Message: 'A senha deve conter pelo menos um caractere especial.' };
  // }
  // else if((control.value || '').length < 8 )
  // {
  //   return { Message: 'A senha deve conter pelo menos 8 carcteres.' };
  // }
  // else
  // {
  //   return null;
  // }
}
