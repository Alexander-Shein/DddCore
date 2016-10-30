using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using Contracts.Crosscutting.Configuration;

namespace Crosscutting.Infrastructure.Configuration
{
    public class WebConfig : IConfig
    {
        public T Get<T>(string key)
        {
            var paths = key.Split('/');

            if (paths.Length > 1)
            {
                return GetFromConfigSection<T>(String.Join("/", paths.Take(paths.Length - 1)), paths.Last());
            }

            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(ConfigurationManager.AppSettings[key]);
        }

        static T GetFromConfigSection<T>(string path, string key)
        {
            return GetValue<T>(GetConfigSection(path)[key]);
        }

        static NameValueCollection GetConfigSection(string path)
        {
            return (NameValueCollection)ConfigurationManager.GetSection(path);
        }

        static T GetValue<T>(string value)
        {
            return (T)Convert.ChangeType(!string.IsNullOrEmpty(value) ? value : string.Empty, typeof(T));
        }
    }
}
