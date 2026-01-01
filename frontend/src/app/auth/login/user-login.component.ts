import { Component, OnInit, SimpleChanges } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/map';
import { AuthService } from 'app/_services/auth.service';
import { ApiService } from 'app/_services/api.service';
import { LoginModel, LoginResponse, Person } from 'app/_models/models';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.scss']
})
export class UserLoginComponent implements OnInit {

  persons: Person[];

  searchModel: any;
  selectedPerson: Person;

  model: LoginModel;
  returnUrl: string;
  errorMessage: string;
  canLogin: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private _authService: AuthService,
    private _apiService: ApiService) { }

  ngOnInit() {
    this._authService.logout();
    this.model = new LoginModel();
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

    this.getPersonsForSearch();
  }

  login() {
    this.errorMessage = "";
    if (!this.model.brukernavn) {
      this.setSelectedPerson(null);
      this.errorMessage = "Det oppstod en feil med pålogging. Vennligst prøv igjen.";
      return;
    }
    this._authService.authenticate(this.model)
      .subscribe((user: LoginResponse) => {
        if (!user || user.rolle != 'user' || !user.token)
          this.errorMessage = "Kunne ikke logge inn. Finner ikke matchende e-post/mobilnr.<br/><br/>Send en e-post til <a href='mailto:sonja.askim@gmail.com'>sonja.askim@gmail.com</a> for å få registrert dette.";
        else if (user.brukernavn == this.model.brukernavn && user.token) {
          this.router.navigate([this.returnUrl]);
        }
      },
      error => {
          this.errorMessage = "Det oppstod en feil med pålogging";
      });
  }

  onPersonSelected(selectedItemEvent) {
    this.setSelectedPerson(selectedItemEvent.item);
  }

  search = (text$: Observable<string>) =>
    text$
      .debounceTime(200)
      .map(term => {
        return term == '' ? [] :  this.persons.filter(p => p.fornavn.toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10);
      });

  searchFormatter(p: {fornavn: string, etternavn: string, fodselsar: number, personID: string}) {
    return p.fornavn + " " + p.etternavn + " (" + p.fodselsar + ")";
  } 

  private getPersonsForSearch() : void {
    this._apiService.GetAllPersons()
      .subscribe((result: Person[]) => {
        this.persons = result;
      });
  }

  setSelectedPerson(person: Person) {
    this.selectedPerson = person;
    console.log(this.selectedPerson);

    if (!person) {
      this.searchModel = null;
      return;
    }
    
    this.model.brukernavn = person.personID.toString();
    this.canLogin = true;
  }
}
