import { Routes } from '@angular/router';
import { AppFirst } from './app-first/app-first';
import { Login } from './login/login';
import { Products } from './products/products';
import { Home } from './home/home';
import { Profile } from './profile/profile';
import { AuthGuard } from './auth-guard';
import { UserList } from './user-list/user-list';
import { Notification } from './notification/notification';

export const routes: Routes = [
    {path:'',component: AppFirst},
    {path:'login', component:Login},
    {path:'products', component:Products},
    {path:'profile', component:Profile, canActivate:[AuthGuard]},
    {path:'home/:un', component:Home,
                children :
                    [
                        {path:'products', component: Products },
                        {path: 'first', component: AppFirst}
                    ]
    },
    {path:'users', component: UserList},
    {path:'notification', component: Notification}
];
