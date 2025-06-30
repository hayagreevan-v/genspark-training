import { Component, signal } from '@angular/core';
import { UserLoginModel } from '../models/user.login.model';
import { UserService } from '../services/user.service';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.css',
  providers: [
    {provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'}}
  ]
})
export class Login {
  userCredentials : UserLoginModel = new UserLoginModel();
  hidePassword = signal(true);

  fc = new FormControl(null);

  constructor(private userService:UserService ){}
  handleLogin(){
    this.userService.login(this.userCredentials).subscribe({
      next : (data : any) =>{
        // console.log(data);
        if(!data.success){
          this.fc.setErrors({server: data.error});
        }
      }
    })
  }
  passwordView(){
    this.hidePassword.set(!this.hidePassword());
  }

}
