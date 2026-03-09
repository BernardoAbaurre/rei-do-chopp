import { AfterViewInit, Component, forwardRef, Injector, Input, OnInit, Optional, Self } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbstractControl, FormControl, FormsModule, NgControl } from '@angular/forms';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';
import { NgxMaskDirective } from 'ngx-mask';

@Component({
  selector: 'app-integer-input',
  standalone: true,
  imports: [CommonModule, FormsModule, NgxMaskDirective],
  templateUrl: './integer-input.component.html',
  styleUrl: './integer-input.component.css',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => IntegerInputComponent),
      multi: true
    }
  ]
})
export class IntegerInputComponent implements ControlValueAccessor, OnInit{
  value: number = 0;
  @Input() maxValue?: number;
  @Input() minValue?: number = 0;
  touched: boolean;
  invalid: boolean

  constructor(private injector: Injector){
  }

  ngOnInit(): void {
  }

  onChange = (val: any) => {};
  onTouched = () => {};

  writeValue(value: number): void {
    this.value = Number.isInteger(value) ? value : 0;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    // opcional: implementar se quiser controlar o estado "disabled"
  }

  incrementar() {
    this.value++;
    this.notificarMudanca();
  }

  decrementar() {
    this.value--;
    this.notificarMudanca();
  }

  onInputChange(event: number) {
    this.value = event;
    this.notificarMudanca();
  }

  private notificarMudanca() {
    const control = this.injector.get(NgControl, null)?.control;

    control?.markAsTouched();

    this.onChange(this.value);
    this.onTouched();

    this.touched = true;
    this.invalid = control.invalid;

  }
}
