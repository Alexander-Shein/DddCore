using DddCore.Domain.Entities.GuidEntities;
using System.Collections.Generic;

namespace DddCore.Tests.Integration.Cars.BLL
{
    public class Car : GuidAggregateRootEntityBase
    {
        private string color;
        public string Color
        {
            get => color;
            set
            {
                Events.Add(new ColorChangedDomainEvent(this));
                color = value;
            }
        }
        public ICollection<Wheel> Wheels { get; set; } = new List<Wheel>();
    }
}