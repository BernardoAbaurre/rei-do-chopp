import { NgSelectModule } from '@ng-select/ng-select';
import { Component, HostListener, OnInit } from '@angular/core';
import { ProductsService } from '../../services/products.service';
import { ProductsListRequest } from '../../models/requests/products-list.request';
import { PaginationResponse } from '../../../shared/models/responses/pagination.response';
import { ProductResponse } from '../../models/responses/product.response';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { debounceTime, finalize } from 'rxjs';
import { PaginationComponent } from "../../../shared/components/pagination/pagination.component";
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { ProductRegisterModalComponent } from '../../components/product-register-modal/product-register-modal.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { InputFocusDirective } from '../../../shared/directives/input-focus.directive';
import { ActiveBadgeComponent } from "../../../shared/components/active-badge/active-badge.component";
import { SortButtonComponent } from "../../../shared/components/sort-button/sort-button.component";
import { OrdenationTypeEnum } from '../../../shared/enums/ordenation-type.enum';

@Component({
  selector: 'app-products-list',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PaginationComponent,
    ModalModule,
    InputFocusDirective,
    ActiveBadgeComponent,
    SortButtonComponent,
    NgSelectModule
  ],
  templateUrl: './products-list.component.html',
  styleUrl: './products-list.component.css'
})
export class ProductsListComponent implements OnInit {

  productsRequest = new ProductsListRequest({ Active: true, OrdenationField: 'Alert', OrdenationType: OrdenationTypeEnum.Descendent });
  filterForm: FormGroup;
  response: PaginationResponse<ProductResponse> = new PaginationResponse<ProductResponse>();
  productRegisterModalRef: BsModalRef;

  statusOptions = [
    { Description: 'Ativo', Value: true },
    { Description: 'Inativo', Value: false },
  ];

  alertOptions = [
    { Description: 'Sim', Value: true },
    { Description: 'Não', Value: false },
  ];

  constructor(private productsService: ProductsService, private fb: FormBuilder, private bsModalService: BsModalService, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.filterForm = this.fb.group({
      DescriptionOrBarCode: [''],
      Active: [true],
      Alert: []
    });

    this.filterForm.valueChanges
      .pipe(debounceTime(500))
      .subscribe(() => this.filter())

    this.list();
  }

  public list() {
    this.spinner.show();
    this.productsService.list(this.productsRequest)
      .pipe(finalize(() => this.spinner.hide()))
      .subscribe(response => {
        this.response = response;
      })
  }

  public filter() {
    this.productsRequest = new ProductsListRequest({ ...this.filterForm.value, OrdenationField: 'Alert', OrdenationType: OrdenationTypeEnum.Descendent });
    this.list()
  }

  public openProductRegisterModal(product?: ProductResponse) {
    this.productRegisterModalRef = this.bsModalService.show(ProductRegisterModalComponent, {
      class: 'modal-xl',
      initialState: { product: product }
    });

    this.productRegisterModalRef.content.saveEmitter.subscribe(() => {
      this.resetForm();
    })
  }

  public changeStatus(product: ProductResponse) {
    this.spinner.show();
    this.productsService.changeStatus(product.Id)
      .pipe(finalize(() => this.spinner.hide()))
      .subscribe(() => {
        this.resetForm();
      })
  }

  public resetForm() {
    this.filterForm.reset({ Active: true });
    this.list();
  }
}
