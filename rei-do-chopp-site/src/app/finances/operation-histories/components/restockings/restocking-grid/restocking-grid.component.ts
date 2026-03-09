import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { debounceTime, finalize } from 'rxjs';
import Swal from 'sweetalert2';
import { RestockingListRequest } from '../../../../../restockings/models/requests/restocking-list.request';
import { RestockingHistoryResponse } from '../../../../../restockings/models/responses/restocking-history.response';
import { RestockingsService } from '../../../../../restockings/services/restockings.service';
import { PaginationComponent } from "../../../../../shared/components/pagination/pagination.component";
import { SortButtonComponent } from "../../../../../shared/components/sort-button/sort-button.component";
import { PaginationResponse } from '../../../../../shared/models/responses/pagination.response';
import { RestockingsDetailsComponent } from "../restocking-details/restocking-details.component";

@Component({
  selector: 'app-restockings-grid',
  imports: [CommonModule, PaginationComponent, SortButtonComponent, RestockingsDetailsComponent],
  templateUrl: './restocking-grid.component.html'
})
export class RestockingsGridComponent implements OnInit {
  @Input() form: FormGroup;
  @Output() refresh = new EventEmitter();
  request: RestockingListRequest;
  response = new PaginationResponse<RestockingHistoryResponse>();

  openDetailsIdexes = [];

  ngOnInit(): void {
    this.request = new RestockingListRequest(this.form.value);
    this.list();

    this.form.valueChanges
      .pipe(debounceTime(500))
      .subscribe(() => {
        this.request = new RestockingListRequest(this.form.value);
        this.list();
      });
  }

  constructor(
    private restockingsService: RestockingsService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) { }

  public list() {
    this.spinner.show();

    this.restockingsService.list(this.request)
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

  public askDelete(restockingId: number)
  {
    Swal.fire({
          title: "",
          text: "Tem certeza que deseja excluir o reabastecimentp Nº " + restockingId,
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Sim',
          cancelButtonText: 'Não',
          allowEscapeKey: false,
          allowOutsideClick: false,
        }).then(response => {
          if(response.isConfirmed)
          {
            this.delete(restockingId);
          }
        })
  }

  public delete(restockingId: number)
  {
    this.spinner.show();
    this.restockingsService.delete(restockingId)
    .pipe(finalize(() => {this.spinner.hide()}))
    .subscribe({
      next: (response) => {
        this.toastr.success(`Reabastecimento Nº ${restockingId} excluído com sucesso`)
        this.refresh.emit();
      }
    });
  }
}
