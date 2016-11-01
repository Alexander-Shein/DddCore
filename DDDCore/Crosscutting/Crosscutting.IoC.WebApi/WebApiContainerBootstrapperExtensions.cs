using System.Web.Http;
using System.Web.Http.Dispatcher;
using Crosscutting.Ioc;

namespace Crosscutting.IoC.WebApi
{
    public static class WebApiContainerBootstrapperExtensions
    {
        public static ContainerBootstrapper SetupWebApi(this ContainerBootstrapper iocBootstrapper, HttpConfiguration configuration)
        {
            configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                new ApiControllerActivator(ContainerHolder.Container));

            return iocBootstrapper;
        }
    }
}
