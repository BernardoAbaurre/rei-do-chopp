import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgxMaskDirective } from 'ngx-mask';
import { NgxSpinnerService } from 'ngx-spinner';
import { ProductsSelectComponent } from '../../../products/components/products-select/products-select.component';
import { ProductsService } from '../../../products/services/products.service';
import { FormInvalidMessageComponent } from '../../../shared/components/form-invalid-message/form-invalid-message.component';
import { IntegerInputComponent } from '../../../shared/components/inputs/integer-input/integer-input.component';
import { FormControlDirective } from '../../../shared/directives/form-control.directive';

@Component({
  selector: 'app-restocking-adding-fee',
  imports: [
    CommonModule,
    NgSelectModule,
    ReactiveFormsModule,
    FormsModule,
    FormControlDirective,
    NgxMaskDirective
  ],
  templateUrl: './restocking-adding-fee.component.html',
})
export class RestockingAddingFeeComponent {
  @Output('add') addEmitter = new EventEmitter();
  addingForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private productsService: ProductsService, spinner: NgxSpinnerService){}
  ngAfterViewInit(): void {

  }

  ngOnInit(): void {
    this.addingForm = this.formBuilder.group({
      'Description': ['', [Validators.required]],
      'Value': ['', [Validators.required]],
    })
    
  }

  public addFee()
  {
    this.addEmitter.emit(this.addingForm.value);
    this.addingForm.reset();
  }
}
