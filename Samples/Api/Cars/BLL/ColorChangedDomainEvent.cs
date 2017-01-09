using System;
using DddCore.Contracts.Domain.Events;

namespace Api.Cars.BLL
{
    public class ColorChangedDomainEvent : IDomainEvent
    {
        public ColorChangedDomainEvent(Car car)
        {
            Car = car;
        }

        public Car Car { get; }

        public DateTime CreatedAt { get; set; }
    }
}
