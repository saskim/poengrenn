import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { KonkurranseDeltaker, KonkurranseKlasse, Person } from 'app/_models/models';

@Component({
  selector: 'app-edit-competition-participant-modal',
  templateUrl: './edit-competition-participant-modal.component.html',
  styleUrls: ['./edit-competition-participant-modal.component.scss'],
  providers: [ApiService]
})
export class EditCompetitionParticipantModalComponent implements OnInit {


  @Input() participant: KonkurranseDeltaker;
  @Input() matchingCompetitionClasses: KonkurranseKlasse[];

  constructor(
    public _activeModal: NgbActiveModal,
    private _apiService: ApiService) { }

  ngOnInit() {
    
  }

  updateCompetitionParticipant() {
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
}
