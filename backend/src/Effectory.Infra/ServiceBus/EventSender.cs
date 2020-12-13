using Effectory.Core.Ports;
using Effectory.Shared.Domain;
using MassTransit;
using System.Collections.Generic;
using System.Transactions;

namespace Effectory.Infra.ServiceBus
{
    public class EventSender : IEventSender
    {
        private readonly IBusControl _Bus;

        public EventSender(IBusControl bus)
        {
            _Bus = bus;
        }

        public void SendEvent(IEvent @event)
        {
            var message = new BusMessage
            {
                MessageType = @event.GetType().Name
            };
            message.SetData(@event);

            _Bus.Send(message);
        }

        public void SendEvents(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
                SendEvent(@event);
        }

    }
}
