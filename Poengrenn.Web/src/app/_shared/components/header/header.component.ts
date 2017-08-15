import { Component, OnInit } from '@angular/core';
import { AuthService } from 'app/_services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  loggedInUser;

  constructor(private _authService: AuthService) { }

  ngOnInit() {
    this.loggedInUser = this._authService.loggedInUser();
  }

  isAuth() {
    return this._authService.isAuthenticated();
  }

  isAdmin() {
    return this._authService.isAdmin();
  }

  
}
