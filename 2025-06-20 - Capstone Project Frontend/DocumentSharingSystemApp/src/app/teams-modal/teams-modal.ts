import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select';
import { TeamModel } from '../models/team.model';
import { TeamService } from '../services/team.service';
import { DocumentDetailsModel } from '../models/document.details.model';

@Component({
  selector: 'app-teams-modal',
  imports: [MatDialogModule, MatFormFieldModule,MatInputModule, MatButtonModule,MatSelectModule, FormsModule, ReactiveFormsModule],
  templateUrl: './teams-modal.html',
  styleUrl: './teams-modal.css',
  providers: [{provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'}}]
})
export class TeamsModal {
	dialogRef = inject(MatDialogRef<TeamsModal>);
	data = inject(MAT_DIALOG_DATA);
	teamService = inject(TeamService);
	formControl: FormControl;
	snackBar = new MatSnackBar();

	constructor(){
		if(this.data.action == 'Add'){
			this.formControl = new FormControl(null, Validators.required)
		} else{
			this.formControl = new FormControl(this.data.teamName, Validators.required);
		}
	}


	onUploadClick(){
		if(this.data.action== "Add"){
			if(this.formControl.valid ){
				this.data.onAccept(this.formControl.value);
				this.dialogRef.close();
				// this.snackBar.open("Team Added Succesfully", undefined, {duration: 3000});
			}
		}
		if(this.data.action== "Edit"){
			if(this.formControl.valid ){
				this.data.onAccept(this.formControl.value);
				this.dialogRef.close();
				// this.snackBar.open("Team Updated Succesfully", undefined, {duration: 3000});
			}
		}

	}
	onCancelClick(){
		this.dialogRef.close();
	}
}
