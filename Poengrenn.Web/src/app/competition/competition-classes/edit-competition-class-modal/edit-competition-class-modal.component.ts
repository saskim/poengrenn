import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { KonkurranseKlasse, KonkurranseType } from 'app/_models/models';

@Component({
  selector: 'app-edit-competition-class-modal',
  templateUrl: './edit-competition-class-modal.component.html',
  styleUrls: ['./edit-competition-class-modal.component.scss'],
  providers: [ApiService]
})
export class EditCompetitionClassModalComponent implements OnInit {

  @Input() competitionClass: KonkurranseKlasse;
  @Input() competitionTypes: KonkurranseType[];
  @Input() genders: any[];

  constructor(
    public _activeModal: NgbActiveModal,
    private _apiService: ApiService) { }

  ngOnInit() {
    
  }

  saveCompetitionClass() {
    this._apiService.UpdateCompetitionClass(this.competitionClass)
      .subscribe(() => {
        this._activeModal.close("Saved");
      });
  }

  deleteCompetitionClass() {
    this._apiService.DeleteCompetitionClass(this.competitionClass.klasseID)
      .subscribe(() => {
        this._activeModal.close("Deleted");
      });
  }
}
