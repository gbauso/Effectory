import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SurveyListComponent } from './components/survey-list/survey-list.component';
import { HttpClientModule } from '@angular/common/http';
import { SurveyRoutingModule } from './survey-routing.module';
import { CardComponent } from './components/card/card.component';
import { AppRoutingModule } from '../app-routing.module';
import { SurveyComponent } from './components/survey/survey.component';
import { QuestionComponent } from './components/question/question.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SurveyFinishedComponent } from './components/survey-finished/survey-finished.component';

@NgModule({
  declarations: [SurveyListComponent, CardComponent, SurveyComponent, QuestionComponent, SurveyFinishedComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    SurveyRoutingModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class SurveyModule { }
