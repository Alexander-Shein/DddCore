using DddCore.Contracts.SL.Services.Infrastructure;

namespace DddCore.Contracts.SL.Services.Application.DomainStack
{
    public interface IGuard : IInfrastructureService
    {
        /// <summary>
        /// If null throws AgrumentNullException.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        void NotNull(object obj, string message = "");
    }
}