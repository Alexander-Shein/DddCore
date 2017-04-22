namespace DddCore.Contracts.Services.Infrastructure.ServiceBus
{
    /// <summary>
    /// Under construction
    /// </summary>
    public interface IServiceBus
    {
        void SendLocal(IApplicationMessage applicationEvent);
    }
}
