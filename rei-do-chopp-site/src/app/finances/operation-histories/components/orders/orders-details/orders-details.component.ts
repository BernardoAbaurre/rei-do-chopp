import { Component, Input } from '@angular/core';
import { OrderProductsDetailsComponent } from "../order-products-details/order-products-details.component";
import { OrderAdditionalFeesDetailsComponent } from "../order-additional-fees-details/order-additional-fees-details.component";

@Component({
  selector: 'app-orders-details',
  imports: [OrderProductsDetailsComponent, OrderAdditionalFeesDetailsComponent],
  templateUrl: './orders-details.component.html',
  styleUrl: './orders-details.component.css'
})
export class OrdersDetailsComponent {
  @Input() orderId: number;
}
