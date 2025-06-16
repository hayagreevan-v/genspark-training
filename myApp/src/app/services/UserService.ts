import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { UserLoginModel } from "../models/UserLoginModel";

@Injectable()
export class UserService {
    private usernameSubject : BehaviorSubject<string|null> = new BehaviorSubject<string|null>(null);
    public username$ = this.usernameSubject.asObservable();

    validateUserLogin(user:UserLoginModel)
    {
        if(user.username.length<3)
        {
            this.usernameSubject.next(null);
            // this.usernameSubject.error("Too short for username");
        }
            
        else
            this.usernameSubject.next(user.username);
    }

    logout(){
        this.usernameSubject.next(null);
    }
}
