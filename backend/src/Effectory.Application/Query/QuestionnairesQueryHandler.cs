using Effectory.Application.Dto;
using Effectory.Application.Extensions;
using Effectory.Infra.Repository.Interfaces;
using Effectory.Shared.Exceptions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Effectory.Application.Query
{
    internal class QuestionnairesQueryHandler : IRequestHandler<QuestionnairesQuery, IEnumerable<SimpleQuestionnaireDto>>
    {
        private readonly IQuestionnaireRepository _questionnaireRepository;

        public QuestionnairesQueryHandler(IQuestionnaireRepository questionnaireRepository)
        {
            _questionnaireRepository = questionnaireRepository;
        }

        public async Task<IEnumerable<SimpleQuestionnaireDto>> Handle(
            QuestionnairesQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var questionnaires = await _questionnaireRepository.GetAllSimple();

                return questionnaires.AsSimpleDto();
            }
            catch(NotFoundException nfe)
            {
                throw nfe;
            }
        }
    }
}
