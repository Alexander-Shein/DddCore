using System;
using DddCore.Contracts.Crosscutting.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace DddCore.Crosscutting.Ioc.MicrosoftDependencyInjection
{
    public class ContainerConfig : IContainerConfig
    {
        #region Private Members

        readonly IServiceCollection serviceCollection;

        #endregion

        public ContainerConfig(IServiceCollection serviceCollection)
        {
            this.serviceCollection = serviceCollection;
        }

        #region Public Methods

        public IComponent Register<TContract, TImplementation>() where TContract : class where TImplementation : class, TContract
        {
            throw new NotImplementedException();
        }

        public IComponent Register<TContract, TContract2, TImplementation>() where TContract : class where TContract2 : class where TImplementation : class, TContract, TContract2
        {
            throw new NotImplementedException();
        }

        public IComponent Register<TContract>(Func<TContract> factoryMethod) where TContract : class
        {
            throw new NotImplementedException();
        }

        public IServiceProvider BuildContainer()
        {
            return serviceCollection.BuildServiceProvider();
        }

        #endregion
    }
}