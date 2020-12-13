using MediatR;
using System;

namespace Effectory.Core.Commands
{
    public class AnswerQuestionCommand : IRequest<bool>
    {
        public int QuestionnaireId { get; set; }
        public int SubjectId { get; set; }
        public int QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public string Answer { get; set; }
        public Guid ExecutionId { get; set; }
    }
}
