using System;
using DddCore.Domain.Entities.GuidEntities;

namespace DddCore.Tests.Integration.Cars.BLL
{
    public class Wheel : GuidEntityBase
    {
        public Guid CarId { get; set; }
    }
}
