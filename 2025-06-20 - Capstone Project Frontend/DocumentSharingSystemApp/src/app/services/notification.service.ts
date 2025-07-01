import { Injectable } from "@angular/core";
import * as signalR from "@microsoft/signalr";
import { BehaviorSubject } from "rxjs";
import { TeamService } from "./team.service";
import { Store } from "@ngxs/store";
import { CurrentUserState } from "../current-user/current-user.state";
import { UserModel } from "../models/user.model";

@Injectable()
export class NotificationService{
    hubConnection! : signalR.HubConnection;
    notifications :{user : string , message : string, teamId : number | null}[] = [];
    currentUser : UserModel | null = null;
    private notificationSubject  = new BehaviorSubject<{user : string, message : string }[]>([]);
    public notification$ = this.notificationSubject.asObservable();
    
    constructor(private store : Store){
        this.store.select(CurrentUserState.getUser).subscribe((data)=>{
            this.currentUser = data;
            if(this.currentUser != null)
                this.sendMessage(`${this.currentUser?.name} (${this.currentUser?.email})`, 'Connected');
        })
    }
    

    startConnection(){
        this.hubConnection= new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:7120/notification", {withCredentials: true,transport: signalR.HttpTransportType.WebSockets})
            .withAutomaticReconnect()
            .build();

        this.hubConnection.start()
            .then(() => console.log("SignalR connected"))
            .catch((ex) => console.log(ex));
        
        this.hubConnection.on("RecieveMessage",(user : string, message : string) => {
            this.notifications.push({user: user, message:message, teamId: null});
            this.notificationSubject.next(this.notifications);
        })
        this.hubConnection.on("RecieveTeamMessage",(user : string, message : string, teamId : number | null) => {
            if(teamId == this.currentUser?.teamId){
                this.notifications.push({user: user, message:message, teamId : teamId});
                this.notificationSubject.next(this.notifications);
            }
        })
    }
    sendMessage(user: string, message : string){
        this.hubConnection.invoke("SendMessage",user,message)
        .then(()=> console.log(`Message sent : ${user} - ${message}`))
        .catch((ex)=> console.log(ex));
    }
    sendTeamMessage(user: string, message : string, teamId : number | null){
        this.hubConnection.invoke("SendTeamMessage",user,message,teamId)
        .then(()=> console.log(`Message sent : Team ${teamId} - ${user} - ${message}`))
        .catch((ex)=> console.log(ex));
    }
}