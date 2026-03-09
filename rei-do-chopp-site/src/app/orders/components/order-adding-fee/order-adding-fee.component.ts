import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgxMaskDirective } from 'ngx-mask';
import { NgxSpinnerService } from 'ngx-spinner';
import { ProductsService } from '../../../products/services/products.service';
import { FormControlDirective } from '../../../shared/directives/form-control.directive';

@Component({
  selector: 'app-order-adding-fee',
  imports: [
    CommonModule,
    NgSelectModule,
    ReactiveFormsModule,
    FormsModule,
    FormControlDirective,
    NgxMaskDirective
  ],
  templateUrl: './order-adding-fee.component.html',
})
export class OrderAddingFeeComponent {
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
