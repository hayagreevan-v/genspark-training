import { Routes } from '@angular/router';
import { Products } from './products/products';
import { About } from './about/about';
import { Login } from './login/login';
import { Productpage } from './productpage/productpage';
import { authGuard } from './auth-guard';

export const routes: Routes = [
    {path:"home", component:Products, canActivate:[authGuard]},
    {path:"about", component:About},
    {path:"login", component:Login},
    {path:"products/:id", component:Productpage, canActivate:[authGuard]}
];
