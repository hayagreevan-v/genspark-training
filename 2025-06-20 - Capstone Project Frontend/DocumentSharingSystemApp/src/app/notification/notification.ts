import { Component } from '@angular/core';
import { Navbar } from "../navbar/navbar";
import { NotificationService } from '../services/notification.service';
import { UserService } from '../services/user.service';
import { UserModel } from '../models/user.model';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-notification',
  imports: [Navbar, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './notification.html',
  styleUrl: './notification.css'
})
export class Notification {
  currentUser : UserModel | null = null;
  notifications : {user : string, message : string, isNew: boolean}[] =[]

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

  dismiss(index: any){
    this.notifyService.dismissNotification(index);
  }

}
