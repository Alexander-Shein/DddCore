using System;
using System.Collections.Concurrent;
using Contracts.Crosscutting.Logging;

namespace Crosscutting.Logging
{
    public abstract class LoggerFactoryBase : ILoggerFactory
    {
        readonly ConcurrentDictionary<string, ILogger> cachedLoggers;

        protected LoggerFactoryBase()
        {
            cachedLoggers = new ConcurrentDictionary<string, ILogger>(StringComparer.OrdinalIgnoreCase);
        }

        public ILogger GetLogger(string name)
        {
            return GetCachedLogger(name);
        }

        protected void ClearLoggerCache()
        {
            lock (cachedLoggers)
            {
                cachedLoggers.Clear();
            }
        }

        protected abstract ILogger CreateLogger(string name);

        ILogger GetCachedLogger(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            ILogger logger;

            if (!cachedLoggers.TryGetValue(name, out logger))
            {
                lock (cachedLoggers)
                {
                    if (!cachedLoggers.TryGetValue(name, out logger))
                    {
                        logger = CreateLogger(name);
                        if (logger == null)
                        {
                            throw new ArgumentException(nameof(name));
                        }
                        cachedLoggers.TryAdd(name, logger);
                    }
                }
            }
            return logger;
        }
    }
}