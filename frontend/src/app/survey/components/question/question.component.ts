import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AnswerType, Question } from '../../model/questionnaire';
import { faArrowAltCircleRight } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.scss'],
})
export class QuestionComponent implements OnInit {
  @Input() question!: Question;
  @Input() answerQuestion!: (answerId?: number, answer?: string) => void;

  answerForm: FormGroup;
  icon = faArrowAltCircleRight;

  constructor(private formBuilder: FormBuilder) {
    this.answerForm = this.formBuilder.group({
      answer: null,
      answerId: null,
    });
  }

  ngOnInit(): void {}

  onSubmit(value: any) {
    this.answerQuestion(value.answerId, value.answer);
    this.answerForm.reset();
  }

  get isTextArea(): boolean {
    return this.question.answerType == AnswerType.TextArea;
  }

  get canSubmit(): boolean {
    return this.isTextArea
      ? this.answerForm.value.answer !== null && this.answerForm.value.answer !== ''
      : this.answerForm.value.answerId !== null;
  }
}
