using Effectory.Shared.Domain;
using System.Collections.Generic;

namespace Effectory.Core.Model
{
    public class Answer : IValueObject
    {
        private Answer() { }

        public int? AnswerId { get; private set; }
        public int OrderNumber { get; private set; }
        public IDictionary<string, string> Texts { get; private set; }
        public int AnswerType { get; private set; }
        public int ItemType { get; private set; }

    }
}
