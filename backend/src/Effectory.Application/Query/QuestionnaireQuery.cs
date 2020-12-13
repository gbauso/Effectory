using Effectory.Application.Dto;
using MediatR;

namespace Effectory.Application.Query
{
    public class QuestionnaireQuery : IRequest<QuestionnaireDto>
    {
        public int QuestionnaireId { get; set; }

        public void Validate()
        {

        }
    }
}
