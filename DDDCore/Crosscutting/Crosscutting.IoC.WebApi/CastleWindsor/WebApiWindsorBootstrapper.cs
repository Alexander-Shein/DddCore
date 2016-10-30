using System.Web.Http;
using System.Web.Http.Dispatcher;
using Crosscutting.IoC.CastleWindsor;

namespace Crosscutting.IoC.WebApi.CastleWindsor
{
    public static class WebApiWindsorBootstrapper
    {
        public static WindsorBootstrapper SetupWebApi(this WindsorBootstrapper windsorBootstrapper, HttpConfiguration configuration)
        {
            configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                new ApiControllerActivator(windsorBootstrapper.Container));

            return windsorBootstrapper;
        }
    }
}
