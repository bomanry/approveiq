import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { AuthService, LoginResponse } from '../../../framework/services/auth.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;
  emailControl: FormControl;
  isLoading = false;
  loginError: string | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private router: Router,
    private authService: AuthService,
    private http: HttpClient
  ) {
    this.emailControl = new FormControl('bo.manry@sparkhound.com', [Validators.required, Validators.email]);
    this.loginForm = this.formBuilder.group({
      email: this.emailControl
    });
  }

  onLogin(): void {
    if (this.loginForm.valid) {
      this.isLoading = true;
      this.loginError = null;
      const email = this.loginForm.value.email;

      const loginRequest = { email };

      this.http.post<LoginResponse>(`${environment.baseUrl}/auth/login`, loginRequest).subscribe({
        next: (response) => {
          this.isLoading = false;
          console.log('Login successful:', response);

          // Use auth service to set login state with JWT token
          this.authService.login(response);
          this.router.navigate(['/home']);
        },
        error: (error) => {
          this.isLoading = false;
          console.error('Login error:', error);
          if (error.status === 401 || error.status === 404) {
            this.loginError = 'User not found or account disabled. Please check your email address.';
          } else if (error.status === 400) {
            this.loginError = 'Invalid email address format.';
          } else {
            this.loginError = 'An error occurred during login. Please try again.';
          }
        }
      });
    } else {
      this.loginError = 'Please enter a valid email address.';
    }
  }
}
