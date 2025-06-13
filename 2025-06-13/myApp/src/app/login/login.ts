import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserLoginModel } from '../models/UserLoginModel';
import { UserService } from '../services/UserService';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  user: UserLoginModel = new UserLoginModel();
  constructor(private userService:UserService){}

  handleLogin(){
    this.userService.validateUserLogin(this.user);
  }
}
