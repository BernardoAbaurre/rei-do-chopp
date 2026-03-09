import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-active-badge',
  imports: [CommonModule],
  templateUrl: './active-badge.component.html'
})
export class ActiveBadgeComponent {
  @Input() active: boolean;
}
