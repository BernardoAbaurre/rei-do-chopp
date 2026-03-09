import { Component, Input, OnInit } from '@angular/core';
import { PaginationResponse } from '../../../../../shared/models/responses/pagination.response';
import { OrdersService } from '../../../../../orders/services/orders.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { OrderAdditionalFeeResponse } from '../../../../../orders/models/responses/order-additional-fee-response';
import { OrderAdditionalFeeListRequest } from '../../../../../orders/models/requests/order-additional-fee-list.request';

@Component({
  selector: 'app-order-additional-fees-details',
  imports: [CommonModule],
  templateUrl: './order-additional-fees-details.component.html',
  styles: ":host{display: contents;}"
})
export class OrderAdditionalFeesDetailsComponent implements OnInit {
  @Input() orderId: number;

  response = new PaginationResponse<OrderAdditionalFeeResponse>();

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

    this.ordersService.listOrderAdditionalFees(new OrderAdditionalFeeListRequest({OrderId: this.orderId}))
    .pipe(finalize(() => {this.spinner.hide()}))
    .subscribe({
      next: (response) => {
        this.response = response;
      }
    });
  }
}
