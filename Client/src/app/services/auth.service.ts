import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal, Signal } from '@angular/core';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http: HttpClient = inject(HttpClient);
  private apiUrl = 'https://localhost:7270';
  private fakeUserName = 'fakeUser';
  private fakeUserPassword = 'FakeUserPa$$word';
  private tokenKey = 'auth_token';
  private _currentUser = signal<any>(null);

  get currentUser(): any {
    return this._currentUser();
  }
  constructor() {
    this.login();
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  private login() {
    var user = { Username: this.fakeUserName, Password: this.fakeUserPassword };
    return this.http
      .post<any>(`${this.apiUrl}/login`, user)
      .pipe(
        tap((response: any) => {
          localStorage.setItem('token', response.token);
          localStorage.setItem('username', this.fakeUserName);
        })
      )
      .subscribe((res) =>{ this._currentUser.set(this.fakeUserName)});
  }
}
