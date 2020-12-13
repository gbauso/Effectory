using Effectory.Shared.Domain;
using MediatR;
using System;
using System.Collections.Generic;

namespace Effectory.Core.Events
{
    public class QuestionAnsweredEvent : IEvent, IRequest<bool>
    {
        public int QuestionnareId { get; set; }
        public IDictionary<string, string> Subject { get; set; }
        public IDictionary<string, string> Question { get; set; }
        public IEnumerable<IDictionary<string, string>> Answers { get; set; }
        public int? AnswerIndex { get; set; }
        public string Answer { get; set; }
        public Guid ExecutionId { get; set; }
    }
}
