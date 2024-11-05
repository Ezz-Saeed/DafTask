import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserProfileService } from '../Services/user-profile.service';

export const authGuard: CanActivateFn = (route, state) => {

  let _userAuthService = inject(UserProfileService)
  let _routerService = inject(Router)
  if(_userAuthService.isAuthenticated()){
    return true;
  }else{
    _routerService.navigate(["/login"])
    return false;
  }

};
