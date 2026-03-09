import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-login',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  private readonly formBuilder = inject(FormBuilder);
  private readonly router = inject(Router);

  protected readonly isSubmitting = signal(false);
  protected readonly loginError = signal<string | null>(null);
  protected hidePassword = true;

  protected readonly loginForm = this.formBuilder.group({
    username: ['', [Validators.required]],
    password: ['', [Validators.required]],
    rememberMe: [false]
  });

  protected submit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.loginError.set(null);
    this.isSubmitting.set(true);

    const username = this.loginForm.controls.username.value ?? '';
    const password = this.loginForm.controls.password.value ?? '';

    setTimeout(() => {
      const isValidLogin =
        username === 'testuser' &&
        password === 'password123';

      this.isSubmitting.set(false);

      if (!isValidLogin) {
        this.loginError.set('Invalid username or password.');
        return;
      }

      this.router.navigate(['/dashboard']);
    }, 1200);
  }

  protected togglePasswordVisibility(): void {
    this.hidePassword = !this.hidePassword;
  }

  protected get usernameControl() {
    return this.loginForm.controls.username;
  }

  protected get passwordControl() {
    return this.loginForm.controls.password;
  }
}