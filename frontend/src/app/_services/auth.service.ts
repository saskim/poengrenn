import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { LoginModel, LoginResponse } from 'app/_models/models';

import { ApiService } from 'app/_services/api.service';


@Injectable({ providedIn: 'root' })
export class AuthService {

  constructor(private _apiService: ApiService) { }

    loggedInUser() : LoginResponse {
        return this.normalizeUser(this.getUserFromLocalStorage());
    }
    isAuthenticated() : boolean {
        let user: LoginResponse = this.normalizeUser(this.getUserFromLocalStorage());
        return (user && (user.rolle == "admin" || user.rolle == "user"));
    }
    isAdmin() : boolean {
        let user: LoginResponse = this.normalizeUser(this.getUserFromLocalStorage());
        return (user && user.rolle == "admin");
    }

    authenticate(loginData: LoginModel) : Observable<LoginResponse> {
        return this._apiService.Login(loginData)
            .pipe(map(response => {
                const normalized = this.normalizeUser(response as LoginResponse);
                localStorage.setItem('currentUser', JSON.stringify(normalized));
                return normalized;
            }));
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

    private normalizeUser(user: LoginResponse) : LoginResponse {
        if (!user)
            return null;

        const anyUser = user as any;
        if (anyUser.rolle === undefined && anyUser.Rolle !== undefined)
            anyUser.rolle = anyUser.Rolle;
        if (anyUser.brukernavn === undefined && anyUser.Brukernavn !== undefined)
            anyUser.brukernavn = anyUser.Brukernavn;
        if (anyUser.token === undefined && anyUser.Token !== undefined)
            anyUser.token = anyUser.Token;
        if (anyUser.personIDer === undefined && anyUser.PersonIDer !== undefined)
            anyUser.personIDer = anyUser.PersonIDer;

        return anyUser as LoginResponse;
    }
}
