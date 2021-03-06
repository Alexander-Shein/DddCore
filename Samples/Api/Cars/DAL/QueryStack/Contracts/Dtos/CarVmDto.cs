﻿using System;
using System.Collections.Generic;

namespace Api.Cars.DAL.QueryStack.Contracts.Dtos
{
    public class CarVmDto
    {
        public Guid Id { get; set; }
        public string Color { get; set; }
        public IEnumerable<WheelVmDto> Wheels { get; set; }
    }
}
