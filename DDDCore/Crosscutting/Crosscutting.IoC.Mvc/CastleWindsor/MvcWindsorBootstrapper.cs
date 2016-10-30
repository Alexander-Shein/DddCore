using System.Web.Mvc;
using Crosscutting.IoC.CastleWindsor;

namespace Crosscutting.IoC.Mvc.CastleWindsor
{
    public static class WebApiWindsorBootstrapper
    {
        public static WindsorBootstrapper SetupMvc(this WindsorBootstrapper windsorBootstrapper)
        {
            var controllerFactory = new WindsorControllerFactory(windsorBootstrapper.Container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            return windsorBootstrapper;
        }
    }
}
