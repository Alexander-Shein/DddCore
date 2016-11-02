using Contracts.Crosscutting.Base;

namespace Contracts.Crosscutting.IoC.Base
{
    public interface IContainerBootstrapper : IBootstrapper<IContainer, ContainerType, IIocModule>
    {
    }
}