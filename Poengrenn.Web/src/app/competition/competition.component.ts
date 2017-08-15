import { Component, OnInit } from '@angular/core';

import { ApiService } from 'app/_services/api.service';
import { AuthService } from 'app/_services/auth.service';

import { Konkurranse } from 'app/_models/models';

@Component({
  selector: 'app-competition',
  templateUrl: './competition.component.html',
  styleUrls: ['./competition.component.scss'],
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

  onCompetitionCreated() {
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
