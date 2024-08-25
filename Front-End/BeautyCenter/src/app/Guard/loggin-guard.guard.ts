import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const logginGuardGuard: CanActivateFn = (route, state) => {
  let t = localStorage.getItem("token")
  if(t == null){
    let router = inject(Router)
    router.navigateByUrl("/login")
    return false;
  }
  return true
};
