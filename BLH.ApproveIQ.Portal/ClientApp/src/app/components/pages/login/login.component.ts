import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { AuthService } from '../../../framework/services/auth.service';

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
    private authService: AuthService
  ) {
    this.emailControl = new FormControl('bo.manry@sparkhound.com', [Validators.required, Validators.email]);
    this.loginForm = this.formBuilder.group({
      email: this.emailControl
    });
  }

  onLogin(): void {
    console.log(this.loginForm.value);
    if (this.loginForm.valid) {
      this.isLoading = true;
      this.loginError = null;
      const email = this.loginForm.value.email;
      console.log(email);
      this.userService.validateUser(email).subscribe({
        next: (response) => {
          this.isLoading = false;
          if (response.isValid && response.user) {
            console.log('Login successful for user:', response.user);
            // Use auth service to set login state
            this.authService.login(response.user);
            this.router.navigate(['/home']);
          } else {
            this.loginError = 'User not found. Please check your email address.';
          }
        },
        error: (error) => {
          this.isLoading = false;
          console.error('Login error:', error);
          if (error.status === 404) {
            this.loginError = 'User not found. Please check your email address.';
          } else {
            this.loginError = 'An error occurred during login. Please try again.';
          }
        }
      });
    } else {
      console.log('Form is invalid');
    }
  }
}
