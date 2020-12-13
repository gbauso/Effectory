using Effectory.Application.Dto;
using Effectory.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Effectory.Application.Extensions
{
    public static class QuestionnaireExtensions
    {
        public static QuestionnaireDto AsDto(this Questionnaire domain, string locale = "en-us")
        {
            return new QuestionnaireDto
            {
                Id = domain.QuestionnaireId,
                Title = domain.Texts?.FirstOrDefault(i => i.Key.ToLower() == locale).Value,
                Questions = domain.Subjects
                    .SelectMany(w => w.Questions, (subject, question) => new { subject, question })
                    .Select(x => new QuestionDto
                        {
                            Id = x.question.QuestionId,
                            AnswerType = (AnswerTypeEnumDto) x.question.AnswerCategoryType,
                            Subject = new SubjectDto
                            {
                                Id = x.subject.SubjectId,
                                Subject = x.subject.Texts?.FirstOrDefault(i => i.Key.ToLower() == locale).Value
                            },
                            Question = x.question.Texts?.FirstOrDefault(i => i.Key.ToLower() == locale).Value,
                            Answers = x.question.Answers.Select(a => new AnswerDto
                            {
                                Id = a.AnswerId,
                                Text = a.Texts?.FirstOrDefault(i => i.Key.ToLower() == locale).Value
                            })
                        }
                    )
            };
        }

        public static IEnumerable<SimpleQuestionnaireDto> AsSimpleDto(this IEnumerable<Questionnaire> list, string locale = "en-us")
        {
            return list.Select(q => new SimpleQuestionnaireDto
            {
                Id = q.QuestionnaireId,
                Title = q.Texts?.FirstOrDefault(i => i.Key.ToLower() == locale).Value
            });
        }
    }
}
