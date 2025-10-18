import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';

export interface LoginResponse {
  token: string;
  email: string;
  firstName: string;
  lastName: string;
  userId: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  private userSubject = new BehaviorSubject<LoginResponse | null>(null);
  private tokenSubject = new BehaviorSubject<string | null>(null);

  constructor(private router: Router) {
    // Check if user is already logged in on service initialization
    this.checkLoginStatus();
  }

  // Observable for components to subscribe to login status changes
  get isLoggedIn$(): Observable<boolean> {
    return this.isLoggedInSubject.asObservable();
  }

  // Observable for components to subscribe to user changes
  get user$(): Observable<LoginResponse | null> {
    return this.userSubject.asObservable();
  }

  // Observable for components to subscribe to token changes
  get token$(): Observable<string | null> {
    return this.tokenSubject.asObservable();
  }

  // Get current login status
  get isLoggedIn(): boolean {
    return this.isLoggedInSubject.value;
  }

  // Get current user
  get currentUser(): LoginResponse | null {
    return this.userSubject.value;
  }

  // Get current token
  get currentToken(): string | null {
    return this.tokenSubject.value;
  }

  // Login method - call this when login is successful
  login(loginResponse: LoginResponse): void {
    this.isLoggedInSubject.next(true);
    this.userSubject.next(loginResponse);
    this.tokenSubject.next(loginResponse.token);
    
    // Store auth info in localStorage for persistence
    localStorage.setItem('authToken', loginResponse.token);
    localStorage.setItem('currentUser', JSON.stringify(loginResponse));
  }

  // Logout method - call this when user logs out
  logout(): void {
    this.isLoggedInSubject.next(false);
    this.userSubject.next(null);
    this.tokenSubject.next(null);
    
    // Clear localStorage
    localStorage.removeItem('authToken');
    localStorage.removeItem('currentUser');
    
    // Navigate to login page
    this.router.navigate(['/']);
  }

  // Check login status from localStorage on app initialization
  private checkLoginStatus(): void {
    const token = localStorage.getItem('authToken');
    const currentUser = localStorage.getItem('currentUser');
    
    if (token && currentUser && !this.isTokenExpired(token)) {
      const user = JSON.parse(currentUser) as LoginResponse;
      this.isLoggedInSubject.next(true);
      this.userSubject.next(user);
      this.tokenSubject.next(token);
    } else {
      // Token expired or invalid, clear storage
      this.logout();
    }
  }

  // Simple token expiration check
  private isTokenExpired(token: string): boolean {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const currentTime = Math.floor(Date.now() / 1000);
      return payload.exp < currentTime;
    } catch {
      return true; // If we can't parse it, consider it expired
    }
  }
}