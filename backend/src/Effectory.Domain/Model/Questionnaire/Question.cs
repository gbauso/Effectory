using Effectory.Shared.Domain;
using System;
using System.Collections.Generic;

namespace Effectory.Core.Model
{
    public class Question : IValueObject
    {
        private Question() { }

        public int QuestionId { get; private set; }
        public int OrderNumber { get; private set; }
        public IDictionary<string, string> Texts { get; private set; }
        public AnswerCategoryType AnswerCategoryType { get; private set; }
        public int ItemType { get; private set; }
        public ICollection<Answer> Answers { get; private set; }
    }
}
