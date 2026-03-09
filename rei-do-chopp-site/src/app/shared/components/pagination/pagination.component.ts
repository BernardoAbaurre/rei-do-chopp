import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-pagination',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pagination.component.html'
})
export class PaginationComponent {
  @Input() request!: { Page: number; PageSize: number; OrdenationField?: string; OrdenationType?: any };
  @Input() response!: { Total: number; Page: number; PageCount: number; Data: any[] };

  @Output() pageChanged = new EventEmitter<typeof this.request>();

  pageSizes = [10, 20, 50, 100];

  setPageSize(size: number) {
    this.request.PageSize = size;
    this.request.Page = 1;
    this.pageChanged.emit(this.request);
  }

  goToPage(page: number) {
    if (page >= 1 && page <= this.response.PageCount) {
      this.request.Page = page;
      this.pageChanged.emit(this.request);
    }
  }

  nextPage() {
    if (this.request.Page < this.response.PageCount) {
      this.request.Page++;
      this.pageChanged.emit(this.request);
    }
  }

  previousPage() {
    if (this.request.Page > 1) {
      this.request.Page--;
      this.pageChanged.emit(this.request);
    }
  }

  get pages(): (number | string)[] {
    const total = this.response.PageCount;
    const current = this.request.Page;
    const delta = 2; // quantas páginas mostrar de cada lado da atual
    const range: (number | string)[] = [];

    const left = Math.max(2, current - delta);
    const right = Math.min(total - 1, current + delta);

    range.push(1); // primeira página sempre

    if (left > 2) {
      range.push('...');
    }

    for (let i = left; i <= right; i++) {
      range.push(i);
    }

    if (right < total - 1) {
      range.push('...');
    }

    if (total > 1) {
      range.push(total); // última página sempre
    }

    return range;
  }

}