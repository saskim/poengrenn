import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { Konkurranse, KonkurranseDeltaker, KonkurranseKlasse, Person } from 'app/_models/models';
import { ITimeViewModel, TimeViewModel } from '../../models';
declare var moment: any;

@Component({
  selector: 'app-register-competition-results-modal',
  templateUrl: './register-competition-results-modal.component.html',
  styleUrls: ['./register-competition-results-modal.component.scss'],
  providers: [ApiService]
})
export class RegisterCompetitionResultsModalComponent implements OnInit {

  @Input() competition: Konkurranse; // including participants and competition classes

  filteredParticipants: KonkurranseDeltaker[];
  currentParticipant: KonkurranseDeltaker;
  defaultStartDiffInSeconds: number;
  lastSaved: string;

  participantIdx = 0;

  findParticipantMessage = "";
  disableSave = false;

  startTime: ITimeViewModel;
  endTime: ITimeViewModel;
  totalTime: ITimeViewModel;
  
  constructor(
    public _activeModal: NgbActiveModal,
    private _apiService: ApiService) {      
  }

  ngOnInit() {
    
    this.competition.konkurranseDeltakere = this.competition.konkurranseDeltakere
      .filter(p => p.tilstede === true)
      .sort(function(a:KonkurranseDeltaker, b: KonkurranseDeltaker) {
      return a.startNummer - b.startNummer;
    });
    
    this.defaultStartDiffInSeconds = 15;

    this.filteredParticipants = this.filterParticipantsWithTime();
    this.getParticipant(this.participantIdx);
  }

  updateTidsforbruk() {
    this.totalTime = new TimeViewModel(0, 0, 0);
    if (this.startTime && this.endTime) {
      let startDuration = moment.duration({seconds: this.startTime.duration.second, minutes: this.startTime.duration.minute, hours: this.startTime.duration.hour});
      let endDuration = moment.duration({seconds: this.endTime.duration.second, minutes: this.endTime.duration.minute, hours: this.endTime.duration.hour});
      if (endDuration >= startDuration) {
        const diff = moment.duration(endDuration.asMilliseconds() - startDuration.asMilliseconds());
        this.totalTime = new TimeViewModel(diff.hours(), diff.minutes(), diff.seconds());
      }
    }
  }

  updateEndTime() {
    this.endTime.duration = this.startTime.duration;
  }

  addSeconds(seconds: number) {
    this.startTime.add(0, 0, seconds);
    this.updateTidsforbruk();
  }

  saveParticipantResult() {    
    this.currentParticipant.startTid = this.startTime.toStringWithLeadingZero();
    this.currentParticipant.sluttTid = this.endTime.toStringWithLeadingZero();
    this.currentParticipant.tidsforbruk = this.totalTime.toStringWithLeadingZero();

    this._apiService.UpdateCompetitionParticipant(this.currentParticipant)
      .subscribe((result: KonkurranseDeltaker) => {
        console.log(result);
        const lastSavedPerson = this.filteredParticipants.find(p => p.person.personID == result.personID).person;
        this.lastSaved = `${lastSavedPerson.personID} - ${lastSavedPerson.fornavn} ${lastSavedPerson.etternavn}`;

        // TODO:
        // Get next participant in selected class
        this.getParticipant(this.nextParticipantWithoutTime());
      });
  }

  prevParticipantWithoutTime() : number {
    // Loop down to zero from participantIdx
    for (let i = this.participantIdx - 1; i >= 0; i--) {
      if (!this.filteredParticipants[i].tidsforbruk) {
        this.updateEndTime();
        return i;
      }
    }

    // Loop down to participantIdx from filteredParticipants
    for (let i = this.filteredParticipants.length - 1; i >= this.participantIdx; i--) {
      if (!this.filteredParticipants[i].tidsforbruk) {
        this.updateEndTime();
        return i;
      }
    }
  }
  nextParticipantWithoutTime(): number {
    if (this.participantIdx === this.filteredParticipants.length - 1)
      return 0;

    // Loop up from participantIdx to filteredParticipants.length
    for (let i = this.participantIdx + 1; i < this.filteredParticipants.length; i++) {
      if (!this.filteredParticipants[i].tidsforbruk) {
        this.addSeconds(this.defaultStartDiffInSeconds);
        this.updateEndTime();
        return i;
      }
    }

    // Not found on previous loop.
    // Then loop up from zero to participantIdx
    for (let i = 0; i >= this.participantIdx; i++) {
      if (i === this.filteredParticipants.length)
        return this.participantIdx;

      if (!this.filteredParticipants[i].tidsforbruk) {
        this.addSeconds(this.defaultStartDiffInSeconds);
        this.updateEndTime();
        return i;
      }
    }
  }

  getParticipant(index) {
    this.findParticipantMessage = "";
    if (index < 0)
      index = this.filteredParticipants.length - 1;
    else if (index >= this.filteredParticipants.length)
      index = 0;

    let prevStartTid = (!this.currentParticipant) ? "00:00:00" : this.currentParticipant.startTid;

    this.currentParticipant = this.filteredParticipants.slice(index)[0];
    this.participantIdx = index;

    if (this.currentParticipant && !this.currentParticipant.tidsforbruk) {
      //this.startTime.add(0, 0, this.defaultStartDiffInSeconds);
      //this.addSeconds(this.defaultStartDiffInSeconds);
      this.currentParticipant.startTid = this.startTime.toStringWithLeadingZero();//prevStartTid;
      this.currentParticipant.sluttTid = this.currentParticipant.startTid;
    }
    this.disableSave = false;

    this.setStartAndEndTime(this.currentParticipant);
  }

  getParticipantByStartnummer(event: any) {
    this.disableSave = true;
    this.findParticipantMessage = "";
    let currentStartnummer = this.currentParticipant.startNummer;

    let startnummer = +event.currentTarget.value;
    let foundIndex = this.filteredParticipants.findIndex(p => p.startNummer == startnummer);
    if (foundIndex > -1) 
      this.getParticipant(foundIndex);
    else {
      this.findParticipantMessage = "Fant ikke startnummer " + startnummer;
    }
  }

  private setStartAndEndTime(participant: KonkurranseDeltaker) {
    if (!participant)
      return;

    this.startTime = new TimeViewModel(
      +participant.startTid.slice(0, 2),
      +participant.startTid.slice(3, 5),
      +participant.startTid.slice(6, 8)
    );

    this.endTime = new TimeViewModel(
      +participant.sluttTid.slice(0, 2),
      +participant.sluttTid.slice(3, 5),
      +participant.sluttTid.slice(6, 8)
    );

    this.updateTidsforbruk();
  }

  private toTimeWithLeadingZeros(time: TimeViewModel): string {
    let hh = (time.duration.hour < 10) ? `0${time.duration.hour}` : time.duration.hour;
    let mm = (time.duration.minute < 10) ? `0${time.duration.minute}` : time.duration.minute;
    let ss = (time.duration.second < 10) ? `0${time.duration.second}` : time.duration.second;
    return `${hh}:${mm}:${ss}`;
  }

  private filterParticipantsWithTime() : KonkurranseDeltaker[] {
    return this.competition.konkurranseDeltakere.filter((deltaker, i) => {
      if (this.isCompetitionClassWithTime(deltaker.klasseID))
        return deltaker;
    });
  }
  private isCompetitionClassWithTime(klasseID) : boolean {
    let compClass = this.competition.konkurranseKlasser.find(k => k.klasseID === klasseID && k.medTidtaking);
    if (compClass)
      return compClass.medTidtaking;
    else
      return false;
  }
}
