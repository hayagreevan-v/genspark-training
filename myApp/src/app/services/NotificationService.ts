import * as signalR from "@microsoft/signalr";
import { BehaviorSubject } from "rxjs";
export class NotificationService {
    private hubConnection! : signalR.HubConnection;

    public messages : {user :string, message : string}[] =[];
    private messageSubject = new BehaviorSubject<{user :string, message : string}[]>([]);
    public messages$ = this.messageSubject.asObservable();

    startConnection(){
        this.hubConnection = new signalR.HubConnectionBuilder()
                                    .withUrl("https://localhost:7120/notification",{withCredentials:true})
                                    .withAutomaticReconnect()
                                    .build();

        this.hubConnection.start()
            .then(() => console.log("SignalR connected"))
            .catch((ex) => console.log(ex));

        this.hubConnection.on("RecieveMessage",(user:string,message:string) =>{
            this.messages.push({user, message});
            this.messageSubject.next(this.messages);
        })
    }
    sendMessage(user:string, message:string) {
        this.hubConnection.invoke("SendMessage",user,message)
        .then(()=> console.log(`Message sent : ${user} - ${message}`))
        .catch((ex)=> console.log(ex));
    }
}