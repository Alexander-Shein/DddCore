using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Contracts.Crosscutting.Ioc;

namespace Crosscutting.Ioc.CastleWindsor
{
    public class WindsorComponent<TContract> : IComponent where TContract : class
    {
        #region Private Members

        readonly ComponentRegistration<TContract> component;
        readonly IWindsorContainer windsorContainer;

        #endregion

        public WindsorComponent(ComponentRegistration<TContract> component, IWindsorContainer windsorContainer)
        {
            this.component = component;
            this.windsorContainer = windsorContainer;
        }

        #region Public Methods

        public IComponent Named(string name)
        {
            component.Named(name);
            return this;
        }

        public ILifeStyle LifeStyle => new WindsorLifeStyle<TContract>(component, windsorContainer);

        #endregion
    }
}