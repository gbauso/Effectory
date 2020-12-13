using Effectory.Core.Model.Response;
using Effectory.Core.Ports;
using Effectory.Infra.Repository.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Effectory.Infra.UnitOfWork
{
    public class SurveyResponseUnitOfWork : UnitOfWorkBase<SurveyResponse>, ISurveyResponseUnitOfWork
    {
        public SurveyResponseUnitOfWork(
            ISurveyResponseRepository repository,
            IEventSender eventSender,
            IDistributedCache cache,
            DistributedCacheEntryOptions options,
            JsonSerializerSettings serializerSettings)
            : base(repository, eventSender, cache, options, serializerSettings)
        {

        }
    }
}
