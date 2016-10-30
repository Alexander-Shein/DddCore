using System.Linq;
using System.Threading.Tasks;
using Contracts.Domain.Entities.Model;
using Contracts.Domain.Entities.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.Entities.Validation
{
    public abstract class AbstractEntityValidator<T> : AbstractValidator<T>, IEntityValidator<T> where T : ICrudState
    {
        #region Public Methods

        public async Task<EntityValidationResult> ValidateAsync(T instance)
        {
            var validationResult = await base.ValidateAsync(instance);
            return Map(validationResult);
        }

        #endregion

        #region Private Methods

        EntityValidationResult Map(ValidationResult validationResult)
        {
            var result = new EntityValidationResult
            {
                Errors = validationResult.Errors.Select(x => new BusinessRuleException(x.ErrorMessage)).ToList()
            };

            return result;
        }

        #endregion
    }
}