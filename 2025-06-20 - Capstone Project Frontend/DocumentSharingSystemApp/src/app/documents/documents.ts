import { Component, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatExpansionModule } from '@angular/material/expansion';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { DocumentService } from '../services/document.service';
import { DocumentModel } from '../models/document.model';
import { UserService } from '../services/user.service';
import { UserModel } from '../models/user.model';
import { MatDialog } from '@angular/material/dialog';
import { Dialog } from '../dialog/dialog';
import { Navbar } from "../navbar/navbar";
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { DocumentSearchModel } from '../models/document.search.model';
import { MatSelectModule } from '@angular/material/select';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { BehaviorSubject, catchError, debounceTime, distinctUntilChanged, distinctUntilKeyChanged, map, Observable, of, startWith, switchMap, tap } from 'rxjs';
import { AsyncPipe, DatePipe } from '@angular/common';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { UploadModal } from '../upload-modal/upload-modal';
import { Store } from '@ngxs/store';
import { CurrentUserState } from '../current-user/current-user.state';
import { MatCardModule } from '@angular/material/card';
import { TeamService } from '../services/team.service';
import { TeamModel } from '../models/team.model';
import { DocumentDetailsModel } from '../models/document.details.model';
import { MatTabGroup, MatTabsModule } from '@angular/material/tabs';
import { MatPaginatorIntl, MatPaginatorModule, PageEvent } from '@angular/material/paginator';

interface selectInterface {
    value : any,
    view: string
}

@Component({
  selector: 'app-documents',
  imports: [
      FormsModule,
      ReactiveFormsModule, 
      MatFormFieldModule, 
      MatIconModule, 
      MatDatepickerModule, 
      MatInputModule, 
      MatButtonModule,
      MatButtonToggleModule,
      MatSelectModule, 
      MatAutocompleteModule,
      MatSnackBarModule,
      MatProgressSpinnerModule,
      MatCardModule,
	  MatTabsModule,
	  MatPaginatorModule,
      AsyncPipe,
      DatePipe,
      Navbar
    ],
  templateUrl: './documents.html',
  styleUrl: './documents.css',
  providers: [provideNativeDateAdapter(),
    {provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline'}},
	// {provide: MatPaginatorIntl, useValue :{itemsPerPageLabel : "Documents per page"}}
  ]


})


export class Documents {

  step = signal("");
  errorMessage : string = "";
  documents : DocumentModel[]=[];
  currentUser : UserModel | null = null;
  documentSearch : DocumentSearchModel = new DocumentSearchModel();
  private snackbar = new MatSnackBar();

  
  documentFilterSubject = new BehaviorSubject<DocumentSearchModel>(this.documentSearch);

  sortByList : selectInterface[] = [
    {value : 'id', view : 'Id'},
    {value : 'originalFileName', view : 'File Name'},
    {value : 'createdByUserName', view : 'Created By User'},
    {value : 'lastUpdatedByUserName', view : 'Last Updated By User'},
    {value : 'createdAt', view : 'Created Time'},
    {value : 'lastUpdatedAt', view : 'Last Updated Time'}
  ]
  createdByUsersList : selectInterface[] = [];
  teamByList : selectInterface[] =[];

  constructor(
	private userService : UserService, 
	private documentService : DocumentService, 
	private teamService : TeamService, 
	private dialog :MatDialog, 
	private store : Store){

    this.documentSearch.SortOrder = "descending";
    this.documentSearch.SortBy = 'createdAt';
	this.documentSearch.pageNo=1;
	this.documentSearch.pageSize=6;

    //  this.userService.user$.subscribe({
    //             next : (data : any) =>{
    //                 this.currentUser= data;
    //             }
    //         });

	// this.currentUser = JSON.parse(localStorage.getItem('currentUser')!);
	this.store.select(CurrentUserState.getUser).subscribe((user) => {
		this.currentUser = user;
	})
    if(this.currentUser==null){
		  userService.getCurrentUserDetails().subscribe({
       		next : (data: UserModel | null) => {
				this.currentUser = data;
				if(this.currentUser==null){
					this.errorMessage = "User not Logged in!";
					return;
				}
				this.loadList();
			}
      });
    }
	else{
		this.loadList();
	}

    
    this.documentFilterSubject.next(this.documentSearch);
  }

  	loadList () {
		this.teamService.getAllTeams(this.currentUser as UserModel)
			.subscribe((res : any) => {
				this.teamByList =[];
				res.data.$values.forEach((t: TeamModel) => {
					this.teamByList.push({value: t.id, view: `${t.name} (${t.id})`})
				});
			});
		this.userService.getAllUsers()
			.subscribe((data: any) => {
				this.createdByUsersList = data.$values.map((u:any) => ({
					value: u.email,
					view: `${u.name} (${u.email})`
				}));
				// console.log(this.createdByUsersList);
			});
	}

	createdByUserDisabled = signal(false);
	tabs = ['All','My Team','My Docs'];
  	activeTab = signal(this.tabs[0]);
  	setTab(value:string){
		if(this.activeTab() == 'My Docs'){
			this.documentSearch.searchByCreatedUserEmail = null;
			this.myControl.enable();	
		}
		if(this.activeTab() == 'My Team'){
			this.documentSearch.view = "All";
		}
		if( value == 'My Docs'){
			this.documentSearch.searchByCreatedUserEmail = this.currentUser?.email as string;
			this.myControl.disable();
		}
		if( value == 'My Team'){
			this.documentSearch.view = "Team";
		}
		this.documentSearch.pageNo=1;
		this.documentFilterSubject.next(this.documentSearch);
		this.activeTab.set(value);
		console.log(value);
  	}
  onValueChange(){
	if (this.documentSearch.searchByCreatedUserEmail === '') {
		this.documentSearch.searchByCreatedUserEmail = null;
	}
	if (this.documentSearch.searchByCreatedTime === '') {
		this.documentSearch.searchByCreatedTime = null;
	}
	this.documentSearch.pageNo=1;
    this.documentFilterSubject.next(this.documentSearch);
    console.log(this.documentSearch);
}

total=0;
handlePageEvent(e: PageEvent) {
	// this.pageEvent = e;
    // this.total= e.length;
    this.documentSearch.pageSize = e.pageSize;
    this.documentSearch.pageNo = e.pageIndex+1;
	// this.onValueChange();
	this.documentFilterSubject.next(this.documentSearch);
  }
  handleUpload(){
    this.dialog.open(UploadModal, { data : {
		currentUser: this.currentUser,
		teamOptions : this.teamByList,
		teamId : this.currentUser?.teamId,
		action : "Add",
		onAccept : (fileData: DocumentDetailsModel,uploadFile : File) =>{
			this.documentService.uploadDocument(this.currentUser as UserModel,fileData,uploadFile)
			.pipe(
				switchMap(() => this.documentService.getByFilter(this.currentUser as UserModel,this.documentSearch).pipe(
					catchError((err)=>{
					console.error('API error:', err);
					if(err.error.errors){
						this.errorMessage = err.error.errors.message;
						this.snackbar.open(err.error.errors.message,undefined,{duration:3000});
					}
					return of({ data: { $values: [] }, pagination: { totalRecords: 0 } })
				})
			))
				)
				.subscribe({
					next : (data : any) => {
						console.log(data);
						this.documents = [];
						data.data.$values.forEach((doc:any) => {
							this.documents.push(DocumentModel.fromData(doc));
						});
						this.snackbar.open("File uploaded successfully!", undefined, {duration: 3000});
					}
				})
			}
		}});
  }
  handleEdit(doc : DocumentModel){
    this.dialog.open(UploadModal, { data : {
		currentUser: this.currentUser,
		teamOptions : this.teamByList,
		teamId : this.currentUser?.teamId,
		editDoc : doc,
		action : "Edit",
		onAccept : (fileData: DocumentDetailsModel) =>{
			this.documentService.updateDocumentDetails(this.currentUser as UserModel,doc.id,fileData)
			.pipe(
				switchMap(() => this.documentService.getByFilter(this.currentUser as UserModel,this.documentSearch).pipe(
					catchError((err)=>{
					console.error('API error:', err);
					if(err.error.errors){
						this.errorMessage = err.error.errors.message;
						this.snackbar.open(err.error.errors.message,undefined,{duration:3000});
					}
					return of({ data: { $values: [] }, pagination: { totalRecords: 0 } })
				})
			))
				)
				.subscribe({
					next : (data : any) => {
						console.log(data);
						this.documents = [];
						data.data.$values.forEach((doc:any) => {
							this.documents.push(DocumentModel.fromData(doc));
						});
						this.snackbar.open("File edited successfully!", undefined, {duration: 3000});
					}
				})
			}
		}});
  }
  setStep(value: string) {
    this.step.set(value);
  }

  onDelete(id : string){
    this.documentService.deleteDocument(this.currentUser!,id).pipe(
		switchMap(() => this.documentService.getByFilter(this.currentUser as UserModel,this.documentSearch).pipe(
			catchError((err)=>{
				console.error('API error:', err);
				if(err.error.errors){
					this.errorMessage = err.error.errors.message;
					this.snackbar.open(err.error.errors.message,undefined,{duration:3000});
				}
				return of({ data: { $values: [] }, pagination: { totalRecords: 0 } });
			})
		))
    ).subscribe({
		next: (res:any) => {
			console.log(res);
			this.documents = [];
			res.data.$values.forEach((doc:any) => {
				this.documents.push(DocumentModel.fromData(doc));
			});
			console.log(this.documents);
		},
		error : (err) =>{
			console.log(err);
		}
    })
}
onDownload(doc : DocumentModel){
	this.documentService.downloadDocument(this.currentUser as UserModel, doc.id)
	.subscribe((blob : Blob) =>{
		const a = document.createElement('a')
        const objectUrl = URL.createObjectURL(blob)
        a.href = objectUrl
        a.download = doc.originalFileName;
        a.click();
        URL.revokeObjectURL(objectUrl);
	})
}
openDeleteDialog(message : string, id : string){
	this.dialog.open(Dialog,{
		data : {
			message : `Want to delete ${message}`, 
			onAccept : ()=>{
				this.onDelete(id);
				this.snackbar.open(`Document ${message} deleted successfully!`,undefined,{duration:3000});
        	}
      }
    })
  }

  onDateChange(date: Date) {
    this.documentSearch.searchByCreatedTime = date ? date.toISOString() : null;
    this.onValueChange();
  }
  clearDate(){
	this.documentSearch.searchByCreatedTime = null;
    this.onValueChange();
  }

  myControl = new FormControl('');
  filteredOptions: Observable<selectInterface[]> | undefined;

  ngOnInit() {
    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value || '')),
    );

    this.documentFilterSubject.pipe(
      debounceTime(500),
      tap(()=> {console.log("API Called")}),
      switchMap((query : DocumentSearchModel) => this.documentService.getByFilter(this.currentUser as UserModel,query).pipe(
        catchError((err)=>{
        console.error('API error:', err);
        if(err.error.errors){
          this.errorMessage = err.error.errors.message;
          this.snackbar.open(err.error.errors.message,undefined,{duration:2000})
        }
        // Return empty result or fallback

        return of({ data: { $values: [] }, pagination: { totalRecords: 0 } });
        } )
      ))
    ).subscribe({
      next: (res:any) => {
        console.log(res);
        this.documents = [];
        res.data.$values.forEach((doc:any) => {
          this.documents.push(DocumentModel.fromData(doc));
        });
		this.total = res?.pagination?.totalRecords??0;
        console.log(this.documents);
      },
      error : (err) =>{
        console.log(err);
      }
    })
  }

  private _filter(value: string): selectInterface[] {
    const filterValue = value.toLowerCase();
    return this.createdByUsersList.filter(cbu => cbu.view.toLowerCase().includes(filterValue));
  }

}
