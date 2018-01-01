import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { CompetitionService } from 'app/competition/competition.service';
import { Konkurranse, KonkurranseDeltaker, KonkurranseKlasse, Person } from 'app/_models/models';
import { TimeViewModel } from '../../models';
declare var moment: any;

@Component({
  selector: 'app-register-competition-results-modal',
  templateUrl: './register-competition-results-modal.component.html',
  styleUrls: ['./register-competition-results-modal.component.scss'],
  providers: [ApiService, CompetitionService]
})
export class RegisterCompetitionResultsModalComponent implements OnInit {

  @Input() competition: Konkurranse; // including participants and competition classes

  filteredParticipants: KonkurranseDeltaker[];
  currentParticipant: KonkurranseDeltaker;
  //selectedCompetitionClass: KonkurranseKlasse;
  defaultStartDiffInSeconds: number;

  participantIdx = 0;

  findParticipantMessage = "";
  disableSave = false;

  startTime: TimeViewModel;
  endTime: TimeViewModel;
  totalTime: TimeViewModel;
  // startTime = {
  //   hour: 0,
  //   minute: 0,
  //   second: 0
  // }
  // endTime = {
  //   hour: 0,
  //   minute: 0,
  //   second: 0
  // }

  constructor(
    public _activeModal: NgbActiveModal,
    private _apiService: ApiService,
    private _compService: CompetitionService) {

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
    if (this.startTime && this.endTime) {
      let startDuration = moment.duration({seconds: this.startTime.second, minutes: this.startTime.minute, hours: this.startTime.hour});
      let endDuration = moment.duration({seconds: this.endTime.second, minutes: this.endTime.minute, hours: this.endTime.hour});
      const diff = endDuration.subtract(startDuration);

      this.totalTime = {
        hour: diff.hours(),
        minute: diff.minutes(),
        second: diff.seconds()
      }
    }
    else {
      this.totalTime = {
        hour: 0,
        minute: 0,
        second: 0
      }
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
    
    this.currentParticipant.startTid = this._compService.toTimeWithLeadingZeros(this.startTime);
    this.currentParticipant.sluttTid = this._compService.toTimeWithLeadingZeros(this.endTime);
    this.currentParticipant.tidsforbruk = this._compService.toTimeWithLeadingZeros(this.totalTime);
    console.log(this.currentParticipant);
      // this._apiService.UpdateCompetitionParticipant(this.participant)
      //   .subscribe((result: KonkurranseDeltaker) => {
      //     console.log(result);
      //     this._activeModal.close({updated: result, old: this.participant});
      //   });
    
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

    this.setStartAndEndTime(this.currentParticipant);
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
    }
  }

  private setStartAndEndTime(participant: KonkurranseDeltaker) {
    this.startTime = {
      hour: +participant.startTid.slice(0, 2),
      minute: +participant.startTid.slice(3, 5),
      second: +participant.startTid.slice(6, 8)
    }
    this.endTime = {
      hour: +participant.sluttTid.slice(0, 2),
      minute: +participant.sluttTid.slice(3, 5),
      second: +participant.sluttTid.slice(6, 8)
    }

    this.updateTidsforbruk();
  }

  private toTimeWithLeadingZeros(time: TimeViewModel): string {
    let hh = (time.hour < 10) ? `0${time.hour}` : time.hour;
    let mm = (time.minute < 10) ? `0${time.minute}` : time.minute;
    let ss = (time.second < 10) ? `0${time.second}` : time.second;
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
