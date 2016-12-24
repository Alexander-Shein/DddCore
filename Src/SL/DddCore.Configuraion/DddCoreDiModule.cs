using DddCore.Contracts.Crosscutting.DependencyInjection.Base;
using DddCore.Contracts.Crosscutting.DependencyInjection;
using DddCore.Crosscutting;
using DddCore.Contracts.Domain.Entities.BusinessRules;
using DddCore.Contracts.Dal.DomainStack;
using System.Linq;
using DddCore.Contracts.Services.Application.DomainStack;
using DddCore.Contracts.Services.Application;
using DddCore.Contracts.Services.Infrastructure;
using DddCore.Contracts.Dal.QueryStack;
using DddCore.Dal.DomainStack.EntityFramework.Context;
using DddCore.Contracts.Domain.Events;
using DddCore.Domain.Events;
using DddCore.Domain.Entities.BusinessRules;

namespace DddCore.Configuraion
{
    public class DddCoreDiModule : IDiModule
    {
        public void Install(IContainerConfig config)
        {
            var typesToRegister =
                AssemblyUtility.GetInterfaceAndInstanceTypes(typeof(IBusinessRulesValidator<>))
                .Concat(AssemblyUtility.GetInterfaceAndInstanceTypes(typeof(IRepository<,>)))
                .Concat(AssemblyUtility.GetInterfaceAndInstanceTypes<IQueryRepository>())
                .Concat(AssemblyUtility.GetInterfaceAndInstanceTypes(typeof(IEntityService<,>)))
                .Concat(AssemblyUtility.GetInterfaceAndInstanceTypes<IWorkflowService>())
                .Concat(AssemblyUtility.GetInterfaceAndInstanceTypes<IInfrastructureService>())
                .Concat(AssemblyUtility.GetInterfaceAndInstanceTypes(typeof(IDomainEventHandler<>)));

            // Register event handlers

            foreach (var types in typesToRegister)
            {
                config
                    .Register(types.Item1, types.Item2)
                    .LifeStyle
                    .PerWebRequest();
            }

            config
                .Register<IUnitOfWork, DataContext>()
                .LifeStyle
                .PerWebRequest();

            config
                .Register<IDataContext, DataContext>()
                .LifeStyle
                .PerWebRequest();

            config
                .Register<IDomainEventDispatcher, DomainEventDispatcher>()
                .LifeStyle
                .PerWebRequest();

            config
                .Register<IDomainEventHandlerFactory, DomainEventHandlerFactory>()
                .LifeStyle
                .PerWebRequest();

            config
                .Register<IBusinessRulesValidatorFactory, BusinessRulesValidatorFactory>()
                .LifeStyle
                .PerWebRequest();
        }
    }
}