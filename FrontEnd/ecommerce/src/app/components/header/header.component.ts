import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Cart, CartItem } from 'src/app/models/cart.model';
import { CartService } from './../../services/cart.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  // Notification banner
  showNotification = true;
  private readonly NOTIF_KEY = '1';

  private _cart: Cart = { items: []};
  itemsQuantity = 0;

  @Input()
  get cart(): Cart{
    return this._cart;
  }
  set cart(cart: Cart){
    this._cart = cart;

    this.itemsQuantity = cart.items
    .map((item)=> item.quantity)
    .reduce((prev, current) => prev + current, 0);
  }

  constructor(private cartService: CartService) { }

  @Output() search = new EventEmitter<string>();

  ngOnInit(): void {
    this.showNotification = localStorage.getItem(this.NOTIF_KEY) !== '1';    
  }

  onSearch(query: string){
    this.search.emit(query);
  }

  onUserClick(){
    // Placeholder - open login dialog or navigate to account
    console.log('User icon clicked');
  }

  getTotal(items: Array<CartItem>): number{
    return this.cartService.getTotal(items);
  }

  onClearCart(){
    this.cartService.clearCart();
  }
  dismissNotification(): void{
    this.showNotification = false;
    try{ localStorage.setItem(this.NOTIF_KEY, '1'); }catch(e){}
  } 

}
