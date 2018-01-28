import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { Konkurranse, KonkurranseDeltaker, KonkurranseKlasse, Person } from 'app/_models/models';
import { IDurationViewModel, DurationViewModel } from '../../models';
import { TagContentType } from '@angular/compiler';
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

  startTime: IDurationViewModel;
  endTime: IDurationViewModel;
  totalTime: IDurationViewModel;

  warning: string;

  constructor(
    public _activeModal: NgbActiveModal,
    private _apiService: ApiService) {      
  }

  ngOnInit() {  
    const compDate = moment(this.competition.dato);
    if (moment().isBefore(compDate)) {
      this.warning = `Konkurransen har ikke vært ennå... Registrerer du på riktig konkurranse?`;
    }
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
    this.totalTime = new DurationViewModel(0, 0, 0);
    if (this.startTime && this.endTime) {
      let startDuration = moment.duration({seconds: this.startTime.duration.second, minutes: this.startTime.duration.minute, hours: this.startTime.duration.hour});
      let endDuration = moment.duration({seconds: this.endTime.duration.second, minutes: this.endTime.duration.minute, hours: this.endTime.duration.hour});
      if (endDuration >= startDuration) {
        const diff = moment.duration(endDuration.asMilliseconds() - startDuration.asMilliseconds());
        this.totalTime = new DurationViewModel(diff.hours(), diff.minutes(), diff.seconds());
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
  saveStartInterval () {
    this._apiService.UpdateCompetition(this.competition)
      .subscribe((result: Konkurranse) => {
        console.log(result);
      });
  }

  saveParticipantResult() {    
    this.currentParticipant.startTid = this.startTime.toStringWithLeadingZero();
    this.currentParticipant.sluttTid = this.endTime.toStringWithLeadingZero();
    this.currentParticipant.tidsforbruk = this.totalTime.toStringWithLeadingZero();

    if (this.currentParticipant.tidsforbruk === "00:00:00") {
      this.currentParticipant.tidsforbruk = null;
    }

    this._apiService.UpdateCompetitionParticipant(this.currentParticipant)
      .subscribe((result: KonkurranseDeltaker) => {
        console.log(result);
        const lastSavedParticipant = this.filteredParticipants.find(p => p.person.personID == result.personID);
        this.lastSaved = `${lastSavedParticipant.startNummer} - ${lastSavedParticipant.person.fornavn} ${lastSavedParticipant.person.etternavn}`;

        // TODO:
        // Get next participant in selected class
        this.getParticipant(this.nextParticipantWithoutTime());
      });
  }

  prevParticipantWithoutTime() : number {
    let tempStartTime = new DurationViewModel();
    if (this.participantIdx === 0)
      return this.filteredParticipants.length - 1;

    // Loop down to zero from participantIdx
    for (let i = this.participantIdx - 1; i >= 0; i--) {
      this.updateTempStartTime(i+1, tempStartTime);
      if (!this.totalTimeExists(this.filteredParticipants[i])) {
        this.updateStartTimeFromTempTime(tempStartTime);
        this.addSeconds(-this.defaultStartDiffInSeconds);
        this.updateEndTime();
        return i;
      }
    }

    // Loop down to participantIdx from filteredParticipants
    tempStartTime = new DurationViewModel();
    for (let i = this.filteredParticipants.length - 1; i >= this.participantIdx; i--) {
      if (i+1 < this.filteredParticipants.length)
        this.updateTempStartTime(i+1, tempStartTime);
      if (!this.totalTimeExists(this.filteredParticipants[i])) {
        this.updateStartTimeFromTempTime(tempStartTime);
        this.addSeconds(-this.defaultStartDiffInSeconds);
        this.updateEndTime();
        return i;
      }
    }
  }
  nextParticipantWithoutTime(): number {
    let tempStartTime = new DurationViewModel();
    if (this.participantIdx === this.filteredParticipants.length - 1)
      return 0;

    // Loop up from participantIdx to filteredParticipants.length
    for (let i = this.participantIdx + 1; i < this.filteredParticipants.length; i++) {
      this.updateTempStartTime(i-1, tempStartTime);
      if (!this.totalTimeExists(this.filteredParticipants[i])) {
        this.updateStartTimeFromTempTime(tempStartTime);
        this.addSeconds(this.defaultStartDiffInSeconds);
        this.updateEndTime();
        return i;
      }
    }

    // Not found on previous loop.
    // Then loop up from zero to participantIdx
    tempStartTime = new DurationViewModel();
    for (let i = 0; i >= this.participantIdx; i++) {
      if (i === this.filteredParticipants.length) {
        return this.participantIdx;
      }
      
      this.updateTempStartTime(i, tempStartTime);
      if (!this.totalTimeExists(this.filteredParticipants[i])) {
        this.updateStartTimeFromTempTime(tempStartTime);
        this.addSeconds(this.defaultStartDiffInSeconds);
        this.updateEndTime();
        return i;
      }
    }
  }
  private totalTimeExists = (participant: KonkurranseDeltaker): boolean => {
    return participant.tidsforbruk && participant.tidsforbruk !== "00:00:00"
  }
  private updateStartTimeFromTempTime = (tempStartTime: DurationViewModel):void => {
    if (!tempStartTime.equals(new DurationViewModel())) {
      this.startTime = new DurationViewModel(
        tempStartTime.duration.hour, 
        tempStartTime.duration.minute, 
        tempStartTime.duration.second
      );
    }
  }
  private updateTempStartTime = (i: number, tempStartTime: DurationViewModel): void => {
    if (this.totalTimeExists(this.filteredParticipants[i])) {
      tempStartTime.setDurationFromString(this.filteredParticipants[i].startTid);
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

    if (this.startTime && this.currentParticipant && !this.currentParticipant.tidsforbruk) {
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

    this.startTime = new DurationViewModel();
    this.endTime = new DurationViewModel();

    if (participant.startTid) {
      this.startTime.setDurationFromString(participant.startTid);
    }

    if (participant.sluttTid) {
      this.endTime.setDurationFromString(participant.sluttTid);

    }

    this.updateTidsforbruk();
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
