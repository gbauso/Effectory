using MongoDB.Bson;
using System.Collections.Generic;

namespace Effectory.Shared.Domain
{
    public abstract class AggregateRootBase : IAggregateRoot
    {
        public AggregateRootBase()
        {
            State = EntityState.Unchanged;
            Id = ObjectId.GenerateNewId().ToString();
        }

        public object Id { get; private set; }

        private ICollection<IEvent> EventsToSend = new List<IEvent>();

        public EntityState State { get; protected set; }

        protected void RaiseEvent(IEvent @event)
        {
            EventsToSend.Add(@event);
        }

        protected void MarkAsModified()
        {
            if(State != EntityState.Added)
                State = EntityState.Dirty;
        }

        public abstract bool IsValid();

        public ICollection<IEvent> GetEventsToSend()
        {
            return EventsToSend;
        }


        public void ClearEvents()
        {
            EventsToSend.Clear();
        }

        public void ClearState()
        {
            State = EntityState.Unchanged;
        }
    }
}
