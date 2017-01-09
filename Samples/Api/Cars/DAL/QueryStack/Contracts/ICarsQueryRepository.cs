using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Cars.DAL.QueryStack.Contracts.Dtos;
using DddCore.Contracts.Dal.QueryStack;

namespace Api.Cars.DAL.QueryStack.Contracts
{
    public interface ICarsQueryRepository : IQueryRepository
    {
        Task<IEnumerable<CarVmDto>> GetAllCarsAsync();
    }
}
