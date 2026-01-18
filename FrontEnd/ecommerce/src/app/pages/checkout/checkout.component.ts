import { Component } from '@angular/core';

@Component({
  selector: 'app-checkout', 
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css'
})
export class CheckoutComponent {
  onPaymentDone(payment: any) {
  console.log(payment);

  // Call backend API to create order
  // Clear cart
  // Navigate to success page
}
}
