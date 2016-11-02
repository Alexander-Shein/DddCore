using Contracts.Crosscutting.Base;

namespace Contracts.Crosscutting.ObjectMapper.Base
{
    public interface IObjectMapperConfigFactory : IFactory<IObjectMapperConfig, ObjectMapperType>
    {
    }
}