import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from '../models/login-request';
import { LoginResponse } from '../models/login-response';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient) { }

  login(credentials: LoginRequest): Observable<LoginResponse> {
    console.log('attempting login...');
    return this.httpClient.post<LoginResponse>('https://localhost:7212/login', credentials)
    .pipe(map((response: LoginResponse) => {
      localStorage.setItem('accessToken', response.accessToken);
      document.cookie = `refreshToken=${response.refreshToken}`;
      return response;
    }));
  }

  refreshToken(): Observable<LoginResponse>{
    const refreshToken = this.getRefreshTokenFromCookie();

    return this.httpClient.post<LoginResponse>('https://localhost:7212/login', {refreshToken})
    .pipe(map((response: LoginResponse) => {
      localStorage.setItem('accessToken', response.accessToken);
      document.cookie = `refreshToken=${response.refreshToken}`;
      return response;
    }));
  }

  private getRefreshTokenFromCookie(): string | null {
    const cookieString = document.cookie;
    const cookieArray = cookieString.split('; ')

    for (const cookie of cookieArray) {
      const [name, value] = cookie.split('=');

      if (name == 'refreshToken') {
        return value;
      }
    }
    return null;
  }

  logout() {
    localStorage.removeItem('accessToken');
  }

  isLoggedIn(): boolean {
    return localStorage.getItem('accessToken') !== null;
  }
}
