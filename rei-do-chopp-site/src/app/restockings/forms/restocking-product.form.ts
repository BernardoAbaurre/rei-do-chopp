import { Validators } from "@angular/forms";

export const RESTOCKING_PRODUCT_FORM_CONFIG =
{
  'Product': [, [Validators.required]],
  'UnitPricePaid': ['', [Validators.required]],
  'Quantity': ['', [Validators.required, Validators.min(1)]]
}
