using DddCore.Contracts.Domain.Events;

namespace DddCore.Tests.Integration.Cars.BLL
{
    public class UpdateColorHandler : IDomainEventHandler<ColorChangedDomainEvent>
    {
        public void Handle(ColorChangedDomainEvent args)
        {
            args.Car.Color += "-Updated";
        }
    }
}
