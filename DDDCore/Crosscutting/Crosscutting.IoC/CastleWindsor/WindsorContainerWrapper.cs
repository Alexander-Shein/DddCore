using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Contracts.Crosscutting.Ioc;

namespace Crosscutting.Ioc.CastleWindsor
{
    public class WindsorContainerWrapper : WindsorContainer, IContainerConfig
    {
        #region Public Methods

        public new T Resolve<T>() where T : class
        {
            return base.Resolve<T>();
        }

        public new T Resolve<T>(string name) where T : class
        {
            return base.Resolve<T>(name);
        }

        public new T[] ResolveAll<T>() where T : class
        {
            return base.ResolveAll<T>();
        }

        public IComponent Register<TContract, TImplementation>() where TImplementation : class, TContract where TContract : class
        {
            var component = Component.For<TContract, TImplementation>();
            return new WindsorComponent<TContract>(component, this);
        }

        public IComponent Register<TContract, TContract2, TImplementation>() where TContract : class where TContract2 : class where TImplementation : class, TContract, TContract2
        {
            var component = Component.For<TContract, TContract2>().ImplementedBy<TImplementation>();
            return new WindsorComponent<TContract>(component, this);
        }

        public IComponent Register<TContract>(Func<TContract> factoryMethod) where TContract : class
        {
            var component = Component.For<TContract>().UsingFactoryMethod(factoryMethod);
            return new WindsorComponent<TContract>(component, this);
        }

        #endregion
    }
}