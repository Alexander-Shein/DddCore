using System;
using DddCore.Contracts.Crosscutting.Base;
using Microsoft.Extensions.DependencyInjection;

namespace DddCore.Contracts.Crosscutting.DependencyInjection.Base
{
    public interface IDiBootstrapper : IBootstrapper<IServiceProvider, IServiceCollection, IDiModule>
    {
    }
}