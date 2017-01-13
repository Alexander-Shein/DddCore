using Api.Cars.BLL;
using DddCore.Dal.DomainStack.EntityFramework.Mapping;

namespace Api.Cars.DAL
{
    public class CarsMappingModule : IMappingModule
    {

        public void Install(IModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wheel>();

            modelBuilder
                .Entity<Car>()
                .HasMany(x => x.Wheels)
                .WithOne()
                .HasForeignKey(x => x.CarId);
        }
    }
}