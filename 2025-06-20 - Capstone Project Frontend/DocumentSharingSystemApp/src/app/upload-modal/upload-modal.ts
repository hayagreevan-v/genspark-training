import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { DocumentService } from '../services/document.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select';
import { TeamModel } from '../models/team.model';
import { TeamService } from '../services/team.service';
import { DocumentDetailsModel } from '../models/document.details.model';

@Component({
  selector: 'app-upload-modal',
  imports: [MatDialogModule, MatFormFieldModule,MatInputModule, MatButtonModule,MatSelectModule, FormsModule, ReactiveFormsModule],
  templateUrl: './upload-modal.html',
  styleUrl: './upload-modal.css',
  providers: [{provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'}}]
})
export class UploadModal {
	dialogRef = inject(MatDialogRef<UploadModal>);
	data = inject(MAT_DIALOG_DATA);
	documentService = inject(DocumentService);
	visibilityOptions = ['Public','Team','Private'];
	uploadFile : File | null =null;
	formGroup: FormGroup;
	snackBar = new MatSnackBar();

	constructor(){
		if(this.data.action == 'Add'){
			this.formGroup= new FormGroup({
				file : new FormControl(null, Validators.required),
				description : new FormControl(null),
				teamId : new FormControl(this.data.teamId, Validators.required),
				visibility : new FormControl("Public", Validators.required)
			})
		} else{
			this.formGroup = new FormGroup({
				description : new FormControl(this.data.editDoc.description),
				teamId : new FormControl(this.data.editDoc.teamId, Validators.required),
				visibility : new FormControl(this.data.editDoc.visibility, Validators.required)
			})
		}
	}

	onFileChange(event : Event){
		const input = event.target as HTMLInputElement;
		if(input.files && input.files.length > 0){
			this.uploadFile = input.files[0];
			console.log("Is file:", this.uploadFile instanceof File);
		}
	}
	onUploadClick(){
		if(this.data.action== "Add"){
			if(this.formGroup.valid && this.uploadFile != null){
				let fileData = new DocumentDetailsModel(
					this.formGroup.get('description')?.value,
					this.formGroup.get('teamId')?.value,
					this.formGroup.get('visibility')?.value
				)
				this.data.onAccept(fileData,this.uploadFile);
				this.dialogRef.close();
				
			}
			else{
				this.formGroup.markAsTouched();
				this.snackBar.open("File is required!", undefined, {duration: 3000});
			}
		}
		if(this.data.action == "Edit"){
			if(this.formGroup.valid) {
				let fileData = new DocumentDetailsModel(
					this.formGroup.get('description')?.value,
					this.formGroup.get('teamId')?.value,
					this.formGroup.get('visibility')?.value
				)
				this.data.onAccept(fileData);
				this.dialogRef.close();
			}
			else{
				this.formGroup.markAsTouched();
				this.snackBar.open("File is required!", undefined, {duration: 3000});
			}
			
		}

	}
	onCancelClick(){
		this.dialogRef.close();
	}
	get fc() : any {
		return this.formGroup.controls;
	}
}
