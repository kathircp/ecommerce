import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/models/product.model';
import { CartService } from 'src/app/services/cart.service';
import { StoreService } from 'src/app/services/store.service';

@Component({
  selector: 'app-product-item-detail',
  templateUrl: './product-item-detail.component.html',
  styleUrl: './product-item-detail.component.css'
})
export class ProductItemDetailComponent {
 id: number | undefined;
  products?: Array<Product> | undefined;
  product?: Product;

  constructor(private route: ActivatedRoute, private productService: StoreService,
     private cartService: CartService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.id = Number(params.get('id'));
      //console.log(1111,this.id);
      
    })
    this.productService.getProduct(this.id!).subscribe(res =>{
      this.product = res;
      //this.product = this.getProductById(this.id)      
      //console.log(2222,this.product)
    })
    
  }
  addToCart() {
    console.log('addtoproduct', this.product )
    this.onAddToCart(this.product!)
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
}