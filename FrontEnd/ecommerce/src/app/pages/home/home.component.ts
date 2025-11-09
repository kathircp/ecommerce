import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Subscription } from 'rxjs';
import { Product } from 'src/app/models/product.model';
import { StoreService } from 'src/app/services/store.service';
import { CartService } from './../../services/cart.service';
import { IndexpageService } from 'src/app/services/indexpage.service';
import { IndexPage } from 'src/app/models/index-page.model';

const ROWS_HEIGHT: { [id:number]: number } = { 1: 400, 2: 380, 3: 335, 4: 350};
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
  
})
export class HomeComponent implements OnInit, OnDestroy {

  showFilters = false;
  showCarousel = true;
  drawerMode: 'side' | 'over' = 'side';

  
  // (banner removed)

  category: string | undefined;
  cols: number = 4;
  rowHeight = ROWS_HEIGHT[this.cols];
  products: Array<Product> | undefined;
  
  sort: string = 'desc';
  count: string = '12';
  productSubscription: Subscription | undefined;  
  activeCategory: any = null;
  filteredProducts: any[] = []; // Array to store filtered products

  constructor(
    private cartService: CartService,
    private storeService: StoreService,   
    private route: ActivatedRoute,
    private breakpoint: BreakpointObserver
  ) { }

  ngOnInit(): void {
   
    this.getProducts();
    this.breakpoint.observe([Breakpoints.XSmall, Breakpoints.Small]).subscribe(result => {
      // Prefer XSmall -> 1 column, Small -> 2 columns, otherwise desktop (4)
      if (result.breakpoints[Breakpoints.XSmall]) {
        this.drawerMode = 'over';
        this.showFilters = false;
        this.showCarousel = false;
        this.cols = 1;
        this.rowHeight = ROWS_HEIGHT[this.cols];
      } else if (result.breakpoints[Breakpoints.Small]) {
        this.drawerMode = 'over';
        this.showFilters = false;
        this.showCarousel = false;
        this.cols = 2;
        this.rowHeight = ROWS_HEIGHT[this.cols];
      } else {
        this.drawerMode = 'side';
        // show filters by default on larger screens
        this.showFilters = true;
        this.showCarousel = true;
        // restore default columns for larger screens
        this.cols = 4;
        this.rowHeight = ROWS_HEIGHT[this.cols];
      }
    });
    this.route.queryParams.subscribe(params => {
      const cat = params['category'];
      if (cat) {
        this.category = cat;
        this.showFilters = true;
      }
    });
  }

  toggleFilters(): void{
    this.showFilters = !this.showFilters;
  }

  getProducts(): void{
    this.productSubscription = this.storeService.getAllProducts(this.count, this.sort)
    .subscribe((_products)=> {
      this.products = _products;      
    })
  }

  onColumnsCountChange(colsNumber: number): void{
    this.cols = colsNumber;
    this.rowHeight = ROWS_HEIGHT[this.cols];
    console.log(`Entrou no onColumns - Cols = ${this.cols}`);
    console.log(`Entrou no onColumns - RowHeight = ${this.rowHeight}`);
  }

  onShowCategory(newCategory: string): void{
    this.category = newCategory;
  }
   openFilter(category: any) {
    this.activeCategory = category;
    this.showFilters = true;
  }

  closeFilter() {
    this.showFilters = false;
    this.activeCategory = null;
  }

  applyFilter(filterCriteria: any) {
    // Implement your filtering logic here based on filterCriteria
    // For example:
    // this.filteredProducts = this.allProducts.filter(product => product.category === filterCriteria.category);
    console.log('Filter applied with:', filterCriteria);
    this.closeFilter();
  }

  onAddToCart(product: Product): void{
    this.cartService.addToCart({
      product: product.imageUrl,
      name: product.name,
      price: product.price,
      quantity: 1,
      id: product.id
    });
  }

  ngOnDestroy(): void {
    if(this.productSubscription){
      this.productSubscription.unsubscribe();
    }
  }
}