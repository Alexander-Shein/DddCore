using NUnit.Framework;

namespace DddCore.Tests.Integration.DAL.QueryStack
{
    [TestFixture()]
    public class QueryRepositoriesTests
    {
        [Test]
        public void Read()
        {
            //var connectonStrings = new ConnectionStrings
            //{
            //    Oltp = "Data Source=(local); Initial Catalog=DddCore.Tests.Integration.Database; Integrated Security=SSPI;",
            //    ReadOnly = "Data Source=(local); Initial Catalog=DddCore.Tests.Integration.Database; Integrated Security=SSPI;"
            //};

            //var optionsMock = new Mock<IOptions<ConnectionStrings>>();
            //optionsMock.Setup(x => x.Value).Returns(connectonStrings);
            //var queryRepository = new CarsQueryRepository(optionsMock.Object);

            //var cars = queryRepository.GetAllCars();


            //repository.PersistAggregateRoot(car);
            //
            //dataContext.Save();
        }
    }
}
