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

        [Fact]
        public async void Read()
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

            var id = new Guid("40D718F0-F7CA-48F7-A683-FDBE90D124FB");

            var car = await repository.ReadAggregateRootAsync(new Guid("40D718F0-F7CA-48F7-A683-FDBE90D124FB"));
        }
    }
}