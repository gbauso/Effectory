using Effectory.Core.Commands;
using Effectory.Test.Helpers;
using Effectory.Test.Stubs;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Effectory.Test.CommandHandlers
{
    public class AnswerQuestionCommandHandlerTest
    {
        private readonly QuestionnaireUnitOfWorkStub _unitOfWork;

        public AnswerQuestionCommandHandlerTest()
        {
            _unitOfWork = new QuestionnaireUnitOfWorkStub();
        }

        [Fact]
        public async Task AnswerQuestionCommandHandler_Handle_ValidCommand()
        {
            var handler = new AnswerQuestionCommandHandler(_unitOfWork);

            var command = new AnswerQuestionCommand()
            {
                Answer = string.Empty,
                QuestionnaireId = 1000,
                QuestionId = 3807638,
                ExecutionId = Guid.NewGuid(),
                SubjectId = 2605515,
                AnswerId = 17969124
            };

            _unitOfWork.SetEntity(DataHelper.GetFakeQuestionnaire());

            var result = await handler.Handle(command, default);

            result.Should().BeTrue();
        }
    }
}
