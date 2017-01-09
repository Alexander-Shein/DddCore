using System;
using System.Collections.Generic;
using System.Linq;
using DddCore.Configuraion;
using DddCore.Contracts.Crosscutting.DependencyInjection;
using DddCore.Contracts.Crosscutting.UserContext;
using DddCore.Contracts.Dal;
using DddCore.Contracts.Dal.DomainStack;
using DddCore.Contracts.Dal.QueryStack;
using DddCore.Contracts.Domain.Events;
using DddCore.Contracts.Services.Application.DomainStack;
using DddCore.Contracts.Services.Infrastructure;
using DddCore.Crosscutting.DependencyInjection.Microsoft;
using DddCore.Dal.DomainStack.EntityFramework;
using DddCore.Dal.DomainStack.EntityFramework.Context;
using DddCore.Domain.Entities;
using DddCore.Services.Application.DomainStack;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace DddCore.Tests.Unit.SL.Configuration
{
    public class DddCoreDiModuleTests
    {
        [Fact]
        public void RegisterDddCoreComponents()
        {
            var serviceCollection = new ServiceCollection();
            var containerConfig = new ContainerConfig(serviceCollection);
            var module = new DddCoreDiModule();

            module.Install(containerConfig);

            serviceCollection.Count.Should().Be(18);
        }

        [Fact]
        public void RegisterRepositories()
        {
            // Act
            var containerConfig = SetupContainerConfig();
            InstallDddCoreDiModule(containerConfig);
            var container = containerConfig.BuildContainer();

            // Assert
            CheckInContainer<IRepository<AggregateRootOne, Guid>>(container, typeof(AggregateRootOneRepository));
            CheckInContainer<IAggregateRootOneRepository>(container, typeof(AggregateRootOneRepository));
            CheckInContainer<IRepository<AggregateRootTwo, Guid>>(container, typeof(Repository<AggregateRootTwo, Guid>));
        }

        [Fact]
        public void RegisterEntityServices()
        {
            // Act
            var containerConfig = SetupContainerConfig();
            InstallDddCoreDiModule(containerConfig);
            var container = containerConfig.BuildContainer();

            // Assert
            CheckInContainer<IEntityService<AggregateRootTwo, Guid>>(container, typeof(AggregateRootTwoEntityService));
            CheckInContainer<IAggregateRootTwoEntityService>(container, typeof(AggregateRootTwoEntityService));
            CheckInContainer<IEntityService<AggregateRootOne, Guid>>(container, typeof(EntityService<AggregateRootOne, Guid>));
        }

        [Fact]
        public void RegisterQueryRepositories()
        {
            // Act
            var containerConfig = SetupContainerConfig();
            InstallDddCoreDiModule(containerConfig);
            var container = containerConfig.BuildContainer();

            // Assert
            CheckInContainer<IAggregateRootTwoQueryRepository>(container, typeof(AggregateRootTwoQueryRepository));
        }

        [Fact]
        public void RegisterDomainEventHandlers()
        {
            // Act
            var containerConfig = SetupContainerConfig();
            InstallDddCoreDiModule(containerConfig);
            var container = containerConfig.BuildContainer();

            // Assert
            var handlers = container.GetService<IEnumerable<IDomainEventHandler<TestDomainEvent>>>();
            handlers.Count().Should().Be(2);
        }

        #region Private Methods

        void CheckInContainer<T>(IServiceProvider container, Type expectedType)
        {
            var aggregateRootOneRepository = container.GetServices<T>();

            aggregateRootOneRepository.Should().NotBeNull();
            aggregateRootOneRepository.Should().AllBeOfType(expectedType);
        }

        void InstallDddCoreDiModule(IContainerConfig containerConfig)
        {
            var module = new DddCoreDiModule();
            module.Install(containerConfig);
        }

        IContainerConfig SetupContainerConfig()
        {
            var serviceCollection = new ServiceCollection();
            var containerConfig = new ContainerConfig(serviceCollection);

            containerConfig.Register<IOptions<ConnectionStrings>, ConnectionStringOptions>().LifeStyle.PerWebRequest();
            containerConfig.Register<IHttpContextAccessor, HttpContextAccessor>().LifeStyle.PerWebRequest();

            return containerConfig;
        }

        #endregion
    }

    public class HttpContextAccessor : IHttpContextAccessor
    {
        public HttpContext HttpContext { get; set; }
    }

    public class ConnectionStringOptions : IOptions<ConnectionStrings>
    {
        public ConnectionStrings Value => new ConnectionStrings();
    }

    public class AggregateRootOne : AggregateRootEntityBase<Guid>
    {
        public string Property { get; set; }
    }

    public class AggregateRootTwo : AggregateRootEntityBase<Guid>
    {
        public string Property { get; set; }
    }

    public interface IAggregateRootOneRepository : IRepository<AggregateRootOne, Guid>
    {
    }

    public class AggregateRootOneRepository : Repository<AggregateRootOne, Guid>, IAggregateRootOneRepository
    {
        public AggregateRootOneRepository(IDataContext dataContext, IUserContext<Guid> userContext) : base(dataContext, userContext)
        {
        }
    }

    public interface IAggregateRootTwoEntityService : IEntityService<AggregateRootTwo, Guid>
    {
    }

    public class AggregateRootTwoEntityService : EntityService<AggregateRootTwo, Guid>, IAggregateRootTwoEntityService
    {
        public AggregateRootTwoEntityService(IRepository<AggregateRootTwo, Guid> repository, IGuard guard, IDomainEventDispatcher domainEventDispatcher) : base(repository, guard, domainEventDispatcher)
        {
        }
    }

    public interface IAggregateRootTwoQueryRepository : IQueryRepository
    { }

    public class AggregateRootTwoQueryRepository : IAggregateRootTwoQueryRepository
    {
    }

    public interface IOneInfrustructureService : IInfrastructureService
    { }

    public class OneInfrustructureService : IOneInfrustructureService
    {
    }

    public class TestDomainEvent : IDomainEvent
    {
        public DateTime CreatedAt { get; set; }
    }

    public class TestHandlerOne : IDomainEventHandler<TestDomainEvent>
    {
        public void Handle(TestDomainEvent args) {}
    }

    public class TestHandlerTwo : IDomainEventHandler<TestDomainEvent>
    {
        public void Handle(TestDomainEvent args) { }
    }
}