using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Contracts.Crosscutting.IoC;
using Crosscutting.Infrastructure.Configuration;

namespace Crosscutting.IoC.CastleWindsor
{
    public class WindsorBootstrapper
    {
        IWindsorContainer container;
        public IWindsorContainer Container
        {
            get { return container; }
            set
            {
                container = value;
                ContainerHolder.Container = value as IContainer;
            }
        }

        public WindsorBootstrapper Bootstrap()
        {
            return Bootstrap(AssemblyUtility.GetInstances<IWindsorInstaller>().ToArray());
        }

        public WindsorBootstrapper Bootstrap(params IWindsorInstaller[] installers)
        {
            Container = new WindsorContainerWrapper();

            Container.Install(installers);
            return this;
        }
    }
}
