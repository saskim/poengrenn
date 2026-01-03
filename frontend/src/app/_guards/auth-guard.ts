import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, CanActivate } from '@angular/router';
import { AuthService } from 'app/_services/auth.service';

@Injectable({ providedIn: 'root' })
export class AdminGuard {

  constructor(private _authService: AuthService) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    return this._authService.isAdmin();
    //return this._authService.isAuthenticated();
  }
}
