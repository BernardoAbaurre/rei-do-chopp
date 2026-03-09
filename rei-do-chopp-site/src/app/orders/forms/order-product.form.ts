import { Validators } from "@angular/forms";

export const ORDER_PRODUCT_FORM_CONFIG =
{
  'Product': [, [Validators.required]],
  'UnitPriceCharged': ['', [Validators.required]],
  'Quantity': [1, [Validators.required, Validators.min(1)]]
}
