using System;

namespace Contracts.Crosscutting.Logging
{
    public interface ILogger
    {
        void ConfigureLogger(string name);
        void LogInformation(string message, params object[] args);
        void LogTrace(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(string message, Exception exception, params object[] args);
        void LogFatal(string message, Exception exception, params object[] args);
        void LogDebug(string message, params object[] args);
    }
}
