using System.Linq;
using Contracts.Crosscutting.ObjectMapper;
using Contracts.Crosscutting.ObjectMapper.Base;
using Crosscutting.Infrastructure.Configuration;

namespace Crosscutting.ObjectMapper
{
    public class ObjectMapperBootstrapper : IObjectMapperBootstrapper
    {
        #region Public Methods

        public IObjectMapper Bootstrap(ObjectMapperType type)
        {
            var modules =
                AssemblyUtility
                    .GetInstances<IObjectMapperModule>()
                    .ToArray();

            return Bootstrap(type, modules);
        }

        public IObjectMapper Bootstrap(ObjectMapperType type, params IObjectMapperModule[] modules)
        {
            var objectMapper = new ObjectMapperConfigFactory().Create(type);

            if (modules != null)
            {
                foreach (var module in modules)
                {
                    module.Install(objectMapper);
                }
            }

            return objectMapper;
        }

        #endregion
    }
}