using DddCore.Contracts.Crosscutting.DependencyInjection.Base;
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
using Microsoft.Extensions.DependencyInjection;

namespace DddCore.Configuraion
{
    public class DddCoreDiModule : IDiModule
    {
        public void Install(IServiceCollection serviceCollection)
        {
            SetupBusinessRulesValidators(serviceCollection);
            SetupRepositories(serviceCollection);
            SetupQueryRepositories(serviceCollection);
            SetupEntityServices(serviceCollection);
            SetupWorkflowServices(serviceCollection);
            SetupInfrastructureServices(serviceCollection);
            SetupDomainEventHandlers(serviceCollection);

            serviceCollection.AddScoped<IUnitOfWork, DataContext>();
            serviceCollection.AddScoped<IDataContext, DataContext>();
            serviceCollection.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            serviceCollection.AddScoped<IDomainEventHandlerFactory, DomainEventHandlerFactory>();
            serviceCollection.AddScoped<IBusinessRulesValidatorFactory, BusinessRulesValidatorFactory>();
            serviceCollection.AddScoped<IUserContext<Guid>, IdentityUserContext>();
        }

        #region Private Members

        void SetupDomainEventHandlers(IServiceCollection serviceCollection)
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
                    serviceCollection.AddScoped(closedHandlerContract, t);
                }
            }
        }

        void SetupInfrastructureServices(IServiceCollection serviceCollection)
        {
            SetupForContract(serviceCollection, typeof(IInfrastructureService));
        }

        void SetupWorkflowServices(IServiceCollection serviceCollection)
        {
            SetupForContract(serviceCollection, typeof(IWorkflowService));
        }

        void SetupEntityServices(IServiceCollection serviceCollection)
        {
            SetupForEachAggregateRoot(serviceCollection, typeof(IEntityService<,>), typeof(EntityService<,>));
        }

        void SetupQueryRepositories(IServiceCollection serviceCollection)
        {
            SetupForContract(serviceCollection, typeof(IQueryRepository));
        }

        void SetupBusinessRulesValidators(IServiceCollection serviceCollection)
        {
            var type = typeof(IBusinessRulesValidator<>);

            foreach (var t in AssemblyUtility.GetTypes(type))
            {
                var contractType =
                    t
                        .GetInterfaces()
                        .FirstOrDefault(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == type);

                serviceCollection.AddSingleton(contractType, t); // https://github.com/JeremySkinner/FluentValidation/wiki/b.-Creating-a-Validator#a-note-on-performance
            }
        }

        void SetupRepositories(IServiceCollection serviceCollection)
        {
            SetupForEachAggregateRoot(serviceCollection, typeof(IRepository<,>), typeof(Repository<,>));
        }

        void SetupForEachAggregateRoot(IServiceCollection serviceCollection, Type contractType, Type serviceType)
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

                    serviceCollection.AddScoped(closedContractType, genericType);

                    continue;
                }
                else
                {
                    serviceCollection.AddScoped(closedContractType, specificType);

                    foreach (var specificContractType in specificType.GetInterfaces().Where(x => x != closedContractType))
                    {
                        serviceCollection.AddScoped(specificContractType, specificType);
                    }
                }
            }
        }

        void SetupForContract(IServiceCollection serviceCollection, Type contractType)
        {
            foreach (var t in AssemblyUtility.GetTypes(contractType))
            {
                foreach (var ct in t.GetInterfaces().Where(c => c != contractType))
                {
                    serviceCollection.AddScoped(ct, t);
                }
            }
        }

        #endregion
    }
}