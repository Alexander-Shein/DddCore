using DddCore.Domain.Entities.BusinessRules;
using FluentValidation;

namespace Api.Cars.BLL
{
    public class CarBusinessRulesValidator : BusinessRulesValidatorBase<Car>
    {
        public CarBusinessRulesValidator()
        {
            RuleFor(x => x.Color)
                .Length(0, 50);
        }
    }
}
