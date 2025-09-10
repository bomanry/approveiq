import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  private userSubject = new BehaviorSubject<any>(null);

  constructor(private router: Router) {
    // Check if user is already logged in on service initialization
    this.checkLoginStatus();
  }

  // Observable for components to subscribe to login status changes
  get isLoggedIn$(): Observable<boolean> {
    return this.isLoggedInSubject.asObservable();
  }

  // Observable for components to subscribe to user changes
  get user$(): Observable<any> {
    return this.userSubject.asObservable();
  }

  // Get current login status
  get isLoggedIn(): boolean {
    return this.isLoggedInSubject.value;
  }

  // Get current user
  get currentUser(): any {
    return this.userSubject.value;
  }

  // Login method - call this when login is successful
  login(user: any): void {
    this.isLoggedInSubject.next(true);
    this.userSubject.next(user);
    
    // Store user info in localStorage for persistence
    localStorage.setItem('isLoggedIn', 'true');
    localStorage.setItem('currentUser', JSON.stringify(user));
  }

  // Logout method - call this when user logs out
  logout(): void {
    this.isLoggedInSubject.next(false);
    this.userSubject.next(null);
    
    // Clear localStorage
    localStorage.removeItem('isLoggedIn');
    localStorage.removeItem('currentUser');
    
    // Navigate to login page
    this.router.navigate(['/']);
  }

  // Check login status from localStorage on app initialization
  private checkLoginStatus(): void {
    const isLoggedIn = localStorage.getItem('isLoggedIn') === 'true';
    const currentUser = localStorage.getItem('currentUser');
    
    if (isLoggedIn && currentUser) {
      this.isLoggedInSubject.next(true);
      this.userSubject.next(JSON.parse(currentUser));
    }
  }
}