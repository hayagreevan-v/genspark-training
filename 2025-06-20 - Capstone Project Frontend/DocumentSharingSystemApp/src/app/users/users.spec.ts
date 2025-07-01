import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Users } from './users';
import { Component, NO_ERRORS_SCHEMA } from '@angular/core';
import { DatePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { UserService } from '../services/user.service';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Store } from '@ngxs/store';
import { TeamService } from '../services/team.service';
import { TeamModel } from '../models/team.model';
import { UserModel } from '../models/user.model';
import { of } from 'rxjs';
import { UserSearchModel } from '../models/user.search.model';

@Component({
  selector: 'app-navbar',
  standalone: true,
  template: ''
})
class FakeNavbar {}
describe('Users', () => {
  let component: Users;
  let fixture: ComponentFixture<Users>;

  let userService : jasmine.SpyObj<UserService>;
  let teamService : jasmine.SpyObj<TeamService>; 
  let dialog : jasmine.SpyObj<MatDialog>; 
  let router : jasmine.SpyObj<Router>; 
  let store : jasmine.SpyObj<Store>;

    let user = new UserModel("1");
	let usersList = [new UserModel("1"), new UserModel("2")];
	let teamsList = [new TeamModel(1), new TeamModel(2)];

  beforeEach(async () => {

	let userServiceSpy = jasmine.createSpyObj("UserService",["getCurrentUserDetails","getAllUsers","deleteUserById","revokeUserById","getByFilter"]);
	let teamServiceSpy = jasmine.createSpyObj("TeamService",["getAllTeams","deleteTeam","restoreTeam","updateTeam","getByFilter","addTeam"]);
	let dialogSpy = jasmine.createSpyObj("MatDialog",["open"]);
	let routerSpy = jasmine.createSpyObj("Router",["navigateByUrl"]);
	let storeSpy = jasmine.createSpyObj("Store",["select"]);

    await TestBed.configureTestingModule({
      imports: [Users,FakeNavbar],
	  schemas: [NO_ERRORS_SCHEMA],
	  providers : [
		{provide: UserService, useValue : userServiceSpy},
		{provide: TeamService, useValue : teamServiceSpy},
		{provide: MatDialog, useValue : dialogSpy},
		{provide: Router, useValue : routerSpy},
		{provide: Store, useValue : storeSpy},
	  ]

    })
    .compileComponents();

    TestBed.overrideComponent(Users, {
        set: {
        imports: [
			FakeNavbar,
			FormsModule,
			ReactiveFormsModule,
			MatProgressSpinnerModule,
			MatTableModule,
			MatButtonModule,
			MatIconModule,
			MatExpansionModule, 
			MatFormFieldModule,  
			MatInputModule,
			MatSnackBarModule,
			MatButtonToggleModule,
			MatSelectModule,
			MatCardModule,
			MatTabsModule,
			DatePipe,
        ]
      }
    });

	userService = TestBed.inject(UserService) as jasmine.SpyObj<UserService>;
	teamService = TestBed.inject(TeamService) as jasmine.SpyObj<TeamService>;
	dialog = TestBed.inject(MatDialog) as jasmine.SpyObj<MatDialog>;
	router = TestBed.inject(Router) as jasmine.SpyObj<Router>;
	store = TestBed.inject(Store) as jasmine.SpyObj<Store>;
	// route = TestBed.inject(ActivatedRoute) as jasmine.SpyObj<ActivatedRoute>;

	store.select.and.returnValue(of(user));
	userService.getCurrentUserDetails.and.returnValue(of(user));
	userService.getAllUsers.and.returnValue(of({ data: { $values: usersList } }));
	userService.getByFilter.and.returnValue(of({ data: { $values: usersList } }));
	teamService.getByFilter.and.returnValue(of({ data: { $values: teamsList } }));
	teamService.getAllTeams.and.returnValue(of({ data: { $values: teamsList } }));

    fixture = TestBed.createComponent(Users);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should get current user data', () => {
    expect(component.currentUser).toBe(user);
  });
  it('should get users data', async () => {
		component.userSearchSubject.next(new UserSearchModel(null,null,null,null));
		await fixture.whenStable();
		fixture.detectChanges();

		expect(component.allUsers.length).toBe(2);
		expect(component.allUsers).toEqual(usersList);
	});

	it("should delete user", async () => {
		userService.deleteUserById.and.returnValue(of({data: {id:"1", name : "Test"}}))
		component.onDelete("1");

		expect(userService.deleteUserById).toHaveBeenCalledWith("1");
	})
	it("should restore user", async () => {
		userService.revokeUserById.and.returnValue(of({data: {id:"1", name : "Test"}}))
		component.onRevoke("1");

		expect(userService.revokeUserById).toHaveBeenCalledWith("1");
	})
});
