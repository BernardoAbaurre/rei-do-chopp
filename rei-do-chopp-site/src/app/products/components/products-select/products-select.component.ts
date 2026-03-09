import { AfterViewInit, Component, ElementRef, EventEmitter, forwardRef, Injector, Input, OnInit, Output, ViewChild } from '@angular/core';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR, NgControl, ReactiveFormsModule } from '@angular/forms';
import { ProductsService } from '../../services/products.service';
import { ProductsListRequest } from '../../models/requests/products-list.request';
import { ProductResponse } from '../../models/responses/product.response';
import { CommonModule } from '@angular/common';
import { NgSelectComponent, NgSelectModule } from '@ng-select/ng-select';
import { debounceTime, map, Subject } from 'rxjs';

@Component({
  selector: 'app-products-select',
  imports: [CommonModule, NgSelectModule, FormsModule],
  templateUrl: './products-select.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ProductsSelectComponent),
      multi: true
    }
  ]
})
export class ProductsSelectComponent implements ControlValueAccessor, OnInit {
  @ViewChild('productSelect') productSelectRef: NgSelectComponent;
  @Output('change') changeEmmiter = new EventEmitter<ProductResponse>();

  value: ProductResponse = null;
  onChange: any = () => { };
  onTouched: any = () => { };
  loading = false;

  search$ = new Subject<string>();

  request = new ProductsListRequest({});
  items: ProductResponse[];

  constructor(private porductsService: ProductsService, private injector: Injector) { }
  ngOnInit(): void {
    this.search$.pipe(debounceTime(400))
      .subscribe(term => this.list(term));
    setTimeout(() => { this.list(null, this.value?.Id); })
  }

  public list(term?: string, id?: number) {
    this.loading = true;
    this.request = new ProductsListRequest({ Id: id, DescriptionOrBarCode: term });
    this.porductsService.list(this.request)
      .pipe(
        map(response => {
          return {
            ...response,
            Data: response.Data.map(item => ({...item, Description: item.Description + (item.Active ? "" : " (Inativo)")}))
          }
        })
      )
      .subscribe(response => {
        this.loading = false;
        this.items = response.Data;
      })
  }

  selectFilter(term: string, item: ProductResponse): boolean {
    return item.BarCode == term || item.Description.toUpperCase().includes(term.toUpperCase())
  }

  writeValue(obj: any): void {
    this.value = obj;
  }

  getfocused() {
    this.productSelectRef?.focus();
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  };

  emmitChange(product: ProductResponse) {
    this.changeEmmiter.emit(product);
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {

  }

  updateValue(product: ProductResponse) {
    this.value = product;
    if (!product) {
      this.list();
    }
    this.changeEmmiter.emit(product);
    this.onChange(product);
    this.onTouched();

  }
}
