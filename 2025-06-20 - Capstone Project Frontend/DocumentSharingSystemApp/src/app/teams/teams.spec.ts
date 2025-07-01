import { ComponentFixture, fakeAsync, flush, TestBed, tick } from '@angular/core/testing';

import { Teams } from './teams';
import { DatePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { Navbar } from '../navbar/navbar';
import { UserService } from '../services/user.service';
import { TeamService } from '../services/team.service';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngxs/store';
import { UserModel } from '../models/user.model';
import { of } from 'rxjs';
import { Component, NO_ERRORS_SCHEMA } from '@angular/core';
import { TeamModel } from '../models/team.model';

@Component({
  selector: 'app-navbar',
  standalone: true,
  template: ''
})
class FakeNavbar {}

describe('Teams', () => {
  let component: Teams;
  let fixture: ComponentFixture<Teams>;
  let userService : jasmine.SpyObj<UserService>;
  let teamService : jasmine.SpyObj<TeamService>; 
  let dialog : jasmine.SpyObj<MatDialog>; 
  let router : jasmine.SpyObj<Router>; 
  let store : jasmine.SpyObj<Store>;
//   let route : jasmine.SpyObj<ActivatedRoute>; 

  let user = new UserModel("1");
  let teamsList = [new TeamModel(1), new TeamModel(2)]

  beforeEach(async () => {
	let userServiceSpy = jasmine.createSpyObj("UserService",["getCurrentUserDetails"]);
	let teamServiceSpy = jasmine.createSpyObj("TeamService",["getAllTeams","deleteTeam","restoreTeam","updateTeam","getByFilter","addTeam"]);
	let dialogSpy = jasmine.createSpyObj("MatDialog",["open"]);
	let routerSpy = jasmine.createSpyObj("Router",["navigateByUrl"]);
	let storeSpy = jasmine.createSpyObj("Store",["select"]);
	// let routeSpy = jasmine.createSpyObj("ActivatedRoute",["snapshot"]);
    await TestBed.configureTestingModule({
      imports: [
		Teams, 
		FakeNavbar
	],
	//   declarations: [FakeNavbar],
	  schemas: [NO_ERRORS_SCHEMA],
      providers:[
        MatListModule,
        MatButtonModule,
        MatInputModule,
        FormsModule,
        ReactiveFormsModule,
        MatProgressSpinnerModule,
        MatButtonModule,
        MatIconModule,
        MatExpansionModule, 
        MatFormFieldModule,  
        MatInputModule,
        MatSnackBarModule,
        DatePipe,
		{provide: UserService, useValue : userServiceSpy},
		{provide: TeamService, useValue : teamServiceSpy},
		{provide: MatDialog, useValue : dialogSpy},
		{provide: Router, useValue : routerSpy},
		{provide: Store, useValue : storeSpy},
		// {provide: ActivatedRoute, useValue : routeSpy}
      ]
    })
    .compileComponents();

	TestBed.overrideComponent(Teams, {
		set: {
		imports: [
			FakeNavbar,
			MatListModule,
			MatButtonModule,
			MatInputModule,
			FormsModule,
			ReactiveFormsModule,
			MatProgressSpinnerModule,
			MatButtonModule,
			MatIconModule,
			MatExpansionModule, 
			MatFormFieldModule,  
			MatInputModule,
			MatSnackBarModule,
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
	teamService.getByFilter.and.returnValue(of({ data: { $values: teamsList } }));

    fixture = TestBed.createComponent(Teams);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should get current user data', () => {
    expect(component.currentUser).toBe(user);
  });
  	it('should get teams data', async () => {
		teamService.getByFilter.and.returnValue(of({ data: { $values: teamsList } }));

		component.teamSearchSubject.next("");
		await fixture.whenStable();
		fixture.detectChanges();

		expect(component.allTeams.length).toBe(2);
		expect(component.allTeams).toEqual(teamsList);
	});

	it('should call getByFilter on teamService when searching', async () => {
		teamService.getByFilter.and.returnValue(of({ data: { $values: teamsList } }));

		component.teamSearchSubject.next("search-term");
		await fixture.whenStable();
		fixture.detectChanges();

		expect(teamService.getByFilter).toHaveBeenCalledWith("search-term",user);
	});

	it("should delete team", async () => {
		teamService.deleteTeam.and.returnValue(of({data: {id:1, name : "Test"}}))
		component.onDelete(1);

		expect(teamService.deleteTeam).toHaveBeenCalledWith(1,user);
	})
	it("should restore team", async () => {
		teamService.restoreTeam.and.returnValue(of({data: {id:1, name : "Test"}}))
		component.onRestore(1);

		expect(teamService.restoreTeam).toHaveBeenCalledWith(1,user);
	})

});
