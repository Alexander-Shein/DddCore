using System;
using System.Collections.Generic;

namespace Api.Cars.SL.Contracts.Models
{
    public class CarVm
    {
        public Guid Id { get; set; }
        public string Color { get; set; }
        public IEnumerable<WheelVm> Wheels { get; set; }
    }
}