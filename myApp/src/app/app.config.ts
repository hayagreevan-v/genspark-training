import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { ProductService } from './services/ProductService';
import { RecipeService } from './services/RecipeService';
import { UserService } from './services/UserService';
import { AuthGuard } from './auth-guard';
import { provideState, provideStore } from '@ngrx/store';
import { userReducer } from './ngrx/user.reducer';
import { NotificationService } from './services/NotificationService';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    provideStore(),
    provideState('user',userReducer),
    ProductService,
    RecipeService,
    UserService,
    NotificationService,
    AuthGuard
  ]
};
