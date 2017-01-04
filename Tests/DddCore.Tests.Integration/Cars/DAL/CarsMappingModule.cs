using DddCore.Dal.DomainStack.EntityFramework.Mapping;
using Microsoft.EntityFrameworkCore;
using DddCore.Tests.Integration.Cars.BLL;

namespace DddCore.Tests.Integration.Cars.DAL
{
    public class CarsMappingModule : IMappingModule
    {
        public void Install(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Wheel>()
                .Ignore(x => x.CrudState)
                .HasKey(x => x.Id)
                .ForSqlServerIsClustered(); ;

            var carEntityBuilder = modelBuilder.Entity<Car>();

            carEntityBuilder
                .HasMany(x => x.Wheels)
                .WithOne()
                .HasForeignKey(x => x.CarId);

            carEntityBuilder
                .Ignore(x => x.CrudState)
                .Property(x => x.Ts)
                .IsRowVersion();

            carEntityBuilder
                .HasKey(x => x.Id)
                .ForSqlServerIsClustered();
        }
    }
}