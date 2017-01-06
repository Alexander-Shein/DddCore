using System;
using System.Collections.Generic;
using Api.Cars.DAL.QueryStack.Dtos;
using DddCore.Contracts.Dal;
using DddCore.Dal.QueryStack.Dapper;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

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

            //var dtos = await GetFilteredListAsync<CarVmDto>(sql);

            //return dtos;
            
            return new List<CarVmDto>
            {
                new CarVmDto
                {
                    Color = "Red",
                    PublicKey = "PublicKey",
                    Wheels = new List<WheelVmDto>
                    {
                        new WheelVmDto
                        {
                            Id = Guid.NewGuid()
                        },

                        new WheelVmDto
                        {
                            Id = Guid.NewGuid()
                        }
                    }
                }
            };
        }
    }
}
