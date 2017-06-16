import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, CanActivate } from '@angular/router';

@Injectable()
export class AuthGuard {

  constructor() { }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    return true;
  }
}
