using Effectory.Core.Events;
using Effectory.Shared.Domain;
using Effectory.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Effectory.Core.Model
{
    public class Questionnaire : AggregateRootBase
    {
        private Questionnaire()
        {

        }

        public int QuestionnaireId { get; private set; }
        public IDictionary<string, string> Texts { get; private set; }
        public ICollection<Subject> Subjects { get; private set; }

        public override bool IsValid()
        {
            return QuestionnaireId > 0 && Subjects.Any() && Texts.Any();
        }

        public void AnswerQuestion(int subjectId , int questionId, int? answerId, string answer, Guid executionId)
        {
            if (string.IsNullOrEmpty(answer) && !answerId.HasValue || executionId == Guid.Empty)
                throw new ArgumentException();

            if (!Subjects.Any(i => i.SubjectId == subjectId))
                throw new DomainException();

            var subject = Subjects.FirstOrDefault(i => i.SubjectId == subjectId);
            var question = subject.Questions.FirstOrDefault(i => i.QuestionId == questionId);

            if (question == null) 
                throw new DomainException();

            if(answerId.HasValue && !question.Answers.Any(i => i.AnswerId == answerId.Value))
                throw new DomainException();

            RaiseEvent(new QuestionAnsweredEvent
            {
                Subject = subject.Texts,
                Answers = question.Answers.OrderBy(i => i.OrderNumber).Select(i => i.Texts),
                Question = question.Texts,
                Answer = answer,
                AnswerIndex = answerId.HasValue ? 
                    question.Answers.FirstOrDefault(i => i.AnswerId == answerId.Value).OrderNumber 
                    : null,
                QuestionnareId = QuestionnaireId,
                ExecutionId = executionId,
            });
        }
    }
}
