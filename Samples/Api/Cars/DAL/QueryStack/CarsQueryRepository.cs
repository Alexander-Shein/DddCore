using System.Collections.Generic;
using DddCore.Contracts.Dal;
using DddCore.Dal.QueryStack.Dapper;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Api.Cars.DAL.QueryStack.Contracts;
using Api.Cars.DAL.QueryStack.Contracts.Dtos;

namespace Api.Cars.DAL.QueryStack
{
    public class CarsQueryRepository : QueryRepositoryBase, ICarsQueryRepository
    {
        public CarsQueryRepository(IOptions<ConnectionStrings> connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<CarVmDto>> GetAllCarsAsync()
        {
            var sql = "SELECT * FROM [dbo].[Car];";

            var dtos = await GetFilteredListAsync<CarVmDto>(sql);
            return dtos;
        }
    }
}
