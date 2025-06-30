import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { BehaviorSubject, catchError, finalize, map, Observable, of, switchMap } from "rxjs";
import { UserModel } from "../models/user.model";
import { UserLoginModel } from "../models/user.login.model";
import { environment } from "../../environments/environment";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Router } from "@angular/router";
import { UserUpdateModel } from "../models/user.update.model";
import { UserSearchModel } from "../models/user.search.model";
import { UserAddModel } from "../models/user.add.model";
import { Store } from "@ngxs/store";
import { RemoveCurrentUserAction, SetCurrentUserAction } from "../current-user/current-user.actions";

@Injectable()
export class UserService {
    private http = inject(HttpClient);
    private router = inject(Router);
    private store = inject(Store);
    private userSubject : BehaviorSubject<UserModel| null> = new BehaviorSubject<UserModel|null>(null);
    public user$ : Observable<UserModel | null> = this.userSubject.asObservable();


    private snackBar : MatSnackBar = new MatSnackBar();

    accessToken : string = "";
    refreshToken : string = "";
    user : UserModel | null = null;

    loginSubject : BehaviorSubject<{success : boolean, error : string | null}> = new BehaviorSubject<{success : boolean, error : string | null}>({success : false, error : null});

    login(creds : UserLoginModel) {
        return this.http.post(environment.serverUrl+'/auth/login',creds).pipe(
                map((res: any) => {
                    this.accessToken = res.accessToken;
                    this.refreshToken = res.refreshToken;
                    // localStorage.setItem('accessToken',res.accessToken);
                    localStorage.setItem('refreshToken',res.refreshToken);
                    this.getCurrentUserDetails();
                    this.snackBar.open("Log In Success!",undefined,{duration:2000})
                    this.router.navigateByUrl("/documents"); 
                    return of({success : true, error : ""})
                }),
                catchError((err : any) =>{
                    console.log(err);
                    return of({success : false, error : err.error.errors.message})

                })
            )
    }
    // login(creds : UserLoginModel)
    logout(){
        this.http.post(environment.serverUrl+'/auth/logout',{},{
            headers: {
                Authorization: `Bearer ${this.accessToken}`
            },
            responseType: 'text'
        }).pipe(
            catchError((err) => {
                console.error(err);
                return (of(null));
            }),
            finalize(()=> {
                localStorage.removeItem('accessToken');
                localStorage.removeItem('refreshToken');
                this.store.dispatch(new RemoveCurrentUserAction());
                this.accessToken = "";
                this.refreshToken = "";
                this.user = null;
                this.userSubject.next(this.user);
                this.router.navigateByUrl("/");
            })
        )
        .subscribe();
    }
    getCurrentUserDetails() : Observable<UserModel | null>{
        let rt = localStorage.getItem('refreshToken');
        if(rt == null){
            console.log('refreshToken not found');
            this.router.navigateByUrl("/"); 
            return of(null);
        }
        return this.http.post(environment.serverUrl+`/auth/refresh?token=${rt}`,{})
        .pipe(
            switchMap((data: any) => {
                this.accessToken = data.accessToken;
                
                return this.http.get(`${environment.serverUrl}/auth/me`, {
                    headers: {
                        Authorization: `Bearer ${this.accessToken}`
                    }
                });
            }),
            map((data:any) => {
                this.user = data.data;
                this.user!.accessToken =this.accessToken;
                this.user!.refreshToken =this.refreshToken;
                this.store.dispatch(new SetCurrentUserAction(this.user));
                this.userSubject.next(this.user);
                return this.user;
                
                }),
                catchError((err: any) => {
                        console.error('Error during login:', err);
                        this.snackBar.open("Session expired. Please login again.", undefined, { duration: 3000 });
                        // this.router.navigateByUrl('/');
                        return of(null);
                })
            )
        }
    
    getAllUsers(){
        return this.http.get(environment.serverUrl+'/users/all',{
                    headers :{
                        Authorization: `Bearer ${this.accessToken}`
                    }
        })
    }

    getUserById(id : string){
        return this.http.get(environment.serverUrl+'/users/'+id,{
                    headers :{
                        Authorization: `Bearer ${this.accessToken}`
                    }
        })
    }
    updateUserById(id : string, updatedUser : UserUpdateModel){
        return this.http.put(environment.serverUrl+'/users/'+id,updatedUser,{
            headers :{
                Authorization: `Bearer ${this.accessToken}`
            }
        })
    }
    changeUserRoleById(id : string, role : string){
        return this.http.post(environment.serverUrl+`/users/revoke/${id}?role=${role}`,null,{
            headers :{
                Authorization: `Bearer ${this.accessToken}`
            }
        })
    }
    deleteUserById(id : string){
        return this.http.delete(environment.serverUrl+'/users/'+id,{
            headers :{
                Authorization: `Bearer ${this.accessToken}`
            }
        })
    }
    revokeUserById(id : string){
        return this.http.post(environment.serverUrl+`/users/revoke/${id}`,null,{
            headers :{
                Authorization: `Bearer ${this.accessToken}`
            }
        })
    }
    getByFilter(userSearch : UserSearchModel){
        return this.http.post(environment.serverUrl+'/users/filter',userSearch,{
            headers :{
                Authorization: `Bearer ${this.accessToken}`
            }
        })
    }
    
    addUser(newUser : UserAddModel){
        return this.http.post(environment.serverUrl+'/users',newUser,{
            headers :{
                Authorization: `Bearer ${this.accessToken}`
            }
        })
    }
}