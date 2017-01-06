using Api.Cars.DAL.QueryStack;
using DddCore.Contracts.Dal;
using DddCore.Contracts.Domain.Entities.Model;
using DddCore.Tests.Integration.Cars.BLL;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace DddCore.Tests.Integration.DAL.QueryStack
{
    public class QueryRepositoriesTests
    {
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
            var queryRepository = new CarsQueryRepository(optionsMock.Object);

            var cars = await queryRepository.GetAllCarsAsync();


            //repository.PersistAggregateRoot(car);
            //
            //dataContext.Save();
        }
    }
}
