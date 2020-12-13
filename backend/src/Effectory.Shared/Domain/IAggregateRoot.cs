using MongoDB.Bson;
using System.Collections.Generic;

namespace Effectory.Shared.Domain
{
    public interface IAggregateRoot
    {
        object Id { get; }
        EntityState State { get; }
        bool IsValid();

        ICollection<IEvent> GetEventsToSend();

        void ClearEvents();

        void ClearState();
    }
}
