import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Store } from '@ngxs/store';
import { CurrentUserState } from '../current-user/current-user.state';
import { UserService } from '../services/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';

export const adminGuard: CanActivateFn = (route, state) => {
  let store = inject(Store);
  let router = inject(Router);
  let userService = inject(UserService);
  let snackBar = new MatSnackBar();
  let currentUser = store.selectSignal(CurrentUserState.getUser);
  if(currentUser() == null){
    let rt = localStorage.getItem('refreshToken');
    if(rt){
      userService.getCurrentUserDetails();
      if(currentUser== null){
        router.navigateByUrl('/');
        return false;
      }
    }
    else{
      snackBar.open("Not Authorized to  visit!",undefined, {duration: 2000});
      router.navigateByUrl('/');
      return false;
    }
  }
  
  if(currentUser()?.role== 'Admin'){
    return true;
  }
  
  snackBar.open("Not Authorized to  visit!",undefined, {duration: 2000});
  router.navigateByUrl('/users');
  return false;
};
