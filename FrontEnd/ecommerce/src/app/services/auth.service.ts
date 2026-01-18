import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'http://localhost:5179/Auth';

  private loggedInSubject = new BehaviorSubject<boolean>(this.hasToken());
  isLoggedIn$ = this.loggedInSubject.asObservable();
  private loggedIn = false;

  constructor(private http: HttpClient) {
  }

  // 🔐 Normal Login
  login(username: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, {
      username,
      password
    }).pipe(
      tap(response => {
        localStorage.setItem('token', response.token);
        this.loggedInSubject.next(true);
        this.loggedIn = true;
      })
    );
  }
  isLoggedIn(): boolean {
    return this.loggedIn;
  }

  // 🚪 Logout
  logout(): void {
    localStorage.removeItem('token');
    this.loggedInSubject.next(false);   
    this.loggedIn = false;
  }

  // 🔎 Token check
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  private hasToken(): boolean {
    return !!localStorage.getItem('token');
  }
  
}
