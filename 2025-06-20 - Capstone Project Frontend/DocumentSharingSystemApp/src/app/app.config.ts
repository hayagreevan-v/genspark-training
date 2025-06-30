import { ApplicationConfig, importProvidersFrom, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { UserService } from './services/user.service';
import { DocumentService } from './services/document.service';
import { NotificationService } from './services/notification.service';
import { NgxsModule, provideStore } from '@ngxs/store';
import { CurrentUserState } from './current-user/current-user.state';
import { TeamService } from './services/team.service';
// import { provideState, provideStore } from '@ngrx/store';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    // provideStore(),
    // provideState()
    UserService,
    DocumentService,
    TeamService,
    NotificationService, 
    // provideStore(),
    importProvidersFrom(NgxsModule.forRoot([CurrentUserState]))
  ]
};
