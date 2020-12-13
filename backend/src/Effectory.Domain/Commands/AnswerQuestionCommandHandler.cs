using Effectory.Core.Ports;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Effectory.Core.Commands
{
    internal class AnswerQuestionCommandHandler : IRequestHandler<AnswerQuestionCommand, bool>
    {
        private readonly IQuestionnaireUnitOfWork _questionaireUnitOfWork;

        public AnswerQuestionCommandHandler(IQuestionnaireUnitOfWork questionaireUnitOfWork)
        {
            _questionaireUnitOfWork = questionaireUnitOfWork;
        }

        public async Task<bool> Handle(AnswerQuestionCommand request, CancellationToken cancellationToken)
        {
            var questionnaire = await _questionaireUnitOfWork
                                        .GetOrCreate(new { request.QuestionnaireId });

            questionnaire.AnswerQuestion(request.SubjectId,
                                         request.QuestionId,
                                         request.AnswerId,
                                         request.Answer,
                                         request.ExecutionId);

            await _questionaireUnitOfWork.Commit();

            return true;
        }
    }
}
