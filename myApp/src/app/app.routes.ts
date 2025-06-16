import { Routes } from '@angular/router';
import { AppFirst } from './app-first/app-first';
import { Login } from './login/login';
import { Products } from './products/products';

export const routes: Routes = [
    {path:'home',component: AppFirst},
    {path:'login', component:Login},
    {path:'products', component:Products}
];
