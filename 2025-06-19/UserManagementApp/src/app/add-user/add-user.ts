import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserModel } from '../models/user.model';
import { passwordCheck } from '../misc/passwordCheck';
import { confirmPasswordCheck } from '../misc/confirmPasswordCheck';
import { UserService } from '../services/user.services';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-add-user',
  imports: [FormsModule,ReactiveFormsModule],
  templateUrl: './add-user.html',
  styleUrl: './add-user.css'
})
export class AddUser {
  userdetails : UserModel = new UserModel();
  confirmpassword : string= "";
    addUserForm : FormGroup = new FormGroup({
      username : new FormControl(null,Validators.required),
      role : new FormControl(null,Validators.required),
      email : new FormControl(null,[Validators.required,Validators.email]),
      password : new FormControl(null,[Validators.required,passwordCheck()]),
      confirmPassword : new FormControl(null,[Validators.required,passwordCheck()])
    }, { validators: confirmPasswordCheck() })

    constructor(private userService : UserService, private router:Router){}


    public get username() : any {
      return this.addUserForm.get("username");
    }
    public get role() : any {
      return this.addUserForm.get("role");
    }
    public get email() : any {
      return this.addUserForm.get("email");
    }
    public get password() : any {
      return this.addUserForm.get("password");
    }
    public get confirmPassword() : any {
      return this.addUserForm.get("confirmPassword");
    }

    handleSubmit(){
      console.log(this.addUserForm);
      this.userService.addUser(this.userdetails);
      alert("User Added Successfully");
      this.router.navigateByUrl("/users")
    }
}
