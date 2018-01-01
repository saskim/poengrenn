import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal, NgbModule, NgbDateParserFormatter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';
import { KonkurranseType, KonkurranseOpprett, KonkurranseKlasse } from 'app/_models/models';

@Component({
  selector: 'app-competition-create',
  templateUrl: './competition-create.component.html',
  styleUrls: ['./competition-create.component.scss'],
  providers: [ApiService]
})
export class CompetitionCreateComponent implements OnInit {

  @Output() onCompetitionCreated = new EventEmitter();

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
    this.model.datoer.push(new Date());
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
    
    //this.model.datoer = new Array<Date>();
    // this.tempDatoer.forEach(dato => {
    //   let d = new Date(this._parserFormatter.format(dato));
    //   if (d)
    //     this.model.datoer.push(d);
    // });
    this.model.datoer.forEach(d => {
      let timezoneOffset = d.getTimezoneOffset();
      d = new Date(d.valueOf() - (timezoneOffset * 60000));
    });
    console.log(this.model);
    this._apiService.CreateCompetition(this.model)
      .subscribe(() => {
        this.onCompetitionCreated.emit();
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
