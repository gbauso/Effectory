using Effectory.Core.Model.Response;
using Effectory.Core.Model.SurveyResponse;
using Effectory.Core.Ports;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Effectory.Core.Events
{
    internal class QuestionAnsweredEventHandler : IRequestHandler<QuestionAnsweredEvent, bool>
    {
        private readonly ISurveyResponseUnitOfWork _surveyResponseUnitOfWork;

        public QuestionAnsweredEventHandler(ISurveyResponseUnitOfWork surveyResponseUnitOfWork)
        {
            _surveyResponseUnitOfWork = surveyResponseUnitOfWork;
        }

        public async Task<bool> Handle(QuestionAnsweredEvent request, CancellationToken cancellationToken)
        {
            SurveyResponse surveyResponse = await _surveyResponseUnitOfWork.GetOrCreate(
                new { request.QuestionnareId, request.ExecutionId },
                () => SurveyResponse.Create(request.ExecutionId, request.QuestionnareId));

            QuestionResponse response = QuestionResponse.Create(request.Subject,
                                                                request.Question,
                                                                request.Answers,
                                                                request.Answer,
                                                                request.AnswerIndex);

            surveyResponse.AddResponse(response);

            await _surveyResponseUnitOfWork.Commit();

            return true;
        }
    }
}
