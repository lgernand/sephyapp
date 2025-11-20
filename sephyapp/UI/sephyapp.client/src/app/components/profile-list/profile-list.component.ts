import { Component, OnInit } from '@angular/core';
import { Profile } from '../../models/profile';
import { ProfileService } from '../../services/profile.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile-list.component.html',
  styleUrl: './profile-list.component.css'
})
export class ProfileListComponent implements OnInit {
  constructor(private profileService: ProfileService) { }
  
  ngOnInit(): void {
    this.loadProfiles();
  }

  profiles: Profile[] = [];

  loadProfiles(): void {
    this.profileService.getProfiles().subscribe(response => {
      this.profiles = response;
    })
  }
}
