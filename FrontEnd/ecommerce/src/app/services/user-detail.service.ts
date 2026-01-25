
// services/user-detail.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Address } from '../models/address.model';
const STORE_BASE_URL = 'http://localhost:5179';
@Injectable({
  providedIn: 'root'
})
export class UserDetailService {

  private baseUrl = `${STORE_BASE_URL}/api`;  

  constructor(private http: HttpClient) {}

  saveAddress(address: Address): Observable<any> {
    return this.http.post(`${this.baseUrl}/UserDetail`, address);
  }
}

