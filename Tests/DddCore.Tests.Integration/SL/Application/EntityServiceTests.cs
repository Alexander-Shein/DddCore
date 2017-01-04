using DddCore.Contracts.Crosscutting.UserContext;
using DddCore.Contracts.Dal;
using DddCore.Contracts.Domain.Entities.Model;
using DddCore.Dal.DomainStack.EntityFramework;
using DddCore.Dal.DomainStack.EntityFramework.Context;
using DddCore.Services.Application.DomainStack;
using DddCore.Tests.Integration.Cars.BLL;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DddCore.Tests.Integration.SL.Application
{
    public class EntityServiceTests
    {
        [Fact]
        public void Persist()
        {
            var connectonStrings = new ConnectionStrings
            {
                Oltp = "Data Source=(local); Initial Catalog=DddCore.Tests.Integration.Database; Integrated Security=SSPI;",
                ReadOnly = "Data Source=(local); Initial Catalog=DddCore.Tests.Integration.Database; Integrated Security=SSPI;"
            };

            var optionsMock = new Mock<IOptions<ConnectionStrings>>();
            optionsMock.Setup(x => x.Value).Returns(connectonStrings);

            var userContextMock = new Mock<IUserContext<Guid>>();
            var dataContext = new DataContext(optionsMock.Object);
            var repository = new Repository<Car, Guid>(dataContext, userContextMock.Object);
            var guard = new Guard(null);
            var entityService = new EntityService<Car, Guid>(repository, guard, null);

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
        }
    }
}