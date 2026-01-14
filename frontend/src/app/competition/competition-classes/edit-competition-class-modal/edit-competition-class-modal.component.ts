import { CommonModule } from '@angular/common';
import { Component, OnInit, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal, NgbDropdownModule, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { KonkurranseKlasse, KonkurranseType } from 'app/_models/models';

@Component({
  selector: 'app-edit-competition-class-modal',
  standalone: true,
  templateUrl: './edit-competition-class-modal.component.html',
  styleUrls: ['./edit-competition-class-modal.component.scss'],
  imports: [CommonModule, FormsModule, NgbDropdownModule],
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

  getSelectedTypeName(): string {
    if (!this.competitionClass?.typeID || !this.competitionTypes) {
      return 'Velg konkurransetype';
    }
    const match = this.competitionTypes.find(ct => ct.typeID == this.competitionClass.typeID);
    return match?.navn || 'Velg konkurransetype';
  }

  getSelectedGenderName(): string {
    if (!this.competitionClass?.kjonn || !this.genders) {
      return 'Velg kjønn';
    }
    const match = this.genders.find(g => g.id == this.competitionClass.kjonn);
    return match?.navn || 'Velg kjønn';
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
