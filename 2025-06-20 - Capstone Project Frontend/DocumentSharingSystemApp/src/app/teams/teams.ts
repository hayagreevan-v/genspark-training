import { Component, signal, WritableSignal } from '@angular/core';
import { Navbar } from "../navbar/navbar";
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { UserModel } from '../models/user.model';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { UserService } from '../services/user.service';
import { TeamService } from '../services/team.service';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Store } from '@ngxs/store';
import { CurrentUserState } from '../current-user/current-user.state';
import { TeamModel } from '../models/team.model';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule } from '@angular/material/form-field';
import { DatePipe } from '@angular/common';
import { Dialog } from '../dialog/dialog';
import { BehaviorSubject, catchError, debounceTime, of, switchMap, tap } from 'rxjs';
import { TeamsModal } from '../teams-modal/teams-modal';

@Component({
  selector: 'app-teams',
  imports: [
    Navbar,
    MatListModule,
    MatButtonModule,
    MatInputModule,
    Navbar,
	  FormsModule,
	  ReactiveFormsModule,
	  MatProgressSpinnerModule,
    MatButtonModule,
    MatIconModule,
    MatExpansionModule, 
    MatFormFieldModule,  
    MatInputModule,
    MatSnackBarModule,
	  DatePipe
  ],
  templateUrl: './teams.html',
  styleUrl: './teams.css',
  providers : [{provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'}}]
})
export class Teams {
	step = signal(0);
	currentUser : UserModel | null = null;
	errorMessage : string | null = null;
  	searchQuery : string= "";
	allTeams : TeamModel[] = [];
	private snackBar : MatSnackBar = new MatSnackBar();

  teamSearchSubject = new BehaviorSubject<string>(this.searchQuery);

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
					// this.loadTeams();;
          this.teamSearchSubject.next(this.searchQuery);
      			}
    		});
    	}else{
			// this.loadTeams();
      this.teamSearchSubject.next(this.searchQuery);
		}
	}
	loadTeams () {
    this.teamService.getAllTeams(this.currentUser as UserModel)
    .subscribe((res : any) => {
      this.allTeams = res.data.$values;
      console.log(this.allTeams);
    });
	}
  
	
	setStep(value: number) {
    this.step.set(value);
	}
	onDelete(id : number){
    this.teamService.deleteTeam(id,this.currentUser as UserModel).subscribe({
      next : (data : any) => {
        		console.log(`Deleted ${data.data.name} (${data.data.id})`);
				this.snackBar.open(`Deleted ${data.data.name} (${data.data.id})`,undefined,{duration: 3000});
				// this.loadTeams();
       			 this.teamSearchSubject.next(this.searchQuery);
			},
		error : (err : any) => {
			console.log(err?.error?.errors?.message??"Deletion Unsuccessful");
			this.snackBar.open(err?.error?.errors?.message??"Deletion Unsuccessful",undefined,{duration: 3000});
		}
		})
	}
	
	onRestore(id : number){
    this.teamService.restoreTeam(id,this.currentUser as UserModel).subscribe({
      next : (data : any) =>{
        console.log(`Restored ${data.data.name} (${data.data.id})`);
				this.snackBar.open(`Restored ${data.data.name} (${data.data.id})`,undefined,{duration: 3000});
				// this.loadTeams();
        		this.teamSearchSubject.next(this.searchQuery);
			}
		})
	}

	openDeleteDialog(message : string, id : number){
		this.dialog.open(Dialog,{
		  data : {
			message : `Want to delete ${message}`, 
			onAccept : () => {
					this.onDelete(id);
				}
			}
		})

	}

	handleEdit(team : TeamModel){
		this.dialog.open(TeamsModal,{
			data : {
				teamName : team.name,
				action : "Edit",
				onAccept : (newName : string) => {
					this.teamService.updateTeam(team.id,newName,this.currentUser as UserModel)
								.pipe(
									switchMap(() => this.teamService.getByFilter(this.searchQuery,this.currentUser as UserModel).pipe(
										catchError((err)=>{
										console.error('API error:', err);
										if(err.error.errors){
											this.errorMessage = err.error.errors.message;
											this.snackBar.open(err.error.errors.message,undefined,{duration:3000});
										}
										return of({ data: { $values: [] } })
									})
								))
									)
									.subscribe({
										next : (data : any) => {
											this.allTeams = data.data.$values;
											this.snackBar.open("Team edited successfully!", undefined, {duration: 3000});
										}
									})
				}
			}
		})
	}
	handleAdd(){
		this.dialog.open(TeamsModal,{
			data : {
				action : "Add",
				onAccept : (newName : string) => {
					this.teamService.addTeam(newName,this.currentUser as UserModel)
								.pipe(
									switchMap(() => this.teamService.getByFilter(this.searchQuery,this.currentUser as UserModel).pipe(
										catchError((err)=>{
										console.error('API error:', err);
										if(err.error.errors){
											this.errorMessage = err.error.errors.message;
											this.snackBar.open(err.error.errors.message,undefined,{duration:3000});
										}
										return of({ data: { $values: [] } })
									})
								))
									)
									.subscribe({
										next : (data : any) => {
											this.allTeams = data.data.$values;
											this.snackBar.open("Team Added successfully!", undefined, {duration: 3000});
										}
									})
				}
			}
		})
	}


  onValueChange(){
	this.teamSearchSubject.next(this.searchQuery);
  }

	ngOnInit(){
		this.teamSearchSubject.pipe(
					  debounceTime(500),
					  tap(()=> {console.log(" Teams API Called")}),
					  switchMap((query : string) => this.teamService.getByFilter(query, this.currentUser as UserModel).pipe(
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
						this.allTeams = res.data.$values;
						// console.log(this.allUsers);
					  },
					  error : (err) =>{
						console.log(err);
					  }
					})
			}

}
