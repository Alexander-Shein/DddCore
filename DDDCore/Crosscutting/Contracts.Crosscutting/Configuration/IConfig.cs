namespace Contracts.Crosscutting.Configuration
{
    public interface IConfig
    {
        T Get<T>(string key);
    }
}
