import { Component, inject } from '@angular/core';
import { HttpClient } from '@microsoft/signalr';
import { BulkInsertService } from '../services/BulkInsertService';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-file-upload-component',
  imports: [JsonPipe],
  templateUrl: './file-upload-component.html',
  styleUrl: './file-upload-component.css'
})
export class FileUploadComponent {

	private service =  inject(BulkInsertService);
	insertedRecords:any;

	handleFileUpload(event: any) {
		const file = event.target.files[0];
		this.service.processData(file).subscribe({
			next:(data)=>this.insertedRecords= data,
			error:(err)=>alert(err)

		})
	}
}
