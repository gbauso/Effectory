using Effectory.Infra.ServiceBus;
using Effectory.Shared.Exceptions;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Effectory.Application.Extensions
{
    public static class ServiceBusExtensions
    {
        public static IRequest<bool> ConvertBusMessageToMediatr(this BusMessage message)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                                               .Where(i => i.FullName.Contains("Core") 
                                                        || i.FullName.Contains("Application"))
                                               .SelectMany(x => x.GetTypes());

            var type = types.FirstOrDefault(x => x.Name.ToLower()
                                                       .Equals(message.MessageType.ToLower()));

            try
            {
                return (IRequest<bool>) JsonConvert.DeserializeObject(message.MessageData, type);
            }
            catch
            {
                throw new CommandException();
            }


        }
    }
}
