using Api.Cars.DAL.QueryStack.Dtos;
using DddCore.Contracts.Dal.QueryStack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Cars.DAL.QueryStack
{
    public interface ICarsQueryRepository : IQueryRepository
    {
        Task<IEnumerable<CarVmDto>> GetAllCarsAsync();
    }
}
