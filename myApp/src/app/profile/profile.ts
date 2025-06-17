import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { UserService } from '../services/UserService';
import { UserProfileModel } from '../models/UserProfileModel';

@Component({
  selector: 'app-profile',
  imports: [],
  templateUrl: './profile.html',
  styleUrl: './profile.css'
})

export class Profile {
  http = inject(HttpClient);
  profileData : UserProfileModel|null = null;
  constructor(private userService:UserService){
    this.userService.callGetProfile().subscribe({
      next: (data:any) =>{
        this.profileData = UserProfileModel.fromForm(data);
      }
    })
  }
  
}
