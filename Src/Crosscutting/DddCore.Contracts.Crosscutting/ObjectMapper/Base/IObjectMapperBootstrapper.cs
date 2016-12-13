using DddCore.Contracts.Crosscutting.Base;

namespace DddCore.Contracts.Crosscutting.ObjectMapper.Base
{
    public interface IObjectMapperBootstrapper : IBootstrapper<IObjectMapper, IObjectMapperConfig, IObjectMapperModule>
    {
    }
}