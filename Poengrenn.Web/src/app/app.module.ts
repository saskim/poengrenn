import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AppRoutingModule } from './app-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MomentModule } from 'angular2-moment';

import { AuthGuard } from './_guards/auth-guard';

import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { NotAuthorizedComponent } from './not-authorized/not-authorized.component';

import { HeaderComponent } from './_shared/components/header/header.component';
import { CompetitionComponent } from './competition/competition.component';
import { CompetitionDetailsComponent } from './competition/competition-details/competition-details.component';
import { CompetitionCreateComponent } from './competition/competition-create/competition-create.component';
import { DateSelectorComponent } from './_shared/components/date-selector/date-selector.component';
import { CompetitionClassesComponent } from './competition/competition-classes/competition-classes.component';
import { EditCompetitionClassModalComponent } from './competition/competition-classes/edit-competition-class-modal/edit-competition-class-modal.component';
import { EditCompetitionParticipantModalComponent } from './competition/competition-details/edit-competition-participant-modal/edit-competition-participant-modal.component';
import { AddCompetitionParticipantModalComponent } from './competition/competition-details/add-competition-participant-modal/add-competition-participant-modal.component';
import { RegisterCompetitionResultsModalComponent } from './competition/competition-details/register-competition-results-modal/register-competition-results-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    NotFoundComponent,
    NotAuthorizedComponent,
    HeaderComponent,
    CompetitionComponent,
    CompetitionDetailsComponent,
    CompetitionCreateComponent,
    DateSelectorComponent,
    CompetitionClassesComponent,
    EditCompetitionClassModalComponent,
    EditCompetitionParticipantModalComponent,
    AddCompetitionParticipantModalComponent,
    RegisterCompetitionResultsModalComponent
  ],
  entryComponents: [
    EditCompetitionClassModalComponent,
    EditCompetitionParticipantModalComponent,
    AddCompetitionParticipantModalComponent,
    RegisterCompetitionResultsModalComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule,
    NgbModule.forRoot(),
    MomentModule
  ],
  providers: [
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
