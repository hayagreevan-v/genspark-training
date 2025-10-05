import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Navbar } from './navbar';
import { UserService } from '../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngxs/store';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { of } from 'rxjs';
import { UserModel } from '../models/user.model';
import { NotificationService } from '../services/notification.service';
import { MatBadgeModule } from '@angular/material/badge';

describe('Navbar', () => {
  let component: Navbar;
  let fixture: ComponentFixture<Navbar>;

  let userService : jasmine.SpyObj<UserService>;
  let notificationService : jasmine.SpyObj<NotificationService>;
  let route: jasmine.SpyObj<ActivatedRoute>; 
  let router : jasmine.SpyObj<Router>; 
  let store : jasmine.SpyObj<Store>;

  let user = new UserModel("1");

  beforeEach(async () => {
    userService = jasmine.createSpyObj("UserService",["getCurrentUserDetails","logout"])
    notificationService = jasmine.createSpyObj("NotificationService",["dismissNotification"], { notification$: of([{ user: "Test", message: "Test", isNew : true }]) })
    router = jasmine.createSpyObj("Router",["navigateByUrl","navigate"]);
    route = jasmine.createSpyObj("AcivatedRoute",["navigate"],{snapshot: {url : "Test"}});
    store = jasmine.createSpyObj("Store",["select"]);

    await TestBed.configureTestingModule({
      imports: [Navbar, MatToolbarModule, MatIconModule, MatButtonModule,MatMenuModule, MatBadgeModule],
      providers : [
        {provide: UserService, useValue : userService},
        {provide: Store, useValue : store},
        {provide: Router, useValue : router},
        {provide: ActivatedRoute, useValue : route},
        {provide : NotificationService, useValue : notificationService}
      ]
    })
    .compileComponents();

    store.select.and.returnValue(of(user));
    userService.getCurrentUserDetails.and.returnValue(of(user));
    fixture = TestBed.createComponent(Navbar);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should get current user data', () => {
    expect(component.currentUser).toBe(user);
  });
  it('should navigate', () => {
    component.navigate("");
    expect(router.navigateByUrl).toHaveBeenCalledOnceWith("");
  });
  it('should logout', () => {
    component.logout();
    expect(userService.logout).toHaveBeenCalled();
  });
  it('should display', () => {
    expect(fixture.nativeElement.textContent).toContain("Document Sharing System");
  });
});
