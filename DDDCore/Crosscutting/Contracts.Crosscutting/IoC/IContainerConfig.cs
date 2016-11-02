namespace Contracts.Crosscutting.IoC
{
    public interface IContainerConfig : IContainer
    {
        IComponent Register<TContract, TImplementation>() where TImplementation : class, TContract where TContract : class;
    }
}