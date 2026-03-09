import { Component, Input } from '@angular/core';
import { RestockingProductsDetailsComponent } from "../restocking-products-details/restocking-products-details.component";
import { RestockingAdditionalFeesDetailsComponent } from "../restocking-additional-fees-details/restocking-additional-fees-details.component";

@Component({
  selector: 'app-restocking-details',
  imports: [RestockingProductsDetailsComponent, RestockingAdditionalFeesDetailsComponent],
  templateUrl: './restocking-details.component.html',
  styleUrl: './restocking-details.component.css'
})
export class RestockingsDetailsComponent {
  @Input() restockingId: number;
}
