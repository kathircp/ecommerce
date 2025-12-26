import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Product } from 'src/app/models/product.model';

@Component({
  selector: 'app-product-box',
  templateUrl: './product-box.component.html',
  styleUrls: ['./product-box.component.css']
})
export class ProductBoxComponent implements OnInit {

  @Input() fullWidthMode = false;
  @Input() product: Product | undefined;
  @Output() addToCart = new EventEmitter();

  selectedProduct: Product | undefined;
  constructor() { }

  ngOnInit(): void {
  }

  onAddToCart(): void{
    this.addToCart.emit(this.product);
  }
  openQuickView() {
    this.selectedProduct = this.product;
  }  
  closeQuickView() {
    this.selectedProduct = undefined;
  }
  addToCartSelected(product: Product) {
    this.addToCart.emit(product);
  }
}
