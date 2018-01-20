declare var moment: any;

export interface ITimeViewModel {
    duration: {
        hour?: number;
        minute?: number;
        second?: number;
    }
    add?: (hours: number, minutes: number, seconds: number) => void;
    toString: () => string;
}

export class TimeViewModel implements ITimeViewModel {
    duration: {
        hour: number;
        minute: number;
        second: number;
    }

    constructor(hours: number, minutes: number, seconds: number) {
        const duration = moment.duration({
            hours: hours,
            minutes: minutes,
            seconds: seconds
        });

        this.setFromDuration(duration);
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

        this.setFromDuration(duration);
    }

    toString() : string {
        return this.formatDuration(this.duration);
    }

    private setFromDuration(duration: any) {
        this.duration = {
            hour: duration.hours(),
            minute: duration.minutes(),
            second: duration.seconds()
        }
    }

    private formatDuration(duration: any) {
        var ensureZeroPrefixed = (x: number): string => {
            return x < 10 ? `0${x}` : `${x}`;
        }
        
        const h = ensureZeroPrefixed(duration.hour);
        const m = ensureZeroPrefixed(duration.minute);
        const s = ensureZeroPrefixed(duration.second);
        return `${h}:${m}:${s}`;
    }
}