using System;
using Api.Cars.DAL.QueryStack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Cars.BLL;
using Api.Cars.DAL.QueryStack.Contracts;
using Api.Cars.SL.Contracts;
using Api.Cars.SL.Contracts.Models;
using DddCore.Contracts.Dal.DomainStack;
using DddCore.Contracts.Domain.Entities.Model;
using DddCore.Contracts.Services.Application.DomainStack;

namespace Api.Cars.SL
{
    public class CarsWorkflowService : ICarsWorkflowService
    {
        readonly ICarsQueryRepository carsQueryRepository;
        readonly IRepository<Car, Guid> carsRepository;
        readonly IEntityService<Car, Guid> carsEntityService;
        readonly IUnitOfWork unitOfWork;

        public CarsWorkflowService(ICarsQueryRepository carsQueryRepository, IRepository<Car, Guid> carsRepository, IEntityService<Car, Guid> carsEntityService, IUnitOfWork unitOfWork)
        {
            this.carsQueryRepository = carsQueryRepository;
            this.carsRepository = carsRepository;
            this.carsEntityService = carsEntityService;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<CarVm> GetAllCars()
        {
            var dtos = carsQueryRepository.GetAllCars();

            var vms = dtos.Select(x => new CarVm
            {
                Color = x.Color,
                Wheels = x.Wheels?.Select(c => new WheelVm
                {
                    Id = c.Id
                })
            });

            return vms;
        }

        public CarVm Update(Guid key, CarIm im)
        {
            var car = carsRepository.ReadAggregateRoot(key);

            car.ChangeColor(im.Color);
            car.CrudState = CrudState.Modified;

            carsEntityService.PersistAggregateRoot(car);
            unitOfWork.Save();

            return new CarVm
            {
                Id = car.Id,
                Color = car.Color
            };
        }
    }
}
