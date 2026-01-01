import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCompetitionModalComponent } from './edit-competition-modal.component';

describe('EditCompetitionModalComponent', () => {
  let component: EditCompetitionModalComponent;
  let fixture: ComponentFixture<EditCompetitionModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditCompetitionModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditCompetitionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
