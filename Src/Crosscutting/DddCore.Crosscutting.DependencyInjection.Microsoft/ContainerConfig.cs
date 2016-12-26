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
            return new Component<TContract, TImplementation>(serviceCollection);
        }

        public IComponent Register<TContract>(Func<TContract> factoryMethod) where TContract : class
        {
            return new Component<TContract>(serviceCollection, factoryMethod);
        }

        public IServiceProvider BuildContainer()
        {
            return serviceCollection.BuildServiceProvider();
        }

        public IComponent Register(Type contract, Type implementation)
        {
            var genericType = typeof(Component<,>).MakeGenericType(contract, implementation);
            return (IComponent)Activator.CreateInstance(genericType, serviceCollection);
        }

        #endregion
    }

    public class Component<TContract, TImplementation> : IComponent where TContract : class where TImplementation : class, TContract
    {
        #region Private Members

        readonly IServiceCollection serviceCollection;

        #endregion

        public Component(IServiceCollection serviceCollection)
        {
            this.serviceCollection = serviceCollection;
        }

        public ILifeStyle LifeStyle => new LifeStyle<TContract, TImplementation>(serviceCollection);
    }

    public class Component<TContract> : IComponent where TContract : class
    {
        #region Private Members

        readonly IServiceCollection serviceCollection;
        readonly Func<TContract> func;

        #endregion

        public Component(IServiceCollection serviceCollection, Func<TContract> func)
        {
            this.serviceCollection = serviceCollection;
            this.func = func;
        }

        public ILifeStyle LifeStyle => new LifeStyle<TContract>(serviceCollection, func);
    }

    public class LifeStyle<TContract, TImplementation> : ILifeStyle where TContract : class where TImplementation : class, TContract
    {
        #region Private Members

        readonly IServiceCollection serviceCollection;

        #endregion

        public LifeStyle(IServiceCollection serviceCollection)
        {
            this.serviceCollection = serviceCollection;
        }

        public void PerWebRequest()
        {
            serviceCollection.AddScoped<TContract, TImplementation>();
        }

        public void Singleton()
        {
            serviceCollection.AddSingleton<TContract, TImplementation>();
        }

        public void Transient()
        {
            serviceCollection.AddTransient<TContract, TImplementation>();
        }
    }

    public class LifeStyle<TContract> : ILifeStyle where TContract : class
    {
        #region Private Members

        readonly IServiceCollection serviceCollection;
        readonly Func<TContract> func;

        #endregion

        public LifeStyle(IServiceCollection serviceCollection, Func<TContract> func)
        {
            this.serviceCollection = serviceCollection;
            this.func = func;
        }

        public void PerWebRequest()
        {
            serviceCollection.AddScoped((x) => func());
        }

        public void Singleton()
        {
            serviceCollection.AddSingleton((x) => func());
        }

        public void Transient()
        {
            serviceCollection.AddTransient((x) => func());
        }
    }
}