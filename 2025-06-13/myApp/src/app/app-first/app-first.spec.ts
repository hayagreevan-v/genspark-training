import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppFirst } from './app-first';

describe('AppFirst', () => {
  let component: AppFirst;
  let fixture: ComponentFixture<AppFirst>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppFirst]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AppFirst);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
