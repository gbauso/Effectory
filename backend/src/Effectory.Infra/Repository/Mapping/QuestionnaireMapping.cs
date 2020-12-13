using Effectory.Core.Model;
using Effectory.Shared.Domain;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Effectory.Infra.Repository.Mapping
{
    public class QuestionnaireMapping : IEntityMapper
    {
        public void Map()
        {
            BsonClassMap.RegisterClassMap<Questionnaire>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.QuestionnaireId).SetElementName("questionnaireId");
                cm.MapMember(c => c.Subjects).SetElementName("subjects");
                cm.MapMember(c => c.Texts).SetElementName("texts");
            });

            BsonClassMap.RegisterClassMap<Subject>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.SubjectId).SetElementName("subjectId");
                cm.MapMember(c => c.ItemType).SetElementName("itemType");
                cm.MapMember(c => c.OrderNumber).SetElementName("orderNumber");
                cm.MapMember(c => c.Questions).SetElementName("questions");
                cm.MapMember(c => c.Texts).SetElementName("texts");
            });

            BsonClassMap.RegisterClassMap<Question>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.QuestionId).SetElementName("questionId");
                cm.MapMember(c => c.ItemType).SetElementName("itemType");
                cm.MapMember(c => c.OrderNumber).SetElementName("orderNumber");
                cm.MapMember(c => c.AnswerCategoryType).SetElementName("answerCategoryType");
                cm.MapMember(c => c.Texts).SetElementName("texts");
                cm.MapMember(c => c.Answers).SetElementName("answers");
            });
            
            BsonClassMap.RegisterClassMap<Answer>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.AnswerId).SetElementName("answerId");
                cm.MapMember(c => c.ItemType).SetElementName("itemType");
                cm.MapMember(c => c.OrderNumber).SetElementName("orderNumber");
                cm.MapMember(c => c.Texts).SetElementName("texts");
                cm.MapMember(c => c.AnswerType).SetElementName("answerType");
            });
        }
    }
}
