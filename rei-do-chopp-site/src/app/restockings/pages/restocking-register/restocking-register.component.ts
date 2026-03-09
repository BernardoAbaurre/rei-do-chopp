import { NgxSpinnerService } from 'ngx-spinner';
import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { ProductsService } from '../../../products/services/products.service';
import { ProductsSelectComponent } from "../../../products/components/products-select/products-select.component";
import { IntegerInputComponent } from '../../../shared/components/inputs/integer-input/integer-input.component';
import { FormControlDirective } from '../../../shared/directives/form-control.directive';
import { FormInvalidMessageComponent } from "../../../shared/components/form-invalid-message/form-invalid-message.component";
import { NgxMaskDirective } from 'ngx-mask';
import { RestockingAddingProductComponent } from "../../components/restocking-adding-product/restocking-adding-product.component";
import { NgxDatesPickerModule } from 'ngx-dates-picker';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { DatePickerInputComponent } from "../../../shared/components/inputs/date-picker/date-picker.component";
import { ProductRegisterModalComponent } from '../../../products/components/product-register-modal/product-register-modal.component';
import { BsModalService } from 'ngx-bootstrap/modal';
import { RestockingAddingFeeComponent } from "../../components/restocking-adding-fee/restocking-adding-fee.component";
import { ActivatedRoute, Router } from '@angular/router';
import { RestockingsService } from '../../services/restockings.service';
import { RestockingRequest } from '../../models/requests/restocking-request';
import { ToastrService } from 'ngx-toastr';
import { combineLatest, debounceTime, finalize, startWith } from 'rxjs';
import { RestockingResponse } from '../../models/responses/restocking-response';
import { RESTOCKING_PRODUCT_FORM_CONFIG } from '../../forms/restocking-product.form';
import { RESTOCKING_ADDITIONAL_FEE_FORM_CONFIG } from '../../forms/restocking-additional-fee.form';
import { ProductResponse } from '../../../products/models/responses/product.response';
import { RestockingProductRequest } from '../../models/requests/restocking-product-request';
import { RestockingTotalsCalculationResponse } from '../../models/responses/restocking-totals-calculation.response';
import { BarcodeListenerDirective } from '../../../shared/directives/bar-code-listener.directive';

@Component({
  selector: 'app-restocking-register',
  standalone: true,
  imports: [
    CommonModule,
    NgSelectModule,
    ReactiveFormsModule,
    FormsModule,
    IntegerInputComponent,
    FormControlDirective,
    FormInvalidMessageComponent,
    NgxMaskDirective,
    RestockingAddingProductComponent,
    BsDatepickerModule,
    DatePickerInputComponent,
    RestockingAddingFeeComponent,
    BarcodeListenerDirective
  ],
  templateUrl: './restocking-register.component.html',
})
export class RestockingRegisterComponent implements OnInit {
  @ViewChild('productInput') productInputRef: ElementRef<HTMLInputElement>;

  @ViewChild(RestockingAddingProductComponent) productInputComponent: RestockingAddingProductComponent;

  restocking: RestockingResponse
  restockingRequest: RestockingRequest;
  form: FormGroup;
  selectedProducts: ProductResponse[] = [];
  totals = new RestockingTotalsCalculationResponse();

  constructor(
    private formBuilder: FormBuilder,
    private spinner: NgxSpinnerService,
    private bsModalService: BsModalService,
    private router: Router,
    private restockingsService: RestockingsService,
    private toastr: ToastrService,
    private activatedRoute: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      'Date': [new Date(), [Validators.required]],
      'Discount': [],
      'RestockingProducts': this.formBuilder.array([]),
      'RestockingAdditionalFees': this.formBuilder.array([])
    });

    this.activatedRoute.params.subscribe(params => {
      const restockingId = params['restockingId']
      if (restockingId != null) {
        this.getRestocking(restockingId);
      }
    });

    this.SubscribeInFormChanges();
  }

  private SubscribeInFormChanges() {
    combineLatest([
      this.form.get('RestockingProducts').valueChanges.pipe(startWith(this.form.get('RestockingProducts').value)),
      this.form.get('RestockingAdditionalFees').valueChanges.pipe(startWith(this.form.get('RestockingAdditionalFees').value)),
      this.form.get('Discount').valueChanges.pipe(startWith(this.form.get('Discount').value)),
    ])
      .pipe(debounceTime(300))
      .subscribe(() => {
        if (this.form.valid)
          this.calculateTotals();
      });
  }

  get products(): FormArray {
    return this.form.get('RestockingProducts') as FormArray;
  }

  get additionalFees(): FormArray {
    return this.form.get('RestockingAdditionalFees') as FormArray;
  }

  public openProductRegisterModal() {
    this.bsModalService.show(ProductRegisterModalComponent, {
      class: 'modal-xl'
    });
  }

  public addProduct(restockingProductForm: any) {
    const productForm = this.formBuilder.group(RESTOCKING_PRODUCT_FORM_CONFIG)

    this.products.insert(0, productForm);

    this.products.at(0).patchValue(restockingProductForm);
  }

  public addAdditionalFee(additionalFee: any) {
    const additionalFeeForm = this.formBuilder.group(RESTOCKING_ADDITIONAL_FEE_FORM_CONFIG)

    this.additionalFees.push(additionalFeeForm);

    this.additionalFees.at(this.additionalFees.length - 1).patchValue(additionalFee);
  }

  public removeProduct(index: number) {
    this.products.removeAt(index);
  }

  public removeAdditionalFee(index: number) {
    this.additionalFees.removeAt(index);
  }
  public cancel() {
    this.router.navigateByUrl('/produtos')
  }

  public save() {
    this.setRestockingRequest();
    this.spinner.show();

    if (this.restocking) {
      this.restockingsService.edit(this.restocking.Id, this.restockingRequest)
        .pipe(finalize(() => { this.spinner.hide() }))
        .subscribe({
          next: () => {
            this.toastr.success('Reabastecimento editado com sucesso!')
            this.router.navigateByUrl('/hub');
          }
        });
    }
    else {
      this.restockingsService.insert(this.restockingRequest)
        .pipe(finalize(() => { this.spinner.hide() }))
        .subscribe({
          next: () => {
            this.toastr.success('Reabastecimento registrado com sucesso!')
            this.router.navigateByUrl('/hub');
          }
        });
    }
  }

  private getRestocking(id: number) {
    this.spinner.show();
    this.restockingsService.get(id)
      .pipe(finalize(() => { this.spinner.hide() }))
      .subscribe({
        next: response => {
          this.restocking = response;
          this.form.patchValue(this.restocking)

          this.restocking.RestockingProducts.forEach(rp => {
            this.products.push(this.formBuilder.group(RESTOCKING_PRODUCT_FORM_CONFIG));
            this.products.at(this.products.length - 1).patchValue(rp);
          })

          this.restocking.RestockingAdditionalFees.forEach(af => {
            this.additionalFees.push(this.formBuilder.group(RESTOCKING_ADDITIONAL_FEE_FORM_CONFIG));
            this.additionalFees.at(this.additionalFees.length - 1).patchValue(af);
          })
        }
      });
  }

  private setRestockingRequest() {
    const restockingProductRequests = this.form.value.RestockingProducts.map(rp => new RestockingProductRequest({ ...rp, ProductId: rp.Product.Id }));

    this.restockingRequest = new RestockingRequest({ ...this.form.value, RestockingProducts: restockingProductRequests, Discount: this.form.value.Discount });
  }

  public calculateTotals() {
    this.setRestockingRequest();
    this.restockingsService.calculateTotals(this.restockingRequest)
      .subscribe({
        next: (response) => {
          this.totals = response;
        }
      });
  }

  public OnScan(event: any)
  {
    this.productInputComponent.scannedProduct(event);
  }
}
