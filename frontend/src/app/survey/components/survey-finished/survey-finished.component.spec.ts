import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurveyFinishedComponent } from './survey-finished.component';

describe('SurveyFinishedComponent', () => {
  let component: SurveyFinishedComponent;
  let fixture: ComponentFixture<SurveyFinishedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SurveyFinishedComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SurveyFinishedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
