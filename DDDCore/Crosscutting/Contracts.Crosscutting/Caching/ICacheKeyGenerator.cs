namespace Contracts.Crosscutting.Caching
{
    public interface ICacheKeyGenerator
    {
        string GetKey(string key);
    }
}