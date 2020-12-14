import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { ActivatedRoute, Router } from '@angular/router';
import Questionnaire, { Question } from '../../model/questionnaire';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.scss'],
})
export class SurveyComponent implements OnInit {
  survey!: Questionnaire;
  currentQuestion!: Question;
  questionIndex: number = 0;
  executionId: Guid;

  constructor(
    private apiService: ApiService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    this.executionId = Guid.create();
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.apiService.getSurvey(id!).subscribe((value) => {
      this.survey = value;
      this.currentQuestion = value.questions[this.questionIndex];
    });
  }

  answerQuestion = (answerId?: number, answer?: string): void => {
    this.apiService
      .answerQuestion({
        questionId: this.currentQuestion.id,
        subjectId: this.currentQuestion.subject.id,
        questionnaireId: this.survey.id,
        answer: answer,
        answerId: answerId,
        executionId: this.executionId.toString(),
      })
      .subscribe(() => {
        if (this.survey.questions.length === this.questionIndex + 1) {
          this.router.navigate(['finished']);
        }
        this.questionIndex++;
        this.currentQuestion = this.survey.questions[this.questionIndex];
      });
  };
}
