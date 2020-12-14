using Effectory.Core.Model.SurveyResponse;
using Effectory.Shared.Domain;
using Effectory.Shared.Exceptions;
using System;
using System.Collections.Generic;

namespace Effectory.Core.Model.Response
{
    public class SurveyResponse : AggregateRootBase
    {
        private SurveyResponse() {}

        public Guid SurveyResponseId { get; private set; }
        public int QuestionnaireId { get; private set; }
        public ICollection<QuestionResponse> Responses { get; private set; }

        public void AddResponse(QuestionResponse response)
        {
            if (Responses.Contains(response))
                throw new DomainException();

            Responses.Add(response);

            MarkAsModified();
        }

        public override bool IsValid()
        {
            return SurveyResponseId != Guid.Empty && QuestionnaireId != 0;
        }

        public static SurveyResponse Create(Guid executionId, int questionnaireId)
        {
            if (executionId == Guid.Empty)
                throw new ArgumentException();

            if(questionnaireId == 0)
                throw new ArgumentException();

            return new SurveyResponse
            {
                QuestionnaireId = questionnaireId,
                SurveyResponseId = executionId,
                Responses = new List<QuestionResponse>(),
                State = EntityState.Added,
            };
        }

    }
}
