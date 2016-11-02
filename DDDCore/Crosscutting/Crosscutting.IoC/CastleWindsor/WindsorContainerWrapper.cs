using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Contracts.Crosscutting.IoC;

namespace Crosscutting.Ioc.CastleWindsor
{
    public class WindsorContainerWrapper : WindsorContainer, IContainerConfig
    {
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
            Register(component);

            return new WindsorComponent<TContract>(component);
        }
    }
}