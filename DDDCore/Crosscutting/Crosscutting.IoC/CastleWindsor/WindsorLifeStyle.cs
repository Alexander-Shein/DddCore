using Contracts.Crosscutting.IoC;

namespace Crosscutting.Ioc.CastleWindsor
{
    public class WindsorLifeStyle<TContract> : ILifeStyle where TContract : class
    {
        readonly WindsorComponent<TContract> windsorComponent;

        public WindsorLifeStyle(WindsorComponent<TContract> windsorComponent)
        {
            this.windsorComponent = windsorComponent;
        }

        public void Transient()
        {
            var component = windsorComponent.ComponentRegistration.LifeStyle.Transient;
        }

        public void PerWebRequest()
        {
            var component = windsorComponent.ComponentRegistration.LifeStyle.PerWebRequest;
        }

        public void Singleton()
        {
            var component = windsorComponent.ComponentRegistration.LifeStyle.Singleton;
        }
    }
}