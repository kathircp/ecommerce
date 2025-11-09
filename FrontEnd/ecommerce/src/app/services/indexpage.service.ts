import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IndexPage } from '../models/index-page.model';
import { Observable } from 'rxjs';


const STORE_BASE_URL = 'http://localhost:5179/api/ecommerce';
@Injectable({
  providedIn: 'root'
})
export class IndexpageService {

  constructor(private httpClient: HttpClient) { }

  getPageIndex(): Observable<Array<IndexPage>>{
    return this.httpClient.get<Array<IndexPage>>(
      `${STORE_BASE_URL}/Page`
    )
  }

}
