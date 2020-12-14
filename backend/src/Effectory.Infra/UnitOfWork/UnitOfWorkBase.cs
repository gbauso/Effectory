using Effectory.Core.Ports;
using Effectory.Shared.Domain;
using Effectory.Shared.Exceptions;
using Effectory.Shared.Extensions;
using Effectory.Shared.Ports;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Effectory.Infra.UnitOfWork
{
    public class UnitOfWorkBase<T> : IUnitOfWork<T> where T : IAggregateRoot
    {
        private readonly IRepository<T> _repository;
        private readonly IEventSender _eventSender;
        private readonly IDistributedCache _distributedCache;
        private readonly DistributedCacheEntryOptions _distributedCacheEntryOptions;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        private T ManagedObject;
        private string CacheKey;

        public UnitOfWorkBase(
            IRepository<T> repository,
            IEventSender eventSender,
            IDistributedCache distributedCache,
            DistributedCacheEntryOptions distributedCacheEntryOptions,
            JsonSerializerSettings jsonSerializer)
        {
            _repository = repository;
            _eventSender = eventSender;
            _distributedCache = distributedCache;
            _distributedCacheEntryOptions = distributedCacheEntryOptions;
            _jsonSerializerSettings = jsonSerializer;
        }

        public async Task Commit()
        {
            if (ManagedObject == null)
                throw new Exception();

            using(var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _eventSender.SendEvents(ManagedObject.GetEventsToSend());
                ManagedObject.ClearEvents();
                if (ManagedObject.State != EntityState.Unchanged)
                {
                    await _repository.Save(ManagedObject);
                }

                await _distributedCache.AddOrUpdateEntry(_distributedCacheEntryOptions,
                                                         CacheKey,
                                                         ManagedObject);

                transaction.Complete();
            }
        }

        public async Task<T> GetOrCreate(object by, Func<T> objectCreation = null)
        {
            CacheKey = GetCacheKey(by);

            if (_distributedCache.TryGetValue(CacheKey, _jsonSerializerSettings, out T value))
            {
                SetManagedObject(value);
                return ManagedObject;
            }

            var fromDatabase = await _repository.Get(by);
            if (fromDatabase == null && objectCreation == null)
                throw new NotFoundException();

            var managedObject = fromDatabase ?? objectCreation();

            SetManagedObject(managedObject);
            await _distributedCache.AddOrUpdateEntry(_distributedCacheEntryOptions, CacheKey, managedObject);
            return ManagedObject;
        }

        private string GetCacheKey(object by)
        {
            return $"{typeof(T).Name}::{string.Join("::", by.GetType().GetProperties().Select(x => $"{x.Name}:{x.GetValue(by)}"))}";
        }

        private void SetManagedObject(T value)
        {
            ManagedObject = value;
        }

    }
}
