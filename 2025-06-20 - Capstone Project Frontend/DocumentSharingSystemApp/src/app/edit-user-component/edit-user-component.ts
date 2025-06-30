import { Component, signal } from '@angular/core';
import { UserService } from '../services/user.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { UserModel } from '../models/user.model';
import { Navbar } from "../navbar/navbar";
import { MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { UserUpdateModel } from '../models/user.update.model';
import { switchMap } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserAddModel } from '../models/user.add.model';
import { Store } from '@ngxs/store';
import { CurrentUserState } from '../current-user/current-user.state';
import { passwordCheck } from '../misc/passwordCheck';
import { confirmPasswordCheck } from '../misc/confirmPasswordCheck';
import { CommonModule } from '@angular/common';
import { TeamModel } from '../models/team.model';
import { TeamService } from '../services/team.service';

@Component({
  selector: 'app-edit-user-component',
  imports: [Navbar,FormsModule, ReactiveFormsModule, CommonModule, RouterLink, MatFormFieldModule, MatInputModule, MatIconModule, MatButtonModule, MatSelectModule],
  templateUrl: './edit-user-component.html',
  styleUrl: './edit-user-component.css',
	providers : [{provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'}}]
})
export class EditUserComponent {
	currentUser : UserModel | null = null;
	editUserId : string|null = ""
	editUser : UserModel | null = null;
	hidePassword = signal(true);
	hideConfirmPassword = signal(true);
	disabled = signal(true);
	action = signal("");
	newRole : string = 'User';
	teams : TeamModel[] =[];

	roles : string[] = ['Admin', 'User'];

	private snackBar : MatSnackBar = new MatSnackBar();

	formGroup : FormGroup = new FormGroup({});


	constructor(private userService : UserService, private teamService : TeamService, private route:ActivatedRoute, private router : Router, private store : Store){
		
		this.store.select(CurrentUserState.getUser).subscribe({
			next : (data) => {
				this.currentUser = data;
			}
		})
		if(this.currentUser == null){
			this.userService.getCurrentUserDetails();
			if(this.currentUser == null)
				return;
		}
		
		this.teamService.getAllTeams(this.currentUser).subscribe({
			next : (res : any) => {
				this.teams = res.data.$values;
				console.log(this.teams);
			}
		})

		this.editUserId = this.route.snapshot.paramMap.get('id');

		if(this.editUserId == null && this.route.snapshot.url[1].path == 'add'){
			console.log(this.route.snapshot.url[1].path == 'add');
			this.editUser = new UserModel();
			this.newRole = "User";
			this.action.set("Add");

			this.formGroup = new FormGroup({
				name: new FormControl(null,[Validators.required]),
				email : new FormControl(null,[Validators.required, Validators.email]),
				role : new FormControl(null, Validators.required),
				teamId : new FormControl(null, Validators.required),
				password : new FormControl(null, [Validators.required, passwordCheck()]),
				confirmPassword : new FormControl(null, Validators.required)
			},{validators : confirmPasswordCheck()});
		}
		else if(this.editUserId != null){
			this.userService.getUserById(this.editUserId!).subscribe({
				next : (res : any) =>{
					this.editUser = res.data;
					this.newRole = res.data.role;
					this.action.set("Edit");
					
					this.formGroup = new FormGroup({
						name: new FormControl({value : this.editUser?.name, disabled : true},[Validators.required]),
						email : new FormControl({ value : this.editUser?.email, disabled : true},[Validators.required, Validators.email]),
						role : new FormControl({value : this.editUser?.role, disabled : true}, Validators.required),
						teamId : new FormControl({value : this.editUser?.teamId, disabled : true}, Validators.required),
						password : new FormControl({value : null, disabled: true},passwordCheck()),
						confirmPassword : new FormControl({value : null, disabled: true})
					},{validators : confirmPasswordCheck()})
				},
				error : err => {
					router.navigateByUrl('documents');
					return;
				}
			})
		}
		else{
			router.navigateByUrl('documents');
			return;
		}
	}

	handleUpdate(){
		if(this.disabled()){
			this.disabled.set(false);
			this.fc.name.enable();
			this.fc.role.enable();
			this.fc.teamId.enable();
			this.fc.email.enable();
			this.fc.password.enable();
			this.fc.confirmPassword.enable();
			return;
		}

		let updatedUser = new UserUpdateModel(this.fc.name.value, this.fc.email.value,this.fc.teamId.value,null);
		if(!this.fc.password.value || this.fc.password.value != null || this.fc.password.value != ""){
			updatedUser.password = this.fc.password.value;
		}
		// console.log(updatedUser);
		if(this.fc.role.value != this.editUser?.role){
			this.userService.changeUserRoleById(this.editUser!.id, this.fc.role.value)
			.pipe(
				switchMap(() => this.userService.updateUserById(this.editUser!.id,updatedUser))
			)
			.subscribe({
				next : (res : any) => {
					this.editUser = res.data;
					this.disabled.set(true);
					this.fc.name.disable();
					this.fc.role.disable();
					this.fc.teamId.disable();
					this.fc.email.disable();
					this.fc.password.disable();
					this.fc.confirmPassword.disable();
					
					if(this.editUser?.id == this.currentUser?.id){
						this.userService.getCurrentUserDetails().subscribe({
							next : (user : UserModel | null) => {
								this.currentUser = user;
								this.router.navigateByUrl('/users');

							}
						})
					}
					// this.newPassword = null;
					this.snackBar.open("User updated successfully!", undefined, {duration: 3000});
				},
				error: (err) => {
					console.error("Error updating user role and data:", err);
				}
			});
		}
		else{
			this.userService.updateUserById(this.editUser!.id,updatedUser).subscribe({
				next : (res : any) => {
					this.editUser = res.data;
					this.disabled.set(true);
					this.fc.name.disable();
					this.fc.role.disable();
					this.fc.teamId.disable();
					this.fc.email.disable();
					this.fc.password.disable();
					this.fc.confirmPassword.disable();
					// this.newPassword = null;
					this.snackBar.open("User updated successfully!", undefined, {duration: 3000});
				},
				error: (err) => {
					console.error("Error updating user : ", err.error.errors.message);
				}
			});
		}
	}

	handleAdd(){
		let newUser = new UserAddModel(this.fc.name.value, this.fc.role.value, this.fc.email.value,this.fc.teamId.value,this.fc.password.value as string);
		this.userService.addUser(newUser).subscribe({
				next : (res : any) =>{
					this.editUser = res.data;
					this.disabled.set(true);
					// this.action.set('Edit');
					// this.newPassword = null;
					this.snackBar.open("User Added successfully!", undefined, {duration: 3000});
					this.router.navigateByUrl('/users');
				}
			});

	}
	passwordView(){
		this.hidePassword.set(!this.hidePassword());
	}
	confirmPasswordView(){
		this.hideConfirmPassword.set(!this.hideConfirmPassword());
	}
	get fc() : any {
		return this.formGroup.controls;
	}
	test(){
		console.log(this.formGroup);
	}
}
