import { Component, EventEmitter, HostBinding, HostListener, Input, Output } from '@angular/core';
import { PaginationRequest } from '../../models/requests/pagination.request';
import { CommonModule } from '@angular/common';
import { OrdenationTypeEnum } from '../../enums/ordenation-type.enum';

@Component({
  selector: 'app-sort-button',
  imports: [CommonModule],
  templateUrl: './sort-button.component.html',
  styles: ':host{cursor: pointer}'
})
export class SortButtonComponent {
  @Output() sort = new EventEmitter();
  @Input() request: PaginationRequest;
  @Input() prop: string;


  ordenationTypes = OrdenationTypeEnum;

  @HostListener('click')
  public orderBy() {
    if (this.request.OrdenationField == this.prop) {
      this.request.OrdenationType = this.request.OrdenationType == OrdenationTypeEnum.Ascendant ? OrdenationTypeEnum.Descendent : OrdenationTypeEnum.Ascendant;
    }
    else {
      this.request.OrdenationType = OrdenationTypeEnum.Ascendant;
      this.request.OrdenationField = this.prop;
    }

    this.sort.emit();
  }
}
