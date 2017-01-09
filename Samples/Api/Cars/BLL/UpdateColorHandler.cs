using DddCore.Contracts.Domain.Events;

namespace Api.Cars.BLL
{
    public class UpdateColorHandler : IDomainEventHandler<ColorChangedDomainEvent>
    {
        public void Handle(ColorChangedDomainEvent args)
        {
            args.Car.Color += "-Updated";
        }
    }
}
