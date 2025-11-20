import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Profile } from '../models/profile';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private httpClient: HttpClient) { }

  getProfiles(): Observable<Profile[]> {
    return this.httpClient.get<Profile[]>('https://localhost:7212/api/sephyprofile');
  }
}
