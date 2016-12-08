using DddCore.Contracts.Crosscutting.Base;

namespace DddCore.Contracts.Crosscutting.ObjectMapper.Base
{
    public interface IObjectMapperConfigFactory : IFactory<IObjectMapperConfig, ObjectMapperType>
    {
    }
}