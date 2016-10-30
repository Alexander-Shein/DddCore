using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Castle.Windsor;

namespace Crosscutting.IoC.WebApi.CastleWindsor
{
    public class ApiControllerActivator : IHttpControllerActivator
    {
        readonly IWindsorContainer container;

        public ApiControllerActivator(IWindsorContainer container)
        {
            this.container = container;
        }

        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            var controller =
                (IHttpController)this.container.Resolve(controllerType);
            return controller;
        }
    }
}