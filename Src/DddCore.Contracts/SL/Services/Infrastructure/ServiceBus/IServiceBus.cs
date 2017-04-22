namespace DddCore.Contracts.SL.Services.Infrastructure.ServiceBus
{
    /// <summary>
    /// Under construction
    /// </summary>
    public interface IServiceBus
    {
        void SendLocal(IApplicationMessage applicationEvent);
    }
}
