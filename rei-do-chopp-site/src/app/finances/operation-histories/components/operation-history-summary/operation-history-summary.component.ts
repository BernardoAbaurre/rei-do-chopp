import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { OperationHistoryResponse } from '../../models/responses/operatin-history.response';

@Component({
  selector: 'app-operation-history-summary',
  imports: [CommonModule],
  templateUrl: './operation-history-summary.component.html'
})
export class OperationHistorySummaryComponent {
  @Input() operationHistoryResponse: OperationHistoryResponse;
}
