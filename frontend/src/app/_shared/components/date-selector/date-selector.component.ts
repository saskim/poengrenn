import { CommonModule } from '@angular/common';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbDateParserFormatter, NgbDateStruct, NgbDatepickerModule, NgbTimepickerModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-date-selector',
  standalone: true,
  templateUrl: './date-selector.component.html',
  styleUrls: ['./date-selector.component.scss'],
  imports: [CommonModule, FormsModule, NgbDatepickerModule, NgbTimepickerModule]
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
  meridian;

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
    this.dateTemp = null;
    this.timeTemp = { hour: 17, minute: 0 };
    this.onDelete.emit(this.index);
    this.onDateChange.next(null);
  }

  modelChange() {
    if (!this.dateTemp) {
      this.onDateChange.next(null);
      return;
    }

    var emitDate = new Date(this.dateTemp.year, this.dateTemp.month - 1, this.dateTemp.day, this.timeTemp.hour, this.timeTemp.minute);
    this.onDateChange.next(emitDate);
  }
}
