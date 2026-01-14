import { CommonModule } from '@angular/common';
import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Konkurranse, KonkurranseDeltaker } from 'app/_models/models';


@Component({
  selector: 'app-lottery-modal',
  standalone: true,
  templateUrl: './lottery-modal.component.html',
  styleUrls: ['./lottery-modal.component.scss'],
  imports: [CommonModule]
})
export class LotteryModalComponent implements OnInit {

  participants: KonkurranseDeltaker[];
  currentWinner: KonkurranseDeltaker;
  winners: KonkurranseDeltaker[];

  @Input() filteredParticpants: KonkurranseDeltaker[];

  constructor(public _activeModal: NgbActiveModal) {

  }

  ngOnInit() {
    this.participants = this.filteredParticpants
                            .filter(participant => participant.tilstede)
                            .slice(0);
    this.winners = [];
  }

  roll_winner() {
    if (this.participants.length > 0) {
      const index = this.roll();
      this.currentWinner = this.participants.splice(index, 1)[0];
      this.winners.push(this.currentWinner);
    } else {
      console.error('No participants in lottery');
    }
  }

  roll() {
    return Math.floor(Math.random() * this.participants.length);
  }

}
