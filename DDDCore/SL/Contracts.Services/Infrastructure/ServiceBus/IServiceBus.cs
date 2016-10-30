namespace Contracts.Services.Infrastructure.ServiceBus
{
    public interface IServiceBus
    {
        void SendLocal(IApplicationMessage applicationEvent);
    }
}
