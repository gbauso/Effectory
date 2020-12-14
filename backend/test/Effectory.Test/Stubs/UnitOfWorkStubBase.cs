using Effectory.Shared.Domain;
using Effectory.Shared.Exceptions;
using Effectory.Shared.Ports;
using System;
using System.Threading.Tasks;

namespace Effectory.Test.Stubs
{
    public abstract class UnitOfWorkStubBase<T> : IUnitOfWork<T> where T: IAggregateRoot
    {
        private T Entity;

        public Task Commit()
        {
            Entity.ClearEvents();
            Entity.ClearState();

            return Task.CompletedTask;
        }

        public Task<T> GetOrCreate(object id, Func<T> objectCreation = null)
        {
            if (Entity != null) return Task.FromResult(Entity);

            if (objectCreation == null) throw new NotFoundException();

            var entity = objectCreation();
            Entity = entity;

            return Task.FromResult(entity);
        }

        public void SetEntity(T entity)
        {
            Entity = entity;
        }
    }
}
