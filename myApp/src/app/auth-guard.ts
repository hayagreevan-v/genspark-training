import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
@Injectable()
export class AuthGuard implements CanActivate{
  constructor(private router: Router){}
  
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const isAuthenticated = localStorage.getItem("token")?true:false;
    if(!isAuthenticated){
      alert("User not logged in!");
      this.router.navigateByUrl("/login");
      return false;
    }
    return true;
  }
  
}
// export const authGuard: CanActivateFn = (route, state) => {
//   return true;
// };
