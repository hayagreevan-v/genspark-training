import { HttpClient, HttpHeaders } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { UserLoginModel } from "../models/UserLoginModel";
import { Router } from "@angular/router";

@Injectable()
export class UserService {
    private http = inject(HttpClient);
    private router = inject(Router)

    validateLogin(userLogin : UserLoginModel){
        this.callLoginAPI(userLogin).subscribe({
            next: (data:any)=>{
                localStorage.setItem("token",data.accessToken);
                alert("Successfully logged in");
                this.router.navigateByUrl("/home");
            },
            error : (err) =>{
                alert(err.error.message);
            }
        })
    }
    callLoginAPI(user : UserLoginModel){
        return this.http.post('https://dummyjson.com/auth/login',user);
    }
    callGetDetails(user : UserLoginModel){
        const token = localStorage.getItem("token");
        let header = new HttpHeaders({
            Authorization : `Bearer ${token}`
        });
        return this.http.get('https://dummyjson.com/auth/me',{headers:header});
    }
}