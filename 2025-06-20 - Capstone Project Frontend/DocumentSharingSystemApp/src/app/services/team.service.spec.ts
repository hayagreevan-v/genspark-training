import { HttpClient } from "@angular/common/http";
import { TestBed } from "@angular/core/testing";
import { UserModel } from "../models/user.model";
import { of } from "rxjs";
import { environment } from "../../environments/environment";
import { TeamService } from "./team.service";
import { TeamModel } from "../models/team.model";

describe("TeamService",() =>{
    let service : TeamService;
    let httpSpy : jasmine.SpyObj<HttpClient>;

    let user = new UserModel("1");
    user.accessToken = "access-token";

    let teams  = [new TeamModel(1), new TeamModel(2)];

    beforeEach(() => {
        httpSpy = jasmine.createSpyObj("HttpClient",["get","post","put","delete"]);
        TestBed.configureTestingModule({
            imports :[],
            providers :[
                TeamService,
                {provide : HttpClient, useValue : httpSpy}
            ]
        });

        service = TestBed.inject(TeamService);
    })

    it("should be created", () => {
        expect(service).toBeTruthy();
    })
    it("should get all teams", () => {
        let mockRes = {data: {$values : teams}}
        httpSpy.get.and.returnValue(of(mockRes));
        service.getAllTeams(user)?.subscribe((res)=>{
            expect(res).toBe(mockRes);
            expect(httpSpy.get).toHaveBeenCalledOnceWith(environment.serverUrl+'/teams',{
                            headers : {
                                Authorization : `Bearer ${user.accessToken}`
                            }
                        })
        })
    })
    it("should get teams by filter", () => {
        let mockRes = {data: {$values : teams}}
        httpSpy.get.and.returnValue(of(mockRes));

        service.getByFilter("search",user)?.subscribe((res)=>{
            expect(res).toBe(mockRes);
            expect(httpSpy.get).toHaveBeenCalledOnceWith(environment.serverUrl+'/teams/filter?searchQuery='+"search",{
                            headers : {
                                Authorization : `Bearer ${user.accessToken}`
                            }
                        })
        })
    })
    it("should delete team", () => {
        let mockRes = {data: teams[0]}
        httpSpy.delete.and.returnValue(of(mockRes));

        service.deleteTeam(1,user)?.subscribe((res)=>{
            expect(res).toBe(mockRes);
            expect(httpSpy.delete).toHaveBeenCalledOnceWith(environment.serverUrl+'/teams/'+"1",{
                            headers : {
                                Authorization : `Bearer ${user.accessToken}`
                            }
                        })
        })
    })
    it("should add team", () => {
        let mockRes = {data: teams[0]}
        httpSpy.post.and.returnValue(of(mockRes));

        service.addTeam("newTeam",user)?.subscribe((res)=>{
            expect(res).toBe(mockRes);
            expect(httpSpy.post).toHaveBeenCalledOnceWith(environment.serverUrl+'/teams?name='+"newTeam",null,{
                            headers : {
                                Authorization : `Bearer ${user.accessToken}`
                            }
                        })
        })
    })
    it("should restore team", () => {
        let mockRes = {data: teams[0]}
        httpSpy.post.and.returnValue(of(mockRes));

        service.restoreTeam(1,user)?.subscribe((res)=>{
            expect(res).toBe(mockRes);
            expect(httpSpy.post).toHaveBeenCalledOnceWith(environment.serverUrl+'/teams/restore/'+"1",null,{
                            headers : {
                                Authorization : `Bearer ${user.accessToken}`
                            }
                        })
        })
    })

    it("should update team", () => {
        let mockRes = {data: teams[0]}
        httpSpy.put.and.returnValue(of(mockRes));

        service.updateTeam(1,"name",user)?.subscribe((res)=>{
            expect(res).toBe(mockRes);
            expect(httpSpy.put).toHaveBeenCalledOnceWith(environment.serverUrl+'/teams/'+"1"+"?name="+"name",null,{
                            headers : {
                                Authorization : `Bearer ${user.accessToken}`
                            }
                        })
        })
    })
})