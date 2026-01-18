import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { LoginDialogComponent } from 'src/app/components/login-dialog/login-dialog.component';
import { Cart, CartItem } from 'src/app/models/cart.model';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  cart: Cart = { items: []};

  dataSource: Array<CartItem> = [];
  displayedColumns: Array<string> = [
    'product',
    'name',
    'price',
    'quantity',
    'total',
    'action',
  ]
  constructor(private cartService: CartService, private authService: AuthService, 
    private router: Router, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.start();
  }

  start(){
    this.cartService.cart.subscribe((_cart: Cart) => {
      this.cart = _cart;
      this.dataSource = this.cart.items;
    });
  }

  getTotal(items: Array<CartItem>): number{
    return this.cartService.getTotal(items);
  }

  onClearCart(): void{
    this.cartService.clearCart();
  }

  onRemoveFromCart(item: CartItem): void{
    this.cartService.removeFromCart(item);
  }

  onAddQuantity(item: CartItem): void{
    this.cartService.addToCart(item);
  }

  onRemoveQuantity(item: CartItem): void{
    this.cartService.removeQuantity(item);
  }
  proceedToCheckout() {
    // 🔒 Step 1: Check login
    if (!this.authService.isLoggedIn()) {
       const dialogRef = this.dialog.open(LoginDialogComponent, {
              width: '400px', // Customize width
              // Add other configuration options like data, disableClose, etc.
            });
            console.log("Login dialog opened");
      // this.router.navigate(['/login'], {
      //   queryParams: { returnUrl: '/checkout' }
      // });
      // return;
    }
    console.log("Proceeding to checkout");
    // 🛒 Step 2: Go to checkout
    if (this.authService.isLoggedIn())
      this.router.navigate(['/checkout']);
  }
}
