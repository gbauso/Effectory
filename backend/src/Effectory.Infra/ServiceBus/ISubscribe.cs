using System.Threading.Tasks;

namespace Effectory.Infra.ServiceBus
{
    public interface ISubscribe
    {
        Task HandleMessage(BusMessage message);
    }
}
