using Effectory.Shared.Domain;
using Effectory.Shared.Ports;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Effectory.Infra.Repository
{
    public abstract class MongoDbBaseRepository<T> : IRepository<T> where T : IAggregateRoot
    {
        protected readonly IMongoCollection<T> _Collection;
        private readonly IClientSessionHandle _clientSessionHandle;

        private const string DATABASE = "effectory";

        public MongoDbBaseRepository(IMongoClient client)
        {
            _clientSessionHandle = client.StartSession();
            var database = client.GetDatabase(DATABASE);
            _Collection = database.GetCollection<T>(typeof(T).Name.ToLower());
        }

        public async Task<T> Get(object getBy)
        {
            var filter = GetFilter(getBy);
            var result = await _Collection.FindAsync<T>(filter);

            return await result.FirstOrDefaultAsync<T>();
        }

        public async Task<T> Save(T entry)
        {
            if(entry.State == EntityState.Dirty)
            {
                var filter = Builders<T>.Filter.Eq("_id", entry.Id);
                await _Collection.DeleteOneAsync(filter);
            }

            entry.ClearState();
            await _Collection.InsertOneAsync(entry);
            return entry;
        }

        private FilterDefinition<T> GetFilter(object fields)
        {
            var builder = Builders<T>.Filter;
            var filterDefinition = builder.Empty;

            foreach (var field in fields.GetType().GetProperties())
            {
                filterDefinition &= builder.Eq(field.Name, field.GetValue(fields));
            }

            return filterDefinition;
        }
    }
}
