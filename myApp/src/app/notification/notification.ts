import { Component } from '@angular/core';
import { NotificationService } from '../services/NotificationService';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-notification',
  imports: [FormsModule],
  templateUrl: './notification.html',
  styleUrl: './notification.css'
})
export class Notification {
  username:string="";
  message:string ="";
  messages : {user:string, message: string}[] =[];

  constructor(private notifyService : NotificationService){
    this.notifyService.messages$.subscribe({
      next: (data : any) =>{
        this.messages= data;
      }
    })
  }

  ngOnInit(): void {
    this.notifyService.startConnection();
  }

  send(): void {
    if (this.username && this.message) {
      this.notifyService.sendMessage(this.username, this.message);
      this.message = '';
    }
  }
}
