using System;
using System.Threading.Tasks;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.State;
using DddCore.Contracts.BLL.Errors;
using FluentValidation;
using FluentValidation.Results;
using Severity = FluentValidation.Severity;

namespace DddCore.BLL.Domain.Entities.BusinessRules
{
    public abstract class BusinessRulesValidatorBase<T> : AbstractValidator<T>, IBusinessRulesValidator<T> where T : ICrudState
    {
        #region Public Methods

        public async Task<OperationResult> ValidateAsync(T instance)
        {
            var validationResult = await base.ValidateAsync(instance);
            return Map(validationResult);
        }

        public new OperationResult Validate(T instance)
        {
            var validationResult = base.Validate(instance);
            return Map(validationResult);
        }

        #endregion

        #region Private Methods

        OperationResult Map(ValidationResult validationResult)
        {
            if (validationResult.IsValid) return OperationResult.Succeed;

            var result = new OperationResult();

            foreach (var validationFailure in validationResult.Errors)
            {
                if (!Int32.TryParse(validationFailure.ErrorCode, out int errorCode))
                {
                    errorCode = -1;
                }

                var error = new Error
                {
                    Code = errorCode,
                    Description = validationFailure.ErrorMessage
                };

                switch (validationFailure.Severity)
                {
                    case Severity.Error:
                    {
                        error.Severity = Contracts.BLL.Errors.Severity.Error;
                        result.Errors.Add(error);
                        break;
                    }
                    case Severity.Info:
                    {
                        error.Severity = Contracts.BLL.Errors.Severity.Info;
                        result.Info.Add(error);
                        break;
                    }
                    case Severity.Warning:
                    {
                        error.Severity = Contracts.BLL.Errors.Severity.Warning;
                        result.Warnings.Add(error);
                        break;
                    }
                    default:
                    {
                        throw new NotSupportedException(validationFailure.Severity.ToString("G"));
                    }
                }
            }

            return result;
        }

        #endregion
    }
}