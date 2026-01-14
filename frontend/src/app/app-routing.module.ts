import { NgModule } from '@angular/core';
import { Routes, RouterModule, CanActivate } from '@angular/router';

import { UserLoginComponent } from 'app/auth/login/user-login.component';
import { AdminLoginComponent } from 'app/auth/login/admin-login.component';
import { LogoutComponent } from 'app/auth/logout/logout.component';
import { NotAuthorizedComponent } from 'app/not-authorized/not-authorized.component';
import { NotFoundComponent } from 'app/not-found/not-found.component';
import { CompetitionComponent } from './competition/competition.component';
import { CompetitionDetailsComponent } from './competition/competition-details/competition-details.component';
import { CompetitionClassesComponent } from './competition/competition-classes/competition-classes.component';

import { AdminGuard } from './_guards/auth-guard';
import { AuthService } from 'app/_services/auth.service';

const routes: Routes = [
  {
    path: '',
    children: [
      { 
        path: '', 
        redirectTo: 'konkurranser',
        pathMatch: 'full'
        //component: DashboardComponent
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
        component: CompetitionClassesComponent,
        canActivate: [AdminGuard],
      }
    ]
  },
  {
    path: 'login',
    component: UserLoginComponent
  },
  {
    path: 'adminlogin',
    component: AdminLoginComponent
  },
  {
    path: 'logout',
    component: LogoutComponent
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
  providers: [AuthService]
})
export class AppRoutingModule { }
