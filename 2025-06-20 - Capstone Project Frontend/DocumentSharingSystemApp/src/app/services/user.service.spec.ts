import { TestBed } from "@angular/core/testing"
import { HttpClient, provideHttpClient } from "@angular/common/http"
import { HttpTestingController, provideHttpClientTesting } from "@angular/common/http/testing"
import { UserService } from "./user.service";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Router } from "@angular/router";
import { Store } from "@ngxs/store";
import { UserLoginModel } from "../models/user.login.model";
import { Observable, of } from "rxjs";
import { RemoveCurrentUserAction, SetCurrentUserAction } from "../current-user/current-user.actions";
import { UserModel } from "../models/user.model";
import { environment } from "../../environments/environment";
import { UserUpdateModel } from "../models/user.update.model";
import { UserSearchModel } from "../models/user.search.model";
import { UserAddModel } from "../models/user.add.model";

describe('UserService', () => {
    let service : UserService;
    let httpSpy: jasmine.SpyObj<HttpClient>;
    let routerSpy: jasmine.SpyObj<Router>;
    let snackBarSpy: jasmine.SpyObj<MatSnackBar>;
    let storeSpy: jasmine.SpyObj<Store>;

    let creds = new UserLoginModel();
    creds.email = 'test@example.com';
    creds.password = 'password';

    const mockResponse = {
      accessToken: 'access-token',
      refreshToken: 'refresh-token'
    };
    const mockUser = new UserModel("1","Test");
    mockUser.accessToken = "access-token";

    beforeEach(() => {
        const http = jasmine.createSpyObj('HttpClient', ['post', 'get', 'put', 'delete']);
        const router = jasmine.createSpyObj('Router', ['navigateByUrl']);
        const snackBar = jasmine.createSpyObj('MatSnackBar', ['open']);
        const store = jasmine.createSpyObj('Store', ['dispatch']);
        TestBed.configureTestingModule({
            imports : [],
            providers : [
                UserService, 
                { provide: HttpClient, useValue: http },
                { provide: Router, useValue: router },
                { provide: MatSnackBar, useValue: snackBar },
                { provide: Store, useValue: store }
            ]
            
        });
        service = TestBed.inject(UserService);
        httpSpy = TestBed.inject(HttpClient) as jasmine.SpyObj<HttpClient>;
        routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;
        snackBarSpy = TestBed.inject(MatSnackBar) as jasmine.SpyObj<MatSnackBar>;
        storeSpy = TestBed.inject(Store) as jasmine.SpyObj<Store>;

    })
    
    it('should be created', () => {
        expect(service).toBeTruthy();
    });
    
    it('should login successfully', () => {
        httpSpy.post.and.returnValue(of(mockResponse));
        spyOn(service, 'getCurrentUserDetails').and.returnValue(of(null));
        
        service.login(creds).subscribe((result: any) => {
            expect(result).toEqual(jasmine.any(Observable));
            expect(service.accessToken).toBe('access-token');
            expect(service.refreshToken).toBe('refresh-token');
            expect(localStorage.getItem('refreshToken')).toBe('refresh-token');
            expect(routerSpy.navigateByUrl).toHaveBeenCalledWith('/documents');
            
        });
    });
    it('should logout successfully', async() => {
        httpSpy.post.and.returnValue(of(mockResponse));
            await service.logout();
            expect(service.accessToken).toBe('');
            expect(service.refreshToken).toBe('');
            expect(localStorage.getItem('refreshToken')).toBeNull();
            expect(service.user).toBeNull();
            expect(storeSpy.dispatch).toHaveBeenCalledWith(new RemoveCurrentUserAction());
            expect(routerSpy.navigateByUrl).toHaveBeenCalledWith('/');
    });

    it('should get current user details successfully', () => {
        localStorage.clear();
        localStorage.setItem('refreshToken', 'refresh-token');

        const mockRefreshResponse = { accessToken: 'new-access-token' };
        const mockMeResponse = { data: mockUser };
        storeSpy.dispatch.and.returnValue(of());
        httpSpy.post.and.returnValue(of(mockRefreshResponse));
        httpSpy.get.and.returnValue(of(mockMeResponse));
        
        service.getCurrentUserDetails().subscribe((result) => {
            expect(result).toEqual(jasmine.any(UserModel));
            expect(service.accessToken).toBe('new-access-token');
            expect(storeSpy.dispatch).toHaveBeenCalled();
        });
    });
    
    it("should get All Users",() =>{
        service.accessToken="access-token";
        httpSpy.get.and.returnValue(of({$values :[mockUser]}));

        service.getAllUsers().subscribe((res)=>{
            expect(res).toEqual({ $values: [mockUser] })
            expect(httpSpy.get).toHaveBeenCalledOnceWith(environment.serverUrl+'/users/all',{
                headers :{
                    Authorization: `Bearer ${service.accessToken}`
                }
            })
        });
        
    })
    
    it("should get User By Id",() =>{
        service.accessToken="access-token";
        httpSpy.get.and.returnValue(of({data :mockUser}));
        
        service.getUserById("1").subscribe((res)=>{
            expect(res).toEqual({ data: mockUser })
            expect(httpSpy.get).toHaveBeenCalledOnceWith(environment.serverUrl+'/users/'+"1",{
                headers :{
                    Authorization: `Bearer ${service.accessToken}`
                }
            })
        });
        
    })
    it("should update User By Id",() =>{
        service.accessToken="access-token";
        httpSpy.put.and.returnValue(of({data :mockUser}));
        let userUpdate = new UserUpdateModel()
        
        service.updateUserById("1",userUpdate).subscribe((res)=> {
            expect(res).toEqual({ data: mockUser })
            expect(httpSpy.put).toHaveBeenCalledOnceWith(environment.serverUrl+'/users/'+"1",userUpdate,{
                        headers :{
                            Authorization: `Bearer ${service.accessToken}`
                        }
            })
        });
    })
    it("should change User role By Id",() =>{
        service.accessToken="access-token";
        httpSpy.post.and.returnValue(of({data :mockUser}));
        
        service.changeUserRoleById("1","Admin").subscribe((res)=> {
            expect(res).toEqual({ data: mockUser })
            expect(httpSpy.post).toHaveBeenCalledOnceWith(environment.serverUrl+'/users/revoke/'+"1"+"?role="+"Admin",null,{
                headers :{
                    Authorization: `Bearer ${service.accessToken}`
                }
            })
        });
    })
    it("should delete User By Id",() =>{
        service.accessToken="access-token";
        httpSpy.delete.and.returnValue(of({data :mockUser}));
        
        service.deleteUserById("1").subscribe((res)=> {
            expect(res).toEqual({ data: mockUser })
            expect(httpSpy.delete).toHaveBeenCalledOnceWith(environment.serverUrl+'/users/'+"1",{
                        headers :{
                            Authorization: `Bearer ${service.accessToken}`
                        }
            })
        });
    })
    it("should revoke User By Id",() =>{
        service.accessToken="access-token";
        httpSpy.post.and.returnValue(of({data :mockUser}));
        
        service.revokeUserById("1").subscribe((res)=> {
            expect(res).toEqual({ data: mockUser })
            expect(httpSpy.post).toHaveBeenCalledOnceWith(environment.serverUrl+'/users/revoke/'+"1",null,{
                        headers :{
                            Authorization: `Bearer ${service.accessToken}`
                        }
            })
        });
    })
    it("should get User By Filter",() =>{
        service.accessToken="access-token";
        httpSpy.post.and.returnValue(of({data :mockUser}));
        let userSearch = new UserSearchModel(null,null,null,null);
        service.getByFilter(userSearch).subscribe((res)=> {
            expect(res).toEqual({ data: mockUser })
            expect(httpSpy.post).toHaveBeenCalledOnceWith(environment.serverUrl+'/users/filter',userSearch,{
                        headers :{
                            Authorization: `Bearer ${service.accessToken}`
                        }
            })
        });
    })
    it("should add User",() =>{
        service.accessToken="access-token";
        httpSpy.post.and.returnValue(of({data :mockUser}));
        let userAdd = new UserAddModel("","","",1,"");
        service.addUser(userAdd).subscribe((res)=> {
            expect(res).toEqual({ data: mockUser })
            expect(httpSpy.post).toHaveBeenCalledOnceWith(environment.serverUrl+'/users',userAdd,{
                        headers :{
                            Authorization: `Bearer ${service.accessToken}`
                        }
            })
        });
    })
})