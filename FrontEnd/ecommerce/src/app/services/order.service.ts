import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private apiUrl = 'https://api.example.com/orders';
  constructor(private http: HttpClient) { }
  getMyOrders() {
    return this.http.get<any[]>(`${this.apiUrl}/my-orders`);
  }

  getOrderById(orderId: string) {
    return this.http.get<any>(`${this.apiUrl}/${orderId}`);
  }
}
