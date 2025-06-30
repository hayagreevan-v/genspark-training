import { Injectable } from "@angular/core";
import * as signalR from "@microsoft/signalr";
import { BehaviorSubject } from "rxjs";

@Injectable()
export class NotificationService{
    hubConnection! : signalR.HubConnection;
    notifications :{user : string , message : string}[] = [];
    private notificationSubject  = new BehaviorSubject<{user : string, message : string }[]>([]);
    public notification$ = this.notificationSubject.asObservable();

    startConnection(){
        this.hubConnection= new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:7120/notification", {withCredentials: true,transport: signalR.HttpTransportType.WebSockets})
            .withAutomaticReconnect()
            .build();

        this.hubConnection.start()
            .then(() => console.log("SignalR connected"))
            .catch((ex) => console.log(ex));
        
        this.hubConnection.on("RecieveMessage",(user : string, message : string) => {
            this.notifications.push({user: user, message:message});
            this.notificationSubject.next(this.notifications);
        })
    }
    sendMessage(user: string, message : string){
        this.hubConnection.invoke("SendMessage",user,message)
        .then(()=> console.log(`Message sent : ${user} - ${message}`))
        .catch((ex)=> console.log(ex));
    }
}