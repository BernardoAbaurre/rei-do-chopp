import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-form-invalid-message',
  imports: [CommonModule],
  templateUrl: './form-invalid-message.component.html',
  host: {
    'class': '',             // remove classes
    'style': 'all: unset;'   // remove todos os estilos herdados do host
  }
})
export class FormInvalidMessageComponent {
  @Input() form!: any;
  @Input() prop!: string;

  get errorMessage(): string | null {
    const control = this.form?.get(this.prop);
    if (!control || !(control.touched || control.dirty) || !control.errors) return null;

    const errors = control.errors;

    if (errors['required']) {
      return 'Este campo é obrigatório.';
    }
    if (errors['minlength']) {
      return `Mínimo de ${errors['minlength'].requiredLength} caracteres.`;
    }
    if (errors['maxlength']) {
      return `Máximo de ${errors['maxlength'].requiredLength} caracteres.`;
    }
    if (errors['min']) {
      return `Valor mínimo permitido: ${errors['min'].min}.`;
    }
    if (errors['max']) {
      return `Valor máximo permitido: ${errors['max'].max}.`;
    }
    if (errors['email']) {
      return 'E-mail inválido.';
    }
    if (errors.Message != '')
    {
      return errors.Message;
    }

    return 'Valor inválido.';
  }
}
