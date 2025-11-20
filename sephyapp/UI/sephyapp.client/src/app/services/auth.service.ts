import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { loginRequest } from '../models/login-request';
import { loginResponse } from '../models/login-response';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient) { }

  login(credentials: loginRequest): Observable<loginResponse> {
    return this.httpClient.post<loginResponse>('https://localhost:7212/api/login', credentials)
    .pipe(map((response: loginResponse) => {
      localStorage.setItem('accessToken', response.accessToken);
      document.cookie = `refreshToken=${response.refreshToken}`;
      return response;
    }))
  }
}
