using Effectory.Application.Dto;
using Effectory.Application.Extensions;
using Effectory.Core.Ports;
using Effectory.Shared.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Effectory.Application.Query
{
    internal class QuestionnaireQueryHandler : IRequestHandler<QuestionnaireQuery, QuestionnaireDto>
    {
        private readonly IQuestionnaireUnitOfWork _QuestionaireUnitOfWork;

        public QuestionnaireQueryHandler(IQuestionnaireUnitOfWork questionaireUnitOfWork)
        {
            _QuestionaireUnitOfWork = questionaireUnitOfWork;
        }

        public async Task<QuestionnaireDto> Handle(QuestionnaireQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var questionnaiere = await _QuestionaireUnitOfWork.GetOrCreate(request);

                return questionnaiere.AsDto();
            }
            catch(NotFoundException nfe)
            {
                throw nfe;
            }
        }
    }
}
