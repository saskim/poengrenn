import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { KonkurranseDeltaker, Konkurranse, Person } from 'app/_models/models';

@Component({
  selector: 'app-add-competition-participant-modal',
  templateUrl: './add-competition-participant-modal.component.html',
  styleUrls: ['./add-competition-participant-modal.component.scss'],
  providers: [ApiService]
})
export class AddCompetitionParticipantModalComponent implements OnInit {

  personModel: Person;

  constructor(
    public _activeModal: NgbActiveModal,
    private _apiService: ApiService) { }

  ngOnInit() {
  }

  START HER!!
}
