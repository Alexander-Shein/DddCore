using Contracts.Crosscutting.Base;

namespace Contracts.Crosscutting.Ioc.Base
{
    public interface IContainerBootstrapper : IBootstrapper<IContainer, ContainerType, IIocModule>
    {
    }
}