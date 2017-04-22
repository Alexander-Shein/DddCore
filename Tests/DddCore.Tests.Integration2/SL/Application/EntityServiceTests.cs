﻿using DddCore.Contracts.Crosscutting.UserContext;
using DddCore.Contracts.Dal;
using DddCore.Contracts.Domain.Entities.BusinessRules;
using DddCore.Contracts.Domain.Entities.Model;
using DddCore.Contracts.Domain.Events;
using DddCore.Dal.DomainStack.EntityFramework;
using DddCore.Dal.DomainStack.EntityFramework.Context;
using DddCore.Domain.Events;
using DddCore.Services.Application.DomainStack;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using Api.Cars.BLL;
using Xunit;

namespace DddCore.Tests.Integration.SL.Application
{
    public class EntityServiceTests
    {
        #region private Members

        readonly ConnectionStrings connectonStrings = new ConnectionStrings
        {
            Oltp = @"Data Source=(local); Initial Catalog=DddCore.Samples; Integrated Security=SSPI;",
            ReadOnly = @"Data Source=(local); Initial Catalog=DddCore.Samples; Integrated Security=SSPI;"
        };

        #endregion

        [Fact]
        public async void PersistAggregateRootAsynct()
        {
            var optionsMock = new Mock<IOptions<ConnectionStrings>>();
            optionsMock.Setup(x => x.Value).Returns(connectonStrings);

            var userContextMock = new Mock<IUserContext<Guid>>();
            var dataContext = new DataContext(optionsMock.Object);
            var repository = new Repository<Car, Guid>(dataContext, userContextMock.Object);
            var businessRulesValidatorFactoryMock = new Mock<IBusinessRulesValidatorFactory>();
            businessRulesValidatorFactoryMock.Setup(x => x.GetBusinessRulesValidator<Car>()).Returns(new CarBusinessRulesValidator());

            var domainEventHandlerFactoryMock = new Mock<IDomainEventHandlerFactory>();
            var domainEventDispather = new DomainEventDispatcher(domainEventHandlerFactoryMock.Object);

            var guard = new Guard(businessRulesValidatorFactoryMock.Object);
            var entityService = new EntityService<Car, Guid>(repository, guard, domainEventDispather);

            var car = new Car
            {
                Color = "Red",
                CrudState = CrudState.Added
            };

            car.Wheels.Add(new Wheel
            {
                CarId = car.Id,
                CrudState = CrudState.Added
            });

            await entityService.PersistAggregateRootAsync(car);

            dataContext.Save();
        }

        [Fact]
        public async void PersistAggregateRootAsynct_DomainEventHandlers()
        {
            var optionsMock = new Mock<IOptions<ConnectionStrings>>();
            optionsMock.Setup(x => x.Value).Returns(connectonStrings);

            var userContextMock = new Mock<IUserContext<Guid>>();
            var dataContext = new DataContext(optionsMock.Object);
            var repository = new Repository<Car, Guid>(dataContext, userContextMock.Object);
            var businessRulesValidatorFactoryMock = new Mock<IBusinessRulesValidatorFactory>();
            businessRulesValidatorFactoryMock.Setup(x => x.GetBusinessRulesValidator<Car>()).Returns(new CarBusinessRulesValidator());

            var domainEventHandlerFactoryMock = new Mock<IDomainEventHandlerFactory>();
            domainEventHandlerFactoryMock.Setup(x => x.GetHandlers<ColorChangedDomainEvent>()).Returns(new List<IDomainEventHandler<ColorChangedDomainEvent>>
            {
                new UpdateColorHandler()
            });

            var domainEventDispather = new DomainEventDispatcher(domainEventHandlerFactoryMock.Object);

            var guard = new Guard(businessRulesValidatorFactoryMock.Object);
            var entityService = new EntityService<Car, Guid>(repository, guard, domainEventDispather);

            var car = new Car
            {
                Color = "Red",
                CrudState = CrudState.Added
            };

            car.Wheels.Add(new Wheel
            {
                CarId = car.Id,
                CrudState = CrudState.Added
            });

            car.Events.Add(new ColorChangedDomainEvent(car));

            await entityService.PersistAggregateRootAsync(car);

            dataContext.Save();
        }
    }
}