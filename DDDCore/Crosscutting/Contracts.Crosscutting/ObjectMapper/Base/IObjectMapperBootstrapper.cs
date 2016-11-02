using Contracts.Crosscutting.Base;

namespace Contracts.Crosscutting.ObjectMapper.Base
{
    public interface IObjectMapperBootstrapper : IBootstrapper<IObjectMapper, ObjectMapperType, IObjectMapperModule>
    {
    }
}