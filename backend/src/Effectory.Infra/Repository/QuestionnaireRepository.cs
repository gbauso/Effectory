using Effectory.Core.Model;
using Effectory.Infra.Repository.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Effectory.Infra.Repository
{
    public class QuestionnaireRepository : MongoDbBaseRepository<Questionnaire>, IQuestionnaireRepository
    {
        protected readonly JsonSerializerSettings _jsonSerializerSettings;

        public QuestionnaireRepository(
            IMongoClient client,
            JsonSerializerSettings jsonSerializerSettings) : base(client)
        {
            _jsonSerializerSettings = jsonSerializerSettings;
        }

        public async Task<ICollection<Questionnaire>> GetAllSimple()
        {
            var filter = Builders<Questionnaire>.Filter.Empty;
            var result = await _Collection.Find(filter).Project(GetProjection()).ToListAsync();

            return JsonConvert.DeserializeObject<ICollection<Questionnaire>>(result.ToJson(), _jsonSerializerSettings);
        }

        private ProjectionDefinition<Questionnaire> GetProjection()
        {
            var projection = Builders<Questionnaire>.Projection
                .Include(x => x.QuestionnaireId)
                .Include(x => x.Texts)
                .Exclude("_id");

            return projection;
        }
    }
}
