import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'app/_services/auth.service';
import { ApiService } from 'app/_services/api.service';
import { LoginModel, LoginResponse } from 'app/_models/models';

@Component({
  selector: 'app-admin-login',
  standalone: true,
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.scss'],
  imports: [CommonModule, FormsModule],
  providers: [ApiService, AuthService]
})
export class AdminLoginComponent implements OnInit {

  model: LoginModel;
  returnUrl: string;
  errorMessage: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private _authService: AuthService,
    private _apiService: ApiService) { }

  ngOnInit() {
    this._authService.logout();
    this.model = new LoginModel();
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  login() {
    this.errorMessage = "";
    this._authService.authenticate(this.model)
      .subscribe((user: LoginResponse) => {
        if (!user)
          this.errorMessage = "Kunne ikke logge inn. Feil brukernavn og/eller passord.";
        else if (user.brukernavn == this.model.brukernavn && user.token) {
          this.router.navigate([this.returnUrl]);
        }
      },
      error => {
          this.errorMessage = "Det oppstod en feil med pålogging";
      });
  }
}
