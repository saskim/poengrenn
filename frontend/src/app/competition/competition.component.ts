import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';

import { ApiService } from 'app/_services/api.service';
import { AuthService } from 'app/_services/auth.service';

import { Konkurranse } from 'app/_models/models';
import { CompetitionCreateComponent } from './competition-create/competition-create.component';

@Component({
  selector: 'app-competition',
  standalone: true,
  templateUrl: './competition.component.html',
  styleUrls: ['./competition.component.scss'],
  imports: [CommonModule, RouterLink, CompetitionCreateComponent],
  providers: [ ]
})
export class CompetitionComponent implements OnInit {

  //competitions: Konkurranse[];
  openCompetitions: Konkurranse[];
  doneCompetitions: Konkurranse[];

  constructor(
    private _apiService: ApiService,
    private _authService: AuthService) { }

  ngOnInit() {
    this.getOpenCompetitions();
    this.getDoneCompetitions();
  }

  onCompetitionCreated(event) {
    console.log("onCompetitionCreated");
    this.getOpenCompetitions();
  }

  isAdmin() {
    return this._authService.isAdmin();
  }

  getOpenCompetitions() {
    this._apiService.GetOpenCompetitions()
      .subscribe((result: Konkurranse[]) => {
        this.openCompetitions = result
      });
  }

  getDoneCompetitions() {
    this._apiService.GetDoneCompetitions()
      .subscribe((result: Konkurranse[]) => {
        this.doneCompetitions = result
      });
  }

  

  // getCompetitions() {
  //   this._apiService.GetAllCompetitions()
  //     .subscribe((result:Konkurranse[]) => {
  //       console.log(result);
  //       this.competitions = result;
  //     });
  // }
}
