﻿using System;
using System.Linq;
using System.Reflection;
using DddCore.BLL.Domain.Entities.BusinessRules;
using DddCore.BLL.Domain.Events;
using DddCore.Contracts.BLL.Domain.Entities;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Events;
using DddCore.Contracts.Crosscutting.DependencyInjection.Base;
using DddCore.Contracts.Crosscutting.UserContext;
using DddCore.Contracts.DAL.DomainStack;
using DddCore.Contracts.DAL.QueryStack;
using DddCore.Contracts.SL.Services.Application;
using DddCore.Contracts.SL.Services.Application.DomainStack;
using DddCore.Contracts.SL.Services.Infrastructure;
using DddCore.Crosscutting;
using DddCore.Crosscutting.UserContext;
using DddCore.DAL.DomainStack.EntityFramework;
using DddCore.DAL.DomainStack.EntityFramework.Context;
using DddCore.SL.Services.Application.DomainStack;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DddCore.SL.Services
{
    public class DddCoreDiModuleInstaller : IDiModuleInstaller
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
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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