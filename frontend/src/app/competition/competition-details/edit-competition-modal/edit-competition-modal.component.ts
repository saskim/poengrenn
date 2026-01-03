import { CommonModule } from '@angular/common';
import { Component, OnInit, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Konkurranse } from 'app/_models/models';
import { COMP_STATUSES } from 'app/_shared/constants/constants';

import { ApiService } from 'app/_services/api.service';
import { DateSelectorComponent } from 'app/_shared/components/date-selector/date-selector.component';

@Component({
  selector: 'app-edit-competition-modal',
  standalone: true,
  templateUrl: './edit-competition-modal.component.html',
  styleUrls: ['./edit-competition-modal.component.scss'],
  imports: [CommonModule, FormsModule, DateSelectorComponent],
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
    console.log(this.tempDate);
  }

  updateCompetition() {
    this.competition.status = this.tempStatus;
    
    if (typeof(this.competition.dato) !== "string") {
      let timezoneOffset = this.competition.dato.getTimezoneOffset();
      this.competition.dato = new Date(this.competition.dato.valueOf() - (timezoneOffset * 60000));
    }
    
    this._apiService.UpdateCompetition(this.competition)
      .subscribe((result: Konkurranse) => {
        console.log(result);

        this._activeModal.close("Updated");
      });
  }

  setDate(index, dato) {
    this.competition.dato = dato;
  }
}
