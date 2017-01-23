using System;
using System.Collections.Generic;
using Api.Cars.SL.Contracts.Models;
using DddCore.Contracts.Services.Application;
using DddCore.Contracts.Services.Application.DomainStack.Crud;

namespace Api.Cars.SL.Contracts
{
    public interface ICarsWorkflowService : IWorkflowService, ICreateOrUpdate<CarVm, Guid, CarIm>, ICreate<CarVm, CarIm>
    {
        IEnumerable<CarVm> GetAllCars();
    }
}