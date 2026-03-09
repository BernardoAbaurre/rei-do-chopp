import { Component, Input, OnInit } from '@angular/core';
import { PaginationResponse } from '../../../../../shared/models/responses/pagination.response';
import { RestockingsService } from '../../../../../restockings/services/restockings.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs';
import { CommonModule } from '@angular/common';
import { RestockingAdditionalFeeResponse } from '../../../../../restockings/models/responses/restocking-additional-fee-response';
import { RestockingAdditionalFeeListRequest } from '../../../../../restockings/models/requests/restocking-additional-fee-list.request';

@Component({
  selector: 'app-restocking-additional-fees-details',
  imports: [CommonModule],
  templateUrl: './restocking-additional-fees-details.component.html',
  styles: ":host{display: contents;}"
})
export class RestockingAdditionalFeesDetailsComponent implements OnInit {
  @Input() restockingId: number;

  response = new PaginationResponse<RestockingAdditionalFeeResponse>();

  ngOnInit(): void {
    this.list();
  }

  constructor(
    private restockingsService: RestockingsService,
    private spinner: NgxSpinnerService
  ) { }

  public list() {
    this.spinner.show();

    this.restockingsService.listRestockingAdditionalFees(new RestockingAdditionalFeeListRequest({ RestockingId: this.restockingId }))
      .pipe(finalize(() => { this.spinner.hide() }))
      .subscribe({
        next: (response) => {
          this.response = response;
        }
      });
  }
}
