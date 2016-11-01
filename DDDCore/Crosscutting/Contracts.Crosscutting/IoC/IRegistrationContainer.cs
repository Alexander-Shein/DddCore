namespace Contracts.Crosscutting.IoC
{
    public interface IRegistrationContainer : IContainer
    {
        IComponent Register<TContract, TImplementation>() where TImplementation : class, TContract where TContract : class;
    }
}