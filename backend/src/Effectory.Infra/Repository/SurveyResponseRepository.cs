using Effectory.Core.Model.Response;
using Effectory.Infra.Repository.Interfaces;
using MongoDB.Driver;

namespace Effectory.Infra.Repository
{
    public class SurveyResponseRepository : MongoDbBaseRepository<SurveyResponse>, ISurveyResponseRepository
    {
        public SurveyResponseRepository(IMongoClient client) : base(client) { }
    }
}
