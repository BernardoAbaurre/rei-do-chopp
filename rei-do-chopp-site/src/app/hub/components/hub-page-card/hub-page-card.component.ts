import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-hub-page-card',
  imports: [],
  templateUrl: './hub-page-card.component.html',
  styleUrl: './hub-page-card.component.css'
})
export class HubPageCardComponent {
  @Input() title: string;
  @Input() description: string;
  @Input() icon: string;
  @Input() link: string;
}

