export default interface AnswerQuestionRequest {
  questionnaireId: number;
  subjectId: number;
  questionId: number;
  executionId: string;
  answerId?: number;
  answer?: string;
}
