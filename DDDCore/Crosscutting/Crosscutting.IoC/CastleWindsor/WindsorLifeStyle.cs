using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Contracts.Crosscutting.Ioc;

namespace Crosscutting.Ioc.CastleWindsor
{
    public class WindsorLifeStyle<TContract> : ILifeStyle where TContract : class
    {
        #region Private Members

        readonly ComponentRegistration<TContract> component;
        readonly IWindsorContainer windsorContainer;

        #endregion

        public WindsorLifeStyle(ComponentRegistration<TContract> component, IWindsorContainer windsorContainer)
        {
            this.component = component;
            this.windsorContainer = windsorContainer;
        }

        #region Public Methods

        public void Transient()
        {
            windsorContainer.Register(component.LifeStyle.Transient);
        }

        public void PerWebRequest()
        {
            windsorContainer.Register(component.LifeStyle.PerWebRequest);
        }

        public void Singleton()
        {
            windsorContainer.Register(component.LifeStyle.Singleton);
        }

        #endregion
    }
}