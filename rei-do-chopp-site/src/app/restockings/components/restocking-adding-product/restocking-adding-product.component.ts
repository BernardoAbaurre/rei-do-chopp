import { CommonModule } from '@angular/common';
import { Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgSelectComponent, NgSelectModule } from '@ng-select/ng-select';
import { NgxMaskDirective } from 'ngx-mask';
import { NgxSpinnerService } from 'ngx-spinner';
import { ProductsSelectComponent } from '../../../products/components/products-select/products-select.component';
import { ProductsService } from '../../../products/services/products.service';
import { FormInvalidMessageComponent } from '../../../shared/components/form-invalid-message/form-invalid-message.component';
import { IntegerInputComponent } from '../../../shared/components/inputs/integer-input/integer-input.component';
import { FormControlDirective } from '../../../shared/directives/form-control.directive';
import { ProductResponse } from '../../../products/models/responses/product.response';
import { RESTOCKING_PRODUCT_FORM_CONFIG } from '../../forms/restocking-product.form';
import { ProductsListRequest } from '../../../products/models/requests/products-list.request';
import { debounceTime, map, Subject } from 'rxjs';
import { OrdenationTypeEnum } from '../../../shared/enums/ordenation-type.enum';

@Component({
  selector: 'app-restocking-adding-product',
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
  templateUrl: './restocking-adding-product.component.html',
})
export class RestockingAddingProductComponent {
  @Output('add') addEmitter = new EventEmitter();
  @ViewChild('productSelect') productSelectRef: NgSelectComponent;
  addingForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private productsService: ProductsService, spinner: NgxSpinnerService) { }
  ngAfterViewInit(): void {

  }

  ngOnInit(): void {
    this.addingForm = this.formBuilder.group(RESTOCKING_PRODUCT_FORM_CONFIG)

    this.searchProduct$.pipe(debounceTime(400))
      .subscribe(term => this.listProducts(term));

    this.listProducts();
  }

  public addProduct() {
    this.addEmitter.emit(this.addingForm.value);
    this.addingForm.reset();
  }

  public selectProduct() {
    this.listProducts();
    this.addProduct();
  }

  // product select
  loadingProducts = false;
  searchProduct$ = new Subject<string>();
  productsRequest = new ProductsListRequest({});
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

  selectFilter(term: string, item: ProductResponse): boolean {
    return item.BarCode == term || item.Description.normalize("NFD").replace(/[\u0300-\u036f]/g, "").toLowerCase().includes(term.normalize("NFD").replace(/[\u0300-\u036f]/g, "").toLowerCase())
  }

  public scannedProduct(barcode: string) {
    this.loadingProducts = true;
    this.productsRequest = new ProductsListRequest({ DescriptionOrBarCode: barcode, OrdenationField: 'Description', OrdenationType: OrdenationTypeEnum.Ascendant, Active: true });
    this.productsService.list(this.productsRequest)
      .subscribe(response => {
        this.productSelectRef?.blur();
        this.loadingProducts = false;
        this.addingForm.get("Product").setValue(response.Data[0]);
        this.selectProduct()
      })
  }
}
