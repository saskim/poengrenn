import { Injectable } from '@angular/core';
import { TimeViewModel } from './models';

@Injectable()
export class CompetitionService {
    toTimeWithLeadingZeros(time: TimeViewModel): string {
        let hh = (time.hour < 10) ? `0${time.hour}` : time.hour;
        let mm = (time.minute < 10) ? `0${time.minute}` : time.minute;
        let ss = (time.second < 10) ? `0${time.second}` : time.second;
        return `${hh}:${mm}:${ss}`;
      }
}