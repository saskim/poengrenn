import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbModule, NgbDateParserFormatter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-date-selector',
  templateUrl: './date-selector.component.html',
  styleUrls: ['./date-selector.component.scss']
})
export class DateSelectorComponent implements OnInit {

  @Input() index: number;
  @Input() date: Date;
  @Input() minDate: NgbDateStruct;
  @Input() maxDate: NgbDateStruct;
  @Input() hideTime: boolean = false;
  @Output() onMonthChange = new EventEmitter<NgbDateStruct>();
  @Output() onDelete = new EventEmitter<number>();
  @Output() onDateChange = new EventEmitter<Date>();
  
  dateTemp: NgbDateStruct;
  timeTemp = { hour: 17, minute: 0 };

  timeSize = "small";

  constructor(private _parserFormatter: NgbDateParserFormatter) { 
    
  }

  ngOnInit() {
    console.log(this.date);

    if (this.date) {
      this.dateTemp = { year: this.date.getFullYear(), month: this.date.getMonth() + 1, day: this.date.getDate() };
      this.timeTemp = { hour: this.date.getHours(), minute: this.date.getMinutes() };
    }
    
  }

  monthChanged(newDate) {
    this.onMonthChange.emit(newDate);
  }
  onDateDelete() {
    console.log(this.index);
    this.onDelete.emit(this.index);
  }

  modelChange() {
    console.log("Model changed " + this.index);
    console.log(this.dateTemp);
    console.log(this.timeTemp);
    var emitDate = new Date(this.dateTemp.year, this.dateTemp.month - 1, this.dateTemp.day, this.timeTemp.hour, this.timeTemp.minute);
    this.onDateChange.emit(emitDate);
  }
}
