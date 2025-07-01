import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeamsModal } from './teams-modal';

describe('UploadModal', () => {
  let component: TeamsModal;
  let fixture: ComponentFixture<TeamsModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TeamsModal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TeamsModal);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
