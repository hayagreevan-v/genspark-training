import { HttpClient } from "@angular/common/http";
import { TestBed } from "@angular/core/testing";
import { UserModel } from "../models/user.model";
import { of } from "rxjs";
import { environment } from "../../environments/environment";
import { TeamService } from "./team.service";
import { TeamModel } from "../models/team.model";
import { NotificationService } from "./notification.service";
import { Store } from "@ngxs/store";
import * as signalR from "@microsoft/signalr";

describe("NotificationService",() =>{
    let service : NotificationService;
    let storeSpy : jasmine.SpyObj<Store>;
    let mockConnection : signalR.HubConnection;

    let user = new UserModel("1");
    user.accessToken = "access-token";


    beforeEach(() => {
        storeSpy = jasmine.createSpyObj("Store",["select"]);
        storeSpy.select.and.returnValue(of(user));
        spyOn(NotificationService.prototype, 'sendMessage').and.callFake(() => {});
        // spyOn(NotificationService.prototype, 'sendTeamMessage').and.callFake(() => {});

        mockConnection = {
            start: jasmine.createSpy().and.returnValue(Promise.resolve()),
            invoke: jasmine.createSpy().and.returnValue(Promise.resolve()),
            on: jasmine.createSpy()
        } as unknown as signalR.HubConnection;
        
        
        spyOn(signalR.HubConnectionBuilder.prototype, 'withUrl').and.returnValue(signalR.HubConnectionBuilder.prototype);
        spyOn(signalR.HubConnectionBuilder.prototype, 'withAutomaticReconnect').and.returnValue(signalR.HubConnectionBuilder.prototype);
        spyOn(signalR.HubConnectionBuilder.prototype, 'build').and.returnValue(mockConnection);
        
        
        TestBed.configureTestingModule({
            imports :[],
            providers :[
                NotificationService,
                {provide : Store, useValue : storeSpy}
            ]
        });
        
        service = TestBed.inject(NotificationService);
        service.hubConnection = mockConnection;
        
    })

    it("should be created", () => {
        expect(service).toBeTruthy();
    })
    it("should start connection", () => {

        service.startConnection();

        expect(mockConnection.start).toHaveBeenCalled();
        expect(mockConnection.on).toHaveBeenCalledTimes(2);
    })
    it("should send message", () => {
        (service.sendMessage as jasmine.Spy).and.callThrough();
        service.sendMessage("user","message");
        expect(mockConnection.invoke).toHaveBeenCalledOnceWith("SendMessage","user","message");
    })
    it("should send team message", () => {
        // (service.sendTeamMessage as jasmine.Spy).and.callThrough();
        service.sendTeamMessage("user","message",1);
        expect(mockConnection.invoke).toHaveBeenCalledOnceWith("SendTeamMessage","user","message",1);
    })
    
   
})