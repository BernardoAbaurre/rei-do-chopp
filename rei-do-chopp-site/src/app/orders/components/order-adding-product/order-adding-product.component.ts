import { CommonModule } from '@angular/common';
import { Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgSelectComponent, NgSelectModule } from '@ng-select/ng-select';
import { NgxMaskDirective } from 'ngx-mask';
import { NgxSpinnerService } from 'ngx-spinner';
import { Subject, debounceTime } from 'rxjs';
import { ProductsSelectComponent } from '../../../products/components/products-select/products-select.component';
import { ProductsListRequest } from '../../../products/models/requests/products-list.request';
import { ProductResponse } from '../../../products/models/responses/product.response';
import { ProductsService } from '../../../products/services/products.service';
import { FormInvalidMessageComponent } from '../../../shared/components/form-invalid-message/form-invalid-message.component';
import { IntegerInputComponent } from '../../../shared/components/inputs/integer-input/integer-input.component';
import { FormControlDirective } from '../../../shared/directives/form-control.directive';
import { OrdenationTypeEnum } from '../../../shared/enums/ordenation-type.enum';
import { ORDER_PRODUCT_FORM_CONFIG } from './../../forms/order-product.form';

@Component({
  selector: 'app-order-adding-product',
  standalone: true,
  imports: [
    CommonModule,
    NgSelectModule,
    ReactiveFormsModule,
    FormsModule,
    IntegerInputComponent,
    FormControlDirective,
    FormInvalidMessageComponent,
    NgxMaskDirective
  ],
  templateUrl: './order-adding-product.component.html',
})
export class OrderAddingProductComponent {
  @ViewChild('productInput') productInputRef: ElementRef<HTMLElement>;;
  @Output('add') addEmitter = new EventEmitter();
  addingForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private productsService: ProductsService, spinner: NgxSpinnerService) { }
  ngAfterViewInit(): void {

  }

  ngOnInit(): void {
    this.addingForm = this.formBuilder.group(ORDER_PRODUCT_FORM_CONFIG)
    this.listProducts();

    this.searchProduct$.pipe(debounceTime(400)).subscribe(term => this.listProducts(term));
  }

  public addProduct() {
    this.addEmitter.emit(this.addingForm.value);
    this.addingForm.reset();
  }


  // product select
  @ViewChild('productSelect') productSelectRef: NgSelectComponent;
  loadingProducts = false;
  searchProduct$ = new Subject<string>();
  productsRequest = new ProductsListRequest({Active: true});
  products: ProductResponse[];

  public listProducts(term?: string, id?: number) {
    this.loadingProducts = true;
    this.productsRequest = new ProductsListRequest({ Id: id, DescriptionOrBarCode: term, OrdenationField: 'Description', OrdenationType: OrdenationTypeEnum.Ascendant, Active: true });
    this.productsService.list(this.productsRequest)
      .subscribe(response => {
        this.loadingProducts = false;
        this.products = response.Data;
      })
  }

  public selectProduct(product: ProductResponse) {
    this.addingForm.get('UnitPriceCharged').setValue(product.SellingPrice);
    this.addingForm.get('Quantity').setValue(1);
    this.listProducts();
    this.addProduct();
    this.productSelectRef?.blur();
  }

  selectFilter(term: string, item: ProductResponse): boolean {
    return item.BarCode == term || item.Description.normalize("NFD").replace(/[\u0300-\u036f]/g, "").toLowerCase().includes(term.normalize("NFD").replace(/[\u0300-\u036f]/g, "").toLowerCase())
  }

  public scannedProduct(barcode: string) {
    this.loadingProducts = true;
    this.productsRequest = new ProductsListRequest({ DescriptionOrBarCode: barcode, OrdenationField: 'Description', OrdenationType: OrdenationTypeEnum.Ascendant });
    this.productsService.list(this.productsRequest)
      .subscribe(response => {
        this.productSelectRef?.blur();
        this.loadingProducts = false;
        this.addingForm.get("Product").setValue(response.Data[0]);
        this.selectProduct(response.Data[0])
      })
  }
}
