using System.Collections.Generic;
using DddCore.Domain.Entities.GuidEntities;

namespace Api.Cars.BLL
{
    public class Car : GuidAggregateRootEntityBase
    {
        public string Color { get; set; }

        public void ChangeColor(string color)
        {
            Color = color;
            Events.Add(new ColorChangedDomainEvent(this));
        }

        public ICollection<Wheel> Wheels { get; set; } = new List<Wheel>();
    }
}