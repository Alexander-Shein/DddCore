using System.Collections.Generic;

namespace Api.Cars.SL
{
    public class CarVM
    {
        public string PublicKey { get; set; }
        public string Color { get; set; }
        public IEnumerable<WheelVM> Wheels { get; set; }
    }
}
