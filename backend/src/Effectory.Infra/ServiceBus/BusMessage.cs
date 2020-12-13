using Newtonsoft.Json;

namespace Effectory.Infra.ServiceBus
{
    public class BusMessage
    {
        public string MessageType { get; set; }
        public string MessageData { get; private set; }

        public void SetData(object obj)
        {
            MessageData = JsonConvert.SerializeObject(obj);
        }
    }
}
