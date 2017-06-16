import { Component, OnInit } from '@angular/core';
import { ActivatedRoute  } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/map';
import { NgbModal, NgbModalOptions  } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { Konkurranse, KonkurranseKlasse, KonkurranseDeltaker, NyKonkurranseDeltaker, Person } from 'app/_models/models';
import { EditCompetitionParticipantModalComponent } from './edit-competition-participant-modal/edit-competition-participant-modal.component';
import { AddCompetitionParticipantModalComponent } from './add-competition-participant-modal/add-competition-participant-modal.component';
import { RegisterCompetitionResultsModalComponent } from './register-competition-results-modal/register-competition-results-modal.component';
import { GENDERS } from 'app/_shared/constants/constants';

@Component({
  selector: 'app-competition-details',
  templateUrl: './competition-details.component.html',
  styleUrls: ['./competition-details.component.scss'],
  providers: [ApiService]
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

  filteredParticpants: KonkurranseDeltaker[];
  filter : {
    genders: string[];
    competitionClasses: string[];
  }

  genders: any[];

  isDone: boolean;

  constructor(
    private _route: ActivatedRoute, 
    private _apiService: ApiService,
    private _modalService: NgbModal) { }

  ngOnInit() {
    let id = this._route.snapshot.params['id'];
    
    this.genders = GENDERS.filter((g) => g.id !== 'Mix');

    this.getPersonsForSearch();
    this.getCompetition(id);

    this.filter = {
      genders: new Array<string>(),
      competitionClasses: new Array<string>()
    }
  }

  onPersonSelected(selectedItemEvent) {
    this.selectedPerson = selectedItemEvent.item;
    console.log(this.selectedPerson);

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

  registerForCompetition() {
    let nyKonkurranseDeltaker = new NyKonkurranseDeltaker();
    nyKonkurranseDeltaker.personID = this.selectedPerson.personID;
    nyKonkurranseDeltaker.klasseID = this.selectedCompetitionClass.klasseID;

    this._apiService.RegisterForCompetition(this.competition.konkurranseID, nyKonkurranseDeltaker)
      .subscribe((result:KonkurranseDeltaker) => {
        console.log(result);
        
        this.getCompetitionParticipants();
      });
  }

  getCompetition(id: number) : void {
    this._apiService.GetCompetition(id)
      .subscribe((result: Konkurranse) => {
        console.log(result);
        this.competition = result;

        this.isDone = this.competition.dato > new Date();

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

  search = (text$: Observable<string>) =>
    text$
      .debounceTime(200)
      .map(term => {
        return term == '' ? [] :  this.persons.filter(p => p.fornavn.toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10);
      });

  searchFormatter(p: {fornavn: string, etternavn: string, fodselsar: number, personID: string}) {
    return p.fornavn + " " + p.etternavn + " (" + p.fodselsar + ")";
  } 

  openEditCompetitionParticipant(participant: KonkurranseDeltaker) {
    let options: NgbModalOptions = { size: "lg" };
    const modalRef = this._modalService.open(EditCompetitionParticipantModalComponent, options);
    
    modalRef.componentInstance.participant = participant;
    modalRef.componentInstance.matchingCompetitionClasses = this.findBestMatchingClass(participant.person);

    modalRef.result.then((result) => {
      this.getCompetitionParticipants();
    });
  }
  openAddPersonModal() {
    let options: NgbModalOptions = { size: "lg" };
    const modalRef = this._modalService.open(AddCompetitionParticipantModalComponent, options);

    modalRef.result.then((result) => {
      // TODO
    });
  }
  openEditResultsModal() {
    let options: NgbModalOptions = { size: "lg" };
    const modalRef = this._modalService.open(RegisterCompetitionResultsModalComponent, options);
    
    modalRef.componentInstance.participants = this.competition.konkurranseDeltakere;

    modalRef.result.then((result) => {
      // TODO
    });
  }

  private filterCompetitionParticipants(participants: KonkurranseDeltaker[]) {
    let genderFilter = this.filter.genders;
    let competitionClassFilter = this.filter.competitionClasses;
    if (genderFilter.length == 0 && competitionClassFilter.length == 0)
      this.filteredParticpants = this.competition.konkurranseDeltakere;

    else {
      let participantsResult = participants.filter(function (participant) {
        let matchingGender = (genderFilter.length == 0) ? true : (genderFilter.some(g => g == participant.person.kjonn));
        let matchingCompetitionClasses = (competitionClassFilter.length == 0) ? true : (competitionClassFilter.some(c => c == participant.klasseID));
        
        return (matchingGender && matchingCompetitionClasses);
      });

      if (participantsResult)
        this.filteredParticpants = participantsResult;
        
      console.log(this.filteredParticpants);
    }
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
      });
  }

  private findBestMatchingClass(person: Person) {
    let alder = new Date().getFullYear() - person.fodselsar;
    return this.competitionClasses.filter(function (item) {
      let matchingAge = (item.minAlder <= alder && item.maxAlder >= alder);
      let matchingGender = (item.kjonn.toLowerCase() === person.kjonn.toLowerCase() || item.kjonn.toLowerCase() === 'mix');
      
      return (matchingAge && matchingGender);
    });
  }
}
