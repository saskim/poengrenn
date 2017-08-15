import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { Endpoints } from 'app/_shared/constants/endpoints';
import { Konkurranse, KonkurranseOpprett, KonkurranseKlasse, KonkurranseDeltaker, NyKonkurranseDeltaker, KonkurranseType, Person, LoginModel, LoginResponse } from 'app/_models/models';

@Injectable()
export class ApiService {

  constructor(private _http: Http) { }

  /* ----- COMPETITION ----- */
  /* Competition types */
  public GetCompetitionTypes() : Observable<KonkurranseType[]> {
    return this.get(Endpoints.CompetitionType.GetAll);
  }

  /* Competition classes */
  public GetCompetitionClasses() : Observable<KonkurranseKlasse[]> {
    return this.get(Endpoints.CompetitionClass.GetAll);
  }
  public GetCompetitionClassesByTypeID(typeID) : Observable<KonkurranseKlasse[]> {
    let url = Endpoints.CompetitionClass.GetByTypeID.replace("{typeId}", encodeURIComponent(typeID));
    return this.get(url);
  }
  public CreateCompetitionClass(competitionClassCreate: KonkurranseKlasse) : Observable<KonkurranseKlasse> {
    return this.post(Endpoints.CompetitionClass.Post, competitionClassCreate);
  }
  public UpdateCompetitionClass(competitionClassUpdate: KonkurranseKlasse) : Observable<KonkurranseKlasse> {
    return this.put(Endpoints.CompetitionClass.Put, competitionClassUpdate);
  }
  public DeleteCompetitionClass(konkurranseKlasseID: string) : Observable<KonkurranseKlasse> {
    let url = Endpoints.CompetitionClass.Delete.replace("{id}", encodeURIComponent(konkurranseKlasseID));
    return this.delete(url);
  }

  /* Competitions */
  public GetAllCompetitions() : Observable<Konkurranse[]> {
    return this.get(Endpoints.Competition.GetAll);
  }
  public GetOpenCompetitions() : Observable<Konkurranse[]> {
    return this.get(Endpoints.Competition.GetOpen);
  }
  public GetDoneCompetitions() : Observable<Konkurranse[]> {
    return this.get(Endpoints.Competition.GetDone);
  }
  public GetCompetition(konkurranseID: number) : Observable<Konkurranse> {
    let url = Endpoints.Competition.GetById.replace("{id}", konkurranseID.toString());
    return this.get(url);
  }
  public CreateCompetition(competitionCreate: KonkurranseOpprett) {
    return this.post(Endpoints.Competition.Post, competitionCreate);
  }
  
  /* Competition participants */
  public RegisterForCompetition(konkurranseID: number, nyKonkurranseDeltaker: NyKonkurranseDeltaker) {
    let url = Endpoints.CompetitionParticipant.Post.replace("{id}", konkurranseID.toString());
    return this.post(url, nyKonkurranseDeltaker);
  }
  public GetCompetitionParticipants(konkurranseID: number) : Observable<KonkurranseDeltaker[]> {
    let url = Endpoints.CompetitionParticipant.GetAll.replace("{id}", konkurranseID.toString());
    return this.get(url);
  }
  public UpdateCompetitionParticipant(deltaker: KonkurranseDeltaker) : Observable<KonkurranseDeltaker> {
    let url = Endpoints.CompetitionParticipant.Put.replace("{id}", deltaker.konkurranseID.toString());
    return this.put(url, deltaker);
  }
  public DeleteCompetitionParticipant(deltaker: KonkurranseDeltaker) {
    let url = Endpoints.CompetitionParticipant.Delete
      .replace("{id}", deltaker.konkurranseID.toString())
      .replace("{deltakerId}", deltaker.personID.toString())
      .replace("{klasseId}", deltaker.klasseID.toString());
    return this.delete(url);
  }

  /* ----- PERSON ----- */
  public GetAllPersons() : Observable<Person[]> {
    return this.get(Endpoints.Person.GetAll);
  }

  public AddPerson(person: Person) : Observable<Person> {
    return this.post(Endpoints.Person.Post, person);
  }

  public UpdatePerson(person: Person) : Observable<Person> {
    return this.put(Endpoints.Person.Put, person);
  }

    /* ----- USER ----- */
  public Login(loginData: LoginModel) : Observable<LoginResponse> {
    return this.post(Endpoints.User.Login, loginData);
  }

  /* PRIVATE methods */
  private post(url: string, body: any) : Observable<any> {
    console.log("--------- POST " + url);

    return this._http
      .post(url, body, this.getRequestOptions())
      .map(res => res.json())
      .catch(err => this.handleError(err)); 
  }
  private get(url: string) : Observable<any> {
    console.log("--------- GET " + url);

    return this._http
      .get(url, this.getRequestOptions())
      .map(res => res.json())
      .catch(err => this.handleError(err));
  }
  private put(url: string, obj: any) : Observable<any> {
    console.log("--------- PUT " + url);

    return this._http
      .put(url, obj, this.getRequestOptions())
      .map(res => res.json())
      .catch(err => this.handleError(err));
  }
  private delete(url: string) : Observable<any> {
    console.log("--------- DELETE " + url);

    return this._http
      .delete(url, this.getRequestOptions())
      .map(res => res.json())
      .catch(err => this.handleError(err));
  }

  private getRequestOptions() : RequestOptions {
    let headers = new Headers(
    {
        'Content-Type': 'application/json'
    });
    return new RequestOptions({
      headers: headers
    });
  }

  private handleError(error: Response) {
    console.error('An error occurred', error);
    return Observable.throw(error.json() || 'Server error');
  }
}
