using Effectory.Core.Model.Response;
using Effectory.Core.Model.SurveyResponse;
using Effectory.Shared.Exceptions;
using Effectory.Test.Helpers;
using FluentAssertions;
using System;
using Xunit;

namespace Effectory.Test.Domain
{
    public class SurveyResponseTest
    {
        [Fact]
        public void SurveyResponse_IsValid_ValidResponse_ShouldBeTrue()
        {
            var survey = SurveyResponse.Create(Guid.NewGuid(), 1000);

            survey.IsValid().Should().BeTrue();
        }

        [Fact]
        public void SurveyResponse_AddResponse_ValidResponse_ShouldAddOnList()
        {
            var survey = SurveyResponse.Create(Guid.NewGuid(), 1000);

            var response = QuestionResponse.Create(DataHelper.GetKeyValuePairs(),
                                                   DataHelper.GetKeyValuePairs(),
                                                   new[] { DataHelper.GetKeyValuePairs() },
                                                   string.Empty,
                                                   2);

            Action addResponse = () => survey.AddResponse(response);

            addResponse.Should().NotThrow();
            survey.Responses.Should().HaveCount(1);
        }

        [Fact]
        public void SurveyResponse_AddResponse_RepeatedResponse_ShouldThrowDomainException()
        {
            var survey = SurveyResponse.Create(Guid.NewGuid(), 1000);

            var response = QuestionResponse.Create(DataHelper.GetKeyValuePairs(),
                                                   DataHelper.GetKeyValuePairs(),
                                                   new[] { DataHelper.GetKeyValuePairs() },
                                                   string.Empty,
                                                   2);

            survey.AddResponse(response);
            Action addResponse = () => survey.AddResponse(response);

            addResponse.Should().Throw<DomainException>();
            survey.Responses.Should().HaveCount(1);
        }

    }
}
