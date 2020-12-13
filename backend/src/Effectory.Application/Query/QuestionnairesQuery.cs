using Effectory.Application.Dto;
using MediatR;
using System.Collections;
using System.Collections.Generic;

namespace Effectory.Application.Query
{
    public class QuestionnairesQuery : IRequest<IEnumerable<SimpleQuestionnaireDto>>
    {
        public void Validate()
        {
            return;
        }
    }
}
