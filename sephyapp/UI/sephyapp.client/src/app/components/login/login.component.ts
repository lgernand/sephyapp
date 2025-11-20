import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { LoginRequest } from '../../models/login-request';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { CommonModule } from '@angular/common';
import { ProfileListComponent } from "../profile-list/profile-list.component";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, NgIf, ProfileListComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService) { }

  credentials: LoginRequest = {
    email: '',
    password: ''
  }

  ngOnInit(): void {
    this.isLoggedIn();
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  login() {
    return this.authService.login(this.credentials)
    .subscribe(() => {
      console.log('login successful');
    });
  }
}
