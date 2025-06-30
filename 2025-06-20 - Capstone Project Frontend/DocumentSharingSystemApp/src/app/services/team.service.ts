import { inject, Injectable } from "@angular/core";
import { UserModel } from "../models/user.model";
import { environment } from "../../environments/environment";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class TeamService{
    private http = inject(HttpClient);

    getAllTeams(user : UserModel) : Observable<any>{
        return this.http.get(environment.serverUrl+'/teams',{
                    headers :{
                        Authorization: `Bearer ${user.accessToken}`
                    }
        })
    }

}