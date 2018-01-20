import { Injectable } from '@angular/core';
import { ITimeViewModel } from './models';

@Injectable()
export class CompetitionService {
    toTimeWithLeadingZeros(time: ITimeViewModel): string {
        if (!time)
            return; 
            
        let hh = (time.duration.hour < 10) ? `0${time.duration.hour}` : time.duration.hour;
        let mm = (time.duration.minute < 10) ? `0${time.duration.minute}` : time.duration.minute;
        let ss = (time.duration.second < 10) ? `0${time.duration.second}` : time.duration.second;
        return `${hh}:${mm}:${ss}`;
      }
}