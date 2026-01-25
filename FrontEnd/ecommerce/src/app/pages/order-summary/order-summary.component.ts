import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';

import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-order-summary',  
  templateUrl: './order-summary.component.html',
  styleUrls: ['./order-summary.component.css']
})
export class OrderSummaryComponent implements OnInit {

  cartItems: any[] = [];
  subtotal = 0;
  deliveryCharge = 50;
  total = 0;

  constructor(private cartService: CartService) {}

  ngOnInit() {
    this.cartItems = this.cartService.getItems();
    this.subtotal = this.cartService.getTotalValue();   
    this.calculateTotal();
  }

  calculateTotal() {
    this.total = this.subtotal + this.deliveryCharge;
  }
}
