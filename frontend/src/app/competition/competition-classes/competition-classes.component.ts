import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbModalOptions  } from '@ng-bootstrap/ng-bootstrap';

import { ApiService } from 'app/_services/api.service';

import { KonkurranseKlasse, KonkurranseType } from 'app/_models/models';
import { GENDERS } from 'app/_shared/constants/constants';

import { EditCompetitionClassModalComponent } from './edit-competition-class-modal/edit-competition-class-modal.component';

@Component({
  selector: 'app-competition-classes',
  templateUrl: './competition-classes.component.html',
  styleUrls: ['./competition-classes.component.scss'],
  providers: [ApiService]
})
export class CompetitionClassesComponent implements OnInit {

  model: KonkurranseKlasse;
  
  competitionClasses: KonkurranseKlasse[];
  competitionTypes: KonkurranseType[];

  defaultCompetitionType: KonkurranseType;
  genders = GENDERS.slice(0, GENDERS.length); 

  constructor(
    private _apiService: ApiService,
    private _modalService: NgbModal) { }

  ngOnInit() {
    this.model = new KonkurranseKlasse();
    this.defaultCompetitionType = new KonkurranseType();
    this.defaultCompetitionType.typeID = "0";
    this.defaultCompetitionType.navn = "Ikke knytt mot konkurransetype";

    this.genders.unshift({ id: "0", navn: "Ikke valgt kjønn" });

    this.getCompetitionClasses();
    this.getCompetitionTypes();
  }


  getCompetitionClasses() {
    this._apiService.GetCompetitionClasses()
      .subscribe((result: KonkurranseKlasse[]) => {
        this.competitionClasses = result.sort(function(a, b) {
          return (a.forsteStartnummer > b.forsteStartnummer) ? 1 : ((a.forsteStartnummer < b.forsteStartnummer) ? -1 : 0);
        });
      });
  }
  getCompetitionTypes() {
    this._apiService.GetCompetitionTypes()
      .subscribe((result: KonkurranseType[]) => {
        this.competitionTypes = result;
        console.log(this.competitionTypes);

        this.competitionTypes.unshift(this.defaultCompetitionType);
        this.model.typeID = this.defaultCompetitionType.typeID;
        this.model.kjonn = "0";
      });
  }

  createCompetitionClass() {
    console.log(this.model);
    this._apiService.CreateCompetitionClass(this.model)
      .subscribe(() => {
        this.getCompetitionClasses();
      });
  }

  openEditCompetitionClassModal(compClass : KonkurranseKlasse) {
    let options: NgbModalOptions = { size: "lg" };
    const modalRef = this._modalService.open(EditCompetitionClassModalComponent, options);
    
    modalRef.componentInstance.competitionClass = compClass;
    modalRef.componentInstance.competitionTypes = this.competitionTypes;
    modalRef.componentInstance.genders = this.genders;

    modalRef.result.then((result) => {
      this.getCompetitionClasses();
    });
  }

  updateClassID() {
    let addSpacer = "";
    this.model.klasseID = "";

    if (this.model.minAlder !== undefined && this.model.maxAlder !== undefined)
    {
      this.model.klasseID += addSpacer + this.model.minAlder + "-" + this.model.maxAlder + "år";
      addSpacer = "-";
    }


    if (this.model.kjonn)
      this.model.klasseID += addSpacer + this.model.kjonn;

    if (this.model.distanseKm)
      this.model.klasseID += addSpacer + Math.floor(this.model.distanseKm) + "km";
  }

  filterByTypeID(competitionClasses) {
    if (!this.model.typeID || !competitionClasses)
      return competitionClasses;
    else
      return competitionClasses.filter(c => c.typeID == this.model.typeID);
  }
}
