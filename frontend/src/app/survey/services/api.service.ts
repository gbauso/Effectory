import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import SimpleQuestionnaire from '../model/simple-questionnaire';
import { environment } from 'src/environments/environment';
import Questionnaire from '../model/questionnaire';
import AnswerQuestionRequest from './request/answer-question-request';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private http: HttpClient) {}

  public getSurveyList(): Observable<SimpleQuestionnaire[]> {
    return this.http.get<SimpleQuestionnaire[]>(
      `${environment.baseUrl}/questionnaire`
    );
  }

  public getSurvey(id: string): Observable<Questionnaire> {
    return this.http.get<Questionnaire>(
      `${environment.baseUrl}/questionnaire/${id}`
    );
  }

  public answerQuestion(request: AnswerQuestionRequest) {
    return this.http.post(
      `${environment.baseUrl}/questionnaire`
    , request);
  }
}
