using DddCore.Domain.Entities.GuidEntities;
using System;
using System.Collections.Generic;

namespace DddCore.Tests.Integration.Cars.BLL
{
    public class Car : GuidAggregateRootEntityBase
    { 
        public string Color { get; set; }
        public ICollection<Wheel> Wheels { get; set; } = new List<Wheel>();
    }
}