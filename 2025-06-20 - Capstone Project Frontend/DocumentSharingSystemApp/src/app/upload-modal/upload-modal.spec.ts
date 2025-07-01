import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UploadModal } from './upload-modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { DocumentService } from '../services/document.service';

describe('UploadModal', () => {
  let component: UploadModal;
  let fixture: ComponentFixture<UploadModal>;
  let dialogRefSpy :any;
  let documentServiceSpy : any;
  let matDialogDataStub : any;

  beforeEach(async () => {
    dialogRefSpy = jasmine.createSpyObj('MatDialogRef', ['close']);
    documentServiceSpy = jasmine.createSpyObj('DocumentService', ['getAll']);
    matDialogDataStub = {
      action: 'Add',
      teamId: 1,
      onAccept: jasmine.createSpy('onAccept')
    };

    await TestBed.configureTestingModule({
      imports: [
        UploadModal,
        MatDialogModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatSelectModule,
        FormsModule,
        ReactiveFormsModule
      ],
      providers: [
        { provide: MatDialogRef, useValue: dialogRefSpy },
        { provide: MAT_DIALOG_DATA, useValue: matDialogDataStub },
        { provide: DocumentService, useValue: documentServiceSpy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(UploadModal);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

    it('should call onAccept when form is valid', () => {
    const fakeFile = new File(['test'], 'test.txt', { type: 'text/plain' });
    component.uploadFile = fakeFile;

    // component.formGroup.get('file')?.setValue(true);
    // component.formGroup.get('description')?.setValue('desc');
    // component.formGroup.get('visibility')?.setValue('Public');

    spyOnProperty(component.formGroup, 'valid').and.returnValue(true);
    component.onUploadClick();

    expect(matDialogDataStub.onAccept).toHaveBeenCalled();
    expect(dialogRefSpy.close).toHaveBeenCalled();
  });
});
