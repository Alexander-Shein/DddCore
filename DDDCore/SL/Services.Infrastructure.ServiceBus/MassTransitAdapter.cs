using System;
using Contracts.Crosscutting.Configuration;
using Contracts.Services.Infrastructure.ServiceBus;

namespace Services.Infrastructure.ServiceBus
{
    public class MassTransitAdapter : IServiceBus
    {
        public IConfig Config { get; set; }
        public MassTransit.IBus MassTransitServiceBus { get; set; }

        public void SendLocal(IApplicationMessage applicationEvent)
        {
            var queuePath = String.Format(Config.Get<string>("msmqNameTmpl"), Environment.MachineName);
            //MassTransitServiceBus.GetSendEndpoint(new Uri(queuePath)).Start(); .Send(applicationEvent);
        }
    }
}
