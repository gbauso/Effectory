import { Component, OnInit } from '@angular/core';
import {  Router } from '@angular/router';
import { Observable } from 'rxjs';
import SimpleQuestionnaire from '../../model/simple-questionnaire';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-survey-list',
  templateUrl: './survey-list.component.html',
  styleUrls: ['./survey-list.component.scss']
})
export class SurveyListComponent implements OnInit {

  surveys$: Observable<SimpleQuestionnaire[]> | undefined;

  constructor(private apiService: ApiService, private router: Router) { }

  ngOnInit(): void {
    this.surveys$ = this.apiService.getSurveyList();
  }

  startSurvey(id: number) {
    this.router.navigate([`/survey/${id }`]);
  }

}
