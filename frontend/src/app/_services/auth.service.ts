import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { LoginModel, LoginResponse } from 'app/_models/models';

import { ApiService } from 'app/_services/api.service';


@Injectable()
export class AuthService {

  constructor(private _apiService: ApiService) { }

    loggedInUser() : LoginResponse {
        return this.getUserFromLocalStorage();
    }
    isAuthenticated() : boolean {
        let user: LoginResponse = this.getUserFromLocalStorage();
        return (user && (user.rolle == "admin" || user.rolle == "user"));
    }
    isAdmin() : boolean {
        let user: LoginResponse = this.getUserFromLocalStorage();
        return (user && user.rolle == "admin");
    }

    authenticate(loginData: LoginModel) : Observable<LoginResponse> {
        return this._apiService.Login(loginData)
            .map(response => {
                localStorage.setItem('currentUser', JSON.stringify(response));
                return response;
            });
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
    }

    private getUserFromLocalStorage() : LoginResponse {
        let user: LoginResponse
        let value : string = localStorage.getItem('currentUser');
        if (value && value != "undefined" && value != "null") {
            return <LoginResponse>JSON.parse(value);
        }
        return null;
    }
}
