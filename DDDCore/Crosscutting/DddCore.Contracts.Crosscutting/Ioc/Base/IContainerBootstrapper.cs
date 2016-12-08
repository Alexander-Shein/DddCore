using DddCore.Contracts.Crosscutting.Base;

namespace DddCore.Contracts.Crosscutting.Ioc.Base
{
    public interface IContainerBootstrapper : IBootstrapper<IContainer, ContainerType, IIocModule>
    {
    }
}