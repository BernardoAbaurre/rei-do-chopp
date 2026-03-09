import { Component, Input, OnInit } from '@angular/core';
import { PaginationResponse } from '../../../../../shared/models/responses/pagination.response';
import { RestockingProductHistoryResponse } from '../../../../../restockings/models/responses/restocking-product-history.response';
import { RestockingsService } from '../../../../../restockings/services/restockings.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { RestockingProductListRequest } from '../../../../../restockings/models/requests/restocking-product-list.request';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-restocking-products-details',
  imports: [CommonModule],
  templateUrl: './restocking-products-details.component.html',
  styles: ":host{display: contents;}"
})
export class RestockingProductsDetailsComponent implements OnInit {
  @Input() restockingId: number;

  response = new PaginationResponse<RestockingProductHistoryResponse>();

  ngOnInit(): void {
    this.list();
  }

  constructor(
    private restockingsService: RestockingsService,
    private spinner: NgxSpinnerService
  ) { }

  public list() {
    this.spinner.show();

    this.restockingsService.listRestockingProducts(new RestockingProductListRequest({ RestockingId: this.restockingId }))
      .pipe(finalize(() => { this.spinner.hide() }))
      .subscribe({
        next: (response) => {
          this.response = response;
        }
      });
  }
}
