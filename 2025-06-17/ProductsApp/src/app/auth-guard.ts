import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const isAuthenticated = localStorage.getItem("token")?true:false;
  let router = inject(Router);
  if(!isAuthenticated){
    alert("User not logged in!");
    router.navigateByUrl("/login");
    return false;
  }
  return true;
};
