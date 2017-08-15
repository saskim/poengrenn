import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { GENDERS } from 'app/_shared/constants/constants';

import { ApiService } from 'app/_services/api.service';
import { KonkurranseDeltaker, Konkurranse, Person } from 'app/_models/models';

@Component({
  selector: 'app-create-person-modal-modal',
  templateUrl: './person-modal.component.html',
  styleUrls: ['./person-modal.component.scss'],
  providers: [ApiService]
})
export class PersonModalComponent implements OnInit {

  @Input() editPerson: Person;

  personModel: Person;

  genders = GENDERS.slice(0, GENDERS.length); 

  constructor(
    public _activeModal: NgbActiveModal,
    private _apiService: ApiService) { }

  ngOnInit() {
    this.personModel = new Person();
    if (this.editPerson)
      this.personModel = this.editPerson;
    console.log(this.genders);
    this.genders.splice(this.genders.findIndex(g => g.id.toLowerCase() == "mix"), 1);
  }

  saveCompetitionParticipant() {
    if (this.editPerson) {
      this._apiService.UpdatePerson(this.personModel)
        .subscribe((result: Person) => {
          if (result)
            this._activeModal.close(result);
        });
    }
    else {
      this._apiService.AddPerson(this.personModel)
        .subscribe((result: Person) => {
          if (result)
            this._activeModal.close(result);
        });
    }
    
  }
}
