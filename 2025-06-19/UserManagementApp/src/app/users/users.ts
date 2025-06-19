import { Component, Output, WritableSignal } from '@angular/core';
import { UserService } from '../services/user.services';
import { UserModel } from '../models/user.model';
import { BehaviorSubject, debounceTime, distinctUntilChanged, switchMap } from 'rxjs';
import { User } from '../user/user';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-users',
  imports: [User,FormsModule],
  templateUrl: './users.html',
  styleUrl: './users.css'
})
export class Users {
  @Output() users : UserModel[]=[];
  searchQuery:string = "";

    searchSubject = new BehaviorSubject<string>("");
    constructor(private userService:UserService, private router : Router){
      this.userService.getUsersFromAPI();
      this.userService.users$.subscribe({
        next: (data : any)=>{
          this.users = data;
          console.log(this.users);
        }
      })
    }

    ngOnInit(){
      this.searchSubject.pipe(
        debounceTime(1000),
        distinctUntilChanged()
      ).subscribe({
        next : (query :string) =>{
          this.userService.searchByUserName(query);
        }
      })
    }
    navigateToAddUser(){
      this.router.navigateByUrl("/add-user")
    }

    search(){
      this.searchSubject.next(this.searchQuery);
      // this.userService.searchByUserName(this.searchQuery);
    }
}
