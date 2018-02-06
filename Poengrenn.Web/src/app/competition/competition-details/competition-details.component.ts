import { Component, OnInit } from '@angular/core';
import { ActivatedRoute  } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/map';
import { NgbModal, NgbModalOptions  } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { AuthService } from 'app/_services/auth.service';
import { Konkurranse, KonkurranseKlasse, KonkurranseDeltaker, NyKonkurranseDeltaker, Person } from 'app/_models/models';
import { EditCompetitionParticipantModalComponent } from './edit-competition-participant-modal/edit-competition-participant-modal.component';
import { PersonModalComponent } from './person-modal/person-modal.component';
import { RegisterCompetitionResultsModalComponent } from './register-competition-results-modal/register-competition-results-modal.component';
import { EditCompetitionModalComponent } from './edit-competition-modal/edit-competition-modal.component';
import { GENDERS, COMP_STATUSES } from 'app/_shared/constants/constants';
declare var moment: any;

@Component({
  selector: 'app-competition-details',
  templateUrl: './competition-details.component.html',
  styleUrls: ['./competition-details.component.scss'],
  providers: []
})
export class CompetitionDetailsComponent implements OnInit {

  competition: Konkurranse;
  competitionClasses: KonkurranseKlasse[];
  persons: Person[];

  searchModel: any;
  selectedPerson: Person;
  selectedCompetitionClass: KonkurranseKlasse;
  matchingCompetitionClasses: KonkurranseKlasse[];
  errorMessage: string;
  canRegister: boolean;

  lastAddedParticipant: KonkurranseDeltaker;
  lastAddedPersonMessage: string;
  updateMessage: string;
  relatedPersons: Person[];  // Persons the logged in user can sign up

  filteredParticpants: KonkurranseDeltaker[];
  filter : {
    genders: string[];
    competitionClasses: string[];
    startNumber: string;
    firstname: string;
    lastname: string;
  }
  showGenderFilter: boolean;
  showClassIDFilter: boolean;

  genders: any[];
  compStatuses: { id: string, displayText: string }[];

  isDone: boolean;
  isActive: boolean;
  compStatus: { id: string, displayText: string };

  constructor(
    private _route: ActivatedRoute,
    private _apiService: ApiService,
    private _authService: AuthService,
    private _modalService: NgbModal) { }

  ngOnInit() {
    let id = this._route.snapshot.params['id'];

    this.genders = GENDERS.slice(0, GENDERS.length-1);
    this.compStatuses = COMP_STATUSES;

    this.getPersonsForSearch();
    this.getCompetition(id);

    this.filter = {
      genders: new Array<string>(),
      competitionClasses: new Array<string>(),
      startNumber: '',
      firstname: '',
      lastname: ''
    }

    this.updateMessage = "";
  }

  isLoggedIn() {
    return this._authService.isAuthenticated();
  }

  setRelatedPersons() {
    const user = this._authService.loggedInUser();
    if (user) {
      this.relatedPersons = this.persons.filter(person => (user['personIDer']) ? user['personIDer'].includes(person.personID) : false);
    }
  }

  isRegistered(person) {
    return this.competition.konkurranseDeltakere.find(deltaker => person.personID === deltaker.personID)
  }

  onPersonSelected(selectedItemEvent) {
    this.setSelectedPerson(selectedItemEvent.item);
  }
  onCompetitionClassChange(competitionClass: KonkurranseKlasse) {
    this.selectedCompetitionClass = competitionClass;
  }

  onFilterChange(event: Event, filterKey: string, filterValue: any) {
    function toggleFilterValue(array: string[], value: string, checked: boolean) {
      let index = array.findIndex(g => g == filterValue);
      if (checked && index === -1)
        array.push(filterValue);
      else if (!checked && index >= 0)
        array.splice(index, 1);
    }

    if (filterKey == 'gender')
    {
      toggleFilterValue(this.filter.genders, filterValue, event.currentTarget['checked']);
    }
    else if (filterKey == 'klasseID')
    {
      toggleFilterValue(this.filter.competitionClasses, filterValue, event.currentTarget['checked']);
    }

    this.filterCompetitionParticipants(this.competition.konkurranseDeltakere);
  }

  onToggleAllClasses(checked: boolean) {
    this.filter.competitionClasses = [];
    if(checked) {
      this.competitionClasses.forEach(c => {
        this.filter.competitionClasses.push(c.klasseID);
      });
    }
  }

  onFilterStartNumber(event: Event) {
    if (!event)
      this.filter.startNumber = "";
    else
      this.filter.startNumber =  event.currentTarget["value"];
    this.filterCompetitionParticipants(this.competition.konkurranseDeltakere);
  }
  onFilterFirstname(event: Event){
    if (!event)
      this.filter.firstname = "";
    else
      this.filter.firstname = event.currentTarget["value"];
    this.filterCompetitionParticipants(this.competition.konkurranseDeltakere);
  }
  onFilterLastname(event: Event){
    if (!event)
      this.filter.lastname = "";
    else
      this.filter.lastname = event.currentTarget["value"];
    this.filterCompetitionParticipants(this.competition.konkurranseDeltakere);
  }

  orderParticipantsBy(type: string) {
    function compareStrings(a, b) {
      // var nameA = a.person.fornavn.toUpperCase(); // ignore upper and lowercase
      // var nameB = b.person.fornavn.toUpperCase(); // ignore upper and lowercase
      var strA = (!a) ? "" : a.toUpperCase(); // ignore upper and lowercase
      var strB = (!b) ? "" : b.toUpperCase(); // ignore upper and lowercase
      if (strA < strB) {
        return -1;
      }
      if (strA > strB) {
        return 1;
      }

      // names must be equal
      return 0;
    }

    switch (type) {
      case 'startnummer':
        this.filteredParticpants.sort((p1, p2) => {
          return p1.startNummer - p2.startNummer;
        });
        break;
      case 'fornavn':
        this.filteredParticpants.sort((p1, p2) => {
          return compareStrings(p1.person.fornavn, p2.person.fornavn);
        });
        break;
      case 'etternavn':
        this.filteredParticpants.sort((p1, p2) => {
          return compareStrings(p1.person.etternavn, p2.person.etternavn);
        });
        break;
      case 'gender':
        this.filteredParticpants.sort((p1, p2) => {
          return compareStrings(p1.person.kjonn, p2.person.kjonn);
        });
        break;
      case 'klasseID':
        this.filteredParticpants.sort((p1, p2) => {
          return compareStrings(p1.klasseID, p2.klasseID);
        });
        break;
      case 'tidsforbruk':
        this.filteredParticpants.sort((p1, p2) => {
          let durationP1 = moment.duration(p1.tidsforbruk);
          let durationP2 = moment.duration(p2.tidsforbruk);

          if (!p1.tidsforbruk) durationP1 = moment.duration(durationP1).add(moment.duration(100000000000000));
          if (!p2.tidsforbruk) durationP2 = moment.duration(durationP2).add(moment.duration(100000000000000));

          if (durationP1.asMilliseconds() === 0)
            durationP1 = moment.duration(durationP1).add(moment.duration(100000000000));
          if (durationP2.asMilliseconds() === 0)
            durationP2 = moment.duration(durationP2).add(moment.duration(100000000000));

          return durationP1 - durationP2;
        });
        break;
      case 'tilstede':
        this.filteredParticpants.sort((p1, p2) => {
          if (p1.tilstede == p2.tilstede) return 0;
          return (p1.tilstede > p2.tilstede) ? -1 : 1;
        });
        break;
      case 'betalt':
        this.filteredParticpants.sort((p1, p2) => {
          if (p1.betalt == p2.betalt) return 0;
          return (p1.betalt > p2.betalt) ? -1 : 1;
        });
        break;
    }

  }

  participantsIsFiltered() : boolean {
    return !!this.filter.startNumber ||
           !!this.filter.firstname ||
           !!this.filter.lastname ||
           this.filter.genders.length > 0 ||
           this.filter.competitionClasses.length > 0;
  }

  countRegistered(): number {
    if (!this.competition || !this.competition.konkurranseDeltakere)
      return 0;
    return this.competition.konkurranseDeltakere.length;
  }
  countAttending(): number {
    if (!this.competition || !this.competition.konkurranseDeltakere)
      return 0;
    return this.competition.konkurranseDeltakere.filter(p => p.tilstede === true).length;
  }
  countPaid(): number {
    if (!this.competition || !this.competition.konkurranseDeltakere)
      return 0;
    return this.competition.konkurranseDeltakere.filter(p => p.betalt === true).length;
  }
  countFiltered(): number {
    if (!this.filteredParticpants)
      return 0;
    return this.filteredParticpants.length;
  }
  countFilteredAttending(): number {
    if (!this.filteredParticpants)
      return 0;
    return this.filteredParticpants.filter(p => p.tilstede === true).length;
  }
  countFilteredPaid(): number {
    if (!this.filteredParticpants)
      return 0;
    return this.filteredParticpants.filter(p => p.betalt === true).length;;
  }

  isAdmin() {
    return this._authService.isAdmin();
  }
  isMe() {
    if (!this.selectedPerson)
      return false;

    let loggedInUser = this._authService.loggedInUser();
    return loggedInUser &&
          (loggedInUser.brukernavn == this.selectedPerson.personID.toString()
          ||
          (loggedInUser.personIDer && loggedInUser.personIDer.indexOf(this.selectedPerson.personID) > -1));
  }

  updateTilstede(participant: KonkurranseDeltaker) {
    participant.tilstede = !participant.tilstede;
    this.saveCompetitionParticipant(participant);
  }

  updateBetalt(participant: KonkurranseDeltaker) {
    participant.betalt = !participant.betalt;
    this.saveCompetitionParticipant(participant);
  }

  private saveCompetitionParticipant(participant: KonkurranseDeltaker) {
    this._apiService.UpdateCompetitionParticipant(participant)
      .subscribe((result: KonkurranseDeltaker) => {
        this.getCompetitionParticipants();
    });
  }

  registerForCompetition() {

    let nyKonkurranseDeltaker = new NyKonkurranseDeltaker();
    nyKonkurranseDeltaker.personID = this.selectedPerson.personID;
    nyKonkurranseDeltaker.klasseID = this.selectedCompetitionClass.klasseID;
    nyKonkurranseDeltaker.typeID = this.selectedCompetitionClass.typeID;

    var lastParticipantInClass = this.competition.konkurranseDeltakere
                            .sort((p1, p2) => { return p2.startNummer - p1.startNummer })
                            .find(p => p.klasseID == nyKonkurranseDeltaker.klasseID);

    var curCompClass = this.competitionClasses.find(c => c.klasseID == nyKonkurranseDeltaker.klasseID);

    if (lastParticipantInClass && lastParticipantInClass.startNummer >= curCompClass.sisteStartnummer) {
      this.lastAddedPersonMessage = `<h5>Sjekk startnummer!</h5>Deltakeren har fått et startnummer som er høyere enn siste startnummer i gjeldende klasse.<br/>Rediger deltakeren og skriv inn et annet startnummer`;
    }

    console.log(nyKonkurranseDeltaker);
    this._apiService.RegisterForCompetition(this.competition.konkurranseID, nyKonkurranseDeltaker)
      .subscribe((result:KonkurranseDeltaker[]) => {
        console.log(result);

        this.getCompetitionParticipants();
        this.lastAddedParticipant = result.find(d => d.konkurranseID == this.competition.konkurranseID);
        this.lastAddedParticipant.person = this.selectedPerson;
        this.setSelectedPerson(null);
      });
  }

  getCompetition(id: number) : void {
    this._apiService.GetCompetition(id)
      .subscribe((result: Konkurranse) => {
        console.log(result);
        this.competition = result;

        this.isDone = this.competition.dato > new Date();
        this.isActive = (this.competition.status === "Aktiv");

        this.compStatus = this.compStatuses.find(s => {
          return (s.id === this.competition.status);
        });

        this.getCompetitionClasses();
        this.getCompetitionParticipants();
      });
  }

  getKonkurranseKlasseNavn(klasseID: string) : string {
    let klasse: KonkurranseKlasse;
    if (this.competitionClasses)
      klasse = this.competitionClasses.find(c => c.klasseID == klasseID);

    return (klasse) ? klasse.navn : '';
  }


  // TODO: Create component of the searchbox
  search = (text$: Observable<string>) =>
    text$
      .debounceTime(200)
      .map(term => {
        return term == '' ? [] : this.filteredOnFirstnameAndLastname(term);
        //return term == '' ? [] :  this.persons.filter(p => p.fornavn.toLowerCase().indexOf(term.toLowerCase()) > -1 || p.etternavn.toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10);
      });


  private filteredOnFirstname(term: string) {
    let persons = this.persons.filter(p => p.fornavn.toLowerCase().indexOf(term.toLowerCase()) > -1);
    return persons;
  }
  private filteredOnLastname(term: string) {
    let persons = this.persons.filter(p => p.etternavn.toLowerCase().indexOf(term.toLowerCase()) > -1);
    return persons;
  }
  private filteredOnFirstnameAndLastname(term: string) {
    this.updateMessage = "";

    let filteredPersons = [];
    let words = term.split(" ");
    words.forEach(word => {
      let foundFirstname = this.filteredOnFirstname(word);
      let foundLastname = this.filteredOnLastname(word);
      filteredPersons = filteredPersons.concat(foundFirstname);
      filteredPersons = filteredPersons.concat(foundLastname);
      filteredPersons = filteredPersons.filter((person, index, self) => self.findIndex(t => t.personID === person.personID) === index)
    });
    console.log(filteredPersons);
    return filteredPersons;//this.persons.filter(p => p.etternavn.toLowerCase().indexOf(term.toLowerCase()) > -1);
  }

  searchFormatter(p: {fornavn: string, etternavn: string, fodselsar: number, personID: string}) {
    return p.fornavn + " " + p.etternavn + " (" + p.fodselsar + ")";
  }

  openEditCompetitionParticipant(participant: KonkurranseDeltaker) {
    let options: NgbModalOptions = { size: "lg" };
    const modalRef = this._modalService.open(EditCompetitionParticipantModalComponent, options);

    modalRef.componentInstance.participant = Object.assign({}, participant);
    modalRef.componentInstance.matchingCompetitionClasses = this.findBestMatchingClass(participant.person);

    modalRef.result.then((result) => {
      if (result.updated) {
        console.log(result.updated);
        let updatedParticipant = result.updated as KonkurranseDeltaker;
        let oldParticipant = result.old as KonkurranseDeltaker;
        let changedStartNumberStr = (updatedParticipant.startNummer != oldParticipant.startNummer) ? `(endret fra ${oldParticipant.startNummer})` : ``;
        this.updateMessage = `Sist oppdatert: ${(updatedParticipant.person) ? updatedParticipant.person.fornavn : ''} ${(updatedParticipant.person) ? updatedParticipant.person.etternavn : ''}
                             <h5>Startnummer: ${updatedParticipant.startNummer} ${changedStartNumberStr}</h5>`;
      }
      else if (result.deleted) {
        console.log(result.deleted);
      }
      this.getCompetitionParticipants();
    });
  }
  openPersonModal(editPerson: Person) {
    let options: NgbModalOptions = { size: "lg" };
    const modalRef = this._modalService.open(PersonModalComponent, options);
    modalRef.componentInstance.editPerson = editPerson;
    modalRef.componentInstance.existingPersons = this.persons;

    modalRef.result.then((result) => {
      if (typeof result == "object" && result.personID) {
        let person = result as Person;
        this.getPersonsForSearch();
        this.setSelectedPerson(person);

        this.searchModel = person;
      }
    });
  }
  openEditResultsModal() {
    let options: NgbModalOptions = { size: "lg" };
    const modalRef = this._modalService.open(RegisterCompetitionResultsModalComponent, options);

    const comp = Object.assign({}, this.competition);
    comp.konkurranseKlasser = this.competitionClasses;
    modalRef.componentInstance.competition = comp;

    modalRef.result.then((result) => {
      this.getCompetition(this.competition.konkurranseID);
      this.getCompetitionParticipants();
    });
  }

  openEditCompetitionModal() {
    let options: NgbModalOptions = { size: "lg" };
    const modalRef = this._modalService.open(EditCompetitionModalComponent, options);

    modalRef.componentInstance.competition = this.competition;

    modalRef.result.then((result) => {
      this.getCompetition(this.competition.konkurranseID);
    });
  }

  setSelectedPerson(person: Person) {
    this.selectedPerson = person;
    console.log(this.selectedPerson);

    if (!person) {
      this.searchModel = null;
      this.matchingCompetitionClasses = null;
      this.errorMessage = "";
      return;
    }
    this.canRegister = true;
    this.errorMessage = "Velg konkurranseklasse";
    this.selectedCompetitionClass = new KonkurranseKlasse();

    let deltaker = this.competition.konkurranseDeltakere.find(d => d.personID == this.selectedPerson.personID);
    if (deltaker) {
      this.canRegister = false;
      this.errorMessage = "Du er allerede påmeldt i klassen '" + this.getKonkurranseKlasseNavn(deltaker.klasseID) + "'";
    }

    this.matchingCompetitionClasses = this.findBestMatchingClass(this.selectedPerson);
    if (this.matchingCompetitionClasses.length == 1 && this.canRegister)
      this.onCompetitionClassChange(this.matchingCompetitionClasses[0]);
  }

  private filterCompetitionParticipants(participants: KonkurranseDeltaker[]) {
    let genderFilter = this.filter.genders;
    let competitionClassFilter = this.filter.competitionClasses;
    let startNumberFilter = this.filter.startNumber;
    let firstnameFilter = this.filter.firstname;
    let lastnameFilter = this.filter.lastname;

    if (genderFilter.length == 0 &&
        competitionClassFilter.length == 0 &&
        startNumberFilter.length == 0 &&
        firstnameFilter.length == 0 &&
        lastnameFilter.length == 0)
      this.filteredParticpants = this.competition.konkurranseDeltakere;

    else {
      let participantsResult = participants.filter(function (participant) {
        let matchingGender = (genderFilter.length == 0) ? true : (genderFilter.some(g => g == participant.person.kjonn));
        let matchingCompetitionClasses = (competitionClassFilter.length == 0) ? true : (competitionClassFilter.some(c => c == participant.klasseID));
        let matchingStartNumber = (startNumberFilter.length == 0) ? true : (participant.startNummer.toString().startsWith(startNumberFilter));
        let matchingFirstname = (firstnameFilter.length == 0) ? true : (participant.person.fornavn.toLowerCase().indexOf(firstnameFilter.toLowerCase()) >= 0);
        let matchingLastname = (lastnameFilter.length == 0) ? true : (participant.person.etternavn.toLowerCase().indexOf(lastnameFilter.toLowerCase()) >= 0);

        return (matchingGender && matchingCompetitionClasses && matchingStartNumber && matchingFirstname && matchingLastname);
      });

      if (participantsResult)
        this.filteredParticpants = participantsResult;
    }
    this.orderParticipantsBy("startnummer");
  }

  private getCompetitionClasses() : void {
    this._apiService.GetCompetitionClassesByTypeID(this.competition.typeID)
      .subscribe((result: KonkurranseKlasse[]) => {
        this.competitionClasses = result.sort(function(a, b) {
          return a.minAlder - b.minAlder;
        });
      });
  }

  private getCompetitionParticipants() : void {
    this._apiService.GetCompetitionParticipants(this.competition.konkurranseID)
      .subscribe((result: KonkurranseDeltaker[]) => {
        this.competition.konkurranseDeltakere = result;
        this.filterCompetitionParticipants(this.competition.konkurranseDeltakere);
      });
  }

  private getPersonsForSearch() : void {
    this._apiService.GetAllPersons()
      .subscribe((result: Person[]) => {
        this.persons = result;
        this.setRelatedPersons();
      });
  }

  private findBestMatchingClass(person: Person) {
    let alder = new Date().getFullYear() - person.fodselsar;

    let compClasses = this.competitionClasses.filter(function (item) {
      let matchingAge = person.fodselsar == 1900 || (item.minAlder <= alder && item.maxAlder >= alder);
      let matchingGender = (item.kjonn.toLowerCase() === person.kjonn.toLowerCase() || item.kjonn.toLowerCase() === 'mix');

      return (matchingAge && matchingGender);
    });

    if (compClasses && compClasses.length > 0)
      return compClasses;
  }



}
