import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LotteryModalComponent } from './lottery-modal.component';

describe('LotteryModalComponent', () => {
  let component: LotteryModalComponent;
  let fixture: ComponentFixture<LotteryModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LotteryModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LotteryModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
