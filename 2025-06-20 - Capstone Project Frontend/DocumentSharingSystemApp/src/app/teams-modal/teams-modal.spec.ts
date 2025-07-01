import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeamsModal } from './teams-modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { TeamService } from '../services/team.service';

describe('TeamsModal', () => {
  let component: TeamsModal;
  let fixture: ComponentFixture<TeamsModal>;


  let dialogRefSpy :any;
  let teamServiceSpy : any;
  let matDialogDataStub : any;

  beforeEach(async () => {
    dialogRefSpy = jasmine.createSpyObj('MatDialogRef', ['close']);
    teamServiceSpy = jasmine.createSpyObj('TeamService', ['getAll']);
    matDialogDataStub = {
      action: 'Add',
      onAccept: jasmine.createSpy('onAccept')
    };


    await TestBed.configureTestingModule({
      imports: [TeamsModal, MatDialogModule, MatFormFieldModule,MatInputModule, MatButtonModule,MatSelectModule, FormsModule, ReactiveFormsModule],
      providers :[
        {provide : MatDialogRef, useValue : dialogRefSpy},
        { provide: MAT_DIALOG_DATA, useValue: matDialogDataStub },
        { provide: TeamService, useValue:  teamServiceSpy},
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TeamsModal);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should call onAccept when form is valid', () => {

    spyOnProperty(component.formControl, 'valid').and.returnValue(true);
    component.onUploadClick();

    expect(matDialogDataStub.onAccept).toHaveBeenCalled();
    expect(dialogRefSpy.close).toHaveBeenCalled();
  });
});
