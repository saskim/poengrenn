import { CommonModule } from '@angular/common';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal, NgbModule, NgbDateParserFormatter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { KonkurranseType, KonkurranseOpprett, KonkurranseKlasse } from 'app/_models/models';
import { DateSelectorComponent } from 'app/_shared/components/date-selector/date-selector.component';

@Component({
  selector: 'app-competition-create',
  standalone: true,
  templateUrl: './competition-create.component.html',
  styleUrls: ['./competition-create.component.scss'],
  imports: [CommonModule, FormsModule, DateSelectorComponent],
  providers: [ApiService]
})
export class CompetitionCreateComponent implements OnInit {

  @Output() change = new EventEmitter<boolean>();

  model: KonkurranseOpprett;
  tempDatoer: NgbDateStruct[];
  competitionTypes: KonkurranseType[];
  competitionClasses: KonkurranseKlasse[];

  defaultCompetitionType = new KonkurranseType();

  minDate = {year: new Date().getFullYear(), month: 1, day: 1};
  //maxDate = {year: new Date().getFullYear(), month: 12, day: 31};

  constructor(private _apiService: ApiService,
              private _parserFormatter: NgbDateParserFormatter) { }

  ngOnInit() {
    this.defaultCompetitionType.typeID = '-1';
    this.defaultCompetitionType.navn = " Velg type konkurranse ";

    this.model = new KonkurranseOpprett();
    this.model.typeID = this.defaultCompetitionType.typeID;
    //this.tempDatoer = new Array<NgbDateStruct>(1);
    this.model.datoer = new Array<Date>(1);
    
    this.getCompetitionTypes();
  }

  onMonthChange(index: number, newDate: NgbDateStruct) {
    if (index == 0)
      this.setDefaultCompetitionName(this.findCompetitionYear(newDate));
  }


  addTempDato() {
    //let dt = this._parserFormatter.parse(new Date().toDateString())
    //this.tempDatoer.push(dt);
    const last = this.getLastSelectedDate();
    const next = last ? new Date(last) : new Date();
    next.setDate(next.getDate() + 7);
    if (last) {
      next.setHours(last.getHours(), last.getMinutes(), 0, 0);
    }
    this.model.datoer.push(next);
  }

  private getLastSelectedDate(): Date | null {
    if (!this.model?.datoer?.length) {
      return null;
    }

    for (let i = this.model.datoer.length - 1; i >= 0; i--) {
      const value: any = this.model.datoer[i];
      if (!value) {
        continue;
      }

      const date = value instanceof Date ? value : new Date(value);
      if (!isNaN(date.getTime())) {
        return date;
      }
    }

    return null;
  }
  removeTempDato(index) {
    //this.tempDatoer.splice(index, 1);
    this.model.datoer.splice(index, 1);
  }

  setTempDato(index, dato) {
    //this.tempDatoer[index] = dato;
    this.model.datoer[index] = dato;
  }

  setDefaultCompetitionName(compYear: number) {
    if (this.model.typeID != "annet") {
      let compType = this.findCompetitionType(this.model.typeID);

      if (!compYear)
        compYear = new Date().getFullYear();
      this.model.navn = compType.navn + ' ' + compYear;
    } 
  }

  getCompetitionTypes() {
    this._apiService.GetCompetitionTypes()
      .subscribe((result: KonkurranseType[]) => {
        this.competitionTypes = result;

        this.competitionTypes.unshift(this.defaultCompetitionType);
      });
  }
  getCompetitionClasses() {
    this._apiService.GetCompetitionClassesByTypeID(this.model.typeID)
      .subscribe((result: KonkurranseKlasse[]) => {
        this.competitionClasses = result.sort(function(a, b) {
          return a.minAlder - b.minAlder;
        });
      });
  }
  createCompetition() {
    
    this.model.datoer = this.model.datoer.map(d => {
      let timezoneOffset = d.getTimezoneOffset();
      return new Date(d.valueOf() - (timezoneOffset * 60000));
    });
    console.log(this.model);
    this._apiService.CreateCompetition(this.model)
      .subscribe(() => {
        this.change.emit(true);
      });
  }

  private findCompetitionType(typeID: string): KonkurranseType {
    return this.competitionTypes.find(ct => ct.typeID == typeID);
  }
  private findCompetitionYear(date: NgbDateStruct): number {
    if (date) 
      return date.year;
    
    return new Date().getFullYear();
  }
}
