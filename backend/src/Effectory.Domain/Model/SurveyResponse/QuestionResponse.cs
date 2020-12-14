using Effectory.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Effectory.Core.Model.SurveyResponse
{
    public class QuestionResponse : IValueObject
    {
        private QuestionResponse()
        {

        }

        public IDictionary<string, string> Subject { get; private set; }
        public IDictionary<string, string> Question { get; private set; }
        public IEnumerable<IDictionary<string, string>> Answers { get; private set; }
        public string Answer { get; private set; }
        public int? AnswerIndex { get; private set; }

        public static QuestionResponse Create(
            IDictionary<string, string> subject,
            IDictionary<string, string> question,
            IEnumerable<IDictionary<string, string>> answers,
            string answer,
            int? answerIndex)
        {
            if (!subject.Any() ||
               !question.Any() ||
               !answers.Any() ||
               (string.IsNullOrEmpty(answer)
               && !answerIndex.HasValue))
            {
                throw new ArgumentException();
            }


            return new QuestionResponse()
            {
                Subject = subject,
                Answer = answer,
                Question = question,
                Answers = answers,
                AnswerIndex = answerIndex
            };
        }
    }
}
