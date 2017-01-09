using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Cars.SL.Contracts.Models;
using DddCore.Contracts.Services.Application;

namespace Api.Cars.SL.Contracts
{
    public interface ICarsWorkflowService : IWorkflowService
    {
        Task<IEnumerable<CarVM>> GetAllCarsAsync();
    }
}