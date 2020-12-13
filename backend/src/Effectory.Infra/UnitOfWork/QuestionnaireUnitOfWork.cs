using Effectory.Core.Model;
using Effectory.Core.Ports;
using Effectory.Infra.Repository;
using Effectory.Infra.Repository.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Effectory.Infra.UnitOfWork
{
    public class QuestionnaireUnitOfWork : UnitOfWorkBase<Questionnaire>, IQuestionnaireUnitOfWork
    {
        public QuestionnaireUnitOfWork(
            IQuestionnaireRepository repository,
            IEventSender eventSender,
            IDistributedCache cache,
            DistributedCacheEntryOptions options,
            JsonSerializerSettings serializerSettings) 
            : base(repository, eventSender, cache, options, serializerSettings)
        {

        }
    }
}
