using DddCore.Contracts.Crosscutting.ObjectMapper;
using DddCore.Contracts.Crosscutting.ObjectMapper.Base;
using DddCore.Crosscutting.Base;

namespace DddCore.Crosscutting.ObjectMapper
{
    public class ObjectMapperBootstrapper : BootstrapperBase<IObjectMapper, IObjectMapperConfig, IObjectMapperModuleInstaller>, IObjectMapperBootstrapper
    {
        protected override IObjectMapper GetInstance(IObjectMapperConfig config)
        {
            return config;
        }
    }
}