using System.Collections.Generic;
using DddCore.Domain.Entities.GuidEntities;

namespace Api.Cars.BLL
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