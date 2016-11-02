using Contracts.Crosscutting.Base;

namespace Contracts.Crosscutting.IoC.Base
{
    public interface IContainerConfigFactory : IFactory<IContainerConfig, ContainerType>
    {
    }
}