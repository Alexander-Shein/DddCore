using DddCore.Contracts.Crosscutting.DependencyInjection.Base;
using DddCore.Contracts.Crosscutting.DependencyInjection;
using DddCore.Crosscutting;
using DddCore.Contracts.Domain.Entities.BusinessRules;
using DddCore.Contracts.Dal.DomainStack;
using System.Linq;
using DddCore.Contracts.Services.Application;
using DddCore.Contracts.Services.Infrastructure;
using DddCore.Contracts.Dal.QueryStack;
using DddCore.Dal.DomainStack.EntityFramework.Context;
using DddCore.Contracts.Domain.Events;
using DddCore.Domain.Events;
using DddCore.Domain.Entities.BusinessRules;
using System.Reflection;
using DddCore.Contracts.Domain.Entities;
using System;
using DddCore.Contracts.Services.Application.DomainStack;
using DddCore.Services.Application.DomainStack;
using DddCore.Dal.DomainStack.EntityFramework;
using DddCore.Contracts.Crosscutting.UserContext;
using DddCore.Crosscutting.UserContext;

namespace DddCore.Configuraion
{
    public class DddCoreDiModule : IDiModule
    {
        public void Install(IContainerConfig config)
        {
            SetupBusinessRulesValidators(config);
            SetupRepositories(config);
            SetupQueryRepositories(config);
            SetupEntityServices(config);
            SetupWorkflowServices(config);
            SetupInfrastructureServices(config);
            SetupDomainEventHandlers(config);

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

            config
                .Register<IUserContext<Guid>, IdentityUserContext>()
                .LifeStyle
                .PerWebRequest();
        }

        #region Private Members

        void SetupDomainEventHandlers(IContainerConfig config)
        {
            var allMessageTypes = AssemblyUtility.GetTypes<IDomainEvent>();
            if (!allMessageTypes.Any()) return;

            var openedHandlerContract = typeof(IDomainEventHandler<>);

            foreach (var messageType in allMessageTypes)
            {
                var closedHandlerContract = openedHandlerContract.MakeGenericType(messageType);

                var handlers = AssemblyUtility.GetTypes(closedHandlerContract);

                if (!handlers.Any()) continue;

                foreach (var t in handlers)
                {
                    config
                        .Register(closedHandlerContract, t)
                        .LifeStyle
                        .PerWebRequest();
                }
            }
        }

        void SetupInfrastructureServices(IContainerConfig config)
        {
            SetupForContract(config, typeof(IInfrastructureService));
        }

        void SetupWorkflowServices(IContainerConfig config)
        {
            SetupForContract(config, typeof(IWorkflowService));
        }

        void SetupEntityServices(IContainerConfig config)
        {
            SetupForEachAggregateRoot(config, typeof(IEntityService<,>), typeof(EntityService<,>));
        }

        void SetupQueryRepositories(IContainerConfig config)
        {
            SetupForContract(config, typeof(IQueryRepository));
        }

        void SetupBusinessRulesValidators(IContainerConfig config)
        {
            var type = typeof(IBusinessRulesValidator<>);

            foreach (var t in AssemblyUtility.GetTypes(type))
            {
                var contractType =
                    t
                        .GetInterfaces()
                        .FirstOrDefault(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == type);

                config
                    .Register(contractType, t)
                    .LifeStyle
                    .PerWebRequest();
            }
        }

        void SetupRepositories(IContainerConfig config)
        {
            SetupForEachAggregateRoot(config, typeof(IRepository<,>), typeof(Repository<,>));
        }

        void SetupForEachAggregateRoot(IContainerConfig config, Type contractType, Type serviceType)
        {
            var allAggregateRoots =
                AssemblyUtility
                    .GetTypes(typeof(IAggregateRootEntity<>))
                    .ToList();

            if (!allAggregateRoots.Any()) return;

            var aggregateRootType = typeof(IAggregateRootEntity<>);

            foreach (var d in allAggregateRoots)
            {
                var keyType =
                    d
                        .GetInterfaces()
                        .First(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == aggregateRootType)
                        .GetGenericArguments()
                        .First();

                var closedContractType = contractType.MakeGenericType(d, keyType);

                var specificType = AssemblyUtility.GetTypes(closedContractType).FirstOrDefault();

                if (specificType == null)
                {
                    var genericType = serviceType.MakeGenericType(closedContractType.GetGenericArguments());

                    config
                        .Register(closedContractType, genericType)
                        .LifeStyle
                        .PerWebRequest();

                    continue;
                }
                else
                {
                    config
                        .Register(closedContractType, specificType)
                        .LifeStyle
                        .PerWebRequest();

                    foreach (var specificContractType in specificType.GetInterfaces().Where(x => x != closedContractType))
                    {
                        config
                            .Register(specificContractType, specificType)
                            .LifeStyle
                            .PerWebRequest();
                    }
                }
            }
        }

        void SetupForContract(IContainerConfig config, Type contractType)
        {
            foreach (var t in AssemblyUtility.GetTypes(contractType))
            {
                foreach (var ct in t.GetInterfaces().Where(c => c != contractType))
                {
                    config
                        .Register(ct, t)
                        .LifeStyle
                        .PerWebRequest();
                }
            }
        }

        #endregion
    }
}