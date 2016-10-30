using Contracts.Crosscutting.Caching;
using Contracts.Crosscutting.Configuration;

namespace Crosscutting.Infrastructure.Caching
{
    public class PrefixCacheKeyGenerator : ICacheKeyGenerator
    {
        const string CachePrefixKey = "CachePrefix";
        const string Template = "{0}_{1}";

        readonly IConfig config;

        public PrefixCacheKeyGenerator(IConfig config)
        {
            this.config = config;
        }

        string prefix;
        string Prefix => prefix ?? (prefix = config.Get<string>(CachePrefixKey));

        #region Public Methods

        public string GetKey(string key)
        {
            return string.Format(Template, Prefix, key);
        }

        #endregion
    }
}