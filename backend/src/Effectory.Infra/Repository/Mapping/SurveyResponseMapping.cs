using Effectory.Core.Model.Response;
using Effectory.Core.Model.SurveyResponse;
using MongoDB.Bson.Serialization;

namespace Effectory.Infra.Repository.Mapping
{
    public class SurveyResponseMapping : IEntityMapper
    {
        public void Map()
        {
            BsonClassMap.RegisterClassMap<SurveyResponse>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.QuestionnaireId).SetElementName("questionnaireId");
                cm.MapMember(c => c.SurveyResponseId).SetElementName("surveyResponseId");
                cm.MapMember(c => c.Responses).SetElementName("responses");
            });

            BsonClassMap.RegisterClassMap<QuestionResponse>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.Question).SetElementName("question");
                cm.MapMember(c => c.Answers).SetElementName("answers");
                cm.MapMember(c => c.Answer).SetElementName("answer");
            });
        }
    }
}
