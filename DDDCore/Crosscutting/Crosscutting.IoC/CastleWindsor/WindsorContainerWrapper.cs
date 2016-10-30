using Castle.Windsor;
using Contracts.Crosscutting.IoC;

namespace Crosscutting.IoC.CastleWindsor
{
    public class WindsorContainerWrapper : WindsorContainer, IContainer
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
    }
}