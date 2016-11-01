using Castle.MicroKernel.Registration;
using Contracts.Crosscutting.IoC;

namespace Crosscutting.Ioc.CastleWindsor
{
    public class WindsorComponent<TContract> : IComponent where TContract : class
    {
        public WindsorComponent(ComponentRegistration<TContract> componentRegistration)
        {
            ComponentRegistration = componentRegistration;
        }

        public ComponentRegistration<TContract> ComponentRegistration { get; }

        public ILifeStyle LifeStyle => new WindsorLifeStyle<TContract>(this);
    }
}