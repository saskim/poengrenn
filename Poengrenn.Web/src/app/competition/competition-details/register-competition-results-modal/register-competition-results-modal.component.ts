import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { KonkurranseDeltaker, KonkurranseKlasse, Person } from 'app/_models/models';

@Component({
  selector: 'app-register-competition-results-modal',
  templateUrl: './register-competition-results-modal.component.html',
  styleUrls: ['./register-competition-results-modal.component.scss'],
  providers: [ApiService]
})
export class RegisterCompetitionResultsModalComponent implements OnInit {

  @Input() participants: KonkurranseDeltaker[];

  currentParticipant: KonkurranseDeltaker;

  constructor(
    public _activeModal: NgbActiveModal,
    private _apiService: ApiService) { }

  ngOnInit() {
  }

}
