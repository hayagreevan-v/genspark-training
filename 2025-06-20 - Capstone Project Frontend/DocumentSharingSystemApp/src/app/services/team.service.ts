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
    getByFilter(searchQuery : string, user : UserModel) : Observable<any>{
        return this.http.get(environment.serverUrl+`/teams/filter?searchQuery=${searchQuery}`,{
                    headers :{
                        Authorization: `Bearer ${user.accessToken}`
                    }
        })
    }
    addTeam(name : string,user : UserModel) : Observable<any>{
        return this.http.post(environment.serverUrl+`/teams?name=${name}`,null,{
                    headers :{
                        Authorization: `Bearer ${user.accessToken}`
                    }
        })
    }
    updateTeam(id: number,name : string,user : UserModel) : Observable<any>{
        return this.http.put(environment.serverUrl+`/teams/${id}?name=${name}`,null,{
                    headers :{
                        Authorization: `Bearer ${user.accessToken}`
                    }
        })
    }
    deleteTeam(id: number,user : UserModel) : Observable<any>{
        return this.http.delete(environment.serverUrl+`/teams/${id}`,{
                    headers :{
                        Authorization: `Bearer ${user.accessToken}`
                    }
        })
    }
    restoreTeam(id: number,user : UserModel) : Observable<any>{
        return this.http.post(environment.serverUrl+`/teams/restore/${id}`,null,{
                    headers :{
                        Authorization: `Bearer ${user.accessToken}`
                    }
        })
    }


}