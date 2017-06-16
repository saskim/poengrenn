import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCompetitionClassModalComponent } from './edit-competition-class-modal.component';

describe('EditCompetitionClassModalComponent', () => {
  let component: EditCompetitionClassModalComponent;
  let fixture: ComponentFixture<EditCompetitionClassModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditCompetitionClassModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditCompetitionClassModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
