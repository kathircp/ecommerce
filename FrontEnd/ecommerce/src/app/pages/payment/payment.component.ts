import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { CartService } from 'src/app/services/cart.service';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
@Component({
  selector: 'app-payment',  
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent {
  
  @Output() paymentCompleted = new EventEmitter<any>();

  paymentForm = this.fb.group({
    paymentMode: ['COD'] // COD | ONLINE
  });
  constructor(private cartService: CartService,private fb: FormBuilder,
     //private orderService: OrderService, 
     private router: Router) {
    
  } 
  placeOrder() {

    const order = {
      items: this.cartService.getItems(),
      total: this.cartService.getTotalValue(),
      paymentMode: 'ONLINE'
    };
    const paymentMode = this.paymentForm.value.paymentMode;

    if (paymentMode === 'COD') {
      this.paymentCompleted.emit({
        mode: 'COD',
        status: 'SUCCESS'
      });
    } else {
      this.startOnlinePayment();
    }
  }

  startOnlinePayment() {
    // 🔔 Integrate Razorpay / Stripe here
    this.paymentCompleted.emit({
      mode: 'ONLINE',
      status: 'SUCCESS',
      transactionId: 'TXN' + Date.now()
    });
  }

  // this.orderService.createOrder(order).subscribe(() => {
  //   this.cartService.clearCart();
  //   this.router.navigate(['/order-success']);
  // });
}
