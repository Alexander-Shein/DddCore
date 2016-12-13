using System;

namespace DddCore.Contracts.Crosscutting.Ioc
{
    public interface IContainerConfig : IContainer
    {
        IComponent Register<TContract, TImplementation>() where TImplementation : class, TContract where TContract : class;
        IComponent Register<TContract, TContract2, TImplementation>() where TImplementation : class, TContract, TContract2 where TContract : class where TContract2 : class;
        IComponent Register<TContract>(Func<TContract> factoryMethod) where TContract : class;
    }
}