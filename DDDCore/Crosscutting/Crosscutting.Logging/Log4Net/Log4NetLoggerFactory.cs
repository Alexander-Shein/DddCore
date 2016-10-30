using Contracts.Crosscutting.Logging;

namespace Crosscutting.Logging.Log4Net
{
    public class Log4NetLoggerFactory : LoggerFactoryBase
    {
        protected override ILogger CreateLogger(string name)
        {
            return new Log4NetLogger(name);
        }
    }
}