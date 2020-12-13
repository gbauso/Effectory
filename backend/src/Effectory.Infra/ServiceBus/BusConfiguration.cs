using System;
using System.Collections.Generic;
using System.Text;

namespace Effectory.Infra.ServiceBus
{
    public class BusConfiguration
    {
        public string Url { get; set; }
        public string Queue { get; set; }
        public BusCrendentials Credentials { get; set; }
    }
}
