import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { IndexpageService } from 'src/app/services/indexpage.service';
import { Subscription } from 'rxjs';
import { IndexPage } from 'src/app/models/index-page.model';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  products: any[] = [];
  filteredProducts: any[] = [];
  categories: any[] = [];
   productSubscription: Subscription | undefined; 
   pageIndex: any[] = [];

  columns = ['image','name','price','stock','category','actions'];

  constructor(private http: HttpClient, private router: Router, private indexpageService: IndexpageService) {}

  ngOnInit() {
    this.loadCategories();    
    this.loadProducts();    
  }

  loadProducts() {
    this.http.get<any[]>('http://localhost:5179/api/ecommerce/products?limit=12')
      .subscribe(res => {
        res.forEach(element => {
          element.categoryName = this.getCategoryName(element.categoryId);
        });
        this.products = res;
        this.filteredProducts = res;
        console.log(res)
      });
  }
  getCategoryName(categoryId: any): any {
    const categories: any = this.pageIndex.filter(x=> x.categoryId == categoryId);
    const category = categories[0];
    
    return category ? category.categoryName : 'Unknown';
  }

  loadCategories() {
    this.productSubscription= this.indexpageService.getPageIndex()
    .subscribe((_pages)=> {
      this.pageIndex = _pages;
      this.categories = [...new Set(_pages
        .map(item => item.categoryName)
        .filter(name => name != null)
      )];

    });  
  }

  applySearch(event: any) {
    const value = event.target.value.toLowerCase();

    this.filteredProducts = this.products.filter(p =>
      p.name.toLowerCase().includes(value) ||
      (p.color && p.color.toLowerCase().includes(value))
    );
  }

  filterByCategory(categoryName: string) {
    if (!categoryName) {
      this.filteredProducts = this.products;
      return;
    }

    this.filteredProducts =
      this.products.filter(p => p.categoryName == categoryName);
  }

  edit(id: number) {
    this.router.navigate(['/admin/product', id]);
  }

  delete(id: number) {
    if (!confirm('Delete this product?')) return;

    this.http.delete(`http://localhost:5179/api/ecommerce/Products/${id}`)
      .subscribe(() => {
        this.loadProducts();
      });
  }
}
