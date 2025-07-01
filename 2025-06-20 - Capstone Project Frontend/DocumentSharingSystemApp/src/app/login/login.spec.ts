import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Login } from './login';
import { UserService } from '../services/user.service';
import { of } from 'rxjs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

describe('Login', () => {
  let component: Login;
  let fixture: ComponentFixture<Login>;
  let userServiceSpy : jasmine.SpyObj<UserService>;

  beforeEach(async () => {
    let spy = jasmine.createSpyObj("UserService",["login"])
    await TestBed.configureTestingModule({
      imports: [Login],
      providers : [
        FormsModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatIconModule,
        {provide: UserService, useValue : spy}
      ]
    })
    .compileComponents();

    userServiceSpy = TestBed.inject(UserService) as jasmine.SpyObj<UserService>;
    userServiceSpy.login.and.returnValue(of({success:true, error:null}))
    fixture = TestBed.createComponent(Login);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should display', () => {
    expect(fixture.nativeElement.textContent).toContain("Welcome");
  });
  it('should call login', () => {
    component.handleLogin();
    expect(userServiceSpy.login).toHaveBeenCalled();
    expect(userServiceSpy.login).toHaveBeenCalledWith(component.userCredentials);
  });
  it('should get server login error', () => {
    userServiceSpy.login.and.returnValue(of({success:false, error: "Invalid Credentials"}));
    fixture.detectChanges();
    component.handleLogin();
    expect(userServiceSpy.login).toHaveBeenCalled();
    expect(userServiceSpy.login).toHaveBeenCalledWith(component.userCredentials);
    expect(component.fc.errors).toEqual({server: "Invalid Credentials"});
  });
  it('should display login error', () => {
    userServiceSpy.login.and.returnValue(of({success:false, error: "Invalid Credentials"}));
    component.fc.markAsTouched();
    component.fc.markAsDirty();
    component.handleLogin();
    fixture.detectChanges();
    expect(fixture.nativeElement.textContent).toContain("Invalid Credentials");
  });
  it('should toggle password visibility', () => {
    const initialView = component.hidePassword();
    component.passwordView();
    expect(component.hidePassword()).toBe(!initialView);
  });
});
