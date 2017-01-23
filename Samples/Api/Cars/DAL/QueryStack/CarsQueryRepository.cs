using System.Collections.Generic;
using DddCore.Contracts.Dal;
using DddCore.Dal.QueryStack.Dapper;
using Microsoft.Extensions.Options;
using Api.Cars.DAL.QueryStack.Contracts;
using Api.Cars.DAL.QueryStack.Contracts.Dtos;

namespace Api.Cars.DAL.QueryStack
{
    public class CarsQueryRepository : QueryRepositoryBase, ICarsQueryRepository
    {
        public CarsQueryRepository(IOptions<ConnectionStrings> connectionStrings) : base(connectionStrings)
        {
        }

        public IEnumerable<CarVmDto> GetAllCars()
        {
            var sql = "SELECT * FROM [dbo].[Car];";

            var dtos = GetFilteredList<CarVmDto>(sql);
            return dtos;
        }
    }
}
