using Api.Cars.DAL.QueryStack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Cars.SL
{
    public class CarsWorkflowService : ICarsWorkflowService
    {
        readonly ICarsQueryRepository carsQueryRepository;

        public CarsWorkflowService(ICarsQueryRepository carsQueryRepository)
        {
            this.carsQueryRepository = carsQueryRepository;
        }

        public async Task<IEnumerable<CarVM>> GetAllCarsAsync()
        {
            var dtos = await carsQueryRepository.GetAllCarsAsync();

            var vms = dtos.Select(x => new CarVM
            {
                Color = x.Color,
                PublicKey = x.PublicKey,
                Wheels = x.Wheels.Select(c => new WheelVM
                {
                    Id = c.Id
                })
            });

            return vms;
        }
    }
}
