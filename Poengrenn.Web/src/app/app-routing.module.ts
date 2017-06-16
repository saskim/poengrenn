import { NgModule } from '@angular/core';
import { Routes, RouterModule, CanActivate } from '@angular/router';

import { DashboardComponent } from 'app/dashboard/dashboard.component';
import { NotAuthorizedComponent } from 'app/not-authorized/not-authorized.component';
import { NotFoundComponent } from 'app/not-found/not-found.component';
import { CompetitionComponent } from './competition/competition.component';
import { CompetitionDetailsComponent } from './competition/competition-details/competition-details.component';
import { CompetitionClassesComponent } from './competition/competition-classes/competition-classes.component';

import { AuthGuard } from './_guards/auth-guard';

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    children: [
      { 
        path: '', 
        component: DashboardComponent
      },
      {
        path: 'konkurranser',
        component: CompetitionComponent
      },
      {
        path: 'konkurranse/:id',
        component: CompetitionDetailsComponent
      },
      {
        path: 'konkurranseklasser',
        component: CompetitionClassesComponent
      }
    ]
  },
  {
    path: 'unauthorized',
    component: NotAuthorizedComponent
  },
  { 
    path: "**",
    component: NotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }
