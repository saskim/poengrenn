import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { KonkurranseDeltaker, KonkurranseKlasse, Person } from 'app/_models/models';
import { IDurationViewModel, DurationViewModel } from '../../models';
declare var moment: any;

@Component({
  selector: 'app-edit-competition-participant-modal',
  templateUrl: './edit-competition-participant-modal.component.html',
  styleUrls: ['./edit-competition-participant-modal.component.scss'],
  providers: [ApiService]
})

export class EditCompetitionParticipantModalComponent implements OnInit {

  @Input() participant: KonkurranseDeltaker;
  @Input() matchingCompetitionClasses: KonkurranseKlasse[];

  startTime: IDurationViewModel;
  endTime: IDurationViewModel;
  totalTime: IDurationViewModel;
  medTidtaking: boolean;

  constructor(
    public _activeModal: NgbActiveModal,
    private _apiService: ApiService) { }

  ngOnInit() {
    let compClass = this.matchingCompetitionClasses.find(c => c.klasseID == this.participant.klasseID);
    this.medTidtaking = compClass.medTidtaking;
    this.setStartAndEndTime(this.participant);
  }

  updateCompetitionParticipant() {    
    if (this.totalTime && this.totalTime.toStringWithLeadingZero() === "00:00:00") {
      this.participant.startTid = null;
      this.participant.sluttTid = null;
      this.participant.tidsforbruk = null;
    }
    else {
      if (this.startTime)
        this.participant.startTid = this.startTime.toStringWithLeadingZero();
      if (this.endTime)
        this.participant.sluttTid = this.endTime.toStringWithLeadingZero();
      if (this.totalTime)
        this.participant.tidsforbruk = this.totalTime.toStringWithLeadingZero();
    }

    this._apiService.UpdateCompetitionParticipant(this.participant)
      .subscribe((result: KonkurranseDeltaker) => {
        console.log(result);
        this._activeModal.close({updated: result, old: this.participant});
      });
  }

  unregisterCompetitionParticipant() {
    if(confirm("Er du sikker på at du vil melde av deltaker?")) {   
      this._apiService.DeleteCompetitionParticipant(this.participant)
        .subscribe(result => {
          console.log(result);
          this._activeModal.close({deleted: result});
        });
    }
  }

  updateTidsforbruk() {
    this.totalTime = new DurationViewModel(0, 0, 0);
    
    if (this.startTime && this.endTime) {
      let startDuration = moment.duration({seconds: this.startTime.duration.second, minutes: this.startTime.duration.minute, hours: this.startTime.duration.hour});
      let endDuration = moment.duration({seconds: this.endTime.duration.second, minutes: this.endTime.duration.minute, hours: this.endTime.duration.hour});
      const diff = endDuration.subtract(startDuration);

      this.totalTime = new DurationViewModel(diff.hours(), diff.minutes(), diff.seconds());
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
}
