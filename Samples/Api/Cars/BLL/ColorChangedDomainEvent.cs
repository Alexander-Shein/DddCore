using DddCore.Contracts.Domain.Events;
using System;

namespace DddCore.Tests.Integration.Cars.BLL
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
