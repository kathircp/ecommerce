import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Cart, CartItem } from 'src/app/models/cart.model';
import { CartService } from './../../services/cart.service';
import { LoginDialogComponent } from 'src/app/login-dialog/login-dialog.component';
import {
  MatDialog,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';

import {ChangeDetectionStrategy} from '@angular/core';
import { IndexpageService } from 'src/app/services/indexpage.service';
import { IndexPage } from 'src/app/models/index-page.model';
import { Subscription } from 'rxjs';
import { MenuItem } from 'src/app/models/menu-item.model';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
 
})
export class HeaderComponent {
 
 
  // Notification banner
  showNotification = true;
  private readonly NOTIF_KEY = '1';
  indexpage : Array<IndexPage> | undefined;
  private _cart: Cart = { items: []};
  itemsQuantity = 0;
  productSubscription: Subscription | undefined;
  category: string | undefined;

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

  constructor(private cartService: CartService, private dialog: MatDialog,
    private indexpageService: IndexpageService
  ) { 
    this.getPageIndex();     
  }

   menuItemsSarees: MenuItem[] = [
    // { label: 'Item 1', action: () => console.log('Item 1 clicked') },
    // { label: 'Item 2', routerLink: '/some-route' },
    // { label: 'Item 3', action: () => console.log('Item 3 clicked') }
  ];
  menuItemsKurtas: MenuItem[] = []
  menuItemsBlouses: MenuItem[] = []  
 
  // Method to add a new item dynamically
  addMenuItemSarees(label: string, action?: () => void, routerLink?: string) {
    this.menuItemsSarees.push({ label, action, routerLink });
  }
  addMenuItemKurtas(label: string, action?: () => void, routerLink?: string) {
    this.menuItemsKurtas.push({ label, action, routerLink });
  }
  addMenuItemBlouses(label: string, action?: () => void, routerLink?: string) {
    this.menuItemsBlouses.push({ label, action, routerLink });
  }
 
  @Output() search = new EventEmitter<string>();

  ngOnInit(): void {     
    this.showNotification = localStorage.getItem(this.NOTIF_KEY) !== '1';    
  }  
  getPageIndex(): void{
    this.productSubscription= this.indexpageService.getPageIndex()
    .subscribe((_pages)=> {
      this.indexpage = _pages;
        const modulesSarees = this.indexpage.filter(item => item.moduleName == "Sarees")
        const distinctSarees: string[] = [...new Set(modulesSarees.map(item => item.categoryName))];
        distinctSarees.forEach((item: string) => {
          this.addMenuItemSarees(item, undefined, `/${item}`);       
        });
        const modulesKurtas = this.indexpage.filter(item => item.moduleName == "Kurthas")
        const distinctKurtas: string[] = [...new Set(modulesKurtas.map(item => item.categoryName))];
        distinctKurtas.forEach((item: string) => {
          this.addMenuItemKurtas(item, undefined, `/${item}`);       
        });        
        const modulesBlouses = this.indexpage.filter(item => item.moduleName == "Blouses")
        const distinctBlouses: string[] = [...new Set(modulesBlouses.map(item => item.categoryName))];
        distinctBlouses.forEach((item: string) => {
          this.addMenuItemBlouses(item, undefined, `/${item}`);       
        });
      //console.log(11111,this.indexpage);
    })
  }
  onSearch(query: string){
    this.search.emit(query);
  }
  
  onUserClick(){
    // Placeholder - open login dialog or navigate to account
    console.log('User icon clicked');
     const dialogRef = this.dialog.open(LoginDialogComponent, {
        width: '400px', // Customize width
        // Add other configuration options like data, disableClose, etc.
      });

      dialogRef.afterClosed().subscribe(() => {
        console.log('The dialog was closed');
        // Handle login result if needed
      });
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
  ngOnDestroy(): void {
    if(this.productSubscription){
      this.productSubscription.unsubscribe();
    }
  }

}