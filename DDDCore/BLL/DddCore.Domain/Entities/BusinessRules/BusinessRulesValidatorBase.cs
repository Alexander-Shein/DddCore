using System.Threading.Tasks;
using DddCore.Contracts.Domain.Entities.BusinessRules;
using DddCore.Contracts.Domain.Entities.Model;
using FluentValidation;
using FluentValidation.Results;

namespace DddCore.Domain.Entities.BusinessRules
{
    public abstract class BusinessRulesValidatorBase<T> : AbstractValidator<T>, IBusinessRulesValidator<T> where T : ICrudState
    {
        #region Public Methods

        public async Task<BusinessRulesValidationResult> ValidateAsync(T instance)
        {
            var validationResult = await base.ValidateAsync(instance);
            return Map(validationResult);
        }

        #endregion

        #region Private Methods

        BusinessRulesValidationResult Map(ValidationResult validationResult)
        {
            var result = new BusinessRulesValidationResult();

            foreach (var validationFailure in validationResult.Errors)
            {
                result.Errors.Add(new BrokenBusinessRule(validationFailure.PropertyName, validationFailure.ErrorMessage));
            }

            return result;
        }

        #endregion
    }
}