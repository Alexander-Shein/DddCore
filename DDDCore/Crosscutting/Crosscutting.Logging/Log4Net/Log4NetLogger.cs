using System;
using Contracts.Crosscutting.Logging;
using log4net;

namespace Crosscutting.Logging.Log4Net
{
    internal class Log4NetLogger : ILogger
    {
        volatile ILog logWriter;
        static readonly object SyncRoot = new Object();
        bool isConfigured = false;

        public Log4NetLogger(string name)
        {
            ConfigureLogger(name);
        }
        public Log4NetLogger(ILog logWriter)
        {
            if (logWriter != null)
                this.logWriter = logWriter;
        }

        public void ConfigureLogger(string name)
        {
            if (logWriter == null)
            {
                lock (SyncRoot)
                {
                    if (logWriter == null)
                    {
                        if (!isConfigured)
                        {
                            log4net.Config.XmlConfigurator.Configure();
                            isConfigured = true;
                        }

                        logWriter = LogManager.GetLogger(name);
                    }
                }
            }
        }

        public void LogInformation(string message, params object[] args)
        {
            if (args != null)
                message = string.Format(message, args);
            logWriter.Info(message);
        }

        public void LogTrace(string message, params object[] args)
        {
            LogInformation(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            if (args != null)
                message = string.Format(message, args);
            logWriter.Warn(message);
        }

        public void LogError(string message, Exception exception, params object[] args)
        {
            if (args != null)
                message = string.Format(message, args);
            if (exception != null)
                logWriter.Error(message, exception);
            else
            {
                logWriter.Error(message);
            }
        }

        public void LogFatal(string message, Exception exception, params object[] args)
        {
            if (args != null)
                message = string.Format(message, args);
            if (exception != null)
                logWriter.Fatal(message, exception);
            else
            {
                logWriter.Fatal(message);
            }
        }

        public void LogDebug(string message, params object[] args)
        {
            if (args != null)
                message = string.Format(message, args);
            logWriter.Debug(message);
        }

        public void LogDebugObject(string message, object item, params object[] args)
        {
            if (args != null)
                message = string.Format(message, args);
            if (item != null)
            {
                var exMsg = String.Format(" : Object Data : {0} ", item);
                message = message + exMsg;
            }
            logWriter.Debug(message);
        }
    }
}