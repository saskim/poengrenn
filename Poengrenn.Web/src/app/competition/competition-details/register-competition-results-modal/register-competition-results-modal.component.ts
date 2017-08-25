import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { Konkurranse, KonkurranseDeltaker, KonkurranseKlasse, Person } from 'app/_models/models';
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
  selectedCompetitionClass: KonkurranseKlasse;
  defaultStartDiffInSeconds: number;

  participantIdx = 0;

  findParticipantMessage = "";
  disableSave = false;

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
    
    this.defaultStartDiffInSeconds = 30;

    this.filteredParticipants = this.filterParticipantsWithTime();
    console.log(this.filteredParticipants);
    this.getParticipant(this.participantIdx);
  }

  updateTidsforbruk() {
    let startTidMs = moment.duration(this.currentParticipant.startTid).asMilliseconds();
    let sluttTidMs = moment.duration(this.currentParticipant.sluttTid).asMilliseconds();
    if (startTidMs < sluttTidMs) {
      let tidMs = sluttTidMs - startTidMs;
      this.currentParticipant.tidsforbruk = moment.utc(tidMs).format("HH:mm:ss");
    }
  }

  addSeconds(seconds: number) {
    //this.defaultStartDiffInSeconds = seconds;
    let updateSluttTid = (this.currentParticipant.startTid == this.currentParticipant.sluttTid);
    let newStartTidMs = moment.duration(this.currentParticipant.startTid).asMilliseconds() + seconds * 1000;
    this.currentParticipant.startTid = moment.utc(newStartTidMs).format("HH:mm:ss");
    
    if (updateSluttTid)
      this.currentParticipant.sluttTid = this.currentParticipant.startTid;

    this.updateTidsforbruk();
  }

  saveParticipantResult() {
    console.log(this.currentParticipant);
    this._apiService.UpdateCompetitionParticipant(this.currentParticipant)
      .subscribe((result: KonkurranseDeltaker) => {
        console.log(result);

        // TODO:
        // Get next participant in selected class
        this.getParticipant(this.nextParticipantWithoutTime());
        this.addSeconds(this.defaultStartDiffInSeconds * this.participantIdx);

        //this._activeModal.close("Saved");
      });
  }

  prevParticipantWithoutTime() : number {
    // Loop down to zero from participantIdx
    for (let i = this.participantIdx - 1; i >= 0; i--) {
      if (!this.filteredParticipants[i].tidsforbruk) {
        return i;
      }
    }

    // Loop down to participantIdx from filteredParticipants
    for (let i = this.filteredParticipants.length - 1; i >= this.participantIdx; i--) {
      if (!this.filteredParticipants[i].tidsforbruk) {
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
        return i;
      }
    }

    // Not found on previous loop.
    // Then loop up from zero to participantIdx
    for (let i = 0; i >= this.participantIdx; i++) {
      if (!this.filteredParticipants[i].tidsforbruk) {
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

    if (!this.currentParticipant.tidsforbruk) {
      this.currentParticipant.startTid = prevStartTid;
      this.currentParticipant.sluttTid = this.currentParticipant.startTid;
    }
    this.disableSave = false;
  }

  getParticipantByStartnummer(event: any) {
    this.disableSave = true;
    this.findParticipantMessage = "";
    let currentStartnummer = this.currentParticipant.startNummer;
    console.log(event.currentTarget.value);
    let startnummer = +event.currentTarget.value;
    let foundIndex = this.filteredParticipants.findIndex(p => p.startNummer == startnummer);
    if (foundIndex > -1) 
      this.getParticipant(foundIndex);
    else {
      this.findParticipantMessage = "Fant ikke startnummer " + startnummer;

      //this.getParticipantByStartnummer(currentStartnummer);
    }
  }

  private filterParticipantsWithTime() : KonkurranseDeltaker[] {
    return this.competition.konkurranseDeltakere.filter((deltaker, i) => {
      if (this.isCompetitionClassWithTime(deltaker.klasseID))
        return deltaker;
    });
  }
  private isCompetitionClassWithTime(klasseID) : boolean {
    //console.log(this.competition);
    let compClass = this.competition.konkurranseKlasser.find(k => k.klasseID === klasseID && k.medTidtaking);
    if (compClass)
      return compClass.medTidtaking;
    else
      return false;
  }
}
