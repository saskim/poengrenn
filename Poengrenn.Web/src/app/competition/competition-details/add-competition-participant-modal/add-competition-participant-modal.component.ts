import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { GENDERS } from 'app/_shared/constants/constants';

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

  genders = GENDERS; 

  constructor(
    public _activeModal: NgbActiveModal,
    private _apiService: ApiService) { }

  ngOnInit() {
    this.personModel = new Person();

    this.genders.unshift({ id: "0", navn: "Ikke valgt kjønn" });
  }

  saveCompetitionParticipant() {
    this._apiService.AddPerson(this.personModel)
      .subscribe((result: Person) => {
        if (result)
          this._activeModal.close(result);
      });
  }
}
