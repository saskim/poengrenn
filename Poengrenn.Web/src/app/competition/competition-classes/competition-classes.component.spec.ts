import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompetitionClassesComponent } from './competition-classes.component';

describe('CompetitionClassesComponent', () => {
  let component: CompetitionClassesComponent;
  let fixture: ComponentFixture<CompetitionClassesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompetitionClassesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompetitionClassesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
