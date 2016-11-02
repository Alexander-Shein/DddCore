using System.Web.Http.Filters;
using Contracts.Crosscutting.Logging;
using Crosscutting.Ioc;

namespace Presentation.WebApi.Filters
{
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var logger =
                ContainerHolder
                    .Container
                    .Resolve<ILoggerFactory>()
                    .GetLogger(typeof(ExceptionHandlingAttribute).Name);

            logger.LogFatal(context.Exception.Message, context.Exception);
            base.OnException(context);
        }
    }
}
