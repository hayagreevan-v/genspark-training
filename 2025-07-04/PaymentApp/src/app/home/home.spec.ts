import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Home } from './home';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { PaymentService } from '../services/payment.service';

describe('Home', () => {
  let component: Home;
  let fixture: ComponentFixture<Home>;
  let service : jasmine.SpyObj<PaymentService>;

  beforeEach(async () => {
    service = jasmine.createSpyObj("PaymentService",["createPayment"]);
    await TestBed.configureTestingModule({
      imports: [Home, FormsModule, ReactiveFormsModule, MatInputModule, MatFormFieldModule,MatButtonModule],
      providers:[{provide : PaymentService, useValue: service}]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Home);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should mark all fields as invalid when empty', () => {
    component.formGroup.setValue({
      name: null,
      email: null,
      contact: null,
      amount: 0
    });

    expect(component.formGroup.valid).toBeFalse();
    expect(component.fc.name.errors?.['required']).toBeTrue();
    expect(component.fc.email.errors?.['required']).toBeTrue();
    expect(component.fc.contact.errors?.['required']).toBeTrue();
    expect(component.fc.amount.errors?.['min']).toBeTruthy();
  });

  it('should invalidate contact with less than 10 digits', () => {
    component.fc.contact.setValue('12345');
    expect(component.fc.contact.errors).toBeTruthy();
    expect(component.fc.contact.errors?.['minlength']).toBeTruthy();
    // expect(component.fc.contact.errors?.['maxlength']).toBeTruthy();
  });
  it('should invalidate contact with greater than 10 digits', () => {
    component.fc.contact.setValue('12345678908');
    expect(component.fc.contact.errors).toBeTruthy();
    // expect(component.fc.contact.errors?.['minlength']).toBeTruthy();
    expect(component.fc.contact.errors?.['maxlength']).toBeTruthy();
  });
  it('should validate contact with exactly 10 digits', () => {
    component.fc.contact.setValue('1234567890');
    expect(component.fc.contact.valid).toBeTrue();
    expect(component.fc.contact.errors).toBeNull();
  });
  it('should validate amount only if amount is greater than 0', () => {
    component.fc.amount.setValue(0);
    expect(component.fc.amount.invalid).toBeTrue();
    expect(component.fc.amount.errors?.["min"]).toBeTruthy();
    component.fc.amount.setValue(1);
    expect(component.fc.amount.valid).toBeTrue();
    expect(component.fc.amount.errors).toBeNull();
  });
});
