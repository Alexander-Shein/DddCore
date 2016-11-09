using Contracts.Crosscutting.Base;

namespace Contracts.Crosscutting.Ioc.Base
{
    public interface IContainerConfigFactory : IFactory<IContainerConfig, ContainerType>
    {
    }
}