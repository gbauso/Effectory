using Effectory.Shared.Exceptions;
using Effectory.Test.Helpers;
using FluentAssertions;
using System;
using Xunit;

namespace Effectory.Test.Domain
{
    public class QuestionnaireTest
    {
        [Fact]
        public void Questionnaire_IsValid_ValidQuestion_ShouldBeTrue()
        {
            var questionnaire = DataHelper.GetFakeQuestionnaire();

            questionnaire.IsValid().Should().BeTrue();
        }

        [Fact]
        public void Questionnaire_AnswerQuestion_ValidAnswer_ShoudRaiseEvent()
        {
            var questionnaire = DataHelper.GetFakeQuestionnaire();

            questionnaire.AnswerQuestion(2605515, 3807638, 17969124, string.Empty, Guid.NewGuid());

            questionnaire.GetEventsToSend().Should().HaveCount(1);
        }

        [Fact]
        public void Questionnaire_AnswerQuestion_InValidAnswer_ShoudThrownDomainException()
        {
            var questionnaire = DataHelper.GetFakeQuestionnaire();

            Action answer = () => questionnaire.AnswerQuestion(2605515, 3807638, 0, string.Empty, Guid.NewGuid());

            answer.Should().Throw<DomainException>();
        }

        [Fact]
        public void Questionnaire_AnswerQuestion_InValidArguments_ShoudThrownArgumentException()
        {
            var questionnaire = DataHelper.GetFakeQuestionnaire();

            Action answer = () => questionnaire.AnswerQuestion(2605515, 3807638, null, null, Guid.Empty);

            answer.Should().Throw<ArgumentException>();
        }


    }
}
