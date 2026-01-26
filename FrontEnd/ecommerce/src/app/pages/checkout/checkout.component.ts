import { Component } from '@angular/core';
import { Order, OrderItem } from 'src/app/models/order.model';
import { CartService } from 'src/app/services/cart.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-checkout', 
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css'
})
export class CheckoutComponent {

  cartItems: any[] = [];
  subtotal = 0;
  deliveryCharge = 50;
  total = 0;
  userName: string | null = '';
  address: any = {};
  order : Order | undefined;
  orderItems : OrderItem[] = [];

  constructor(private cartService: CartService, private orderService: OrderService) {
   

  }
  onAddressSaved(address: any) {    
    this.address = address;
  }

  onPaymentDone(payment: any) {
    console.log('payment', payment);
    this.userName = localStorage.getItem('userName');
    this.cartItems = this.cartService.getItems();
    this.subtotal = this.cartService.getTotalValue();
    // console.log(this.userName);
    console.log(this.cartItems);
    // console.log(this.address);
    this.calculateTotal();

    this.cartItems.forEach(p => {
      this.orderItems.push(
        {
          productId: p.id,
          productName: p.name,
          unitPrice: p.price,
          quantity: p.quantity,
          lineTotal: p.price * p.quantity
        }
      );
    });
    // Call backend API to create order
    // Clear cart
    // Navigate to success page
    this.order = {
      userName: this.userName || '',
      createdAt: new Date().toISOString(),
      total: this.total,  
      orderStatus: "Booked",
      items: this.orderItems,
      address: this.address,
      payment: payment,
      remarks: "Order placed successfully"
    };

    this.orderService.saveOrderItem(this.order).subscribe({
      next: (res) => {
        console.log('Order saved successfully', res);
        alert('Order placed successfully');
        this.cartService.clearCart();
        // Navigate to success page
      },
      error: (err) => {
        console.error('Error saving order', err);
      }
    });
  }
  calculateTotal() {
    this.total = this.subtotal + this.deliveryCharge;
  }
}
