using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DddCore.Domain.Entities.GuidEntities;
using Api.Cars.BLL;

namespace Api.People
{
    public class Person : GuidAggregateRootEntityBase
    {
        public ICollection<Car> Cars { get; set; }
    }
}