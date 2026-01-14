import { CommonModule } from '@angular/common';
import { Component, OnInit, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal, NgbDropdownModule, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Konkurranse } from 'app/_models/models';
import { COMP_STATUSES } from 'app/_shared/constants/constants';

import { ApiService } from 'app/_services/api.service';
import { DateSelectorComponent } from 'app/_shared/components/date-selector/date-selector.component';

@Component({
  selector: 'app-edit-competition-modal',
  standalone: true,
  templateUrl: './edit-competition-modal.component.html',
  styleUrls: ['./edit-competition-modal.component.scss'],
  imports: [CommonModule, FormsModule, DateSelectorComponent, NgbDropdownModule],
  providers: [ApiService]
})
export class EditCompetitionModalComponent implements OnInit {

  @Input() competition: Konkurranse;

  minDate = {year: new Date().getFullYear(), month: 1, day: 1};
  competitionStatuses = COMP_STATUSES;
  tempStatus: string;
  tempDate: Date;

  constructor(
    public _activeModal: NgbActiveModal,
    private _apiService: ApiService) { }

  ngOnInit() {
    this.tempStatus = this.competition.status;
    this.tempDate = new Date(this.competition.dato);
  }

  updateCompetition() {
    const payload: Partial<Konkurranse> = {
      konkurranseID: this.competition.konkurranseID,
      typeID: this.competition.typeID,
      serie: this.competition.serie,
      navn: this.competition.navn,
      dato: this.competition.dato,
      status: this.tempStatus,
      startInterval: this.competition.startInterval
    };

    this._apiService.UpdateCompetition(payload as Konkurranse)
      .subscribe((result: Konkurranse) => {
        this._activeModal.close("Updated");
      });
  }

  setDate(index, dato) {
    this.competition.dato = dato;
  }

  getSelectedStatusName(): string {
    if (!this.tempStatus || !this.competitionStatuses) {
      return 'Velg status';
    }
    const match = this.competitionStatuses.find(status => status.id == this.tempStatus);
    return match?.displayText || 'Velg status';
  }

  selectStatus(statusId: string) {
    this.tempStatus = statusId;
  }
}
