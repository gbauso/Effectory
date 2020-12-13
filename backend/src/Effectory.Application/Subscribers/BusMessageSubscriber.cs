using Effectory.Application.Extensions;
using Effectory.Infra.ServiceBus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Effectory.Application.MessageHandler
{
    public class BusMessageSubscriber : ISubscribe
    {
        private readonly IServiceProvider _ServiceProvider;

        public BusMessageSubscriber(IServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;
        }

        public async Task HandleMessage(BusMessage message)
        {
            using (var scope = _ServiceProvider.CreateScope())
            {
                var request = message.ConvertBusMessageToMediatr();
                await scope.ServiceProvider.GetRequiredService<IMediator>().Send(request);
            }
        }
    }
}
