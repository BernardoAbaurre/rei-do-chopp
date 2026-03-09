import { Directive, ElementRef, OnInit } from '@angular/core';
import { ControlContainer, FormGroup } from '@angular/forms';

@Directive({
  selector: '[appFormControl]'
})
export class FormControlDirective implements OnInit{

  constructor(private el: ElementRef<HTMLInputElement>, private controlContainer: ControlContainer) {}

  ngOnInit(): void {
    this.iniciar();
  }

  private iniciar()
  {
    const control = this.controlContainer.control.get(this.el.nativeElement.getAttribute('formControlName'));

    this.updateClass(control);

    control.statusChanges?.subscribe(() => this.updateClass(control))
    control.valueChanges?.subscribe(() => this.updateClass(control));
    this.el.nativeElement.addEventListener('blur', () => {
      this.updateClass(control);
    });
  }

  private updateClass(control: any)
  {
    if(control.invalid && control.touched)
    {
      this.el.nativeElement.classList.add('is-invalid');
    }
    else
    {
      this.el.nativeElement.classList.remove('is-invalid');
    }
  }
}
