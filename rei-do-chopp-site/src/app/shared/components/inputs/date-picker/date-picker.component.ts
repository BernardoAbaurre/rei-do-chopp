import {
  ChangeDetectorRef,
  Component,
  forwardRef,
  Input,
  OnInit
} from '@angular/core';
import {
  ControlValueAccessor,
  NG_VALUE_ACCESSOR,
  FormsModule,
  ReactiveFormsModule
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BsDatepickerModule, BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-date-picker-input',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, BsDatepickerModule],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DatePickerInputComponent),
      multi: true
    }
  ],
  templateUrl: './date-picker.component.html',
  styleUrl: './date-picker.component.css'
})
export class DatePickerInputComponent implements ControlValueAccessor, OnInit {
  @Input() placeholderText: string = 'Selecione uma data';
  @Input() time? = true;

  date: Date | null = null;
  hour: number = 0;
  minute: number = 0;

  bsConfig: Partial<BsDatepickerConfig> = {
    containerClass: 'theme-default',
    dateInputFormat: 'DD/MM/YYYY',
    showWeekNumbers: false
  };

  private propagateChange = (_: any) => {};
  private propagateTouched = () => {};

  constructor(private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {}

  writeValue(value: Date): void {
    if (value) {
      value = new Date(value);
      this.date = new Date(value);
      if(this.time)
      {
        this.hour = value.getHours();
        this.minute = value.getMinutes();
      }
      else
      {
        this.hour = 0;
        this.minute = 0;
      }
      this.propagateChange(this.getCombinedDate());
    }
  }

  registerOnChange(fn: any): void {
    this.propagateChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.propagateTouched = fn;
  }

  onDateChange(newDate: Date): void {
    this.date = newDate;
    this.emitValue();
  }

  onHourChange(hour: any): void {
    this.hour = +hour['value'];
    this.emitValue();
  }

  onMinuteChange(minute: any): void {
    this.minute = +minute['value'];
    this.emitValue();
  }

  onBlur(): void {
    this.propagateTouched();
  }

  private emitValue(): void {
    const combined = this.getCombinedDate();
    this.propagateChange(combined);
  }

  private getCombinedDate(): Date | null {
    if (!this.date) return null;

    const combined = new Date(this.date);

    combined.setHours(this.hour);
    combined.setMinutes(this.minute);
    combined.setSeconds(0);
    combined.setMilliseconds(0);

    return new Date(combined);
  }
}
