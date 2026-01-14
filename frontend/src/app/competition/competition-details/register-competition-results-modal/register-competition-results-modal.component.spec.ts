import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterCompetitionResultsModalComponent } from './register-competition-results-modal.component';

describe('RegisterCompetitionResultsModalComponent', () => {
  let component: RegisterCompetitionResultsModalComponent;
  let fixture: ComponentFixture<RegisterCompetitionResultsModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisterCompetitionResultsModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterCompetitionResultsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
