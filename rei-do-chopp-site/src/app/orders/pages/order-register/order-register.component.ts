import { ProductsListRequest } from './../../../products/models/requests/products-list.request';
import { OrderTotalsCalculationResponse } from './../../models/responses/order-totals-calculation.response';
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
import { OrderAddingProductComponent } from "../../components/order-adding-product/order-adding-product.component";
import { NgxDatesPickerModule } from 'ngx-dates-picker';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { DatePickerInputComponent } from "../../../shared/components/inputs/date-picker/date-picker.component";
import { ProductRegisterModalComponent } from '../../../products/components/product-register-modal/product-register-modal.component';
import { BsModalService } from 'ngx-bootstrap/modal';
import { OrderAddingFeeComponent } from "../../components/order-adding-fee/order-adding-fee.component";
import { ActivatedRoute, Router } from '@angular/router';
import { OrdersService } from '../../services/orders.service';
import { ToastrService } from 'ngx-toastr';
import { combineLatest, debounceTime, finalize, startWith } from 'rxjs';
import { OrderResponse } from '../../models/responses/order-response';
import { ORDER_PRODUCT_FORM_CONFIG } from '../../forms/order-product.form';
import { ORDER_ADDITIONAL_FEE_FORM_CONFIG } from '../../forms/order-additional-fee.form';
import { OrderRequest } from '../../models/requests/order.request';
import { RestockingAddingProductComponent } from "../../../restockings/components/restocking-adding-product/restocking-adding-product.component";
import { UsersSelectComponent } from "../../../users/components/user-select/user-select.component";
import { UsersService } from '../../../users/services/users.service';
import { ProductResponse } from '../../../products/models/responses/product.response';
import { OrderProductRequest } from '../../models/requests/order-product.request';
import Swal from 'sweetalert2';
import { BarcodeListenerDirective } from '../../../shared/directives/bar-code-listener.directive';

@Component({
  selector: 'app-order-register',
  standalone: true,
  imports: [
    CommonModule,
    NgSelectModule,
    ReactiveFormsModule,
    FormsModule,
    ProductsSelectComponent,
    IntegerInputComponent,
    FormControlDirective,
    FormInvalidMessageComponent,
    NgxMaskDirective,
    BsDatepickerModule,
    DatePickerInputComponent,
    OrderAddingProductComponent,
    OrderAddingFeeComponent,
    UsersSelectComponent,
    BarcodeListenerDirective
],
  templateUrl: './order-register.component.html',
})
export class OrderRegisterComponent implements OnInit {
  @ViewChild('productInput') productInputRef: ElementRef<HTMLInputElement>;
  @ViewChild(OrderAddingProductComponent) productInputComponent: OrderAddingProductComponent;

  order: OrderResponse
  form: FormGroup;

  orderRequest: OrderRequest;
  totals = new OrderTotalsCalculationResponse();

  constructor(
    private formBuilder: FormBuilder,
    private spinner: NgxSpinnerService,
    private bsModalService: BsModalService,
    private router: Router,
    private ordersService: OrdersService,
    private toastr: ToastrService,
    private activatedRoute: ActivatedRoute,
    private usersService: UsersService,
    private productsService: ProductsService
  ){}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      'AttendantId': [,[Validators.required]],
      'OrderDate': [new Date(), [Validators.required]],
      'Discount': [],
      'OrderProducts': this.formBuilder.array([]),
      'OrderAdditionalFees': this.formBuilder.array([])
    })


    const orderId = this.activatedRoute.snapshot.params['orderId']

    if(orderId != null)
      {
        this.getOrder(orderId);
      }
      else
      {
        this.getCurrentUser();
      }

      combineLatest([
        this.form.get('OrderProducts').valueChanges.pipe(startWith(this.form.get('OrderProducts').value)),
        this.form.get('OrderAdditionalFees').valueChanges.pipe(startWith(this.form.get('OrderAdditionalFees').value)),
        this.form.get('Discount').valueChanges.pipe(startWith(this.form.get('Discount').value)),
      ])
      .pipe(debounceTime(300))
      .subscribe(() => {
        if(this.form.valid)
          this.calculateTotals();
      });
  }

  get products(): FormArray {
    return this.form.get('OrderProducts') as FormArray;
  }

  get additionalFees(): FormArray {
    return this.form.get('OrderAdditionalFees') as FormArray;
  }

  public openProductRegisterModal()
    {
      this.bsModalService.show(ProductRegisterModalComponent, {
        class: 'modal-xl'
      });
    }

  public addProduct(product: any)
  {
    const productForm = this.formBuilder.group(ORDER_PRODUCT_FORM_CONFIG)

    this.products.insert(0, productForm);

    this.products.at(0).patchValue(product);
  }

  public addAdditionalFee(additionalFee: any)
  {
    const additionalFeeForm = this.formBuilder.group(ORDER_ADDITIONAL_FEE_FORM_CONFIG)

    this.additionalFees.push(additionalFeeForm);

    this.additionalFees.at(this.additionalFees.length - 1).patchValue(additionalFee);
  }

  public removeProduct(index: number)
  {
    this.products.removeAt(index);
  }

  public removeAdditionalFee(index: number)
  {
    this.additionalFees.removeAt(index);
  }
  public cancel()
  {
    this.router.navigateByUrl('/hub')
  }

  private setOrderRequest()
  {
    const orderProductRequests = this.form.value.OrderProducts.map(rp => new OrderProductRequest({...rp, ProductId: rp.Product.Id, UnitPrice: rp.Product.SellingPrice}));

    this.orderRequest = new OrderRequest({...this.form.value, OrderProducts: orderProductRequests, Discount: this.form.value.Discount});
  }

  public save()
  {
    this.setOrderRequest();

    this.spinner.show();

    if(this.order != null)
    {
      this.ordersService.edit(this.order.Id, this.orderRequest)
      .pipe(finalize(() => {this.spinner.hide()}))
      .subscribe({
        next: response => {
          this.order = response;
          this.toastr.success("Venda editada com sucesso!", "Sucesso");
          this.print();
        }
      });
    }
    else
    {
      this.ordersService.insert(this.orderRequest)
      .pipe(finalize(() => {this.spinner.hide()}))
      .subscribe({
        next: response => {
          this.order = response;
          this.toastr.success("Venda cadastrada com sucesso!", "Sucesso");
          this.print();
        }
      });
    }
  }

  private resetScreen()
  {
    this.router.navigateByUrl('/hub', { skipLocationChange: true }).then(() => {
      this.router.navigateByUrl("/vendas");
    });
  }

  private getOrder(id: number)
  {
    this.spinner.show();
    this.ordersService.get(id)
    .pipe(finalize(() => {this.spinner.hide()}))
    .subscribe({
      next: response => {
        this.order = response;
        this.form.patchValue(this.order)

        this.order.OrderProducts.forEach(rp => {
          this.products.push(this.formBuilder.group(ORDER_PRODUCT_FORM_CONFIG));
          this.products.at(this.products.length - 1).patchValue(rp);
        })

        this.order.OrderAdditionalFees.forEach(af => {
          this.additionalFees.push(this.formBuilder.group(ORDER_ADDITIONAL_FEE_FORM_CONFIG));
          this.additionalFees.at(this.additionalFees.length - 1).patchValue(af);
        })

      }
    });
  }

  private getCurrentUser()
  {
    this.spinner.show();
    this.usersService.getCurrentUser()
    .pipe(finalize(() => {this.spinner.hide()}))
    .subscribe({
      next: (response) => {
        this.form.get('AttendantId').setValue(response.Id);
      }
    });
  }

  public selectProduct(index: number, product: ProductResponse)
  {
    this.products.at(index).get('UnitPriceCharged').setValue(product.SellingPrice);
  }

  public calculateTotals()
  {
    this.setOrderRequest();
    this.ordersService.calculateTotals(this.orderRequest)
    .subscribe({
      next: (response) => {
        this.totals = response;
      }
    });
  }

  public askPrint(text: string)
  {
    Swal.fire({
      title: text,
      text: "Deseja emitir recibo?",
      icon: 'success',
      showCancelButton: true,
      confirmButtonText: 'Sim',
      cancelButtonText: 'Não',
      allowEscapeKey: false,
      allowOutsideClick: false,
    }).then(response => {
      response.isConfirmed ? this.print() : this.finalize();
    })
  }

  private print()
  {
    this.spinner.show();

    this.ordersService.print(this.order.Id)
    .pipe(finalize(() => {this.spinner.hide()}))
    .subscribe({
      next: (response) => {
        if (!response)
        {
          this.toastr.error("Tente novamente", "Erro ao imprimir recibo")
        }
        else
        {
          this.resetScreen();
        }
      }
    });
  }

  private finalize()
  {
    this.spinner.show();
    this.productsService.list(new ProductsListRequest({Alert: true, PageSize: 10}))
    .pipe(finalize(() => {this.spinner.hide()}))
    .subscribe({
      next: (response) => {
        if(response.Total <= 0)
        {
          this.router.navigateByUrl('/hub');
          return;
        }


        const text = `<b>Os seguintes produtos estão acabando:</b><br> ${response.Data.map(x => x.Description).join(',<br>')}${response.Total > 10 ? '<br>+' + (response.Total - 10) : ''}. <br><b>Deseja vizualizar produtos?</b>`

        Swal.fire({
          title: "Produtos Acabando",
          html: text,
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Sim',
          cancelButtonText: 'Não',
          allowEscapeKey: false,
          allowOutsideClick: false,
        }).then(response => {
          response.isConfirmed ? this.router.navigateByUrl('/produtos') : this.router.navigateByUrl('/hub');
        })
      }
    });
  }

  public OnScan(event: any)
  {
    this.productInputComponent.scannedProduct(event);
  }
}
