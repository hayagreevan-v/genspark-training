import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LoginService } from '../Services/LoginService';
import { LoginCredentials } from '../Models/LoginCredentials';

@Component({
  selector: 'app-loginpage',
  imports: [FormsModule],
  templateUrl: './loginpage.html',
  styleUrl: './loginpage.css'
})
export class Loginpage {
  username :string | undefined;
  password :string | undefined;
  constructor(private loginService:LoginService){}
  login(){
    let verified = this.loginService.validate(new LoginCredentials(this.username,this.password));
    // console.log(this.username+" "+this.password+" "+verified);
    if(verified){
      console.log("user verified");
      // localStorage.setItem("user",this.username||"user");
      sessionStorage.setItem("user",this.username||"user");
    }
    else{
      alert("Invalid username and password");
    }
  }
}
