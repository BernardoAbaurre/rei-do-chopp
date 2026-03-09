import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { OperationHistoriesService } from '../../services/operation-histories.service';
import { debounceTime, finalize, last, Subject } from 'rxjs';
import { OperationHistoryResponse } from '../../models/responses/operatin-history.response';
import { OperationHistoryListRequest } from '../../models/requests/operation-history-list.request';
import { NgxSpinnerService } from 'ngx-spinner';
import { CommonModule } from '@angular/common';
import { DatePickerInputComponent } from "../../../../shared/components/inputs/date-picker/date-picker.component";
import { OperationHistorySummaryComponent } from "../../components/operation-history-summary/operation-history-summary.component";
import { OrdersGridComponent } from "../../components/orders/orders-grid/orders-grid.component";
import { RestockingsGridComponent } from "../../components/restockings/restocking-grid/restocking-grid.component";
import { ProductResponse } from '../../../../products/models/responses/product.response';
import { ProductsService } from '../../../../products/services/products.service';
import { ProductsListRequest } from '../../../../products/models/requests/products-list.request';
import { NgSelectModule } from '@ng-select/ng-select';

@Component({
  selector: 'app-operation-histories-list',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    DatePickerInputComponent,
    OperationHistorySummaryComponent,
    OrdersGridComponent,
    RestockingsGridComponent,
    NgSelectModule
],
  templateUrl: './operation-histories-list.component.html',
  styleUrl: './operation-histories-list.component.css'
})
export class OperationHistoriesListComponent {
  filterForm: FormGroup;
  operationHistoryResponse: OperationHistoryResponse;
  operationHistoryRequest: OperationHistoryListRequest;

  firstDay: Date;
  lastDay: Date;

  constructor(
    private operationHistoriesService: OperationHistoriesService,
    private fb: FormBuilder,
    private spinner: NgxSpinnerService,
    private productsService: ProductsService
  ) { }

  ngOnInit(): void {
    this.initializeForm();
    this.listProducts();
    this.searchProduct$.pipe(debounceTime(400)).subscribe(term => this.listProducts(term));
  }

  private initializeForm() {
    const baseDate = new Date();
    this.firstDay = new Date(baseDate.getFullYear(), baseDate.getMonth(), 1);
    this.lastDay = new Date(baseDate.getFullYear(), baseDate.getMonth() + 1, 0);

    this.filterForm = this.fb.group({
      'InitialDate': [this.firstDay, [Validators.required]],
      'FinalDate': [this.lastDay, [Validators.required]],
      'ProductsIds':[[]]
    });

    this.operationHistoryRequest = new OperationHistoryListRequest({ ...this.filterForm.value });

    this.filterForm.valueChanges
      .pipe(debounceTime(500))
      .subscribe(() => this.filter());
  }

  public filter() {
    this.operationHistoryRequest = new OperationHistoryListRequest(this.filterForm.value);
    this.list();
  }

  public list() {
    this.spinner.show();
    this.operationHistoriesService.list(this.operationHistoryRequest)
      .pipe(finalize(() => { this.spinner.hide() }))
      .subscribe({
        next: (response) => {
          this.operationHistoryResponse = response;
        }
      });
  }

  resetFilter() {
    this.filterForm.reset();
    this.filterForm.patchValue({ InitialDate: this.firstDay, FinalDate: this.lastDay });
  }

  // product select
  loadingProducts = false;
  searchProduct$ = new Subject<string>();
  productsRequest = new ProductsListRequest({});
  products: ProductResponse[];

  public listProducts(term?: string, id?: number) {
    this.loadingProducts = true;
    this.productsRequest = new ProductsListRequest({ Id: id, DescriptionOrBarCode: term });
    this.productsService.list(this.productsRequest)
      .subscribe(response => {
        this.loadingProducts = false;
        this.products = response.Data;
      })
  }

  selectFilter(term: string, item: ProductResponse): boolean {
    return item.BarCode == term || item.Description.normalize("NFD").replace(/[\u0300-\u036f]/g, "").toLowerCase().includes(term.normalize("NFD").replace(/[\u0300-\u036f]/g, "").toLowerCase())
  }
}
