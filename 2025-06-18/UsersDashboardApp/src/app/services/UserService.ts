import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";

@Injectable()
export class UserService {
    http = inject(HttpClient);
    getUsers(){
        return this.http.get("https://dummyjson.com/users?limit=300");
    }
}