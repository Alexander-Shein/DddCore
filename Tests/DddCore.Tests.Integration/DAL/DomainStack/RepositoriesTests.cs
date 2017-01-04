using DddCore.Dal.DomainStack.EntityFramework;
using DddCore.Dal.DomainStack.EntityFramework.Context;
using DddCore.Tests.Integration.Cars.BLL;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Options;
using DddCore.Contracts.Dal;
using DddCore.Contracts.Crosscutting.UserContext;
using DddCore.Contracts.Domain.Entities.Model;

namespace DddCore.Tests.Integration.DAL.DomainStack
{
    public class RepositoriesTests
    {
        [Fact]
        public void Insert()
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

            repository.PersistAggregateRoot(car);

            dataContext.Save();
        }
    }
}