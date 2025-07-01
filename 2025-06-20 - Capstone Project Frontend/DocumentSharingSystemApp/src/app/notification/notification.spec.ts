import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Notification } from './notification';
import { Component } from '@angular/core';
import { UserService } from '../services/user.service';
import { NotificationService } from '../services/notification.service';
import { of } from 'rxjs';
import { UserModel } from '../models/user.model';

@Component({
  selector: 'app-navbar',
  standalone: true,
  template: ''
})
class FakeNavbar {}

describe('Notification', () => {
  let component: Notification;
  let fixture: ComponentFixture<Notification>;

  let userServiceSpy : jasmine.SpyObj<UserService>;
  let notifyServiceSpy : jasmine.SpyObj<NotificationService>;

  beforeEach(async () => {
    notifyServiceSpy = jasmine.createSpyObj("NotificationService",["startConnection"],{ notification$: of([{ user: "Test", message: "Test" }]) });
    userServiceSpy = jasmine.createSpyObj("UserService",["getAll"],{ user$: of(new UserModel("1")) });
    await TestBed.configureTestingModule({
      imports: [Notification],
      providers : [
        {provide : UserService, useValue : userServiceSpy},
        {provide : NotificationService, useValue : notifyServiceSpy},
      ]
    })
    .compileComponents();

    TestBed.overrideComponent(Notification, {
		set: {
		imports: [
			FakeNavbar
			]
		}
	});

    // notifyServiceSpy.notification$ = of([{user: "Test", message:"Test"}]);
    // userServiceSpy.user$ = of(new UserModel("1"));
    fixture = TestBed.createComponent(Notification);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
