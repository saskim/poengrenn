import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { KonkurranseDeltaker, KonkurranseKlasse, Person } from 'app/_models/models';
import { ITimeViewModel, TimeViewModel } from '../../models';
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

  startTime: ITimeViewModel;
  endTime: ITimeViewModel;
  totalTime: ITimeViewModel;
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
    this.participant.startTid = this.startTime.toStringWithLeadingZero();
    this.participant.sluttTid = this.endTime.toStringWithLeadingZero();
    this.participant.tidsforbruk = this.totalTime.toStringWithLeadingZero();
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
    this.totalTime = new TimeViewModel(0, 0, 0);
    
    if (this.startTime && this.endTime) {
      let startDuration = moment.duration({seconds: this.startTime.duration.second, minutes: this.startTime.duration.minute, hours: this.startTime.duration.hour});
      let endDuration = moment.duration({seconds: this.endTime.duration.second, minutes: this.endTime.duration.minute, hours: this.endTime.duration.hour});
      const diff = endDuration.subtract(startDuration);

      this.totalTime = new TimeViewModel(diff.hours(), diff.minutes(), diff.seconds());
    }
  }

  private setStartAndEndTime(participant: KonkurranseDeltaker) {
    this.startTime.duration = {
      hour: +participant.startTid.slice(0, 2),
      minute: +participant.startTid.slice(3, 5),
      second: +participant.startTid.slice(6, 8)
    }
    this.endTime.duration = {
      hour: +participant.sluttTid.slice(0, 2),
      minute: +participant.sluttTid.slice(3, 5),
      second: +participant.sluttTid.slice(6, 8)
    }

    this.updateTidsforbruk();
  }
}
