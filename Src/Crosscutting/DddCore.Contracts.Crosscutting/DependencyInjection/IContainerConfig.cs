using System;

namespace DddCore.Contracts.Crosscutting.DependencyInjection
{
    public interface IContainerConfig
    {
        IComponent Register<TContract, TImplementation>() where TImplementation : class, TContract where TContract : class;
        IComponent Register<TContract>(Func<TContract> factoryMethod) where TContract : class;
        IServiceProvider BuildContainer();
    }
}