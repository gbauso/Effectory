using Effectory.Shared.Domain;
using System.Collections.Generic;
using System.Transactions;

namespace Effectory.Core.Ports
{
    public interface IEventSender
    {
        void SendEvent(IEvent @event);
        void SendEvents(IEnumerable<IEvent> events);
    }
}
