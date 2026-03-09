import { Injectable, signal } from '@angular/core';
import { Observable, delay, map, of, tap } from 'rxjs';

type LoginResult = {
  success: boolean;
  errorMessage?: string;
};

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly authStorageKey = 'selenium-training-authenticated';
  private readonly authenticated = signal(this.readStoredAuthState());

  login(username: string, password: string, rememberMe: boolean): Observable<LoginResult> {
    return of({ username, password, rememberMe }).pipe(
      delay(1200),
      map(({ username, password }) => {
        const isValidLogin = username === 'testuser' && password === 'password123';

        if (!isValidLogin) {
          return {
            success: false,
            errorMessage: 'Invalid username or password.',
          };
        }

        return {
          success: true,
        };
      }),
      tap((result) => {
        if (result.success) {
          this.authenticated.set(true);
          this.storeAuthState(true, rememberMe);
        } else {
          this.authenticated.set(false);
          this.clearStoredAuthState();
        }
      }),
    );
  }

  logout(): void {
    this.authenticated.set(false);
    this.clearStoredAuthState();
  }

  isAuthenticated(): boolean {
    return this.authenticated();
  }

  private readStoredAuthState(): boolean {
    if (typeof window === 'undefined') {
      return false;
    }

    return (
      localStorage.getItem(this.authStorageKey) === 'true' ||
      sessionStorage.getItem(this.authStorageKey) === 'true'
    );
  }

  private storeAuthState(value: boolean, rememberMe: boolean): void {
    if (typeof window === 'undefined') {
      return;
    }

    const storage = rememberMe ? localStorage : sessionStorage;
    const otherStorage = rememberMe ? sessionStorage : localStorage;

    storage.setItem(this.authStorageKey, String(value));
    otherStorage.removeItem(this.authStorageKey);
  }

  private clearStoredAuthState(): void {
    if (typeof window === 'undefined') {
      return;
    }

    localStorage.removeItem(this.authStorageKey);
    sessionStorage.removeItem(this.authStorageKey);
  }
}
