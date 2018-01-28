declare var moment: any;

export interface IDurationViewModel {
    duration: IDuration;

    add: (hours: number, minutes: number, seconds: number) => void;
    equals: (durationViewModel: DurationViewModel) => boolean;
    toStringWithLeadingZero: () => string;
    setDurationFromString: (hhmmss: string) => void;
}
export interface IDuration {
    hour?: number;
    minute?: number;
    second?: number;
}

export class DurationViewModel implements IDurationViewModel {
    duration: IDuration;

    constructor(hours: number = 0, minutes: number = 0, seconds: number = 0) {
        const duration = moment.duration({
            hours: hours,
            minutes: minutes,
            seconds: seconds
        });

        this.setFromMomentDuration(duration);
    }

    add(hours: number, minutes: number, seconds: number) {
        let duration = moment.duration({
            seconds: this.duration.second,
            minutes: this.duration.minute,
            hours: this.duration.hour
        });
        duration.add(hours, "hours")
                .add(minutes, "minutes")
                .add(seconds, "seconds");

        this.setFromMomentDuration(duration);
    }

    equals(durationViewModel: DurationViewModel): boolean {
        return this.duration.hour === durationViewModel.duration.hour && 
               this.duration.minute === durationViewModel.duration.minute && 
               this.duration.second === durationViewModel.duration.second;
    }

    toStringWithLeadingZero() : string {
        let hh = this.ensureLeadingZero(this.duration.hour);
        let mm = this.ensureLeadingZero(this.duration.minute);
        let ss = this.ensureLeadingZero(this.duration.second);
        return `${hh}:${mm}:${ss}`;
    }
    
    setDurationFromString(hhmmss: string): void {
        this.duration.hour = +hhmmss.slice(0, 2);
        this.duration.minute = +hhmmss.slice(3, 5);
        this.duration.second = +hhmmss.slice(6, 8);
    }

    private ensureLeadingZero = (x: number): string => {
        return x < 10 ? `0${x}` : `${x}`;
    }

    private setFromMomentDuration = (momentDuration: any) => {
        this.duration = {
            hour: momentDuration.hours(),
            minute: momentDuration.minutes(),
            second: momentDuration.seconds()
        }
    }
}