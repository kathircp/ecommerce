import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Address } from '../models/address.model';
import { Observable } from 'rxjs';
import { Order } from '../models/order.model';

const STORE_BASE_URL = 'http://localhost:5179/api/ecommerce';
@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private apiUrl = `${STORE_BASE_URL}/orders`;
  constructor(private http: HttpClient) { }
  getMyOrders() {
    return this.http.get<any[]>(`${this.apiUrl}/CreateOrderItem`);
  }

  getOrderById(orderId: string) {
    return this.http.get<any>(`${this.apiUrl}/${orderId}`);
  }
  saveOrderItem(order: Order): Observable<any> {
    console.log(order);
    return this.http.post(`${this.apiUrl}/CreateOrderItem`, order);
  }
}
