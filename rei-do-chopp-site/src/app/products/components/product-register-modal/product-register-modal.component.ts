import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, EventEmitter, Input, input, OnInit, Output, ViewChild } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ProductResponse } from '../../models/responses/product.response';
import { ProductsService } from '../../services/products.service';
import { NgxMaskDirective } from 'ngx-mask';
import { ProductsInsertRequest } from '../../models/requests/products-insert.request';
import { InputFocusDirective } from '../../../shared/directives/input-focus.directive';
import { FormControlDirective } from '../../../shared/directives/form-control.directive';
import { FormInvalidMessageComponent } from "../../../shared/components/form-invalid-message/form-invalid-message.component";
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs';
import { IntegerInputComponent } from '../../../shared/components/inputs/integer-input/integer-input.component';

@Component({
  selector: 'app-product-register-modal',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgxMaskDirective,
    InputFocusDirective,
    FormControlDirective,
    FormInvalidMessageComponent,
    IntegerInputComponent
  ],
  templateUrl: './product-register-modal.component.html',
})
export class ProductRegisterModalComponent implements OnInit {

  @Input() product?: ProductResponse
  @Output('save') saveEmitter = new EventEmitter();

  form: FormGroup;

  constructor(private readonly modalRef: BsModalRef, private readonly productService: ProductsService, private readonly formBuilder: FormBuilder, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      'BarCode': ['', [Validators.required]],
      'Description': ['', [Validators.required]],
      'SellingPrice': ['', [Validators.required, Validators.min(0)]],
      'AlertQuantity': [10, [Validators.required]]
    })

    this.form.patchValue(this.product);
  }

  public closeModal() {
    this.modalRef.hide();
  }

  public salvar() {
    this.spinner.show()
    const request = new ProductsInsertRequest({ ...this.form.value, StockQuantity: 0 })

    if (this.product) {
      this.productService.edit(this.product.Id, request)
        .pipe(finalize(() => this.spinner.hide()))
        .subscribe({
          next: () => {
            this.closeModal();
            this.saveEmitter.emit();
          }
        });
    }
    else {
      this.productService.insert(request)
        .pipe(finalize(() => this.spinner.hide()))
        .subscribe({
          next: () => {
            this.closeModal();
            this.saveEmitter.emit();
          }
        });
    }
  }
}
