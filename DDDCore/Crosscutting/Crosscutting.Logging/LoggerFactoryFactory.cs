using System;
using Contracts.Crosscutting.Logging;
using Contracts.Crosscutting.Logging.Base;
using Crosscutting.Logging.Log4Net;

namespace Crosscutting.Logging
{
    public class LoggerFactoryFactory : ILoggerFactoryFactory
    {
        #region Public Methods

        public ILoggerFactory Create(LoggerType loggerType)
        {
            switch (loggerType)
            {
                case LoggerType.Log4Net:
                    return new Log4NetLoggerFactory();

                default: throw new NotImplementedException();
            }
        }

        #endregion
    }
}