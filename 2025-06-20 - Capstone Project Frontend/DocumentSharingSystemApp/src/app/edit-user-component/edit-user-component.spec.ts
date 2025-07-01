import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EditUserComponent } from './edit-user-component';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Store } from '@ngxs/store';
import { TeamService } from '../services/team.service';
import { UserService } from '../services/user.service';
import { Component, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { of } from 'rxjs';
import { TeamModel } from '../models/team.model';
import { UserModel } from '../models/user.model';

@Component({
  selector: 'app-navbar',
  standalone: true,
  template: ''
})
class FakeNavbar {}

describe('EditUserComponent', () => {
  let component: EditUserComponent;
  let fixture: ComponentFixture<EditUserComponent>;
  let userService : jasmine.SpyObj<UserService>;
  let teamService : jasmine.SpyObj<TeamService>; 
  let route: jasmine.SpyObj<ActivatedRoute>; 
  let router : jasmine.SpyObj<Router>; 
  let store : jasmine.SpyObj<Store>;

  let user = new UserModel("1");
  let teamsList = [new TeamModel(1), new TeamModel(2)]
  

  beforeEach(async () => {
    userService = jasmine.createSpyObj("UserService",["getCurrentUserDetails","getUserById","updateUserById","addUser","logout","changeUserRoleById"])
    teamService = jasmine.createSpyObj("TeamService",["getAllTeams"])
    router = jasmine.createSpyObj("Router",["navigateByUrl","navigate"]);
    route = jasmine.createSpyObj("AcivatedRoute",["navigate"],
        {snapshot: {
          url : [null,{path: "Add"}], 
          paramMap: {
            get: (id : string)=>{
              return "test"
            }
          }
        }});
    store = jasmine.createSpyObj("Store",["select"]);

    await TestBed.configureTestingModule({
      imports: [EditUserComponent, FakeNavbar],
      schemas : [NO_ERRORS_SCHEMA],
      providers :[
        FormsModule, 
        ReactiveFormsModule, 
        CommonModule, 
        RouterLink, 
        MatFormFieldModule, 
        MatInputModule, 
        MatIconModule, 
        MatButtonModule, 
        MatSelectModule,
        {provide: UserService, useValue : userService},
        {provide: TeamService, useValue : teamService},
        {provide: ActivatedRoute, useValue : route},
        {provide: Router, useValue : router},
        {provide: Store, useValue : store},
      ]
    })
    .compileComponents();

    TestBed.overrideComponent(EditUserComponent, {
		set: {
		imports: [
			FakeNavbar,
			FormsModule, 
        ReactiveFormsModule, 
        CommonModule, 
        RouterLink, 
        MatFormFieldModule, 
        MatInputModule, 
        MatIconModule, 
        MatButtonModule, 
        MatSelectModule,
			]
		}
	});

      store.select.and.returnValue(of(user));
      userService.getCurrentUserDetails.and.returnValue(of(user));
      userService.getUserById.and.returnValue(of(user));
      userService.addUser.and.returnValue(of({ data: user }));
      userService.changeUserRoleById.and.returnValue(of({ data: user }));
      teamService.getAllTeams.and.returnValue(of({ data: { $values: teamsList } }));

    fixture = TestBed.createComponent(EditUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should get current user data', () => {
    expect(component.currentUser).toBe(user);
  });

  it('should load teams', () => {
    expect(teamService.getAllTeams).toHaveBeenCalledWith(component.currentUser!);
    expect(component.teams).toEqual(teamsList);
  });

  it('should add user', () => {
    component.formGroup = new FormGroup({
      name: new FormControl('Test User'),
      email: new FormControl('test@example.com'),
      role: new FormControl('User'),
      teamId: new FormControl(1),
      password: new FormControl('Password123!'),
      confirmPassword: new FormControl('Password123!')
    });

    component.handleAdd();

    expect(userService.addUser).toHaveBeenCalled();
  });
  it('should update user', () => {

    component.formGroup = new FormGroup({
      name: new FormControl('Test User'),
      email: new FormControl('test@example.com'),
      role: new FormControl('User'),
      teamId: new FormControl(1),
      password: new FormControl('Password123!'),
      confirmPassword: new FormControl('Password123!')
    });

	component.disabled.set(false);
	component.editUser = user;
    component.handleUpdate();
    expect(userService.updateUserById).toHaveBeenCalled();
  });
});
