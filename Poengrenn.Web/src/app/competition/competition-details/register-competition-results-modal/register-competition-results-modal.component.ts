import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { Konkurranse, KonkurranseDeltaker, KonkurranseLag, KonkurranseDeltakerTid, KonkurranseKlasse, Person } from 'app/_models/models';
import { IDurationViewModel, DurationViewModel } from '../../models';
import { TagContentType } from '@angular/compiler';
import { Observable } from 'rxjs/Observable';
import { resetFakeAsyncZone } from '@angular/core/testing';
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
  teams: KonkurranseLag[];
  currentTeam: KonkurranseLag;
  lastSaved: string;

  participantIdx = 0;
  teamIdx = 0;

  findParticipantMessage = "";
  disableSave = false;
  hasTeams: boolean = false;

  startTime: IDurationViewModel;
  endTime: IDurationViewModel;
  totalTime: IDurationViewModel;

  warning: string;
  registerForTeams: boolean;

  constructor(
    public _activeModal: NgbActiveModal,
    private _apiService: ApiService) {      
  }

  ngOnInit() { 
    console.log("ngInit", this.competition);
    this.hasTeams = this.competition.konkurranseDeltakere.some(d => this.isTeam(d));
    
    const compDate = moment(this.competition.dato);
    if (moment().isBefore(compDate)) {
      this.warning = `Konkurransen har ikke vært ennå... Registrerer du på riktig konkurranse?`;
    }
    this.competition.konkurranseDeltakere = this.competition.konkurranseDeltakere
      .filter(p => p.tilstede === true)
      .sort(function(a:KonkurranseDeltaker, b: KonkurranseDeltaker) {
      return a.startNummer - b.startNummer;
    });

    this.filteredParticipants = this.getFilteredParticipantsWithTimeRegistration();
    this.setParticipant(this.participantIdx);
    
    if (this.hasTeams)
    {
      this.teams = this.getTeams();
      this.setTeam(this.teamIdx);
    }
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
        this.competition.startInterval = result.startInterval;
      });
  }

  saveParticipantResult() {
    if (!this.registerForTeams) {  
      this.currentParticipant.startTid = this.startTime.toStringWithLeadingZero();
      this.currentParticipant.sluttTid = this.endTime.toStringWithLeadingZero();
      this.currentParticipant.tidsforbruk = this.totalTime.toStringWithLeadingZero();

      if (this.currentParticipant.tidsforbruk === "00:00:00") {
        this.currentParticipant.tidsforbruk = null;
      }

      this.saveToDb([this.currentParticipant]);
    } 
    else {
      const teamParticipants = this.competition.konkurranseDeltakere.filter(p => p.lagNummer === this.currentTeam.lagNummer);
      teamParticipants.map(p => {
        p.startTid = this.startTime.toStringWithLeadingZero();
        p.sluttTid = this.endTime.toStringWithLeadingZero();
        p.tidsforbruk = this.totalTime.toStringWithLeadingZero();
        
        if (p.tidsforbruk === "00:00:00") {
          p.tidsforbruk = null;
        }
      });

      this.saveToDb(teamParticipants);
    }
  }
  private saveToDb(participants: KonkurranseDeltaker[]) {
    this.lastSaved = "";
    participants.forEach((participant, index) => {
      this._apiService.UpdateCompetitionParticipant(participant)
        .subscribe((result: KonkurranseDeltaker) => {
          console.log(result);
          const lastSavedParticipant = this.competition.konkurranseDeltakere.find(p => p.person.personID == result.personID);
          this.lastSaved += `${lastSavedParticipant.startNummer} - ${lastSavedParticipant.person.fornavn} ${lastSavedParticipant.person.etternavn}<br/>`;

          if (!this.registerForTeams)
            this.setParticipant(this.nextParticipantWithoutTime());
          else if (index === participants.length - 1) {
            this.setTeam(this.nextTeamWithoutTime());
          }
        });

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
        this.addSeconds(-this.competition.startInterval);
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
        this.addSeconds(-this.competition.startInterval);
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
        this.addSeconds(this.competition.startInterval);
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
        this.addSeconds(this.competition.startInterval);
        this.updateEndTime();
        return i;
      }
    }
  }
  prevTeamWithoutTime() : number {
    let tempStartTime = new DurationViewModel();
    if (this.teamIdx === 0)
      return this.teams.length - 1;

    // Loop down to zero from teamIdx
    for (let i = this.teamIdx - 1; i >= 0; i--) {
      this.updateTempStartTime(i+1, tempStartTime);
      if (!this.totalTimeExists(this.teams[i])) {
        this.updateStartTimeFromTempTime(tempStartTime);
        this.addSeconds(-this.competition.startInterval);
        this.updateEndTime();
        return i;
      }
    }

    // Loop down to teamIdx from teams
    tempStartTime = new DurationViewModel();
    for (let i = this.teams.length - 1; i >= this.teamIdx; i--) {
      if (i+1 < this.teams.length)
        this.updateTempStartTime(i+1, tempStartTime);
      if (!this.totalTimeExists(this.teams[i])) {
        this.updateStartTimeFromTempTime(tempStartTime);
        this.addSeconds(-this.competition.startInterval);
        this.updateEndTime();
        return i;
      }
    }
  }
  nextTeamWithoutTime(): number {
    let tempStartTime = new DurationViewModel();
    if (this.teamIdx === this.teams.length - 1)
      return 0;

    // Loop up from teamIdx to teams.length
    for (let i = this.teamIdx + 1; i < this.teams.length; i++) {
      this.updateTempStartTime(i-1, tempStartTime);
      if (!this.totalTimeExists(this.teams[i])) {
        this.updateStartTimeFromTempTime(tempStartTime);
        this.addSeconds(this.competition.startInterval);
        this.updateEndTime();
        return i;
      }
    }

    // Not found on previous loop.
    // Then loop up from zero to teamIdx
    tempStartTime = new DurationViewModel();
    for (let i = 0; i >= this.teamIdx; i++) {
      if (i === this.teams.length) {
        return this.teamIdx;
      }
      
      this.updateTempStartTime(i, tempStartTime);
      if (!this.totalTimeExists(this.teams[i])) {
        this.updateStartTimeFromTempTime(tempStartTime);
        this.addSeconds(this.competition.startInterval);
        this.updateEndTime();
        return i;
      }
    }
  }
  private totalTimeExists = (participant: KonkurranseDeltakerTid): boolean => {
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
    let participant: KonkurranseDeltakerTid = this.filteredParticipants[i];
    if (this.registerForTeams) {
      participant = this.teams[i];
    }

    if (this.totalTimeExists(participant)) {
      tempStartTime.setDurationFromString(participant.startTid);
    }
  }

  setParticipant(index: number): void {
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

  setParticipantByStartnummer(event: any) {
    this.disableSave = true;
    this.findParticipantMessage = "";
    let currentStartnummer = this.currentParticipant.startNummer;

    let startnummer = +event.currentTarget.value;
    let foundIndex = this.filteredParticipants.findIndex(p => p.startNummer == startnummer);
    if (foundIndex > -1) 
      this.setParticipant(foundIndex);
    else {
      this.findParticipantMessage = "Fant ikke startnummer " + startnummer;
    }
  }

  getTeams(): KonkurranseLag[] {
    const teamParticipants = this.competition.konkurranseDeltakere.filter(d => this.isTeam(d));
    const teams: KonkurranseLag[] = [];
    teamParticipants.forEach((p) => {
      const index = teams.findIndex(t => t.lagNummer === p.lagNummer);
      if (index < 0) {
        let team = new KonkurranseLag();
        team.konkurranseID = p.konkurranseID;
        team.lagNummer = p.lagNummer;
        team.lagNavn = `${p.person.fornavn} ${p.person.etternavn}`;
        team.startNummer = p.startNummer;
        team.startTid = p.startTid;
        team.sluttTid = p.sluttTid;
        team.tidsforbruk = p.tidsforbruk;
        teams.push(team);
      }
      else {
        teams[index].lagNavn += ` & <br/>${p.person.fornavn} ${p.person.etternavn}`;
      }
    });
    return teams;
  }

  setTeam(index: number): void {
    if (index < 0)
      index = this.teams.length - 1;
    else if (index >= this.teams.length)
      index = 0;

    this.currentTeam = this.teams.slice(index)[0];
    this.teamIdx = index;

    if (this.startTime && this.currentTeam && !this.currentTeam.tidsforbruk) {
      this.currentTeam.startTid = this.startTime.toStringWithLeadingZero();
      this.currentTeam.sluttTid = this.currentTeam.startTid;
    }
    this.disableSave = false;

    this.setStartAndEndTime(this.currentTeam);
  }

  setTeamByTeamNumber(event: any) {
    this.disableSave = true;
    this.findParticipantMessage = "";
    let currentTeamNumber = this.currentTeam.lagNummer;

    let teamNumber = +event.currentTarget.value;
    let foundIndex = this.teams.findIndex(p => p.lagNummer == teamNumber);
    if (foundIndex > -1) 
      this.setTeam(foundIndex);
    else {
      this.findParticipantMessage = "Fant ikke lag nummer " + teamNumber;
    }
  }

  private setStartAndEndTime(participant: KonkurranseDeltakerTid) {
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

  private getFilteredParticipantsWithTimeRegistration() : KonkurranseDeltaker[] {
    return this.competition.konkurranseDeltakere.filter((deltaker, i) => {
      if (this.isCompetitionClassWithTimeRegistration(deltaker.klasseID) && deltaker.lagNummer == null)
        return deltaker;
    });
  }
  private isCompetitionClassWithTimeRegistration(klasseID) : boolean {
    let compClass = this.competition.konkurranseKlasser.find(k => k.klasseID === klasseID && k.medTidtaking);
    if (compClass)
      return compClass.medTidtaking;
    else
      return false;
  }

  private isTeam(participant: KonkurranseDeltaker): boolean {
    return participant.lagNummer !== null;
  }
}
