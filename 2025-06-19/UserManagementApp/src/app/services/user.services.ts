import { HttpClient } from "@angular/common/http"
import { inject, Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { UserModel } from "../models/user.model";

@Injectable()
export class UserService {
    private http = inject(HttpClient);
    private users : UserModel[] =[];
    private userSubject : BehaviorSubject<UserModel[]> = new BehaviorSubject<UserModel[]>([]);
    public users$ : Observable<UserModel[]> = this.userSubject.asObservable();
    
    getUsersFromAPI(){
        if(this.users.length>0) return;
        this.http.get('https://dummyjson.com/users?limit=300').subscribe({
            next : (data : any) =>{
                 data.users.forEach((u:any) => {
                    this.users.push(new UserModel(u.id,u.username,u.email,u.password,u.role));
                });
                this.userSubject.next(this.users);
            }
        })
    }
    searchByUserName(query : string){
        let lowerCaseSearchQuery = query.toLowerCase();
        let filteredUser : UserModel[] = [];
        this.users.forEach((user: UserModel) =>{
            if(user.username.toLowerCase().indexOf(lowerCaseSearchQuery)!=-1){
                filteredUser.push(user);
            }
        });
        this.userSubject.next(filteredUser);
    }
    addUser(user : UserModel){
        this.users.push(user);
        this.userSubject.next(this.users);
        console.log(this.users);
    }
}