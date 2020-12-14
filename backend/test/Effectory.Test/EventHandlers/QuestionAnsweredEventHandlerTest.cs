using Effectory.Core.Events;
using Effectory.Core.Model.Response;
using Effectory.Core.Ports;
using Effectory.Test.Helpers;
using Effectory.Test.Stubs;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Effectory.Test.EventHandlers
{
    public class QuestionAnsweredEventHandlerTest
    {
        private readonly SurveyResponseUnitOfWorkStub _unitOfWork;

        public QuestionAnsweredEventHandlerTest()
        {
            _unitOfWork = new SurveyResponseUnitOfWorkStub();
        }

        [Fact]
        public async Task QuestionAnsweredEventHandlerTest_Handle_ValidInput()
        {
            var executionId = Guid.NewGuid();
            var eventHander = new QuestionAnsweredEventHandler(_unitOfWork);
            var survey = SurveyResponse.Create(executionId, 1000);
            var @event = new QuestionAnsweredEvent
            {
                Answer = string.Empty,
                AnswerIndex = 2,
                Answers = new[] { DataHelper.GetKeyValuePairs() },
                ExecutionId = executionId,
                Question = DataHelper.GetKeyValuePairs(),
                QuestionnareId = 1000,
                Subject = DataHelper.GetKeyValuePairs()
            };

            _unitOfWork.SetEntity(survey);

            var handleResult = await eventHander.Handle(@event, default);
            handleResult.Should().BeTrue();
        }

    }
}
