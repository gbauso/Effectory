export default interface Questionnaire {
    id: number;
    title: string;
    questions: Question[];
}

export interface Subject {
    id: number;
    subject: string;
}

export interface Question {
    id: number;
    question: string;
    subject: Subject;
    answerType: AnswerType;
    answers: Answer[];
}

export interface Answer {
    id: number;
    text: string;
}

export enum AnswerType {
    MultipleChoice = 0,
    TextArea = 2
}
