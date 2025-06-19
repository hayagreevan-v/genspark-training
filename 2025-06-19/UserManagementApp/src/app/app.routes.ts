import { Routes } from '@angular/router';
import { Users } from './users/users';
import { AddUser } from './add-user/add-user';

export const routes: Routes = [
    {path:'users', component:Users},
    {path:'add-user', component:AddUser}
];
