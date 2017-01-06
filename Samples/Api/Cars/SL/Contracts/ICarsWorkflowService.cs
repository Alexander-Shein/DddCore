using DddCore.Contracts.Services.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Cars.SL
{
    public interface ICarsWorkflowService : IWorkflowService
    {
        Task<IEnumerable<CarVM>> GetAllCarsAsync();
    }
}