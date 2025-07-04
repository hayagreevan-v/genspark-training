import { Component, signal } from '@angular/core';
import { Navbar } from "../navbar/navbar";
import { UserService } from '../services/user.service';
import { UserModel } from '../models/user.model';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { DatePipe } from '@angular/common';
import { MatExpansionModule } from '@angular/material/expansion';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Dialog } from '../dialog/dialog';
import { MatDialog } from '@angular/material/dialog';
import { Router, RouterOutlet } from '@angular/router';
import { UserSearchModel } from '../models/user.search.model';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { BehaviorSubject, catchError, debounceTime, of, switchMap, tap } from 'rxjs';
import { MatSelectModule } from '@angular/material/select';
import { Store } from '@ngxs/store';
import { CurrentUserState } from '../current-user/current-user.state';
import { MatCardModule } from '@angular/material/card';
import { TeamService } from '../services/team.service';
import { TeamModel } from '../models/team.model';
import { MatTabsModule } from '@angular/material/tabs';

interface selectInterface {
    value : any,
    view: string
}

@Component({
  selector: 'app-users',
  imports: [
	Navbar,
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
],
  templateUrl: './users.html',
  styleUrl: './users.css',
  providers : [{provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'}}]
})
export class Users {
	step = signal('');
	currentUser : UserModel | null = null;
	errorMessage : string | null = null;
	allUsers : any[] = [];

	displayedColumns :string[] = [];
	userSearch : UserSearchModel = new UserSearchModel("", "ascending",null,null);
	teamByList : selectInterface[] =[];


	userSearchSubject = new BehaviorSubject<UserSearchModel>(this.userSearch);
	
	private snackBar : MatSnackBar = new MatSnackBar();

	roleList : selectInterface[] = [
		{value : null, view : "All"},
		{value : 'Admin', view : 'Admin'},
		{value : 'User', view : 'User'}
	]


	constructor(private userService : UserService, private teamService: TeamService, private dialog :MatDialog, private router : Router, private store : Store){
		this.store.select(CurrentUserState.getUser).subscribe({
			next : (data:any) => {
				this.currentUser = data;
			}
		})
		if(this.currentUser==null){
			userService.getCurrentUserDetails().subscribe({
      			next : (data: UserModel | null) => {
        			this.currentUser = data;
        			if(this.currentUser==null){
						this.errorMessage = "User not Logged in!";
						return;
					}
					this.loadTeams();;
      			}
    		});
    	}else{
			this.loadTeams();
		}
		this.userSearchSubject.next(this.userSearch);
	}
	loadTeams () {
		this.teamService.getAllTeams(this.currentUser as UserModel)
			.subscribe((res : any) => {
				this.teamByList =[];
				this.teamByList.push({value : null, view: "All"});
				res.data.$values.forEach((t: TeamModel) => {
					this.teamByList.push({value: t.id, view: `${t.name} (${t.id})`})
				});
			});
	}

	teamDisabled = signal(false);
	tabs = ['All','My Team'];
  	activeTab = signal(this.tabs[0]);
  	setTab(value:string){
		if(this.activeTab() == 'My Team'){
			this.userSearch.teamId = null;
			this.teamDisabled.set(false);
		}
		if( value == 'My Team'){
			this.userSearch.teamId = this.currentUser?.teamId as number;
			this.teamDisabled.set(true);
		}
		this.userSearchSubject.next(this.userSearch);
		this.activeTab.set(value);
		console.log(value);
  	}

	getAllUsers(){
		this.userService.getAllUsers().subscribe({
			next : (data : any) =>{
				this.allUsers = data.$values;
				console.log(this.allUsers);
			}
		})
	}
	setStep(value: string) {
		this.step.set(value);
	}
	onDelete(id : string){
		this.userService.deleteUserById(id).subscribe({
			next : (data : any) => {
				console.log(`Deleted ${data.data.name} (${data.data.email})`);
				this.snackBar.open(`Deleted ${data.data.name} (${data.data.email})`,undefined,{duration: 3000});
				// this.getAllUsers();
				this.userService.getByFilter(this.userSearch).pipe(
					catchError((err)=>{
						console.error('API error:', err);
						if(err.error.errors){
							this.errorMessage = err.error.errors.message;
							this.snackBar.open(err.error.errors.message,undefined,{duration:2000})
						}
				// Return empty result or fallback
						return of({ data: { $values: [] } });
					})
				).subscribe({
					next: (res:any) => {
						console.log(res);
						this.allUsers = res.data.$values;
						console.log(this.allUsers);
					},
					error : (err) =>{
						console.log(err);
					}
				})
			}
		})
	}
	
	onRevoke(id : string){
		this.userService.revokeUserById(id).subscribe({
			next : (data : any) =>{
				console.log(`Restored ${data.data.name} (${data.data.email})`);
				this.snackBar.open(`Restored ${data.data.name} (${data.data.email})`,undefined,{duration: 3000});
				this.userSearchSubject.next(this.userSearch);
			}
		})
	}

	openDeleteDialog(message : string, id : string){
		this.dialog.open(Dialog,{
		  data : {
			message : `Want to delete ${message}`, 
			onAccept : () => {
					this.onDelete(id);
				}
			}
		})

	}

	handleEdit(id : string){
		this.router.navigate(['users','edit',id]);
	}
	handleAdd(){
		this.router.navigate(['users','add']);
	}

	onValueChange(){
		this.userSearchSubject.next(this.userSearch);
	}

	ngOnInit(){
		this.userSearchSubject.pipe(
			  debounceTime(500),
			  tap(()=> {console.log("API Called")}),
			  switchMap((query : UserSearchModel) => this.userService.getByFilter(query).pipe(
				catchError((err)=>{
				console.error('API error:', err);
				if(err.error.errors){
				  this.errorMessage = err.error.errors.message;
				  this.snackBar.open(err.error.errors.message,undefined,{duration:2000})
				}
				// Return empty result or fallback
		
				return of({ data: { $values: [] } });
				} )
			  ))
			).subscribe({
			  next: (res:any) => {
				console.log(res);
				this.allUsers = res.data.$values;
				console.log(this.allUsers);
			  },
			  error : (err) =>{
				console.log(err);
			  }
			})
	}
}
