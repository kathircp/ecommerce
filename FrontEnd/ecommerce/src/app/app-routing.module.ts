import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CartComponent } from './pages/cart/cart.component';
import { HomeComponent } from './pages/home/home.component';
import { ProductItemDetailComponent } from './pages/product-item-detail/product-item-detail.component';

import { CheckoutComponent } from './pages/checkout/checkout.component';
import { OrderListComponent } from './pages/order-list/order-list.component';
import { OrderTrackComponent } from './pages/order-track/order-track.component';
import { AuthGuard } from './guards/auth.guard';
import { LoginComponent } from './auth/login/login.component';


const routes: Routes = [
  {
    path: 'home', component: HomeComponent
  },
  {
    path: 'cart', component: CartComponent
  },  
  {
    path: 'checkout', component: CheckoutComponent
  },
  {
    path: 'orders',
    component: OrderListComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'orders/:id',
    component: OrderTrackComponent
  },
  {
    path: '', redirectTo: 'home', pathMatch: 'full'
  },
  {
    path: 'home/item-detail/:id' , 
    component: ProductItemDetailComponent
  },
  {
    path: 'login', 
    component: LoginComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
