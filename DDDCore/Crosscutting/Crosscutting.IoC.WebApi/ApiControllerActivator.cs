using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Contracts.Crosscutting.Ioc;

namespace Crosscutting.IoC.WebApi
{
    public class ApiControllerActivator : IHttpControllerActivator
    {
        readonly IContainer container;

        public ApiControllerActivator(IContainer container)
        {
            this.container = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = (IHttpController)container.Resolve(controllerType);
            return controller;
        }
    }
}