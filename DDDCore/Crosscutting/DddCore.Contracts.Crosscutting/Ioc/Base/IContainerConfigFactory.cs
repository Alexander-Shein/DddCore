using DddCore.Contracts.Crosscutting.Base;

namespace DddCore.Contracts.Crosscutting.Ioc.Base
{
    public interface IContainerConfigFactory : IFactory<IContainerConfig, ContainerType>
    {
    }
}