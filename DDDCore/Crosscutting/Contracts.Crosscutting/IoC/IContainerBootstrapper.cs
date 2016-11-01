namespace Contracts.Crosscutting.IoC
{
    public interface IContainerBootstrapper
    {
        void Bootstrap(ContainerType containerType);
        void Bootstrap(ContainerType containerType, params IIocModule[] iocModules);
    }
}