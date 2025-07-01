import { ComponentFixture, TestBed } from '@angular/core/testing';
import { App } from './app';
import { NotificationService } from './services/notification.service';
import { Component } from '@angular/core';

describe('App', () => {

  let app : App;
  let fixture : ComponentFixture<App>;
  let notifyService : jasmine.SpyObj<NotificationService>;
  beforeEach(async () => {
    notifyService = jasmine.createSpyObj("NotificationService",["startConnection"]);
    await TestBed.configureTestingModule({
      imports: [App],
      providers: [
        {provide : NotificationService, useValue : notifyService}
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(App);
    app = fixture.componentInstance;

  });

  it('should create the app', () => {
    expect(app).toBeTruthy();
  });
  it('should connect to notification service', () => {
    expect(notifyService.startConnection).toHaveBeenCalled();
  });
});
