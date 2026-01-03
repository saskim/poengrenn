import { Routes } from '@angular/router';

import { AdminLoginComponent } from 'app/auth/login/admin-login.component';
import { UserLoginComponent } from 'app/auth/login/user-login.component';
import { LogoutComponent } from 'app/auth/logout/logout.component';
import { CompetitionComponent } from './competition/competition.component';
import { CompetitionDetailsComponent } from './competition/competition-details/competition-details.component';
import { CompetitionClassesComponent } from './competition/competition-classes/competition-classes.component';
import { NotAuthorizedComponent } from './not-authorized/not-authorized.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { AdminGuard } from './_guards/auth-guard';

export const appRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        redirectTo: 'konkurranser',
        pathMatch: 'full'
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
        canActivate: [AdminGuard]
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
    path: '**',
    component: NotFoundComponent
  }
];
