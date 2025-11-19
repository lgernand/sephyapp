import { HttpClient, provideHttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SephyUser } from '../models/SephyUser.model';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AsyncPipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  http = inject(HttpClient);

  sephyUsers$ = this.getUsers();

  private getUsers(): Observable<SephyUser[]> {
    return this.http.get<SephyUser[]>('https://localhost:7212/api/sephyusers');
  }
}
