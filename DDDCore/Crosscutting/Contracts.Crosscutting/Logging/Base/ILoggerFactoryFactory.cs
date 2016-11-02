using Contracts.Crosscutting.Base;

namespace Contracts.Crosscutting.Logging.Base
{
    public interface ILoggerFactoryFactory : IFactory<ILoggerFactory, LoggerType>
    {
    }
}