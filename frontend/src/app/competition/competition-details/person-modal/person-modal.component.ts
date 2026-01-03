import { CommonModule } from '@angular/common';
import { Component, OnInit, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { GENDERS } from 'app/_shared/constants/constants';

import { ApiService } from 'app/_services/api.service';
import { AuthService } from 'app/_services/auth.service';
import { KonkurranseDeltaker, Konkurranse, Person } from 'app/_models/models';

@Component({
  selector: 'app-create-person-modal-modal',
  standalone: true,
  templateUrl: './person-modal.component.html',
  styleUrls: ['./person-modal.component.scss'],
  imports: [CommonModule, FormsModule],
  providers: []
})
export class PersonModalComponent implements OnInit {

  @Input() editPerson: Person;
  @Input() existingPersons: Person[];

  personModel: Person;

  genders = GENDERS.slice(0, GENDERS.length); 
  duplicates: Person[];
  errorMessage: string;

  constructor(
    public _activeModal: NgbActiveModal,
    private _apiService: ApiService,
    private _authService: AuthService) { }

  ngOnInit() {
    this.personModel = new Person();
    if (this.editPerson)
      this.personModel = this.editPerson;
    console.log(this.genders);
    this.genders.splice(this.genders.findIndex(g => g.id.toLowerCase() == "mix"), 1);
  }

  private isAdmin() {
    return this._authService.isAdmin();
  }

  hasDuplicates(): boolean {
    if (this.isAdmin())
      return false;

    return (this.duplicates && 
            this.duplicates.findIndex(d => d.fodselsar == this.personModel.fodselsar && d.kjonn == this.personModel.kjonn) > -1);
  }
  hasErrors(): boolean {
    this.errorMessage = "";
    if (this.isAdmin())
      return false;
    
    if (this.personModel.fodselsar && (this.personModel.fodselsar < 1900 || this.personModel.fodselsar > new Date().getFullYear()))
    {
      this.errorMessage += "Vennligst skriv inn et gyldig fødselsår, 4 siffer.";
      return true;
    }
      
    if (!this.personModel.epost && !this.personModel.telefon) {
      this.errorMessage += "E-post og/eller telefon må fylles ut.";
      return true;
    }

    if (this.hasDuplicates()) {
      this.errorMessage += `Det er funnet liknende person(er) med samme fødselsår. <br/>
                            Send en e-post til <a href='mailto:sonja.askim@gmail.com'>sonja.askim@gmail.com</a> hvis du ønsker å opprette ny person med tilsvarende navn.`;
      return true;
    }
    return false;
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
    if (this.personModel.fornavn && this.personModel.etternavn) {
      this.duplicates = this.existingPersons
                            .filter(e => e.fornavn.toLowerCase().startsWith(this.personModel.fornavn.toLowerCase()) &&
                                         e.etternavn.toLowerCase().startsWith(this.personModel.etternavn.toLowerCase()));
    }
  }
}
