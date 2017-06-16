import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCompetitionParticipantModalComponent } from './edit-competition-participant-model.component';

describe('EditCompetitionParticipantModalComponent', () => {
  let component: EditCompetitionParticipantModalComponent;
  let fixture: ComponentFixture<EditCompetitionParticipantModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditCompetitionParticipantModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditCompetitionParticipantModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
