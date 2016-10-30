using System.Linq;
using System.Threading.Tasks;
using Contracts.Domain.Entities.BusinessRules;
using Contracts.Domain.Entities.Model;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.Entities.BusinessRules
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
            var result = new BusinessRulesValidationResult
            {
                Errors = validationResult.Errors.Select(x => new BusinessRuleException(x.ErrorMessage)).ToList()
            };

            return result;
        }

        #endregion
    }
}