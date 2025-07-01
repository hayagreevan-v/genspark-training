import { Component } from '@angular/core';
import { Navbar } from "../navbar/navbar";
import { NotificationService } from '../services/notification.service';
import { UserService } from '../services/user.service';
import { UserModel } from '../models/user.model';

@Component({
  selector: 'app-notification',
  imports: [Navbar],
  templateUrl: './notification.html',
  styleUrl: './notification.css'
})
export class Notification {
  currentUser : UserModel | null = null;
  notifications : {user : string, message : string}[] =[]

  constructor(private userService : UserService,private notifyService : NotificationService){
    this.userService.user$.subscribe({
      next : (data : any) => {
        this.currentUser = data;
      }
    });
    
    this.notifyService.notification$.subscribe({
      next: (data : any) =>{
        this.notifications = data;
      }
    });
    if(this.currentUser == null){
      return;
    }
  }

}
