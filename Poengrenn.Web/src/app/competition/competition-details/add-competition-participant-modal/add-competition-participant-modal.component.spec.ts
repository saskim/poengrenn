import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCompetitionParticipantModalComponent } from './add-competition-participant-modal.component';

describe('AddCompetitionParticipantModalComponent', () => {
  let component: AddCompetitionParticipantModalComponent;
  let fixture: ComponentFixture<AddCompetitionParticipantModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddCompetitionParticipantModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddCompetitionParticipantModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
