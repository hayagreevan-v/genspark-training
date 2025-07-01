import { Routes } from '@angular/router';
import { Login } from './login/login';
import { Documents } from './documents/documents';
import { Notification } from './notification/notification';
import { Users } from './users/users';
import { EditUserComponent } from './edit-user-component/edit-user-component';
import { adminGuard } from './guards/admin-guard';
import { specifiedUserGuard } from './guards/specified-user-guard';
import { Teams } from './teams/teams';

export const routes: Routes = [
    {path:'', component: Login},
    {path:'documents', component:Documents},
    {path: 'users', component:Users},
    {path :'users/edit/:id', component : EditUserComponent, canActivate : [specifiedUserGuard]},
    {path :'users/add', component : EditUserComponent, canActivate: [adminGuard]},
    {path :'teams', component : Teams, canActivate: [adminGuard]},
    {path:'notifications', component: Notification}
];
