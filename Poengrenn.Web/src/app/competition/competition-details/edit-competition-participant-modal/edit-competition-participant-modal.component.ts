import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { CompetitionService } from 'app/competition/competition.service';
import { KonkurranseDeltaker, KonkurranseKlasse, Person } from 'app/_models/models';
import { TimeViewModel } from '../../models';
declare var moment: any;

@Component({
  selector: 'app-edit-competition-participant-modal',
  templateUrl: './edit-competition-participant-modal.component.html',
  styleUrls: ['./edit-competition-participant-modal.component.scss'],
  providers: [ApiService, CompetitionService]
})

export class EditCompetitionParticipantModalComponent implements OnInit {

  @Input() participant: KonkurranseDeltaker;
  @Input() matchingCompetitionClasses: KonkurranseKlasse[];

  startTime: TimeViewModel;
  endTime: TimeViewModel;
  totalTime: TimeViewModel;

  constructor(
    public _activeModal: NgbActiveModal,
    private _apiService: ApiService,
    private _compService: CompetitionService) { }

  ngOnInit() {
    this.setStartAndEndTime(this.participant);
  }

  updateCompetitionParticipant() {
    this.participant.startTid = this._compService.toTimeWithLeadingZeros(this.startTime);
    this.participant.sluttTid = this._compService.toTimeWithLeadingZeros(this.endTime);
    this.participant.tidsforbruk = this._compService.toTimeWithLeadingZeros(this.totalTime);
    this._apiService.UpdateCompetitionParticipant(this.participant)
      .subscribe((result: KonkurranseDeltaker) => {
        console.log(result);
        this._activeModal.close({updated: result, old: this.participant});
      });
  }

  deleteCompetitionParticipant() {
    if(confirm("Er du sikker på at du vil slette deltaker?")) {   
      this._apiService.DeleteCompetitionParticipant(this.participant)
        .subscribe(result => {
          console.log(result);
          this._activeModal.close({deleted: result});
        });
    }
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
}
