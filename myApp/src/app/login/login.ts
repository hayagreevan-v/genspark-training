import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserLoginModel } from '../models/UserLoginModel';
import { UserService } from '../services/UserService';
import { Router } from '@angular/router';
import { TextValidator } from '../misc/TextValidator';

@Component({
  selector: 'app-login',
  imports: [FormsModule,ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  user: UserLoginModel = new UserLoginModel();
  loginForm : FormGroup;
  constructor(private userService:UserService, private router:Router){
    this.loginForm = new FormGroup({
      un: new FormControl(null,Validators.required),
      pass: new FormControl(null,[Validators.required,TextValidator()]),
    })
  }

  public get un():any {
    return this.loginForm.get("un");
  }
  public get pass():any {
    return this.loginForm.get("pass");
  }
  // handleLogin(){
  //   this.userService.validateUserLogin(this.user);
  //   this.router.navigateByUrl(`/home/${this.user.username}`);
  // }

  handleLogin(){
    // console.log(un);
    // console.log(pass);
    // if(un.control.errors || pass.control.errors){
    //   return;
    // }
    console.log(this.loginForm);
    if(this.loginForm.invalid){
      return;
    }
    this.userService.validateUserLogin(this.user);
    this.router.navigateByUrl(`/home/${this.user.username}`);
  }
}
