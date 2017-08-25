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
  @Input() existingPersons: Person[];

  personModel: Person;

  genders = GENDERS.slice(0, GENDERS.length); 
  duplicates: Person[];

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
      // let doCancel = true;
      // this.duplicates = this.findDuplicates();
      // if (this.duplicates.length > 0)
      //   doCancel = confirm(`Fant person(er) med samme navn i databasen. Vil du fortsatt opprette ${this.personModel.fornavn} ${this.personModel.etternavn}`)
      
      // if (!doCancel) {
      this._apiService.AddPerson(this.personModel)
        .subscribe((result: Person) => {
          if (result)
            this._activeModal.close(result);
        });
      //}
    }
    
  }

  findDuplicates():void {
    console.log(this.personModel);
    if (this.personModel.fornavn && this.personModel.etternavn) {
      console.log(this.personModel);
      this.duplicates = this.existingPersons
                            .filter(e => e.fornavn.toLowerCase().startsWith(this.personModel.fornavn.toLowerCase()) &&
                                         e.etternavn.toLowerCase().startsWith(this.personModel.etternavn.toLowerCase()));
    }
  }
}
