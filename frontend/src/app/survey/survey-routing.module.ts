import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SurveyFinishedComponent } from './components/survey-finished/survey-finished.component';
import { SurveyListComponent } from './components/survey-list/survey-list.component';
import { SurveyComponent } from './components/survey/survey.component';

const routes: Routes = [{
  path: '',
  component: SurveyListComponent
},
{
  path: 'survey/:id',
  component: SurveyComponent
},
{
  path: 'finished',
  component: SurveyFinishedComponent
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class SurveyRoutingModule { }
