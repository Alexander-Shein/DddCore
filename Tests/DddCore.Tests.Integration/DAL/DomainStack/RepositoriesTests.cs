﻿//using DddCore.Dal.DomainStack.EntityFramework;
//using DddCore.Dal.DomainStack.EntityFramework.Context;
//using System;
//using Api.Cars.BLL;
//using Xunit;
//using Moq;
//using Microsoft.Extensions.Options;
//using DddCore.Contracts.Dal;
//using DddCore.Contracts.Crosscutting.UserContext;
//using DddCore.Contracts.Domain.Entities.Model;

//namespace DddCore.Tests.Integration.DAL.DomainStack
//{
//    public class RepositoriesTests
//    {
//        [Fact]
//        public void Insert()
//        {
//            var connectonStrings = new ConnectionStrings
//            {
//                Oltp = "Data Source=(local); Initial Catalog=DddCore.Tests.Integration.Database; Integrated Security=SSPI;",
//                ReadOnly = "Data Source=(local); Initial Catalog=DddCore.Tests.Integration.Database; Integrated Security=SSPI;"
//            };

//            var optionsMock = new Mock<IOptions<ConnectionStrings>>();
//            optionsMock.Setup(x => x.Value).Returns(connectonStrings);

//            var userContextMock = new Mock<IUserContext<Guid>>();
//            var dataContext = new DataContext(optionsMock.Object);
//            var repository = new Repository<Car, Guid>(dataContext, userContextMock.Object);

//            var car = new Car
//            {
//                Color = "Red",
//                CrudState = CrudState.Added
//            };

//            car.Wheels.Add(new Wheel
//            {
//                CarId = car.Id,
//                CrudState = CrudState.Added
//            });

//            repository.PersistAggregateRoot(car);

//            dataContext.Save();
//        }

//        [Fact]
//        public async void Read()
//        {
//            var connectonStrings = new ConnectionStrings
//            {
//                Oltp = "Data Source=(local); Initial Catalog=DddCore.Tests.Integration.Database; Integrated Security=SSPI;",
//                ReadOnly = "Data Source=(local); Initial Catalog=DddCore.Tests.Integration.Database; Integrated Security=SSPI;"
//            };

//            var optionsMock = new Mock<IOptions<ConnectionStrings>>();
//            optionsMock.Setup(x => x.Value).Returns(connectonStrings);

//            var userContextMock = new Mock<IUserContext<Guid>>();
//            var dataContext = new DataContext(optionsMock.Object);
//            var repository = new Repository<Car, Guid>(dataContext, userContextMock.Object);

//            var car = await repository.ReadAggregateRootAsync(new Guid("17FAF315-7BB4-401C-8683-1616AA08DE67"));
//        }
//    }
//}