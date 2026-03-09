import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { debounceTime, finalize } from 'rxjs';
import Swal from 'sweetalert2';
import { OrderListRequest } from '../../../../../orders/models/requests/order-list.request';
import { OrderHistoryResponse } from '../../../../../orders/models/responses/order-history.response';
import { OrdersService } from '../../../../../orders/services/orders.service';
import { PaginationComponent } from "../../../../../shared/components/pagination/pagination.component";
import { SortButtonComponent } from "../../../../../shared/components/sort-button/sort-button.component";
import { PaginationResponse } from '../../../../../shared/models/responses/pagination.response';
import { OrdersDetailsComponent } from '../orders-details/orders-details.component';

@Component({
  selector: 'app-orders-grid',
  imports: [CommonModule, SortButtonComponent, PaginationComponent, OrdersDetailsComponent],
  templateUrl: './orders-grid.component.html'
})
export class OrdersGridComponent implements OnInit {
  @Input() form: FormGroup;
  @Output() refresh = new EventEmitter();
  request: OrderListRequest;
  response = new PaginationResponse<OrderHistoryResponse>();

  openDetailsIdexes = [];

  ngOnInit(): void {
    this.request = new OrderListRequest(this.form.value);

    this.form.valueChanges
      .pipe(debounceTime(500))
      .subscribe(() => {
        this.request = new OrderListRequest(this.form.value);
        this.list();
      });

    this.list();
  }

  constructor(
    private ordersService: OrdersService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) { }

  public list() {
    this.spinner.show();

    this.ordersService.list(this.request)
      .pipe(finalize(() => { this.spinner.hide() }))
      .subscribe({
        next: (response) => {
          this.response = response;
          this.openDetailsIdexes = [];
        }
      });
  }

  public toggleDetail(detailIndex: number) {
    const index = this.openDetailsIdexes.indexOf(detailIndex);

    if (index > -1) {
      this.openDetailsIdexes.splice(index, 1);
    }
    else {
      this.openDetailsIdexes.push(detailIndex);
    }
  }

  public print(orderId: number) {
    this.spinner.show();

    this.ordersService.print(orderId)
      .pipe(finalize(() => { this.spinner.hide() }))
      .subscribe({
        next: (response) => {
          if (!response) {
            this.toastr.error("Tente novamente", "Erro ao imprimir recibo")
          }
        }
      });
  }

  public askDelete(orderId: number)
  {
    Swal.fire({
          title: "",
          text: "Tem certeza que deseja excluir a venda Nº " + orderId,
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Sim',
          cancelButtonText: 'Não',
          allowEscapeKey: false,
          allowOutsideClick: false,
        }).then(response => {
          if(response.isConfirmed)
          {
            this.delete(orderId);
          }
        })
  }

  public delete(orderId: number)
  {
    this.spinner.show();
    this.ordersService.delete(orderId)
    .pipe(finalize(() => {this.spinner.hide()}))
    .subscribe({
      next: (response) => {
        this.toastr.success(`Venda Nº ${orderId} excluída com sucesso`)
        this.refresh.emit();
      }
    });
  }

}
