import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Documents } from './documents';
import { Component, NO_ERRORS_SCHEMA } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Store } from '@ngxs/store';
import { DocumentService } from '../services/document.service';
import { TeamService } from '../services/team.service';
import { UserService } from '../services/user.service';
import { UserModel } from '../models/user.model';
import { TeamModel } from '../models/team.model';
import { AsyncPipe, DatePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTabsModule } from '@angular/material/tabs';
import { Users } from '../users/users';
import { of } from 'rxjs';
import { DocumentModel } from '../models/document.model';
import { DocumentSearchModel } from '../models/document.search.model';

@Component({
  selector: 'app-navbar',
  standalone: true,
  template: ''
})
class FakeNavbar {}

describe('Documents', () => {
  let component: Documents;
  let fixture: ComponentFixture<Documents>;

  let userService : jasmine.SpyObj<UserService>;
  let documentService : jasmine.SpyObj<DocumentService>;
  let teamService : jasmine.SpyObj<TeamService>; 
  let dialog : jasmine.SpyObj<MatDialog>;  
  let store : jasmine.SpyObj<Store>;

  let user = new UserModel("1");
  let usersList = [new UserModel("1"), new UserModel("2")];
  let teamsList = [new TeamModel(1), new TeamModel(2)];
  let doc = new DocumentModel("1");
  let docsList = [new DocumentModel("1"),new DocumentModel("2")];

  beforeEach(async () => {

    let userServiceSpy = jasmine.createSpyObj("UserService",["getCurrentUserDetails","getAllUsers"]);
    let documentServiceSpy = jasmine.createSpyObj("DocumentService",["uploadDocument","getByFilter","updateDocumentDetails","deleteDocument","downloadDocument"]);
    let teamServiceSpy = jasmine.createSpyObj("TeamService",["getAllTeams","deleteTeam","restoreTeam","updateTeam","getByFilter","addTeam"]);
    let dialogSpy = jasmine.createSpyObj("MatDialog",["open"]);
    let storeSpy = jasmine.createSpyObj("Store",["select"]);

    await TestBed.configureTestingModule({
      imports: [Documents],
      schemas: [NO_ERRORS_SCHEMA],
      providers : [
        {provide: UserService, useValue : userServiceSpy},
        {provide: DocumentService, useValue : documentServiceSpy},
        {provide: TeamService, useValue : teamServiceSpy},
        {provide: MatDialog, useValue : dialogSpy},
        {provide: Store, useValue : storeSpy},
      ]
    })
    .compileComponents();

    TestBed.overrideComponent(Documents, {
        set: {
        imports: [
          FakeNavbar,
          FormsModule,
          ReactiveFormsModule,
          MatExpansionModule, 
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
          AsyncPipe,
          DatePipe,
        ]
      }
    });

    userService = TestBed.inject(UserService) as jasmine.SpyObj<UserService>;
    teamService = TestBed.inject(TeamService) as jasmine.SpyObj<TeamService>;
    dialog = TestBed.inject(MatDialog) as jasmine.SpyObj<MatDialog>;
    documentService = TestBed.inject(DocumentService) as jasmine.SpyObj<DocumentService>;
    store = TestBed.inject(Store) as jasmine.SpyObj<Store>;

    store.select.and.returnValue(of(user));
    userService.getCurrentUserDetails.and.returnValue(of(user));
    userService.getAllUsers.and.returnValue(of( { $values: usersList } ));
    teamService.getByFilter.and.returnValue(of({ data: { $values: teamsList } }));
    teamService.getAllTeams.and.returnValue(of({ data: { $values: teamsList } }));
    documentService.getByFilter.and.returnValue(of({ data: { $values: docsList } }));


    fixture = TestBed.createComponent(Documents);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should load list data', () => {
    expect(teamService.getAllTeams).toHaveBeenCalled();
    expect(userService.getAllUsers).toHaveBeenCalled();
  });
  it('should get current user data', () => {
    expect(component.currentUser).toBe(user);
  });

  it('should get documents data', async () => {
      component.documentFilterSubject.next(new DocumentSearchModel());
      await fixture.whenStable();
      fixture.detectChanges();
  
      expect(component.documents.length).toBe(2);
      expect(component.documents).toEqual(docsList);
    });

    it("should delete document", async () => {
		documentService.deleteDocument.and.returnValue(of({data: doc}))
		component.onDelete("1");

		expect(documentService.deleteDocument).toHaveBeenCalledWith(user,"1");
	})
	it("should download document", async () => {
		documentService.downloadDocument.and.returnValue(of())
		component.onDownload(doc);

		expect(documentService.downloadDocument).toHaveBeenCalledWith(user,doc.id);
	})
});
