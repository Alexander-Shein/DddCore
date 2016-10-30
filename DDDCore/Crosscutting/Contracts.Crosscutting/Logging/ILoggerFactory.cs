namespace Contracts.Crosscutting.Logging
{
    public interface ILoggerFactory
    {
        ILogger GetLogger(string name);
    }
}