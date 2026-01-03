import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from 'app/_services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  imports: [CommonModule, RouterLink, RouterLinkActive]
})
export class HeaderComponent implements OnInit {

  loggedInUser;
  menuOpen = false;

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

  toggleMenu() {
    this.menuOpen = !this.menuOpen;
  }

  closeMenu() {
    this.menuOpen = false;
  }
}
