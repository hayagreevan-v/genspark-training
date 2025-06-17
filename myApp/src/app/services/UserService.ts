import { inject, Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { UserLoginModel } from "../models/UserLoginModel";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { UserProfileModel } from "../models/UserProfileModel";

@Injectable()
export class UserService {
    private usernameSubject : BehaviorSubject<string|null> = new BehaviorSubject<string|null>(null);
    public username$ = this.usernameSubject.asObservable();
    private http = inject(HttpClient);

    validateUserLogin(user:UserLoginModel)
    {
        if(user.username.length<3)
        {
            this.usernameSubject.next(null);
            // this.usernameSubject.error("Too short for username");
        }
            
        else{
            this.callLoginAPI(user).subscribe({
                next: (data:any) =>{
                    console.log(data);
                    this.usernameSubject.next(user.username);
                    localStorage.setItem("token",data.accessToken)
                },
                error : (err)=>{
                    console.log(err);
                    console.log(err.error.message);
                }
            })
        }
    }

    callLoginAPI(user:UserLoginModel){
        return this.http.post('https://dummyjson.com/auth/login',user);
    }
    callGetProfile(){
        const token = localStorage.getItem("token");
        const httpHeader = new HttpHeaders({
            'Authorization':`Bearer ${token}`
        });
        return this.http.get('https://dummyjson.com/auth/me',{headers : httpHeader});
    }

    logout(){
        this.usernameSubject.next(null);
    }
}
