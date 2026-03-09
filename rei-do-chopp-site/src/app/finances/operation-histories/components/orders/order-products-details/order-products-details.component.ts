import { Component, Input, OnInit } from '@angular/core';
import { PaginationResponse } from '../../../../../shared/models/responses/pagination.response';
import { OrderProductHistoryResponse } from '../../../../../orders/models/responses/order-product-history.response';
import { OrdersService } from '../../../../../orders/services/orders.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { OrderProductRequest } from '../../../../../orders/models/requests/order-product.request';
import { OrderProductListRequest } from '../../../../../orders/models/requests/order-product-list.request';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-order-products-details',
  imports: [CommonModule],
  templateUrl: './order-products-details.component.html',
  styles: ":host{display: contents;}"
})
export class OrderProductsDetailsComponent implements OnInit {
  @Input() orderId: number;

  response = new PaginationResponse<OrderProductHistoryResponse>();

  ngOnInit(): void {
    this.list();
  }

  constructor(
    private ordersService: OrdersService,
    private spinner: NgxSpinnerService
  ){}

  public list()
  {
    this.spinner.show();

    this.ordersService.listOrderProducts(new OrderProductListRequest({OrderId: this.orderId}))
    .pipe(finalize(() => {this.spinner.hide()}))
    .subscribe({
      next: (response) => {
        this.response = response;
      }
    });
  }
}
