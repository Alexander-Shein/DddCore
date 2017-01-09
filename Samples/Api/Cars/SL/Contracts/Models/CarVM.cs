using System.Collections.Generic;

namespace Api.Cars.SL.Contracts.Models
{
    public class CarVM
    {
        public string PublicKey { get; set; }
        public string Color { get; set; }
        public IEnumerable<WheelVM> Wheels { get; set; }
    }
}
