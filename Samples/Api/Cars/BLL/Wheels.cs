using System;
using DddCore.Domain.Entities.GuidEntities;

namespace Api.Cars.BLL
{
    public class Wheel : GuidEntityBase
    {
        public Guid CarId { get; set; }
    }
}
